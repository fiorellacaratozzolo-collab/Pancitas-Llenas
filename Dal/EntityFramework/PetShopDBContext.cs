using Domain.Detalles;
using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Dal.EntityFramework
{
    public class PetShopDBContext : DbContext
    {

        public PetShopDBContext() : base("name=MainConString")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaDetalle> VentaDetalles { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<InventarioProducto> InventarioProductos { get; set; }
        public DbSet<InventarioSucursal> InventarioSucursales { get; set; }
        public DbSet<OrdenDeCompra> OrdenDeCompras { get; set; }
        public DbSet<OrdenDeCompraDetalle> OrdenDeCompraDetalles { get; set; }
        public DbSet<OrdenDePedido> OrdenDePedidos { get; set; }
        public DbSet<OrdenDePedidoDetalle> OrdenDePedidoDetalles { get; set; }
        public DbSet<Encargado> Encargados { get; set; }
        public DbSet<EncargadoSucursal> EncargadoSucursales { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<ProveedorProducto> ProveedorProductos { get; set; }
        public DbSet<SolicitudDePedido> SolicitudDePedidos { get; set; }
        public DbSet<SolicitudDePedidoDetalle> SolicitudDePedidoDetalles { get; set; }
        public DbSet<SolicitudDeTraspasoDeProductos> SolicitudDeTraspasoDeProductos { get; set; }
        public DbSet<SolicitudDeTraspasoDeProductosDetalle> SolicitudDeTraspasoDeProductosDetalles { get; set; }
        public DbSet<TipoClienteEnum> TipoClienteEnums { get; set; }
        public DbSet<TipoVentaEnum> TipoVentaEnums { get; set; }
        public DbSet<TipoSucursalEnum> TipoSucursalEnums { get; set; }
        public DbSet<EstadoOCEnum> EstadoOCEnums { get; set; }
        public DbSet<EstadoOPEnum> EstadoOPEnums { get; set; }
        public DbSet<EstadoSPEnum> EstadoSPEnums { get; set; }
        public DbSet<EstadoSTPEnum> EstadoSTPEnums { get; set; }
        public DbSet<EstadoStockEnum> EstadoStockEnums { get; set; }
        public DbSet<EstadoISEnum> EstadoISEnums { get; set; }

        //MAPEO DE TODAS LAS ENTIDADES
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Remover convención de nombres plurales
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Configuración de Cliente
            modelBuilder.Entity<Cliente>()
                .ToTable("Cliente")
                .HasKey(c => c.IdCliente)
                .Property(c => c.IdCliente)
                .HasColumnName("IdCliente")
                .IsRequired();

            modelBuilder.Entity<Cliente>()
                .Property(c => c.NombreCliente)
                .HasColumnName("NombreCliente")
                .HasMaxLength(50)
                .IsOptional();

            modelBuilder.Entity<Cliente>()
                .Property(c => c.DNI)
                .HasColumnName("DNI")
                .IsOptional();

            modelBuilder.Entity<Cliente>()
                .Property(c => c.IdTipoCliente)
                .HasColumnName("IdTipoCliente")
                .IsRequired();

            // Relación con TipoClienteEnum
            modelBuilder.Entity<Cliente>()
                .HasRequired(c => c.TipoCliente)
                .WithMany()
                .HasForeignKey(c => c.IdTipoCliente);

            // Configuración de TipoClienteEnum
            modelBuilder.Entity<TipoClienteEnum>()
                .ToTable("TipoClienteEnum")
                .HasKey(tc => tc.IdTipoCliente)
                .Property(tc => tc.IdTipoCliente)
                .HasColumnName("IdTipoCliente")
                .IsRequired();

            modelBuilder.Entity<TipoClienteEnum>()
                .Property(tc => tc.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(20)
                .IsRequired();

            // Configuración de Producto
            modelBuilder.Entity<Producto>()
                .ToTable("Producto")
                .HasKey(p => p.IdProducto)
                .Property(p => p.IdProducto)
                .HasColumnName("IdProducto")
                .IsRequired();

            modelBuilder.Entity<Producto>()
                .Property(p => p.NombreProducto)
                .HasColumnName("NombreProducto")
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Producto>()
                .Property(p => p.Marca)
                .HasColumnName("Marca")
                .HasMaxLength(50)
                .IsOptional();

            modelBuilder.Entity<Producto>()
                .Property(p => p.PesoNeto)
                .HasColumnName("PesoNeto")
                .HasPrecision(10, 2)
                .IsOptional();

            modelBuilder.Entity<Producto>()
                .Property(p => p.Unidad)
                .HasColumnName("Unidad")
                .HasMaxLength(20)
                .IsOptional();

            modelBuilder.Entity<Producto>()
                .Property(p => p.PrecioNeto)
                .HasColumnName("PrecioNeto")
                .HasPrecision(10, 2)
                .IsOptional();

            modelBuilder.Entity<Producto>()
                .Property(p => p.Descripcion)
                .HasColumnName("Descripcion")
                .IsOptional();

            // Configuración de Inventario
            modelBuilder.Entity<Inventario>()
                .ToTable("Inventario")
                .HasKey(i => i.IdInventario)
                .Property(i => i.IdInventario)
                .HasColumnName("IdInventario")
                .IsRequired();

            modelBuilder.Entity<Inventario>()
                .Property(i => i.IdEstadoStock)
                .HasColumnName("IdEstadoStock")
                .IsRequired();

            // Relación con EstadoStockEnum
            modelBuilder.Entity<Inventario>()
                .HasRequired(i => i.EstadoStock)
                .WithMany()
                .HasForeignKey(i => i.IdEstadoStock);

            // Configuración de EstadoStockEnum
            modelBuilder.Entity<EstadoStockEnum>()
                .ToTable("EstadoStockEnum")
                .HasKey(es => es.IdEstadoStock)
                .Property(es => es.IdEstadoStock)
                .HasColumnName("IdEstadoStock")
                .IsRequired();

            modelBuilder.Entity<EstadoStockEnum>()
                .Property(es => es.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(10)
                .IsRequired();

            // Configuración de InventarioProducto
            modelBuilder.Entity<InventarioProducto>()
                .ToTable("InventarioProducto")
                .HasKey(ip => ip.IdInventarioProducto)
                .Property(ip => ip.IdInventarioProducto)
                .HasColumnName("IdInventarioProducto")
                .IsRequired();

            modelBuilder.Entity<InventarioProducto>()
                .Property(ip => ip.IdInventario)
                .HasColumnName("IdInventario")
                .IsRequired();

            modelBuilder.Entity<InventarioProducto>()
                .Property(ip => ip.IdProducto)
                .HasColumnName("IdProducto")
                .IsRequired();

            // Relación con Inventario
            modelBuilder.Entity<InventarioProducto>()
                .HasRequired(ip => ip.Inventario)
                .WithMany(i => i.InventarioProductos)
                .HasForeignKey(ip => ip.IdInventario)
                .WillCascadeOnDelete(false);

            // Relación con Producto
            modelBuilder.Entity<InventarioProducto>()
                .HasRequired(ip => ip.Producto)
                .WithMany(p => p.InventarioProductos)
                .HasForeignKey(ip => ip.IdProducto)
                .WillCascadeOnDelete(false);

            // Configuración de InventarioSucursal
            modelBuilder.Entity<InventarioSucursal>()
                .ToTable("InventarioSucursal")
                .HasKey(ip => ip.IdInventarioSucursal)
                .Property(ip => ip.IdInventarioSucursal)
                .HasColumnName("IdInventarioSucursal")
                .IsRequired();

            modelBuilder.Entity<InventarioSucursal>()
                .Property(ip => ip.IdInventario)
                .HasColumnName("IdInventario")
                .IsRequired();

            modelBuilder.Entity<InventarioSucursal>()
                .Property(ip => ip.IdSucursal)
                .HasColumnName("IdSucursal")
                .IsRequired();

            // Relación con Inventario
            modelBuilder.Entity<InventarioSucursal>()
                .HasRequired(ip => ip.Inventario)
                .WithMany(i => i.InventarioSucursal)
                .HasForeignKey(ip => ip.IdInventario)
                .WillCascadeOnDelete(false);

            // Relación con Sucursal
            modelBuilder.Entity<InventarioSucursal>()
                .HasRequired(ip => ip.Sucursal)
                .WithMany(p => p.InventarioSucursal)
                .HasForeignKey(ip => ip.IdSucursal)
                .WillCascadeOnDelete(false);

            // Relación con EstadoISEnum con InventarioSucursal
            modelBuilder.Entity<InventarioSucursal>()
                .HasRequired(s => s.EstadoIS)
                .WithMany()
                .HasForeignKey(s => s.IdEstadoIS);

            // Configuración de Proveedor
            modelBuilder.Entity<Proveedor>()
                .ToTable("Proveedor")
                .HasKey(p => p.IdProveedor)
                .Property(p => p.IdProveedor)
                .HasColumnName("IdProveedor")
                .IsRequired();

            modelBuilder.Entity<Proveedor>()
                .Property(p => p.NombreProveedor)
                .HasColumnName("NombreProveedor")
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Proveedor>()
                .Property(p => p.CUIT)
                .HasColumnName("CUIT")
                .IsRequired();

            modelBuilder.Entity<Proveedor>()
                .Property(p => p.Telefono)
                .HasColumnName("Telefono")
                .IsOptional();

            modelBuilder.Entity<Proveedor>()
                .Property(p => p.Direccion)
                .HasColumnName("Direccion")
                .HasMaxLength(100)
                .IsOptional();

            // Configuración de ProveedorProducto
            modelBuilder.Entity<ProveedorProducto>()
                .ToTable("ProveedorProducto")
                .HasKey(ip => ip.IdProveedorProducto)
                .Property(ip => ip.IdProveedorProducto)
                .HasColumnName("IdProveedorProducto")
                .IsRequired();

            modelBuilder.Entity<ProveedorProducto>()
                .Property(pp => pp.IdProveedorProducto)
                .HasColumnName("IdProveedorProducto")
                .IsRequired();

            //Configuración de relación entre ProveedorProducto y Proveedor
            modelBuilder.Entity<ProveedorProducto>()
                .HasRequired(pp => pp.Proveedor)
                .WithMany(p => p.ProveedorProductos)
                .HasForeignKey(pp => pp.IdProveedor)
                .WillCascadeOnDelete(false);

            //Configuración de relación entre ProveedorProducto y Producto
            modelBuilder.Entity<ProveedorProducto>()
                .HasRequired(pp => pp.Producto)
                .WithMany(p => p.ProveedorProductos)
                .HasForeignKey(pp => pp.IdProducto)
                .WillCascadeOnDelete(false);

            // Configuración de TipoVentaEnum
            modelBuilder.Entity<TipoVentaEnum>()
                .ToTable("TipoVentaEnum")
                .HasKey(tve => tve.IdTipoVenta)
                .Property(tve => tve.IdTipoVenta)
                .HasColumnName("IdTipoVenta")
                .IsRequired();

            modelBuilder.Entity<TipoVentaEnum>()
                .Property(tve => tve.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(20)
                .IsRequired();

            // Configuración de Venta
            modelBuilder.Entity<Venta>()
                .ToTable("Venta")
                .HasKey(v => v.IdVenta)
                .Property(v => v.IdVenta)
                .HasColumnName("IdVenta")
                .IsRequired();

            modelBuilder.Entity<Venta>()
                .Property(v => v.FechaVenta)
                .HasColumnName("FechaVenta")
                .IsRequired();

            modelBuilder.Entity<Venta>()
                .Property(v => v.IdTipoVenta)
                .HasColumnName("IdTipoVenta")
                .IsRequired();

            // Relación con TipoVentaEnum
            modelBuilder.Entity<Venta>()
                .HasRequired(v => v.TipoVenta)
                .WithMany()
                .HasForeignKey(v => v.IdTipoVenta);

            // Configuración de VentaDetalle
            modelBuilder.Entity<VentaDetalle>()
                .ToTable("VentaDetalle")
                .HasKey(vd => vd.IdVentaDetalle)
                .Property(vd => vd.IdVentaDetalle)
                .HasColumnName("IdVentaDetalle")
                .IsRequired();

            modelBuilder.Entity<VentaDetalle>()
                .Property(vd => vd.IdVenta)
                .HasColumnName("IdVenta")
                .IsRequired();

            modelBuilder.Entity<VentaDetalle>()
                .Property(vd => vd.Cantidad)
                .HasColumnName("Cantidad")
                .IsRequired();

            modelBuilder.Entity<VentaDetalle>()
                .Property(vd => vd.PesoNeto)
                .HasColumnName("PesoNeto")
                .IsRequired();

            modelBuilder.Entity<VentaDetalle>()
                .Property(vd => vd.Unidad)
                .HasColumnName("Unidad")
                .IsRequired();

            modelBuilder.Entity<VentaDetalle>()
                .Property(vd => vd.PrecioUnitario)
                .HasColumnName("PrecioUnitario")
                .HasPrecision(10, 2)
                .IsRequired();

            modelBuilder.Entity<VentaDetalle>()
                .Property(vd => vd.Subtotal)
                .HasColumnName("Subtotal")
                .IsRequired();

            // Relación con Venta
            modelBuilder.Entity<VentaDetalle>()
                .HasRequired(vd => vd.Venta)
                .WithMany(v => v.VentaDetalles)
                .HasForeignKey(vd => vd.IdVenta)
                .WillCascadeOnDelete(false);

            // Configuración de TipoSucursalEnum
            modelBuilder.Entity<TipoSucursalEnum>()
                .ToTable("TipoSucursalEnum")
                .HasKey(tse => tse.IdTipoSucursal)
                .Property(tse => tse.IdTipoSucursal)
                .HasColumnName("IdTipoSucursal")
                .IsRequired();

            modelBuilder.Entity<TipoSucursalEnum>()
                .Property(tse => tse.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(20)
                .IsRequired();

            // Configuración de Sucursal
            modelBuilder.Entity<Sucursal>()
                .ToTable("Sucursal")
                .HasKey(s => s.IdSucursal)
                .Property(s => s.IdSucursal)
                .HasColumnName("IdSucursal")
                .IsRequired();

            modelBuilder.Entity<Sucursal>()
                .Property(s => s.Direccion)
                .HasColumnName("Direccion")
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Sucursal>()
                .Property(s => s.NombreSucursal)
                .HasColumnName("NombreSucursal")
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Sucursal>()
                .Property(p => p.Telefono)
                .HasColumnName("Telefono")
                .IsOptional();

            modelBuilder.Entity<Sucursal>()
                .Property(s => s.IdTipoSucursal)
                .HasColumnName("IdTipoSucursal")
                .IsRequired();

            // Relación con TipoSucursalEnum
            modelBuilder.Entity<Sucursal>()
                .HasRequired(s => s.TipoSucursal)
                .WithMany()
                .HasForeignKey(s => s.IdTipoSucursal);    

            // Configuración de EstadoOCEnum
            modelBuilder.Entity<EstadoOCEnum>()
                .ToTable("EstadoOCEnum")
                .HasKey(e => e.IdEstadoOC);

            modelBuilder.Entity<EstadoOCEnum>()
                .Property(e => e.IdEstadoOC)
                .HasColumnName("IdEstadoOC")
                .IsRequired();

            modelBuilder.Entity<EstadoOCEnum>()
                .Property(e => e.Descripcion)
                .HasColumnName("Descripcion")
                .IsRequired()
                .HasMaxLength(20);

            // Relación con EstadoOCEnum con OrdenDeCompra
            modelBuilder.Entity<OrdenDeCompra>()
                .HasRequired(s => s.EstadoOC)
                .WithMany()
                .HasForeignKey(s => s.IdEstadoOC);

            // Configuración de OrdenDeCompra
            modelBuilder.Entity<OrdenDeCompra>()
                .ToTable("OrdenDeCompra")
                .HasKey(e => e.IdOrdenDeCompra);

            modelBuilder.Entity<OrdenDeCompra>()
                .Property(e => e.IdOrdenDeCompra)
                .HasColumnName("IdOrdenDeCompra");

            modelBuilder.Entity<OrdenDeCompra>()
                .Property(e => e.FechaOC)
                .HasColumnName("FechaOC")
                .IsRequired();

            modelBuilder.Entity<OrdenDeCompra>()
                .Property(e => e.IdEstadoOC)
                .HasColumnName("IdEstadoOC")
                .IsRequired();

            modelBuilder.Entity<OrdenDeCompra>()
                .Property(e => e.Total)
                .HasColumnName("Total")
                .HasColumnType("decimal(18, 0)")
                .IsRequired();

            // Configuración de OrdenDeCompraDetalle
            modelBuilder.Entity<OrdenDeCompraDetalle>()
                .ToTable("OrdenDeCompraDetalle")
                .HasKey(e => e.IdOrdenDeCompraDetalle);

            modelBuilder.Entity<OrdenDeCompraDetalle>()
                .Property(e => e.IdOrdenDeCompraDetalle)
                .HasColumnName("IdOrdenDeCompraDetalle")
                .IsRequired();

            modelBuilder.Entity<OrdenDeCompraDetalle>()
                .Property(e => e.IdOrdenDeCompra)
                .HasColumnName("IdOrdenDeCompra")
                .IsRequired();

            modelBuilder.Entity<OrdenDeCompraDetalle>()
                .Property(e => e.Cantidad)
                .HasColumnName("Cantidad")
                .IsRequired();

            modelBuilder.Entity<OrdenDeCompraDetalle>()
                .Property(e => e.PesoNeto)
                .HasColumnName("PesoNeto")
                .HasColumnType("decimal(18, 0)")
                .IsRequired();

            modelBuilder.Entity<OrdenDeCompraDetalle>()
                .Property(e => e.PrecioUnitario)
                .HasColumnName("PrecioUnitario")
                .HasColumnType("decimal(18, 0)")
                .IsRequired();

            modelBuilder.Entity<OrdenDeCompraDetalle>()
                .Property(e => e.Unidad)
                .HasColumnName("Unidad")
                .HasColumnType("varchar(20)")
                .IsRequired();

            modelBuilder.Entity<OrdenDeCompraDetalle>()
                .Property(e => e.Subtotal)
                .HasColumnName("Subtotal")
                .HasColumnType("decimal(18, 0)")
                .IsRequired();

            // Relación con OrdenDeCompra
            modelBuilder.Entity<OrdenDeCompraDetalle>()
                .HasRequired(vd => vd.OrdenDeCompra)
                .WithMany(v => v.OrdenDeCompraDetalle)
                .HasForeignKey(vd => vd.IdOrdenDeCompra)
                .WillCascadeOnDelete(false);

            // Configuración de SolicitudDePedido
            modelBuilder.Entity<SolicitudDePedido>()
                .ToTable("SolicitudDePedido")
                .HasKey(e => e.IdSolicitudDePedido);

            modelBuilder.Entity<SolicitudDePedido>()
                .Property(e => e.IdSolicitudDePedido)
                .HasColumnName("IdSolicitudDePedido")
                .IsRequired();

            modelBuilder.Entity<SolicitudDePedido>()
                .Property(e => e.FechaSP)
                .HasColumnName("FechaSP")
                .IsRequired();

            modelBuilder.Entity<SolicitudDePedido>()
                .Property(e => e.IdEstadoSP)
                .HasColumnName("IdEstadoSP")
                .IsRequired();             

            // Configuración de EstadoSPEnum
            modelBuilder.Entity<EstadoSPEnum>()
                .ToTable("EstadoSPEnum")
                .HasKey(e => e.IdEstadoSP);

            modelBuilder.Entity<EstadoSPEnum>()
                .Property(e => e.IdEstadoSP)
                .HasColumnName("IdEstadoSP")
                .IsRequired();

            modelBuilder.Entity<EstadoSPEnum>()
                .Property(e => e.Descripcion)
                .HasColumnName("Descripcion")
                .IsRequired();

            // Relación con EstadoSPEnum con SolicitudDePedido
            modelBuilder.Entity<SolicitudDePedido>()
                .HasRequired(s => s.EstadoSP)
                .WithMany()
                .HasForeignKey(s => s.IdEstadoSP);

            // Configuración de SolicitudDePedidoDetalle
            modelBuilder.Entity<SolicitudDePedidoDetalle>()
                .ToTable("SolicitudDePedidoDetalle")
                .HasKey(e => e.IdSolicitudDePedidoDetalle);

            modelBuilder.Entity<SolicitudDePedidoDetalle>()
                .Property(e => e.IdSolicitudDePedidoDetalle)
                .HasColumnName("IdSolicitudDePedidoDetalle")
                .IsRequired();

            modelBuilder.Entity<SolicitudDePedidoDetalle>()
                .Property(e => e.IdSolicitudDePedido)
                .HasColumnName("IdSolicitudDePedido")
                .IsRequired();

            modelBuilder.Entity<SolicitudDePedidoDetalle>()
                .Property(e => e.IdProducto)
                .HasColumnName("IdProducto")
                .IsRequired();

            modelBuilder.Entity<SolicitudDePedidoDetalle>()
                .Property(e => e.Cantidad)
                .HasColumnName("Cantidad")
                .IsRequired()
                .HasColumnType("int");

            modelBuilder.Entity<SolicitudDePedidoDetalle>()
                .Property(e => e.PesoNeto)
                .HasColumnName("PesoNeto")
                .IsRequired()
                .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<SolicitudDePedidoDetalle>()
                .Property(e => e.Unidad)
                .HasColumnName("Unidad")
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(20);

            // Configuración de relación entre Producto y SolicitudDePedidoDetalle
            modelBuilder.Entity<Producto>()
                .HasMany(e => e.SolicitudDePedidoDetalle) // One SolicitudDePedido has many Detalles
                .WithRequired() // Each Detalle requires a SolicitudDePedido
                .HasForeignKey(d => d.IdProducto); // Foreign key in Detalle

            // Configuración de relación entre SolicitudDePedido y SolicitudDePedidoDetalle
            modelBuilder.Entity<SolicitudDePedido>()
                .HasMany(e => e.SolicitudDePedidoDetalle) // One SolicitudDePedido has many Detalles
                .WithRequired() // Each Detalle requires a SolicitudDePedido
                .HasForeignKey(d => d.IdSolicitudDePedido); // Foreign key in Detalle

            // Configuración de OrdenDePedido
            modelBuilder.Entity<OrdenDePedido>()
                .ToTable("OrdenDePedido")
                .HasKey(e => e.IdOrdenDePedido);

            modelBuilder.Entity<OrdenDePedido>()
                .Property(e => e.IdOrdenDePedido)
                .HasColumnName("IdOrdenDePedido")
                .IsRequired();

            modelBuilder.Entity<OrdenDePedido>()
                .Property(e => e.FechaOP)
                .HasColumnName("FechaOP")
                .IsRequired();

            modelBuilder.Entity<OrdenDePedido>()
                .Property(e => e.IdEstadoOP)
                .HasColumnName("IdEstadoOP")
                .IsRequired();

            modelBuilder.Entity<OrdenDePedido>()
                .Property(e => e.Total)
                .HasColumnName("Total")
                .IsRequired()
                .HasColumnType("decimal(18, 0)");

            // Configuración de relación entre OrdenDePedido y OrdenDePedidoDetalle one-to-many
            modelBuilder.Entity<OrdenDePedido>()
                .HasMany(e => e.OrdenDePedidoDetalle) // One OrdenDePedido has many Detalles
                .WithRequired() // Each Detalle requires an OrdenDePedido
                .HasForeignKey(d => d.IdOrdenDePedido); // Foreign key in Detalle

            // Configuración de OrdenDePedidoDetalle
            modelBuilder.Entity<OrdenDePedidoDetalle>()
                .ToTable("OrdenDePedidoDetalle")
                .HasKey(d => d.IdOrdenDePedidoDetalle);

            modelBuilder.Entity<OrdenDePedidoDetalle>()
                .Property(d => d.IdOrdenDePedidoDetalle)
                .HasColumnName("IdOrdenDePedidoDetalle")
                .IsRequired();

            modelBuilder.Entity<OrdenDePedidoDetalle>()
                .Property(d => d.IdOrdenDePedido)
                .HasColumnName("IdOrdenDePedido")
                .IsRequired();

            modelBuilder.Entity<OrdenDePedidoDetalle>()
                .Property(d => d.Cantidad)
                .HasColumnName("Cantidad")
                .IsRequired();

            modelBuilder.Entity<OrdenDePedidoDetalle>()
                .Property(d => d.PesoNeto)
                .HasColumnName("PesoNeto")
                .IsRequired()
                .HasColumnType("decimal(18, 0)");

            modelBuilder.Entity<OrdenDePedidoDetalle>()
                .Property(d => d.PrecioUnitario)
                .HasColumnName("PrecioUnitario")
                .IsRequired()
                .HasColumnType("decimal(18, 0)");

            modelBuilder.Entity<OrdenDePedidoDetalle>()
                .Property(d => d.Unidad)
                .HasColumnName("Unidad")
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(20);

            modelBuilder.Entity<OrdenDePedidoDetalle>()
                .Property(d => d.Subtotal)
                .HasColumnName("Subtotal")
                .IsRequired()
                .HasColumnType("decimal(18, 0)");

            // Relación con EstadoOPEnum con OrdenDePedido
            modelBuilder.Entity<OrdenDePedido>()
                .HasRequired(s => s.EstadoOP)
                .WithMany()
                .HasForeignKey(s => s.IdEstadoOP);

            // Configuración de EstadoOPEnum
            modelBuilder.Entity<EstadoOPEnum>()
                .ToTable("EstadoOPEnum")
                .HasKey(e => e.IdEstadoOP);

            modelBuilder.Entity<EstadoOPEnum>()
                .Property(e => e.IdEstadoOP)
                .HasColumnName("IdEstadoOP")
                .IsRequired();

            modelBuilder.Entity<EstadoOPEnum>()
                .Property(e => e.Descripcion)
                .HasColumnName("Descripcion")
                .IsRequired();

            // Configure SolicitudDeTraspasoDeProductos
            modelBuilder.Entity<SolicitudDeTraspasoDeProductos>()
                .ToTable("SolicitudDeTraspasoDeProductos")
                .HasKey(e => e.IdSolicitudDeTraspasoDeProductos);

            modelBuilder.Entity<SolicitudDeTraspasoDeProductos>()
                .Property(e => e.IdSolicitudDeTraspasoDeProductos)
                .HasColumnName("IdSolicitudDeTraspasoDeProductos")
                .IsRequired();

            modelBuilder.Entity<SolicitudDeTraspasoDeProductos>()
                .Property(e => e.FechaSTP)
                .HasColumnName("FechaSTP")
                .IsRequired();

            modelBuilder.Entity<SolicitudDeTraspasoDeProductos>()
                .Property(e => e.IdEstadoSTP)
                .HasColumnName("IdEstadoSTP")
                .IsRequired()
                .HasColumnType("int");

            // Configuración de relación entre SolicitudDeTraspasoDeProducto y SolicitudDeTraspasoDeProductoDetalle 
            modelBuilder.Entity<SolicitudDeTraspasoDeProductos>()
                .HasMany(e => e.SolicitudDeTraspasoDeProductosDetalle) // One SolicitudDeTraspaso has many Detalles
                .WithRequired() // Each Detalle requires a SolicitudDeTraspaso
                .HasForeignKey(d => d.IdSolicitudDeTraspasoDeProductos); // Foreign key in Detalle

            // Configure SolicitudDeTraspasoDeProductosDetalle
            modelBuilder.Entity<SolicitudDeTraspasoDeProductosDetalle>()
                .ToTable("SolicitudDeTraspasoDeProductosDetalle")
                .HasKey(d => d.IdSolicitudDeTraspasoDeProductosDetalle);

            modelBuilder.Entity<SolicitudDeTraspasoDeProductosDetalle>()
                .Property(d => d.IdSolicitudDeTraspasoDeProductosDetalle)
                .HasColumnName("IdSolicitudDeTraspasoDeProductosDetalle")
                .IsRequired();

            modelBuilder.Entity<SolicitudDeTraspasoDeProductosDetalle>()
                .Property(d => d.IdSolicitudDeTraspasoDeProductos)
                .HasColumnName("IdSolicitudDeTraspasoDeProductos")
                .IsRequired();

            modelBuilder.Entity<SolicitudDeTraspasoDeProductosDetalle>()
                .Property(d => d.IdProducto)
                .HasColumnName("IdProducto")
                .IsRequired();

            modelBuilder.Entity<SolicitudDeTraspasoDeProductosDetalle>()
                .Property(d => d.Cantidad)
                .HasColumnName("Cantidad")
                .IsRequired();

            modelBuilder.Entity<SolicitudDeTraspasoDeProductosDetalle>()
                .Property(d => d.PesoNeto)
                .HasColumnName("PesoNeto")
                .IsRequired()
                .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<SolicitudDeTraspasoDeProductosDetalle>()
                .Property(d => d.Unidad)
                .HasColumnName("Unidad")
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(20);

            // Relación con SolicitudDeTraspasoDeProductosDetalle con Producto
            modelBuilder.Entity<SolicitudDeTraspasoDeProductosDetalle>()
                .HasRequired(vd => vd.Producto)
                .WithMany(v => v.SolicitudDeTraspasoDeProductosDetalles)
                .HasForeignKey(vd => vd.IdProducto)
                .WillCascadeOnDelete(false);

            // Configuración de EstadoSTPEnum
            modelBuilder.Entity<EstadoSTPEnum>()
                .ToTable("EstadoSTPEnum")
                .HasKey(e => e.IdEstadoSTP);

            modelBuilder.Entity<EstadoSTPEnum>()
                .Property(e => e.IdEstadoSTP)
                .HasColumnName("IdEstadoSTP")
                .IsRequired();

            modelBuilder.Entity<EstadoSTPEnum>()
                .Property(e => e.Descripcion)
                .HasColumnName("Descripcion")
                .IsRequired();

            // Relación con EstadoSTPEnum con SolicitudDeTraspasoDeProductos
            modelBuilder.Entity<SolicitudDeTraspasoDeProductos>()
                .HasRequired(s => s.EstadoSTP)
                .WithMany()
                .HasForeignKey(s => s.IdEstadoSTP);

            // Configuración de Encargado
            modelBuilder.Entity<Encargado>()
                .ToTable("Encargado")
                .HasKey(i => i.IdEncargado)
                .Property(i => i.IdEncargado)
                .HasColumnName("IdEncargado")
                .IsRequired();

            modelBuilder.Entity<Encargado>()
                .Property(i => i.NombreEncargado)
                .HasColumnName("NombreEncargado")
                .IsRequired();

            modelBuilder.Entity<Encargado>()
                .Property(i => i.DNI)
                .HasColumnName("DNI")
                .IsRequired();

            // Configuración de EncargadoSucursal
            modelBuilder.Entity<EncargadoSucursal>()
                .ToTable("EncargadoSucursal")
                .HasKey(ip => ip.IdEncargadoSucursal)
                .Property(ip => ip.IdEncargadoSucursal)
                .HasColumnName("IdEncargadoSucursal")
                .IsRequired();

            modelBuilder.Entity<EncargadoSucursal>()
                .Property(ip => ip.IdEncargado)
                .HasColumnName("IdEncargado")
                .IsRequired();

            modelBuilder.Entity<EncargadoSucursal>()
                .Property(ip => ip.IdSucursal)
                .HasColumnName("IdSucursal")
                .IsRequired();

            // Relación con Encargado
            modelBuilder.Entity<EncargadoSucursal>()
                .HasRequired(i => i.Encargado)
                .WithMany()
                .HasForeignKey(i => i.IdEncargado);

            // Relación con Sucursal
            modelBuilder.Entity<EncargadoSucursal>()
                .HasRequired(i => i.Sucursal)
                .WithMany()
                .HasForeignKey(i => i.IdSucursal);

        }
    }
}


