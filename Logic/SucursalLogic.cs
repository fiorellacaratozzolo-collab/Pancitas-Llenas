using AutoMapper;
using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Interfaces;
using DataAccess.Models;
using Logic.MappingProfiles;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Valida los campos obligatorios, mapea el DTO y registra una nueva sucursal en el sistema.
        /// </summary>
        public Guid CrearSucursal(SucursalDTO sucursalDTO)
        {
            Sucursal sucursalEntity = _mapper.Map<Sucursal>(sucursalDTO);

            if (string.IsNullOrWhiteSpace(sucursalEntity.NombreSucursal) || string.IsNullOrWhiteSpace(sucursalEntity.Direccion))
            {
                throw new ArgumentException("Nombre y Dirección de la sucursal son obligatorios.");
            }

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
        /// Elimina o deshabilita una sucursal del sistema basándose en su identificador único.
        /// </summary>
        public void DeshabilitarSucursal(Guid id)
        {
            _unitOfWork.Sucursales.Delete(id);
            _unitOfWork.Complete();
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
