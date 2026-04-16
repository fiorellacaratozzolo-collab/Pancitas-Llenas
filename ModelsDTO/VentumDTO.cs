using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public partial class VentumDTO
    {
        public Guid IdVenta { get; set; }

        public Guid IdSucursal { get; set; }

        public Guid IdCliente { get; set; }

        public int NumeroVenta { get; set; }

        public DateTime FechaVenta { get; set; }

        public decimal Total { get; set; }

        public string MetodoPago { get; set; } = null!;

        public decimal MontoDescuento { get; set; }
        public string? NombreCliente { get; set; }
        public bool EsMayorista { get; set; }

        public virtual ClienteDTO IdClienteNavigation { get; set; } = null!;

        public virtual ICollection<VentaDetalleDTO> VentaDetalles { get; set; } = new List<VentaDetalleDTO>();
    }
}
