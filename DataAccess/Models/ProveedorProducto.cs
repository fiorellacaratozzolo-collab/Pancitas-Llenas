using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class ProveedorProducto
{
    public Guid IdProveedorProducto { get; set; }

    public Guid IdProveedor { get; set; }

    public Guid IdProducto { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;
}
