namespace ModelsDTO
{
    public partial class SolicitudDePedidoDetalleDTO
    {
        public Guid IdSolicitudDePedidoDetalle { get; set; }

        public Guid IdSolicitudDePedido { get; set; }

        public Guid IdProducto { get; set; }

        public int Cantidad { get; set; }

        public decimal PesoNeto { get; set; }
        public string? Marca { get; set; }
        public string Unidad { get; set; } = null!;

        public string? NombreProducto { get; set; } 
    }
}
