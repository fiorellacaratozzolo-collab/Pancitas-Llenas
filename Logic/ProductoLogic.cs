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
        //UnitOfWork
        private readonly IUnitOfWork _unitOfWork;

        public ProductoLogic()
        {
            _unitOfWork = new DataAccess.Implementations.SqlServer.UnitOfWork();
        }

        public Guid CrearProductoConProveedor(Producto producto, Guid idProveedor)
        {
            // 1. Validación de Proveedor Existente
            if (_unitOfWork.Proveedores.GetById(idProveedor) == null)
            {
                throw new InvalidOperationException($"No se puede crear el producto: El proveedor con ID {idProveedor} no existe en la base de datos.");
            }

            if (string.IsNullOrWhiteSpace(producto.NombreProducto))
            {
                throw new ArgumentException("El nombre del producto es obligatorio.");
            }

            // 2. Persistencia
            Guid idProducto = _unitOfWork.Productos.Create(producto, idProveedor);

            // 3. Confirmar la Transacción (Commit)
            _unitOfWork.Complete();

            return idProducto;
        }

        public List<Producto> ObtenerProductosPorProveedor(Guid idProveedor)
        {
            if (idProveedor == Guid.Empty)
            {
                throw new ArgumentException("El ID de Proveedor no puede ser vacío para la búsqueda.");
            }
            // Consulta: Accedemos al repositorio a través del UoW
            return _unitOfWork.Productos.GetByProveedor(idProveedor);
        }

        public List<Producto> ObtenerTodos() => _unitOfWork.Productos.GetAll();

        public void DeshabilitarProducto(Guid id)
        {
            _unitOfWork.Productos.Delete(id);
            _unitOfWork.Complete(); // Confirma la eliminación
        }
    }
}
