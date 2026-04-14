using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Facade
{
    public class TraspasoService
    {
        private readonly TraspasoLogic _logic;

        public TraspasoService()
        {
            _logic = new TraspasoLogic();
        }

        public Guid GenerarSolicitud(SolicitudDeTraspasoDeProductoDTO solicitud, List<SolicitudDeTraspasoDeProductosDetalleDTO> detalles)
        {
            return _logic.GenerarSolicitud(solicitud, detalles);
        }

        public List<SolicitudDeTraspasoDeProductoDTO> ObtenerSolicitudesPendientes(Guid idSucursalOrigen)
        {
            return _logic.ObtenerSolicitudesPendientes(idSucursalOrigen);
        }

        public List<SolicitudDeTraspasoDeProductosDetalleDTO> ObtenerDetallesPorSolicitud(Guid idSolicitud)
        {
            return _logic.ObtenerDetallesPorSolicitud(idSolicitud);
        }

        public void AprobarTraspaso(Guid idSolicitud)
        {
            _logic.AprobarTraspaso(idSolicitud);
        }

        public void RechazarTraspaso(Guid idSolicitud)
        {
            _logic.RechazarTraspaso(idSolicitud);
        }
    }
}
