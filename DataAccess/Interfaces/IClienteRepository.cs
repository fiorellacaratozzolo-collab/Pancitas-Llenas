using DataAccess.Models;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define el contrato de persistencia para gestionar las operaciones CRUD de la entidad Cliente.
    /// </summary>
    public interface IClienteRepository
    {
        /// <summary>Registra un nuevo cliente y retorna su identificador único.</summary>
        Guid Create(Cliente cliente);

        /// <summary>Elimina o deshabilita el registro de un cliente existente.</summary>
        void Delete(Guid id);

        /// <summary>Obtiene la lista completa de clientes registrados.</summary>
        List<Cliente> GetAll();

        /// <summary>Filtra y obtiene los clientes según su tipo o condición fiscal.</summary>
        List<Cliente> GetByTipoCliente(int IdTipoCliente);

        /// <summary>Busca un cliente específico utilizando su número de DNI o documento.</summary>
        Cliente? GetByDni(int? dni);

        /// <summary>Actualiza los datos del perfil de un cliente en la base de datos.</summary>
        void Update(Cliente cliente);
    }
}