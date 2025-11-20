using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IOrdenDePedidoRepository
    {
        Guid Create(OrdenDePedido orden);
        OrdenDePedido? GetById(Guid id);
        List<OrdenDePedido> GetAll();
        void Update(OrdenDePedido orden);
    }
}
