using System;
using System.Collections.Generic;

namespace FormUI.Models;

public partial class EstadoOcenum
{
    public int IdEstadoOc { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<OrdenDeCompra> OrdenDeCompras { get; set; } = new List<OrdenDeCompra>();
}
