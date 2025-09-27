using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public class EstadoISEnum
    {
        public int IdEstadoIS { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<InventarioSucursal> InventarioSucursal { get; set; }
    }
}
