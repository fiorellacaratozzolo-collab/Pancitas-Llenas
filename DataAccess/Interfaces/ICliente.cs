using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Interfaz para las operaciones de acceso a datos relacionadas con los clientes.
    /// Define los métodos CRUD básicos para trabajar con la entidad Cliente.
    /// </summary>
    public interface IClienteRepository
    {
        Guid Create(Cliente cliente);
        List<Cliente> GetByTipoCliente(int IdTipoCliente);
    }
}
