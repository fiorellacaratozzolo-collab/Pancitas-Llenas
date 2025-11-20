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
            .Include(op => op.OrdenDeCompraDetalles)
            .Include(op => op.IdEstadoOcNavigation)
            .ToList();
        }

        public OrdenDeCompra? GetById(Guid id)
        {
            return _context.OrdenDeCompras
            .Include(op => op.OrdenDeCompraDetalles)
            .Include(op => op.IdEstadoOcNavigation)
            .FirstOrDefault(op => op.IdOrdenDeCompra == id);
        }

        public void Update(OrdenDeCompra orden)
        {
            _context.OrdenDeCompras.Attach(orden);
            _context.Entry(orden).State = EntityState.Modified;
        }
    }
}
