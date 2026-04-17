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
    public class HistorialIngresoStockRepository : IHistorialIngresoStockRepository
    {
        private readonly PetShopDbContext _context;

        public HistorialIngresoStockRepository(PetShopDbContext context)
        {
            _context = context;
        }

        public void Create(HistorialIngresoStock historial)
        {
            _context.HistorialIngresoStocks.Add(historial);
        }

        public List<HistorialIngresoStock> GetAll()
        {
            return _context.HistorialIngresoStocks
            .Include(h => h.IdProductoNavigation)  
            .Include(h => h.IdProveedorNavigation) 
                .ToList();
        }
    }
}
