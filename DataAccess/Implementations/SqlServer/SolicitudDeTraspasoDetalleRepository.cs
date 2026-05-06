using DataAccess.Contexts;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementations.SqlServer
{
    /// <summary>
    /// Implementación concreta del repositorio para los renglones de detalle de las solicitudes de traspaso.
    /// </summary>
    public class SolicitudDeTraspasoDetalleRepository : ISolicitudDeTraspasoDetalleRepository
    {
        private readonly PetShopDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio.
        /// </summary>
        public SolicitudDeTraspasoDetalleRepository(PetShopDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Inserta un nuevo renglón de detalle para un traspaso y genera su identificador.
        /// </summary>
        public void Create(SolicitudDeTraspasoDeProductosDetalle detalle)
        {
            detalle.IdSolicitudDeTraspasoDeProductosDetalle = Guid.NewGuid();
            _context.SolicitudDeTraspasoDeProductosDetalles.Add(detalle);
        }

        /// <summary>
        /// Obtiene todos los renglones de detalle de traspasos almacenados en el sistema.
        /// </summary>
        public List<SolicitudDeTraspasoDeProductosDetalle> GetAll()
        {
            return _context.SolicitudDeTraspasoDeProductosDetalles.ToList();
        }

        /// <summary>
        /// Recupera los detalles correspondientes a una solicitud de traspaso específica.
        /// </summary>
        public List<SolicitudDeTraspasoDeProductosDetalle> GetByIdSolicitud(Guid idSolicitud)
        {
            return _context.SolicitudDeTraspasoDeProductosDetalles
                .Include(d => d.IdProductoNavigation)
                .Where(d => d.IdSolicitudDeTraspasoDeProductos == idSolicitud)
                .ToList();
        }
    }
}