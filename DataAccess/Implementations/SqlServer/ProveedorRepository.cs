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
    /// Implementación concreta del repositorio de proveedores utilizando Entity Framework.
    /// </summary>
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly PetShopDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio.
        /// </summary>
        public ProveedorRepository(PetShopDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Inserta un nuevo proveedor en la base de datos previa validación de nulidad.
        /// </summary>
        public Guid Create(Proveedor proveedor)
        {
            if (proveedor == null)
            {
                throw new ArgumentNullException(nameof(proveedor), "El proveedor no puede ser nulo.");
            }

            proveedor.IdProveedor = Guid.NewGuid();
            _context.Proveedors.Add(proveedor);

            return proveedor.IdProveedor;
        }

        /// <summary>
        /// Obtiene el catálogo materializado de todos los proveedores registrados.
        /// </summary>
        public List<Proveedor> GetAll()
        {
            return _context.Proveedors.ToList();
        }

        /// <summary>
        /// Elimina físicamente el registro de un proveedor de la base de datos.
        /// </summary>
        public void Delete(Guid id)
        {
            var proveedor = _context.Proveedors.Find(id);
            if (proveedor != null)
            {
                _context.Proveedors.Remove(proveedor);
            }
        }

        /// <summary>
        /// Busca la primera coincidencia de un proveedor en base a su número de CUIT.
        /// </summary>
        public Proveedor? GetByCuit(int cuit)
        {
            return _context.Proveedors.FirstOrDefault(p => p.Cuit == cuit);
        }

        /// <summary>
        /// Recupera un proveedor específico por su clave primaria.
        /// </summary>
        public Proveedor? GetById(Guid id)
        {
            return _context.Proveedors.Find(id);
        }
    }
}
