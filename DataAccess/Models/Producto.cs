using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Producto
{
    public Guid IdProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public decimal? PesoNeto { get; set; }

    public string? Unidad { get; set; }

    public decimal? PrecioNeto { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<InventarioProducto> InventarioProductos { get; set; } = new List<InventarioProducto>();

    public virtual ICollection<ProveedorProducto> ProveedorProductos { get; set; } = new List<ProveedorProducto>();

    public virtual ICollection<SolicitudDePedidoDetalle> SolicitudDePedidoDetalles { get; set; } = new List<SolicitudDePedidoDetalle>();

    public virtual ICollection<SolicitudDeTraspasoDeProductosDetalle> SolicitudDeTraspasoDeProductosDetalles { get; set; } = new List<SolicitudDeTraspasoDeProductosDetalle>();
}
