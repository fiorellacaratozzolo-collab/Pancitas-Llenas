using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainModel.Logging
{
    /// <summary>
    /// Representa una entrada individual de registro destinada al archivo físico de logs del sistema.
    /// </summary>
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public LogLevel Level { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        /// <summary>
        /// Formatea la entrada del registro como una cadena de texto estructurada para su escritura en el archivo.
        /// </summary>
        public override string ToString()
        {
            string exceptionInfo = Exception != null ? string.Format("\nExcepción: {0}", Exception.ToString()) : string.Empty;
            return string.Format("[{0:yyyy-MM-dd HH:mm:ss.fff zzz}] [{1}] {2}{3}", Timestamp, Level, Message, exceptionInfo);
        }
    }
}