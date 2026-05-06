using DataAccess.Contexts;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementations.SqlServer
{
    /// <summary>
    /// Implementación concreta del repositorio para los renglones de detalle de las órdenes de compra a proveedores.
    /// </summary>
    public class OrdenDeCompraDetalleRepository : IOrdenDeCompraDetalleRepository
    {
        private readonly PetShopDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio.
        /// </summary>
        public OrdenDeCompraDetalleRepository(PetShopDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Inserta de forma masiva una lista de detalles para una orden de compra generada.
        /// </summary>
        public void AddRange(IEnumerable<OrdenDeCompraDetalle> detalles)
        {
            _context.OrdenDeCompraDetalles.AddRange(detalles);
        }

        /// <summary>
        /// Recupera los detalles correspondientes a una orden específica asegurando la carga relacional del producto.
        /// </summary>
        public List<OrdenDeCompraDetalle> GetByIdOrdenCompra(Guid idOrdenCompra)
        {
            return _context.OrdenDeCompraDetalles
                .Include(d => d.IdProductoNavigation)
                .Where(d => d.IdOrdenDeCompra == idOrdenCompra)
                .ToList();
        }
    }
}
