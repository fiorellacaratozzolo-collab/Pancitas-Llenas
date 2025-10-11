using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class SucursalLogic
    {

        public Guid CrearSucursal(Sucursal sucursal)
        {        
            using (var unitOfWork = new UnitOfWork())
            {
                if (string.IsNullOrWhiteSpace(sucursal.NombreSucursal) || string.IsNullOrWhiteSpace(sucursal.Direccion))
                {
                    throw new ArgumentException("Nombre y Dirección de la sucursal son obligatorios.");
                }
                Guid idSucursal = unitOfWork.Sucursales.Create(sucursal);

                unitOfWork.Complete();

                return idSucursal;
            }
        }

        public List<Sucursal> ObtenerTodasLasSucursales()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.Sucursales.GetAll();
            }
        }

        public List<Sucursal> BuscarPorTipoSucursal(int idTipoSucursal)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.Sucursales.GetByTipoSucursal(idTipoSucursal);
            }
        }

        public Sucursal? GetById(Guid id)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.Sucursales.GetById(id);
            }
        }

        public void DeshabilitarSucursal(Guid id)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                unitOfWork.Sucursales.Delete(id);
                unitOfWork.Complete(); // Confirma la eliminación
            }
        }

        public void ActualizarSucursal(Sucursal sucursal)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                unitOfWork.Sucursales.Update(sucursal);
                unitOfWork.Complete(); // Confirma la actualización
            }
        }

        public Sucursal? ObtenerSucursalPorId(Guid id)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                if (id == Guid.Empty) return null;
                return unitOfWork.Sucursales.GetById(id);
            }
        }

        public List<Sucursal> SearchByDireccion(string direccionFragment)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.Sucursales.SearchByDireccion(direccionFragment);
            }
        }
    }
}
