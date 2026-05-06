using DataAccess.Models;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Facade
{
    /// <summary>
    /// Fachada que encapsula la lógica de administración y mantenimiento del directorio de clientes.
    /// </summary>
    public class ClienteService
    {
        private readonly ClienteLogic _clienteLogic;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de clientes.
        /// </summary>
        public ClienteService()
        {
            _clienteLogic = new ClienteLogic();
        }

        /// <summary>
        /// Registra un nuevo cliente en la plataforma validando su unicidad.
        /// </summary>
        public Guid CreateCliente(ClienteDTO clienteDTO)
        {
            return _clienteLogic.CreateCliente(clienteDTO);
        }

        /// <summary>
        /// Recupera el listado completo de todos los clientes activos.
        /// </summary>
        public List<ClienteDTO> GetAllClientes()
        {
            return _clienteLogic.ObtenerTodosLosClientes();
        }

        /// <summary>
        /// Filtra la base de clientes según su categoría o tipo fiscal.
        /// </summary>
        public List<ClienteDTO> BuscarClientesPorTipo(int idTipoCliente)
        {
            return _clienteLogic.BuscarClientesPorTipo(idTipoCliente);
        }

        /// <summary>
        /// Deshabilita el perfil de un cliente existente.
        /// </summary>
        public void DeshabilitarCliente(Guid id)
        {
            _clienteLogic.DeshabilitarCliente(id);
        }

        /// <summary>
        /// Actualiza la información personal y fiscal de un cliente.
        /// </summary>
        public void UpdateCliente(ClienteDTO clienteDTO)
        {
            _clienteLogic.UpdateCliente(clienteDTO);
        }
    }
}