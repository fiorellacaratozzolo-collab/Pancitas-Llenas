using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    namespace Logic
    {
        public class InventarioLogic
        {

            // Constantes para la regla del Semáforo
            private const double LIMITE_AMARILLO_PCT = 0.50; // 50% o menos del deseado es Amarillo
            private const double LIMITE_ROJO_PCT = 0.20;     // 20% o menos del deseado es Rojo
            private const int ESTADO_VERDE = 1;
            private const int ESTADO_AMARILLO = 2;
            private const int ESTADO_ROJO = 3;

            // --- MÉTODO AUXILIAR PARA EL SEMÁFORO ---

            private int CalcularEstadoSemaforo(int stockActual, int stockDeseado)
            {
                if (stockDeseado <= 0)
                {
                    return ESTADO_VERDE;
                }

                double proporcion = (double)stockActual / stockDeseado;

                if (proporcion >= LIMITE_AMARILLO_PCT)
                {
                    return ESTADO_VERDE;
                }
                else if (proporcion > LIMITE_ROJO_PCT)
                {
                    return ESTADO_AMARILLO;
                }
                else
                {
                    return ESTADO_ROJO;
                }
            }

            // --- LÓGICA DE ESCRITURA (Transaccional) ---

            public void AgregarOActualizarStock(Guid idSucursal, Guid idProducto, int cantidadAAgregar, int stockDeseado = 0)
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    // 1. Buscar el registro de stock
                    StockPorSucursal? stockRegistro =
                        unitOfWork.Stocks.GetBySucursalAndProducto(idSucursal, idProducto);

                    // (Usar las constantes ESTADO_VERDE, ESTADO_AMARILLO, ESTADO_ROJO definidas previamente)
                    // const int ESTADO_VERDE = 1; // Esta redefinición se elimina o se pone fuera del método

                    if (stockRegistro == null)
                    {
                        // 2. Si NO existe, crearlo
                        stockRegistro = new StockPorSucursal
                        {
                            IdStockSucursal = Guid.NewGuid(),
                            IdSucursal = idSucursal,
                            IdProducto = idProducto,
                            StockActual = cantidadAAgregar,
                            StockDeseado = stockDeseado > 0 ? stockDeseado : cantidadAAgregar * 2,
                            IdEstadoStock = ESTADO_VERDE
                        };
                        stockRegistro.IdEstadoStock = CalcularEstadoSemaforo(stockRegistro.StockActual, stockRegistro.StockDeseado);
                        unitOfWork.Stocks.Create(stockRegistro);
                    }
                    else
                    {
                        // 3. Si SÍ existe, actualizar
                        stockRegistro.StockActual += cantidadAAgregar;
                        if (stockDeseado > 0)
                        {
                            stockRegistro.StockDeseado = stockDeseado;
                        }
                        stockRegistro.IdEstadoStock = CalcularEstadoSemaforo(stockRegistro.StockActual, stockRegistro.StockDeseado);
                        unitOfWork.Stocks.Update(stockRegistro);
                    }

                    // 4. Confirmar la Transacción
                    unitOfWork.Complete();
                }
            }

            /// <summary> Resta la cantidad vendida al stock y recalcula el estado del semáforo. </summary>
            public void RestarStockPorVenta(Guid idSucursal, Guid idProducto, int cantidadVendida)
            {
                if (cantidadVendida <= 0)
                    throw new ArgumentException("La cantidad a vender debe ser positiva.");

                using (var unitOfWork = new UnitOfWork())
                {
                    // 1. Buscar el registro de stock por la clave única
                    StockPorSucursal? stockRegistro =
                        unitOfWork.Stocks.GetBySucursalAndProducto(idSucursal, idProducto);

                    if (stockRegistro == null)
                    {
                        throw new InvalidOperationException($"Error: El producto {idProducto} no está inventariado en la sucursal {idSucursal}.");
                    }

                    // 2. VALIDACIÓN DE STOCK CRÍTICA
                    if (stockRegistro.StockActual < cantidadVendida)
                    {
                        //(StockInsuficienteException)
                        throw new InvalidOperationException($"Stock insuficiente. Solo hay {stockRegistro.StockActual} unidades disponibles del producto {stockRegistro.IdProductoNavigation?.NombreProducto ?? idProducto.ToString()}.");
                    }

                    // 3. Aplicar el descuento y recalcular estado
                    stockRegistro.StockActual -= cantidadVendida;
                    stockRegistro.IdEstadoStock = CalcularEstadoSemaforo(stockRegistro.StockActual, stockRegistro.StockDeseado);

                    // 4. Persistir los cambios
                    unitOfWork.Stocks.Update(stockRegistro);
                    unitOfWork.Complete();
                }
            }

            // --- MÉTODOS DE LECTURA (Consultas) ---

            /// <summary> Obtiene todos los stocks por sucursal. </summary>
            public List<StockPorSucursal> ObtenerTodoElStock()
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    return unitOfWork.Stocks.GetAll();
                }
            }

            /// <summary> Obtiene el stock de una sucursal específica. </summary>
            public List<StockPorSucursal> ObtenerStockPorSucursal(Guid idSucursal)
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    return unitOfWork.Stocks.GetBySucursal(idSucursal);
                }
            }

            /// <summary> Obtiene el estado del semáforo para un producto/sucursal. </summary>
            public int ObtenerEstadoSemaforo(Guid idSucursal, Guid idProducto)
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    StockPorSucursal? stockRegistro =
                        unitOfWork.Stocks.GetBySucursalAndProducto(idSucursal, idProducto);

                    return stockRegistro?.IdEstadoStock ?? ESTADO_ROJO;
                }
            }
        }
    }
}
