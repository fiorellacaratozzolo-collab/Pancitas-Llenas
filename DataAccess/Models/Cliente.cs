using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Cliente
{
    public Guid IdCliente { get; set; }

    public string? NombreCliente { get; set; }

    public int? Dni { get; set; }

    public int IdTipoCliente { get; set; }

    public virtual TipoClienteEnum IdTipoClienteNavigation { get; set; } = null!;

    public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
}
