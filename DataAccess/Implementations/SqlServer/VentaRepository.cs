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
    public class VentaRepository : IVentaRepository
    {
        private readonly PetShopDbContext _context;

        public VentaRepository(PetShopDbContext context)
        {
            _context = context;
        }

        public Guid Create(Ventum venta)
        {
            venta.IdVenta = Guid.NewGuid();
            _context.Venta.Add(venta);
            return venta.IdVenta;
        }

        public List<Ventum> GetAll()
        {
            return _context.Venta
                .Include(v => v.IdClienteNavigation)
                .ToList();
        }
        public void Delete(Guid id)
        {
            var ventaAEliminar = _context.Venta.Find(id);
            if (ventaAEliminar != null)
            {
                _context.Venta.Remove(ventaAEliminar);
            }
        }
        public List<Ventum> GetBySucursal(Guid idSucursal)
        {
            return _context.Venta 
                .Include(v => v.IdClienteNavigation) 
                .Where(v => v.IdSucursal == idSucursal)
                .ToList();
        }
    }
}
