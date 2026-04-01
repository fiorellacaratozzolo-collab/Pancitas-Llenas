using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Bll;
using Services.DomainModel.Composite;

namespace Services.Facade
{

    public static class LoginService
    {
        public static void Login(string user, string password)
        {
            // La BLL valida las credenciales y trae el árbol de permisos
            Usuario usuarioValido = UsuarioBll.ValidarCredenciales(user, password);

            // El Facade guarda al usuario en el Singleton de Sesión
            SessionManager.Current.Login(usuarioValido);
        }

        public static void Logout()
        {
            SessionManager.Current.Logout();
        }

        public static void RegistrarUsuario(Usuario usuario)
        {
            UsuarioBll.RegistrarUsuario(usuario);
        }
    }

}
