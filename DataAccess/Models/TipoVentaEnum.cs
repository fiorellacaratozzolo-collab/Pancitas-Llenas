using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class TipoVentaEnum
{
    public int IdTipoVenta { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
