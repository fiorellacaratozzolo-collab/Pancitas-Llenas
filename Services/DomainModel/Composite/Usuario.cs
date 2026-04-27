using System;
using System.Collections.Generic;

namespace Services.DomainModel.Composite
{
    public class Usuario
    {
        public Guid IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Habilitado { get; set; }
        public Guid? IdSucursal { get; set; }
        public List<Component> Privilegios { get; set; }

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
