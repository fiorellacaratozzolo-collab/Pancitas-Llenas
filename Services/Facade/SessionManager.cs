using Services.DomainModel.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Facade
{
    public class SessionManager
    {
        private static SessionManager _instance;
        private Usuario _usuarioLogueado;

        // Singleton
        public static SessionManager Current
        {
            get
            {
                if (_instance == null)
                    _instance = new SessionManager();
                return _instance;
            }
        }

        private SessionManager() { }

        public Usuario UsuarioLogueado => _usuarioLogueado;

        public void Login(Usuario usuario)
        {
            _usuarioLogueado = usuario;
        }

        public void Logout()
        {
            _usuarioLogueado = null;
            // Se puede agregar un evento para avisarle a los Forms que se cerró la sesión
        }

        // Método clave para que los Forms validen permisos rápidamente
        public bool TienePermiso(string dataKeyPermiso)
        {
            if (_usuarioLogueado == null) return false;

            foreach (var privilegio in _usuarioLogueado.Privilegios)
            {
                if (ValidarPermisoRecursivo(privilegio, dataKeyPermiso))
                    return true;
            }
            return false;
        }

        private bool ValidarPermisoRecursivo(Component componente, string dataKey)
        {
            if (componente is Patente patente)
            {
                if (patente.DataKey == dataKey) return true;
            }
            else if (componente is Familia familia)
            {
                foreach (var hijo in familia.Hijos)
                {
                    if (ValidarPermisoRecursivo(hijo, dataKey)) return true;
                }
            }
            return false;
        }
    }
}
