using DataAccess.Models;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define el contrato de persistencia para las cabeceras de las órdenes de pedido.
    /// </summary>
    public interface IOrdenDePedidoRepository
    {
        /// <summary>Crea una nueva orden de pedido y retorna su identificador único.</summary>
        Guid Create(OrdenDePedido orden);

        /// <summary>Recupera una orden de pedido específica mediante su identificador.</summary>
        OrdenDePedido? GetById(Guid id);

        /// <summary>Obtiene el listado completo de órdenes de pedido registradas.</summary>
        List<OrdenDePedido> GetAll();

        /// <summary>Actualiza la información o el estado de una orden de pedido existente.</summary>
        void Update(OrdenDePedido orden);
    }
}