using DataAccess.Models;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define el contrato de persistencia para la administración del directorio de sucursales.
    /// </summary>
    public interface ISucursalRepository
    {
        /// <summary>Inserta una nueva sucursal y retorna su identificador único.</summary>
        Guid Create(Sucursal sucursal);

        /// <summary>Realiza un borrado lógico cambiando el estado de la sucursal a inactivo.</summary>
        void Delete(Guid id);

        /// <summary>Reactiva una sucursal previamente deshabilitada.</summary>
        void Habilitar(Guid id);

        /// <summary>Recupera una lista de sucursales filtrada por su categoría o tipo.</summary>
        List<Sucursal> GetByTipoSucursal(int idTipoSucursal);

        /// <summary>Obtiene el catálogo completo de sucursales registradas (sin filtrar por estado).</summary>
        List<Sucursal> GetAll();

        /// <summary>Busca una sucursal por su identificador único, retornando nulo si no existe.</summary>
        Sucursal? GetById(Guid id);

        /// <summary>Aplica las modificaciones realizadas sobre los datos de una sucursal existente.</summary>
        void Update(Sucursal sucursal);

        /// <summary>Realiza una búsqueda de sucursales que contengan una cadena de texto específica en su dirección.</summary>
        List<Sucursal> SearchByDireccion(string direccionFragment);
    }
}