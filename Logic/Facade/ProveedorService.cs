using DataAccess.Models;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Facade
{
    public class ProveedorService
    {
        private readonly ProveedorLogic _proveedorLogic;

        public ProveedorService()
        {
            _proveedorLogic = new ProveedorLogic();
        }

        public Guid CreateProveedor(ProveedorDTO proveedorDTO)
        {
            return _proveedorLogic.CreateProveedor(proveedorDTO);
        }
        public List<ProveedorDTO> GetAllProveedores()
        {
            return _proveedorLogic.ObtenerTodosLosProveedores();
        }

        public void DeshabilitarProveedor(Guid id)
        {
            _proveedorLogic.DeshabilitarProveedor(id);
        }

        public ProveedorDTO? GetByCuit(int cuit)
        {
            return _proveedorLogic.GetByCuit(cuit);
        }

    }
}
