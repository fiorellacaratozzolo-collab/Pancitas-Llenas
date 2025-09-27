using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Implementations.SqlServer;
using DataAccess.Models;

namespace Logic
{
    public class ClienteLogic
    {
        private readonly ClienteRepository _clienteRepository;

        public ClienteLogic()
        {
            _clienteRepository = new ClienteRepository();
        }

        public Guid CreateCliente(Cliente cliente)
        {
            return _clienteRepository.Create(cliente);
        }
        public List<Cliente> ObtenerTodosLosClientes()
        {
            // Delega la operación al repositorio
            return _clienteRepository.GetAll();
        }

        public List<Cliente> BuscarClientesPorTipo(int idTipoCliente)
        {
            // Retorna la lista filtrada por el repositorio.
            return _clienteRepository.GetByTipoCliente(idTipoCliente);
        }

        public void DeshabilitarCliente(Guid id)
        {
            _clienteRepository.Delete(id);
        }

    }
}
