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
        private readonly VentaLogic _ventaLogic = new VentaLogic();
        // private readonly TipoVentaLogic _tipoVentaLogic = new TipoVentaLogic(); // Necesario para el RadioButton

        //public Guid RegistrarVenta(Ventum venta, List<VentaDetalle> detalles)
        //{
        //    // La UI llama directamente a este método
        //    return _ventaLogic.RegistrarVenta(venta, detalles);
        //}

        // public List<TipoVentaEnum> GetTiposVenta() { ... } // Para el ComboBox/RadioButton
    }
}
