using AutoMapper;
using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Models;
using Logic.MappingProfiles;
using ModelsDTO;

namespace Logic
{
    /// <summary>
    /// Gestiona el alta, la validación de duplicados, la actualización y la consulta del directorio de clientes del sistema.
    /// </summary>
    public class ClienteLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;

        /// <summary>
        /// Inicializa una nueva instancia de la lógica de clientes.
        /// </summary>
        public ClienteLogic()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Crea un nuevo cliente o reactiva uno inactivo si coincide su DNI.
        /// </summary>
        public Guid CreateCliente(ClienteDTO clienteDTO)
        {
            if (string.IsNullOrWhiteSpace(clienteDTO.NombreCliente)) throw new ArgumentException("El nombre del cliente es obligatorio.");
            if (clienteDTO.Dni == 0) throw new ArgumentException("El DNI/Identificador del cliente es obligatorio.");

            Cliente? clienteExistente = _unitOfWork.Clientes.GetByDni(clienteDTO.Dni);

            if (clienteExistente != null)
            {
                if (clienteExistente.Activo == false)
                {
                    // Si el cliente existe pero estaba "borrado", lo reactivamos y actualizamos sus datos
                    clienteExistente.Activo = true;
                    clienteExistente.NombreCliente = clienteDTO.NombreCliente;
                    clienteExistente.IdTipoCliente = clienteDTO.IdTipoCliente;

                    _unitOfWork.Clientes.Update(clienteExistente);
                    _unitOfWork.Complete();

                    return clienteExistente.IdCliente;
                }
                else
                {
                    throw new InvalidOperationException(string.Format("Ya existe un cliente ACTIVO con el DNI/Identificador {0}.", clienteDTO.Dni));
                }
            }

            // Si no existe, procedemos con el alta normal
            var cliente = _mapper.Map<Cliente>(clienteDTO);
            cliente.Activo = true;

            Guid idCliente = _unitOfWork.Clientes.Create(cliente);
            _unitOfWork.Complete();

            return idCliente;
        }

        /// <summary>
        /// Obtiene solo los clientes que están actualmente ACTIVOS.
        /// </summary>
        public List<ClienteDTO> ObtenerActivos()
        {
            List<Cliente> clientes = _unitOfWork.Clientes.GetAll().Where(c => c.Activo == true).ToList();
            return _mapper.Map<List<ClienteDTO>>(clientes);
        }

        /// <summary>
        /// Obtiene solo los clientes que están actualmente DESHABILITADOS.
        /// </summary>
        public List<ClienteDTO> ObtenerDeshabilitados()
        {
            List<Cliente> clientes = _unitOfWork.Clientes.GetAll().Where(c => c.Activo == false).ToList();
            return _mapper.Map<List<ClienteDTO>>(clientes);
        }

        /// <summary>
        /// Obtiene y mapea el listado completo de todos los clientes activos.
        /// </summary>
        public List<ClienteDTO> ObtenerTodosLosClientes()
        {
            List<Cliente> clientes = _unitOfWork.Clientes.GetAll();
            return _mapper.Map<List<ClienteDTO>>(clientes);
        }

        /// <summary>
        /// Filtra y devuelve la lista de clientes que coinciden con una categoría o condición fiscal específica.
        /// </summary>
        public List<ClienteDTO> BuscarClientesPorTipo(int idTipoCliente)
        {
            List<Cliente> clientes = _unitOfWork.Clientes.GetByTipoCliente(idTipoCliente);
            return _mapper.Map<List<ClienteDTO>>(clientes);
        }

        /// <summary>
        /// Deshabilita o elimina lógicamente un registro de cliente del sistema.
        /// </summary>
        public void DeshabilitarCliente(Guid id)
        {
            _unitOfWork.Clientes.Delete(id);
            _unitOfWork.Complete();
        }

        /// <summary>
        /// Habilita lógicamente un registro de cliente del sistema.
        /// </summary>
        public void HabilitarCliente(Guid id)
        {
            _unitOfWork.Clientes.Habilitar(id);
            _unitOfWork.Complete();
        }

        /// <summary>
        /// Aplica y confirma las modificaciones realizadas sobre el perfil de un cliente existente.
        /// </summary>
        public void UpdateCliente(ClienteDTO clienteDTO)
        {
            if (clienteDTO == null) throw new ArgumentNullException(nameof(clienteDTO));

            Cliente cliente = _mapper.Map<Cliente>(clienteDTO);
            _unitOfWork.Clientes.Update(cliente);
            _unitOfWork.Complete();
        }
    }
}