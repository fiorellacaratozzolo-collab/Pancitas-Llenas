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
    public class SolicitudDePedidoLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;

        public SolicitudDePedidoLogic()
        {
            _unitOfWork = new UnitOfWork();
        }

        public Guid CrearSolicitud(SolicitudDePedidoDTO solicitudDTO)
        {
            if (solicitudDTO.SolicitudDePedidoDetalles == null || !solicitudDTO.SolicitudDePedidoDetalles.Any())
                throw new ArgumentException("La solicitud debe tener al menos un detalle.");

            // 1. Mapear DTO a Entidad (DAO)
            var solicitud = _mapper.Map<SolicitudDePedido>(solicitudDTO);
            solicitud.FechaSp = DateTime.Today;
            solicitud.IdEstadoSp = 1; // Pendiente

            // 2. Mapear Detalles y enlazar
            solicitud.SolicitudDePedidoDetalles = new List<SolicitudDePedidoDetalle>();
            foreach (var detalleDTO in solicitudDTO.SolicitudDePedidoDetalles)
            {
                var detalle = _mapper.Map<SolicitudDePedidoDetalle>(detalleDTO);
                detalle.IdSolicitudDePedido = solicitud.IdSolicitudDePedido;
                solicitud.SolicitudDePedidoDetalles.Add(detalle);
            }

            // 3. Persistencia y Commit
            Guid id = _unitOfWork.SolicitudDePedidos.Create(solicitud);
            _unitOfWork.SolicitudDePedidoDetalles.AddRange(solicitud.SolicitudDePedidoDetalles);
            _unitOfWork.Complete();

            return id;
        }

        public List<SolicitudDePedidoDTO> ObtenerTodas()
        {
            var solicitudes = _unitOfWork.SolicitudDePedidos.GetAll();
            return _mapper.Map<List<SolicitudDePedidoDTO>>(solicitudes);
        }

        public SolicitudDePedidoDTO ObtenerPorId(Guid id)
        {
            var solicitud = _unitOfWork.SolicitudDePedidos.GetById(id);

            if (solicitud == null)
                throw new KeyNotFoundException($"No se encontró la solicitud con ID {id}");

            return _mapper.Map<SolicitudDePedidoDTO>(solicitud);
        }

        // --- GESTIÓN DE ESTADO ---

        public void RechazarSolicitud(Guid solicitudId)
        {
            var solicitud = _unitOfWork.SolicitudDePedidos.GetById(solicitudId);
            if (solicitud == null)
                throw new KeyNotFoundException($"No se encontró la solicitud con ID {solicitudId}");

            solicitud.IdEstadoSp = 3; // Rechazada
            _unitOfWork.SolicitudDePedidos.Update(solicitud);
            _unitOfWork.Complete();
        }       

        public Guid AprobarYSolicitarOrdenDePedido(Guid solicitudId)
        {
            var solicitud = _unitOfWork.SolicitudDePedidos.GetById(solicitudId);
            if (solicitud == null)
                throw new KeyNotFoundException($"No se encontró la solicitud con ID {solicitudId}");

            // 1. Marcar la Solicitud como Convertida/Aprobada
            solicitud.IdEstadoSp = 2; // Convertida a OP
            _unitOfWork.SolicitudDePedidos.Update(solicitud);

            // 2. Crear la Orden de Pedido (OP) usando una UnitOfWork COMPARTIDA
            var ordenDePedidoLogic = new OrdenDePedidoLogic(_unitOfWork);
            Guid idOrdenDePedido = ordenDePedidoLogic.CrearOrdenDesdeSolicitud(solicitud);

            // 3. Commit de la transacción atómica
            _unitOfWork.Complete();

            return idOrdenDePedido;
        }
    }
}
