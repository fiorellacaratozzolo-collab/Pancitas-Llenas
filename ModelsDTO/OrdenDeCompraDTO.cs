using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public partial class OrdenDeCompraDTO
    {
        public Guid IdOrdenDeCompra { get; set; }

        public DateTime FechaOc { get; set; }

        public decimal Total { get; set; }

        public int IdEstadoOc { get; set; }

        public virtual EstadoOcenumDTO IdEstadoOcNavigation { get; set; } = null!;

        public virtual ICollection<OrdenDeCompraDetalleDTO> OrdenDeCompraDetalles { get; set; } = new List<OrdenDeCompraDetalleDTO>();
    }
}
