using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Inventario
{
    public Guid IdInventario { get; set; }

    public int IdEstadoStock { get; set; }

    public virtual EstadoStockEnum IdEstadoStockNavigation { get; set; } = null!;

    public virtual ICollection<InventarioProducto> InventarioProductos { get; set; } = new List<InventarioProducto>();
}
