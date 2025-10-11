using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
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
        public Guid CreateProveedor(Proveedor proveedor)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                // Persistencia
                Guid idProveedor = unitOfWork.Proveedores.Create(proveedor);
                unitOfWork.Complete();

                return idProveedor;
            }
        }

        public List<Proveedor> ObtenerTodosLosProveedores()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                // Accedemos al Repositorio a través del UoW
                return unitOfWork.Proveedores.GetAll();
            }
        }

        public void DeshabilitarProveedor(Guid id)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                unitOfWork.Proveedores.Delete(id);
                unitOfWork.Complete();
            }
        }

        public Proveedor? GetByCuit(int cuit)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.Proveedores.GetByCuit(cuit);
            }
        }
    }
}

