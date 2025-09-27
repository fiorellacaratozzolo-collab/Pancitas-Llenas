using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Models;
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

        public ClienteLogic()
        {
            _unitOfWork = new UnitOfWork();
        }

        public Guid CreateCliente(Cliente cliente)
        {
            //Validaciones
            if (string.IsNullOrWhiteSpace(cliente.NombreCliente))
            {
                throw new ArgumentException("El nombre del cliente es obligatorio.");
            }
            if (cliente.Dni == 0) // O cualquier validación para el campo DNI
            {
                throw new ArgumentException("El DNI/Identificador del cliente es obligatorio.");
            }
            // Verificamos si ya existe un cliente con ese DNI
            Cliente? clienteExistente = _unitOfWork.Clientes.GetByDni(cliente.Dni);

            if (clienteExistente != null)
            {
                throw new InvalidOperationException($"No se puede crear el cliente: Ya existe un cliente con el DNI/Identificador {cliente.Dni}.");
            }

            // Persistencia
            Guid idCliente = _unitOfWork.Clientes.Create(cliente);

            // Commit
            _unitOfWork.Complete();

            return idCliente;
        }

        public List<Cliente> ObtenerTodosLosClientes()
        {
            return _unitOfWork.Clientes.GetAll();
        }

        public List<Cliente> BuscarClientesPorTipo(int idTipoCliente)
        {
            return _unitOfWork.Clientes.GetByTipoCliente(idTipoCliente);
        }

        public void DeshabilitarCliente(Guid id)
        {
            _unitOfWork.Clientes.Delete(id);
            _unitOfWork.Complete(); // Confirma la eliminación
        }

    }
}
