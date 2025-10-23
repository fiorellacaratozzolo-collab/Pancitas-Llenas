using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public partial class SolicitudDeTraspasoDeProductoDTO
    {
        public Guid IdSolicitudDeTraspasoDeProductos { get; set; }

        public DateTime FechaStp { get; set; }

        public int IdEstadoStp { get; set; }

        public virtual EstadoStpenumDTO IdEstadoStpNavigation { get; set; } = null!;

        public virtual ICollection<SolicitudDeTraspasoDeProductosDetalleDTO> SolicitudDeTraspasoDeProductosDetalles { get; set; } = new List<SolicitudDeTraspasoDeProductosDetalleDTO>();
    }
}
