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
    /// Fachada que centraliza los métodos para la administración del directorio de sucursales.
    /// </summary>
    public class SucursalService
    {
        private readonly SucursalLogic _sucursalLogic = new SucursalLogic();

        /// <summary>
        /// Registra una nueva sucursal en el sistema.
        /// </summary>
        public Guid CreateSucursal(SucursalDTO sucursalDTO)
        {
            return _sucursalLogic.CrearSucursal(sucursalDTO);
        }

        /// <summary>
        /// Recupera el catálogo completo de sucursales habilitadas en la plataforma.
        /// </summary>
        public List<SucursalDTO> GetAllSucursales()
        {
            return _sucursalLogic.ObtenerActivas();
        }

        /// <summary>
        /// Recupera el catálogo completo de sucursales HABILITADAS.
        /// </summary>
        public List<SucursalDTO> ObtenerActivas()
        {
            return _sucursalLogic.ObtenerActivas();
        }

        /// <summary>
        /// Recupera el catálogo completo de sucursales DESHABILITADAS.
        /// </summary>
        public List<SucursalDTO> ObtenerDeshabilitadas()
        {
            return _sucursalLogic.ObtenerDeshabilitadas();
        }

        /// <summary>
        /// Obtiene una lista de sucursales filtrada por su categoría o tipo.
        /// </summary>
        public List<SucursalDTO> GetByTipoSucursal(int idTipoSucursal)
        {
            return _sucursalLogic.BuscarPorTipoSucursal(idTipoSucursal);
        }

        /// <summary>
        /// Da de baja lógicamente una sucursal del sistema.
        /// </summary>
        public void DisableSucursal(Guid id)
        {
            _sucursalLogic.DeshabilitarSucursal(id);
        }

        /// <summary>
        /// Da de alta lógicamente una sucursal del sistema.
        /// </summary>
        public void HabilitarSucursal(Guid id)
        {
            _sucursalLogic.HabilitarSucursal(id);
        }

        /// <summary>
        /// Actualiza la información y configuración de una sucursal existente.
        /// </summary>
        public void UpdateSucursal(SucursalDTO sucursalDTO)
        {
            _sucursalLogic.ActualizarSucursal(sucursalDTO);
        }

        /// <summary>
        /// Busca y devuelve los datos de una sucursal utilizando su identificador único.
        /// </summary>
        public SucursalDTO? GetById(Guid id)
        {
            return _sucursalLogic.GetById(id);
        }

        /// <summary>
        /// Realiza una búsqueda de sucursales cuyas direcciones coincidan parcialmente con el texto ingresado.
        /// </summary>
        public List<SucursalDTO> SearchByDireccion(string direccionFragment)
        {
            return _sucursalLogic.SearchByDireccion(direccionFragment);
        }
    }
}
