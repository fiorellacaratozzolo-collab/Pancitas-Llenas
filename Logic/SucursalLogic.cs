using AutoMapper;
using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Models;
using Logic.MappingProfiles;
using ModelsDTO;

namespace Logic
{
    /// <summary>
    /// Gestiona las reglas de negocio y validaciones correspondientes a la administración de sucursales.
    /// </summary>
    public class SucursalLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;

        /// <summary>
        /// Inicializa una nueva instancia de la lógica de sucursales.
        /// </summary>
        public SucursalLogic()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Valida los campos, reactiva una sucursal si ya existía inactiva (mismo nombre y dirección), o registra una nueva.
        /// </summary>
        public Guid CrearSucursal(SucursalDTO sucursalDTO)
        {
            if (string.IsNullOrWhiteSpace(sucursalDTO.NombreSucursal) || string.IsNullOrWhiteSpace(sucursalDTO.Direccion))
            {
                throw new ArgumentException("Nombre y Dirección de la sucursal son obligatorios.");
            }

            // Reactivación Inteligente: Buscamos si existe una sucursal exactamente igual
            var sucursalExistente = _unitOfWork.Sucursales.GetAll().FirstOrDefault(s =>
                s.NombreSucursal.Trim().Equals(sucursalDTO.NombreSucursal.Trim(), StringComparison.OrdinalIgnoreCase) &&
                s.Direccion.Trim().Equals(sucursalDTO.Direccion.Trim(), StringComparison.OrdinalIgnoreCase));

            if (sucursalExistente != null)
            {
                if (sucursalExistente.Activo == false)
                {
                    sucursalExistente.Activo = true;
                    sucursalExistente.Telefono = sucursalDTO.Telefono;
                    sucursalExistente.IdTipoSucursal = sucursalDTO.IdTipoSucursal;

                    _unitOfWork.Sucursales.Update(sucursalExistente);
                    _unitOfWork.Complete();

                    return sucursalExistente.IdSucursal;
                }
                else
                {
                    throw new InvalidOperationException(string.Format("Ya existe una sucursal ACTIVA con el nombre '{0}' en la dirección '{1}'.", sucursalDTO.NombreSucursal, sucursalDTO.Direccion));
                }
            }

            // Alta Normal
            Sucursal sucursalEntity = _mapper.Map<Sucursal>(sucursalDTO);
            sucursalEntity.Activo = true;

            Guid idSucursal = _unitOfWork.Sucursales.Create(sucursalEntity);
            _unitOfWork.Complete();

            return idSucursal;
        }

        /// <summary>
        /// Mapea los cambios y actualiza la información de una sucursal existente.
        /// </summary>
        public void ActualizarSucursal(SucursalDTO sucursalDTO)
        {
            Sucursal sucursalEntity = _mapper.Map<Sucursal>(sucursalDTO);
            _unitOfWork.Sucursales.Update(sucursalEntity);
            _unitOfWork.Complete();
        }

        /// <summary>
        /// Deshabilita una sucursal del sistema basándose en su identificador único.
        /// </summary>
        public void DeshabilitarSucursal(Guid id)
        {
            _unitOfWork.Sucursales.Delete(id);
            _unitOfWork.Complete();
        }

        /// <summary>
        /// Habilita una sucursal del sistema basándose en su identificador único.
        /// </summary>
        public void HabilitarSucursal(Guid id)
        {
            _unitOfWork.Sucursales.Habilitar(id);
            _unitOfWork.Complete();
        }

        /// <summary>
        /// Obtiene solo las sucursales que están actualmente ACTIVAS.
        /// </summary>
        public List<SucursalDTO> ObtenerActivas()
        {
            List<Sucursal> sucursales = _unitOfWork.Sucursales.GetAll().Where(s => s.Activo == true).ToList();
            return _mapper.Map<List<SucursalDTO>>(sucursales);
        }

        /// <summary>
        /// Obtiene solo las sucursales que están actualmente DESHABILITADAS.
        /// </summary>
        public List<SucursalDTO> ObtenerDeshabilitadas()
        {
            List<Sucursal> sucursales = _unitOfWork.Sucursales.GetAll().Where(s => s.Activo == false).ToList();
            return _mapper.Map<List<SucursalDTO>>(sucursales);
        }

        /// <summary>
        /// Obtiene el catálogo completo de sucursales y las mapea a objetos de transferencia de datos.
        /// </summary>
        public List<SucursalDTO> ObtenerTodasLasSucursales()
        {
            List<Sucursal> sucursales = _unitOfWork.Sucursales.GetAll();
            return _mapper.Map<List<SucursalDTO>>(sucursales);
        }

        /// <summary>
        /// Filtra la lista de sucursales según un tipo específico.
        /// </summary>
        public List<SucursalDTO> BuscarPorTipoSucursal(int idTipoSucursal)
        {
            List<Sucursal> sucursales = _unitOfWork.Sucursales.GetByTipoSucursal(idTipoSucursal);
            return _mapper.Map<List<SucursalDTO>>(sucursales);
        }

        /// <summary>
        /// Recupera una sucursal específica a partir de su ID.
        /// </summary>
        public SucursalDTO? GetById(Guid id)
        {
            return ObtenerSucursalPorId(id);
        }

        /// <summary>
        /// Recupera y mapea una sucursal específica gestionando valores nulos o identificadores vacíos.
        /// </summary>
        public SucursalDTO? ObtenerSucursalPorId(Guid id)
        {
            if (id == Guid.Empty) return null;

            Sucursal? sucursal = _unitOfWork.Sucursales.GetById(id);
            if (sucursal == null) return null;

            return _mapper.Map<SucursalDTO>(sucursal);
        }

        /// <summary>
        /// Realiza una búsqueda de sucursales que contengan una cadena específica en su dirección.
        /// </summary>
        public List<SucursalDTO> SearchByDireccion(string direccionFragment)
        {
            List<Sucursal> sucursales = _unitOfWork.Sucursales.SearchByDireccion(direccionFragment);
            return _mapper.Map<List<SucursalDTO>>(sucursales);
        }
    }
}