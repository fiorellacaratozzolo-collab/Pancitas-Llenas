using DataAccess.Contexts;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementations.SqlServer
{
    /// <summary>
    /// Implementación concreta del repositorio para los renglones de detalle de las órdenes de pedido interno.
    /// </summary>
    public class OrdenDePedidoDetalleRepository : IOrdenDePedidoDetalleRepository
    {
        private readonly PetShopDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio.
        /// </summary>
        public OrdenDePedidoDetalleRepository(PetShopDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Inserta una colección completa de detalles asociados a una orden de pedido.
        /// </summary>
        public void AddRange(IEnumerable<OrdenDePedidoDetalle> detalles)
        {
            _context.OrdenDePedidoDetalles.AddRange(detalles);
        }

        /// <summary>
        /// Recupera todos los renglones de una orden específica, incluyendo los datos del producto involucrado.
        /// </summary>
        public List<OrdenDePedidoDetalle> GetByIdOrden(Guid idOrden)
        {
            return _context.OrdenDePedidoDetalles
                .Include(d => d.IdProductoNavigation)
                .Where(d => d.IdOrdenDePedido == idOrden)
                .ToList();
        }
    }
}
