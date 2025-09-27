using DataAccess.Models;
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

        public Guid CreateProveedor(Proveedor proveedor)
        {
            return _proveedorLogic.CreateProveedor(proveedor);
        }
        public List<Proveedor> GetAllProveedores()
        {
            return _proveedorLogic.ObtenerTodosLosProveedores();
        }

        public void DeshabilitarProveedor(Guid id)
        {
            _proveedorLogic.DeshabilitarProveedor(id);
        }

        public Proveedor? GetByCuit(int cuit)
        {
            // El método de Lógica (ProveedorLogic) debe llamarse de forma similar.
            return _proveedorLogic.GetByCuit(cuit);
        }

    }
}
