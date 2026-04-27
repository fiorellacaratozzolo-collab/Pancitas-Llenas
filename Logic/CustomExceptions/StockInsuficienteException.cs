using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.CustomExceptions
{
    public class StockInsuficienteException : Exception
    {
        // Propiedades para que la UI sepa exactamente qué pasó
        public string NombreProducto { get; private set; }
        public int CantidadDisponible { get; private set; }
        public int CantidadSolicitada { get; private set; }

        // Constructor básico
        public StockInsuficienteException(string mensaje) : base(mensaje) { }

        // Constructor recomendado: recibe los datos del error
        public StockInsuficienteException(string nombreProducto, int disponible, int solicitada)
            : base($"Stock insuficiente para '{nombreProducto}'. Disponible: {disponible}, Solicitada: {solicitada}.")
        {
            this.NombreProducto = nombreProducto;
            this.CantidadDisponible = disponible;
            this.CantidadSolicitada = solicitada;

        }
    }
}
