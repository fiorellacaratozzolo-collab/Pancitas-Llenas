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

        public VentaRepository(PetShopDBContext context)
        {
            _context = context;
        }

        public Guid Create(Ventum venta)
        {
            venta.IdVenta = Guid.NewGuid();
            _context.Venta.Add(venta);
            return venta.IdVenta;
        }
    }
}
