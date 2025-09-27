using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class InventarioProducto
{
    public Guid IdInventarioProducto { get; set; }

    public Guid IdInventario { get; set; }

    public Guid IdProducto { get; set; }

    public virtual Inventario IdInventarioNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
