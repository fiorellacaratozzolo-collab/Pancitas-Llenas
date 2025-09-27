using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class InventarioSurcusal
{
    public Guid IdIventarioSucursal { get; set; }

    public Guid IdInventario { get; set; }

    public Guid IdSucursal { get; set; }

    public int IdEstadoIs { get; set; }

    public virtual EstadoIsenum IdEstadoIsNavigation { get; set; } = null!;

    public virtual Inventario IdInventarioNavigation { get; set; } = null!;

    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;
}
