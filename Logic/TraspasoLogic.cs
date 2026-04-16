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
        public TraspasoLogic()
        {
            _unitOfWork = new UnitOfWork();
        }

        public Guid GenerarSolicitud(SolicitudDeTraspasoDeProductoDTO solicitudDTO, List<SolicitudDeTraspasoDeProductosDetalleDTO> detallesDTO)
        {
            if (detallesDTO == null || !detallesDTO.Any())
            {
                throw new ArgumentException("La solicitud debe contener al menos un producto.");
            }

            // Mapeamos de DTO a Entidades de EF
            var solicitud = _mapper.Map<SolicitudDeTraspasoDeProducto>(solicitudDTO);
            var detalles = _mapper.Map<List<SolicitudDeTraspasoDeProductosDetalle>>(detallesDTO);
            solicitud.IdSucursalDestinoNavigation = null!; // Evitamos problemas de navegación al crear la solicitud
            solicitud.IdSucursalOrigenNavigation = null!; // Evitamos problemas de navegación al crear la solicitud

            try
            {
                solicitud.IdEstadoStp = 1;
                Guid idSolicitud = _unitOfWork.SolicitudesTraspaso.Create(solicitud);

                foreach (var detalle in detalles)
                {
                    detalle.IdSolicitudDeTraspasoDeProductos = idSolicitud;
                    detalle.IdSolicitudDeTraspasoDeProductosNavigation = null;
                    detalle.IdProductoNavigation = null;
                    if (string.IsNullOrWhiteSpace(detalle.Unidad))
                    {
                        detalle.Unidad = "KG";
                    }
                    _unitOfWork.SolicitudesTraspasoDetalles.Create(detalle);
                }

                _unitOfWork.Complete();

                return idSolicitud;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al registrar la solicitud de traspaso.", ex);
            }
        }

        public void AprobarTraspaso(Guid idSolicitud)
        {
            var solicitud = _unitOfWork.SolicitudesTraspaso.GetById(idSolicitud);
            var detalles = _unitOfWork.SolicitudesTraspasoDetalles.GetByIdSolicitud(idSolicitud);

            try
            {
                foreach (var item in detalles)
                {
                    // 1. Descontamos stock del Origen (Depósito)
                    var stockOrigen = _unitOfWork.Stocks.GetByProductoYSucursal(item.IdProducto, solicitud.IdSucursalOrigen);
                    if (stockOrigen != null)
                    {
                        stockOrigen.StockActual -= item.Cantidad;
                        _unitOfWork.Stocks.Update(stockOrigen);
                    }

                    // 2. Sumamos stock al Destino (Sucursal Venta)
                    var stockDestino = _unitOfWork.Stocks.GetByProductoYSucursal(item.IdProducto, solicitud.IdSucursalDestino);
                    if (stockDestino != null)
                    {
                        stockDestino.StockActual += item.Cantidad;
                        _unitOfWork.Stocks.Update(stockDestino);
                    }
                    else
                    {
                        // Si la sucursal de destino nunca tuvo este producto, le creamos el registro
                        var nuevoStock = new StockPorSucursal
                        {
                            IdStockSucursal = Guid.NewGuid(),
                            IdProducto = item.IdProducto,
                            IdSucursal = solicitud.IdSucursalDestino,
                            StockActual = item.Cantidad,
                            StockDeseado = 0,
                            IdEstadoStock = 2
                        };
                        _unitOfWork.Stocks.Create(nuevoStock);
                    }
                }

                // 3. Cambiamos estado a Aprobado (2)
                solicitud.IdEstadoStp = 2; 
                _unitOfWork.SolicitudesTraspaso.Update(solicitud);

                // 4. Commiteamos toda la transacción junta
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al aprobar el traspaso y mover el stock.", ex);
            }
        }

        public List<SolicitudDeTraspasoDeProductoDTO> ObtenerSolicitudesPendientes(Guid idSucursalOrigen)
        {
            var solicitudes = _unitOfWork.SolicitudesTraspaso.GetPendientesPorSucursalOrigen(idSucursalOrigen);
            return _mapper.Map<List<SolicitudDeTraspasoDeProductoDTO>>(solicitudes);
        }

        public List<SolicitudDeTraspasoDeProductosDetalleDTO> ObtenerDetallesPorSolicitud(Guid idSolicitud)
        {
            var detalles = _unitOfWork.SolicitudesTraspasoDetalles.GetByIdSolicitud(idSolicitud);
            return _mapper.Map<List<SolicitudDeTraspasoDeProductosDetalleDTO>>(detalles);
        }

        public void RechazarTraspaso(Guid idSolicitud)
        {
            var solicitud = _unitOfWork.SolicitudesTraspaso.GetById(idSolicitud);

            try
            {
                solicitud.IdEstadoStp = 3;
                _unitOfWork.SolicitudesTraspaso.Update(solicitud);

                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al rechazar el traspaso.", ex);
            }
        }

        public List<HistorialTraspasoDTO> ObtenerHistorialTraspasos(Guid idSucursalActual)
        {

            // 1. Buscamos en tu repositorio (acordate de cambiar el "2" por tu estado de "Aprobado")
            var solicitudesBd = _unitOfWork.SolicitudesTraspaso.GetAll()
                .Where(t => (t.IdSucursalOrigen == idSucursalActual || t.IdSucursalDestino == idSucursalActual)
                         && t.IdEstadoStp == 2)
                .OrderByDescending(t => t.FechaStp) // Usamos tu campo FechaStp
                .ToList();

            var listaHistorial = new List<HistorialTraspasoDTO>();

            foreach (var t in solicitudesBd)
            {
                // 2. Usamos el nombre EXACTO de tu colección de detalles
                foreach (var detalle in t.SolicitudDeTraspasoDeProductosDetalles)
                {
                    var dto = new HistorialTraspasoDTO
                    {
                        Fecha = t.FechaStp, // Tu campo de fecha

                        // NOTA: Borré el UsuarioResponsable porque no está en tu base de datos.

                        // Asumo que tu detalle tiene una navegación al Producto, chequeá este nombre:
                        Producto = detalle.IdProductoNavigation?.NombreProducto ?? "Desconocido",
                        Cantidad = detalle.Cantidad
                    };

                    // 3. Definimos si entró o salió mercadería
                    if (t.IdSucursalDestino == idSucursalActual)
                    {
                        dto.TipoMovimiento = "INGRESO";
                        dto.SucursalInvolucrada = $"Desde: {t.IdSucursalOrigenNavigation?.NombreSucursal}";
                    }
                    else
                    {
                        dto.TipoMovimiento = "EGRESO";
                        dto.SucursalInvolucrada = $"Hacia: {t.IdSucursalDestinoNavigation?.NombreSucursal}";
                    }

                    listaHistorial.Add(dto);
                }
            }

            return listaHistorial;
        }

    }
}
