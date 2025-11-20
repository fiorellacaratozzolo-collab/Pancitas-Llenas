using AutoMapper;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Interfaces;
using DataAccess.Models;
using Logic.MappingProfiles;
using ModelsDTO;
using System;
using Logic.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class OrdenDePedidoLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;

        // Recibe IUnitOfWork para la atomicidad en la transición SP -> OP
        public OrdenDePedidoLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Guid CrearOrdenDesdeSolicitud(SolicitudDePedido solicitudAprobada)
        {
            // 1. Mapeo SP -> OP (Cabecera)
            var ordenDePedido = _mapper.Map<OrdenDePedido>(solicitudAprobada);
            ordenDePedido.IdOrdenDePedido = Guid.NewGuid();
            ordenDePedido.FechaOp = DateTime.Today;
            ordenDePedido.IdEstadoOp = 1; // Estado inicial: Pendiente de Gestión
            ordenDePedido.Total = 0; // Se debe calcular o asignar

            // 2. Mapeo Detalles
            ordenDePedido.OrdenDePedidoDetalles = solicitudAprobada.SolicitudDePedidoDetalles
                .Select(detalleSP =>
                {
                    var detalleOP = _mapper.Map<OrdenDePedidoDetalle>(detalleSP); // Mapeo Detalle SP -> Detalle OP
                    detalleOP.IdOrdenDePedidoDetalle = Guid.NewGuid();
                    detalleOP.IdOrdenDePedido = ordenDePedido.IdOrdenDePedido;
                    // Aquí se calcularía/asignaría PrecioUnitario
                    detalleOP.PrecioUnitario = 1; // EJEMPLO
                    return detalleOP;
                }).ToList();

            // 3. Persistencia
            _unitOfWork.OrdenDePedidos.Create(ordenDePedido);
            _unitOfWork.OrdenDePedidoDetalles.AddRange(ordenDePedido.OrdenDePedidoDetalles);

            // NO se llama Complete(). Lo hace SolicitudDePedidoLogic.
            return ordenDePedido.IdOrdenDePedido;
        }

        public List<OrdenDePedidoDTO> ObtenerTodas()
        {
            var ordenes = _unitOfWork.OrdenDePedidos.GetAll();
            return _mapper.Map<List<OrdenDePedidoDTO>>(ordenes);
        }

        public OrdenDePedidoDTO ObtenerPorId(Guid id)
        {
            var orden = _unitOfWork.OrdenDePedidos.GetById(id);
            if (orden == null)
                throw new KeyNotFoundException($"No se encontró la Orden de Pedido con ID {id}");

            return _mapper.Map<OrdenDePedidoDTO>(orden);
        }

        // --- GESTIÓN DE ESTADO ---

        public void RechazarOrden(Guid ordenId)
        {
            var orden = _unitOfWork.OrdenDePedidos.GetById(ordenId);
            if (orden == null) throw new KeyNotFoundException();

            orden.IdEstadoOp = 3; // Rechazada
            _unitOfWork.OrdenDePedidos.Update(orden);
            _unitOfWork.Complete();
        }

        public Dictionary<Guid, Guid> AprobarYGenerarOrdenesDeCompra(Guid ordenId)
        {
            var orden = _unitOfWork.OrdenDePedidos.GetById(ordenId);
            if (orden == null)
                throw new KeyNotFoundException($"No se encontró la Orden de Pedido con ID {ordenId}");

            var proveedorHelper = new Logic.Helpers.ProveedorHelper(_unitOfWork);
            var ordenDeCompraLogic = new OrdenDeCompraLogic(_unitOfWork);

            // 1. Asignar proveedor/precio a CADA detalle y agrupar
            var detallesOCConInfo = orden.OrdenDePedidoDetalles
                .Select(detalleOP =>
                {
                    // Obtener info del proveedor y precio del producto
                    var info = proveedorHelper.ObtenerProveedorInfoParaProducto(detalleOP.IdProducto);

                    // Mapear la entidad de detalle OP a la entidad de detalle OC (usando AutoMapper)
                    var detalleOC = _mapper.Map<DataAccess.Models.OrdenDeCompraDetalle>(detalleOP);

                    // Soluciones a errores de EF Core e inyección de lógica
                    detalleOC.IdOrdenDeCompraDetalle = Guid.NewGuid();
                    detalleOC.PrecioUnitario = info.PrecioNeto;
                    detalleOC.Subtotal = detalleOC.Cantidad * detalleOC.PrecioUnitario;

                    return new
                    {
                        DetalleOC = detalleOC,
                        info.IdProveedor
                    };
                })
                .ToList();

            // 2. Agrupar los detalles de OC por proveedor
            var detallesAgrupados = detallesOCConInfo.GroupBy(d => d.IdProveedor);

            // 3. Marcar la OP como Aprobada (ID 2) y persistir el estado
            orden.IdEstadoOp = 2;
            _unitOfWork.OrdenDePedidos.Update(orden);

            var resultadosOC = new Dictionary<Guid, Guid>();

            // 4. Crear una OC por cada grupo (proveedor)
            foreach (var grupo in detallesAgrupados)
            {
                Guid idProveedor = grupo.Key;

                // Extraer las entidades OrdenDeCompraDetalle del objeto anónimo
                List<DataAccess.Models.OrdenDeCompraDetalle> nuevosDetallesOC = grupo
                    .Select(item => item.DetalleOC)
                    .ToList();

                // 5. Llamar a la creación monoproveedor (pasa el ID del proveedor)
                Guid idNuevaOC = ordenDeCompraLogic.CrearOrdenDesdePedidoAgrupado(orden, nuevosDetallesOC, idProveedor);
                resultadosOC.Add(idProveedor, idNuevaOC);
            }

            // 6. Commit de la transacción atómica
            _unitOfWork.Complete();

            return resultadosOC;
        }
    }
}