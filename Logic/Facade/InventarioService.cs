using DataAccess.Models;
using Logic.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Facade
{
    public class InventarioService
    {
        private readonly InventarioLogic _inventarioLogic;

        public InventarioService()
        {
            // La fachada crea la instancia de la lógica
            _inventarioLogic = new InventarioLogic();
        }

        /// <summary>
        /// Descuenta stock de una sucursal.
        /// </summary>
        public void DescontarStockPorVenta(Guid idSucursal, Guid idProducto, int cantidadVendida)
        {
            _inventarioLogic.RestarStockPorVenta(idSucursal, idProducto, cantidadVendida);
        }

        public List<StockPorSucursal> ObtenerTodoElStock()
        {
            return _inventarioLogic.ObtenerTodoElStock();
        }

        public List<StockPorSucursal> ObtenerStockPorSucursal(Guid idSucursal)
        {
            return _inventarioLogic.ObtenerStockPorSucursal(idSucursal);
        }

        public int ObtenerEstadoSemaforo(Guid idSucursal, Guid idProducto)
        {
            return _inventarioLogic.ObtenerEstadoSemaforo(idSucursal, idProducto);
        }

        public void AgregarStock(Guid idSucursal, Guid idProducto, int cantidadAAgregar, int stockDeseado = 0)
        {
            _inventarioLogic.AgregarOActualizarStock(idSucursal, idProducto, cantidadAAgregar, stockDeseado);
        }
    }
}
