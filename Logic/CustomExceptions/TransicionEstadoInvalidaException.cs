using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.CustomExceptions
{
    /// <summary>
    /// Excepción lanzada cuando se intenta cambiar el estado de un documento a un flujo no permitido por la máquina de estados del sistema.
    /// </summary>
    public class TransicionEstadoInvalidaException : Exception
    {
        public string EstadoOrigen { get; private set; }
        public string EstadoDestino { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la excepción detallando desde qué estado y hacia qué estado se intentó realizar el salto inválido.
        /// </summary>
        public TransicionEstadoInvalidaException(string origen, string destino)
            : base(string.Format("Operación no permitida: No se puede pasar un documento de '{0}' a '{1}'.", origen, destino))
        {
            this.EstadoOrigen = origen;
            this.EstadoDestino = destino;
        }
    }
}