using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class SolicitudDeTraspasoDeProductosDetalle
{
    public Guid IdSolicitudDeTraspasoDeProductosDetalle { get; set; }

    public Guid IdSolicitudDeTraspasoDeProductos { get; set; }

    public Guid IdProducto { get; set; }

    public int Cantidad { get; set; }

    public decimal PesoNeto { get; set; }

    public string Unidad { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual SolicitudDeTraspasoDeProducto IdSolicitudDeTraspasoDeProductosNavigation { get; set; } = null!;
}
