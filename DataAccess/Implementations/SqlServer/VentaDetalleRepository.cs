using DataAccess.Contexts;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Implementations.SqlServer
{
    /// <summary>
    /// Implementación concreta del repositorio de detalles de venta utilizando Entity Framework.
    /// </summary>
    public class VentaDetalleRepository : IVentaDetalleRepository
    {
        private readonly PetShopDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio inyectando el contexto de base de datos.
        /// </summary>
        public VentaDetalleRepository(PetShopDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Persiste un nuevo renglón de detalle asociado a una venta.
        /// </summary>
        public void Create(VentaDetalle detalle)
        {
            detalle.IdVentaDetalle = Guid.NewGuid();
            _context.VentaDetalles.Add(detalle);
        }

        /// <summary>
        /// Recupera todos los renglones de detalles de ventas registrados en el sistema.
        /// </summary>
        public List<VentaDetalle> GetAll()
        {
            return _context.VentaDetalles.ToList();
        }
    }
}
