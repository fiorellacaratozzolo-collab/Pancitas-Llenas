using DataAccess.Models;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define el contrato de persistencia para el ciclo de vida y gestión de estados de las solicitudes de pedido interno.
    /// </summary>
    public interface ISolicitudDePedidoRepository
    {
        /// <summary>Crea un nuevo registro de solicitud de pedido y devuelve su identificador único generado.</summary>
        Guid Create(SolicitudDePedido solicitud);

        /// <summary>Obtiene el historial completo de todas las solicitudes de pedido registradas.</summary>
        List<SolicitudDePedido> GetAll();

        /// <summary>Busca una solicitud específica por su identificador, pudiendo retornar un valor nulo si no se encuentra.</summary>
        SolicitudDePedido? GetById(Guid id);

        /// <summary>Actualiza la cabecera de la solicitud en la base de datos (generalmente para cambios de estado).</summary>
        void Update(SolicitudDePedido solicitud);

        /// <summary>Recupera una solicitud específica asegurando la inclusión (Join) de sus entidades relacionales de detalle.</summary>
        SolicitudDePedido? GetByIdWithDetails(Guid id);
    }
}