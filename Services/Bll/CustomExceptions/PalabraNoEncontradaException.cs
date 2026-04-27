using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bll.CustomExceptions
{
    /// <summary>
    /// Se pueden aplicar políticas de excepciones a nivel Objeto (Tipo)
    /// </summary>
    internal class PalabraNoEncontradaException : Exception
    {
        // Se puede atar cualquier INFO pertinente que el diseñador quiera compartir con aquel
        // que debe tratar esta excepción.
        public string Palabra { get; private set; }
        public PalabraNoEncontradaException(string palabra) : base($"La palabra {palabra} no ha sido encontrada")
        {
            this.Palabra = palabra;
        }
    }
}
