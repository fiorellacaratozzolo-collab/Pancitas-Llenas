using ModelsDTO;

namespace Logic.Facade
{
    /// <summary>
    /// Fachada que provee acceso a las operaciones de control de stock, reposición y semáforos de inventario.
    /// </summary>
    public class InventarioService
    {
        private readonly InventarioLogic _inventarioLogic;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de inventario.
        /// </summary>
        public InventarioService()
        {
            _inventarioLogic = new InventarioLogic();
        }

        /// <summary>
        /// Efectúa el descuento de mercadería en el inventario producto de una operación de venta.
        /// </summary>
        public void DescontarStockPorVenta(Guid idSucursal, Guid idProducto, int cantidadVendida)
        {
            _inventarioLogic.RestarStockPorVenta(idSucursal, idProducto, cantidadVendida);
        }

        /// <summary>
        /// Obtiene los niveles de inventario de todas las sucursales del sistema.
        /// </summary>
        public List<StockPorSucursalDTO> ObtenerTodoElStock()
        {
            return _inventarioLogic.ObtenerTodoElStock();
        }

        /// <summary>
        /// Filtra y devuelve el inventario actual de una sucursal en específico.
        /// </summary>
        public List<StockPorSucursalDTO> ObtenerStockPorSucursal(Guid idSucursal)
        {
            return _inventarioLogic.ObtenerStockPorSucursal(idSucursal);
        }

        /// <summary>
        /// Devuelve el indicador numérico del semáforo de abastecimiento para un producto en una sucursal.
        /// </summary>
        public int ObtenerEstadoSemaforo(Guid idSucursal, Guid idProducto)
        {
            return _inventarioLogic.ObtenerEstadoSemaforo(idSucursal, idProducto);
        }

        /// <summary>
        /// Incrementa o inicializa el stock de un producto en una sucursal, registrando el movimiento como un nuevo ingreso.
        /// </summary>
        public void AgregarStock(Guid idSucursal, Guid idProducto, int cantidadAAgregar, int stockDeseado = 0, Guid? idProveedor = null)
        {
            _inventarioLogic.AgregarOActualizarStock(idSucursal, idProducto, cantidadAAgregar, stockDeseado, idProveedor);
        }

        /// <summary>
        /// Obtiene el registro cronológico de ingresos de mercadería para una sucursal.
        /// </summary>
        public List<HistorialEntregaDTO> ObtenerHistorialEntregas(Guid idSucursal)
        {
            return _inventarioLogic.ObtenerHistorialEntregas(idSucursal);
        }
    }
}