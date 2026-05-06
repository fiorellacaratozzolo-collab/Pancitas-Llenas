using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainModel.Logging
{
    /// <summary>
    /// Define los niveles de severidad utilizados para el registro de auditoría en la base de datos (Bitácora).
    /// </summary>
    public enum Criticidad
    {
        Info,
        Warning,
        Error,
        Fatal
    }
}