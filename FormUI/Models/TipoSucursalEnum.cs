using System;
using System.Collections.Generic;

namespace FormUI.Models;

public partial class TipoSucursalEnum
{
    public int IdTipoSucursal { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Sucursal> Sucursals { get; set; } = new List<Sucursal>();
}
