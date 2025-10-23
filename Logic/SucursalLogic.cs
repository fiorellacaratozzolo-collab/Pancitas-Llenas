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
    public class SucursalLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;

        public SucursalLogic()
        {
            _unitOfWork = new UnitOfWork();
        }

        public Guid CrearSucursal(SucursalDTO sucursalDTO)
        {
            // 1. Mapear DTO de entrada a Entidad DAO
            Sucursal sucursalEntity = _mapper.Map<Sucursal>(sucursalDTO);

            if (string.IsNullOrWhiteSpace(sucursalEntity.NombreSucursal) || string.IsNullOrWhiteSpace(sucursalEntity.Direccion))
            {
                throw new ArgumentException("Nombre y Dirección de la sucursal son obligatorios.");
            }
            // 2. Persistencia
            Guid idSucursal = _unitOfWork.Sucursales.Create(sucursalEntity);
            // 3. Commit
            _unitOfWork.Complete();

            return idSucursal;
        }

        public void ActualizarSucursal(SucursalDTO sucursalDTO)
        {
            // 1.Mapear DTO de entrada a Entidad DAO para la actualización
            Sucursal sucursalEntity = _mapper.Map<Sucursal>(sucursalDTO);
            _unitOfWork.Sucursales.Update(sucursalEntity);
            _unitOfWork.Complete(); // Confirma la actualización
        }

        public void DeshabilitarSucursal(Guid id)
        {
            _unitOfWork.Sucursales.Delete(id);
            _unitOfWork.Complete();
        }

        public List<SucursalDTO> ObtenerTodasLasSucursales()
        {
            List<Sucursal> sucursales = _unitOfWork.Sucursales.GetAll();
            return _mapper.Map<List<SucursalDTO>>(sucursales);
        }

        public List<SucursalDTO> BuscarPorTipoSucursal(int idTipoSucursal)
        {
            List<Sucursal> sucursales = _unitOfWork.Sucursales.GetByTipoSucursal(idTipoSucursal);
            return _mapper.Map<List<SucursalDTO>>(sucursales);
        }

        public SucursalDTO? GetById(Guid id)
        {
            return ObtenerSucursalPorId(id);
        }

        public SucursalDTO? ObtenerSucursalPorId(Guid id)
        {
            if (id == Guid.Empty) return null;

            Sucursal? sucursal = _unitOfWork.Sucursales.GetById(id);

            if (sucursal == null) return null;
            return _mapper.Map<SucursalDTO>(sucursal);
        }

        public List<SucursalDTO> SearchByDireccion(string direccionFragment)
        {
            List<Sucursal> sucursales = _unitOfWork.Sucursales.SearchByDireccion(direccionFragment);
            return _mapper.Map<List<SucursalDTO>>(sucursales);
        }
    }
}
