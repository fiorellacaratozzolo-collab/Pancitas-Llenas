using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Facade
{
    public class ProductoService
    {
        private readonly ProductoLogic _productoLogic = new ProductoLogic();

        public Guid CrearProductoConProveedor(Producto producto, Guid idProveedor)
        {
            return _productoLogic.CrearProductoConProveedor(producto, idProveedor);
        }

        public List<Producto> GetByProveedor(Guid idProveedor)
        {
            return _productoLogic.ObtenerProductosPorProveedor(idProveedor);
        }

        public List<Producto> GetAllProductos()
        {
            return _productoLogic.ObtenerTodos();
        }

        public void DeshabilitarProducto(Guid id)
        {
            _productoLogic.DeshabilitarProducto(id);
        }
    }
}
