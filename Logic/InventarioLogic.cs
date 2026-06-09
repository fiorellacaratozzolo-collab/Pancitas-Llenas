using AutoMapper;
using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Models;
using Logic.MappingProfiles;
using ModelsDTO;

namespace Logic
{
    /// <summary>
    /// Centraliza las operaciones y reglas de negocio vinculadas al control de inventario, semáforos de disponibilidad y reposición de stock.
    /// </summary>
    public class InventarioLogic
    {
        private const double LIMITE_AMARILLO_PCT = 0.50;
        private const double LIMITE_ROJO_PCT = 0.20;
        private const int ESTADO_VERDE = 1;
        private const int ESTADO_AMARILLO = 2;
        private const int ESTADO_ROJO = 3;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;

        /// <summary>
        /// Inicializa una nueva instancia de la lógica de inventario, permitiendo inyectar un contexto de trabajo o crear uno nuevo.
        /// </summary>
        public InventarioLogic(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        /// <summary>
        /// Calcula el estado del semáforo de disponibilidad en función de la proporción matemática entre el stock actual y el deseado.
        /// </summary>
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

        /// <summary>
        /// Incrementa el inventario de un producto en una sucursal, creando el registro inicial si no existe, y audita la entrada en el historial de ingresos.
        /// </summary>
        public void AgregarOActualizarStock(Guid idSucursal, Guid idProducto, int cantidadAAgregar, int stockDeseado = 0, Guid? idProveedor = null)
        {
            StockPorSucursal? stockRegistro = _unitOfWork.Stocks.GetBySucursalAndProducto(idSucursal, idProducto);

            if (stockRegistro == null)
            {
                stockRegistro = new StockPorSucursal
                {
                    IdStockSucursal = Guid.NewGuid(),
                    IdSucursal = idSucursal,
                    IdProducto = idProducto,
                    StockActual = cantidadAAgregar,
                    StockDeseado = stockDeseado > 0 ? stockDeseado : cantidadAAgregar * 2,
                };
                stockRegistro.IdEstadoStock = CalcularEstadoSemaforo(stockRegistro.StockActual, stockRegistro.StockDeseado);
                _unitOfWork.Stocks.Create(stockRegistro);
            }
            else
            {
                stockRegistro.StockActual += cantidadAAgregar;
                if (stockDeseado > 0)
                {
                    stockRegistro.StockDeseado = stockDeseado;
                }
                stockRegistro.IdEstadoStock = CalcularEstadoSemaforo(stockRegistro.StockActual, stockRegistro.StockDeseado);
                _unitOfWork.Stocks.Update(stockRegistro);
            }

            var nuevoIngreso = new HistorialIngresoStock
            {
                IdHistorialIngreso = Guid.NewGuid(),
                FechaIngreso = DateTime.Now,
                IdSucursal = idSucursal,
                IdProducto = idProducto,
                CantidadAgregada = cantidadAAgregar,
                IdProveedor = idProveedor
            };

            _unitOfWork.HistorialIngresos.Create(nuevoIngreso);
            _unitOfWork.Complete();
        }

        /// <summary> 
        /// Resta la cantidad vendida al stock en memoria y recalcula el estado del semáforo, sin ejecutar el commit final para preservar la atomicidad externa de la venta. 
        /// </summary>
        public void RestarStockPorVenta(Guid idSucursal, Guid idProducto, int cantidadVendida)
        {
            if (cantidadVendida <= 0)
                throw new ArgumentException("La cantidad a vender debe ser positiva.");

            StockPorSucursal? stockRegistro = _unitOfWork.Stocks.GetBySucursalAndProducto(idSucursal, idProducto);

            if (stockRegistro == null)
            {
                throw new InvalidOperationException(string.Format("Error: El producto {0} no está inventariado en la sucursal {1}.", idProducto, idSucursal));
            }

            if (stockRegistro.StockActual < cantidadVendida)
            {
                throw new InvalidOperationException(string.Format("Stock insuficiente. Solo hay {0} unidades disponibles del producto {1}.", stockRegistro.StockActual, stockRegistro.IdProductoNavigation?.NombreProducto ?? idProducto.ToString()));
            }

            stockRegistro.StockActual -= cantidadVendida;
            stockRegistro.IdEstadoStock = CalcularEstadoSemaforo(stockRegistro.StockActual, stockRegistro.StockDeseado);

            _unitOfWork.Stocks.Update(stockRegistro);
        }

        /// <summary> 
        /// Obtiene todos los registros de stock de la base de datos sin aplicar filtros de sucursal. 
        /// </summary>
        public List<StockPorSucursalDTO> ObtenerTodoElStock()
        {
            List<StockPorSucursal> stock = _unitOfWork.Stocks.GetAll();
            return _mapper.Map<List<StockPorSucursalDTO>>(stock);
        }

        /// <summary> 
        /// Extrae el stock inventariado correspondiente a una sucursal en específico. 
        /// </summary>
        public List<StockPorSucursalDTO> ObtenerStockPorSucursal(Guid idSucursal)
        {
            var stock = _unitOfWork.Stocks.GetBySucursal(idSucursal);
            return _mapper.Map<List<StockPorSucursalDTO>>(stock);
        }

        /// <summary> 
        /// Consulta directamente el identificador del estado de abastecimiento (semáforo) actual para una combinación específica de producto y sucursal. 
        /// </summary>
        public int ObtenerEstadoSemaforo(Guid idSucursal, Guid idProducto)
        {
            StockPorSucursal? stockRegistro = _unitOfWork.Stocks.GetBySucursalAndProducto(idSucursal, idProducto);

            return stockRegistro?.IdEstadoStock ?? ESTADO_ROJO;
        }

        /// <summary>
        /// Recupera y mapea el historial cronológico de ingresos de inventario registrados para una sucursal dada.
        /// </summary>
        public List<HistorialEntregaDTO> ObtenerHistorialEntregas(Guid idSucursal)
        {
            var ingresosBd = _unitOfWork.HistorialIngresos.GetAll()
                .Where(i => i.IdSucursal == idSucursal)
                .OrderByDescending(i => i.FechaIngreso)
                .ToList();

            var listaHistorial = new List<HistorialEntregaDTO>();

            foreach (var ingreso in ingresosBd)
            {
                listaHistorial.Add(new HistorialEntregaDTO
                {
                    Fecha = ingreso.FechaIngreso,
                    Producto = ingreso.IdProductoNavigation?.NombreProducto ?? "Desconocido",
                    Cantidad = ingreso.CantidadAgregada,
                    NombreProveedor = ingreso.IdProveedorNavigation?.NombreProveedor ?? "Ingreso Manual / Sin Proveedor",
                    Marca = ingreso.IdProductoNavigation?.Marca,
                    PesoUnitario = ingreso.IdProductoNavigation?.PesoNeto ?? 0
                });
            }

            return listaHistorial;
        }
    }
}