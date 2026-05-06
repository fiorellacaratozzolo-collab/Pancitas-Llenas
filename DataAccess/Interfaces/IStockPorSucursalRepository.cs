using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define los métodos de acceso a datos para la gestión del stock, semáforos y disponibilidad de productos a nivel de sucursal.
    /// </summary>
    public interface IStockPorSucursalRepository
    {
        /// <summary>Obtiene el registro de inventario único para una combinación de sucursal y producto, tolerando nulos si no existe.</summary>
        StockPorSucursal? GetBySucursalAndProducto(Guid idSucursal, Guid idProducto);

        /// <summary>Inserta un nuevo registro de inventario en una sucursal.</summary>
        Guid Create(StockPorSucursal stock);

        /// <summary>Actualiza las cantidades o estados de un registro de inventario existente.</summary>
        void Update(StockPorSucursal stock);

        /// <summary>Obtiene la totalidad de registros de inventario de todas las sucursales.</summary>
        List<StockPorSucursal> GetAll();

        /// <summary>Filtra y recupera el inventario completo correspondiente a una única sucursal.</summary>
        List<StockPorSucursal> GetBySucursal(Guid idSucursal);

        /// <summary>Obtiene el registro de inventario para un producto y sucursal de forma estricta.</summary>
        StockPorSucursal GetByProductoYSucursal(Guid idProducto, Guid idSucursal);
    }
}