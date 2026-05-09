using DataAccess.Models;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Facade
{
    /// <summary>
    /// Fachada que centraliza las operaciones de negocio relacionadas con la gestión del catálogo de proveedores.
    /// </summary>
    public class ProveedorService
    {
        private readonly ProveedorLogic _proveedorLogic;

        public ProveedorService()
        {
            _proveedorLogic = new ProveedorLogic();
        }

        /// <summary>
        /// Registra un nuevo proveedor en el sistema.
        /// </summary>
        public Guid CreateProveedor(ProveedorDTO proveedorDTO)
        {
            return _proveedorLogic.CreateProveedor(proveedorDTO);
        }

        /// <summary>
        /// Actualiza a un proveedor en el sistema.
        /// </summary>
        public void UpdateProveedor(ProveedorDTO proveedorDTO)
        {
            _proveedorLogic.UpdateProveedor(proveedorDTO);
        }

        /// <summary>
        /// Obtiene el catálogo de proveedores ACTIVOS (mantiene la compatibilidad con otros módulos).
        /// </summary>
        public List<ProveedorDTO> GetAllProveedores()
        {
            return _proveedorLogic.ObtenerActivos();
        }

        /// <summary>
        /// Obtiene el catálogo de únicamente proveedores ACTIVOS.
        /// </summary>
        public List<ProveedorDTO> ObtenerActivos()
        {
            return _proveedorLogic.ObtenerActivos();
        }

        /// <summary>
        /// Obtiene el catálogo de únicamente proveedores DESHABILITADOS.
        /// </summary>
        public List<ProveedorDTO> ObtenerDeshabilitados()
        {
            return _proveedorLogic.ObtenerDeshabilitados();
        }

        /// <summary>
        /// Deshabilita lógicamente un proveedor del directorio.
        /// </summary>
        public void DeshabilitarProveedor(Guid id)
        {
            _proveedorLogic.DeshabilitarProveedor(id);
        }

        /// <summary>
        /// Habilita a un proveedor deshabilitado.
        /// </summary>
        public void HabilitarProveedor(Guid id)
        {
            _proveedorLogic.HabilitarProveedor(id);
        }

        /// <summary>
        /// Recupera la información de un proveedor mediante su CUIT.
        /// </summary>
        public ProveedorDTO? GetByCuit(int cuit)
        {
            return _proveedorLogic.GetByCuit(cuit);
        }
    }
}