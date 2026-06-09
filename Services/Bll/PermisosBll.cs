using System;
using System.Collections.Generic;

namespace Services.Bll
{
    /// <summary>
    /// Gestiona las reglas de negocio relacionadas con la asignación y obtención de permisos (familias y patentes).
    /// </summary>
    public class PermisosBll
    {
        private Dal.Implementations.PermisosRepository _repo = new Dal.Implementations.PermisosRepository();

        /// <summary>
        /// Obtiene el catálogo completo de familias (roles) del sistema.
        /// </summary>
        public List<Services.DomainModel.Composite.Familia> GetAllFamilias()
        {
            return _repo.GetAllFamilias();
        }

        /// <summary>
        /// Obtiene el catálogo completo de patentes (permisos individuales) del sistema.
        /// </summary>
        public List<Services.DomainModel.Composite.Patente> GetAllPatentes()
        {
            return _repo.GetAllPatentes();
        }

        /// <summary>
        /// Procesa y almacena la nueva configuración de privilegios de un usuario y audita la acción crítica.
        /// </summary>
        public void GuardarPermisosUsuario(Guid idUsuario, List<Guid> familiasIds, List<Guid> patentesIds)
        {
            if (idUsuario == Guid.Empty) throw new Exception("Error interno: Usuario no válido.");

            _repo.GuardarPermisosUsuario(idUsuario, familiasIds, patentesIds);

            Dal.Implementations.UsuarioRepository usuarioRepo = new Dal.Implementations.UsuarioRepository();
            var usuario = usuarioRepo.GetById(idUsuario);
            string nombreUsuario = usuario != null ? usuario.Nombre : "Desconocido";

            Services.Bll.BitácoraBll bitacora = new Services.Bll.BitácoraBll();
            bitacora.RegistrarLog(
                string.Format("Se actualizaron los roles y permisos del usuario: {0}", nombreUsuario),
                Services.DomainModel.Logging.Criticidad.Warning);
        }
    }
}