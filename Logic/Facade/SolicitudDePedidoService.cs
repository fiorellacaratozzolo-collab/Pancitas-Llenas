using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Facade
{
    public class SolicitudDePedidoService
    {
        private readonly SolicitudDePedidoLogic _logic;

        public SolicitudDePedidoService()
        {
            _logic = new SolicitudDePedidoLogic();
        }

        public Guid CrearSolicitud(SolicitudDePedidoDTO solicitudDTO)
        {
            return _logic.CrearSolicitud(solicitudDTO);
        }

        public List<SolicitudDePedidoDTO> ObtenerTodas()
        {
            return _logic.ObtenerTodas();
        }

        public SolicitudDePedidoDTO ObtenerPorId(Guid id)
        {
            return _logic.ObtenerPorId(id);
        }
    }
}
