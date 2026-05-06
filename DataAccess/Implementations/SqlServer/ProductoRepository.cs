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
    /// <summary>
    /// Implementación concreta del repositorio de productos, gestionando tanto la entidad base como sus relaciones obligatorias.
    /// </summary>
    public class ProductoRepository : IProductoRepository
    {
        private readonly PetShopDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio inyectando el contexto de Entity Framework.
        /// </summary>
        public ProductoRepository(PetShopDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Inserta un nuevo producto en el catálogo y crea automáticamente su vínculo inicial en la tabla intermedia con el proveedor especificado.
        /// </summary>
        public Guid Create(Producto producto, Guid idProveedor)
        {
            if (producto == null) throw new ArgumentNullException(nameof(producto));
            if (idProveedor == Guid.Empty) throw new ArgumentException("El ID de Proveedor no puede ser vacío.", nameof(idProveedor));

            producto.IdProducto = Guid.NewGuid();
            _context.Productos.Add(producto);

            var proveedorProducto = new ProveedorProducto
            {
                IdProveedorProducto = Guid.NewGuid(),
                IdProducto = producto.IdProducto,
                IdProveedor = idProveedor
            };
            _context.ProveedorProductos.Add(proveedorProducto);

            return producto.IdProducto;
        }

        /// <summary>
        /// Elimina físicamente un producto de la base de datos a partir de su identificador.
        /// </summary>
        public void Delete(Guid id)
        {
            var producto = _context.Productos.Find(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }
        }

        /// <summary>
        /// Obtiene el catálogo de productos vinculados a un proveedor específico navegando a través de la tabla intermedia.
        /// </summary>
        public List<Producto> GetByProveedor(Guid idProveedor)
        {
            var productos = _context.ProveedorProductos
                                    .Where(pp => pp.IdProveedor == idProveedor)
                                    .Select(pp => pp.IdProductoNavigation)
                                    .ToList();

            return productos;
        }

        /// <summary>
        /// Recupera un producto específico mediante su identificador único.
        /// </summary>
        public Producto? GetById(Guid id)
        {
            return _context.Productos.Find(id);
        }

        /// <summary>
        /// Obtiene el listado completo de todos los productos registrados.
        /// </summary>
        public List<Producto> GetAll()
        {
            return _context.Productos.ToList();
        }
    }
}
