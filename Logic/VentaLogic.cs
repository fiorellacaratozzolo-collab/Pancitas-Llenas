using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Models;
using Logic.Facade;
using Logic.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class VentaLogic
    {
        /// <summary>
        /// Procesa una venta completa, incluyendo la persistencia y el descuento de stock.
        /// </summary>
        public Guid RegistrarVenta(Ventum venta, List<VentaDetalle> detalles, Guid idSucursal)
        {
            if (venta == null || detalles == null || !detalles.Any())
            {
                throw new ArgumentException("La venta debe contener al menos un producto.");
            }

            using (var unitOfWork = new UnitOfWork())
            {
                // El servicio de inventario se inicializa aquí
                var inventarioLogic = new InventarioLogic();

                // 1. VALIDACIÓN DE STOCK
                foreach (var detalle in detalles)
                {
                    var stockActual = unitOfWork.Stocks.GetBySucursalAndProducto(idSucursal, detalle.IdProducto);

                    if (stockActual == null || stockActual.StockActual < detalle.Cantidad)
                    {
                        // (StockInsuficienteException)
                        throw new InvalidOperationException($"Stock insuficiente para el producto ID {detalle.IdProducto}. Disponible: {stockActual?.StockActual ?? 0}. Requerido: {detalle.Cantidad}.");
                    }
                }

                try
                {
                    // 2. CREACIÓN DE LA VENTA EN EL ENCABEZADO Y DETALLE
                    Guid idVenta = unitOfWork.Ventas.Create(venta);

                    foreach (var detalle in detalles)
                    {
                        detalle.IdVenta = idVenta;
                        unitOfWork.VentaDetalles.Create(detalle);

                        // 3. DESCUENTO DE STOCK
                        // La lógica de inventario ya maneja su propio UoW y commit.
                        inventarioLogic.RestarStockPorVenta(idSucursal, detalle.IdProducto, detalle.Cantidad);
                    }

                    // 4. COMMIT de la VENTA y DETALLES
                    unitOfWork.Complete();

                    return idVenta;
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Error al registrar la venta. No se aplicaron cambios.", ex);
                }
            }
        }
    }
}