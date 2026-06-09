using DataAccess.Contexts;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementations.SqlServer
{
    /// <summary>
    /// Implementación concreta del repositorio para la tabla de resolución de muchos a muchos entre Proveedores y Productos.
    /// </summary>
    public class ProveedorProductoRepository : IProveedorProductoRepository
    {
        private readonly PetShopDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio.
        /// </summary>
        public ProveedorProductoRepository(PetShopDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene la colección completa de vínculos, asegurando la carga de los objetos relacionados y desconectando el seguimiento de cambios (AsNoTracking).
        /// </summary>
        public List<ProveedorProducto> GetAll()
        {
            return _context.ProveedorProductos
                           .Include(pp => pp.IdProductoNavigation)
                           .Include(pp => pp.IdProveedorNavigation)
                           .AsNoTracking()
                           .ToList();
        }

        /// <summary>
        /// Registra un nuevo vínculo entre un proveedor y un producto.
        /// </summary>
        public Guid Create(ProveedorProducto proveedorProducto)
        {
            _context.ProveedorProductos.Add(proveedorProducto);
            return proveedorProducto.IdProveedor;
        }
    }
}
