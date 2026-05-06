using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainModel.Logging
{
    /// <summary>
    /// Define los niveles de severidad utilizados para el registro de eventos en el archivo físico (Logger).
    /// </summary>
    public enum LogLevel
    {
        Trace,
        Debug,
        Information,
        Warning,
        Error,
        Fatal
    }
}
