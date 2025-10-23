using DataAccess.Models;
using ModelsDTO;
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

        public Guid CreateCliente(ClienteDTO clienteDTO)
        {
            return _clienteLogic.CreateCliente(clienteDTO);
        }
        public List<ClienteDTO> GetAllClientes()
        {
            return _clienteLogic.ObtenerTodosLosClientes();
        }
        public List<ClienteDTO> BuscarClientesPorTipo(int idTipoCliente)
        {
            return _clienteLogic.BuscarClientesPorTipo(idTipoCliente);
        }

        public void DeshabilitarCliente(Guid id)
        {
            _clienteLogic.DeshabilitarCliente(id);
        }

    }
}