using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>Obtiene el catálogo completo de productos registrados.</summary>
        List<Producto> GetAll();

        /// <summary>Elimina o deshabilita un producto del sistema.</summary>
        void Delete(Guid id);
    }
}