using Services.Bll.CustomExceptions;
using Services.Dal.Implementations;
using Services.Dal.Interfaces;
using Services.DomainModel.Composite;
using Services.DomainModel.Logging;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bll
{
    /// <summary>
    /// Gestiona las reglas de negocio, validaciones y operaciones relacionadas con los usuarios del sistema.
    /// </summary>
    public class UsuarioBll
    {
        private readonly UsuarioRepository _usuarioRepo;

        /// <summary>
        /// Inicializa una nueva instancia de la clase de negocio de Usuario y su repositorio de datos asociado.
        /// </summary>
        public UsuarioBll()
        {
            _usuarioRepo = new UsuarioRepository();
        }

        /// <summary>
        /// Valida las reglas de negocio (campos obligatorios y unicidad de datos), encripta la contraseña y registra un nuevo usuario en el sistema.
        /// </summary>
        public void RegistrarUsuario(string nombre, string email, string contraseñaClara, Guid? idSucursal)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(contraseñaClara))
            {
                throw new Exception("Todos los campos (Nombre, Email y Contraseña) son obligatorios.");
            }

            Usuario usuarioExistente = _usuarioRepo.GetByUserName(nombre);
            if (usuarioExistente != null)
            {
                throw new Exception(string.Format("El nombre de usuario '{0}' ya está en uso. Por favor, elija otro.", nombre));
            }

            var todosLosUsuarios = _usuarioRepo.GetAll();
            if (todosLosUsuarios.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception(string.Format("El email '{0}' ya se encuentra registrado en el sistema.", email));
            }

            string contraseñaHasheada = Services.Facade.CryptographyService.HashMd5(contraseñaClara);

            Usuario nuevoUsuario = new Usuario(
                Guid.NewGuid(),
                nombre,
                email,
                contraseñaHasheada,
                true,
                idSucursal
            );

            _usuarioRepo.Add(nuevoUsuario);

            BitácoraBll bitacora = new BitácoraBll();
            bitacora.RegistrarLog(string.Format("Se dio de alta al nuevo usuario: {0} ({1})", nombre, email), Criticidad.Info);
        }

        /// <summary>
        /// Valida las credenciales de inicio de sesión, verifica el estado de habilitación del usuario y ensambla su árbol de privilegios.
        /// </summary>
        public Usuario ValidarCredenciales(string username, string passwordClara)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(passwordClara))
                throw new Exception("Debe ingresar usuario y contraseña.");

            string passwordHasheada = CryptographyService.HashMd5(passwordClara);
            Usuario usuarioLogueado = _usuarioRepo.GetByCredentials(username, passwordHasheada);

            if (usuarioLogueado == null)
                throw new Exception("Usuario o contraseña incorrectos.");

            if (!usuarioLogueado.Habilitado)
            {
                throw new UsuarioBloqueadoException(username, "El usuario fue deshabilitado por el administrador.");
            }

            Services.Dal.Implementations.PermisosRepository permisosRepo = new Services.Dal.Implementations.PermisosRepository();
            permisosRepo.CargarPrivilegios(usuarioLogueado);

            return usuarioLogueado;
        }

        /// <summary>
        /// Obtiene la colección completa de usuarios registrados en el sistema.
        /// </summary>
        public IEnumerable<Usuario> ListarTodos()
        {
            return _usuarioRepo.GetAll();
        }

        /// <summary>
        /// Recupera un usuario específico mediante su identificador, incluyendo la carga de sus permisos asociados.
        /// </summary>
        public Usuario GetById(Guid idUsuario)
        {
            if (idUsuario == Guid.Empty) throw new ArgumentException("El ID no puede estar vacío.");

            Usuario usuarioEncontrado = _usuarioRepo.GetById(idUsuario);

            if (usuarioEncontrado != null)
            {
                Services.Dal.Implementations.PermisosRepository permisosRepo = new Services.Dal.Implementations.PermisosRepository();
                permisosRepo.CargarPrivilegios(usuarioEncontrado);
            }

            return usuarioEncontrado;
        }

        /// <summary>
        /// Valida y actualiza la información básica de perfil de un usuario, garantizando que no se generen nombres o correos duplicados.
        /// </summary>
        public void ActualizarUsuario(Guid idUsuario, string nombre, string email, Guid? idSucursal)
        {
            if (idUsuario == Guid.Empty) throw new Exception("Debe seleccionar un usuario de la grilla.");

            nombre = nombre?.Trim();
            email = email?.Trim();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(email))
                throw new Exception("Nombre y Email son obligatorios.");

            var todosLosUsuarios = _usuarioRepo.GetAll().ToList();

            bool nombreDuplicado = todosLosUsuarios.Any(u =>
                u.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase) &&
                u.IdUsuario != idUsuario);

            if (nombreDuplicado)
            {
                throw new Exception(string.Format("El nombre de usuario '{0}' ya está en uso por otra persona.", nombre));
            }

            bool emailDuplicado = todosLosUsuarios.Any(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                u.IdUsuario != idUsuario);

            if (emailDuplicado)
            {
                throw new Exception(string.Format("El email '{0}' ya se encuentra registrado por otra persona.", email));
            }

            Usuario usuarioExistente = _usuarioRepo.GetById(idUsuario);
            if (usuarioExistente == null) throw new Exception("El usuario no existe en la base de datos.");

            usuarioExistente.Nombre = nombre;
            usuarioExistente.Email = email;
            usuarioExistente.IdSucursal = idSucursal;

            _usuarioRepo.Update(usuarioExistente);

            BitácoraBll bitacora = new BitácoraBll();
            bitacora.RegistrarLog(string.Format("Se actualizó el usuario: {0}", nombre), Services.DomainModel.Logging.Criticidad.Info);
        }

        /// <summary>
        /// Ejecuta una baja lógica del usuario en el sistema impidiendo su acceso, dejando constancia en la auditoría.
        /// </summary>
        public void DeshabilitarUsuario(Guid idUsuario)
        {
            if (idUsuario == Guid.Empty) throw new Exception("Debe seleccionar un usuario de la grilla.");

            Usuario usuarioExistente = _usuarioRepo.GetById(idUsuario);
            if (usuarioExistente == null) throw new Exception("El usuario no existe en la base de datos.");

            usuarioExistente.Habilitado = false;

            _usuarioRepo.Update(usuarioExistente);

            BitácoraBll bitacora = new BitácoraBll();
            bitacora.RegistrarLog(string.Format("Se deshabilitó al usuario: {0}", usuarioExistente.Nombre), Criticidad.Warning);
        }

        /// <summary>
        /// Rehabilita el acceso al sistema para un usuario previamente deshabilitado.
        /// </summary>
        public void HabilitarUsuario(Guid idUsuario)
        {
            if (idUsuario == Guid.Empty) throw new Exception("Debe seleccionar un usuario de la grilla.");

            Usuario usuarioExistente = _usuarioRepo.GetById(idUsuario);
            if (usuarioExistente == null) throw new Exception("El usuario no existe en la base de datos.");

            usuarioExistente.Habilitado = true;

            _usuarioRepo.Update(usuarioExistente);

            BitácoraBll bitacora = new BitácoraBll();
            bitacora.RegistrarLog(string.Format("Se habilitó al usuario: {0}", usuarioExistente.Nombre), Criticidad.Info);
        }

        /// <summary>
        /// Filtra la lista de usuarios en memoria buscando coincidencias ignorando mayúsculas y minúsculas por nombre o correo electrónico.
        /// </summary>
        public IEnumerable<Usuario> BuscarUsuarios(string criterio)
        {
            var todosLosUsuarios = _usuarioRepo.GetAll();

            if (string.IsNullOrWhiteSpace(criterio))
                return todosLosUsuarios;

            criterio = criterio.ToLower();

            return todosLosUsuarios.Where(u =>
                u.Nombre.ToLower().Contains(criterio) ||
                u.Email.ToLower().Contains(criterio)
            ).ToList();
        }

        /// <summary>
        /// Sobrescribe la contraseña de un usuario encriptando la nueva clave proporcionada.
        /// </summary>
        public void ModificarContraseña(Guid idUsuario, string nuevaContraseñaClara)
        {
            if (idUsuario == Guid.Empty) throw new Exception("Debe seleccionar un usuario de la grilla.");
            if (string.IsNullOrWhiteSpace(nuevaContraseñaClara)) throw new Exception("Debe ingresar la nueva contraseña.");

            Usuario usuarioExistente = _usuarioRepo.GetById(idUsuario);
            if (usuarioExistente == null) throw new Exception("El usuario no existe.");

            usuarioExistente.Password = Services.Facade.CryptographyService.HashMd5(nuevaContraseñaClara);

            _usuarioRepo.Update(usuarioExistente);

            BitácoraBll bitacora = new BitácoraBll();
            bitacora.RegistrarLog(string.Format("Se modificó la contraseña del usuario: {0}", usuarioExistente.Nombre), Criticidad.Warning);
        }

        /// <summary>
        /// Actualiza la preferencia de idioma de un usuario y registra el evento en la auditoría del sistema.
        /// </summary>
        public void ActualizarIdiomaPredeterminado(Guid idUsuario, string nuevoIdioma)
        {
            if (idUsuario == Guid.Empty) throw new Exception("ID de usuario inválido.");

            Usuario usuarioExistente = _usuarioRepo.GetById(idUsuario);

            if (usuarioExistente == null) throw new Exception("El usuario no existe en la base de datos.");

            usuarioExistente.IdiomaPredeterminado = nuevoIdioma;

            _usuarioRepo.Update(usuarioExistente);

            BitácoraBll bitacora = new BitácoraBll();
            bitacora.RegistrarLog(string.Format("Se modificó el idioma predeterminado ({0}) del usuario: {1}", nuevoIdioma, usuarioExistente.Nombre), Criticidad.Info);
        }
    }
}

