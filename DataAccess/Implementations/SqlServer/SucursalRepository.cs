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
    /// Implementación concreta del repositorio de sucursales para el manejo de persistencia relacional.
    /// </summary>
    public class SucursalRepository : ISucursalRepository
    {
        private readonly PetShopDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio inyectando el contexto de base de datos compartido.
        /// </summary>
        public SucursalRepository(PetShopDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Persiste una nueva sucursal en la base de datos y retorna su GUID generado.
        /// </summary>
        public Guid Create(Sucursal sucursal)
        {
            if (sucursal == null) throw new ArgumentNullException(nameof(sucursal));

            if (sucursal.IdSucursal == Guid.Empty)
            {
                sucursal.IdSucursal = Guid.NewGuid();
            }

            _context.Sucursals.Add(sucursal);
            return sucursal.IdSucursal;
        }

        /// <summary>
        /// Obtiene el catálogo completo de sucursales disponibles en el sistema.
        /// </summary>
        public List<Sucursal> GetAll()
        {
            return _context.Sucursals.ToList();
        }

        /// <summary>
        /// Filtra y recupera las sucursales basándose en su tipo o categoría.
        /// </summary>
        public List<Sucursal> GetByTipoSucursal(int idTipoSucursal)
        {
            return _context.Sucursals.Where(s => s.IdTipoSucursal == idTipoSucursal).ToList();
        }

        /// <summary>
        /// Recupera una sucursal específica a partir de su identificador único.
        /// </summary>
        public Sucursal? GetById(Guid id)
        {
            return _context.Sucursals.Find(id);
        }

        /// <summary>
        /// Actualiza los valores de una sucursal existente reemplazando el estado de la entidad desconectada.
        /// </summary>
        public void Update(Sucursal sucursal)
        {
            if (sucursal == null) throw new ArgumentNullException(nameof(sucursal));

            var sucursalDb = _context.Sucursals.Find(sucursal.IdSucursal);

            if (sucursalDb != null)
            {
                _context.Entry(sucursalDb).CurrentValues.SetValues(sucursal);
            }
        }

        /// <summary>
        /// Realiza un Borrado Lógico de la sucursal (Activo = false).
        /// </summary>
        public void Delete(Guid id)
        {
            var sucursal = _context.Sucursals.Find(id);
            if (sucursal != null)
            {
                sucursal.Activo = false;
                _context.Sucursals.Update(sucursal);
            }
        }

        /// <summary>
        /// Reactiva una sucursal previamente deshabilitada (Activo = true).
        /// </summary>
        public void Habilitar(Guid id)
        {
            var sucursal = _context.Sucursals.Find(id);
            if (sucursal != null)
            {
                sucursal.Activo = true;
                _context.Sucursals.Update(sucursal);
            }
        }

        /// <summary>
        /// Busca sucursales cuya dirección contenga el fragmento de texto proporcionado, ignorando mayúsculas y minúsculas.
        /// </summary>
        public List<Sucursal> SearchByDireccion(string direccionFragment)
        {
            string search = direccionFragment.ToLower();
            return _context.Sucursals.Where(s => s.Direccion.ToLower().Contains(search)).ToList();
        }
    }
}