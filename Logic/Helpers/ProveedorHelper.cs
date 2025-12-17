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
    public class ProveedorHelper
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProveedorHelper(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        /// <summary>
        /// Obtiene el proveedor asociado al producto y su precio de costo.
        /// Lanza KeyNotFoundException si no encuentra el vínculo.
        /// </summary>
        public ProveedorProductoInfo ObtenerProveedorInfoParaProducto(Guid idProducto)
        {
            // Busca el vínculo ProveedorProducto e incluye la entidad Producto.
            // Nota: Se asume que IdProductoNavigation es la propiedad de navegación al Producto.
            var vinculo = _unitOfWork.ProveedorProductos.GetAll()
                .AsQueryable()
                .Include(pp => pp.IdProductoNavigation)
                .FirstOrDefault(pp => pp.IdProducto == idProducto);

            // Usamos KeyNotFoundException, que tu Servicio
            // captura y maneja para dar feedback claro al usuario.
            if (vinculo == null || vinculo.IdProductoNavigation == null)
            {
                // Este mensaje de error le dirá al usuario qué ID de producto falta en la tabla ProveedorProductos
                throw new KeyNotFoundException($"No se pudo generar la OC: El Producto con ID '{idProducto}' no tiene un proveedor asignado o los datos de producto están incompletos.");
            }

            // Asume que la entidad Producto tiene la propiedad PrecioNeto (decimal?)
            // Si PrecioNeto es nulo, se asigna 0M (cero decimal)
            decimal costoUnitario = vinculo.IdProductoNavigation.PrecioNeto ?? 0M;         

            return new ProveedorProductoInfo
            {
                IdProveedor = vinculo.IdProveedor,
                PrecioNeto = costoUnitario
            };
        }

    }
}
