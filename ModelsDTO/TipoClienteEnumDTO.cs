namespace ModelsDTO
{
    public partial class TipoClienteEnumDTO
    {
        public int IdTipoCliente { get; set; }

        public string Descripcion { get; set; } = null!;

        public virtual ICollection<ClienteDTO> Clientes { get; set; } = new List<ClienteDTO>();
    }
}
