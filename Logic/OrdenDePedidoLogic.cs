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
            // 1. Obtener la OP
            var orden = _unitOfWork.OrdenDePedidos.GetById(ordenId);
            if (orden == null)
                throw new KeyNotFoundException($"No se encontró la Orden de Pedido con ID {ordenId}");

            var proveedorHelper = new Logic.Helpers.ProveedorHelper(_unitOfWork);
            var ordenDeCompraLogic = new OrdenDeCompraLogic(_unitOfWork);

            // 2. Procesar Detalles, Obtener Precio/Proveedor y Mapear a OCDetalle
            var detallesOCConInfo = orden.OrdenDePedidoDetalles
                .Select(detalleOP =>
                {
                    try
                    {
                        // Usa el IdProducto recién agregado a la entidad OPDetalle
                        //CRÍTICO: La llamada al Helper DEBE lanzar KeyNotFoundException si no hay vínculo.
                        var info = proveedorHelper.ObtenerProveedorInfoParaProducto(detalleOP.IdProducto);

                        // Mapeo OPDetalle -> OCDetalle
                        var detalleOC = _mapper.Map<DataAccess.Models.OrdenDeCompraDetalle>(detalleOP);
                        //Obtenemos la unidad del Producto
                        var producto = _unitOfWork.Productos.GetById(detalleOP.IdProducto);
                        // Inyección de lógica financiera
                        detalleOC.IdOrdenDeCompraDetalle = Guid.NewGuid();
                        detalleOC.PrecioUnitario = info.PrecioNeto;
                        detalleOC.Subtotal = detalleOC.Cantidad * detalleOC.PrecioUnitario;
                        detalleOC.Unidad = producto?.Unidad ?? "Unidad"; // Usar el valor del producto o un valor por defecto.
                        // Aseguramos que el IdProducto se mantiene
                        detalleOC.IdProducto = detalleOP.IdProducto;

                        return new
                        {
                            DetalleOC = detalleOC,
                            info.IdProveedor // Este es el campo clave para la agrupación
                        };
                    }
                    catch (KeyNotFoundException ex)
                    {
                        // Si el Helper no encuentra el proveedor, relanzamos con una InvalidOperationException 
                        // para que el Servicio capture el error de forma limpia antes del Commit.
                        throw new InvalidOperationException($"Fallo de datos: {ex.Message} (OP ID: {ordenId}).", ex);
                    }
                })
                .ToList(); // El ToList() fuerza la ejecución del Select y las llamadas al Helper.

            // 3. Agrupar por Proveedor
            var detallesAgrupados = detallesOCConInfo.GroupBy(d => d.IdProveedor);

            // 4. Marcar OP como Aprobada
            orden.IdEstadoOp = 2;
            _unitOfWork.OrdenDePedidos.Update(orden);

            var resultadosOC = new Dictionary<Guid, Guid>();

            // 5. Crear una OC por cada grupo (proveedor)
            foreach (var grupo in detallesAgrupados)
            {
                Guid idProveedor = grupo.Key;

                //VALIDACIÓN DE INTEGRIDAD DE DATOS: Asegura que la clave no sea vacía.
                if (idProveedor == Guid.Empty)
                {
                    throw new InvalidOperationException($"Error de integridad de datos: Se detectó un proveedor vacío (GUID 000...) en el pedido {ordenId}. Esto indica un fallo en la obtención de datos del Helper.");
                }

                List<DataAccess.Models.OrdenDeCompraDetalle> nuevosDetallesOC = grupo
                    .Select(item => item.DetalleOC)
                    .ToList();

                // Llamada a la creación que ahora persiste IdProveedor en la cabecera
                Guid idNuevaOC = ordenDeCompraLogic.CrearOrdenDesdePedidoAgrupado(orden, nuevosDetallesOC, idProveedor);
                resultadosOC.Add(idProveedor, idNuevaOC);
            }

            // 6. Commit de la transacción atómica (actualiza OP y crea todas las OCs)
            _unitOfWork.Complete();

            return resultadosOC;
        }
    }
}