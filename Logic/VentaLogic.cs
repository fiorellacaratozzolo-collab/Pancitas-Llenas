using AutoMapper;
using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Interfaces;
using DataAccess.Models;
using Logic.Facade;
using Logic.MappingProfiles;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Centraliza las reglas de negocio, validaciones y la orquestación transaccional para las operaciones de venta.
    /// </summary>
    public class VentaLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;
        private readonly InventarioLogic _inventarioLogic;

        /// <summary>
        /// Inicializa una nueva instancia de la lógica de ventas junto con sus dependencias de acceso a datos e inventario.
        /// </summary>
        public VentaLogic()
        {
            _unitOfWork = new UnitOfWork();
            _inventarioLogic = new InventarioLogic(_unitOfWork);
        }

        /// <summary>
        /// Procesa una venta completa validando el stock y garantizando la atomicidad de la transacción (Venta, Detalles y Descuento de Inventario).
        /// </summary>
        public Guid RegistrarVenta(VentumDTO ventaDTO, List<VentaDetalleDTO> detallesDTO, Guid idSucursal)
        {
            if (ventaDTO == null || detallesDTO == null || !detallesDTO.Any())
            {
                throw new ArgumentException("La venta debe contener al menos un producto.");
            }

            Ventum venta = _mapper.Map<Ventum>(ventaDTO);
            List<VentaDetalle> detalles = _mapper.Map<List<VentaDetalle>>(detallesDTO);

            foreach (var detalle in detalles)
            {
                var stockActual = _unitOfWork.Stocks.GetBySucursalAndProducto(idSucursal, detalle.IdProducto);

                if (stockActual == null || stockActual.StockActual < detalle.Cantidad)
                {
                    throw new InvalidOperationException(string.Format("Stock insuficiente para el producto ID {0}. Disponible: {1}. Requerido: {2}.", detalle.IdProducto, stockActual?.StockActual ?? 0, detalle.Cantidad));
                }
            }

            try
            {
                Guid idVenta = _unitOfWork.Ventas.Create(venta);

                foreach (var detalle in detalles)
                {
                    detalle.IdVenta = idVenta;
                    _unitOfWork.VentaDetalles.Create(detalle);

                    _inventarioLogic.RestarStockPorVenta(idSucursal, detalle.IdProducto, detalle.Cantidad);
                }

                _unitOfWork.Complete();

                return idVenta;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al registrar la venta. La transacción ha sido revertida.", ex);
            }
        }

        /// <summary>
        /// Recupera y mapea la lista de ventas realizadas en una sucursal específica durante una fecha determinada.
        /// </summary>
        public List<VentumDTO> GetVentasPorSucursalYFecha(Guid idSucursal, DateTime fecha)
        {
            var todasLasVentas = _unitOfWork.Ventas.GetAll();

            var ventasFiltradas = todasLasVentas
                .Where(v => v.IdSucursal == idSucursal && v.FechaVenta.Date == fecha.Date)
                .ToList();

            return _mapper.Map<List<VentumDTO>>(ventasFiltradas);
        }

        /// <summary>
        /// Obtiene los detalles de los productos correspondientes a una venta específica, rellenando la información relacional como el nombre del producto.
        /// </summary>
        public List<VentaDetalleDTO> GetDetallesDeVenta(Guid idVenta)
        {
            var detalles = _unitOfWork.VentaDetalles.GetAll()
                .Where(d => d.IdVenta == idVenta)
                .ToList();

            var listaDTO = _mapper.Map<List<VentaDetalleDTO>>(detalles);

            foreach (var dto in listaDTO)
            {
                var producto = _unitOfWork.Productos.GetById(dto.IdProducto);
                if (producto != null)
                {
                    dto.NombreProducto = producto.NombreProducto;
                }
            }

            return listaDTO;
        }

        /// <summary>
        /// Revierte una venta procesada, devolviendo las cantidades al inventario de la sucursal y eliminando el registro original.
        /// </summary>
        public void AnularVenta(Guid idVenta, Guid idSucursal)
        {
            var detalles = _unitOfWork.VentaDetalles.GetAll().Where(d => d.IdVenta == idVenta).ToList();

            foreach (var detalle in detalles)
            {
                _inventarioLogic.AgregarOActualizarStock(idSucursal, detalle.IdProducto, detalle.Cantidad);
            }

            _unitOfWork.Ventas.Delete(idVenta);

            _unitOfWork.Complete();
        }

        /// <summary>
        /// Recupera el historial completo de ventas correspondientes a una sucursal específica.
        /// </summary>
        public List<VentumDTO> ObtenerVentasPorSucursal(Guid idSucursal)
        {
            var ventas = _unitOfWork.Ventas.GetBySucursal(idSucursal);
            return _mapper.Map<List<VentumDTO>>(ventas);
        }
    }
}