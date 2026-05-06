using AutoMapper;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Interfaces;
using DataAccess.Models;
using Logic.CustomExceptions;
using Logic.Helpers;
using Logic.MappingProfiles;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    /// <summary>
    /// Gestiona las reglas de negocio, cálculos de totales y transiciones de estado para las órdenes de pedido internas.
    /// </summary>
    public class OrdenDePedidoLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;

        /// <summary>
        /// Inicializa una nueva instancia de la lógica con un contexto de trabajo inyectado para garantizar la atomicidad compartida.
        /// </summary>
        public OrdenDePedidoLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Genera una orden de pedido formal calculando sus subtotales y total general a partir de una solicitud de pedido previamente aprobada.
        /// </summary>
        public Guid CrearOrdenDesdeSolicitud(SolicitudDePedido solicitudAprobada)
        {
            var ordenDePedido = _mapper.Map<OrdenDePedido>(solicitudAprobada);
            ordenDePedido.IdOrdenDePedido = Guid.NewGuid();
            ordenDePedido.FechaOp = DateTime.Today;
            ordenDePedido.IdEstadoOp = 1;

            decimal totalCalculado = 0;

            ordenDePedido.OrdenDePedidoDetalles = new List<OrdenDePedidoDetalle>();

            foreach (var detalleSP in solicitudAprobada.SolicitudDePedidoDetalles)
            {
                var detalleOP = _mapper.Map<OrdenDePedidoDetalle>(detalleSP);
                detalleOP.IdOrdenDePedidoDetalle = Guid.NewGuid();
                detalleOP.IdOrdenDePedido = ordenDePedido.IdOrdenDePedido;
                var producto = _unitOfWork.Productos.GetById(detalleSP.IdProducto);
                decimal precioReal = producto?.PrecioNeto ?? 0m;

                detalleOP.PrecioUnitario = precioReal;

                totalCalculado += (detalleOP.Cantidad * precioReal);

                ordenDePedido.OrdenDePedidoDetalles.Add(detalleOP);
            }

            ordenDePedido.Total = totalCalculado;

            _unitOfWork.OrdenDePedidos.Create(ordenDePedido);
            _unitOfWork.OrdenDePedidoDetalles.AddRange(ordenDePedido.OrdenDePedidoDetalles);

            return ordenDePedido.IdOrdenDePedido;
        }

        /// <summary>
        /// Recupera el listado completo de las órdenes de pedido generadas en el sistema.
        /// </summary>
        public List<OrdenDePedidoDTO> ObtenerTodas()
        {
            var ordenes = _unitOfWork.OrdenDePedidos.GetAll();
            return _mapper.Map<List<OrdenDePedidoDTO>>(ordenes);
        }

        /// <summary>
        /// Recupera una orden de pedido específica validando previamente su existencia en la base de datos.
        /// </summary>
        public OrdenDePedidoDTO ObtenerPorId(Guid id)
        {
            var orden = _unitOfWork.OrdenDePedidos.GetById(id);
            if (orden == null)
                throw new KeyNotFoundException(string.Format("No se encontró la Orden de Pedido con ID {0}", id));

            return _mapper.Map<OrdenDePedidoDTO>(orden);
        }

        /// <summary>
        /// Extrae el detalle de productos, cantidades y precios asociados a una orden de pedido específica.
        /// </summary>
        public List<OrdenDePedidoDetalleDTO> ObtenerDetallesPorOrden(Guid idOrden)
        {
            var detalles = _unitOfWork.OrdenDePedidoDetalles.GetByIdOrden(idOrden);

            return _mapper.Map<List<OrdenDePedidoDetalleDTO>>(detalles);
        }

        /// <summary>
        /// Cambia el estado de una orden de pedido a Rechazada, validando previamente que no haya sido aprobada o rechazada con anterioridad.
        /// </summary>
        public void RechazarOrden(Guid ordenId)
        {
            var orden = _unitOfWork.OrdenDePedidos.GetById(ordenId);
            if (orden == null) throw new KeyNotFoundException();

            if (orden.IdEstadoOp == 2)
            {
                throw new TransicionEstadoInvalidaException("Aprobada", "Rechazada");
            }
            else if (orden.IdEstadoOp == 3)
            {
                throw new TransicionEstadoInvalidaException("Rechazada", "Rechazada");
            }

            orden.IdEstadoOp = 3;
            _unitOfWork.OrdenDePedidos.Update(orden);
            _unitOfWork.Complete();
        }

        /// <summary>
        /// Aprueba una orden de pedido pendiente, agrupa sus detalles por proveedor y dispara la generación de múltiples órdenes de compra de manera atómica.
        /// </summary>
        public Dictionary<Guid, Guid> AprobarYGenerarOrdenesDeCompra(Guid ordenId)
        {
            var orden = _unitOfWork.OrdenDePedidos.GetById(ordenId);
            if (orden == null)
                throw new KeyNotFoundException(string.Format("No se encontró la Orden de Pedido con ID {0}", ordenId));

            if (orden.IdEstadoOp == 2)
            {
                throw new TransicionEstadoInvalidaException("Aprobada", "Aprobada");
            }
            else if (orden.IdEstadoOp == 3)
            {
                throw new TransicionEstadoInvalidaException("Rechazada", "Aprobada");
            }

            var proveedorHelper = new Logic.Helpers.ProveedorHelper(_unitOfWork);
            var ordenDeCompraLogic = new OrdenDeCompraLogic(_unitOfWork);

            var detallesOCConInfo = orden.OrdenDePedidoDetalles
                .Select(detalleOP =>
                {
                    try
                    {
                        var info = proveedorHelper.ObtenerProveedorInfoParaProducto(detalleOP.IdProducto);

                        var detalleOC = _mapper.Map<DataAccess.Models.OrdenDeCompraDetalle>(detalleOP);
                        var producto = _unitOfWork.Productos.GetById(detalleOP.IdProducto);

                        detalleOC.IdOrdenDeCompraDetalle = Guid.NewGuid();
                        detalleOC.PrecioUnitario = info.PrecioNeto;
                        detalleOC.Subtotal = detalleOC.Cantidad * detalleOC.PrecioUnitario;
                        detalleOC.Unidad = producto?.Unidad ?? "Unidad";
                        detalleOC.IdProducto = detalleOP.IdProducto;

                        return new
                        {
                            DetalleOC = detalleOC,
                            info.IdProveedor
                        };
                    }
                    catch (KeyNotFoundException ex)
                    {
                        throw new InvalidOperationException(string.Format("Fallo de datos: {0} (OP ID: {1}).", ex.Message, ordenId), ex);
                    }
                })
                .ToList();

            var detallesAgrupados = detallesOCConInfo.GroupBy(d => d.IdProveedor);

            orden.IdEstadoOp = 2;
            _unitOfWork.OrdenDePedidos.Update(orden);

            var resultadosOC = new Dictionary<Guid, Guid>();

            foreach (var grupo in detallesAgrupados)
            {
                Guid idProveedor = grupo.Key;

                if (idProveedor == Guid.Empty)
                {
                    throw new InvalidOperationException(string.Format("Error de integridad de datos: Se detectó un proveedor vacío (GUID 000...) en el pedido {0}. Esto indica un fallo en la obtención de datos del Helper.", ordenId));
                }

                List<DataAccess.Models.OrdenDeCompraDetalle> nuevosDetallesOC = grupo
                    .Select(item => item.DetalleOC)
                    .ToList();

                Guid idNuevaOC = ordenDeCompraLogic.CrearOrdenDesdePedidoAgrupado(orden, nuevosDetallesOC, idProveedor);
                resultadosOC.Add(idProveedor, idNuevaOC);
            }

            _unitOfWork.Complete();

            return resultadosOC;
        }
    }
}