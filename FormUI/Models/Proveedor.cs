using System;
using System.Collections.Generic;

namespace FormUI.Models;

public partial class Proveedor
{
    public Guid IdProveedor { get; set; }

    public string NombreProveedor { get; set; } = null!;

    public int Cuit { get; set; }

    public int? Telefono { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<OrdenDeCompra> OrdenDeCompras { get; set; } = new List<OrdenDeCompra>();

    public virtual ICollection<ProveedorProducto> ProveedorProductos { get; set; } = new List<ProveedorProducto>();
}
