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
    public class SucursalRepository : ISucursalRepository
    {
        private readonly PetShopDBContext _context;

        public SucursalRepository(PetShopDBContext context)
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
            // Filtra por la clave foránea IdTipoSucursal
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
            _context.Sucursals.Update(sucursal);
        }

        public void Delete(Guid id)
        {
            var sucursal = _context.Sucursals.Find(id);
            if (sucursal != null)
            {
                _context.Sucursals.Remove(sucursal); // Borrado físico
                // Lógica de Soft Delete si el modelo Sucursal tiene una propiedad 'Activo'
            }
        }

        public List<Sucursal> SearchByDireccion(string direccionFragment)
        {
            // Usamos ToLower() y Contains() para una búsqueda parcial insensible a mayúsculas/minúsculas.
            string search = direccionFragment.ToLower();

            return _context.Sucursals
                           .Where(s => s.Direccion.ToLower().Contains(search))
                           .ToList();
        }
    }
}
