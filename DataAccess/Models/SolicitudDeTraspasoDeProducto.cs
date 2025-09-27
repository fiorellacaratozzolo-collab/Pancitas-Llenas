using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class SolicitudDeTraspasoDeProducto
{
    public Guid IdSolicitudDeTraspasoDeProductos { get; set; }

    public DateTime FechaStp { get; set; }

    public int IdEstadoStp { get; set; }

    public virtual EstadoStpenum IdEstadoStpNavigation { get; set; } = null!;

    public virtual ICollection<SolicitudDeTraspasoDeProductosDetalle> SolicitudDeTraspasoDeProductosDetalles { get; set; } = new List<SolicitudDeTraspasoDeProductosDetalle>();
}
