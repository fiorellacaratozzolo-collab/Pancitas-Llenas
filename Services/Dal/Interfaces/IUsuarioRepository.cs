using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.DomainModel.Composite;

namespace Services.Dal.Interfaces
{
    internal interface IUsuarioRepository
    {
        void RegistrarUsuario(Usuario usuario);
        Usuario GetByCredentials(string user, string password);
    }
}
