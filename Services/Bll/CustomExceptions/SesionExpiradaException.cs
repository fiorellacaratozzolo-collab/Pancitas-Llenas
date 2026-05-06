using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bll.CustomExceptions
{
    /// <summary>
    /// Excepción lanzada cuando se intenta acceder a recursos del sistema sin un usuario activo en memoria.
    /// </summary>
    public class SesionExpiradaException : Exception
    {
        public DateTime FechaCaida { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la excepción de sesión expirada.
        /// </summary>
        public SesionExpiradaException()
            : base("La sesión ha expirado o se ha perdido la conexión. Por favor, vuelva a iniciar sesión por seguridad.")
        {
            this.FechaCaida = DateTime.Now;
        }
    }
}
