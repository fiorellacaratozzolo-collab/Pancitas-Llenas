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
    public class SolicitudDePedidoRepository : ISolicitudDePedidoRepository
    {
        private readonly PetShopDbContext _context;

        public SolicitudDePedidoRepository(PetShopDbContext context)
        {
            _context = context;
        }

        public Guid Create(SolicitudDePedido solicitud)
        {
            _context.SolicitudDePedidos.Add(solicitud);
            return solicitud.IdSolicitudDePedido;
        }

        public List<SolicitudDePedido> GetAll()
        {
            return _context.SolicitudDePedidos
                .Include("SolicitudDePedidoDetalles")
                .Include("SolicitudDePedidoDetalles.IdProductoNavigation")
                .ToList();
        }

        public SolicitudDePedido? GetById(Guid id)
        {
            return _context.SolicitudDePedidos
                .Include("SolicitudDePedidoDetalles")
                .Include("SolicitudDePedidoDetalles.IdProductoNavigation")
                .FirstOrDefault(x => x.IdSolicitudDePedido == id);
        }
    }
}
