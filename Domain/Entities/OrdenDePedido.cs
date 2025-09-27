using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Detalles;
using Domain.Enum;

namespace Domain.Entities
{
    public class OrdenDePedido
    {
        public Guid IdOrdenDePedido { get; set; }

        public DateTime FechaOP { get; set; }

        public int IdEstadoOP { get; set; } //Clave Foránea a EstadoOPEnum

        public EstadoOPEnum EstadoOP { get; set; } //Propiedad de navegación

        public List<OrdenDePedidoDetalle> OrdenDePedidoDetalle { get; set; }

        public decimal Total { get; set; }

        public virtual EstadoOPEnum EstadoOPEnum { get; set; }

    }
}
