using DataAccess.Contexts;
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
    internal class OrdenDeCompraRepository : IOrdenDeCompraRepository
    {
        private readonly PetShopDbContext _context;

        public OrdenDeCompraRepository(PetShopDbContext context)
        {
            _context = context;
        }

        public Guid Create(OrdenDeCompra orden)
        {
            _context.OrdenDeCompras.Add(orden);
            return orden.IdOrdenDeCompra;
        }

        public List<OrdenDeCompra> GetAll()
        {
            return _context.OrdenDeCompras 
                .Include(oc => oc.IdProveedorNavigation)
                .ToList();
        }

        public OrdenDeCompra? GetById(Guid id)
        {
            return _context.OrdenDeCompras
                .Include(oc => oc.IdProveedorNavigation)
                .Include(oc => oc.OrdenDeCompraDetalles)
                .ThenInclude(detalle => detalle.IdProductoNavigation)
                .FirstOrDefault(oc => oc.IdOrdenDeCompra == id);
        }

        public void Update(OrdenDeCompra orden)
        {
            _context.OrdenDeCompras.Attach(orden);
            _context.Entry(orden).State = EntityState.Modified;
        }
    }
}
