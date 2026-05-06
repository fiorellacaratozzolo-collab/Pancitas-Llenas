using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Dal.Interfaces;
using Services.DomainModel.Logging;
using Services.Dal.Implementations;

namespace Services.Bll
{
    /// <summary>
    /// Administra la configuración base necesaria para instanciar el sistema de registro de archivos (Logger).
    /// </summary>
    public class LoggerConfiguration
    {
        public string LogFilePath { get; set; } = "Logs/app.log";
        public LogLevel MinimumLogLevel { get; set; } = LogLevel.Information;

        /// <summary>
        /// Evalúa las configuraciones establecidas y fabrica una instancia concreta del FileLogger.
        /// </summary>
        public ILogger CreateFileLogger()
        {
            return new FileLogger(LogFilePath, MinimumLogLevel);
        }
    }
}