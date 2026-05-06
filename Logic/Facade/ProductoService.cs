using DataAccess.Models;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Facade
{
    /// <summary>
    /// Fachada que expone las funcionalidades para la administración del catálogo de productos y sus vínculos.
    /// </summary>
    public class ProductoService
    {
        private readonly ProductoLogic _productoLogic = new ProductoLogic();

        /// <summary>
        /// Registra un nuevo producto y establece su vínculo inicial con un proveedor.
        /// </summary>
        public Guid CrearProductoConProveedor(ProductoDTO productoDTO, Guid idProveedor)
        {
            return _productoLogic.CrearProductoConProveedor(productoDTO, idProveedor);
        }

        /// <summary>
        /// Obtiene el catálogo de productos filtrado por un proveedor en particular.
        /// </summary>
        public List<ProductoDTO> GetByProveedor(Guid idProveedor)
        {
            return _productoLogic.ObtenerProductosPorProveedor(idProveedor);
        }

        /// <summary>
        /// Recupera todos los productos registrados en el sistema.
        /// </summary>
        public List<ProductoDTO> GetAllProductos()
        {
            return _productoLogic.ObtenerTodos();
        }

        /// <summary>
        /// Deshabilita o elimina un producto del catálogo.
        /// </summary>
        public void DeshabilitarProducto(Guid id)
        {
            _productoLogic.DeshabilitarProducto(id);
        }

        /// <summary>
        /// Extrae la lista de productos asociados a un proveedor consultando la tabla intermedia.
        /// </summary>
        public List<ProductoDTO> GetProductosByProveedor(Guid idProveedor)
        {
            return _productoLogic.GetProductosByProveedor(idProveedor);
        }

        /// <summary>
        /// Obtiene el listado completo de todos los vínculos activos entre proveedores y productos.
        /// </summary>
        public List<ProveedorProductoDTO> GetTodosLosVinculosProveedorProducto()
        {
            return _productoLogic.GetTodosLosVinculosProveedorProducto();
        }
    }
}