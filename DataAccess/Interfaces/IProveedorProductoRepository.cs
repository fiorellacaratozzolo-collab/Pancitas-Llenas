using DataAccess.Models;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define el contrato de persistencia para la tabla intermedia que vincula proveedores con productos.
    /// </summary>
    public interface IProveedorProductoRepository
    {
        /// <summary>Obtiene todos los registros de vínculos asegurando la carga de las propiedades de navegación.</summary>
        List<ProveedorProducto> GetAll();

        /// <summary>Crea un nuevo vínculo entre un proveedor y un producto.</summary>
        Guid Create(ProveedorProducto proveedorProducto);
    }
}