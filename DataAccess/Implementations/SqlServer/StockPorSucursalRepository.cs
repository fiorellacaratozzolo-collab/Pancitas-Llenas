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
    public class StockPorSucursalRepository : IStockPorSucursalRepository
    {
        private readonly PetShopDbContext _context;

        // El constructor recibe el DbContext, no lo crea.
        public StockPorSucursalRepository(PetShopDbContext context)
        {
            _context = context;
        }

        public Guid Create(StockPorSucursal stock)
        {
            if (stock == null)
                throw new ArgumentNullException(nameof(stock));

            stock.IdStockSucursal = Guid.NewGuid();
            _context.StockPorSucursals.Add(stock);
            // NO se llama a SaveChanges()
            return stock.IdStockSucursal;
        }

        public void Update(StockPorSucursal stock)
        {
            if (stock == null)
                throw new ArgumentNullException(nameof(stock));

            // Adjuntar y marcar el estado como modificado
            _context.StockPorSucursals.Update(stock);
            // NO se llama a SaveChanges()
        }

        public StockPorSucursal? GetBySucursalAndProducto(Guid idSucursal, Guid idProducto)
        {
            // Busca el stock único para esa combinación de producto y sucursal
            return _context.StockPorSucursals
                           .FirstOrDefault(s => s.IdSucursal == idSucursal && s.IdProducto == idProducto);
        }

        public List<StockPorSucursal> GetBySucursal(Guid idSucursal)
        {
            // Obtiene todos los stocks en una sucursal
            return _context.StockPorSucursals
                           .Where(s => s.IdSucursal == idSucursal)
                           .ToList();
        }

        public List<StockPorSucursal> GetAll()
        {
            return _context.StockPorSucursals
                           .Include(s => s.IdProductoNavigation) // Carga el producto
                           .Include(s => s.IdSucursalNavigation) // Carga la sucursal
                           .Include(s => s.IdEstadoStockNavigation) // Carga la descripción del estado
                           .ToList();
        }

        public int GetTotalStockByProducto(Guid idProducto)
        {
            // Calcula el stock total de un producto sumando el StockActual en todas las sucursales
            return _context.StockPorSucursals
                           .Where(s => s.IdProducto == idProducto)
                           .Sum(s => s.StockActual);
        }
    }
}