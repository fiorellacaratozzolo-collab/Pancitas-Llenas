using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class EncargadoSucursal
{
    public Guid IdEncargadoSucursal { get; set; }

    public Guid IdEncargado { get; set; }

    public Guid IdSucursal { get; set; }

    public virtual Encargado IdEncargadoNavigation { get; set; } = null!;

    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;
}
