using Services.Dal.Implementations;
using Services.DomainModel.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bll
{
    /// <summary>
    /// Gestiona la recuperación de patentes (permisos individuales) disponibles en el sistema.
    /// </summary>
    public class PatenteBll
    {
        private readonly PatenteRepository _patenteRepo;

        /// <summary>
        /// Inicializa una nueva instancia de la clase de negocio de Patente y su repositorio.
        /// </summary>
        public PatenteBll()
        {
            _patenteRepo = new PatenteRepository();
        }

        /// <summary>
        /// Obtiene una lista materializada de todas las patentes registradas.
        /// </summary>
        public List<Patente> ObtenerTodas()
        {
            return _patenteRepo.GetAll().ToList();
        }
    }
}