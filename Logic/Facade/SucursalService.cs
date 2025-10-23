using DataAccess.Models;
using ModelsDTO;
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

        public Guid CreateSucursal(SucursalDTO sucursalDTO)
        {
            return _sucursalLogic.CrearSucursal(sucursalDTO);
        }

        public List<SucursalDTO> GetAllSucursales()
        {
            return _sucursalLogic.ObtenerTodasLasSucursales();
        }

        public List<SucursalDTO> GetByTipoSucursal(int idTipoSucursal)
        {
            return _sucursalLogic.BuscarPorTipoSucursal(idTipoSucursal);
        }

        public void DisableSucursal(Guid id)
        {
            _sucursalLogic.DeshabilitarSucursal(id);
        }

        public void UpdateSucursal(SucursalDTO sucursalDTO)
        {
            _sucursalLogic.ActualizarSucursal(sucursalDTO);
        }

        public SucursalDTO? GetById(Guid id)
        {
            return _sucursalLogic.GetById(id);
        }

        public List<SucursalDTO> SearchByDireccion(string direccionFragment)
        {
            return _sucursalLogic.SearchByDireccion(direccionFragment);
        }
    }
}
