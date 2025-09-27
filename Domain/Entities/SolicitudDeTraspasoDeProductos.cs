using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Detalles;
using Domain.Enum;

namespace Domain.Entities
{
    public class SolicitudDeTraspasoDeProductos
    {
        public Guid IdSolicitudDeTraspasoDeProductos { get; set; }

        public DateTime FechaSTP { get; set; }
  
        public int IdEstadoSTP { get; set; } //Clave Foránea a EstadoSTPEnum

        public EstadoSTPEnum EstadoSTP { get; set; } //Propiedad de navegación

        public List<SolicitudDeTraspasoDeProductosDetalle> SolicitudDeTraspasoDeProductosDetalle { get; set; }

        public virtual ICollection<SolicitudDeTraspasoDeProductosDetalle> SolicitudDeTraspasoDeProductosDetalles { get; set; }

        public virtual EstadoSTPEnum EstadoSTPEnum { get; set; }
    }
}
