using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class StockPorSucursal
{
    public Guid IdStockSucursal { get; set; }

    public Guid IdProducto { get; set; }

    public Guid IdSucursal { get; set; }

    public int StockActual { get; set; }

    public int StockDeseado { get; set; }

    public int IdEstadoStock { get; set; }

    public virtual EstadoStockEnum IdEstadoStockNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;
}
