using ModelsDTO;
using System;
using System.Collections.Generic;

namespace Logic.Facade
{
    /// <summary>
    /// Fachada que proporciona acceso al flujo de trabajo de creación y aprobación de solicitudes de pedido interno.
    /// </summary>
    public class SolicitudDePedidoService
    {
        private readonly SolicitudDePedidoLogic _logic;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de solicitudes de pedido.
        /// </summary>
        public SolicitudDePedidoService()
        {
            _logic = new SolicitudDePedidoLogic();
        }

        /// <summary>
        /// Registra una nueva solicitud de pedido de mercadería en estado pendiente.
        /// </summary>
        public Guid CrearSolicitud(SolicitudDePedidoDTO solicitudDTO)
        {
            return _logic.CrearSolicitud(solicitudDTO);
        }

        /// <summary>
        /// Obtiene la lista completa de solicitudes generadas independientemente de su estado.
        /// </summary>
        public List<SolicitudDePedidoDTO> ObtenerTodas()
        {
            return _logic.ObtenerTodas();
        }

        /// <summary>
        /// Recupera una solicitud de pedido específica mediante su identificador.
        /// </summary>
        public SolicitudDePedidoDTO ObtenerPorId(Guid id)
        {
            return _logic.ObtenerPorId(id);
        }

        /// <summary>
        /// Extrae el listado de productos y cantidades requeridos dentro de una solicitud.
        /// </summary>
        public List<SolicitudDePedidoDetalleDTO> ObtenerDetallesPorSolicitud(Guid idSolicitud)
        {
            return _logic.ObtenerDetallesPorSolicitud(idSolicitud);
        }

        /// <summary>
        /// Deniega la continuidad de una solicitud de pedido, actualizando su estado a Rechazada.
        /// </summary>
        public void RechazarSolicitud(Guid solicitudId)
        {
            _logic.RechazarSolicitud(solicitudId);
        }

        /// <summary>
        /// Aprueba una solicitud de pedido y desencadena la generación de su correspondiente Orden de Pedido.
        /// </summary>
        public Guid AprobarYSolicitarOrdenDePedido(Guid solicitudId)
        {
            return _logic.AprobarYSolicitarOrdenDePedido(solicitudId);
        }
    }
}