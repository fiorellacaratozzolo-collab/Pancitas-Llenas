using Services.Dal.Implementations;
using Services.Dal.Interfaces;
using Services.DomainModel.Composite;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bll
{
    internal static class UsuarioBll
    {
        private static IUsuarioRepository _usuarioRepository;

        static UsuarioBll()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        public static Usuario ValidarCredenciales(string user, string password)
        {
            // Hasheamos la password ingresada para compararla con la BD
            //string hashedPassword = CryptographyService.HashMd5(password);

            Usuario usuario = _usuarioRepository.GetByCredentials(user, password); //hashedPassword);

            if (usuario == null)
                throw new Exception("Usuario o contraseña incorrectos.");

            if (!usuario.Habilitado)
                throw new Exception("El usuario está deshabilitado. Contacte al administrador.");

            return usuario;
        }

        public static void RegistrarUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario), "El usuario no puede ser nulo.");

            // ¡MUY IMPORTANTE! Encriptar la clave ANTES de guardarla
            usuario.Password = CryptographyService.HashMd5(usuario.Password);

            _usuarioRepository.Add(usuario); // Asegúrate de que tu Repositorio tiene el método Add()
        }
    }
}
