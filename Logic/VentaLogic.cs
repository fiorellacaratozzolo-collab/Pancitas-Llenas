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

    public class VentaLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;
        private readonly InventarioLogic _inventarioLogic;

        public VentaLogic()
        {
            _unitOfWork = new UnitOfWork();
            _inventarioLogic = new InventarioLogic();
        }

        /// <summary>
        /// Procesa una venta completa, garantizando la atomicidad de la transacción.
        /// </summary>
        public Guid RegistrarVenta(VentumDTO ventaDTO, List<VentaDetalleDTO> detallesDTO, Guid idSucursal)
        {
            if (ventaDTO == null || detallesDTO == null || !detallesDTO.Any())
            {
                throw new ArgumentException("La venta debe contener al menos un producto.");
            }

            Ventum venta = _mapper.Map<Ventum>(ventaDTO);
            List<VentaDetalle> detalles = _mapper.Map<List<VentaDetalle>>(detallesDTO);

            // 1. VALIDACIÓN DE STOCK (Usando el UoW de VentaLogic para la consulta)
            foreach (var detalle in detalles)
            {
                var stockActual = _unitOfWork.Stocks.GetBySucursalAndProducto(idSucursal, detalle.IdProducto);

                if (stockActual == null || stockActual.StockActual < detalle.Cantidad)
                {
                    throw new InvalidOperationException($"Stock insuficiente para el producto ID {detalle.IdProducto}. Disponible: {stockActual?.StockActual ?? 0}. Requerido: {detalle.Cantidad}.");
                }
            }

            try
            {
                // 2. CREACIÓN DE LA VENTA
                Guid idVenta = _unitOfWork.Ventas.Create(venta);

                foreach (var detalle in detalles)
                {
                    detalle.IdVenta = idVenta;
                    _unitOfWork.VentaDetalles.Create(detalle);

                    // 3. DESCUENTO DE STOCK 
                    // Se llama a la lógica de inventario, que solo modifica la entidad en memoria.
                    _inventarioLogic.RestarStockPorVenta(idSucursal, detalle.IdProducto, detalle.Cantidad);
                }

                // 4. COMMIT ATÓMICO: Guarda Venta, Detalle Y los cambios hechos al Stock por InventarioLogic.
                _unitOfWork.Complete();

                return idVenta;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al registrar la venta. La transacción ha sido revertida.", ex);
            }
        }       
    }
}