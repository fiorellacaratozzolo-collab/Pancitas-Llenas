using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IVentaRepository
    {
        // Define las operaciones CRUD (o CUD) para la entidad Venta
        public interface IVentaRepository
        {
            /// <summary>
            /// Crea una nueva Venta junto con sus Detalles asociados en una única transacción.
            /// </summary>
            /// <param name="venta">La entidad Ventum principal.</param>
            /// <param name="detalles">La lista de VentaDetalle.</param>
            /// <returns>El ID (Guid) de la Venta recién creada.</returns>
            Guid Create(Ventum venta, List<VentaDetalle> detalles);
            Ventum GetById(Guid id);
            List<Ventum> GetAll();
        }
    }
}
