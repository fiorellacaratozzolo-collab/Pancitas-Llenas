using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define el contrato de persistencia para los detalles de las órdenes de pedido.
    /// </summary>
    public interface IOrdenDePedidoDetalleRepository
    {
        /// <summary>Inserta una colección de detalles asociados a una orden de pedido.</summary>
        void AddRange(IEnumerable<OrdenDePedidoDetalle> detalles);

        /// <summary>Recupera los detalles correspondientes a una orden de pedido específica.</summary>
        List<OrdenDePedidoDetalle> GetByIdOrden(Guid idOrden);
    }
}