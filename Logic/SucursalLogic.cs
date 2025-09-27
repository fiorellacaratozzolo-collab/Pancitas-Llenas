using DataAccess.Implementations.SqlServer;
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
        private readonly ISucursalRepository _sucursalRepository;

        public SucursalLogic()
        {
            _sucursalRepository = new SucursalRepository();
        }

        public Guid CrearSucursal(Sucursal sucursal)
        {
            // Validaciones de negocio:
            if (string.IsNullOrWhiteSpace(sucursal.NombreSucursal) || string.IsNullOrWhiteSpace(sucursal.Direccion))
            {
                throw new ArgumentException("Nombre y Dirección de la sucursal son obligatorios.");
            }
            // Podrías agregar una validación para asegurar que IdTipoSucursal sea 1 (Venta) o 2 (DepositoVenta)

            return _sucursalRepository.Create(sucursal);
        }

        public List<Sucursal> ObtenerTodasLasSucursales()
        {
            return _sucursalRepository.GetAll();
        }

        public List<Sucursal> BuscarPorTipoSucursal(int idTipoSucursal)
        {
            return _sucursalRepository.GetByTipoSucursal(idTipoSucursal);
        }

        public void DeshabilitarSucursal(Guid id)
        {
            _sucursalRepository.Delete(id);
        }

        public void ActualizarSucursal(Sucursal sucursal)
        {
            // Validaciones previas a la actualización
            _sucursalRepository.Update(sucursal);
        }

        public Sucursal? ObtenerSucursalPorId(Guid id) // Importante usar el '?' para evitar CS8603
        {
            if (id == Guid.Empty)
            {
                return null;
            }
            return _sucursalRepository.GetById(id);
        }

        public List<Sucursal> SearchByDireccion(string direccionFragment)
        {
            // Llama al método del Logic/Repository
            return _sucursalRepository.SearchByDireccion(direccionFragment);
        }
    }
}
