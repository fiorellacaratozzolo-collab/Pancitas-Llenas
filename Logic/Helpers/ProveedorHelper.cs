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
        /// Asume que la entidad Producto tiene la propiedad PrecioNeto (decimal?).
        /// </summary>
        public ProveedorProductoInfo ObtenerProveedorInfoParaProducto(Guid idProducto)
        {
            // Busca el vínculo ProveedorProducto e incluye la entidad Producto.
            var vinculo = _unitOfWork.ProveedorProductos.GetAll()
                .AsQueryable()
                .Include(pp => pp.IdProductoNavigation)
                .FirstOrDefault(pp => pp.IdProducto == idProducto);

            if (vinculo == null || vinculo.IdProductoNavigation == null)
            {
                throw new InvalidOperationException($"El producto con ID {idProducto} no tiene vínculo de proveedor válido o el producto no existe.");
            }

            // Asume que la entidad Producto tiene la propiedad PrecioNeto (decimal?)
            decimal costoUnitario = vinculo.IdProductoNavigation.PrecioNeto ?? 0M;

            return new ProveedorProductoInfo
            {
                IdProveedor = vinculo.IdProveedor,
                PrecioNeto = costoUnitario
            };
        }

        // --- MÉTODO ADICIONAL PARA LA INFERENCIA EN LA LECTURA ---

        /// <summary>
        /// Obtiene el IdProveedor asociado al IdProducto de un detalle de OC.
        /// </summary>
        public Guid InferirProveedorDesdeProducto(Guid idProducto)
        {
            var vinculo = _unitOfWork.ProveedorProductos.GetAll()
                .FirstOrDefault(pp => pp.IdProducto == idProducto);

            if (vinculo == null)
            {
                throw new KeyNotFoundException($"No se pudo inferir el proveedor para el producto con ID {idProducto}.");
            }
            return vinculo.IdProveedor;
        }
    }
}

