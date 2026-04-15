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
    public class SolicitudDePedidoDetalleRepository : ISolicitudDePedidoDetalleRepository
    {
        private readonly PetShopDbContext _context;

        public SolicitudDePedidoDetalleRepository(PetShopDbContext context)
        {
            _context = context;
        }

        public void AddRange(IEnumerable<SolicitudDePedidoDetalle> detalles)
        {
            _context.SolicitudDePedidoDetalles.AddRange(detalles);
        }

        public List<SolicitudDePedidoDetalle> GetByIdSolicitud(Guid idSolicitud)
        {
            return _context.SolicitudDePedidoDetalles 
                .Include(d => d.IdProductoNavigation) 
                .Where(d => d.IdSolicitudDePedido == idSolicitud)
                .ToList();
        }
    }
}
