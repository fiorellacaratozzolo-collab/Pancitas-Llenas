using AutoMapper;
using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Models;
using Logic.MappingProfiles;
using ModelsDTO;

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
        /// Registra un nuevo producto. Si el producto ya existe en la base de datos (activo o inactivo), bloquea la operación.
        /// </summary>
        public Guid CrearProductoConProveedor(ProductoDTO productoDTO, Guid idProveedor)
        {
            if (_unitOfWork.Proveedores.GetById(idProveedor) == null)
            {
                throw new InvalidOperationException(string.Format("No se puede procesar el producto: El proveedor con ID {0} no existe.", idProveedor));
            }

            var productoExistente = _unitOfWork.Productos.GetAll()
                .FirstOrDefault(p => p.NombreProducto.Trim().Equals(productoDTO.NombreProducto.Trim(), StringComparison.OrdinalIgnoreCase)
                                  && p.Marca.Trim().Equals(productoDTO.Marca.Trim(), StringComparison.OrdinalIgnoreCase));

            if (productoExistente != null)
            {
                if (productoExistente.Activo)
                {
                    throw new InvalidOperationException(string.Format("El producto '{0}' de la marca '{1}' ya existe y está ACTIVO.", productoDTO.NombreProducto, productoDTO.Marca));
                }
                else
                {
                    throw new InvalidOperationException(string.Format("El producto '{0}' ya existe pero está DESHABILITADO.\nPor favor, vaya a la vista de 'Deshabilitados' y actívelo.", productoDTO.NombreProducto));
                }
            }

            if (string.IsNullOrWhiteSpace(productoDTO.NombreProducto))
            {
                throw new ArgumentException("El nombre del producto es obligatorio.");
            }

            Producto nuevoProducto = _mapper.Map<Producto>(productoDTO);
            nuevoProducto.Activo = true;
            if (nuevoProducto.IdProducto == Guid.Empty)
            {
                nuevoProducto.IdProducto = Guid.NewGuid();
            }

            Guid idGenerado = _unitOfWork.Productos.Create(nuevoProducto, idProveedor);
            _unitOfWork.Complete();

            return idGenerado;
        }

        /// <summary>
        /// Valida los datos y actualiza un producto existente, modificando también su proveedor asociado si este hubiese cambiado.
        /// </summary>
        public void UpdateProducto(ProductoDTO productoDTO, Guid idProveedor)
        {
            Producto productoActualizado = _mapper.Map<Producto>(productoDTO);

            if (_unitOfWork.Proveedores.GetById(idProveedor) == null)
            {
                throw new InvalidOperationException(string.Format("No se puede actualizar: El proveedor con ID {0} no existe.", idProveedor));
            }

            if (string.IsNullOrWhiteSpace(productoActualizado.NombreProducto))
            {
                throw new ArgumentException("El nombre del producto es obligatorio para la actualización.");
            }

            _unitOfWork.Productos.Update(productoActualizado, idProveedor);
            _unitOfWork.Complete();
        }

        /// <summary>
        /// Deshabilita un producto del catálogo del sistema (Borrado Lógico).
        /// </summary>
        public void DeshabilitarProducto(Guid id)
        {
            _unitOfWork.Productos.Delete(id);
            _unitOfWork.Complete();
        }

        /// <summary>
        /// Rehabilita un producto previamente dado de baja, verificando primero que su proveedor se encuentre activo.
        /// </summary>
        public void HabilitarProducto(Guid id)
        {
            var vinculos = _unitOfWork.ProveedorProductos.GetAll()
                                      .Where(pp => pp.IdProducto == id)
                                      .ToList();

            foreach (var vinculo in vinculos)
            {
                var proveedorAsociado = _unitOfWork.Proveedores.GetById(vinculo.IdProveedor);

                if (proveedorAsociado != null && proveedorAsociado.Activo == false)
                {
                    throw new InvalidOperationException(string.Format("No se puede habilitar el producto porque su proveedor ('{0}') está deshabilitado.\nPor favor, vaya a la gestión de Proveedores y habilítelo primero.", proveedorAsociado.NombreProveedor));
                }
            }
            _unitOfWork.Productos.Habilitar(id);
            _unitOfWork.Complete();
        }

        /// <summary>
        /// Obtiene la lista de productos ACTIVOS suministrados por un proveedor específico.
        /// </summary>
        public List<ProductoDTO> ObtenerProductosPorProveedor(Guid idProveedor)
        {
            if (idProveedor == Guid.Empty) throw new ArgumentException("El ID de Proveedor no puede ser vacío.");

            List<Producto> productos = _unitOfWork.Productos.GetByProveedor(idProveedor)
                                                          .Where(p => p.Activo == true)
                                                          .ToList();

            return _mapper.Map<List<ProductoDTO>>(productos);
        }

        /// <summary>
        /// Recupera el catálogo de productos registrados que estén ACTIVOS en el sistema.
        /// </summary>
        public List<ProductoDTO> ObtenerActivos()
        {
            List<Producto> productos = _unitOfWork.Productos.GetAll()
                                                          .Where(p => p.Activo == true)
                                                          .ToList();

            return _mapper.Map<List<ProductoDTO>>(productos);
        }

        /// <summary>
        /// Recupera el catálogo de productos registrados que estén INACTIVOS/DESHABILITADOS en el sistema.
        /// </summary>
        public List<ProductoDTO> ObtenerDeshabilitados()
        {
            List<Producto> productos = _unitOfWork.Productos.GetAll()
                                                          .Where(p => p.Activo == false)
                                                          .ToList();

            return _mapper.Map<List<ProductoDTO>>(productos);
        }

        /// <summary>
        /// Obtiene los productos vinculados a un proveedor analizando la tabla intermedia (Sin filtrar por Activo/Inactivo aquí, se filtra en la UI).
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