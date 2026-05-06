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
    /// <summary>
    /// Implementación concreta del repositorio encargado de gestionar el nivel de abastecimiento e inventario físico de las sucursales.
    /// </summary>
    public class StockPorSucursalRepository : IStockPorSucursalRepository
    {
        private readonly PetShopDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio con el contexto de Entity Framework provisto por el Unit of Work.
        /// </summary>
        public StockPorSucursalRepository(PetShopDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Persiste un nuevo registro de inventario para un producto en una sucursal.
        /// </summary>
        public Guid Create(StockPorSucursal stock)
        {
            if (stock == null)
                throw new ArgumentNullException(nameof(stock));

            stock.IdStockSucursal = Guid.NewGuid();
            _context.StockPorSucursals.Add(stock);
            return stock.IdStockSucursal;
        }

        /// <summary>
        /// Marca un registro de inventario existente como modificado para su posterior actualización en la base de datos.
        /// </summary>
        public void Update(StockPorSucursal stock)
        {
            if (stock == null)
                throw new ArgumentNullException(nameof(stock));

            _context.StockPorSucursals.Update(stock);
        }

        /// <summary>
        /// Obtiene el registro de stock único que cruza un producto con una sucursal dada.
        /// </summary>
        public StockPorSucursal? GetBySucursalAndProducto(Guid idSucursal, Guid idProducto)
        {
            return _context.StockPorSucursals
                           .FirstOrDefault(s => s.IdSucursal == idSucursal && s.IdProducto == idProducto);
        }

        /// <summary>
        /// Obtiene todo el inventario perteneciente a una sucursal específica incluyendo los datos relacionales del producto.
        /// </summary>
        public List<StockPorSucursal> GetBySucursal(Guid idSucursal)
        {
            return _context.StockPorSucursals
                           .Include(s => s.IdProductoNavigation)
                           .Where(s => s.IdSucursal == idSucursal)
                           .ToList();
        }

        /// <summary>
        /// Recupera todo el inventario a nivel global incluyendo las propiedades de navegación principales.
        /// </summary>
        public List<StockPorSucursal> GetAll()
        {
            return _context.StockPorSucursals
                           .Include(s => s.IdProductoNavigation)
                           .Include(s => s.IdSucursalNavigation)
                           .Include(s => s.IdEstadoStockNavigation)
                           .ToList();
        }

        /// <summary>
        /// Suma la cantidad física total disponible de un producto a lo largo de todas las sucursales registradas.
        /// </summary>
        public int GetTotalStockByProducto(Guid idProducto)
        {
            return _context.StockPorSucursals
                           .Where(s => s.IdProducto == idProducto)
                           .Sum(s => s.StockActual);
        }

        /// <summary>
        /// Método de compatibilidad funcional para obtener el stock por producto y sucursal.
        /// </summary>
        public StockPorSucursal GetByProductoYSucursal(Guid idProducto, Guid idSucursal)
        {
            return _context.StockPorSucursals
            .FirstOrDefault(s => s.IdProducto == idProducto && s.IdSucursal == idSucursal);
        }
    }
}