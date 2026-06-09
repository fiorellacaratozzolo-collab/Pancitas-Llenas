namespace Logic.Helpers
{
    /// <summary>
    /// Estructura de transferencia interna que encapsula el identificador del proveedor y el precio neto asociado a un producto específico.
    /// </summary>
    public class ProveedorProductoInfo
    {
        public Guid IdProveedor { get; set; }
        public decimal PrecioNeto { get; set; }
    }
}