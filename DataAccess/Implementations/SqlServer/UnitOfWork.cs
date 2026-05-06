using DataAccess.Contexts;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementations.SqlServer
{
    /// <summary>
    /// Implementación concreta del patrón Unit of Work utilizando Entity Framework para gestionar el contexto de base de datos en SQL Server y garantizar la atomicidad transaccional.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PetShopDbContext _context;

        /// <summary>Repositorio para la gestión de productos.</summary>
        public IProductoRepository Productos { get; }

        /// <summary>Repositorio para la gestión de proveedores.</summary>
        public IProveedorRepository Proveedores { get; }

        /// <summary>Repositorio para la gestión de sucursales.</summary>
        public ISucursalRepository Sucursales { get; }

        /// <summary>Repositorio para la gestión de clientes.</summary>
        public IClienteRepository Clientes { get; }

        /// <summary>Repositorio para las cabeceras de ventas.</summary>
        public IVentaRepository Ventas { get; }

        /// <summary>Repositorio para los detalles de las ventas.</summary>
        public IVentaDetalleRepository VentaDetalles { get; }

        /// <summary>Repositorio para la tabla intermedia de vínculos entre proveedores y productos.</summary>
        public IProveedorProductoRepository ProveedorProductos { get; }

        /// <summary>Repositorio para las cabeceras de las solicitudes de pedido interno.</summary>
        public ISolicitudDePedidoRepository SolicitudDePedidos { get; }

        /// <summary>Repositorio para los renglones de detalle de las solicitudes de pedido.</summary>
        public ISolicitudDePedidoDetalleRepository SolicitudDePedidoDetalles { get; }

        /// <summary>Repositorio para las cabeceras de las órdenes de pedido.</summary>
        public IOrdenDePedidoRepository OrdenDePedidos { get; }

        /// <summary>Repositorio para los renglones de detalle de las órdenes de pedido.</summary>
        public IOrdenDePedidoDetalleRepository OrdenDePedidoDetalles { get; }

        /// <summary>Repositorio para las cabeceras de las órdenes de compra externas.</summary>
        public IOrdenDeCompraRepository OrdenDeCompras { get; }

        /// <summary>Repositorio para los renglones de detalle de las órdenes de compra.</summary>
        public IOrdenDeCompraDetalleRepository OrdenDeCompraDetalles { get; }

        /// <summary>Repositorio para las cabeceras de las solicitudes de traspaso de inventario.</summary>
        public ISolicitudDeTraspasoRepository SolicitudesTraspaso { get; }

        /// <summary>Repositorio para los renglones de detalle de los traspasos de inventario.</summary>
        public ISolicitudDeTraspasoDetalleRepository SolicitudesTraspasoDetalles { get; }

        /// <summary>Repositorio para el control de inventario y semáforos por sucursal.</summary>
        public IStockPorSucursalRepository Stocks { get; }

        /// <summary>Repositorio para auditar el historial de ingresos de mercadería al sistema.</summary>
        public IHistorialIngresoStockRepository HistorialIngresos { get; }

        /// <summary>
        /// Inicializa una nueva instancia del contexto de base de datos y de todos los repositorios asociados a esta unidad de trabajo.
        /// </summary>
        public UnitOfWork()
        {
            _context = new PetShopDbContext();

            Productos = new ProductoRepository(_context);
            Proveedores = new ProveedorRepository(_context);
            Sucursales = new SucursalRepository(_context);
            Clientes = new ClienteRepository(_context);
            Stocks = new StockPorSucursalRepository(_context);
            Ventas = new VentaRepository(_context);
            VentaDetalles = new VentaDetalleRepository(_context);
            ProveedorProductos = new ProveedorProductoRepository(_context);
            SolicitudDePedidos = new SolicitudDePedidoRepository(_context);
            SolicitudDePedidoDetalles = new SolicitudDePedidoDetalleRepository(_context);
            OrdenDePedidos = new OrdenDePedidoRepository(_context);
            OrdenDePedidoDetalles = new OrdenDePedidoDetalleRepository(_context);
            OrdenDeCompras = new OrdenDeCompraRepository(_context);
            OrdenDeCompraDetalles = new OrdenDeCompraDetalleRepository(_context);
            SolicitudesTraspaso = new SolicitudDeTraspasoRepository(_context);
            SolicitudesTraspasoDetalles = new SolicitudDeTraspasoDetalleRepository(_context);
            HistorialIngresos = new HistorialIngresoStockRepository(_context);
        }

        /// <summary>
        /// Confirma y persiste todos los cambios realizados en los repositorios dentro de la transacción actual en la base de datos.
        /// </summary>
        /// <returns>El número de registros afectados.</returns>
        public int Complete()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// Libera los recursos del contexto de base de datos y suprime la finalización pendiente.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}