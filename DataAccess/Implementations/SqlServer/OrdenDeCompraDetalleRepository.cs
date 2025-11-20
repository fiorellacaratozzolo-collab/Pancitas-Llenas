using DataAccess.Contexts;
using DataAccess.Interfaces;
using DataAccess.Models;
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
    }
}
