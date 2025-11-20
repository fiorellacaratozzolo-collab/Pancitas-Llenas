using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
using ModelsDTO;
using System;
using System.Collections.Generic;

namespace Logic.Facade
{
    public class OrdenDeCompraService
    {
        private readonly OrdenDeCompraLogic _logic;

        public OrdenDeCompraService()
        {
            _logic = new OrdenDeCompraLogic(new UnitOfWork());
        }

        public OrdenDeCompraDTO ObtenerPorId(Guid id)
        {
            return _logic.ObtenerPorId(id);
        }

        public List<OrdenDeCompraDTO> ObtenerTodas()
        {
            return _logic.ObtenerTodas();
        }

        public void RechazarOrden(Guid ordenId)
        {
            _logic.RechazarOrden(ordenId);
        }

        public void FinalizarOrden(Guid ordenId)
        {
            _logic.FinalizarOrden(ordenId);
        }
    }
}
