using DataAccess.Models;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define el contrato de persistencia para la gestión de cabeceras de ventas en la base de datos.
    /// </summary>
    public interface IVentaRepository
    {
        /// <summary>Inserta una nueva venta y retorna su identificador único generado.</summary>
        Guid Create(Ventum venta);

        /// <summary>Recupera el historial completo de todas las ventas del sistema.</summary>
        List<Ventum> GetAll();

        /// <summary>Elimina o anula un registro de venta existente.</summary>
        void Delete(Guid id);

        /// <summary>Filtra y recupera las ventas correspondientes a una sucursal específica.</summary>
        List<Ventum> GetBySucursal(Guid idSucursal);
    }
}
