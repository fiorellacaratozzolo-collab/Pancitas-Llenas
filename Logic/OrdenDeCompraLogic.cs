using AutoMapper;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Interfaces;
using DataAccess.Models;
using Logic.MappingProfiles;
using Microsoft.EntityFrameworkCore;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class OrdenDeCompraLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;

        public OrdenDeCompraLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Guid CrearOrdenDesdePedidoAgrupado(OrdenDePedido ordenDePedidoOrigen,List<DataAccess.Models.OrdenDeCompraDetalle> detallesOC,Guid idProveedor)
        {
            // 1. Mapeo de Cabecera (OP -> OC)
            var ordenDeCompra = _mapper.Map<OrdenDeCompra>(ordenDePedidoOrigen);
            ordenDeCompra.IdOrdenDeCompra = Guid.NewGuid();
            ordenDeCompra.IdProveedor = idProveedor;
            ordenDeCompra.FechaOc = DateTime.Today;
            ordenDeCompra.IdEstadoOc = 1;

            // 2. Enlazar detalles, calcular Total
            ordenDeCompra.OrdenDeCompraDetalles = detallesOC;
            ordenDeCompra.Total = detallesOC.Sum(d => d.Subtotal);

            _unitOfWork.OrdenDeCompras.Create(ordenDeCompra);

            // 3. Asignar FK a los detalles
            foreach (var detalle in ordenDeCompra.OrdenDeCompraDetalles)
            {
                detalle.IdOrdenDeCompra = ordenDeCompra.IdOrdenDeCompra;
            }
            _unitOfWork.OrdenDeCompraDetalles.AddRange(ordenDeCompra.OrdenDeCompraDetalles);

            return ordenDeCompra.IdOrdenDeCompra;
        }

        public List<OrdenDeCompraDetalleDTO> ObtenerDetallesPorOrden(Guid idOrdenCompra)
        {
            var detalles = _unitOfWork.OrdenDeCompraDetalles.GetByIdOrdenCompra(idOrdenCompra);
            return _mapper.Map<List<OrdenDeCompraDetalleDTO>>(detalles);
        }

        // --- CRUD BÁSICO ---

        public OrdenDeCompraDTO ObtenerPorId(Guid id)
        {
            var orden = _unitOfWork.OrdenDeCompras.GetAll()
        .AsQueryable()
        .Include(o => o.IdProveedorNavigation) //Carga el Proveedor
        .Include(o => o.OrdenDeCompraDetalles) // Carga Detalles
            .ThenInclude(d => d.IdProductoNavigation) //Carga Producto para Peso/Nombre
        .FirstOrDefault(o => o.IdOrdenDeCompra == id);

            return _mapper.Map<OrdenDeCompraDTO>(orden);
        }

        public List<OrdenDeCompraDTO> ObtenerTodas()
        {
            var ordenes = _unitOfWork.OrdenDeCompras.GetAll()
        .AsQueryable()
        .Include(o => o.IdProveedorNavigation) // 👈 Carga el Proveedor
        .ToList();
            return _mapper.Map<List<OrdenDeCompraDTO>>(ordenes);
        }

        // --- GESTIÓN DE ESTADO ---

        public void RechazarOrden(Guid ordenId)
        {
            var orden = _unitOfWork.OrdenDeCompras.GetById(ordenId);
            if (orden == null) throw new KeyNotFoundException();

            orden.IdEstadoOc = 3; // Rechazada
            _unitOfWork.OrdenDeCompras.Update(orden);
            _unitOfWork.Complete();
        }

        public void FinalizarOrden(Guid ordenId)
        {
            var orden = _unitOfWork.OrdenDeCompras.GetById(ordenId);
            if (orden == null) throw new KeyNotFoundException();

            orden.IdEstadoOc = 2; //Aprobada

            _unitOfWork.OrdenDeCompras.Update(orden);
            _unitOfWork.Complete();
        }    
    }
}