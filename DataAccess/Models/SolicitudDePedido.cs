using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class SolicitudDePedido
{
    public Guid IdSolicitudDePedido { get; set; }

    public DateTime FechaSp { get; set; }

    public int IdEstadoSp { get; set; }

    public virtual EstadoSpenum IdEstadoSpNavigation { get; set; } = null!;

    public virtual ICollection<SolicitudDePedidoDetalle> SolicitudDePedidoDetalles { get; set; } = new List<SolicitudDePedidoDetalle>();
}
