using System;
using System.Collections.Generic;

namespace FormUI.Models;

public partial class Ventum
{
    public Guid IdVenta { get; set; }

    public Guid IdSucursal { get; set; }

    public Guid IdCliente { get; set; }

    public int NumeroVenta { get; set; }

    public DateTime FechaVenta { get; set; }

    public decimal Total { get; set; }

    public string MetodoPago { get; set; } = null!;

    public decimal MontoDescuento { get; set; }

    public bool EsMayorista { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
}
