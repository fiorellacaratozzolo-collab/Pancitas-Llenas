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
    public class SucursalRepository : ISucursalRepository
    {
        private readonly PetShopDbContext _context;

        public SucursalRepository(PetShopDbContext context)
        {
            _context = context;
        }


        public Guid Create(Sucursal sucursal)
        {
            sucursal.IdSucursal = Guid.NewGuid();
            _context.Sucursals.Add(sucursal);
            return sucursal.IdSucursal;
        }

        public List<Sucursal> GetAll()
        {
            return _context.Sucursals.ToList();
        }

        public List<Sucursal> GetByTipoSucursal(int idTipoSucursal)
        {
            return _context.Sucursals
                           .Where(s => s.IdTipoSucursal == idTipoSucursal)
                           .ToList();
        }

        public Sucursal? GetById(Guid id)
        {
            return _context.Sucursals.Find(id);
        }

        public void Update(Sucursal sucursal)
        {
            if (sucursal == null)
                throw new ArgumentNullException(nameof(sucursal));
            var sucursalDb = _context.Sucursals.Find(sucursal.IdSucursal);

            if (sucursalDb != null)
            {
                _context.Entry(sucursalDb).CurrentValues.SetValues(sucursal);
            }
        }

        public void Delete(Guid id)
        {
            var sucursal = _context.Sucursals.Find(id);
            if (sucursal != null)
            {
                _context.Sucursals.Remove(sucursal);
            }
        }

        public List<Sucursal> SearchByDireccion(string direccionFragment)
        {
            string search = direccionFragment.ToLower();

            return _context.Sucursals
                           .Where(s => s.Direccion.ToLower().Contains(search))
                           .ToList();
        }
    }
}
