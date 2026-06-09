namespace ModelsDTO
{
    public partial class EstadoStpenumDTO
    {
        public int IdEstadoStp { get; set; }

        public string Descripcion { get; set; } = null!;

        public virtual ICollection<SolicitudDeTraspasoDeProductoDTO> SolicitudDeTraspasoDeProductos { get; set; } = new List<SolicitudDeTraspasoDeProductoDTO>();
    }
}
