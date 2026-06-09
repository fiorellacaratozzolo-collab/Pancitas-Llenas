using DataAccess.Models;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define el contrato de persistencia para la gestión de productos en el catálogo del sistema.
    /// </summary>
    public interface IProductoRepository
    {
        /// <summary>Crea un nuevo producto y lo vincula inmediatamente a un proveedor específico.</summary>
        Guid Create(Producto producto, Guid idProveedor);

        /// <summary>Obtiene todos los productos vinculados a un proveedor específico.</summary>
        List<Producto> GetByProveedor(Guid idProveedor);

        /// <summary>Recupera un producto específico a partir de su identificador único.</summary>
        Producto? GetById(Guid id);

        /// <summary>Obtiene el catálogo completo de productos registrados (sin filtrar por estado).</summary>
        List<Producto> GetAll();

        /// <summary>Realiza un Borrado Lógico del producto cambiando su estado a inactivo (Activo = false).</summary>
        void Delete(Guid id);

        /// <summary>Reactiva un producto previamente deshabilitado (Activo = true).</summary>
        void Habilitar(Guid id);

        /// <summary>Actualiza los valores de un producto existente y su relación en la tabla intermedia de proveedores.</summary>
        void Update(Producto producto, Guid idNuevoProveedor);
    }
}