using ModelsDTO;

namespace Logic.Facade
{
    /// <summary>
    /// Fachada que expone las operaciones de ventas a las capas superiores de la aplicación.
    /// </summary>
    public class VentaService
    {
        private readonly VentaLogic _ventaLogic;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de ventas.
        /// </summary>
        public VentaService()
        {
            _ventaLogic = new VentaLogic();
        }

        /// <summary>
        /// Registra una nueva venta junto con sus detalles y aplica el descuento de stock correspondiente.
        /// </summary>
        public Guid RegistrarVenta(VentumDTO ventaDTO, List<VentaDetalleDTO> detallesDTO, Guid idSucursal)
        {
            return _ventaLogic.RegistrarVenta(ventaDTO, detallesDTO, idSucursal);
        }

        /// <summary>
        /// Obtiene el listado de ventas realizadas en una sucursal durante una fecha específica.
        /// </summary>
        public List<VentumDTO> GetVentasPorSucursalYFecha(Guid idSucursal, DateTime fecha)
        {
            return _ventaLogic.GetVentasPorSucursalYFecha(idSucursal, fecha);
        }

        /// <summary>
        /// Recupera el detalle de los productos incluidos en una venta determinada.
        /// </summary>
        public List<VentaDetalleDTO> GetDetallesDeVenta(Guid idVenta)
        {
            return _ventaLogic.GetDetallesDeVenta(idVenta);
        }

        /// <summary>
        /// Anula una venta previamente registrada, restaurando el stock en la sucursal de origen.
        /// </summary>
        public void AnularVenta(Guid idVenta, Guid idSucursal)
        {
            _ventaLogic.AnularVenta(idVenta, idSucursal);
        }

        /// <summary>
        /// Obtiene el registro histórico de todas las ventas asociadas a una sucursal.
        /// </summary>
        public List<VentumDTO> ObtenerVentasPorSucursal(Guid idSucursal)
        {
            return _ventaLogic.ObtenerVentasPorSucursal(idSucursal);
        }
    }
}
