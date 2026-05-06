using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dal.Interfaces
{
    /// <summary>
    /// Define el contrato para el sistema de registro de eventos (Logger), soportando múltiples niveles de severidad y manejo detallado de excepciones.
    /// </summary>
    public interface ILogger
    {
        /// <summary>Registra un mensaje de seguimiento (Trace).</summary>
        void Trace(string message);
        /// <summary>Registra un mensaje de seguimiento (Trace) junto con los detalles de una excepción.</summary>
        void Trace(string message, Exception exception);

        /// <summary>Registra un mensaje de depuración (Debug).</summary>
        void Debug(string message);
        /// <summary>Registra un mensaje de depuración (Debug) junto con los detalles de una excepción.</summary>
        void Debug(string message, Exception exception);

        /// <summary>Registra un mensaje informativo (Information).</summary>
        void Information(string message);
        /// <summary>Registra un mensaje informativo (Information) junto con los detalles de una excepción.</summary>
        void Information(string message, Exception exception);

        /// <summary>Registra un mensaje de advertencia (Warning).</summary>
        void Warning(string message);
        /// <summary>Registra un mensaje de advertencia (Warning) junto con los detalles de una excepción.</summary>
        void Warning(string message, Exception exception);

        /// <summary>Registra un mensaje de error (Error).</summary>
        void Error(string message);
        /// <summary>Registra un mensaje de error (Error) junto con los detalles de una excepción.</summary>
        void Error(string message, Exception exception);

        /// <summary>Registra un mensaje de error crítico o fatal (Fatal).</summary>
        void Fatal(string message);
        /// <summary>Registra un mensaje de error crítico o fatal (Fatal) junto con los detalles de una excepción.</summary>
        void Fatal(string message, Exception exception);
    }
}