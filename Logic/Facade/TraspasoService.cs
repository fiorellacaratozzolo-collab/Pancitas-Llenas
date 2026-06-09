using ModelsDTO;

namespace Logic.Facade
{
    /// <summary>
    /// Fachada que expone las operaciones de solicitud y transferencia de inventario entre sucursales.
    /// </summary>
    public class TraspasoService
    {
        private readonly TraspasoLogic _logic;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de traspasos.
        /// </summary>
        public TraspasoService()
        {
            _logic = new TraspasoLogic();
        }

        /// <summary>
        /// Genera una nueva solicitud de transferencia de mercadería entre dos sucursales.
        /// </summary>
        public Guid GenerarSolicitud(SolicitudDeTraspasoDeProductoDTO solicitud, List<SolicitudDeTraspasoDeProductosDetalleDTO> detalles)
        {
            return _logic.GenerarSolicitud(solicitud, detalles);
        }

        /// <summary>
        /// Obtiene todas las solicitudes de traspaso emitidas por una sucursal específica.
        /// </summary>
        public List<SolicitudDeTraspasoDeProductoDTO> ObtenerTodasPorSucursal(Guid idSucursalOrigen)
        {
            return _logic.ObtenerTodasPorSucursal(idSucursalOrigen);
        }

        /// <summary>
        /// Recupera los renglones de detalle de los productos incluidos en una solicitud de traspaso.
        /// </summary>
        public List<SolicitudDeTraspasoDeProductosDetalleDTO> ObtenerDetallesPorSolicitud(Guid idSolicitud)
        {
            return _logic.ObtenerDetallesPorSolicitud(idSolicitud);
        }

        /// <summary>
        /// Aprueba una solicitud pendiente y efectúa el movimiento real de stock entre las sucursales involucradas.
        /// </summary>
        public void AprobarTraspaso(Guid idSolicitud)
        {
            _logic.AprobarTraspaso(idSolicitud);
        }

        /// <summary>
        /// Modifica el estado de una solicitud de traspaso a rechazada, finalizando su ciclo.
        /// </summary>
        public void RechazarTraspaso(Guid idSolicitud)
        {
            _logic.RechazarTraspaso(idSolicitud);
        }

        /// <summary>
        /// Obtiene el historial de movimientos de inventario por transferencias (ingresos y egresos) de una sucursal.
        /// </summary>
        public List<HistorialTraspasoDTO> ObtenerHistorialTraspasos(Guid idSucursalActual)
        {
            return _logic.ObtenerHistorialTraspasos(idSucursalActual);
        }
    }
}