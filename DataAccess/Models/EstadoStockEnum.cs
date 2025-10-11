using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class EstadoStockEnum
{
    public int IdEstadoStock { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<StockPorSucursal> StockPorSucursals { get; set; } = new List<StockPorSucursal>();
}
