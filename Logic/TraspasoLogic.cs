using AutoMapper;
using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Models;
using Logic.CustomExceptions;
using Logic.MappingProfiles;
using ModelsDTO;

namespace Logic
{
    /// <summary>
    /// Gestiona las reglas de negocio y los flujos de estado para el traspaso de inventario entre distintas sucursales.
    /// </summary>
    public class TraspasoLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;

        /// <summary>
        /// Inicializa una nueva instancia de la lógica de traspasos.
        /// </summary>
        public TraspasoLogic()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Genera una nueva solicitud formal de traspaso de mercadería, validando el destino y la integridad de los productos solicitados.
        /// </summary>
        public Guid GenerarSolicitud(SolicitudDeTraspasoDeProductoDTO solicitudDTO, List<SolicitudDeTraspasoDeProductosDetalleDTO> detallesDTO)
        {
            if (solicitudDTO.IdSucursalOrigen == solicitudDTO.IdSucursalDestino)
            {
                throw new TraspasoMismaSucursalException();
            }

            if (detallesDTO == null || !detallesDTO.Any())
            {
                throw new ArgumentException("La solicitud debe contener al menos un producto.");
            }

            foreach (var det in detallesDTO)
            {
                if (det.Cantidad <= 0)
                {
                    string nombreProd = !string.IsNullOrWhiteSpace(det.NombreProducto) ? det.NombreProducto : "Seleccionado";
                    throw new CantidadInvalidaException(nombreProd, det.Cantidad);
                }
            }

            var solicitud = _mapper.Map<SolicitudDeTraspasoDeProducto>(solicitudDTO);
            var detalles = _mapper.Map<List<SolicitudDeTraspasoDeProductosDetalle>>(detallesDTO);

            solicitud.IdSucursalDestinoNavigation = null!;
            solicitud.IdSucursalOrigenNavigation = null!;

            try
            {
                solicitud.IdEstadoStp = 1;
                Guid idSolicitud = _unitOfWork.SolicitudesTraspaso.Create(solicitud);

                foreach (var detalle in detalles)
                {
                    detalle.IdSolicitudDeTraspasoDeProductos = idSolicitud;
                    detalle.IdSolicitudDeTraspasoDeProductosNavigation = null;
                    detalle.IdProductoNavigation = null;

                    if (string.IsNullOrWhiteSpace(detalle.Unidad))
                    {
                        detalle.Unidad = "KG";
                    }

                    _unitOfWork.SolicitudesTraspasoDetalles.Create(detalle);
                }

                _unitOfWork.Complete();

                return idSolicitud;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al registrar la solicitud de traspaso.", ex);
            }
        }

        /// <summary>
        /// Aprueba una solicitud de traspaso pendiente, orquestando el movimiento real de stock entre el depósito origen y la sucursal destino de manera atómica.
        /// </summary>
        public void AprobarTraspaso(Guid idSolicitud)
        {
            var solicitud = _unitOfWork.SolicitudesTraspaso.GetById(idSolicitud);
            var detalles = _unitOfWork.SolicitudesTraspasoDetalles.GetByIdSolicitud(idSolicitud);

            try
            {
                foreach (var item in detalles)
                {
                    var stockOrigen = _unitOfWork.Stocks.GetByProductoYSucursal(item.IdProducto, solicitud.IdSucursalOrigen);
                    if (stockOrigen != null)
                    {
                        stockOrigen.StockActual -= item.Cantidad;
                        _unitOfWork.Stocks.Update(stockOrigen);
                    }

                    var stockDestino = _unitOfWork.Stocks.GetByProductoYSucursal(item.IdProducto, solicitud.IdSucursalDestino);
                    if (stockDestino != null)
                    {
                        stockDestino.StockActual += item.Cantidad;
                        _unitOfWork.Stocks.Update(stockDestino);
                    }
                    else
                    {
                        var nuevoStock = new StockPorSucursal
                        {
                            IdStockSucursal = Guid.NewGuid(),
                            IdProducto = item.IdProducto,
                            IdSucursal = solicitud.IdSucursalDestino,
                            StockActual = item.Cantidad,
                            StockDeseado = 0,
                            IdEstadoStock = 2
                        };
                        _unitOfWork.Stocks.Create(nuevoStock);
                    }
                }

                solicitud.IdEstadoStp = 2;
                _unitOfWork.SolicitudesTraspaso.Update(solicitud);

                _unitOfWork.Complete();
            }
            catch (StockInsuficienteException)
            {
                throw;
            }
        }

