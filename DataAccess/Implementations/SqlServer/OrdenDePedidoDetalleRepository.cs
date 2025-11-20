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

    public class OrdenDePedidoDetalleRepository : IOrdenDePedidoDetalleRepository
    {
        private readonly PetShopDbContext _context;

        public OrdenDePedidoDetalleRepository(PetShopDbContext context)
        {
            _context = context;
        }

        public void AddRange(IEnumerable<OrdenDePedidoDetalle> detalles)
        {
            _context.OrdenDePedidoDetalles.AddRange(detalles);
        }
    }
}
