using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class EstadoStpenum
{
    public int IdEstadoStp { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<SolicitudDeTraspasoDeProducto> SolicitudDeTraspasoDeProductos { get; set; } = new List<SolicitudDeTraspasoDeProducto>();
}
