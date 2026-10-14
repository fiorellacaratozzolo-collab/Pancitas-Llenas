namespace ModelsDTO
{
    public partial class ProveedorDTO
    {
        public Guid IdProveedor { get; set; }

        public string NombreProveedor { get; set; } = null!;

        public string? Cuit { get; set; }

        public int? Telefono { get; set; }

        public string? Direccion { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<ProveedorProductoDTO> ProveedorProductos { get; set; } = new List<ProveedorProductoDTO>();
    }
}
