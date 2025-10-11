using System;
using System.Collections.Generic;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework;

public partial class PetShopDBContext : DbContext
{
    public PetShopDBContext()
    {
    }

    public PetShopDBContext(DbContextOptions<PetShopDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Encargado> Encargados { get; set; }

    public virtual DbSet<EncargadoSucursal> EncargadoSucursals { get; set; }

    public virtual DbSet<EstadoIsenum> EstadoIsenums { get; set; }

    public virtual DbSet<EstadoOcenum> EstadoOcenums { get; set; }

    public virtual DbSet<EstadoOpenum> EstadoOpenums { get; set; }

    public virtual DbSet<EstadoSpenum> EstadoSpenums { get; set; }

    public virtual DbSet<EstadoStockEnum> EstadoStockEnums { get; set; }

    public virtual DbSet<EstadoStpenum> EstadoStpenums { get; set; }

    public virtual DbSet<OrdenDeCompra> OrdenDeCompras { get; set; }

    public virtual DbSet<OrdenDeCompraDetalle> OrdenDeCompraDetalles { get; set; }

    public virtual DbSet<OrdenDePedido> OrdenDePedidos { get; set; }

    public virtual DbSet<OrdenDePedidoDetalle> OrdenDePedidoDetalles { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<ProveedorProducto> ProveedorProductos { get; set; }

    public virtual DbSet<SolicitudDePedido> SolicitudDePedidos { get; set; }

    public virtual DbSet<SolicitudDePedidoDetalle> SolicitudDePedidoDetalles { get; set; }

    public virtual DbSet<SolicitudDeTraspasoDeProducto> SolicitudDeTraspasoDeProductos { get; set; }

    public virtual DbSet<SolicitudDeTraspasoDeProductosDetalle> SolicitudDeTraspasoDeProductosDetalles { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<TipoClienteEnum> TipoClienteEnums { get; set; }

    public virtual DbSet<TipoSucursalEnum> TipoSucursalEnums { get; set; }

    public virtual DbSet<TipoVentaEnum> TipoVentaEnums { get; set; }

    public virtual DbSet<VentaDetalle> VentaDetalles { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    public virtual DbSet<StockPorSucursal> StockPorSucursals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer("Data Source=FIORE;Initial Catalog=PetShopDB;Integrated Security=True;TrustServerCertificate=True");

    //MAPEO DE TODAS LAS ENTIDADES
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente);

            entity.ToTable("Cliente");

            entity.Property(e => e.IdCliente).ValueGeneratedNever();
            entity.Property(e => e.Dni).HasColumnName("DNI");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTipoClienteNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdTipoCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_TipoClienteEnum");
        });

        modelBuilder.Entity<Encargado>(entity =>
        {
            entity.HasKey(e => e.IdEncargado);

            entity.ToTable("Encargado");

            entity.Property(e => e.IdEncargado).ValueGeneratedNever();
            entity.Property(e => e.Dni).HasColumnName("DNI");
            entity.Property(e => e.NombreEncargado)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EncargadoSucursal>(entity =>
        {
            entity.HasKey(e => e.IdEncargadoSucursal);

            entity.ToTable("EncargadoSucursal");

            entity.Property(e => e.IdEncargadoSucursal).ValueGeneratedNever();

            entity.HasOne(d => d.IdEncargadoNavigation).WithMany(p => p.EncargadoSucursals)
                .HasForeignKey(d => d.IdEncargado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EncargadoSucursal_Encargado");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.EncargadoSucursals)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EncargadoSucursal_Sucursal");
        });

        modelBuilder.Entity<EstadoIsenum>(entity =>
        {
            entity.HasKey(e => e.IdEstadoIs).HasName("PK__EstadoIS__A40E4169897FF9CD");

            entity.ToTable("EstadoISEnum");

            entity.Property(e => e.IdEstadoIs)
                .ValueGeneratedNever()
                .HasColumnName("IdEstadoIS");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadoOcenum>(entity =>
        {
            entity.HasKey(e => e.IdEstadoOc).HasName("PK__EstadoOC__A40E91BCFA3EF346");

            entity.ToTable("EstadoOCEnum");

            entity.HasIndex(e => e.Descripcion, "UQ_EstadoOCEnum_Descripcion").IsUnique();

            entity.Property(e => e.IdEstadoOc).HasColumnName("IdEstadoOC");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadoOpenum>(entity =>
        {
            entity.HasKey(e => e.IdEstadoOp).HasName("PK__EstadoOP__A40E91B12C96C4A8");

            entity.ToTable("EstadoOPEnum");

            entity.HasIndex(e => e.Descripcion, "UQ_EstadoOPEnum_Descripcion").IsUnique();

            entity.Property(e => e.IdEstadoOp).HasColumnName("IdEstadoOP");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadoSpenum>(entity =>
        {
            entity.HasKey(e => e.IdEstadoSp).HasName("PK__EstadoSP__A402BE324F608734");

            entity.ToTable("EstadoSPEnum");

            entity.HasIndex(e => e.Descripcion, "UQ_EstadoSPEnum_Descripcion").IsUnique();

            entity.Property(e => e.IdEstadoSp).HasColumnName("IdEstadoSP");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadoStockEnum>(entity =>
        {
            entity.HasKey(e => e.IdEstadoStock).HasName("PK__EstadoSt__A9968E1F1F8B29BD");

            entity.ToTable("EstadoStockEnum");

            entity.HasIndex(e => e.Descripcion, "UQ_EstadoStockEnum_Descripcion").IsUnique();

            entity.Property(e => e.Descripcion)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadoStpenum>(entity =>
        {
            entity.HasKey(e => e.IdEstadoStp).HasName("PK__EstadoST__1B48908BBDFE491C");

            entity.ToTable("EstadoSTPEnum");

            entity.HasIndex(e => e.Descripcion, "UQ_EstadoSTPEnum_Descripcion").IsUnique();

            entity.Property(e => e.IdEstadoStp).HasColumnName("IdEstadoSTP");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OrdenDeCompra>(entity =>
        {
            entity.HasKey(e => e.IdOrdenDeCompra);

            entity.ToTable("OrdenDeCompra");

            entity.Property(e => e.IdOrdenDeCompra).ValueGeneratedNever();
            entity.Property(e => e.FechaOc)
                .HasColumnType("datetime")
                .HasColumnName("FechaOC");
            entity.Property(e => e.IdEstadoOc).HasColumnName("IdEstadoOC");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdEstadoOcNavigation).WithMany(p => p.OrdenDeCompras)
                .HasForeignKey(d => d.IdEstadoOc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenDeCompra_EstadoOCEnum");
        });

        modelBuilder.Entity<OrdenDeCompraDetalle>(entity =>
        {
            entity.HasKey(e => e.IdOrdenDeCompraDetalle);

            entity.ToTable("OrdenDeCompraDetalle");

            entity.Property(e => e.IdOrdenDeCompraDetalle).ValueGeneratedNever();
            entity.Property(e => e.PesoNeto).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Unidad)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdOrdenDeCompraNavigation).WithMany(p => p.OrdenDeCompraDetalles)
                .HasForeignKey(d => d.IdOrdenDeCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenDeCompraDetalle_OrdenDeCompra");
        });

        modelBuilder.Entity<OrdenDePedido>(entity =>
        {
            entity.HasKey(e => e.IdOrdenDePedido);

            entity.ToTable("OrdenDePedido");

            entity.Property(e => e.IdOrdenDePedido).ValueGeneratedNever();
            entity.Property(e => e.FechaOp)
                .HasColumnType("datetime")
                .HasColumnName("FechaOP");
            entity.Property(e => e.IdEstadoOp).HasColumnName("IdEstadoOP");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdEstadoOpNavigation).WithMany(p => p.OrdenDePedidos)
                .HasForeignKey(d => d.IdEstadoOp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenDePedido_EstadoOPEnum");
        });

        modelBuilder.Entity<OrdenDePedidoDetalle>(entity =>
        {
            entity.HasKey(e => e.IdOrdenDePedidoDetalle);

            entity.ToTable("OrdenDePedidoDetalle");

            entity.Property(e => e.IdOrdenDePedidoDetalle).ValueGeneratedNever();
            entity.Property(e => e.PesoNeto).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Unidad)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdOrdenDePedidoNavigation).WithMany(p => p.OrdenDePedidoDetalles)
                .HasForeignKey(d => d.IdOrdenDePedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenDePedidoDetalle_OrdenDePedido");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);

            entity.ToTable("Producto");

            entity.Property(e => e.IdProducto).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).IsUnicode(false);
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PesoNeto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PrecioNeto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Unidad)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor);

            entity.ToTable("Proveedor");

            entity.Property(e => e.IdProveedor).ValueGeneratedNever();
            entity.Property(e => e.Cuit).HasColumnName("CUIT");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreProveedor)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProveedorProducto>(entity =>
        {
            entity.HasKey(e => e.IdProveedorProducto);

            entity.ToTable("ProveedorProducto");

            entity.Property(e => e.IdProveedorProducto).ValueGeneratedNever();

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProveedorProductos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProveedorProducto_Producto");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ProveedorProductos)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProveedorProducto_Proveedor");
        });

        modelBuilder.Entity<SolicitudDePedido>(entity =>
        {
            entity.HasKey(e => e.IdSolicitudDePedido);

            entity.ToTable("SolicitudDePedido");

            entity.Property(e => e.IdSolicitudDePedido).ValueGeneratedNever();
            entity.Property(e => e.FechaSp)
                .HasColumnType("datetime")
                .HasColumnName("FechaSP");
            entity.Property(e => e.IdEstadoSp).HasColumnName("IdEstadoSP");

            entity.HasOne(d => d.IdEstadoSpNavigation).WithMany(p => p.SolicitudDePedidos)
                .HasForeignKey(d => d.IdEstadoSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitudDePedido_EstadoSPEnum");
        });

        modelBuilder.Entity<SolicitudDePedidoDetalle>(entity =>
        {
            entity.HasKey(e => e.IdSolicitudDePedidoDetalle);

            entity.ToTable("SolicitudDePedidoDetalle");

            entity.Property(e => e.IdSolicitudDePedidoDetalle).ValueGeneratedNever();
            entity.Property(e => e.Cantidad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PesoNeto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Unidad)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.SolicitudDePedidoDetalles)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitudDePedidoDetalle_Producto");

            entity.HasOne(d => d.IdSolicitudDePedidoNavigation).WithMany(p => p.SolicitudDePedidoDetalles)
                .HasForeignKey(d => d.IdSolicitudDePedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitudDePedidoDetalle_SolicitudDePedido");
        });

        modelBuilder.Entity<SolicitudDeTraspasoDeProducto>(entity =>
        {
            entity.HasKey(e => e.IdSolicitudDeTraspasoDeProductos);

            entity.Property(e => e.IdSolicitudDeTraspasoDeProductos).ValueGeneratedNever();
            entity.Property(e => e.FechaStp)
                .HasColumnType("datetime")
                .HasColumnName("FechaSTP");
            entity.Property(e => e.IdEstadoStp).HasColumnName("IdEstadoSTP");

            entity.HasOne(d => d.IdEstadoStpNavigation).WithMany(p => p.SolicitudDeTraspasoDeProductos)
                .HasForeignKey(d => d.IdEstadoStp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitudDeTraspasoDeProductos_EstadoSTPEnum");
        });

        modelBuilder.Entity<SolicitudDeTraspasoDeProductosDetalle>(entity =>
        {
            entity.HasKey(e => e.IdSolicitudDeTraspasoDeProductosDetalle);

            entity.ToTable("SolicitudDeTraspasoDeProductosDetalle");

            entity.Property(e => e.IdSolicitudDeTraspasoDeProductosDetalle).ValueGeneratedNever();
            entity.Property(e => e.PesoNeto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Unidad)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.SolicitudDeTraspasoDeProductosDetalles)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitudDeTraspasoDeProductosDetalle_Producto");

            entity.HasOne(d => d.IdSolicitudDeTraspasoDeProductosNavigation).WithMany(p => p.SolicitudDeTraspasoDeProductosDetalles)
                .HasForeignKey(d => d.IdSolicitudDeTraspasoDeProductos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SolicitudDeTraspasoDeProductosDetalle_SolicitudDeTraspasoDeProductos");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.IdSucursal);

            entity.ToTable("Sucursal");

            entity.Property(e => e.IdSucursal).ValueGeneratedNever();
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreSucursal)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTipoSucursalNavigation).WithMany(p => p.Sucursals)
                .HasForeignKey(d => d.IdTipoSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sucursal_TipoSucursalEnum");
        });

        modelBuilder.Entity<TipoClienteEnum>(entity =>
        {
            entity.HasKey(e => e.IdTipoCliente).HasName("PK__TipoClie__F173C7FAD0E202C9");

            entity.ToTable("TipoClienteEnum");

            entity.Property(e => e.IdTipoCliente).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoSucursalEnum>(entity =>
        {
            entity.HasKey(e => e.IdTipoSucursal).HasName("PK__TipoSucu__66F50EDB86DD0D64");

            entity.ToTable("TipoSucursalEnum");

            entity.HasIndex(e => e.Descripcion, "UQ_TipoSucursalEnum_Descripcion").IsUnique();

            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoVentaEnum>(entity =>
        {
            entity.HasKey(e => e.IdTipoVenta).HasName("PK__TipoVent__191D80C0599BD2F7");

            entity.ToTable("TipoVentaEnum");

            entity.HasIndex(e => e.Descripcion, "UQ_TipoVentaEnum_Descripcion").IsUnique();

            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VentaDetalle>(entity =>
        {
            entity.HasKey(e => e.IdVentaDetalle);

            entity.ToTable("VentaDetalle");

            entity.Property(e => e.IdVentaDetalle).ValueGeneratedNever();
            entity.Property(e => e.PesoNeto).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Unidad)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdProductoNavigation).WithMany()
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VentaDetalle_Producto");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.VentaDetalles)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VentaDetalle_Venta");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta);

            entity.Property(e => e.IdVenta).ValueGeneratedNever();
            entity.Property(e => e.FechaVenta).HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdTipoVentaNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdTipoVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Venta_TipoVentaEnum");
        });

        modelBuilder.Entity<StockPorSucursal>(entity =>
        {
            entity.HasKey(e => e.IdStockSucursal);
            entity.ToTable("StockPorSucursal");
            entity.Property(e => e.IdStockSucursal)
                .ValueGeneratedNever();

            entity.Property(e => e.StockActual)
                .HasColumnType("int")
                .HasDefaultValue(0);

            entity.Property(e => e.StockDeseado)
                .HasColumnType("int")
                .HasDefaultValue(0);            

            // Relación a PRODUCTO: Se usa la FK correcta (IdProducto), evitando el error 'ProductoIdProducto'.
            entity.HasOne(d => d.IdProductoNavigation)
                .WithMany()
                .HasForeignKey(d => d.IdProducto) // <-- ¡Correcto!
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockSucursal_Producto");

            // Relación a SUCURSAL: Se usa la FK correcta (IdSucursal), evitando el error 'SucursalIdSucursal'.
            entity.HasOne(d => d.IdSucursalNavigation)
                .WithMany()
                .HasForeignKey(d => d.IdSucursal) // <-- ¡Correcto!
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockSucursal_Sucursal");

            // Relación a ESTADO STOCK: Se usa la FK correcta (IdEstadoStock), evitando el error 'EstadoStockEnumIdEstadoStock'.
            entity.HasOne(d => d.IdEstadoStockNavigation)
                .WithMany()
                .HasForeignKey(d => d.IdEstadoStock) // <-- ¡Correcto!
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockSucursal_EstadoStock");

            // --- Índice Único (Muy importante para el stock) ---
            // Esto garantiza que solo haya una fila de Stock por Producto y por Sucursal.
            entity.HasIndex(e => new { e.IdProducto, e.IdSucursal })
                .IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
