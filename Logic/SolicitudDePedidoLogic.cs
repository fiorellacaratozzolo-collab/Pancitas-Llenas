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
using System.Text;
using System.Threading.Tasks;

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

            var solicitud = _mapper.Map<SolicitudDePedido>(solicitudDTO);
            solicitud.FechaSp = DateTime.Today;
            solicitud.IdEstadoSp = 1; // Pendiente

            foreach (var detalleDTO in solicitudDTO.SolicitudDePedidoDetalles)
            {
                var detalle = _mapper.Map<SolicitudDePedidoDetalle>(detalleDTO);
                detalle.IdSolicitudDePedido = solicitud.IdSolicitudDePedido;
                solicitud.SolicitudDePedidoDetalles.Add(detalle);
            }

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
    }
}
