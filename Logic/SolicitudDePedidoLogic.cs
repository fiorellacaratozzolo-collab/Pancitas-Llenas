using AutoMapper;
using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Interfaces;
using DataAccess.Models;
using Logic.MappingProfiles;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    /// <summary>
    /// Gestiona el ciclo de vida, desde la creación hasta la aprobación o rechazo, de las solicitudes de pedido de mercadería.
    /// </summary>
    public class SolicitudDePedidoLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;

        /// <summary>
        /// Inicializa una nueva instancia de la lógica de solicitudes de pedido.
        /// </summary>
        public SolicitudDePedidoLogic()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Valida la integridad de la solicitud, establece su estado inicial e inserta el requerimiento en el sistema de manera atómica.
        /// </summary>
        public Guid CrearSolicitud(SolicitudDePedidoDTO solicitudDTO)
        {
            if (solicitudDTO.SolicitudDePedidoDetalles == null || !solicitudDTO.SolicitudDePedidoDetalles.Any())
                throw new ArgumentException("La solicitud debe tener al menos un detalle.");

            var solicitud = _mapper.Map<SolicitudDePedido>(solicitudDTO);
            solicitud.FechaSp = DateTime.Today;
            solicitud.IdEstadoSp = 1;
            solicitud.IdEstadoSpNavigation = null;

            foreach (var detalle in solicitud.SolicitudDePedidoDetalles)
            {
                detalle.IdProductoNavigation = null;
                detalle.IdSolicitudDePedidoNavigation = null;
            }

            Guid id = _unitOfWork.SolicitudDePedidos.Create(solicitud);

            _unitOfWork.Complete();

            return id;
        }

        /// <summary>
        /// Recupera la lista completa de solicitudes de pedido históricas y activas.
        /// </summary>
        public List<SolicitudDePedidoDTO> ObtenerTodas()
        {
            var solicitudes = _unitOfWork.SolicitudDePedidos.GetAll();
            return _mapper.Map<List<SolicitudDePedidoDTO>>(solicitudes);
        }

        /// <summary>
        /// Recupera una solicitud específica evaluando su existencia previa en el repositorio.
        /// </summary>
        public SolicitudDePedidoDTO ObtenerPorId(Guid id)
        {
            var solicitud = _unitOfWork.SolicitudDePedidos.GetById(id);

            if (solicitud == null)
                throw new KeyNotFoundException(string.Format("No se encontró la solicitud con ID {0}", id));

            return _mapper.Map<SolicitudDePedidoDTO>(solicitud);
        }

        /// <summary>
        /// Extrae el detalle de productos y cantidades requeridas en una solicitud de pedido específica.
        /// </summary>
        public List<SolicitudDePedidoDetalleDTO> ObtenerDetallesPorSolicitud(Guid idSolicitud)
        {
            var detalles = _unitOfWork.SolicitudDePedidoDetalles.GetByIdSolicitud(idSolicitud);

            return _mapper.Map<List<SolicitudDePedidoDetalleDTO>>(detalles);
        }

        /// <summary>
        /// Cambia el estado de una solicitud a Rechazada (Estado 3), cerrando su ciclo en el flujo de aprobación.
        /// </summary>
        public void RechazarSolicitud(Guid solicitudId)
        {
            var solicitud = _unitOfWork.SolicitudDePedidos.GetById(solicitudId);
            if (solicitud == null)
                throw new KeyNotFoundException(string.Format("No se encontró la solicitud con ID {0}", solicitudId));

            solicitud.IdEstadoSp = 3;
            _unitOfWork.SolicitudDePedidos.Update(solicitud);
            _unitOfWork.Complete();
        }

        /// <summary>
        /// Orquesta la transición de una solicitud al estado Aprobada (Estado 2) e invoca la generación de una Orden de Pedido formal compartiendo el contexto transaccional.
        /// </summary>
        public Guid AprobarYSolicitarOrdenDePedido(Guid solicitudId)
        {
            var solicitud = _unitOfWork.SolicitudDePedidos.GetById(solicitudId);
            if (solicitud == null)
                throw new KeyNotFoundException(string.Format("No se encontró la solicitud con ID {0}", solicitudId));

            solicitud.IdEstadoSp = 2;
            _unitOfWork.SolicitudDePedidos.Update(solicitud);

            var ordenDePedidoLogic = new OrdenDePedidoLogic(_unitOfWork);
            Guid idOrdenDePedido = ordenDePedidoLogic.CrearOrdenDesdeSolicitud(solicitud);

            _unitOfWork.Complete();

            return idOrdenDePedido;
        }
    }
}