using DataAccess.EntityFramework;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementations.SqlServer
{
    public class ProveedorProductoRepository : IProveedorProductoRepository
    {
        private readonly PetShopDBContext _context;

        public ProveedorProductoRepository(PetShopDBContext context)
        {
            _context = context;
        }

        public List<ProveedorProducto> GetAll()
        {
            // 1. Obtiene todos los registros de la tabla intermedia
            // 2. Utiliza .Include() para cargar los objetos Producto y Proveedor relacionados
            return _context.ProveedorProductos
                           .Include(pp => pp.IdProductoNavigation)
                           .Include(pp => pp.IdProveedorNavigation)
                           .AsNoTracking()
                           .ToList();
        }

        public Guid Create(ProveedorProducto proveedorProducto)
        {
            _context.ProveedorProductos.Add(proveedorProducto);
            return proveedorProducto.IdProveedor;
        }
    }
}
