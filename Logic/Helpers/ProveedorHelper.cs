using DataAccess.Implementations.UnitOfWork;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helpers
{
    /// <summary>
    /// Proporciona métodos de asistencia para resolver relaciones complejas entre proveedores y productos, extrayendo información crítica para las operaciones de compra.
    /// </summary>
    public class ProveedorHelper
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia del helper inyectando un contexto de trabajo (Unit of Work) compartido para mantener la consistencia en las consultas.
        /// </summary>
        public ProveedorHelper(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        /// <summary>
        /// Obtiene el proveedor asociado a un producto y su precio de costo actual, resolviendo la tabla intermedia. Lanza una excepción si la relación no existe o los datos están incompletos.
        /// </summary>
        public ProveedorProductoInfo ObtenerProveedorInfoParaProducto(Guid idProducto)
        {
            var vinculo = _unitOfWork.ProveedorProductos.GetAll()
                .AsQueryable()
                .Include(pp => pp.IdProductoNavigation)
                .FirstOrDefault(pp => pp.IdProducto == idProducto);

            if (vinculo == null || vinculo.IdProductoNavigation == null)
            {
                throw new KeyNotFoundException(string.Format("No se pudo generar la OC: El Producto con ID '{0}' no tiene un proveedor asignado o los datos de producto están incompletos.", idProducto));
            }

            decimal costoUnitario = vinculo.IdProductoNavigation.PrecioNeto ?? 0M;

            return new ProveedorProductoInfo
            {
                IdProveedor = vinculo.IdProveedor,
                PrecioNeto = costoUnitario
            };
        }
    }
}