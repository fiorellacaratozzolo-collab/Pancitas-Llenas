using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class HistorialIngresoStock
{
    public Guid IdHistorialIngreso { get; set; }

    public DateTime FechaIngreso { get; set; }

    public Guid IdSucursal { get; set; }

    public Guid IdProducto { get; set; }

    public int CantidadAgregada { get; set; }

    public Guid? IdProveedor { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;

    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;
}
