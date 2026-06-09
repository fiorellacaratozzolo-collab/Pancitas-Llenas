using Services.Bll;
using Services.Dal.Interfaces;
using Services.DomainModel.Logging;

namespace Services.Facade
{
    /// <summary>
    /// Servicio centralizado para la configuración y obtención de instancias del sistema de registro de eventos (Logger).
    /// </summary>
    public class LoggerService
    {
        /// <summary>
        /// Configura y genera una nueva instancia del registrador de eventos basado en archivos físicos, estableciendo la ruta y el nivel mínimo de depuración.
        /// </summary>
        public static ILogger GetLogger()
        {
            var config = new LoggerConfiguration
            {
                LogFilePath = "Logs/mi_app.log",
                MinimumLogLevel = LogLevel.Debug
            };

            return config.CreateFileLogger();
        }
    }
}
