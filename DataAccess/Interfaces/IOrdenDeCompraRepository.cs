using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IOrdenDeCompraRepository
    {
        Guid Create(OrdenDeCompra orden);
        OrdenDeCompra? GetById(Guid id);
        List<OrdenDeCompra> GetAll();
        void Update(OrdenDeCompra orden); // Para cambio de estado
    }
}
