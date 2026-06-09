using DataAccess.Contexts;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Implementations.SqlServer
{
    /// <summary>
    /// Implementación concreta del repositorio de proveedores utilizando Entity Framework.
    /// </summary>
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly PetShopDbContext _context;

        public ProveedorRepository(PetShopDbContext context)
        {
            _context = context;
        }

        public Guid Create(Proveedor proveedor)
        {
            if (proveedor == null) throw new ArgumentNullException(nameof(proveedor), "El proveedor no puede ser nulo.");

            if (proveedor.IdProveedor == Guid.Empty)
            {
                proveedor.IdProveedor = Guid.NewGuid();
            }

            _context.Proveedors.Add(proveedor);
            return proveedor.IdProveedor;
        }

        /// <summary>
        /// Realiza un Borrado Lógico del proveedor (Activo = false).
        /// </summary>
        public void Delete(Guid id)
        {
            var proveedor = _context.Proveedors.Find(id);
            if (proveedor != null)
            {
                proveedor.Activo = false;
                _context.Proveedors.Update(proveedor);
            }
        }

        /// <summary>
        /// Reactiva un proveedor previamente deshabilitado (Activo = true).
        /// </summary>
        public void Habilitar(Guid id)
        {
            var proveedor = _context.Proveedors.Find(id);
            if (proveedor != null)
            {
                proveedor.Activo = true;
                _context.Proveedors.Update(proveedor);
            }
        }

        /// <summary>
        /// Actualiza los campos escalares de un proveedor existente.
        /// </summary>
        public void Update(Proveedor proveedor)
        {
            var proveedorDb = _context.Proveedors.Find(proveedor.IdProveedor);
            if (proveedorDb != null)
            {
                _context.Entry(proveedorDb).CurrentValues.SetValues(proveedor);
            }
        }

        /// <summary>
        /// Obtiene el catálogo materializado de todos los proveedores registrados.
        /// </summary>
        public List<Proveedor> GetAll()
        {
            return _context.Proveedors.ToList();
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
