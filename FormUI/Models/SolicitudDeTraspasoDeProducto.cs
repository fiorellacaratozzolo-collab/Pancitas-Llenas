using System;
using System.Collections.Generic;

namespace FormUI.Models;

public partial class SolicitudDeTraspasoDeProducto
{
    public Guid IdSolicitudDeTraspasoDeProductos { get; set; }

    public DateTime FechaStp { get; set; }

    public int IdEstadoStp { get; set; }

    public Guid IdSucursalOrigen { get; set; }

    public Guid IdSucursalDestino { get; set; }

    public virtual EstadoStpenum IdEstadoStpNavigation { get; set; } = null!;

    public virtual Sucursal IdSucursalDestinoNavigation { get; set; } = null!;

    public virtual Sucursal IdSucursalOrigenNavigation { get; set; } = null!;

    public virtual ICollection<SolicitudDeTraspasoDeProductosDetalle> SolicitudDeTraspasoDeProductosDetalles { get; set; } = new List<SolicitudDeTraspasoDeProductosDetalle>();
}
