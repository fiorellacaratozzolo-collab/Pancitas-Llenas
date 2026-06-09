using Services.Bll;
using Services.DomainModel.Composite;

namespace Services.Facade
{
    /// <summary>
    /// Servicio estático que centraliza las operaciones de autenticación y control de acceso al sistema.
    /// </summary>
    public static class LoginService
    {
        /// <summary>
        /// Valida las credenciales proporcionadas contra la base de datos y, de ser correctas, inicializa la sesión global del usuario.
        /// </summary>
        public static void Login(string username, string passwordClara)
        {
            UsuarioBll usuarioBll = new UsuarioBll();
            Usuario usuarioValido = usuarioBll.ValidarCredenciales(username, passwordClara);

            SessionManager.Current.Login(usuarioValido);
        }

        /// <summary>
        /// Cierra la sesión activa actual y destruye los datos de autenticación del usuario en memoria.
        /// </summary>
        public static void Logout()
        {
            SessionManager.Current.Logout();
        }
    }
}
