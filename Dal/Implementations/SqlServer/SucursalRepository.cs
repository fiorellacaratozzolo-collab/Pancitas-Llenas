using Dal.EntityFramework;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Implementations.SqlServer
{
    public class SucursalRepository
    {
        private readonly PetShopDBContext _context;

        public SucursalRepository(PetShopDBContext context)
        {
            _context = context;
        }

        public List<Sucursal> GetAll()
        {
            return _context.Sucursales.ToList();
        }
        public Sucursal GetById(Guid id)
        {
            return _context.Sucursales.Find(id);
        }

        public void Add(Sucursal sucursal)
        {
            _context.Sucursales.Add(sucursal);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var sucursal = _context.Sucursales.Find(id);
            if (sucursal != null)
            {
                _context.Sucursales.Remove(sucursal);
                _context.SaveChanges();
            }
        }
    }
}
