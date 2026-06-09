namespace ModelsDTO
{
    public partial class ClienteDTO
    {
        public Guid IdCliente { get; set; }

        public string? NombreCliente { get; set; }

        public int? Dni { get; set; }

        public int IdTipoCliente { get; set; }
        public bool Activo { get; set; }

        public virtual TipoClienteEnumDTO IdTipoClienteNavigation { get; set; } = null!;

        public virtual ICollection<VentumDTO> Venta { get; set; } = new List<VentumDTO>();
    }
}
