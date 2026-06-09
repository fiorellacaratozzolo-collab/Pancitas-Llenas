using AutoMapper;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Models;
using Logic.MappingProfiles;
using Microsoft.EntityFrameworkCore;
using ModelsDTO;

namespace Logic
{
    /// <summary>
    /// Gestiona la creación, consulta y transiciones de estado de las órdenes de compra.
    /// </summary>
    public class OrdenDeCompraLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;

        /// <summary>
        /// Inicializa una nueva instancia de la lógica con un contexto de trabajo inyectado para mantener la atomicidad de las operaciones conjuntas.
        /// </summary>
        public OrdenDeCompraLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Genera una orden de compra y sus detalles a partir de un pedido aprobado, agrupado previamente por proveedor.
        /// </summary>
        public Guid CrearOrdenDesdePedidoAgrupado(OrdenDePedido ordenDePedidoOrigen, List<DataAccess.Models.OrdenDeCompraDetalle> detallesOC, Guid idProveedor)
        {
            var ordenDeCompra = _mapper.Map<OrdenDeCompra>(ordenDePedidoOrigen);
            ordenDeCompra.IdOrdenDeCompra = Guid.NewGuid();
            ordenDeCompra.IdProveedor = idProveedor;
            ordenDeCompra.FechaOc = DateTime.Today;
            ordenDeCompra.IdEstadoOc = 1;

            ordenDeCompra.OrdenDeCompraDetalles = detallesOC;
            ordenDeCompra.Total = detallesOC.Sum(d => d.Subtotal);

            _unitOfWork.OrdenDeCompras.Create(ordenDeCompra);

            foreach (var detalle in ordenDeCompra.OrdenDeCompraDetalles)
            {
                detalle.IdOrdenDeCompra = ordenDeCompra.IdOrdenDeCompra;
            }
            _unitOfWork.OrdenDeCompraDetalles.AddRange(ordenDeCompra.OrdenDeCompraDetalles);

            return ordenDeCompra.IdOrdenDeCompra;
        }

        /// <summary>
        /// Obtiene la lista de detalles vinculados a una orden de compra específica.
        /// </summary>
        public List<OrdenDeCompraDetalleDTO> ObtenerDetallesPorOrden(Guid idOrdenCompra)
        {
            var detalles = _unitOfWork.OrdenDeCompraDetalles.GetByIdOrdenCompra(idOrdenCompra);
            return _mapper.Map<List<OrdenDeCompraDetalleDTO>>(detalles);
        }

        /// <summary>
        /// Obtiene una orden de compra completa, incluyendo sus relaciones directas (proveedor, detalles, productos y estado), a partir de su identificador.
        /// </summary>
        public OrdenDeCompraDTO ObtenerPorId(Guid id)
        {
            var orden = _unitOfWork.OrdenDeCompras.GetAll()
                .AsQueryable()
                .Include(o => o.IdProveedorNavigation)
                .Include(o => o.OrdenDeCompraDetalles)
                    .ThenInclude(d => d.IdProductoNavigation)
                .Include(o => o.IdEstadoOcNavigation)
                .FirstOrDefault(o => o.IdOrdenDeCompra == id);

            return _mapper.Map<OrdenDeCompraDTO>(orden);
        }

        /// <summary>
        /// Obtiene el catálogo completo de órdenes de compra con su información resumida de proveedor y estado.
        /// </summary>
        public List<OrdenDeCompraDTO> ObtenerTodas()
        {
            var ordenes = _unitOfWork.OrdenDeCompras.GetAll()
                .AsQueryable()
                .Include(o => o.IdProveedorNavigation)
                .Include(o => o.IdEstadoOcNavigation)
                .ToList();

            return _mapper.Map<List<OrdenDeCompraDTO>>(ordenes);
        }

        /// <summary>
        /// Actualiza el estado de una orden de compra a Rechazada.
        /// </summary>
        public void RechazarOrden(Guid ordenId)
        {
            var orden = _unitOfWork.OrdenDeCompras.GetById(ordenId);
            if (orden == null) throw new KeyNotFoundException();

            orden.IdEstadoOc = 3;
            _unitOfWork.OrdenDeCompras.Update(orden);
            _unitOfWork.Complete();
        }

        /// <summary>
        /// Actualiza el estado de una orden de compra a Aprobada o Finalizada.
        /// </summary>
        public void FinalizarOrden(Guid ordenId)
        {
            var orden = _unitOfWork.OrdenDeCompras.GetById(ordenId);
            if (orden == null) throw new KeyNotFoundException();

            orden.IdEstadoOc = 2;

            _unitOfWork.OrdenDeCompras.Update(orden);
            _unitOfWork.Complete();
        }
    }
}