using System;
using System.Collections.Generic;

namespace FormUI.Models;

public partial class Sucursal
{
    public Guid IdSucursal { get; set; }

    public string Direccion { get; set; } = null!;

    public string NombreSucursal { get; set; } = null!;

    public int? Telefono { get; set; }

    public int IdTipoSucursal { get; set; }

    public virtual TipoSucursalEnum IdTipoSucursalNavigation { get; set; } = null!;

    public virtual ICollection<SolicitudDeTraspasoDeProducto> SolicitudDeTraspasoDeProductoIdSucursalDestinoNavigations { get; set; } = new List<SolicitudDeTraspasoDeProducto>();

    public virtual ICollection<SolicitudDeTraspasoDeProducto> SolicitudDeTraspasoDeProductoIdSucursalOrigenNavigations { get; set; } = new List<SolicitudDeTraspasoDeProducto>();

    public virtual ICollection<StockPorSucursal> StockPorSucursals { get; set; } = new List<StockPorSucursal>();
}
