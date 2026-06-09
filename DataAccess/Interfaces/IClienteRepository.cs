using DataAccess.Models;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define el contrato de persistencia para gestionar las operaciones CRUD de la entidad Cliente.
    /// </summary>
    public interface IClienteRepository
    {
        /// <summary>Registra un nuevo cliente y retorna su identificador único.</summary>
        Guid Create(Cliente cliente);

        /// <summary>Realiza un borrado lógico del cliente cambiando su estado a inactivo.</summary>
        void Delete(Guid id);

        /// <summary>Reactiva un cliente previamente deshabilitado.</summary>
        void Habilitar(Guid id);

        /// <summary>Obtiene la lista completa de clientes registrados (sin filtrar por estado).</summary>
        List<Cliente> GetAll();

        /// <summary>Filtra y obtiene los clientes según su tipo o condición fiscal.</summary>
        List<Cliente> GetByTipoCliente(int IdTipoCliente);

        /// <summary>Busca un cliente específico utilizando su número de DNI o documento.</summary>
        Cliente? GetByDni(int? dni);

        /// <summary>Recupera un cliente específico por su clave primaria.</summary>
        Cliente? GetById(Guid id);

        /// <summary>Actualiza los datos del perfil de un cliente en la base de datos.</summary>
        void Update(Cliente cliente);
    }
}