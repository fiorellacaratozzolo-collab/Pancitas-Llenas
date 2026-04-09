using System;
using System.Collections.Generic;

namespace FormUI.Models;

public partial class EstadoOpenum
{
    public int IdEstadoOp { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<OrdenDePedido> OrdenDePedidos { get; set; } = new List<OrdenDePedido>();
}
