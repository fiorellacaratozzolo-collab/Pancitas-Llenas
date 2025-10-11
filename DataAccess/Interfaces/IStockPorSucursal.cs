using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define los métodos de acceso a datos para la gestión del stock a nivel de sucursal.
    /// </summary>
    public interface IStockPorSucursalRepository
    {
        // Obtener stock único para un producto/sucursal
        StockPorSucursal? GetBySucursalAndProducto(Guid idSucursal, Guid idProducto);

        // Operaciones básicas de stock
        Guid Create(StockPorSucursal stock);
        void Update(StockPorSucursal stock);
        List<StockPorSucursal> GetAll();
        List<StockPorSucursal> GetBySucursal(Guid idSucursal);
    }
}
