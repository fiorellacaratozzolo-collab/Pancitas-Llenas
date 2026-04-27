using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bll.CustomExceptions
{
    public class UsuarioBloqueadoException : Exception
    {
        public string Username { get; private set; }
        public DateTime FechaBloqueo { get; private set; }

        public UsuarioBloqueadoException(string username)
            : base($"El usuario '{username}' se encuentra bloqueado. Contacte al administrador.")
        {
            this.Username = username;
            this.FechaBloqueo = DateTime.Now;
        }

        public UsuarioBloqueadoException(string username, string motivo)
            : base($"Usuario '{username}' bloqueado. Motivo: {motivo}")
        {
            this.Username = username;
        }
    }
}
