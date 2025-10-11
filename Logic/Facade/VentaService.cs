using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Facade
{
    public class VentaService
    {
        private readonly VentaLogic _ventaLogic;

        public VentaService()
        {
            _ventaLogic = new VentaLogic();
        }

        public Guid RegistrarVenta(Ventum venta, List<VentaDetalle> detalles, Guid idSucursal)
        {
            return _ventaLogic.RegistrarVenta(venta, detalles, idSucursal);
        }

    }
}
