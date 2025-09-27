using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ISucursalRepository
    {
        Guid Create(Sucursal sucursal);

        // Para la búsqueda por Tipo de Sucursal
        List<Sucursal> GetByTipoSucursal(int idTipoSucursal);

        List<Sucursal> GetAll();

        Sucursal? GetById(Guid id);

        void Update(Sucursal sucursal);

        void Delete(Guid id); // Para deshabilitar/eliminar

        List<Sucursal> SearchByDireccion(string direccionFragment);
    }
}
