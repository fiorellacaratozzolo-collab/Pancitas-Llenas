using DataAccess.Models;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define el contrato de persistencia para los detalles de las solicitudes de pedido interno.
    /// </summary>
    public interface ISolicitudDePedidoDetalleRepository
    {
        /// <summary>Inserta una colección de renglones de detalle asociados a una solicitud de pedido.</summary>
        void AddRange(IEnumerable<SolicitudDePedidoDetalle> detalles);

        /// <summary>Recupera los renglones de detalle pertenecientes a una solicitud de pedido específica.</summary>
        List<SolicitudDePedidoDetalle> GetByIdSolicitud(Guid idSolicitud);
    }
}