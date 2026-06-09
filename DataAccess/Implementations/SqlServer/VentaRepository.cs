using DataAccess.Contexts;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementations.SqlServer
{
    /// <summary>
    /// Implementación concreta del repositorio de ventas utilizando Entity Framework para SQL Server.
    /// </summary>
    public class VentaRepository : IVentaRepository
    {
        private readonly PetShopDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio inyectando el contexto de base de datos.
        /// </summary>
        public VentaRepository(PetShopDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Persiste una nueva cabecera de venta en la base de datos generando un nuevo identificador.
        /// </summary>
        public Guid Create(Ventum venta)
        {
            venta.IdVenta = Guid.NewGuid();
            _context.Venta.Add(venta);
            return venta.IdVenta;
        }

        /// <summary>
        /// Recupera el historial completo de ventas incluyendo la información del cliente asociado.
        /// </summary>
        public List<Ventum> GetAll()
        {
            return _context.Venta
                .Include(v => v.IdClienteNavigation)
                .ToList();
        }

        /// <summary>
        /// Elimina un registro de venta de la base de datos a partir de su identificador.
        /// </summary>
        public void Delete(Guid id)
        {
            var ventaAEliminar = _context.Venta
                                         .Include(v => v.VentaDetalles)
                                         .FirstOrDefault(v => v.IdVenta == id);
            if (ventaAEliminar != null)
            {
                if (ventaAEliminar.VentaDetalles.Any())
                {
                    _context.VentaDetalles.RemoveRange(ventaAEliminar.VentaDetalles);
                }
                _context.Venta.Remove(ventaAEliminar);
            }
        }

        /// <summary>
        /// Filtra y devuelve la lista de ventas correspondientes a una sucursal específica, incluyendo los datos del cliente.
        /// </summary>
        public List<Ventum> GetBySucursal(Guid idSucursal)
        {
            return _context.Venta
                .Include(v => v.IdClienteNavigation)
                .Where(v => v.IdSucursal == idSucursal)
                .ToList();
        }
    }
}