using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define el contrato de persistencia para la administración del directorio de sucursales.
    /// </summary>
    public interface ISucursalRepository
    {
        /// <summary>Inserta una nueva sucursal y retorna su identificador único.</summary>
        Guid Create(Sucursal sucursal);

        /// <summary>Recupera una lista de sucursales filtrada por su categoría o tipo.</summary>
        List<Sucursal> GetByTipoSucursal(int idTipoSucursal);

        /// <summary>Obtiene el catálogo completo de sucursales registradas.</summary>
        List<Sucursal> GetAll();

        /// <summary>Busca una sucursal por su identificador único, retornando nulo si no existe.</summary>
        Sucursal? GetById(Guid id);

        /// <summary>Aplica las modificaciones realizadas sobre los datos de una sucursal existente.</summary>
        void Update(Sucursal sucursal);

        /// <summary>Deshabilita o elimina lógicamente una sucursal de la base de datos.</summary>
        void Delete(Guid id);

        /// <summary>Realiza una búsqueda de sucursales que contengan una cadena de texto específica en su dirección.</summary>
        List<Sucursal> SearchByDireccion(string direccionFragment);
    }
}