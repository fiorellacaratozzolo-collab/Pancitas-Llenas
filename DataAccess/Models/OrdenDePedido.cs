using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class OrdenDePedido
{
    public Guid IdOrdenDePedido { get; set; }

    public DateTime FechaOp { get; set; }

    public int IdEstadoOp { get; set; }

    public decimal Total { get; set; }

    public virtual EstadoOpenum IdEstadoOpNavigation { get; set; } = null!;

    public virtual ICollection<OrdenDePedidoDetalle> OrdenDePedidoDetalles { get; set; } = new List<OrdenDePedidoDetalle>();
}
