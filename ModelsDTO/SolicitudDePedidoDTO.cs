namespace ModelsDTO
{
    public partial class SolicitudDePedidoDTO
    {
        public Guid IdSolicitudDePedido { get; set; }

        public DateTime FechaSp { get; set; }
        public string? EstadoTexto { get; set; }
        public int IdEstadoSp { get; set; }

        public virtual ICollection<SolicitudDePedidoDetalleDTO> SolicitudDePedidoDetalles { get; set; } = new List<SolicitudDePedidoDetalleDTO>();
    }
}
