using AutoMapper;
using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
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
    public class ProveedorLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;

        public ProveedorLogic()
        {
            _unitOfWork = new UnitOfWork();   
        }

        public Guid CreateProveedor(ProveedorDTO proveedorDTO)
        {

            //1. Mapear DTO de entrada a Entidad DAO
            Proveedor proveedor = _mapper.Map<Proveedor>(proveedorDTO);
            // 2. Persistencia
            Guid idProveedor = _unitOfWork.Proveedores.Create(proveedor);
            // 3. Commit
            _unitOfWork.Complete();
            return idProveedor;
        }

        public void DeshabilitarProveedor(Guid id)
        {
            _unitOfWork.Proveedores.Delete(id);
            _unitOfWork.Complete();
        }

        public List<ProveedorDTO> ObtenerTodosLosProveedores()
        {
            // 1. Obtener Entidades DAO
            List<Proveedor> proveedores = _unitOfWork.Proveedores.GetAll();
            // 2. Mapear la lista de Entidades (DAO) a DTOs
            return _mapper.Map<List<ProveedorDTO>>(proveedores);
        }

        public ProveedorDTO? GetByCuit(int cuit)
        {
            // 1. Obtener la Entidad DAO
            Proveedor? proveedor = _unitOfWork.Proveedores.GetByCuit(cuit);

            if (proveedor == null)
            {
                return null;
            }
            // 2. Mapear la Entidad (DAO) a DTO
            return _mapper.Map<ProveedorDTO>(proveedor);
        }
    }
}

