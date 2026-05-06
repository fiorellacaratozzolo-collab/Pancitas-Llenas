using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementations.UnitOfWork
{
    /// <summary>
    /// Define el contrato para el patrón Unit of Work, encapsulando el contexto de la base de datos y garantizando que múltiples operaciones de repositorios se resuelvan en una única transacción atómica.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>Repositorio para la gestión de productos.</summary>
        IProductoRepository Productos { get; }

        /// <summary>Repositorio para la gestión de proveedores.</summary>
        IProveedorRepository Proveedores { get; }

        /// <summary>Repositorio para la gestión de sucursales.</summary>
        ISucursalRepository Sucursales { get; }

        /// <summary>Repositorio para la gestión de clientes.</summary>
        IClienteRepository Clientes { get; }

        /// <summary>Repositorio para el control de inventario y semáforos por sucursal.</summary>
        IStockPorSucursalRepository Stocks { get; }

        /// <summary>Repositorio para las cabeceras de ventas.</summary>
        IVentaRepository Ventas { get; }

        /// <summary>Repositorio para los detalles de las ventas.</summary>
        IVentaDetalleRepository VentaDetalles { get; }

        /// <summary>Repositorio para la tabla intermedia de vínculos entre proveedores y productos.</summary>
        IProveedorProductoRepository ProveedorProductos { get; }

        /// <summary>Repositorio para las cabeceras de las solicitudes de pedido interno.</summary>
        ISolicitudDePedidoRepository SolicitudDePedidos { get; }

        /// <summary>Repositorio para los renglones de detalle de las solicitudes de pedido.</summary>
        ISolicitudDePedidoDetalleRepository SolicitudDePedidoDetalles { get; }

        /// <summary>Repositorio para las cabeceras de las órdenes de pedido.</summary>
        IOrdenDePedidoRepository OrdenDePedidos { get; }

        /// <summary>Repositorio para los renglones de detalle de las órdenes de pedido.</summary>
        IOrdenDePedidoDetalleRepository OrdenDePedidoDetalles { get; }

        /// <summary>Repositorio para las cabeceras de las órdenes de compra externas.</summary>
        IOrdenDeCompraRepository OrdenDeCompras { get; }

        /// <summary>Repositorio para los renglones de detalle de las órdenes de compra.</summary>
        IOrdenDeCompraDetalleRepository OrdenDeCompraDetalles { get; }

        /// <summary>Repositorio para las cabeceras de las solicitudes de traspaso de inventario.</summary>
        ISolicitudDeTraspasoRepository SolicitudesTraspaso { get; }

        /// <summary>Repositorio para los renglones de detalle de los traspasos de inventario.</summary>
        ISolicitudDeTraspasoDetalleRepository SolicitudesTraspasoDetalles { get; }

        /// <summary>Repositorio para auditar el historial de ingresos de mercadería al sistema.</summary>
        IHistorialIngresoStockRepository HistorialIngresos { get; }

        /// <summary>
        /// Confirma y guarda todos los cambios realizados en el contexto de datos de forma atómica.
        /// </summary>
        /// <returns>El número de registros afectados en la base de datos.</returns>
        int Complete();
    }
}