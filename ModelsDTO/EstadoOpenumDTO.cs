namespace ModelsDTO
{
    public partial class EstadoOpenumDTO
    {
        public int IdEstadoOp { get; set; }
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<OrdenDePedidoDTO> OrdenDePedidos { get; set; } = new List<OrdenDePedidoDTO>();
    }
}
