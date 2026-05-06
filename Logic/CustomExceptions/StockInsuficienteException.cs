using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.CustomExceptions
{
    /// <summary>
    /// Excepción lanzada al intentar procesar un movimiento de inventario para un producto que no posee la cantidad suficiente en la sucursal origen.
    /// </summary>
    public class StockInsuficienteException : Exception
    {
        public string? NombreProducto { get; private set; }
        public int CantidadDisponible { get; private set; }
        public int CantidadSolicitada { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la excepción con un mensaje de error genérico.
        /// </summary>
        public StockInsuficienteException(string mensaje) : base(mensaje) { }

        /// <summary>
        /// Inicializa una nueva instancia de la excepción detallando el producto, el stock actual y la cantidad requerida que disparó el error.
        /// </summary>
        public StockInsuficienteException(string nombreProducto, int disponible, int solicitada)
            : base(string.Format("Stock insuficiente para '{0}'. Disponible: {1}, Solicitada: {2}.", nombreProducto, disponible, solicitada))
        {
            this.NombreProducto = nombreProducto;
            this.CantidadDisponible = disponible;
            this.CantidadSolicitada = solicitada;
        }
    }
}