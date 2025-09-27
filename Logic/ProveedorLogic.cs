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
        //IUnitOfWork
        private readonly IUnitOfWork _unitOfWork;

        public ProveedorLogic()
        {
            // Se instancia el UoW (que a su vez instancia y pasa el contexto al repositorio)
            _unitOfWork = new UnitOfWork();
        }

        public Guid CreateProveedor(Proveedor proveedor)
        {
            // La validación se mantiene aquí si es necesaria

            // Persistencia (UoW.Proveedores es el ProveedorRepository modificado)
            Guid idProveedor = _unitOfWork.Proveedores.Create(proveedor);

            // Confirmación de la transacción (Commit)
            _unitOfWork.Complete();

            return idProveedor;
        }

        public List<Proveedor> ObtenerTodosLosProveedores()
        {
            // Accedemos al Repositorio a través del UoW
            return _unitOfWork.Proveedores.GetAll();
        }

        public void DeshabilitarProveedor(Guid id)
        {
            _unitOfWork.Proveedores.Delete(id);
            _unitOfWork.Complete(); // Confirma la eliminación
        }

        public Proveedor? GetByCuit(int cuit)
        {
            return _unitOfWork.Proveedores.GetByCuit(cuit);
        }
    }
}

