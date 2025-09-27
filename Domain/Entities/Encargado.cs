using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Encargado
    {
        public Guid IdEncargado { get; set; }

        public string NombreEncargado { get; set; }

        public int DNI {  get; set; }

        public virtual ICollection<EncargadoSucursal> EncargadoSucursales { get; set; } //Relación con tabla intermedia

    }
}
