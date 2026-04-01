using AutoMapper;
using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Models;
using Logic.MappingProfiles;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class TraspasoLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;

        // Constructor que instancia el UnitOfWork (Igual a tu ejemplo)
        public TraspasoLogic()
        {
            _unitOfWork = new UnitOfWork();
        }

        // Constructor sobrecargado para permitir UnitOfWork compartido (Útil para transacciones complejas)
        public TraspasoLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Guid CrearSolicitud(SolicitudDeTraspasoDeProductoDTO solicitudDTO)
        {
            if (solicitudDTO.SolicitudDeTraspasoDeProductosDetalles == null || !solicitudDTO.SolicitudDeTraspasoDeProductosDetalles.Any())
                throw new ArgumentException("La solicitud debe tener al menos un detalle.");

            // 1. Mapear DTO a Entidad
            var solicitud = _mapper.Map<SolicitudDeTraspasoDeProducto>(solicitudDTO);
            solicitud.FechaStp = DateTime.Today;
            solicitud.IdEstadoStp = 1; // Pendiente

            // 2. Mapear Detalles y enlazar manualmente (Siguiendo tu patrón)
            solicitud.SolicitudDeTraspasoDeProductosDetalles = new List<SolicitudDeTraspasoDeProductosDetalle>();
            foreach (var detalleDTO in solicitudDTO.SolicitudDeTraspasoDeProductosDetalles)
            {
                var detalle = _mapper.Map<SolicitudDeTraspasoDeProductosDetalle>(detalleDTO);
                detalle.IdSolicitudDeTraspasoDeProductos = solicitud.IdSolicitudDeTraspasoDeProductos;

                // Buscamos datos técnicos del maestro de productos
                var productoMaestro = _unitOfWork.Productos.GetById(detalle.IdProducto);
                if (productoMaestro != null)
                {
                    detalle.PesoNeto = productoMaestro.PesoNeto ?? 0;
                    detalle.Unidad = productoMaestro.Unidad ?? "UN";
                }

                solicitud.SolicitudDeTraspasoDeProductosDetalles.Add(detalle);
            }

            // 3. Persistencia y Commit (Uso de repositorios específicos)
            Guid id = _unitOfWork.SolicitudesTraspaso.Create(solicitud);
            _unitOfWork.SolicitudesTraspasoDetalles.AddRange(solicitud.SolicitudDeTraspasoDeProductosDetalles.ToList());

            _unitOfWork.Complete();

            return id;
        }

        public List<SolicitudDeTraspasoDeProductoDTO> ObtenerTodas()
        {
            var solicitudes = _unitOfWork.SolicitudesTraspaso.GetAll();
            return _mapper.Map<List<SolicitudDeTraspasoDeProductoDTO>>(solicitudes);
        }

        public SolicitudDeTraspasoDeProductoDTO ObtenerPorId(Guid id)
        {
            var solicitud = _unitOfWork.SolicitudesTraspaso.GetById(id);
            if (solicitud == null)
                throw new KeyNotFoundException($"No se encontró la solicitud con ID {id}");

            return _mapper.Map<SolicitudDeTraspasoDeProductoDTO>(solicitud);
        }

        // --- GESTIÓN DE ESTADO Y STOCK ---

        public void RechazarSolicitud(Guid solicitudId)
        {
            var solicitud = _unitOfWork.SolicitudesTraspaso.GetById(solicitudId);
            if (solicitud == null)
                throw new KeyNotFoundException($"No se encontró la solicitud con ID {solicitudId}");

            solicitud.IdEstadoStp = 3; // Rechazada
            _unitOfWork.SolicitudesTraspaso.Update(solicitud);
            _unitOfWork.Complete();
        }

        public void AprobarYProcesarStock(Guid solicitudId)
        {
            var solicitud = _unitOfWork.SolicitudesTraspaso.GetById(solicitudId);
            if (solicitud == null)
                throw new KeyNotFoundException($"No se encontró la solicitud con ID {solicitudId}");

            if (solicitud.IdEstadoStp != 1)
                throw new InvalidOperationException("La solicitud ya no se encuentra en estado Pendiente.");

            foreach (var detalle in solicitud.SolicitudDeTraspasoDeProductosDetalles)
            {
                // 1. Descuento en Origen (Depósito)
                var stockOrigen = _unitOfWork.StocksPorSucursal.GetBySucursalAndProducto(solicitud.IdSucursalOrigen, detalle.IdProducto);
                if (stockOrigen == null || stockOrigen.StockActual < detalle.Cantidad)
                    throw new Exception($"Stock insuficiente en origen para el producto ID: {detalle.IdProducto}");

                stockOrigen.StockActual -= detalle.Cantidad;
                _unitOfWork.StocksPorSucursal.Update(stockOrigen);

                // 2. Aumento en Destino (Sucursal solicitante)
                var stockDestino = _unitOfWork.StocksPorSucursal.GetBySucursalAndProducto(solicitud.IdSucursalDestino, detalle.IdProducto);

                if (stockDestino == null)
                {
                    var nuevoStock = new StockPorSucursal
                    {
                        IdStockSucursal = Guid.NewGuid(),
                        IdSucursal = solicitud.IdSucursalDestino,
                        IdProducto = detalle.IdProducto,
                        StockActual = detalle.Cantidad,
                        StockDeseado = 0,
                        IdEstadoStock = 1 // Activo
                    };
                    _unitOfWork.StocksPorSucursal.Create(nuevoStock);
                }
                else
                {
                    stockDestino.StockActual += detalle.Cantidad;
                    _unitOfWork.StocksPorSucursal.Update(stockDestino);
                }
            }

            // 3. Marcar solicitud como Aprobada
            solicitud.IdEstadoStp = 2; // Aprobada/Procesada
            _unitOfWork.SolicitudesTraspaso.Update(solicitud);

            // 4. Commit final de toda la operación
            _unitOfWork.Complete();
        }
    }
}
