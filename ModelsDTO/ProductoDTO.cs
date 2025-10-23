using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public partial class ProductoDTO
    {
        public Guid IdProducto { get; set; }

        public string NombreProducto { get; set; } = null!;

        public string Marca { get; set; } = null!;

        public decimal? PesoNeto { get; set; }

        public string? Unidad { get; set; }

        public decimal? PrecioNeto { get; set; }

        public string? Descripcion { get; set; }

        public virtual ICollection<ProveedorProductoDTO> ProveedorProductos { get; set; } = new List<ProveedorProductoDTO>();

        public virtual ICollection<SolicitudDePedidoDetalleDTO> SolicitudDePedidoDetalles { get; set; } = new List<SolicitudDePedidoDetalleDTO>();

        public virtual ICollection<SolicitudDeTraspasoDeProductosDetalleDTO> SolicitudDeTraspasoDeProductosDetalles { get; set; } = new List<SolicitudDeTraspasoDeProductosDetalleDTO>();

        public virtual ICollection<StockPorSucursalDTO> StockPorSucursals { get; set; } = new List<StockPorSucursalDTO>();

        public virtual ICollection<VentaDetalleDTO> VentaDetalles { get; set; } = new List<VentaDetalleDTO>();
    }
}
