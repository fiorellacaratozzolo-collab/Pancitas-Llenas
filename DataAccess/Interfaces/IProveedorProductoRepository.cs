using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IProveedorProductoRepository
    {
        /// <summary>
        /// Obtiene todos los registros de la tabla intermedia ProveedorProducto.
        /// Es crucial para la lógica que esto cargue las propiedades de navegación (Include)
        /// si se usa para obtener el objeto Producto.
        /// </summary>
        List<ProveedorProducto> GetAll();
        Guid Create(ProveedorProducto proveedorProducto);
    }
}
