using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public class EstadoOPEnum
    {
        public int IdEstadoOP { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<OrdenDePedido> OrdenDePedido { get; set; }
    }
}
