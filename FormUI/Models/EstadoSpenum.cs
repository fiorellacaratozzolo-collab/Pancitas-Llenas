using System;
using System.Collections.Generic;

namespace FormUI.Models;

public partial class EstadoSpenum
{
    public int IdEstadoSp { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<SolicitudDePedido> SolicitudDePedidos { get; set; } = new List<SolicitudDePedido>();
}
