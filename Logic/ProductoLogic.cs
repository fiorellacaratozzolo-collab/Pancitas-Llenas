using DataAccess.Implementations.SqlServer;
using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class ProductoLogic
    {
        private readonly IProductoRepository _productoRepository;
        // Asumimos que también tenemos un IProveedorRepository para la validación
        private readonly IProveedorRepository _proveedorRepository;

        public ProductoLogic()
        {
            _productoRepository = new ProductoRepository();
            // Esto debería ser inyectado, pero lo instanciamos directo por simplicidad:
            _proveedorRepository = new ProveedorRepository(); 
        }

        public Guid CrearProductoConProveedor(Producto producto, Guid idProveedor)
        {
            // 1. Validación de Proveedor Existente
            if (_proveedorRepository.GetById(idProveedor) == null)
            {
                throw new InvalidOperationException($"No se puede crear el producto: El proveedor con ID {idProveedor} no existe en la base de datos.");
            }

            if (string.IsNullOrWhiteSpace(producto.NombreProducto))
            {
                throw new ArgumentException("El nombre del producto es obligatorio.");
            }
            // ... otras validaciones

            // 2. Persistencia
            return _productoRepository.Create(producto, idProveedor);
        }

        public List<Producto> ObtenerProductosPorProveedor(Guid idProveedor)
        {
            if (idProveedor == Guid.Empty)
            {
                throw new ArgumentException("El ID de Proveedor no puede ser vacío para la búsqueda.");
            }
            return _productoRepository.GetByProveedor(idProveedor);
        }

        public List<Producto> ObtenerTodos() => _productoRepository.GetAll();
        public void DeshabilitarProducto(Guid id) => _productoRepository.Delete(id);
    }
}
