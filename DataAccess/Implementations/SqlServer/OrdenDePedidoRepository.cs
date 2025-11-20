using DataAccess.Contexts;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Implementations.SqlServer
{

    public class OrdenDePedidoRepository : IOrdenDePedidoRepository
    {
        private readonly PetShopDbContext _context;

        public OrdenDePedidoRepository(PetShopDbContext context)
        {
            _context = context;
        }

        public Guid Create(OrdenDePedido orden)
        {
            _context.OrdenDePedidos.Add(orden);
            return orden.IdOrdenDePedido;
        }

        public List<OrdenDePedido> GetAll()
        {
            return _context.OrdenDePedidos
            .Include(op => op.OrdenDePedidoDetalles)
            .Include(op => op.IdEstadoOpNavigation)
            .ToList();
        }

        public OrdenDePedido? GetById(Guid id)
        {
            return _context.OrdenDePedidos
            .Include(op => op.OrdenDePedidoDetalles)
            .Include(op => op.IdEstadoOpNavigation)
            .FirstOrDefault(op => op.IdOrdenDePedido == id);
        }

        public void Update(OrdenDePedido orden)
        {
            _context.OrdenDePedidos.Attach(orden);
            _context.Entry(orden).State = EntityState.Modified;
        }
    }
}
