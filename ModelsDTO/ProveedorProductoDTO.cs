namespace ModelsDTO
{
    public partial class ProveedorProductoDTO
    {
        public Guid IdProveedorProducto { get; set; }

        public Guid IdProveedor { get; set; }

        public Guid IdProducto { get; set; }

        public virtual ProductoDTO IdProductoNavigation { get; set; } = null!;

        public virtual ProveedorDTO IdProveedorNavigation { get; set; } = null!;
    }
}
