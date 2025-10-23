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
    public class ClienteLogic
    {
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;

        
        public ClienteLogic()
        {
            _unitOfWork = new UnitOfWork();
        }

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
                throw new InvalidOperationException($"No se puede crear el cliente: Ya existe un cliente con el DNI/Identificador {cliente.Dni}.");
            }

            Guid idCliente = _unitOfWork.Clientes.Create(cliente);
            _unitOfWork.Complete();

            return idCliente;
        }

        public List<ClienteDTO> ObtenerTodosLosClientes()
        {

            List<Cliente> clientes = _unitOfWork.Clientes.GetAll();
            return _mapper.Map<List<ClienteDTO>>(clientes);
        }

        public List<ClienteDTO> BuscarClientesPorTipo(int idTipoCliente)
        {
            List<Cliente> clientes = _unitOfWork.Clientes.GetByTipoCliente(idTipoCliente);
            return _mapper.Map<List<ClienteDTO>>(clientes);
        }

        public void DeshabilitarCliente(Guid id)
        {
            _unitOfWork.Clientes.Delete(id);
            _unitOfWork.Complete();
        }
    }
}