using AutoMapper;
using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Models;
using Logic.MappingProfiles;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class InventarioLogic
    {
        // Constantes para la regla del Semáforo
        private const double LIMITE_AMARILLO_PCT = 0.50;
        private const double LIMITE_ROJO_PCT = 0.20;
        private const int ESTADO_VERDE = 1;
        private const int ESTADO_AMARILLO = 2;
        private const int ESTADO_ROJO = 3;

        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;


        public InventarioLogic()
        {
            _unitOfWork = new UnitOfWork();
        }

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

        public void AgregarOActualizarStock(Guid idSucursal, Guid idProducto, int cantidadAAgregar, int stockDeseado = 0)
        {

            // 1. Buscar el registro de stock usando la instancia inyectada
            StockPorSucursal? stockRegistro =
                _unitOfWork.Stocks.GetBySucursalAndProducto(idSucursal, idProducto);

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
                    // El estado se calcula al final de la inicialización
                };
                stockRegistro.IdEstadoStock = CalcularEstadoSemaforo(stockRegistro.StockActual, stockRegistro.StockDeseado);
                _unitOfWork.Stocks.Create(stockRegistro);
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
                _unitOfWork.Stocks.Update(stockRegistro);
            }

            // 4. Confirmar la Transacción
            _unitOfWork.Complete();
        }

        /// <summary> Resta la cantidad vendida al stock y recalcula el estado del semáforo. </summary>
        public void RestarStockPorVenta(Guid idSucursal, Guid idProducto, int cantidadVendida)
        {
            if (cantidadVendida <= 0)
                throw new ArgumentException("La cantidad a vender debe ser positiva.");

            // 1. Buscar el registro de stock por la clave única
            StockPorSucursal? stockRegistro =
                _unitOfWork.Stocks.GetBySucursalAndProducto(idSucursal, idProducto);

            if (stockRegistro == null)
            {
                throw new InvalidOperationException($"Error: El producto {idProducto} no está inventariado en la sucursal {idSucursal}.");
            }

            // 2. VALIDACIÓN DE STOCK CRÍTICA
            if (stockRegistro.StockActual < cantidadVendida)
            {
                throw new InvalidOperationException($"Stock insuficiente. Solo hay {stockRegistro.StockActual} unidades disponibles del producto {stockRegistro.IdProductoNavigation?.NombreProducto ?? idProducto.ToString()}.");
            }

            // 3. Aplicar el descuento y recalcular estado
            stockRegistro.StockActual -= cantidadVendida;
            stockRegistro.IdEstadoStock = CalcularEstadoSemaforo(stockRegistro.StockActual, stockRegistro.StockDeseado);

            // 4. Persistir los cambios
            _unitOfWork.Stocks.Update(stockRegistro);
            //_unitOfWork.Complete(); El método NO debe llamar a Complete() para permitir la atomicidad en VentaLogic.
        }

        /// <summary> Obtiene todos los stocks por sucursal y los mapea a DTO. </summary>
        public List<StockPorSucursalDTO> ObtenerTodoElStock()
        {
            List<StockPorSucursal> stock = _unitOfWork.Stocks.GetAll();
            return _mapper.Map<List<StockPorSucursalDTO>>(stock);
        }

        /// <summary> Obtiene el stock de una sucursal específica y lo mapea a DTO. </summary>
        public List<StockPorSucursalDTO> ObtenerStockPorSucursal(Guid idSucursal)
        {
            List<StockPorSucursal> stock = _unitOfWork.Stocks.GetBySucursal(idSucursal);
            return _mapper.Map<List<StockPorSucursalDTO>>(stock);
        }

        /// <summary> Obtiene el estado del semáforo para un producto/sucursal. </summary>
        public int ObtenerEstadoSemaforo(Guid idSucursal, Guid idProducto)
        {
            StockPorSucursal? stockRegistro =
                _unitOfWork.Stocks.GetBySucursalAndProducto(idSucursal, idProducto);

            // Este método puede devolver el IdEstadoStock directamente sin mapeo si es solo un entero.
            return stockRegistro?.IdEstadoStock ?? ESTADO_ROJO;
        }
    }
}