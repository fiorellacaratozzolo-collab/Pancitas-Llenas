using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Encargado
{
    public Guid IdEncargado { get; set; }

    public string NombreEncargado { get; set; } = null!;

    public int Dni { get; set; }

    public virtual ICollection<EncargadoSucursal> EncargadoSucursals { get; set; } = new List<EncargadoSucursal>();
}
