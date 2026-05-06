using DataAccess.Contexts;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Implementations.SqlServer
{
    /// <summary>
    /// Implementación concreta del repositorio para la cabecera de las órdenes de pedido interno.
    /// </summary>
    public class OrdenDePedidoRepository : IOrdenDePedidoRepository
    {
        private readonly PetShopDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio inyectando el contexto.
        /// </summary>
        public OrdenDePedidoRepository(PetShopDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registra una nueva orden de pedido en la base de datos.
        /// </summary>
        public Guid Create(OrdenDePedido orden)
        {
            _context.OrdenDePedidos.Add(orden);
            return orden.IdOrdenDePedido;
        }

        /// <summary>
        /// Recupera el historial completo de órdenes de pedido junto con su estado y detalles asociados.
        /// </summary>
        public List<OrdenDePedido> GetAll()
        {
            return _context.OrdenDePedidos
            .Include(op => op.OrdenDePedidoDetalles)
            .Include(op => op.IdEstadoOpNavigation)
            .ToList();
        }

        /// <summary>
        /// Recupera una orden específica con toda su jerarquía relacional mediante su identificador.
        /// </summary>
        public OrdenDePedido? GetById(Guid id)
        {
            return _context.OrdenDePedidos
            .Include(op => op.OrdenDePedidoDetalles)
            .Include(op => op.IdEstadoOpNavigation)
            .FirstOrDefault(op => op.IdOrdenDePedido == id);
        }

        /// <summary>
        /// Adjunta una entidad de orden desconectada y marca su estado como modificado para su actualización.
        /// </summary>
        public void Update(OrdenDePedido orden)
        {
            _context.OrdenDePedidos.Attach(orden);
            _context.Entry(orden).State = EntityState.Modified;
        }
    }
}
