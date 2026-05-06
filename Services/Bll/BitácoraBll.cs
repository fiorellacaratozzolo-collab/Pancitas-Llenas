using Services.Dal.Implementations;
using Services.DomainModel.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bll
{
    /// <summary>
    /// Provee los métodos de negocio para la inserción y consulta centralizada de los eventos de auditoría y errores del sistema.
    /// </summary>
    public class BitácoraBll
    {
        private readonly BitácoraRepository _bitacoraRepo;

        /// <summary>
        /// Inicializa una nueva instancia de la clase de negocio de Bitácora.
        /// </summary>
        public BitácoraBll()
        {
            _bitacoraRepo = new BitácoraRepository();
        }

        /// <summary>
        /// Registra un nuevo evento de auditoría en la base de datos de manera segura, sin interrumpir el flujo si ocurre un error de persistencia.
        /// </summary>
        public void RegistrarLog(string mensaje, Criticidad criticidad, Guid? idUsuario = null)
        {
            try
            {
                Bitácora nuevoLog = new Bitácora
                {
                    Mensaje = mensaje,
                    Criticidad = criticidad,
                    IdUsuario = idUsuario
                };

                _bitacoraRepo.Insertar(nuevoLog);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Error al guardar en bitácora: {0}", ex.Message));
            }
        }

        /// <summary>
        /// Consulta y retorna el registro histórico completo de la bitácora del sistema.
        /// </summary>
        public List<DomainModel.Logging.Bitácora> ListarBitacora()
        {
            try
            {
                return _bitacoraRepo.ListarTodos();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error al consultar la bitácora: {0}", ex.Message));
            }
        }
    }
}