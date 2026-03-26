using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementations.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        // Repositorios existentes (NO TOCAR para no romper nada)
        IProductoRepository Productos { get; }
        IProveedorRepository Proveedores { get; }
        ISucursalRepository Sucursales { get; }
        IClienteRepository Clientes { get; }
        IStockPorSucursalRepository Stocks { get; } 
        IVentaRepository Ventas { get; }
        IVentaDetalleRepository VentaDetalles { get; }
        IProveedorProductoRepository ProveedorProductos { get; }
        ISolicitudDePedidoRepository SolicitudDePedidos { get; }
        ISolicitudDePedidoDetalleRepository SolicitudDePedidoDetalles { get; }
        IOrdenDePedidoRepository OrdenDePedidos { get; }
        IOrdenDePedidoDetalleRepository OrdenDePedidoDetalles { get; }
        IOrdenDeCompraRepository OrdenDeCompras { get; }
        IOrdenDeCompraDetalleRepository OrdenDeCompraDetalles { get; }

        // Nuevos Repositorios para Traspaso
        ISolicitudDeTraspasoRepository SolicitudesTraspaso { get; }
        ISolicitudDeTraspasoDetalleRepository SolicitudesTraspasoDetalles { get; }
        IStockPorSucursalRepository StocksPorSucursal { get; }

        int Complete();
    }
}
