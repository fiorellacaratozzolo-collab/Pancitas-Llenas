using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
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

        public Guid CrearProductoConProveedor(Producto producto, Guid idProveedor)
        {
            using (var unitOfWork = new DataAccess.Implementations.SqlServer.UnitOfWork())
            {
                // 1. Validación de Proveedor Existente
                if (unitOfWork.Proveedores.GetById(idProveedor) == null)
                {
                    // (ProveedorNoExisteException)
                    throw new InvalidOperationException($"No se puede crear el producto: El proveedor con ID {idProveedor} no existe en la base de datos.");
                }

                if (string.IsNullOrWhiteSpace(producto.NombreProducto))
                {
                    throw new ArgumentException("El nombre del producto es obligatorio.");
                }

                // 2. Persistencia
                Guid idProducto = unitOfWork.Productos.Create(producto, idProveedor);

                // 3. Confirmar la Transacción (Commit)
                unitOfWork.Complete();

                return idProducto;
            }
        }

        public List<Producto> ObtenerProductosPorProveedor(Guid idProveedor)
        {
            using (var unitOfWork = new DataAccess.Implementations.SqlServer.UnitOfWork())
            {
                if (idProveedor == Guid.Empty)
                {
                    throw new ArgumentException("El ID de Proveedor no puede ser vacío para la búsqueda.");
                }
                return unitOfWork.Productos.GetByProveedor(idProveedor);
            }
        }

        public List<Producto> ObtenerTodos()
        {         
            using (var unitOfWork = new DataAccess.Implementations.SqlServer.UnitOfWork())
            {
                return unitOfWork.Productos.GetAll();
            }
        }

        public void DeshabilitarProducto(Guid id)
        {
            using (var unitOfWork = new DataAccess.Implementations.SqlServer.UnitOfWork())
            {
                unitOfWork.Productos.Delete(id);
                unitOfWork.Complete();
            }
        }

        public List<Producto> GetProductosByProveedor(Guid idProveedor)
        {
            using (var unitOfWork = new DataAccess.Implementations.SqlServer.UnitOfWork())
            {
                // Carga la relación intermedia y selecciona solo los productos.
                return unitOfWork.ProveedorProductos
                                    .GetAll()
                                    .Where(pp => pp.IdProveedor == idProveedor)
                                    .Select(pp => pp.IdProductoNavigation)
                                    .ToList();
            }
        }

        public List<ProveedorProducto> GetTodosLosVinculosProveedorProducto()
        {
            using (var unitOfWork = new DataAccess.Implementations.SqlServer.UnitOfWork())
            {
                // Llama al repositorio para obtener todos los vínculos
                return unitOfWork.ProveedorProductos.GetAll();
            }
        }
    }
}