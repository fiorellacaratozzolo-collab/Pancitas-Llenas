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

        public ProveedorLogic()
        {
            _unitOfWork = new UnitOfWork();
        }

        /// <summary>
        /// Registra un nuevo proveedor o reactiva uno inactivo si coincide su CUIT.
        /// </summary>
        public Guid CreateProveedor(ProveedorDTO proveedorDTO)
        {
            var proveedorExistente = _unitOfWork.Proveedores.GetByCuit(proveedorDTO.Cuit);

            if (proveedorExistente != null)
            {
                if (proveedorExistente.Activo == false)
                {
                    proveedorExistente.Activo = true;
                    proveedorExistente.NombreProveedor = proveedorDTO.NombreProveedor;
                    proveedorExistente.Telefono = proveedorDTO.Telefono;
                    proveedorExistente.Direccion = proveedorDTO.Direccion;

                    _unitOfWork.Proveedores.Update(proveedorExistente);
                    _unitOfWork.Complete();

                    return proveedorExistente.IdProveedor;
                }
                else
                {
                    throw new InvalidOperationException(string.Format("El proveedor con CUIT {0} ya existe y se encuentra activo.", proveedorDTO.Cuit));
                }
            }

            Proveedor nuevoProveedor = _mapper.Map<Proveedor>(proveedorDTO);
            nuevoProveedor.Activo = true;
            Guid idProveedor = _unitOfWork.Proveedores.Create(nuevoProveedor);

            _unitOfWork.Complete();
            return idProveedor;
        }

        /// <summary>
        /// Actualiza los datos de un proveedor existente.
        /// </summary>
        public void UpdateProveedor(ProveedorDTO proveedorDTO)
        {
            Proveedor proveedor = _mapper.Map<Proveedor>(proveedorDTO);
            _unitOfWork.Proveedores.Update(proveedor);
            _unitOfWork.Complete();
        }

        /// <summary>
        /// Realiza un borrado lógico en cascada: deshabilita al proveedor y a todos los productos exclusivos asociados a él.
        /// </summary>
        public void DeshabilitarProveedor(Guid id)
        {
            var proveedor = _unitOfWork.Proveedores.GetById(id);
            if (proveedor != null)
            {
                proveedor.Activo = false;
                _unitOfWork.Proveedores.Update(proveedor);

                var vinculosAsociados = _unitOfWork.ProveedorProductos.GetAll()
                    .Where(pp => pp.IdProveedor == id)
                    .ToList();

                foreach (var vinculo in vinculosAsociados)
                {
                    _unitOfWork.Productos.Delete(vinculo.IdProducto);
                }
                _unitOfWork.Complete();
            }
        }

        /// <summary>
        /// Rehabilita un proveedor inactivo.
        /// </summary>
        public void HabilitarProveedor(Guid id)
        {
            _unitOfWork.Proveedores.Habilitar(id);
            _unitOfWork.Complete();
        }

        /// <summary>
        /// Obtiene solo los proveedores que están actualmente ACTIVOS.
        /// </summary>
        public List<ProveedorDTO> ObtenerActivos()
        {
            List<Proveedor> proveedores = _unitOfWork.Proveedores.GetAll().Where(p => p.Activo == true).ToList();
            return _mapper.Map<List<ProveedorDTO>>(proveedores);
        }

        /// <summary>
        /// Obtiene solo los proveedores que están actualmente DESHABILITADOS.
        /// </summary>
        public List<ProveedorDTO> ObtenerDeshabilitados()
        {
            List<Proveedor> proveedores = _unitOfWork.Proveedores.GetAll().Where(p => p.Activo == false).ToList();
            return _mapper.Map<List<ProveedorDTO>>(proveedores);
        }

        /// <summary>
        /// Obtiene los proveedores por su CUIT.
        /// </summary>
        public ProveedorDTO? GetByCuit(int cuit)
        {
            Proveedor? proveedor = _unitOfWork.Proveedores.GetByCuit(cuit);
            if (proveedor == null) return null;

            return _mapper.Map<ProveedorDTO>(proveedor);
        }
    }
}

