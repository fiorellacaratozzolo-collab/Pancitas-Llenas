using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.CustomExceptions
{
    public class CantidadInvalidaException : Exception
    {
        public int CantidadIngresada { get; private set; }

        public CantidadInvalidaException(int cantidad)
            : base($"La cantidad ingresada ({cantidad}) no es válida. Debe ser un número mayor a cero.")
        {
            this.CantidadIngresada = cantidad;
        }

        public CantidadInvalidaException(string nombreProducto, int cantidad)
            : base($"No se puede procesar '{nombreProducto}' con cantidad {cantidad}. Ingrese un valor mayor a cero.")
        {
            this.CantidadIngresada = cantidad;
        }
    }
}
