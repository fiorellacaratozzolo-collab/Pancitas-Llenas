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
    /// Implementación concreta del repositorio para las cabeceras de las solicitudes de pedido interno.
    /// </summary>
    public class SolicitudDePedidoRepository : ISolicitudDePedidoRepository
    {
        private readonly PetShopDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio.
        /// </summary>
        public SolicitudDePedidoRepository(PetShopDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Persiste una nueva solicitud de pedido en la base de datos.
        /// </summary>
        public Guid Create(SolicitudDePedido solicitud)
        {
            _context.SolicitudDePedidos.Add(solicitud);
            return solicitud.IdSolicitudDePedido;
        }

        /// <summary>
        /// Recupera el historial de solicitudes de pedido incluyendo sus colecciones de detalle.
        /// </summary>
        public List<SolicitudDePedido> GetAll()
        {
            return _context.SolicitudDePedidos
                .Include("SolicitudDePedidoDetalles")
                .Include("SolicitudDePedidoDetalles.IdProductoNavigation")
                .ToList();
        }

        /// <summary>
        /// Recupera una solicitud específica asegurando la carga de sus datos de producto anidados.
        /// </summary>
        public SolicitudDePedido? GetById(Guid id)
        {
            return _context.SolicitudDePedidos
                .Include("SolicitudDePedidoDetalles")
                .Include("SolicitudDePedidoDetalles.IdProductoNavigation")
                .FirstOrDefault(x => x.IdSolicitudDePedido == id);
        }

        /// <summary>
        /// Recupera una solicitud específica forzando únicamente la inclusión de sus detalles directos.
        /// </summary>
        public SolicitudDePedido? GetByIdWithDetails(Guid id)
        {
            return _context.SolicitudDePedidos
                .Include(s => s.SolicitudDePedidoDetalles)
                .FirstOrDefault(x => x.IdSolicitudDePedido == id);
        }

        /// <summary>
        /// Actualiza la cabecera de la solicitud en memoria.
        /// </summary>
        public void Update(SolicitudDePedido solicitud)
        {
            _context.SolicitudDePedidos.Update(solicitud);
        }
    }
}
