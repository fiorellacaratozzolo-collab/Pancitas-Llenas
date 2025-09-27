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
        private readonly IUnitOfWork _unitOfWork;

        public SucursalLogic()
        {
            _unitOfWork = new UnitOfWork();
        }

        public Guid CrearSucursal(Sucursal sucursal)
        {
            if (string.IsNullOrWhiteSpace(sucursal.NombreSucursal) || string.IsNullOrWhiteSpace(sucursal.Direccion))
            {
                throw new ArgumentException("Nombre y Dirección de la sucursal son obligatorios.");
            }

            // Persistencia
            Guid idSucursal = _unitOfWork.Sucursales.Create(sucursal);

            // Confirmación de la transacción
            _unitOfWork.Complete();

            return idSucursal;
        }

        public List<Sucursal> ObtenerTodasLasSucursales()
        {
            return _unitOfWork.Sucursales.GetAll();
        }

        public List<Sucursal> BuscarPorTipoSucursal(int idTipoSucursal)
        {
            return _unitOfWork.Sucursales.GetByTipoSucursal(idTipoSucursal);
        }


        public void DeshabilitarSucursal(Guid id)
        {
            _unitOfWork.Sucursales.Delete(id);
            _unitOfWork.Complete(); // Confirma la eliminación
        }

        public void ActualizarSucursal(Sucursal sucursal)
        {
            _unitOfWork.Sucursales.Update(sucursal);
            _unitOfWork.Complete(); // Confirma la actualización
        }

        public Sucursal? ObtenerSucursalPorId(Guid id)
        {
            if (id == Guid.Empty) return null;
            return _unitOfWork.Sucursales.GetById(id);
        }

        public List<Sucursal> SearchByDireccion(string direccionFragment)
        {
            return _unitOfWork.Sucursales.SearchByDireccion(direccionFragment);
        }
    }
}
