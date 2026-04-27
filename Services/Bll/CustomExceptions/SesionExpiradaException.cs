using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bll.CustomExceptions
{
    public class SesionExpiradaException : Exception
    {
        public DateTime FechaCaida { get; private set; }

        public SesionExpiradaException()
            : base("La sesión ha expirado o se ha perdido la conexión. Por favor, vuelva a iniciar sesión por seguridad.")
        {
            this.FechaCaida = DateTime.Now;
        }
    }
}
