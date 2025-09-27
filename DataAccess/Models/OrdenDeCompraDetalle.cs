using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class OrdenDeCompraDetalle
{
    public Guid IdOrdenDeCompraDetalle { get; set; }

    public Guid IdOrdenDeCompra { get; set; }

    public int Cantidad { get; set; }

    public decimal PesoNeto { get; set; }

    public decimal PrecioUnitario { get; set; }

    public string Unidad { get; set; } = null!;

    public decimal Subtotal { get; set; }

    public virtual OrdenDeCompra IdOrdenDeCompraNavigation { get; set; } = null!;
}
