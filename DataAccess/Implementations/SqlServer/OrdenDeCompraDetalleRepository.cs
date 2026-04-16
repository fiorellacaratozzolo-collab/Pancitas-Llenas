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
    public class OrdenDeCompraDetalleRepository : IOrdenDeCompraDetalleRepository
    {
        private readonly PetShopDbContext _context;

        public OrdenDeCompraDetalleRepository(PetShopDbContext context)
        {
            _context = context;
        }

        public void AddRange(IEnumerable<OrdenDeCompraDetalle> detalles)
        {
            _context.OrdenDeCompraDetalles.AddRange(detalles);
        }

        public List<OrdenDeCompraDetalle> GetByIdOrdenCompra(Guid idOrdenCompra)
        {
            return _context.OrdenDeCompraDetalles
                .Include(d => d.IdProductoNavigation)
                .Where(d => d.IdOrdenDeCompra == idOrdenCompra)
                .ToList();
        }
    }
}
