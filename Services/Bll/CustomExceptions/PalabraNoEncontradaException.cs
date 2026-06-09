using System;

namespace Services.Bll.CustomExceptions
{
    /// <summary>
    /// Excepción interna lanzada por el repositorio de idiomas cuando se solicita la traducción de un término inexistente en el diccionario.
    /// </summary>
    internal class PalabraNoEncontradaException : Exception
    {
        public string Palabra { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la excepción almacenando la palabra exacta que generó el fallo de traducción.
        /// </summary>
        public PalabraNoEncontradaException(string palabra)
            : base(string.Format("La palabra {0} no ha sido encontrada", palabra))
        {
            this.Palabra = palabra;
        }
    }
}