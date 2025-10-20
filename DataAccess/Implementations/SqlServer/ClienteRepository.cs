using DataAccess.Contexts;
using DataAccess.Interfaces;
using DataAccess.Models;
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
            // La persistencia se hará a través del UoW.Complete()

            return cliente.IdCliente;
        }

        public List<Cliente> GetAll()
        {
            // Usa ToList() para ejecutar la consulta de Entity Framework
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
                // Borrado físico (NO RECOMENDADO para producción)
                _context.Clientes.Remove(cliente);

                // LÓGICA DE BORRADO LÓGICO (Ideal):
                // cliente.Activo = false; // Se necesita agregar 'public bool Activo { get; set; }' al modelo Cliente
                // _context.Entry(cliente).State = EntityState.Modified; 
            }
        }

        public Cliente? GetByDni(int? dni)
        {
            // Busca el primer cliente con el DNI proporcionado o devuelve null si no existe
            return _context.Clientes.FirstOrDefault(c => c.Dni == dni);
        }
    }
}
