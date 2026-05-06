using DataAccess.Contexts;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementations.SqlServer
{
    /// <summary>
    /// Implementación concreta del repositorio para los renglones de detalle de las solicitudes de pedido.
    /// </summary>
    public class SolicitudDePedidoDetalleRepository : ISolicitudDePedidoDetalleRepository
    {
        private readonly PetShopDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio.
        /// </summary>
        public SolicitudDePedidoDetalleRepository(PetShopDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Inserta de forma masiva una colección de renglones de detalle en el contexto.
        /// </summary>
        public void AddRange(IEnumerable<SolicitudDePedidoDetalle> detalles)
        {
            _context.SolicitudDePedidoDetalles.AddRange(detalles);
        }

        /// <summary>
        /// Recupera la lista de detalles pertenecientes a una solicitud de pedido en particular.
        /// </summary>
        public List<SolicitudDePedidoDetalle> GetByIdSolicitud(Guid idSolicitud)
        {
            return _context.SolicitudDePedidoDetalles
                .Include(d => d.IdProductoNavigation)
                .Where(d => d.IdSolicitudDePedido == idSolicitud)
                .ToList();
        }
    }
}
