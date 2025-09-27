using Dal.EntityFramework;
using Dal.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Implementations.SqlServer
{
    public class ClienteRepository
    {
        private readonly PetShopDBContext _context;

        public ClienteRepository(PetShopDBContext context)
        {
            _context = context;
        }

        public List<Cliente> GetAll()
        {
            return _context.Clientes.ToList();
        }

        public Cliente GetById(Guid id)
        {
            return _context.Clientes.Find(id);
        }


    }
}
