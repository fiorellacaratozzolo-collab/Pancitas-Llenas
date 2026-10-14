using AutoMapper;
using DataAccess.Implementations.SqlServer;
using DataAccess.Implementations.UnitOfWork;
using DataAccess.Models;
using Logic.MappingProfiles;
using ModelsDTO;
using Services.Facade;

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
            // Encriptamos el CUIT plano que viene de la interfaz
            string cuitEncriptado = CryptographyService.Encrypt(proveedorDTO.Cuit);

            // Buscamos en la BD usando el CUIT ENCRIPTADO
            var proveedorExistente = _unitOfWork.Proveedores.GetByCuit(cuitEncriptado);

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
                    // Mostramos el CUIT plano en el mensaje de error para que el usuario entienda
                    throw new InvalidOperationException(string.Format("El proveedor con CUIT {0} ya existe y se encuentra activo.", proveedorDTO.Cuit));
                }
            }

            //Mapeamos el DTO a la entidad
            Proveedor nuevoProveedor = _mapper.Map<Proveedor>(proveedorDTO);

            //Pisamos el CUIT plano con el CUIT ENCRIPTADO antes de guardar en BD
            nuevoProveedor.Cuit = cuitEncriptado;
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
            proveedor.Cuit = CryptographyService.Encrypt(proveedorDTO.Cuit);
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
            List<ProveedorDTO> dtos = _mapper.Map<List<ProveedorDTO>>(proveedores);
            foreach (var dto in dtos)
            {
                if (!string.IsNullOrEmpty(dto.Cuit))
                    dto.Cuit = CryptographyService.Decrypt(dto.Cuit);
            }
            return dtos;
        }

        /// <summary>
        /// Obtiene solo los proveedores que están actualmente DESHABILITADOS.
        /// </summary>
        public List<ProveedorDTO> ObtenerDeshabilitados()
        {
            List<Proveedor> proveedores = _unitOfWork.Proveedores.GetAll().Where(p => p.Activo == false).ToList();
            List<ProveedorDTO> dtos = _mapper.Map<List<ProveedorDTO>>(proveedores);
            foreach (var dto in dtos)
            {
                if (!string.IsNullOrEmpty(dto.Cuit))
                    dto.Cuit = CryptographyService.Decrypt(dto.Cuit);
            }

            return dtos;
        }

        /// <summary>
        /// Obtiene los proveedores por su CUIT.
        /// </summary>
        public ProveedorDTO? GetByCuit(string cuit)
        {
            string cuitEncriptado = CryptographyService.Encrypt(cuit);
            Proveedor? proveedor = _unitOfWork.Proveedores.GetByCuit(cuitEncriptado);
            if (proveedor == null) return null;
            ProveedorDTO dto = _mapper.Map<ProveedorDTO>(proveedor);
            dto.Cuit = CryptographyService.Decrypt(dto.Cuit);

            return dto;
        }       
    }
}

