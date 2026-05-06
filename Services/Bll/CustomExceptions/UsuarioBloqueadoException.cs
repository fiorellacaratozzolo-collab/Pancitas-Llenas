using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bll.CustomExceptions
{
    /// <summary>
    /// Excepción lanzada cuando se intenta autenticar o utilizar una cuenta de usuario que ha sido deshabilitada en el sistema.
    /// </summary>
    public class UsuarioBloqueadoException : Exception
    {
        public string Username { get; private set; }
        public DateTime FechaBloqueo { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la excepción indicando el nombre del usuario bloqueado.
        /// </summary>
        public UsuarioBloqueadoException(string username)
            : base(string.Format("El usuario '{0}' se encuentra bloqueado. Contacte al administrador.", username))
        {
            this.Username = username;
            this.FechaBloqueo = DateTime.Now;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la excepción detallando el nombre del usuario y el motivo específico del bloqueo.
        /// </summary>
        public UsuarioBloqueadoException(string username, string motivo)
            : base(string.Format("Usuario '{0}' bloqueado. Motivo: {1}", username, motivo))
        {
            this.Username = username;
            this.FechaBloqueo = DateTime.Now;
        }
    }
}
