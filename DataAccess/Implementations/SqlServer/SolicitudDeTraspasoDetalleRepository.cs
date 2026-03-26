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
    public class SolicitudDeTraspasoDetalleRepository : ISolicitudDeTraspasoDetalleRepository
    {
        private readonly PetShopDbContext _context;

        public SolicitudDeTraspasoDetalleRepository(PetShopDbContext context)
        {
            _context = context;
        }

        public void AddRange(List<SolicitudDeTraspasoDeProductosDetalle> detalles)
        {
            if (detalles != null && detalles.Any())
            {
                _context.SolicitudDeTraspasoDeProductosDetalles.AddRange(detalles);
            }
        }

        public List<SolicitudDeTraspasoDeProductosDetalle> GetBySolicitudId(Guid idSolicitud)
        {
            return _context.SolicitudDeTraspasoDeProductosDetalles
                .Include(d => d.IdProductoNavigation)
                .Where(d => d.IdSolicitudDeTraspasoDeProductos == idSolicitud)
                .ToList();
        }
    }
}
