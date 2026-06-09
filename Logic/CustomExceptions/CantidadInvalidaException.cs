namespace Logic.CustomExceptions
{
    /// <summary>
    /// Excepción lanzada cuando se detecta un valor numérico cero o negativo en operaciones que requieren cantidades estrictamente positivas.
    /// </summary>
    public class CantidadInvalidaException : Exception
    {
        public int CantidadIngresada { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la excepción indicando únicamente el valor numérico rechazado.
        /// </summary>
        public CantidadInvalidaException(int cantidad)
            : base(string.Format("La cantidad ingresada ({0}) no es válida. Debe ser un número mayor a cero.", cantidad))
        {
            this.CantidadIngresada = cantidad;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la excepción detallando el producto involucrado y el valor numérico rechazado.
        /// </summary>
        public CantidadInvalidaException(string nombreProducto, int cantidad)
            : base(string.Format("No se puede procesar '{0}' con cantidad {1}. Ingrese un valor mayor a cero.", nombreProducto, cantidad))
        {
            this.CantidadIngresada = cantidad;
        }
    }
}