using DataAccess.Models;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define el contrato de persistencia para los renglones de detalle asociados a las ventas.
    /// </summary>
    public interface IVentaDetalleRepository
    {
        /// <summary>Inserta un nuevo renglón de detalle para una venta existente.</summary>
        void Create(VentaDetalle detalle);

        /// <summary>Recupera todos los registros de detalles de ventas.</summary>
        List<VentaDetalle> GetAll();
    }
}
