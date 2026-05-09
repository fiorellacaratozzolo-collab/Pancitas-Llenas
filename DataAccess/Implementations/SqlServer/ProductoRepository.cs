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
        /// Inserta un nuevo producto en el catálogo y crea automáticamente su vínculo inicial en la tabla intermedia.
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
        /// Realiza un Borrado Lógico del producto cambiando su estado a inactivo (Activo = false).
        /// </summary>
        public void Delete(Guid id)
        {
            var prod = _context.Productos.Find(id);
            if (prod != null)
            {
                prod.Activo = false;
                _context.Productos.Update(prod);
            }
        }

        /// <summary>
        /// Reactiva un producto previamente deshabilitado (Activo = true).
        /// </summary>
        public void Habilitar(Guid id)
        {
            var prod = _context.Productos.Find(id);
            if (prod != null)
            {
                prod.Activo = true;
                _context.Productos.Update(prod);
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

        /// <summary>
        /// Actualiza los valores de un producto existente y actualiza su relación en la tabla intermedia de proveedores.
        /// </summary>
        public void Update(Producto producto, Guid idNuevoProveedor)
        {
            var productoDb = _context.Productos.Find(producto.IdProducto);
            if (productoDb != null)
            {
                _context.Entry(productoDb).CurrentValues.SetValues(producto);
                var vinculoAnterior = _context.ProveedorProductos.FirstOrDefault(pp => pp.IdProducto == producto.IdProducto);

                if (vinculoAnterior != null)
                {
                    if (vinculoAnterior.IdProveedor != idNuevoProveedor)
                    {
                        _context.ProveedorProductos.Remove(vinculoAnterior);

                        _context.ProveedorProductos.Add(new ProveedorProducto
                        {
                            IdProveedorProducto = Guid.NewGuid(),
                            IdProducto = producto.IdProducto,
                            IdProveedor = idNuevoProveedor
                        });
                    }
                }
                else
                {
                    _context.ProveedorProductos.Add(new ProveedorProducto
                    {
                        IdProveedorProducto = Guid.NewGuid(),
                        IdProducto = producto.IdProducto,
                        IdProveedor = idNuevoProveedor
                    });
                }
            }
        }
    }
}
