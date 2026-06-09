using Services.DomainModel.Composite;

namespace Services.Dal.Interfaces
{
    /// <summary>
    /// Define el contrato para las operaciones de acceso a datos específicas de la entidad Usuario, extendiendo el repositorio genérico.
    /// </summary>
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        /// <summary>
        /// Recupera un usuario basándose exclusivamente en su nombre de usuario.
        /// </summary>
        Usuario GetByUserName(string username);

        /// <summary>
        /// Valida y recupera un usuario comprobando que coincidan de manera exacta su usuario y contraseña cifrada.
        /// </summary>
        Usuario GetByCredentials(string user, string password);
    }
}