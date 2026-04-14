using DataAccess.Models;
using ModelsDTO;
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

        public Guid RegistrarVenta(VentumDTO ventaDTO, List<VentaDetalleDTO> detallesDTO, Guid idSucursal)
        {
            return _ventaLogic.RegistrarVenta(ventaDTO, detallesDTO, idSucursal);
        }

        public List<VentumDTO> GetVentasPorSucursalYFecha(Guid idSucursal, DateTime fecha)
        {
            return _ventaLogic.GetVentasPorSucursalYFecha(idSucursal, fecha);
        }

        public List<VentaDetalleDTO> GetDetallesDeVenta(Guid idVenta)
        {
            return _ventaLogic.GetDetallesDeVenta(idVenta);
        }

        public void AnularVenta(Guid idVenta, Guid idSucursal)
        {
            _ventaLogic.AnularVenta(idVenta, idSucursal);
        }

    }
}
