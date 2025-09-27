using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IProductoRepository
    {
        /// <summary>
        /// Crea un Producto y lo vincula inmediatamente a un Proveedor específico.
        /// </summary>
        /// <param name="producto">La entidad Producto a crear.</param>
        /// <param name="idProveedor">El GUID del proveedor al que se vinculará.</param>
        /// <returns>El ID (Guid) del Producto creado.</returns>
        Guid Create(Producto producto, Guid idProveedor);

        /// <summary>
        /// Obtiene todos los productos vinculados a un proveedor específico.
        /// </summary>
        /// <param name="idProveedor">El GUID del proveedor para filtrar.</param>
        /// <returns>Una lista de Productos.</returns>
        List<Producto> GetByProveedor(Guid idProveedor);

        // Métodos base
        Producto? GetById(Guid id);
        List<Producto> GetAll();
        void Delete(Guid id); // Para deshabilitar/eliminar
    }
}
