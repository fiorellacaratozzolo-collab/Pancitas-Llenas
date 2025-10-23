using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public partial class EstadoSpenumDTO
    {
        public int IdEstadoSp { get; set; }

        public string Descripcion { get; set; } = null!;

        public virtual ICollection<SolicitudDePedidoDTO> SolicitudDePedidos { get; set; } = new List<SolicitudDePedidoDTO>();
    }

}
