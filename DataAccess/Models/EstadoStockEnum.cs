using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class EstadoStockEnum
{
    public int IdEstadoStock { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
