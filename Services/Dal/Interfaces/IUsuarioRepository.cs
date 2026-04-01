using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.DomainModel.Composite;

namespace Services.Dal.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        // Método para buscar solo por nombre (útil para recuperar contraseñas, por ejemplo)
        Usuario GetByUserName(string username);
       
        // Método para el login comprobando usuario y contraseña hasheada
        Usuario GetByCredentials(string user, string password);
    }
}
