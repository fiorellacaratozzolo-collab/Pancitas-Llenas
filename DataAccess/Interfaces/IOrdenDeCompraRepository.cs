using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define el contrato de persistencia para las cabeceras de las órdenes de compra externas.
    /// </summary>
    public interface IOrdenDeCompraRepository
    {
        /// <summary>Crea una nueva orden de compra y retorna su identificador único.</summary>
        Guid Create(OrdenDeCompra orden);

        /// <summary>Recupera una orden de compra específica mediante su identificador.</summary>
        OrdenDeCompra? GetById(Guid id);

        /// <summary>Obtiene el listado completo de órdenes de compra del sistema.</summary>
        List<OrdenDeCompra> GetAll();

        /// <summary>Actualiza los datos o el estado de una orden de compra existente.</summary>
        void Update(OrdenDeCompra orden);
    }
}