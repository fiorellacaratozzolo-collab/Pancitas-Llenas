using System;
using System.Collections.Generic;

namespace Services.DomainModel.Composite
{
    /// <summary>
    /// Representa a un usuario del sistema, incluyendo sus credenciales, estado y su árbol de privilegios (roles y permisos individuales).
    /// </summary>
    public class Usuario
    {
        public Guid IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Habilitado { get; set; }
        public string IdiomaPredeterminado { get; set; }
        public Guid? IdSucursal { get; set; }
        public List<Component> Privilegios { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase Usuario con los datos básicos y prepara su colección de privilegios.
        /// </summary>
        public Usuario(Guid id, string nombre, string email, string password, bool habilitado, Guid? idSucursal = null)
        {
            IdUsuario = id;
            Nombre = nombre;
            Email = email;
            Password = password;
            Habilitado = habilitado;
            IdSucursal = idSucursal;
            Privilegios = new List<Component>();
        }
    }
}
