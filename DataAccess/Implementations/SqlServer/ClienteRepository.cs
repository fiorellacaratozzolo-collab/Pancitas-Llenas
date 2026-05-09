using DataAccess.Contexts;
using DataAccess.Interfaces;
using DataAccess.Models;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementations.SqlServer
{
    /// <summary>
    /// Implementación concreta del repositorio para la gestión persistente del directorio de clientes en SQL Server.
    /// </summary>
    public class ClienteRepository : IClienteRepository
    {
        private readonly PetShopDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio inyectando el contexto de base de datos.
        /// </summary>
        public ClienteRepository(PetShopDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Inserta un nuevo registro de cliente en la base de datos previa validación de nulidad.
        /// </summary>
        public Guid Create(Cliente cliente)
        {
            if (cliente == null) throw new ArgumentNullException(nameof(cliente), "El cliente no puede ser nulo.");

            if (cliente.IdCliente == Guid.Empty)
            {
                cliente.IdCliente = Guid.NewGuid();
            }

            _context.Clientes.Add(cliente);
            return cliente.IdCliente;
        }

        /// <summary>
        /// Obtiene la lista completa de todos los clientes registrados.
        /// </summary>
        public List<Cliente> GetAll()
        {
            return _context.Clientes.ToList();
        }

        /// <summary>
        /// Filtra y obtiene los clientes según su categoría o clasificación.
        /// </summary>
        public List<Cliente> GetByTipoCliente(int IdTipoCliente)
        {
            return _context.Clientes.Where(c => c.IdTipoCliente == IdTipoCliente).ToList();
        }

        /// <summary>
        /// Realiza un Borrado Lógico del cliente (Activo = false).
        /// </summary>
        public void Delete(Guid id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente != null)
            {
                cliente.Activo = false;
                _context.Clientes.Update(cliente);
            }
        }

        /// <summary>
        /// Reactiva un cliente previamente deshabilitado (Activo = true).
        /// </summary>
        public void Habilitar(Guid id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente != null)
            {
                cliente.Activo = true;
                _context.Clientes.Update(cliente);
            }
        }

        /// <summary>
        /// Realiza una búsqueda exacta de un cliente utilizando su número de documento de identidad (DNI).
        /// </summary>
        public Cliente? GetByDni(int? dni)
        {
            return _context.Clientes.FirstOrDefault(c => c.Dni == dni);
        }

        /// <summary>
        /// Actualiza los datos de un cliente existente reemplazando sus propiedades en el contexto.
        /// </summary>
        public void Update(Cliente cliente)
        {
            if (cliente == null) throw new ArgumentNullException(nameof(cliente));

            var clienteDb = _context.Clientes.Find(cliente.IdCliente);
            if (clienteDb != null)
            {
                _context.Entry(clienteDb).CurrentValues.SetValues(cliente);
            }
        }

        /// <summary>
        /// Realiza una búsqueda exacta de un cliente utilizando su número de ID.
        /// </summary>
        public Cliente? GetById(Guid id)
        {
            return _context.Clientes.Find(id);
        }
    }
}
