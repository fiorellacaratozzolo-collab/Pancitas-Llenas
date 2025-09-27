using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class OrdenDeCompra
{
    public Guid IdOrdenDeCompra { get; set; }

    public DateTime FechaOc { get; set; }

    public decimal Total { get; set; }

    public int IdEstadoOc { get; set; }

    public virtual EstadoOcenum IdEstadoOcNavigation { get; set; } = null!;

    public virtual ICollection<OrdenDeCompraDetalle> OrdenDeCompraDetalles { get; set; } = new List<OrdenDeCompraDetalle>();
}
