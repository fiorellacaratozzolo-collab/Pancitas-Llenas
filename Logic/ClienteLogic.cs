using AutoMapper;
using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Models;
using Logic.MappingProfiles;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Crea un nuevo registro de cliente validando la integridad de sus datos y garantizando que su documento de identidad sea único en la base de datos.
        /// </summary>
        public Guid CreateCliente(ClienteDTO clienteDTO)
        {
            var cliente = _mapper.Map<Cliente>(clienteDTO);

            if (string.IsNullOrWhiteSpace(cliente.NombreCliente))
            {
                throw new ArgumentException("El nombre del cliente es obligatorio.");
            }
            if (cliente.Dni == 0)
            {
                throw new ArgumentException("El DNI/Identificador del cliente es obligatorio.");
            }

            Cliente? clienteExistente = _unitOfWork.Clientes.GetByDni(cliente.Dni);

            if (clienteExistente != null)
            {
                throw new InvalidOperationException(string.Format("No se puede crear el cliente: Ya existe un cliente con el DNI/Identificador {0}.", cliente.Dni));
            }

            Guid idCliente = _unitOfWork.Clientes.Create(cliente);
            _unitOfWork.Complete();

            return idCliente;
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
        /// Aplica y confirma las modificaciones realizadas sobre el perfil de un cliente existente.
        /// </summary>
        public void UpdateCliente(ClienteDTO clienteDTO)
        {
            if (clienteDTO == null)
                throw new ArgumentNullException(nameof(clienteDTO));

            Cliente cliente = _mapper.Map<Cliente>(clienteDTO);
            _unitOfWork.Clientes.Update(cliente);
            _unitOfWork.Complete();
        }
    }
}