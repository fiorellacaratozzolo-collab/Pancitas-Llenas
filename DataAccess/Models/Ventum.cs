using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Ventum
{
    public Guid IdVenta { get; set; }

    public int NumeroVenta { get; set; }

    public DateTime FechaVenta { get; set; }

    public Guid IdCliente { get; set; }

    public int IdTipoVenta { get; set; }

    public decimal Total { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual TipoVentaEnum IdTipoVentaNavigation { get; set; } = null!;

    public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
}
