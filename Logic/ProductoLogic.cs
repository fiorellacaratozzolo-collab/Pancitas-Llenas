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
    /// <summary>
    /// Gestiona las reglas de negocio, validaciones y relaciones para los productos del inventario.
    /// </summary>
    public class ProductoLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;

        /// <summary>
        /// Inicializa una nueva instancia de la lógica de productos.
        /// </summary>
        public ProductoLogic()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Registra un nuevo producto en el catálogo validando sus campos obligatorios y estableciendo su vínculo inicial con un proveedor existente.
        /// </summary>
        public Guid CrearProductoConProveedor(ProductoDTO productoDTO, Guid idProveedor)
        {
            Producto producto = _mapper.Map<Producto>(productoDTO);

            if (_unitOfWork.Proveedores.GetById(idProveedor) == null)
            {
                throw new InvalidOperationException(string.Format("No se puede crear el producto: El proveedor con ID {0} no existe en la base de datos.", idProveedor));
            }

            if (string.IsNullOrWhiteSpace(producto.NombreProducto))
            {
                throw new ArgumentException("El nombre del producto es obligatorio.");
            }

            Guid idProducto = _unitOfWork.Productos.Create(producto, idProveedor);

            _unitOfWork.Complete();

            return idProducto;
        }

        /// <summary>
        /// Elimina o deshabilita un producto del catálogo del sistema.
        /// </summary>
        public void DeshabilitarProducto(Guid id)
        {
            _unitOfWork.Productos.Delete(id);
            _unitOfWork.Complete();
        }

        /// <summary>
        /// Obtiene la lista de productos suministrados por un proveedor específico mediante una consulta directa.
        /// </summary>
        public List<ProductoDTO> ObtenerProductosPorProveedor(Guid idProveedor)
        {
            if (idProveedor == Guid.Empty)
            {
                throw new ArgumentException("El ID de Proveedor no puede ser vacío para la búsqueda.");
            }

            List<Producto> productos = _unitOfWork.Productos.GetByProveedor(idProveedor);
            return _mapper.Map<List<ProductoDTO>>(productos);
        }

        /// <summary>
        /// Recupera el catálogo completo de productos registrados en el sistema.
        /// </summary>
        public List<ProductoDTO> ObtenerTodos()
        {
            List<Producto> productos = _unitOfWork.Productos.GetAll();
            return _mapper.Map<List<ProductoDTO>>(productos);
        }

        /// <summary>
        /// Obtiene los productos vinculados a un proveedor analizando la tabla intermedia de relaciones.
        /// </summary>
        public List<ProductoDTO> GetProductosByProveedor(Guid idProveedor)
        {
            List<Producto> productos = _unitOfWork.ProveedorProductos
                                                    .GetAll()
                                                    .Where(pp => pp.IdProveedor == idProveedor)
                                                    .Select(pp => pp.IdProductoNavigation)
                                                    .ToList();

            return _mapper.Map<List<ProductoDTO>>(productos);
        }

        /// <summary>
        /// Recupera el listado completo de todos los vínculos existentes entre proveedores y productos.
        /// </summary>
        public List<ProveedorProductoDTO> GetTodosLosVinculosProveedorProducto()
        {
            List<ProveedorProducto> vinculos = _unitOfWork.ProveedorProductos.GetAll();
            return _mapper.Map<List<ProveedorProductoDTO>>(vinculos);
        }
    }
}