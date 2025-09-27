using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public class EstadoOCEnum
    {
        public int IdEstadoOC { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<OrdenDeCompra> OrdenDeCompra { get; set; }

    }

}
