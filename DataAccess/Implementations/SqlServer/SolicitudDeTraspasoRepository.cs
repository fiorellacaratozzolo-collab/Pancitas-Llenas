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
    public class SolicitudDeTraspasoRepository : ISolicitudDeTraspasoRepository
    {
        private readonly PetShopDbContext _context;

        public SolicitudDeTraspasoRepository(PetShopDbContext context)
        {
            _context = context;
        }

        public Guid Create(SolicitudDeTraspasoDeProducto solicitud)
        {
            _context.SolicitudDeTraspasoDeProductos.Add(solicitud);
            return solicitud.IdSolicitudDeTraspasoDeProductos;
        }

        public List<SolicitudDeTraspasoDeProducto> GetAll()
        {
            return _context.SolicitudDeTraspasoDeProductos
                .Include(s => s.IdEstadoStpNavigation)
                .Include(s => s.IdSucursalOrigenNavigation)
                .Include(s => s.IdSucursalDestinoNavigation)
                .Include(s => s.SolicitudDeTraspasoDeProductosDetalles)
                .ToList();
        }

        public SolicitudDeTraspasoDeProducto? GetById(Guid id)
        {
            return _context.SolicitudDeTraspasoDeProductos
                .Include(s => s.IdEstadoStpNavigation)
                .Include(s => s.IdSucursalOrigenNavigation)
                .Include(s => s.IdSucursalDestinoNavigation)
                .Include(s => s.SolicitudDeTraspasoDeProductosDetalles)
                    .ThenInclude(d => d.IdProductoNavigation)
                .FirstOrDefault(s => s.IdSolicitudDeTraspasoDeProductos == id);
        }

        public void Update(SolicitudDeTraspasoDeProducto solicitud)
        {
            _context.SolicitudDeTraspasoDeProductos.Attach(solicitud);
            _context.Entry(solicitud).State = EntityState.Modified;
        }
    }
}

