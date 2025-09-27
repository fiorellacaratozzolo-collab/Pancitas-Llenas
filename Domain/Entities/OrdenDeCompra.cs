using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Detalles;
using Domain.Enum;

namespace Domain.Entities
{
    public class OrdenDeCompra
    {
        public Guid IdOrdenDeCompra { get; set; }

        public DateTime FechaOC { get; set; }

        public decimal Total {  get; set; }

        public int IdEstadoOC { get; set; } //Clave Foránea a EstadoOCEnum

        public EstadoOCEnum EstadoOC { get; set; } //Propiedad de navegación

        public List<OrdenDeCompraDetalle> OrdenDeCompraDetalle { get; set; }

        public virtual EstadoOCEnum EstadoOCEnum { get; set; }

    }
}
