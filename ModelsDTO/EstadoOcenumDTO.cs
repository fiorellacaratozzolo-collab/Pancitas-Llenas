namespace ModelsDTO
{
    public partial class EstadoOcenumDTO
    {
        public int IdEstadoOc { get; set; }
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<OrdenDeCompraDTO> OrdenDeCompras { get; set; } = new List<OrdenDeCompraDTO>();
    }
}
