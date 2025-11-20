using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
using ModelsDTO;
using System;
using System.Collections.Generic;

namespace Logic.Facade
{
    public class OrdenDePedidoService
    {
        private readonly OrdenDePedidoLogic _logic;

        public OrdenDePedidoService()
        {
            // Instancia la lógica con una UnitOfWork para operaciones independientes (CRUD/Gestión)
            _logic = new OrdenDePedidoLogic(new UnitOfWork());
        }

        public List<OrdenDePedidoDTO> ObtenerTodas()
        {
            return _logic.ObtenerTodas();
        }

        public OrdenDePedidoDTO ObtenerPorId(Guid id)
        {
            return _logic.ObtenerPorId(id);
        }

        // --- Métodos de Gestión (Flujo de trabajo OP -> OC) ---

        public void RechazarOrden(Guid ordenId)
        {
            _logic.RechazarOrden(ordenId);
        }

        public Dictionary<Guid, Guid> AprobarYGenerarOrdenesDeCompra(Guid ordenId)
        {
            return _logic.AprobarYGenerarOrdenesDeCompra(ordenId);
        }
    }
}