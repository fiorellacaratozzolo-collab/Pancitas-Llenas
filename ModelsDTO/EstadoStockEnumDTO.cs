namespace ModelsDTO
{
    public partial class EstadoStockEnumDTO
    {
        public int IdEstadoStock { get; set; }

        public string Descripcion { get; set; } = null!;

        public virtual ICollection<StockPorSucursalDTO> StockPorSucursals { get; set; } = new List<StockPorSucursalDTO>();
    }
}
