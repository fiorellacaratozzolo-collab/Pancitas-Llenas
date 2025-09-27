using DataAccess.EntityFramework;
using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementations.SqlServer
{
    public class VentaRepository : IVentaRepository
    {
        private readonly PetShopDBContext _context;

        public VentaRepository()
        {
            _context = new PetShopDBContext();
        }

        public Guid Create(Ventum venta, List<VentaDetalle> detalles)
        {
            // 1. Iniciar Transacción para asegurar la atomicidad de Venta y sus Detalles
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Generar ID y la fecha de la venta si no vinieron ya
                    venta.IdVenta = Guid.NewGuid();
                    // Opcional: Asignar un NumeroVenta secuencial si es necesario.

                    // 2. Agregar la Venta principal
                    _context.Venta.Add(venta);

                    // 3. Preparar y agregar los Detalles de Venta
                    foreach (var detalle in detalles)
                    {
                        detalle.IdVentaDetalle = Guid.NewGuid();
                        detalle.IdVenta = venta.IdVenta; // Establecer la FK al ID de la Venta principal
                        _context.VentaDetalles.Add(detalle);

                        // Opcional: Lógica para descontar stock del Producto (Requiere IProductoRepository)
                    }

                    // 4. Guardar todos los cambios
                    _context.SaveChanges();

                    // 5. Confirmar la transacción
                    transaction.Commit();

                    return venta.IdVenta;
                }
                catch (Exception)
                {
                    // Si algo falla, revertir todos los cambios
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
