using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Detalles;
using Domain.Enum;

namespace Domain.Entities
{
    public class Venta
    {
        public Guid IdVenta { get; set; }

        public int NumeroVenta { get; set; }

        public DateTime FechaVenta { get; set; }

        public int IdTipoVenta { get; set; } //Clave Foránea a TipoVentaEnum

        public TipoVentaEnum TipoVenta { get; set; } // Propiedad de navegación

        public string Descripcion { get; set; } 

        public decimal Total {  get; set; }

        public List<VentaDetalle> VentaDetalle { get; set; }

        public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } //Relación con tabla VentaDetalle

        public virtual TipoVentaEnum VipoVentaEnum { get; set; }

    }
}
