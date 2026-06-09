namespace ModelsDTO
{
    public partial class StockPorSucursalDTO
    {
        public Guid IdStockSucursal { get; set; }
        public Guid IdProducto { get; set; }
        public Guid IdSucursal { get; set; }
        public int StockActual { get; set; }
        public int StockDeseado { get; set; }
        public string? PesoNeto { get; set; }
        public string? Unidad { get; set; }
        public int IdEstadoStock { get; set; }
        public bool Activo { get; set; }
        public string? NombreProducto { get; set; }
        public string? Marca { get; set; }
        public virtual EstadoStockEnumDTO IdEstadoStockNavigation { get; set; } = null!;

        public virtual ProductoDTO IdProductoNavigation { get; set; } = null!;

        public virtual SucursalDTO IdSucursalNavigation { get; set; } = null!;
    }

}
