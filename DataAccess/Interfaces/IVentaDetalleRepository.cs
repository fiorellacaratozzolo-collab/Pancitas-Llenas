using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define el contrato de persistencia para los renglones de detalle asociados a las ventas.
    /// </summary>
    public interface IVentaDetalleRepository
    {
        /// <summary>Inserta un nuevo renglón de detalle para una venta existente.</summary>
        void Create(VentaDetalle detalle);

        /// <summary>Recupera todos los registros de detalles de ventas.</summary>
        List<VentaDetalle> GetAll();
    }
}
