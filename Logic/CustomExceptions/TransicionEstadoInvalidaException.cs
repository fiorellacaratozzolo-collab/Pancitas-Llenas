using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.CustomExceptions
{
    public class TransicionEstadoInvalidaException : Exception
    {
        public string EstadoOrigen { get; private set; }
        public string EstadoDestino { get; private set; }

        public TransicionEstadoInvalidaException(string origen, string destino)
            : base($"Operación no permitida: No se puede pasar un documento de '{origen}' a '{destino}'.")
        {
            this.EstadoOrigen = origen;
            this.EstadoDestino = destino;
        }
    }
}
