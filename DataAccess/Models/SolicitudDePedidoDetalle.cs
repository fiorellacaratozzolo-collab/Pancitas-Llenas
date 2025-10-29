using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class SolicitudDePedidoDetalle
{
    public Guid IdSolicitudDePedidoDetalle { get; set; } = Guid.NewGuid();

    public Guid IdSolicitudDePedido { get; set; }

    public Guid IdProducto { get; set; }

    public string Cantidad { get; set; } = null!;

    public decimal PesoNeto { get; set; }

    public string Unidad { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual SolicitudDePedido IdSolicitudDePedidoNavigation { get; set; } = null!;
}
