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

        // Propiedades de Repositorios
        public IProductoRepository Productos { get; private set; }
        public IProveedorRepository Proveedores { get; private set; }
        public ISucursalRepository Sucursales { get; private set; }
        public IClienteRepository Clientes { get; private set; }
        public IStockPorSucursalRepository Stocks { get; private set; }
        public IVentaRepository Ventas { get; }
        public IVentaDetalleRepository VentaDetalles { get; }
        public IProveedorProductoRepository ProveedorProductos { get; private set; }
        public ISolicitudDePedidoRepository SolicitudDePedidos { get; }
        public ISolicitudDePedidoDetalleRepository SolicitudDePedidoDetalles { get; }
        public UnitOfWork()
        {
            // La magia del UoW: SOLO UNA INSTANCIA del Contexto
            _context = new PetShopDbContext();

            // Inicializar Repositorios y pasarles la instancia del Contexto
            // NOTA: Debes modificar los Repositorios para que acepten el DbContext en su constructor.
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
        }

        public int Complete()
        {
            // Guardar todos los cambios acumulados en una sola operación
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