        /// <summary>
        /// Recupera todas las solicitudes de traspaso emitidas por una sucursal origen determinada.
        /// </summary>
        public List<SolicitudDeTraspasoDeProductoDTO> ObtenerTodasPorSucursal(Guid idSucursalOrigen)
        {
            var solicitudes = _unitOfWork.SolicitudesTraspaso.GetTodasPorSucursalOrigen(idSucursalOrigen);
            return _mapper.Map<List<SolicitudDeTraspasoDeProductoDTO>>(solicitudes);
        }

        /// <summary>
        /// Obtiene los detalles de los productos involucrados en una solicitud de traspaso específica.
        /// </summary>
        public List<SolicitudDeTraspasoDeProductosDetalleDTO> ObtenerDetallesPorSolicitud(Guid idSolicitud)
        {
            var detalles = _unitOfWork.SolicitudesTraspasoDetalles.GetByIdSolicitud(idSolicitud);
            return _mapper.Map<List<SolicitudDeTraspasoDeProductosDetalleDTO>>(detalles);
        }

        /// <summary>
        /// Cancela una solicitud de traspaso en curso, actualizando su estado sin afectar el inventario.
        /// </summary>
        public void RechazarTraspaso(Guid idSolicitud)
        {
            var solicitud = _unitOfWork.SolicitudesTraspaso.GetById(idSolicitud);

            try
            {
                solicitud.IdEstadoStp = 3;
                _unitOfWork.SolicitudesTraspaso.Update(solicitud);

                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al rechazar el traspaso.", ex);
            }
        }

        /// <summary>
        /// Ensambla el historial de movimientos de inventario (ingresos y egresos) aprobados para una sucursal específica.
        /// </summary>
        public List<HistorialTraspasoDTO> ObtenerHistorialTraspasos(Guid idSucursalActual)
        {
            var solicitudesBd = _unitOfWork.SolicitudesTraspaso.GetAll()
                .Where(t => (t.IdSucursalOrigen == idSucursalActual || t.IdSucursalDestino == idSucursalActual)
                         && t.IdEstadoStp == 2)
                .OrderByDescending(t => t.FechaStp)
                .ToList();

            var listaHistorial = new List<HistorialTraspasoDTO>();

            foreach (var t in solicitudesBd)
            {
                foreach (var detalle in t.SolicitudDeTraspasoDeProductosDetalles)
                {
                    var dto = new HistorialTraspasoDTO
                    {
                        Fecha = t.FechaStp,
                        Producto = detalle.IdProductoNavigation?.NombreProducto ?? "Desconocido",
                        Marca = detalle.IdProductoNavigation?.Marca,
                        Unidad = detalle.Unidad,
                        PesoNeto = detalle.PesoNeto,
                        Cantidad = detalle.Cantidad
                    };

                    if (t.IdSucursalDestino == idSucursalActual)
                    {
                        dto.TipoMovimiento = "INGRESO";
                        dto.SucursalInvolucrada = string.Format("Desde: {0}", t.IdSucursalOrigenNavigation?.NombreSucursal);
                    }
                    else
                    {
                        dto.TipoMovimiento = "EGRESO";
                        dto.SucursalInvolucrada = string.Format("Hacia: {0}", t.IdSucursalDestinoNavigation?.NombreSucursal);
                    }

                    listaHistorial.Add(dto);
                }
            }

            return listaHistorial;
        }
    }
}