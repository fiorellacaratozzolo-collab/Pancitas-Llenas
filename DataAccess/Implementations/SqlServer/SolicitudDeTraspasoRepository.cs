using DataAccess.Contexts;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementations.SqlServer
{
    /// <summary>
    /// Implementación concreta del repositorio para la cabecera de las solicitudes de traspaso de inventario.
    /// </summary>
    public class SolicitudDeTraspasoRepository : ISolicitudDeTraspasoRepository
    {
        private readonly PetShopDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio inyectando el contexto de datos.
        /// </summary>
        public SolicitudDeTraspasoRepository(PetShopDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Inserta una nueva cabecera de solicitud de traspaso y retorna su identificador generado.
        /// </summary>
        public Guid Create(SolicitudDeTraspasoDeProducto solicitud)
        {
            solicitud.IdSolicitudDeTraspasoDeProductos = Guid.NewGuid();
            _context.SolicitudDeTraspasoDeProductos.Add(solicitud);
            return solicitud.IdSolicitudDeTraspasoDeProductos;
        }

        /// <summary>
        /// Recupera el historial completo de solicitudes de traspaso incluyendo sus propiedades de navegación.
        /// </summary>
        public List<SolicitudDeTraspasoDeProducto> GetAll()
        {
            return _context.SolicitudDeTraspasoDeProductos
            .Include(s => s.IdSucursalOrigenNavigation)
            .Include(s => s.IdSucursalDestinoNavigation)
            .Include(s => s.SolicitudDeTraspasoDeProductosDetalles)
                .ThenInclude(d => d.IdProductoNavigation)
            .ToList();
        }

        /// <summary>
        /// Obtiene una solicitud específica a partir de su identificador único.
        /// </summary>
        public SolicitudDeTraspasoDeProducto GetById(Guid idSolicitud)
        {
            return _context.SolicitudDeTraspasoDeProductos.Find(idSolicitud);
        }

        /// <summary>
        /// Filtra y obtiene todas las solicitudes emitidas desde una sucursal origen determinada.
        /// </summary>
        public List<SolicitudDeTraspasoDeProducto> GetTodasPorSucursalOrigen(Guid idSucursal)
        {
            return _context.SolicitudDeTraspasoDeProductos
                .Include(s => s.IdSucursalOrigenNavigation)
                .Include(s => s.IdSucursalDestinoNavigation)
                .Where(s => s.IdSucursalOrigen == idSucursal)
                .ToList();
        }

        /// <summary>
        /// Actualiza la información y/o estado de una solicitud de traspaso en memoria.
        /// </summary>
        public void Update(SolicitudDeTraspasoDeProducto solicitud)
        {
            _context.SolicitudDeTraspasoDeProductos.Update(solicitud);
        }
    }
}

