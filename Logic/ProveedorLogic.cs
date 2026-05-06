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
    /// <summary>
    /// Gestiona las operaciones de negocio y la persistencia relacionadas con los proveedores del sistema.
    /// </summary>
    public class ProveedorLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper = MapperConfigInitializer.Mapper;

        /// <summary>
        /// Inicializa una nueva instancia de la lógica de proveedores.
        /// </summary>
        public ProveedorLogic()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Mapea y registra un nuevo proveedor en la base de datos de manera atómica.
        /// </summary>
        public Guid CreateProveedor(ProveedorDTO proveedorDTO)
        {
            Proveedor proveedor = _mapper.Map<Proveedor>(proveedorDTO);
            Guid idProveedor = _unitOfWork.Proveedores.Create(proveedor);

            _unitOfWork.Complete();

            return idProveedor;
        }

        /// <summary>
        /// Elimina o deshabilita un proveedor del sistema mediante su identificador único.
        /// </summary>
        public void DeshabilitarProveedor(Guid id)
        {
            _unitOfWork.Proveedores.Delete(id);
            _unitOfWork.Complete();
        }

        /// <summary>
        /// Recupera el catálogo completo de proveedores registrados y los mapea a objetos de transferencia.
        /// </summary>
        public List<ProveedorDTO> ObtenerTodosLosProveedores()
        {
            List<Proveedor> proveedores = _unitOfWork.Proveedores.GetAll();
            return _mapper.Map<List<ProveedorDTO>>(proveedores);
        }

        /// <summary>
        /// Recupera un proveedor específico a partir de su número de CUIT.
        /// </summary>
        public ProveedorDTO? GetByCuit(int cuit)
        {
            Proveedor? proveedor = _unitOfWork.Proveedores.GetByCuit(cuit);

            if (proveedor == null)
            {
                return null;
            }

            return _mapper.Map<ProveedorDTO>(proveedor);
        }
    }
}

