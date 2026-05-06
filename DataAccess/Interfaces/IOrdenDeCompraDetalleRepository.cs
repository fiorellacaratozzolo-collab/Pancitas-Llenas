using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define el contrato de persistencia para los renglones de detalle de las órdenes de compra.
    /// </summary>
    public interface IOrdenDeCompraDetalleRepository
    {
        /// <summary>Inserta un bloque de detalles asociados a una orden de compra.</summary>
        void AddRange(IEnumerable<OrdenDeCompraDetalle> detalles);

        /// <summary>Obtiene los detalles de los productos incluidos en una orden de compra específica.</summary>
        List<OrdenDeCompraDetalle> GetByIdOrdenCompra(Guid idOrdenCompra);
    }
}