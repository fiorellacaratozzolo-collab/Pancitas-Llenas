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
    /// Implementación concreta del repositorio encargado de persistir el historial y la auditoría de los ingresos de mercadería.
    /// </summary>
    public class HistorialIngresoStockRepository : IHistorialIngresoStockRepository
    {
        private readonly PetShopDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio inyectando el contexto de base de datos.
        /// </summary>
        public HistorialIngresoStockRepository(PetShopDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Inserta un nuevo registro de auditoría correspondiente al ingreso físico de stock.
        /// </summary>
        public void Create(HistorialIngresoStock historial)
        {
            _context.HistorialIngresoStocks.Add(historial);
        }

        /// <summary>
        /// Recupera el historial global de ingresos incluyendo los datos del producto abastecido y del proveedor asociado.
        /// </summary>
        public List<HistorialIngresoStock> GetAll()
        {
            return _context.HistorialIngresoStocks
            .Include(h => h.IdProductoNavigation)
            .Include(h => h.IdProveedorNavigation)
                .ToList();
        }
    }
}
