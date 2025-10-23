using AutoMapper;
using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Interfaces;
using DataAccess.Models;
using Logic.MappingProfiles;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class ProductoLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;

        public ProductoLogic()
        {
            _unitOfWork = new UnitOfWork();
        }

        public Guid CrearProductoConProveedor(ProductoDTO productoDTO, Guid idProveedor)
        {
            // 1. Mappeo de DTO a Entidad
            Producto producto = _mapper.Map<Producto>(productoDTO);

            // 2. Validación de Proveedor Existente (usa la UoW inyectada)
            if (_unitOfWork.Proveedores.GetById(idProveedor) == null)
            {
                throw new InvalidOperationException($"No se puede crear el producto: El proveedor con ID {idProveedor} no existe en la base de datos.");
            }

            if (string.IsNullOrWhiteSpace(producto.NombreProducto))
            {
                throw new ArgumentException("El nombre del producto es obligatorio.");
            }

            Guid idProducto = _unitOfWork.Productos.Create(producto, idProveedor);
            _unitOfWork.Complete();

            return idProducto;
        }

        public void DeshabilitarProducto(Guid id)
        {
            _unitOfWork.Productos.Delete(id);
            _unitOfWork.Complete();
        }

        public List<ProductoDTO> ObtenerProductosPorProveedor(Guid idProveedor)
        {
            if (idProveedor == Guid.Empty)
            {
                throw new ArgumentException("El ID de Proveedor no puede ser vacío para la búsqueda.");
            }

            List<Producto> productos = _unitOfWork.Productos.GetByProveedor(idProveedor);
            return _mapper.Map<List<ProductoDTO>>(productos);
        }

        public List<ProductoDTO> ObtenerTodos()
        {
            List<Producto> productos = _unitOfWork.Productos.GetAll();
            return _mapper.Map<List<ProductoDTO>>(productos);
        }

        public List<ProductoDTO> GetProductosByProveedor(Guid idProveedor)
        {
            List<Producto> productos = _unitOfWork.ProveedorProductos
                                                    .GetAll()
                                                    .Where(pp => pp.IdProveedor == idProveedor)
                                                    .Select(pp => pp.IdProductoNavigation)
                                                    .ToList();
            return _mapper.Map<List<ProductoDTO>>(productos);
        }

        public List<ProveedorProductoDTO> GetTodosLosVinculosProveedorProducto()
        {
            List<ProveedorProducto> vinculos = _unitOfWork.ProveedorProductos.GetAll();
            return _mapper.Map<List<ProveedorProductoDTO>>(vinculos);
        }
    }
}