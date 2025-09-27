using DataAccess.Implementations.SqlServer;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class ProveedorLogic
    {
        private readonly ProveedorRepository _proveedorRepository;

        public ProveedorLogic()
        {
            _proveedorRepository = new ProveedorRepository();
        }

        public Guid CreateProveedor(Proveedor proveedor)
        {
            return _proveedorRepository.Create(proveedor);
        }
        public List<Proveedor> ObtenerTodosLosProveedores()
        {
            // Delega la operación al repositorio
            return _proveedorRepository.GetAll();
        }

        public void DeshabilitarProveedor(Guid id)
        {
            _proveedorRepository.Delete(id);
        }

        public Proveedor? GetByCuit(int cuit)
        {
            return _proveedorRepository.GetByCuit(cuit);
        }
    }
}

