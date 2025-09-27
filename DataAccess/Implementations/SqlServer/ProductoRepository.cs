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
    public class ProductoRepository : IProductoRepository
    {
        private readonly PetShopDBContext _context;

        public ProductoRepository()
        {
            _context = new PetShopDBContext();
        }

        public Guid Create(Producto producto, Guid idProveedor)
        {
            if (producto == null) throw new ArgumentNullException(nameof(producto));
            if (idProveedor == Guid.Empty) throw new ArgumentException("El ID de Proveedor no puede ser vacío.", nameof(idProveedor));

            // Usamos una transacción para asegurar que si el producto se crea, también se crea la relación.
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // 1. Crear el Producto
                    producto.IdProducto = Guid.NewGuid();
                    _context.Productos.Add(producto);

                    // 2. Crear la relación ProveedorProducto
                    var proveedorProducto = new ProveedorProducto
                    {
                        IdProveedorProducto = Guid.NewGuid(),
                        IdProducto = producto.IdProducto,
                        IdProveedor = idProveedor
                    };
                    _context.ProveedorProductos.Add(proveedorProducto);

                    _context.SaveChanges();
                    transaction.Commit();

                    return producto.IdProducto;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<Producto> GetByProveedor(Guid idProveedor)
        {
            // Usamos LINQ para hacer la consulta a través de la tabla intermedia.
            // Esto equivale a un JOIN en SQL.
            var productos = _context.ProveedorProductos
                                    .Where(pp => pp.IdProveedor == idProveedor)
                                    .Select(pp => pp.IdProductoNavigation) // Navegamos a la entidad Producto
                                    .ToList();

            return productos;
        }        

        public Producto? GetById(Guid id)
        {
            return _context.Productos.Find(id);
        }

        public List<Producto> GetAll()
        {
            return _context.Productos.ToList();
        }

        public void Delete(Guid id)
        {
            var producto = _context.Productos.Find(id);
            if (producto != null)
            {
                // Para borrado físico:
                _context.Productos.Remove(producto);
                _context.SaveChanges();
            }
        }
    }
}
