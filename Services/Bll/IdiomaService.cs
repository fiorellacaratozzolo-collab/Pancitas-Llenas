using Services.Dal;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Services.Bll
{
    /// <summary>
    /// Servicio Singleton encargado de gestionar el sistema de internacionalización y traducción dinámica de la aplicación.
    /// </summary>
    public sealed class IdiomaService
    {
        #region Singleton
        private readonly static IdiomaService _instance = new IdiomaService();

        /// <summary>
        /// Obtiene la instancia única del servicio de idiomas.
        /// </summary>
        public static IdiomaService Current
        {
            get
            {
                return _instance;
            }
        }

        private IdiomaService()
        {
        }
        #endregion

        /// <summary>
        /// Traduce un texto al idioma configurado en el hilo actual. Si el término no existe en el diccionario, devuelve el texto original.
        /// </summary>
        public static string Traducir(string texto)
        {
            try
            {
                // Llamamos directamente al repositorio. Si no encuentra la palabra, el repositorio ya se encarga de devolver el texto original.
                return IdiomaRepository.Traducir(texto);
            }
            catch (Exception ex)
            {
                // Registramos cualquier error grave (ej: el archivo de texto se borró, no hay permisos de lectura, etc.)
                // Asumiendo que tu BitácoraBll funciona así:
                BitácoraBll bitacora = new BitácoraBll();
                bitacora.RegistrarLog(
                    string.Format("Error crítico leyendo los archivos de traducción: {0}", ex.Message),
                    DomainModel.Logging.Criticidad.Error
                );

                // En lugar de hacer un "throw;" que haría explotar toda la interfaz gráfica de golpe, 
                // devolvemos el texto original. Así el sistema sigue funcionando (aunque no se traduzca).
                return texto;
            }
        }

        /// <summary>
        /// Obtiene la lista completa de idiomas (culturas) que la aplicación soporta actualmente.
        /// </summary>
        public static List<CultureInfo> ObtenerIdiomas()
        {
            return IdiomaRepository.ObtenerIdiomas();
        }
    }
}

