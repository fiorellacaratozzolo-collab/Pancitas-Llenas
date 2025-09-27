using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class VentaDetalle
{
    public Guid IdVentaDetalle { get; set; }

    public Guid IdVenta { get; set; }

    public Guid IdCliente { get; set; }

    public int Cantidad { get; set; }

    public decimal PesoNeto { get; set; }

    public string Unidad { get; set; } = null!;

    public decimal PrecioUnitario { get; set; }

    public decimal Subtotal { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Ventum IdVentaNavigation { get; set; } = null!;
}
