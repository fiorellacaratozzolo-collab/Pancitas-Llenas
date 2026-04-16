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
    public class ClienteRepository : IClienteRepository
    {
        private readonly PetShopDbContext _context;

        public ClienteRepository(PetShopDbContext context)
        {
            _context = context;
        }

        public Guid Create(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente), "El cliente no puede ser nulo.");
            }

            cliente.IdCliente = Guid.NewGuid();
            _context.Clientes.Add(cliente);

            return cliente.IdCliente;
        }

        public List<Cliente> GetAll()
        {
            return _context.Clientes.ToList();
        }

        public List<Cliente> GetByTipoCliente(int IdTipoCliente)
        {
            return _context.Clientes
                           .Where(c => c.IdTipoCliente == IdTipoCliente)
                           .ToList();
        }

        public void Delete(Guid id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
        }

        public Cliente? GetByDni(int? dni)
        {
            return _context.Clientes.FirstOrDefault(c => c.Dni == dni);
        }

        public void Update(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));
            _context.Clientes.Update(cliente);
        }
    }
}
