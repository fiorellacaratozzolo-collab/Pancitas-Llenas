using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Facade
{
    public class ClienteService
    {
        private readonly ClienteLogic _clienteLogic;

        public ClienteService()
        {
            _clienteLogic = new ClienteLogic();
        }

        public Guid CreateCliente(Cliente cliente)
        {
            return _clienteLogic.CreateCliente(cliente);
        }
        public List<Cliente> GetAllClientes()
        {
            return _clienteLogic.ObtenerTodosLosClientes();
        }
        public List<Cliente> BuscarClientesPorTipo(int idTipoCliente)
        {
            return _clienteLogic.BuscarClientesPorTipo(idTipoCliente);
        }

        public void DeshabilitarCliente(Guid id)
        {
            _clienteLogic.DeshabilitarCliente(id);
        }

    }
}