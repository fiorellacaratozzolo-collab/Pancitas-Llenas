using DataAccess.Models;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define el contrato de persistencia para los detalles de productos incluidos en los traspasos de mercadería.
    /// </summary>
    public interface ISolicitudDeTraspasoDetalleRepository
    {
        /// <summary>Inserta un nuevo renglón de detalle asociado a una solicitud de traspaso.</summary>
        void Create(SolicitudDeTraspasoDeProductosDetalle detalle);

        /// <summary>Recupera todos los registros de detalles de traspasos en el sistema.</summary>
        List<SolicitudDeTraspasoDeProductosDetalle> GetAll();

        /// <summary>Obtiene los renglones de detalle pertenecientes a una solicitud de traspaso específica.</summary>
        List<SolicitudDeTraspasoDeProductosDetalle> GetByIdSolicitud(Guid idSolicitud);
    }
}