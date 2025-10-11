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

        public Guid CreateCliente(Cliente cliente)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                //Validaciones
                if (string.IsNullOrWhiteSpace(cliente.NombreCliente))
                {
                    throw new ArgumentException("El nombre del cliente es obligatorio.");
                }
                if (cliente.Dni == 0)
                {
                    throw new ArgumentException("El DNI/Identificador del cliente es obligatorio.");
                }

                // Verificamos si ya existe un cliente con ese DNI
                Cliente? clienteExistente = unitOfWork.Clientes.GetByDni(cliente.Dni);

                if (clienteExistente != null)
                {
                    //(ClienteDniDuplicadoException)
                    throw new InvalidOperationException($"No se puede crear el cliente: Ya existe un cliente con el DNI/Identificador {cliente.Dni}.");
                }

                // Persistencia
                Guid idCliente = unitOfWork.Clientes.Create(cliente);

                // Commit
                unitOfWork.Complete();

                return idCliente;
            }
        }

        public List<Cliente> ObtenerTodosLosClientes()
        {

            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.Clientes.GetAll();
            }
        }

        public List<Cliente> BuscarClientesPorTipo(int idTipoCliente)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.Clientes.GetByTipoCliente(idTipoCliente);
            }
        }

        public void DeshabilitarCliente(Guid id)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                unitOfWork.Clientes.Delete(id);
                unitOfWork.Complete();
            }
        }
    }
}