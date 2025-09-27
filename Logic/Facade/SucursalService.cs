using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Facade
{
    public class SucursalService
    {
        private readonly SucursalLogic _sucursalLogic = new SucursalLogic();

        public Guid CreateSucursal(Sucursal sucursal)
        {
            return _sucursalLogic.CrearSucursal(sucursal);
        }

        public List<Sucursal> GetAllSucursales()
        {
            return _sucursalLogic.ObtenerTodasLasSucursales();
        }

        public List<Sucursal> GetByTipoSucursal(int idTipoSucursal)
        {
            return _sucursalLogic.BuscarPorTipoSucursal(idTipoSucursal);
        }

        public void DisableSucursal(Guid id)
        {
            _sucursalLogic.DeshabilitarSucursal(id);
        }

        public void UpdateSucursal(Sucursal sucursal)
        {
            _sucursalLogic.ActualizarSucursal(sucursal);
        }

        public Sucursal? GetById(Guid id)
        {
            return _sucursalLogic.ObtenerSucursalPorId(id);
        }

        public List<Sucursal> SearchByDireccion(string direccionFragment)
        {
            // Llama al método del Logic/Repository
            return _sucursalLogic.SearchByDireccion(direccionFragment);
        }
    }

}
