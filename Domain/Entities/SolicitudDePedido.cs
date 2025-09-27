using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Detalles;
using Domain.Enum;

namespace Domain.Entities
{
    public class SolicitudDePedido
    {
        public Guid IdSolicitudDePedido { get; set; }

        public DateTime FechaSP { get; set; }

        public int IdEstadoSP { get; set; } //Clave Foránea a EstadoSPEnum

        public EstadoSPEnum EstadoSP { get; set; } //Propiedad de navegación

        public List<SolicitudDePedidoDetalle> SolicitudDePedidoDetalle { get; set; }

        public virtual EstadoSPEnum EstadoSPEnum { get; set; }
    }
}
