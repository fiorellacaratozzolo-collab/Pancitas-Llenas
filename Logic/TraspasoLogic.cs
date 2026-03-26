using AutoMapper;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Models;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class TraspasoLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TraspasoLogic(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // 1. Alta de Solicitud (Estado: Pendiente)
        public Guid CrearSolicitud(SolicitudDeTraspasoDeProductoDTO dto)
        {
            // Convertimos el DTO que viene de la UI a la Entidad de BD
            var solicitud = _mapper.Map<SolicitudDeTraspasoDeProducto>(dto);

            // Configuramos los valores iniciales de la cabecera
            solicitud.IdSolicitudDeTraspasoDeProductos = Guid.NewGuid();
            solicitud.FechaStp = DateTime.Now;
            solicitud.IdEstadoStp = 1; // 1 = Pendiente (Asegúrate que en tu tabla EstadoStpenum el 1 sea Pendiente)

            // Procesamos los detalles para completar datos técnicos
            foreach (var detalle in solicitud.SolicitudDeTraspasoDeProductosDetalles)
            {
                detalle.IdSolicitudDeTraspasoDeProductosDetalle = Guid.NewGuid();
                detalle.IdSolicitudDeTraspasoDeProductos = solicitud.IdSolicitudDeTraspasoDeProductos;

                // Buscamos el producto en el maestro para obtener Peso y Unidad reales
                var productoMaestro = _unitOfWork.Productos.GetById(detalle.IdProducto);
                if (productoMaestro != null)
                {
                    detalle.PesoNeto = productoMaestro.PesoNeto ?? 0;
                    detalle.Unidad = productoMaestro.Unidad ?? "UN";
                }
            }

            // Usamos el repositorio de cabecera (que debería manejar los detalles por cascada o agregándolos manualmente)
            _unitOfWork.SolicitudesTraspaso.Create(solicitud);

            // Si tu repositorio no guarda detalles automáticamente, podrías necesitar:
            // _unitOfWork.SolicitudesTraspasoDetalles.AddRange(solicitud.SolicitudDeTraspasoDeProductosDetalles.ToList());

            _unitOfWork.Complete(); // Confirmamos la transacción en la BD

            return solicitud.IdSolicitudDeTraspasoDeProductos;
        }

        // 2. Obtener todas las solicitudes (Para el Form de Gestión)
        public List<SolicitudDeTraspasoDeProductoDTO> ObtenerTodas()
        {
            var entidades = _unitOfWork.SolicitudesTraspaso.GetAll();
            return _mapper.Map<List<SolicitudDeTraspasoDeProductoDTO>>(entidades);
        }

        // 3. Cambiar Estado (Aprobar/Rechazar)
        public void ActualizarEstadoSolicitud(Guid idSolicitud, int nuevoEstado)
        {
            var solicitud = _unitOfWork.SolicitudesTraspaso.GetById(idSolicitud);
            if (solicitud != null)
            {
                solicitud.IdEstadoStp = nuevoEstado;
                _unitOfWork.SolicitudesTraspaso.Update(solicitud);
                _unitOfWork.Complete();
            }
        }
    }
}
