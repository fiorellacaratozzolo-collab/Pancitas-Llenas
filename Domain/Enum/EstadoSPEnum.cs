using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public class EstadoSPEnum
    {
        public int IdEstadoSP { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<SolicitudDePedido> SolicitudDePedido { get; set; }
    }
}
