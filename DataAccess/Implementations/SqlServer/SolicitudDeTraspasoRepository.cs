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
            solicitud.IdSolicitudDeTraspasoDeProductos = Guid.NewGuid();

            _context.SolicitudDeTraspasoDeProductos.Add(solicitud);

            return solicitud.IdSolicitudDeTraspasoDeProductos;
        }

        public List<SolicitudDeTraspasoDeProducto> GetAll()
        {
            return _context.SolicitudDeTraspasoDeProductos
            .Include(s => s.IdSucursalOrigenNavigation)
            .Include(s => s.IdSucursalDestinoNavigation)
            .Include(s => s.SolicitudDeTraspasoDeProductosDetalles)
                .ThenInclude(d => d.IdProductoNavigation) 
            .ToList();
        }

        public SolicitudDeTraspasoDeProducto GetById(Guid idSolicitud)
        {
            return _context.SolicitudDeTraspasoDeProductos.Find(idSolicitud);
        }

        public List<SolicitudDeTraspasoDeProducto> GetTodasPorSucursalOrigen(Guid idSucursal)
        {
            return _context.SolicitudDeTraspasoDeProductos
                .Include(s => s.IdSucursalOrigenNavigation)
                .Include(s => s.IdSucursalDestinoNavigation)
                .Where(s => s.IdSucursalOrigen == idSucursal)
                .ToList();
        }

        public void Update(SolicitudDeTraspasoDeProducto solicitud)
        {
            _context.SolicitudDeTraspasoDeProductos.Update(solicitud);
        }
    }
}

