using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class OrdenDePedidoDetalle
{
    public Guid IdOrdenDePedidoDetalle { get; set; }

    public Guid IdOrdenDePedido { get; set; }

    public int Cantidad { get; set; }

    public decimal PesoNeto { get; set; }

    public decimal PrecioUnitario { get; set; }

    public string Unidad { get; set; } = null!;

    public decimal Subtotal { get; set; }

    public virtual OrdenDePedido IdOrdenDePedidoNavigation { get; set; } = null!;
}
