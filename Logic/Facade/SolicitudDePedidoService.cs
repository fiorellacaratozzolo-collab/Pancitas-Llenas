using ModelsDTO;
using System;
using System.Collections.Generic;

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

        // --- Nuevos Métodos de Gestión ---
        public void RechazarSolicitud(Guid solicitudId)
        {
            _logic.RechazarSolicitud(solicitudId);
        }

        public Guid AprobarYSolicitarOrdenDePedido(Guid solicitudId)
        {
            return _logic.AprobarYSolicitarOrdenDePedido(solicitudId);
        }
    }
}