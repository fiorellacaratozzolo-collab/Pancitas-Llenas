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
            // Inicialización manual (Estilo actual)
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

        public void RechazarOrden(Guid ordenId)
        {
            _logic.RechazarOrden(ordenId);
        }

        // Ahora devuelve el DTO de resultado con feedback
        public ResultadoGeneracionOCsDTO AprobarYGenerarOCs(Guid ordenId)
        {
            try
            {
                // 1. Llamada a la lógica de agrupación
                // (Ahora usa _logic, corrigiendo el error anterior)
                var resultadosOC = _logic.AprobarYGenerarOrdenesDeCompra(ordenId);

                int numOCs = resultadosOC.Count;

                // 2. Éxito: Se genera el DTO con el feedback positivo
                return new ResultadoGeneracionOCsDTO
                {
                    Exito = true,
                    Mensaje = $"¡Proceso completado con éxito! Se generaron {numOCs} Orden(es) de Compra para {numOCs} proveedor(es).",
                    OrdenesCreadas = resultadosOC // Se adjunta el diccionario de OCs creadas
                };
            }
            catch (KeyNotFoundException ex)
            {
                // 3. Error controlado: Ejemplo, la OP no existe o un producto no tiene proveedor
                return new ResultadoGeneracionOCsDTO
                {
                    Exito = false,
                    Mensaje = $"Error de datos: {ex.Message}",
                    OrdenesCreadas = new Dictionary<Guid, Guid>()
                };
            }
            catch (Exception ex)
            {
                // 4. Error general: Problemas de BD, conexión, lógica no controlada
                return new ResultadoGeneracionOCsDTO
                {
                    Exito = false,
                    Mensaje = $"Error grave en el sistema: {ex.Message}",
                    OrdenesCreadas = new Dictionary<Guid, Guid>()
                };
            }
        }
    }
}