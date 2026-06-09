using ModelsDTO;

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
        /// Actualiza los datos escalares de un producto existente y reasigna su proveedor.
        /// </summary>
        public void UpdateProducto(ProductoDTO productoDTO, Guid idProveedor)
        {
            _productoLogic.UpdateProducto(productoDTO, idProveedor);
        }

        /// <summary>
        /// Obtiene la lista de productos ACTIVOS suministrados por un proveedor específico.
        /// </summary>
        public List<ProductoDTO> GetByProveedor(Guid idProveedor)
        {
            return _productoLogic.ObtenerProductosPorProveedor(idProveedor);
        }

        /// <summary>
        /// Recupera el catálogo de productos registrados que estén ACTIVOS en el sistema.
        /// </summary>
        public List<ProductoDTO> ObtenerActivos()
        {
            return _productoLogic.ObtenerActivos();
        }

        /// <summary>
        /// Recupera el catálogo de productos registrados que estén INACTIVOS/DESHABILITADOS en el sistema.
        /// </summary>
        public List<ProductoDTO> ObtenerDeshabilitados()
        {
            return _productoLogic.ObtenerDeshabilitados();
        }

        /// <summary>
        /// Deshabilita un producto del catálogo (Borrado Lógico).
        /// </summary>
        public void DeshabilitarProducto(Guid id)
        {
            _productoLogic.DeshabilitarProducto(id);
        }

        /// <summary>
        /// Rehabilita un producto previamente dado de baja, devolviéndolo al catálogo activo.
        /// </summary>
        public void HabilitarProducto(Guid id)
        {
            _productoLogic.HabilitarProducto(id);
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
