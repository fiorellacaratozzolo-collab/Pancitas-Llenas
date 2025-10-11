using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Sucursal
{
    public Guid IdSucursal { get; set; }

    public string Direccion { get; set; } = null!;

    public string NombreSucursal { get; set; } = null!;

    public int? Telefono { get; set; }

    public int IdTipoSucursal { get; set; }

    public virtual ICollection<StockPorSucursal> StockPorSucursals { get; set; } = new List<StockPorSucursal>();
    public virtual ICollection<EncargadoSucursal> EncargadoSucursals { get; set; } = new List<EncargadoSucursal>();

    public virtual TipoSucursalEnum IdTipoSucursalNavigation { get; set; } = null!;
}
