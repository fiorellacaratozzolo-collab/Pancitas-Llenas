using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bll
{
    public class PermisosBll
    {
        private Dal.Implementations.PermisosRepository _repo = new Dal.Implementations.PermisosRepository();

        public List<Services.DomainModel.Composite.Familia> GetAllFamilias()
        {
            return _repo.GetAllFamilias();
        }

        public List<Services.DomainModel.Composite.Patente> GetAllPatentes()
        {
            return _repo.GetAllPatentes();
        }

        public void GuardarPermisosUsuario(Guid idUsuario, List<Guid> familiasIds, List<Guid> patentesIds)
        {
            if (idUsuario == Guid.Empty) throw new Exception("Error interno: Usuario no válido.");

            // Llamamos al repositorio
            _repo.GuardarPermisosUsuario(idUsuario, familiasIds, patentesIds);

            // Dejamos registro en la bitácora
            Services.Bll.BitácoraBll bitacora = new Services.Bll.BitácoraBll();
            bitacora.RegistrarLog(
                $"Se actualizaron los roles y permisos del usuario ID: {idUsuario}",
                Services.DomainModel.Logging.Criticidad.Warning); // Warning porque cambiar permisos es algo crítico
        }
    }
}
