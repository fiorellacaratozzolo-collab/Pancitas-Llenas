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

        /// <summary>
        /// Inicializa una nueva instancia del servicio de proveedores.
        /// </summary>
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
        /// Obtiene la lista completa de proveedores registrados.
        /// </summary>
        public List<ProveedorDTO> GetAllProveedores()
        {
            return _proveedorLogic.ObtenerTodosLosProveedores();
        }

        /// <summary>
        /// Deshabilita o elimina lógicamente un proveedor del directorio.
        /// </summary>
        public void DeshabilitarProveedor(Guid id)
        {
            _proveedorLogic.DeshabilitarProveedor(id);
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
