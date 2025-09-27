using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Proveedor
{
    public Guid IdProveedor { get; set; }

    public string NombreProveedor { get; set; } = null!;

    public int Cuit { get; set; }

    public int? Telefono { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<ProveedorProducto> ProveedorProductos { get; set; } = new List<ProveedorProducto>();
}
