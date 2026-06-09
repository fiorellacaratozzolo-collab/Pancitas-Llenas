using Services.Dal.Implementations;
using Services.DomainModel.Composite;
using System.Collections.Generic;
using System.Linq;

namespace Services.Bll
{
    /// <summary>
    /// Gestiona la recuperación de jerarquías de permisos y roles (Familias) disponibles en el sistema.
    /// </summary>
    public class FamiliaBll
    {
        private readonly FamiliaRepository _familiaRepo;

        /// <summary>
        /// Inicializa una nueva instancia de la clase de negocio de Familia y su repositorio asociado.
        /// </summary>
        public FamiliaBll()
        {
            _familiaRepo = new FamiliaRepository();
        }

        /// <summary>
        /// Obtiene una lista materializada de todas las familias (roles) registradas.
        /// </summary>
        public List<Familia> ObtenerTodas()
        {
            return _familiaRepo.GetAll().ToList();
        }
    }
}