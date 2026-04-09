using System;
using System.Collections.Generic;

namespace FormUI.Models;

public partial class OrdenDeCompra
{
    public Guid IdOrdenDeCompra { get; set; }

    public Guid IdProveedor { get; set; }

    public int IdEstadoOc { get; set; }

    public DateTime FechaOc { get; set; }

    public decimal Total { get; set; }

    public virtual EstadoOcenum IdEstadoOcNavigation { get; set; } = null!;

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;

    public virtual ICollection<OrdenDeCompraDetalle> OrdenDeCompraDetalles { get; set; } = new List<OrdenDeCompraDetalle>();
}
