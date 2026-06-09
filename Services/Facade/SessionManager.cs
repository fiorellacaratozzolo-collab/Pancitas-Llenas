using Services.Bll.CustomExceptions;
using Services.DomainModel.Composite;
using System;

namespace Services.Facade
{
    /// <summary>
    /// Gestiona la sesión activa del usuario en el sistema, permitiendo el acceso global al perfil autenticado y la validación de permisos mediante el patrón Singleton.
    /// </summary>
    public class SessionManager
    {
        private static SessionManager _instance;

        /// <summary>
        /// Proporciona el acceso a la instancia única de la sesión actual.
        /// </summary>
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

        private Usuario _usuarioLogueado;

        /// <summary>
        /// Retorna el usuario que ha iniciado sesión. Lanza una excepción de sesión expirada si no hay un usuario autenticado.
        /// </summary>
        public Usuario UsuarioLogueado
        {
            get
            {
                if (_usuarioLogueado == null)
                {
                    throw new SesionExpiradaException();
                }
                return _usuarioLogueado;
            }
            private set
            {
                _usuarioLogueado = value;
            }
        }

        /// <summary>
        /// Obtiene o establece el identificador único de la sucursal donde el usuario está operando actualmente.
        /// </summary>
        public Guid? IdSucursalActual { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre de la sucursal activa.
        /// </summary>
        public string NombreSucursalActual { get; set; }

        /// <summary>
        /// Establece el usuario autenticado en la sesión actual y asigna su sucursal fija si la posee.
        /// </summary>
        public void Login(Usuario usuario)
        {
            UsuarioLogueado = usuario;
            IdSucursalActual = usuario?.IdSucursal;
        }

        /// <summary>
        /// Cierra la sesión activa eliminando los datos del usuario y la sucursal de la memoria de la aplicación.
        /// </summary>
        public void Logout()
        {
            _usuarioLogueado = null;
            IdSucursalActual = null;
            NombreSucursalActual = null;
        }

        /// <summary>
        /// Verifica si el usuario actual posee un permiso específico (patente o familia) basándose en su clave de acceso.
        /// </summary>
        public bool TienePermiso(string dataKeyPermiso)
        {
            foreach (var privilegio in UsuarioLogueado.Privilegios)
            {
                if (ValidarPermisoRecursivo(privilegio, dataKeyPermiso))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Realiza una búsqueda recursiva en la jerarquía de componentes para determinar si una clave de permiso existe dentro de las patentes o familias.
        /// </summary>
        private bool ValidarPermisoRecursivo(Component componente, string dataKey)
        {
            if (componente is Patente patente)
            {
                if (patente.DataKey == dataKey) return true;
            }
            else if (componente is Familia familia)
            {
                if (familia.Hijos != null)
                {
                    foreach (var hijo in familia.Hijos)
                    {
                        if (ValidarPermisoRecursivo(hijo, dataKey)) return true;
                    }
                }
            }
            return false;
        }
    }
}