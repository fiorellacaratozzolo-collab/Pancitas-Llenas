using Services.Bll.CustomExceptions;
using Services.Dal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.Bll
{
    /// <summary>
    /// Servicio Singleton encargado de gestionar el sistema de internacionalización, traducción dinámica y auto-descubrimiento de términos de la aplicación.
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
        /// Traduce un texto al idioma configurado en el hilo actual. Si el término no existe, lo registra automáticamente en el repositorio para su futura traducción.
        /// </summary>
        public static string Traducir(string texto)
        {
            try
            {
                return IdiomaRepository.Traducir(texto);
            }
            catch (PalabraNoEncontradaException ex)
            {
                IdiomaRepository.AgregarPalabra(texto);

                Facade.LoggerService.GetLogger().Warning(string.Format("Término no encontrado y auto-registrado en el diccionario: {0}", ex.Palabra));

                return texto;
            }
            catch (Exception ex)
            {
                BitácoraBll bitacora = new BitácoraBll();
                bitacora.RegistrarLog(
                    string.Format("Error crítico en el servicio de traducción: {0}", ex.Message),
                    DomainModel.Logging.Criticidad.Error
                );

                throw;
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

