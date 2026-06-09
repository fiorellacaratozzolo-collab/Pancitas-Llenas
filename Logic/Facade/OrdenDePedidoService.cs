using DataAccess.Implementations.SqlServer;
using ModelsDTO;

namespace Logic.Facade
{
    /// <summary>
    /// Fachada encargada de orquestar el flujo de órdenes de pedido interno y su conversión a órdenes de compra externas.
    /// </summary>
    public class OrdenDePedidoService
    {
        private readonly OrdenDePedidoLogic _logic;

        /// <summary>
        /// Inicializa una nueva instancia del servicio inyectando el contexto de trabajo base.
        /// </summary>
        public OrdenDePedidoService()
        {
            _logic = new OrdenDePedidoLogic(new UnitOfWork());
        }

        /// <summary>
        /// Recupera la lista completa de órdenes de pedido generadas.
        /// </summary>
        public List<OrdenDePedidoDTO> ObtenerTodas()
        {
            return _logic.ObtenerTodas();
        }

        /// <summary>
        /// Obtiene una orden de pedido específica mediante su identificador.
        /// </summary>
        public OrdenDePedidoDTO ObtenerPorId(Guid id)
        {
            return _logic.ObtenerPorId(id);
        }

        /// <summary>
        /// Modifica el estado de una orden de pedido a Rechazada.
        /// </summary>
        public void RechazarOrden(Guid ordenId)
        {
            _logic.RechazarOrden(ordenId);
        }

        /// <summary>
        /// Recupera el detalle de renglones y productos incluidos en una orden de pedido.
        /// </summary>
        public List<OrdenDePedidoDetalleDTO> ObtenerDetallesPorOrden(Guid idOrden)
        {
            return _logic.ObtenerDetallesPorOrden(idOrden);
        }

        /// <summary>
        /// Ejecuta el proceso atómico de aprobación de una orden de pedido, agrupando sus detalles y generando las órdenes de compra resultantes.
        /// </summary>
        public ResultadoGeneracionOCsDTO AprobarYGenerarOCs(Guid ordenId)
        {
            try
            {
                var resultadosOC = _logic.AprobarYGenerarOrdenesDeCompra(ordenId);
                int numOCs = resultadosOC.Count;

                return new ResultadoGeneracionOCsDTO
                {
                    Exito = true,
                    Mensaje = string.Format("¡Proceso completado con éxito! Se generaron {0} Orden(es) de Compra para {1} proveedor(es).", numOCs, numOCs),
                    OrdenesCreadas = resultadosOC
                };
            }
            catch (KeyNotFoundException ex)
            {
                return new ResultadoGeneracionOCsDTO
                {
                    Exito = false,
                    Mensaje = string.Format("Error de datos: {0}", ex.Message),
                    OrdenesCreadas = new Dictionary<Guid, Guid>()
                };
            }
            catch (Exception ex)
            {
                return new ResultadoGeneracionOCsDTO
                {
                    Exito = false,
                    Mensaje = string.Format("Error grave en el sistema: {0}", ex.Message),
                    OrdenesCreadas = new Dictionary<Guid, Guid>()
                };
            }
        }
    }
}