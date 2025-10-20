using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Encargado",
                columns: table => new
                {
                    IdEncargado = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreEncargado = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DNI = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encargado", x => x.IdEncargado);
                });

            migrationBuilder.CreateTable(
                name: "EstadoISEnum",
                columns: table => new
                {
                    IdEstadoIS = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EstadoIS__A40E4169897FF9CD", x => x.IdEstadoIS);
                });

            migrationBuilder.CreateTable(
                name: "EstadoOCEnum",
                columns: table => new
                {
                    IdEstadoOC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EstadoOC__A40E91BCFA3EF346", x => x.IdEstadoOC);
                });

            migrationBuilder.CreateTable(
                name: "EstadoOPEnum",
                columns: table => new
                {
                    IdEstadoOP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EstadoOP__A40E91B12C96C4A8", x => x.IdEstadoOP);
                });

            migrationBuilder.CreateTable(
                name: "EstadoSPEnum",
                columns: table => new
                {
                    IdEstadoSP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EstadoSP__A402BE324F608734", x => x.IdEstadoSP);
                });

            migrationBuilder.CreateTable(
                name: "EstadoStockEnum",
                columns: table => new
                {
                    IdEstadoStock = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EstadoSt__A9968E1F1F8B29BD", x => x.IdEstadoStock);
                });

            migrationBuilder.CreateTable(
                name: "EstadoSTPEnum",
                columns: table => new
                {
                    IdEstadoSTP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EstadoST__1B48908BBDFE491C", x => x.IdEstadoSTP);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    IdProducto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreProducto = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Marca = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PesoNeto = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Unidad = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    PrecioNeto = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Descripcion = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.IdProducto);
                });

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    IdProveedor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreProveedor = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CUIT = table.Column<int>(type: "int", nullable: false),
                    Telefono = table.Column<int>(type: "int", nullable: true),
                    Direccion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedor", x => x.IdProveedor);
                });

            migrationBuilder.CreateTable(
                name: "TipoClienteEnum",
                columns: table => new
                {
                    IdTipoCliente = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipoClie__F173C7FAD0E202C9", x => x.IdTipoCliente);
                });

            migrationBuilder.CreateTable(
                name: "TipoSucursalEnum",
                columns: table => new
                {
                    IdTipoSucursal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipoSucu__66F50EDB86DD0D64", x => x.IdTipoSucursal);
                });

            migrationBuilder.CreateTable(
                name: "TipoVentaEnum",
                columns: table => new
                {
                    IdTipoVenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TipoVent__191D80C0599BD2F7", x => x.IdTipoVenta);
                });

            migrationBuilder.CreateTable(
                name: "OrdenDeCompra",
                columns: table => new
                {
                    IdOrdenDeCompra = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaOC = table.Column<DateTime>(type: "datetime", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    IdEstadoOC = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenDeCompra", x => x.IdOrdenDeCompra);
                    table.ForeignKey(
                        name: "FK_OrdenDeCompra_EstadoOCEnum",
                        column: x => x.IdEstadoOC,
                        principalTable: "EstadoOCEnum",
                        principalColumn: "IdEstadoOC");
                });

            migrationBuilder.CreateTable(
                name: "OrdenDePedido",
                columns: table => new
                {
                    IdOrdenDePedido = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaOP = table.Column<DateTime>(type: "datetime", nullable: false),
                    IdEstadoOP = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenDePedido", x => x.IdOrdenDePedido);
                    table.ForeignKey(
                        name: "FK_OrdenDePedido_EstadoOPEnum",
                        column: x => x.IdEstadoOP,
                        principalTable: "EstadoOPEnum",
                        principalColumn: "IdEstadoOP");
                });

            migrationBuilder.CreateTable(
                name: "SolicitudDePedido",
                columns: table => new
                {
                    IdSolicitudDePedido = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaSP = table.Column<DateTime>(type: "datetime", nullable: false),
                    IdEstadoSP = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudDePedido", x => x.IdSolicitudDePedido);
                    table.ForeignKey(
                        name: "FK_SolicitudDePedido_EstadoSPEnum",
                        column: x => x.IdEstadoSP,
                        principalTable: "EstadoSPEnum",
                        principalColumn: "IdEstadoSP");
                });

            migrationBuilder.CreateTable(
                name: "SolicitudDeTraspasoDeProductos",
                columns: table => new
                {
                    IdSolicitudDeTraspasoDeProductos = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaSTP = table.Column<DateTime>(type: "datetime", nullable: false),
                    IdEstadoSTP = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudDeTraspasoDeProductos", x => x.IdSolicitudDeTraspasoDeProductos);
                    table.ForeignKey(
                        name: "FK_SolicitudDeTraspasoDeProductos_EstadoSTPEnum",
                        column: x => x.IdEstadoSTP,
                        principalTable: "EstadoSTPEnum",
                        principalColumn: "IdEstadoSTP");
                });

            migrationBuilder.CreateTable(
                name: "ProveedorProducto",
                columns: table => new
                {
                    IdProveedorProducto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProveedor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProducto = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveedorProducto", x => x.IdProveedorProducto);
                    table.ForeignKey(
                        name: "FK_ProveedorProducto_Producto",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "IdProducto");
                    table.ForeignKey(
                        name: "FK_ProveedorProducto_Proveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedor",
                        principalColumn: "IdProveedor");
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    IdCliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreCliente = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DNI = table.Column<int>(type: "int", nullable: true),
                    IdTipoCliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.IdCliente);
                    table.ForeignKey(
                        name: "FK_Cliente_TipoClienteEnum",
                        column: x => x.IdTipoCliente,
                        principalTable: "TipoClienteEnum",
                        principalColumn: "IdTipoCliente");
                });

            migrationBuilder.CreateTable(
                name: "Sucursal",
                columns: table => new
                {
                    IdSucursal = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Direccion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    NombreSucursal = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Telefono = table.Column<int>(type: "int", nullable: true),
                    IdTipoSucursal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursal", x => x.IdSucursal);
                    table.ForeignKey(
                        name: "FK_Sucursal_TipoSucursalEnum",
                        column: x => x.IdTipoSucursal,
                        principalTable: "TipoSucursalEnum",
                        principalColumn: "IdTipoSucursal");
                });

            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    IdVenta = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroVenta = table.Column<int>(type: "int", nullable: false),
                    FechaVenta = table.Column<DateTime>(type: "datetime", nullable: false),
                    IdTipoVenta = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venta", x => x.IdVenta);
                    table.ForeignKey(
                        name: "FK_Venta_TipoVentaEnum",
                        column: x => x.IdTipoVenta,
                        principalTable: "TipoVentaEnum",
                        principalColumn: "IdTipoVenta");
                });

            migrationBuilder.CreateTable(
                name: "OrdenDeCompraDetalle",
                columns: table => new
                {
                    IdOrdenDeCompraDetalle = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdOrdenDeCompra = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PesoNeto = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Unidad = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenDeCompraDetalle", x => x.IdOrdenDeCompraDetalle);
                    table.ForeignKey(
                        name: "FK_OrdenDeCompraDetalle_OrdenDeCompra",
                        column: x => x.IdOrdenDeCompra,
                        principalTable: "OrdenDeCompra",
                        principalColumn: "IdOrdenDeCompra");
                });

            migrationBuilder.CreateTable(
                name: "OrdenDePedidoDetalle",
                columns: table => new
                {
                    IdOrdenDePedidoDetalle = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdOrdenDePedido = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PesoNeto = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Unidad = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenDePedidoDetalle", x => x.IdOrdenDePedidoDetalle);
                    table.ForeignKey(
                        name: "FK_OrdenDePedidoDetalle_OrdenDePedido",
                        column: x => x.IdOrdenDePedido,
                        principalTable: "OrdenDePedido",
                        principalColumn: "IdOrdenDePedido");
                });

            migrationBuilder.CreateTable(
                name: "SolicitudDePedidoDetalle",
                columns: table => new
                {
                    IdSolicitudDePedidoDetalle = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSolicitudDePedido = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProducto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cantidad = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PesoNeto = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Unidad = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudDePedidoDetalle", x => x.IdSolicitudDePedidoDetalle);
                    table.ForeignKey(
                        name: "FK_SolicitudDePedidoDetalle_Producto",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "IdProducto");
                    table.ForeignKey(
                        name: "FK_SolicitudDePedidoDetalle_SolicitudDePedido",
                        column: x => x.IdSolicitudDePedido,
                        principalTable: "SolicitudDePedido",
                        principalColumn: "IdSolicitudDePedido");
                });

            migrationBuilder.CreateTable(
                name: "SolicitudDeTraspasoDeProductosDetalle",
                columns: table => new
                {
                    IdSolicitudDeTraspasoDeProductosDetalle = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSolicitudDeTraspasoDeProductos = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProducto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PesoNeto = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Unidad = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudDeTraspasoDeProductosDetalle", x => x.IdSolicitudDeTraspasoDeProductosDetalle);
                    table.ForeignKey(
                        name: "FK_SolicitudDeTraspasoDeProductosDetalle_Producto",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "IdProducto");
                    table.ForeignKey(
                        name: "FK_SolicitudDeTraspasoDeProductosDetalle_SolicitudDeTraspasoDeProductos",
                        column: x => x.IdSolicitudDeTraspasoDeProductos,
                        principalTable: "SolicitudDeTraspasoDeProductos",
                        principalColumn: "IdSolicitudDeTraspasoDeProductos");
                });

            migrationBuilder.CreateTable(
                name: "EncargadoSucursal",
                columns: table => new
                {
                    IdEncargadoSucursal = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdEncargado = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSucursal = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncargadoSucursal", x => x.IdEncargadoSucursal);
                    table.ForeignKey(
                        name: "FK_EncargadoSucursal_Encargado",
                        column: x => x.IdEncargado,
                        principalTable: "Encargado",
                        principalColumn: "IdEncargado");
                    table.ForeignKey(
                        name: "FK_EncargadoSucursal_Sucursal",
                        column: x => x.IdSucursal,
                        principalTable: "Sucursal",
                        principalColumn: "IdSucursal");
                });

            migrationBuilder.CreateTable(
    name: "StockPorSucursal",
    columns: table => new
    {
        IdStockSucursal = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
        IdProducto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
        IdSucursal = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
        IdEstadoStock = table.Column<int>(type: "int", nullable: false),
        StockActual = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
        StockDeseado = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_StockPorSucursal", x => x.IdStockSucursal);
        table.ForeignKey(
            name: "FK_StockPorSucursal_EstadoStockEnum_IdEstadoStock",
            column: x => x.IdEstadoStock,
            principalTable: "EstadoStockEnum",
            principalColumn: "IdEstadoStock");
        table.ForeignKey(
            name: "FK_StockPorSucursal_Producto_IdProducto",
            column: x => x.IdProducto,
            principalTable: "Producto",
            principalColumn: "IdProducto");
        table.ForeignKey(
            name: "FK_StockPorSucursal_Sucursal_IdSucursal",
            column: x => x.IdSucursal,
            principalTable: "Sucursal",
            principalColumn: "IdSucursal");
            
    });

            migrationBuilder.CreateTable(
                name: "VentaDetalle",
                columns: table => new
                {
                    IdVentaDetalle = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdVenta = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProducto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PesoNeto = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Unidad = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    ClienteIdCliente = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductoIdProducto = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaDetalle", x => x.IdVentaDetalle);
                    table.ForeignKey(
                        name: "FK_VentaDetalle_Cliente_ClienteIdCliente",
                        column: x => x.ClienteIdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK_VentaDetalle_Producto",
                        column: x => x.IdProducto,
                        principalTable: "Producto",
                        principalColumn: "IdProducto");
                    table.ForeignKey(
                        name: "FK_VentaDetalle_Producto_ProductoIdProducto",
                        column: x => x.ProductoIdProducto,
                        principalTable: "Producto",
                        principalColumn: "IdProducto");
                    table.ForeignKey(
                        name: "FK_VentaDetalle_Venta",
                        column: x => x.IdVenta,
                        principalTable: "Venta",
                        principalColumn: "IdVenta");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_IdTipoCliente",
                table: "Cliente",
                column: "IdTipoCliente");

            migrationBuilder.CreateIndex(
                name: "IX_EncargadoSucursal_IdEncargado",
                table: "EncargadoSucursal",
                column: "IdEncargado");

            migrationBuilder.CreateIndex(
                name: "IX_EncargadoSucursal_IdSucursal",
                table: "EncargadoSucursal",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "UQ_EstadoOCEnum_Descripcion",
                table: "EstadoOCEnum",
                column: "Descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_EstadoOPEnum_Descripcion",
                table: "EstadoOPEnum",
                column: "Descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_EstadoSPEnum_Descripcion",
                table: "EstadoSPEnum",
                column: "Descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_EstadoStockEnum_Descripcion",
                table: "EstadoStockEnum",
                column: "Descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_EstadoSTPEnum_Descripcion",
                table: "EstadoSTPEnum",
                column: "Descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdenDeCompra_IdEstadoOC",
                table: "OrdenDeCompra",
                column: "IdEstadoOC");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenDeCompraDetalle_IdOrdenDeCompra",
                table: "OrdenDeCompraDetalle",
                column: "IdOrdenDeCompra");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenDePedido_IdEstadoOP",
                table: "OrdenDePedido",
                column: "IdEstadoOP");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenDePedidoDetalle_IdOrdenDePedido",
                table: "OrdenDePedidoDetalle",
                column: "IdOrdenDePedido");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedorProducto_IdProducto",
                table: "ProveedorProducto",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedorProducto_IdProveedor",
                table: "ProveedorProducto",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudDePedido_IdEstadoSP",
                table: "SolicitudDePedido",
                column: "IdEstadoSP");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudDePedidoDetalle_IdProducto",
                table: "SolicitudDePedidoDetalle",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudDePedidoDetalle_IdSolicitudDePedido",
                table: "SolicitudDePedidoDetalle",
                column: "IdSolicitudDePedido");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudDeTraspasoDeProductos_IdEstadoSTP",
                table: "SolicitudDeTraspasoDeProductos",
                column: "IdEstadoSTP");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudDeTraspasoDeProductosDetalle_IdProducto",
                table: "SolicitudDeTraspasoDeProductosDetalle",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudDeTraspasoDeProductosDetalle_IdSolicitudDeTraspasoDeProductos",
                table: "SolicitudDeTraspasoDeProductosDetalle",
                column: "IdSolicitudDeTraspasoDeProductos");

            migrationBuilder.CreateIndex(
                name: "IX_StockPorSucursal_EstadoStockEnumIdEstadoStock",
                table: "StockPorSucursal",
                column: "EstadoStockEnumIdEstadoStock");

            migrationBuilder.CreateIndex(
                name: "IX_StockPorSucursal_IdEstadoStock",
                table: "StockPorSucursal",
                column: "IdEstadoStock");

            migrationBuilder.CreateIndex(
                name: "IX_StockPorSucursal_IdProducto_IdSucursal",
                table: "StockPorSucursal",
                columns: new[] { "IdProducto", "IdSucursal" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockPorSucursal_IdSucursal",
                table: "StockPorSucursal",
                column: "IdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_StockPorSucursal_ProductoIdProducto",
                table: "StockPorSucursal",
                column: "ProductoIdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_StockPorSucursal_SucursalIdSucursal",
                table: "StockPorSucursal",
                column: "SucursalIdSucursal");

            migrationBuilder.CreateIndex(
                name: "IX_Sucursal_IdTipoSucursal",
                table: "Sucursal",
                column: "IdTipoSucursal");

            migrationBuilder.CreateIndex(
                name: "UQ_TipoSucursalEnum_Descripcion",
                table: "TipoSucursalEnum",
                column: "Descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TipoVentaEnum_Descripcion",
                table: "TipoVentaEnum",
                column: "Descripcion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venta_IdTipoVenta",
                table: "Venta",
                column: "IdTipoVenta");

            migrationBuilder.CreateIndex(
                name: "IX_VentaDetalle_ClienteIdCliente",
                table: "VentaDetalle",
                column: "ClienteIdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_VentaDetalle_IdProducto",
                table: "VentaDetalle",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_VentaDetalle_IdVenta",
                table: "VentaDetalle",
                column: "IdVenta");

            migrationBuilder.CreateIndex(
                name: "IX_VentaDetalle_ProductoIdProducto",
                table: "VentaDetalle",
                column: "ProductoIdProducto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EncargadoSucursal");

            migrationBuilder.DropTable(
                name: "EstadoISEnum");

            migrationBuilder.DropTable(
                name: "OrdenDeCompraDetalle");

            migrationBuilder.DropTable(
                name: "OrdenDePedidoDetalle");

            migrationBuilder.DropTable(
                name: "ProveedorProducto");

            migrationBuilder.DropTable(
                name: "SolicitudDePedidoDetalle");

            migrationBuilder.DropTable(
                name: "SolicitudDeTraspasoDeProductosDetalle");

            migrationBuilder.DropTable(
                name: "StockPorSucursal");

            migrationBuilder.DropTable(
                name: "VentaDetalle");

            migrationBuilder.DropTable(
                name: "Encargado");

            migrationBuilder.DropTable(
                name: "OrdenDeCompra");

            migrationBuilder.DropTable(
                name: "OrdenDePedido");

            migrationBuilder.DropTable(
                name: "Proveedor");

            migrationBuilder.DropTable(
                name: "SolicitudDePedido");

            migrationBuilder.DropTable(
                name: "SolicitudDeTraspasoDeProductos");

            migrationBuilder.DropTable(
                name: "EstadoStockEnum");

            migrationBuilder.DropTable(
                name: "Sucursal");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Venta");

            migrationBuilder.DropTable(
                name: "EstadoOCEnum");

            migrationBuilder.DropTable(
                name: "EstadoOPEnum");

            migrationBuilder.DropTable(
                name: "EstadoSPEnum");

            migrationBuilder.DropTable(
                name: "EstadoSTPEnum");

            migrationBuilder.DropTable(
                name: "TipoSucursalEnum");

            migrationBuilder.DropTable(
                name: "TipoClienteEnum");

            migrationBuilder.DropTable(
                name: "TipoVentaEnum");
        }
    }
}
