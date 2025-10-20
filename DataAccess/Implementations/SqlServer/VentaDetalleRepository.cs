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
    public class VentaDetalleRepository : IVentaDetalleRepository
    {
        private readonly PetShopDbContext _context;

        public VentaDetalleRepository(PetShopDbContext context)
        {
            _context = context;
        }

        public void Create(VentaDetalle detalle)
        {
            detalle.IdVentaDetalle = Guid.NewGuid();
            _context.VentaDetalles.Add(detalle);
        }
    }
}
