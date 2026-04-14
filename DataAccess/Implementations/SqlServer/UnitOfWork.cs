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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PetShopDbContext _context;

        // Propiedades de Repositorios - Agrupadas para mejor lectura
        public IProductoRepository Productos { get; }
        public IProveedorRepository Proveedores { get; }
        public ISucursalRepository Sucursales { get; }
        public IClienteRepository Clientes { get; }
        public IStockPorSucursalRepository StocksPorSucursal { get; } // Centralizado
        public IVentaRepository Ventas { get; }
        public IVentaDetalleRepository VentaDetalles { get; }
        public IProveedorProductoRepository ProveedorProductos { get; }

        // Flujo de Pedidos y Compras
        public ISolicitudDePedidoRepository SolicitudDePedidos { get; }
        public ISolicitudDePedidoDetalleRepository SolicitudDePedidoDetalles { get; }
        public IOrdenDePedidoRepository OrdenDePedidos { get; }
        public IOrdenDePedidoDetalleRepository OrdenDePedidoDetalles { get; }
        public IOrdenDeCompraRepository OrdenDeCompras { get; }
        public IOrdenDeCompraDetalleRepository OrdenDeCompraDetalles { get; }

        // Flujo de Traspasos (STP)
        public ISolicitudDeTraspasoRepository SolicitudesTraspaso { get; }
        public ISolicitudDeTraspasoDetalleRepository SolicitudesTraspasoDetalles { get; }
        public IStockPorSucursalRepository Stocks { get; }

        public UnitOfWork()
        {
            _context = new PetShopDbContext();

            // Inicialización coordinada
            Productos = new ProductoRepository(_context);
            Proveedores = new ProveedorRepository(_context);
            Sucursales = new SucursalRepository(_context);
            Clientes = new ClienteRepository(_context);
            StocksPorSucursal = new StockPorSucursalRepository(_context);
            Ventas = new VentaRepository(_context);
            VentaDetalles = new VentaDetalleRepository(_context);
            ProveedorProductos = new ProveedorProductoRepository(_context);
            SolicitudDePedidos = new SolicitudDePedidoRepository(_context);
            SolicitudDePedidoDetalles = new SolicitudDePedidoDetalleRepository(_context);
            OrdenDePedidos = new OrdenDePedidoRepository(_context);
            OrdenDePedidoDetalles = new OrdenDePedidoDetalleRepository(_context);
            OrdenDeCompras = new OrdenDeCompraRepository(_context);
            OrdenDeCompraDetalles = new OrdenDeCompraDetalleRepository(_context);
            StocksPorSucursal = new StockPorSucursalRepository(_context);
            SolicitudesTraspaso = new SolicitudDeTraspasoRepository(_context);
            SolicitudesTraspasoDetalles = new SolicitudDeTraspasoDetalleRepository(_context);
            var stockRepo = new StockPorSucursalRepository(_context);
            this.Stocks = stockRepo;
            this.StocksPorSucursal = stockRepo;
            this.SolicitudesTraspaso = new SolicitudDeTraspasoRepository(_context);
            this.SolicitudesTraspasoDetalles = new SolicitudDeTraspasoDetalleRepository(_context);
        }        

        public int Complete()
        {
            // Atómica: Guarda todo o nada
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}