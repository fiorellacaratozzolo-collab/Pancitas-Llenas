using System;
using System.IO;
using Services.Dal.Interfaces;
using Services.DomainModel.Logging;

namespace Services.Dal.Implementations
{
    /// <summary>
    /// Implementación concreta de ILogger que registra los eventos de la aplicación en un archivo físico de texto de forma segura y sin bloqueos de concurrencia.
    /// </summary>
    public class FileLogger : ILogger
    {
        private readonly string _logFilePath;
        private readonly LogLevel _minimumLogLevel;

        /// <summary>
        /// Inicializa el registrador comprobando o creando el directorio requerido.
        /// </summary>
        public FileLogger(string logFilePath, LogLevel minimumLogLevel)
        {
            _logFilePath = logFilePath;
            _minimumLogLevel = minimumLogLevel;

            string directoryPath = Path.GetDirectoryName(_logFilePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        /// <summary>
        /// Evalúa la severidad, abre el archivo de texto, escribe la entrada de registro y lo cierra inmediatamente para evitar bloqueos del sistema operativo.
        /// </summary>
        private void Log(LogLevel level, string message, Exception exception = null)
        {
            if (level >= _minimumLogLevel)
            {
                var logEntry = new LogEntry
                {
                    Timestamp = DateTime.Now,
                    Level = level,
                    Message = message,
                    Exception = exception
                };

                using (StreamWriter writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine(logEntry.ToString());
                }
            }
        }

        /// <summary>Registra un mensaje de seguimiento.</summary>
        public void Trace(string message) => Log(LogLevel.Trace, message);

        /// <summary>Registra un mensaje de seguimiento con excepción.</summary>
        public void Trace(string message, Exception exception) => Log(LogLevel.Trace, message, exception);

        /// <summary>Registra un mensaje de depuración.</summary>
        public void Debug(string message) => Log(LogLevel.Debug, message);

        /// <summary>Registra un mensaje de depuración con excepción.</summary>
        public void Debug(string message, Exception exception) => Log(LogLevel.Debug, message, exception);

        /// <summary>Registra un mensaje informativo.</summary>
        public void Information(string message) => Log(LogLevel.Information, message);

        /// <summary>Registra un mensaje informativo con excepción.</summary>
        public void Information(string message, Exception exception) => Log(LogLevel.Information, message, exception);

        /// <summary>Registra una advertencia.</summary>
        public void Warning(string message) => Log(LogLevel.Warning, message);

        /// <summary>Registra una advertencia con excepción.</summary>
        public void Warning(string message, Exception exception) => Log(LogLevel.Warning, message, exception);

        /// <summary>Registra un error.</summary>
        public void Error(string message) => Log(LogLevel.Error, message);

        /// <summary>Registra un error con excepción.</summary>
        public void Error(string message, Exception exception) => Log(LogLevel.Error, message, exception);

        /// <summary>Registra un error crítico o fatal.</summary>
        public void Fatal(string message) => Log(LogLevel.Fatal, message);

        /// <summary>Registra un error crítico o fatal con excepción.</summary>
        public void Fatal(string message, Exception exception) => Log(LogLevel.Fatal, message, exception);
    }
}