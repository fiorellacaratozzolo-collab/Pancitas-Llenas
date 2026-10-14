using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class TipoClienteEnum
{
    public int IdTipoCliente { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
