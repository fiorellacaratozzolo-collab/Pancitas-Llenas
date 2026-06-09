using DataAccess.Contexts;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementations.SqlServer
{
    /// <summary>
    /// Implementación concreta del repositorio para las cabeceras de las órdenes de compra emitidas a proveedores externos.
    /// </summary>
    internal class OrdenDeCompraRepository : IOrdenDeCompraRepository
    {
        private readonly PetShopDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio inyectando el contexto de datos.
        /// </summary>
        public OrdenDeCompraRepository(PetShopDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registra una nueva orden de compra en el sistema.
        /// </summary>
        public Guid Create(OrdenDeCompra orden)
        {
            _context.OrdenDeCompras.Add(orden);
            return orden.IdOrdenDeCompra;
        }

        /// <summary>
        /// Recupera el historial completo de órdenes de compra incluyendo la información del proveedor destinatario.
        /// </summary>
        public List<OrdenDeCompra> GetAll()
        {
            return _context.OrdenDeCompras
                .Include(oc => oc.IdProveedorNavigation)
                .ToList();
        }

        /// <summary>
        /// Obtiene una orden de compra específica con todo su árbol de relaciones (proveedor, detalles y productos) cargado.
        /// </summary>
        public OrdenDeCompra? GetById(Guid id)
        {
            return _context.OrdenDeCompras
                .Include(oc => oc.IdProveedorNavigation)
                .Include(oc => oc.OrdenDeCompraDetalles)
                .ThenInclude(detalle => detalle.IdProductoNavigation)
                .FirstOrDefault(oc => oc.IdOrdenDeCompra == id);
        }

        /// <summary>
        /// Marca la entidad desconectada como modificada para su persistencia en el próximo Commit.
        /// </summary>
        public void Update(OrdenDeCompra orden)
        {
            _context.OrdenDeCompras.Attach(orden);
            _context.Entry(orden).State = EntityState.Modified;
        }
    }
}
