using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
using ModelsDTO;
using System;
using System.Collections.Generic;

namespace Logic.Facade
{
    /// <summary>
    /// Fachada para la gestión, seguimiento y transición de estado de las órdenes de compra emitidas a proveedores.
    /// </summary>
    public class OrdenDeCompraService
    {
        private readonly OrdenDeCompraLogic _logic;

        /// <summary>
        /// Inicializa una nueva instancia del servicio inyectando el contexto de trabajo base.
        /// </summary>
        public OrdenDeCompraService()
        {
            _logic = new OrdenDeCompraLogic(new UnitOfWork());
        }

        /// <summary>
        /// Recupera una orden de compra completa, incluyendo sus detalles y relaciones, a partir de su ID.
        /// </summary>
        public OrdenDeCompraDTO ObtenerPorId(Guid id)
        {
            return _logic.ObtenerPorId(id);
        }

        /// <summary>
        /// Obtiene el catálogo completo de órdenes de compra del sistema.
        /// </summary>
        public List<OrdenDeCompraDTO> ObtenerTodas()
        {
            return _logic.ObtenerTodas();
        }

        /// <summary>
        /// Cambia el estado de una orden de compra a Rechazada.
        /// </summary>
        public void RechazarOrden(Guid ordenId)
        {
            _logic.RechazarOrden(ordenId);
        }

        /// <summary>
        /// Aprueba y finaliza el ciclo de vida de una orden de compra.
        /// </summary>
        public void FinalizarOrden(Guid ordenId)
        {
            _logic.FinalizarOrden(ordenId);
        }

        /// <summary>
        /// Recupera el detalle de los productos y precios pactados en una orden de compra específica.
        /// </summary>
        public List<OrdenDeCompraDetalleDTO> ObtenerDetallesPorOrden(Guid idOrdenCompra)
        {
            return _logic.ObtenerDetallesPorOrden(idOrdenCompra);
        }
    }
}