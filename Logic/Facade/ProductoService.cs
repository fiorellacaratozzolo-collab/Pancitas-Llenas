using DataAccess.Models;
using ModelsDTO;
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

        public Guid CrearProductoConProveedor(ProductoDTO productoDTO, Guid idProveedor)
        {
            return _productoLogic.CrearProductoConProveedor(productoDTO, idProveedor);
        }

        public List<ProductoDTO> GetByProveedor(Guid idProveedor)
        {
            return _productoLogic.ObtenerProductosPorProveedor(idProveedor);
        }

        public List<ProductoDTO> GetAllProductos()
        {
            return _productoLogic.ObtenerTodos();
        }

        public void DeshabilitarProducto(Guid id)
        {
            _productoLogic.DeshabilitarProducto(id);
        }
        public List<ProductoDTO> GetProductosByProveedor(Guid idProveedor)
        {
            return _productoLogic.GetProductosByProveedor(idProveedor);
        }
        public List<ProveedorProductoDTO> GetTodosLosVinculosProveedorProducto()
        {
            return _productoLogic.GetTodosLosVinculosProveedorProducto();
        }
    }
}
