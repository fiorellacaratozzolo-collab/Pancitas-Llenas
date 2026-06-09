namespace Logic.CustomExceptions
{
    /// <summary>
    /// Excepción lanzada cuando se intenta realizar una transferencia de inventario donde el origen y el destino son idénticos.
    /// </summary>
    public class TraspasoMismaSucursalException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de la excepción de traspaso inválido.
        /// </summary>
        public TraspasoMismaSucursalException()
            : base("Operación inválida: La sucursal de origen y la sucursal de destino no pueden ser la misma.")
        {
        }
    }
}
