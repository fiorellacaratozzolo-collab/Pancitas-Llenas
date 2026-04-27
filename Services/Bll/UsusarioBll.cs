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
    public class UsuarioBll
    {
        private readonly UsuarioRepository _usuarioRepo;

        public UsuarioBll()
        {
            _usuarioRepo = new UsuarioRepository();
        }

        public void RegistrarUsuario(string nombre, string email, string contraseñaClara, Guid? idSucursal)
        {
            // 1. Validaciones de campos vacíos (esto ya lo tenías)
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(contraseñaClara))
            {
                throw new Exception("Todos los campos (Nombre, Email y Contraseña) son obligatorios.");
            }

            // ---> 2. NUEVA VALIDACIÓN: VERIFICAR DUPLICADOS <---

            // A) Verificamos que el Nombre de Usuario no exista
            Usuario usuarioExistente = _usuarioRepo.GetByUserName(nombre);
            if (usuarioExistente != null)
            {
                throw new Exception($"El nombre de usuario '{nombre}' ya está en uso. Por favor, elija otro.");
            }

            // B) Verificamos que el Email no esté registrado usando LINQ
            var todosLosUsuarios = _usuarioRepo.GetAll();
            if (todosLosUsuarios.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception($"El email '{email}' ya se encuentra registrado en el sistema.");
            }
            // ---------------------------------------------------

            // 3. Encriptamos la clave y guardamos (esto queda exactamente igual)
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
            bitacora.RegistrarLog($"Se dio de alta al nuevo usuario: {nombre} ({email})", Criticidad.Info);
        }

        // Método para el LOGIN 
        public Usuario ValidarCredenciales(string username, string passwordClara)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(passwordClara))
                throw new Exception("Debe ingresar usuario y contraseña.");

            string passwordHasheada = CryptographyService.HashMd5(passwordClara);
            Usuario usuarioLogueado = _usuarioRepo.GetByCredentials(username, passwordHasheada);

            if (usuarioLogueado == null)
                throw new Exception("Usuario o contraseña incorrectos.");

            // ---> ACÁ IMPLEMENTAMOS TU EXCEPCIÓN PERSONALIZADA <---
            if (!usuarioLogueado.Habilitado)
            {
                // Lanzamos la excepción que creamos, pasándole el username y un motivo
                throw new UsuarioBloqueadoException(username, "El usuario fue deshabilitado por el administrador.");
            }

            // ---> ¡LA PIEZA FALTANTE! Llenamos la mochila antes de dejarlo entrar <---
            Services.Dal.Implementations.PermisosRepository permisosRepo = new Services.Dal.Implementations.PermisosRepository();
            permisosRepo.CargarPrivilegios(usuarioLogueado);

            return usuarioLogueado;
        }

        // Método preparado para tu botón "Buscar" y para llenar el dgvUsuarios
        public IEnumerable<Usuario> ListarTodos()
        {
            return _usuarioRepo.GetAll();
        }

        // MÉTODO PARA OBTENER UN USUARIO POR SU ID (Usado para permisos y edición)
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

        // MÉTODO PARA ACTUALIZAR (Solo datos básicos)
        public void ActualizarUsuario(Guid idUsuario, string nombre, string email, Guid? idSucursal)
        {
            if (idUsuario == Guid.Empty) throw new Exception("Debe seleccionar un usuario de la grilla.");

            // Limpiamos los espacios en blanco adelante y atrás por las dudas
            nombre = nombre?.Trim();
            email = email?.Trim();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(email))
                throw new Exception("Nombre y Email son obligatorios.");

            // 1. Traemos TODOS los usuarios a la memoria para revisarlos uno por uno
            var todosLosUsuarios = _usuarioRepo.GetAll().ToList();

            // 2. Revisamos si alguien MÁS ya tiene ese nombre (ignorando mayúsculas)
            bool nombreDuplicado = todosLosUsuarios.Any(u =>
                u.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase) &&
                u.IdUsuario != idUsuario);

            if (nombreDuplicado)
            {
                throw new Exception($"El nombre de usuario '{nombre}' ya está en uso por otra persona.");
            }

            // 3. Revisamos si alguien MÁS ya tiene ese email (ignorando mayúsculas)
            bool emailDuplicado = todosLosUsuarios.Any(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                u.IdUsuario != idUsuario);

            if (emailDuplicado)
            {
                throw new Exception($"El email '{email}' ya se encuentra registrado por otra persona.");
            }

            // 4. Si pasó los controles, traemos el usuario para guardar
            Usuario usuarioExistente = _usuarioRepo.GetById(idUsuario);
            if (usuarioExistente == null) throw new Exception("El usuario no existe en la base de datos.");

            // Actualizamos
            usuarioExistente.Nombre = nombre;
            usuarioExistente.Email = email;
            usuarioExistente.IdSucursal = idSucursal;

            _usuarioRepo.Update(usuarioExistente);

            BitácoraBll bitacora = new BitácoraBll();
            bitacora.RegistrarLog($"Se actualizó el usuario: {nombre}", Services.DomainModel.Logging.Criticidad.Info);
        }

        // MÉTODO PARA DESHABILITAR (Baja Lógica)
        public void DeshabilitarUsuario(Guid idUsuario)
        {
            if (idUsuario == Guid.Empty) throw new Exception("Debe seleccionar un usuario de la grilla.");

            Usuario usuarioExistente = _usuarioRepo.GetById(idUsuario);
            if (usuarioExistente == null) throw new Exception("El usuario no existe en la base de datos.");

            // Le bajamos el pulgar
            usuarioExistente.Habilitado = false;

            _usuarioRepo.Update(usuarioExistente);

            BitácoraBll bitacora = new BitácoraBll();
            bitacora.RegistrarLog($"Se deshabilitó al usuario: {usuarioExistente.Nombre}", Criticidad.Warning);
        }

        // MÉTODO PARA HABILITAR
        public void HabilitarUsuario(Guid idUsuario)
        {
            if (idUsuario == Guid.Empty) throw new Exception("Debe seleccionar un usuario de la grilla.");

            Usuario usuarioExistente = _usuarioRepo.GetById(idUsuario);
            if (usuarioExistente == null) throw new Exception("El usuario no existe en la base de datos.");

            // Le subimos el pulgar
            usuarioExistente.Habilitado = true;

            _usuarioRepo.Update(usuarioExistente);

            BitácoraBll bitacora = new BitácoraBll();
            bitacora.RegistrarLog($"Se habilitó al usuario: {usuarioExistente.Nombre}", Criticidad.Info);
        }

        // MÉTODO PARA BUSCAR USUARIOS
        public IEnumerable<Usuario> BuscarUsuarios(string criterio)
        {
            // Traemos todos los usuarios
            var todosLosUsuarios = _usuarioRepo.GetAll();

            // Si no escribieron nada para buscar, devolvemos toda la lista
            if (string.IsNullOrWhiteSpace(criterio))
                return todosLosUsuarios;

            // Convertimos a minúsculas para que la búsqueda no sea sensible a mayúsculas
            criterio = criterio.ToLower();

            // Filtramos los que coincidan en Nombre o Email
            return todosLosUsuarios.Where(u =>
                u.Nombre.ToLower().Contains(criterio) ||
                u.Email.ToLower().Contains(criterio)
            ).ToList();
        }

        // MÉTODO PARA MODIFICAR LA CONTRASEÑA
        public void ModificarContraseña(Guid idUsuario, string nuevaContraseñaClara)
        {
            if (idUsuario == Guid.Empty) throw new Exception("Debe seleccionar un usuario de la grilla.");
            if (string.IsNullOrWhiteSpace(nuevaContraseñaClara)) throw new Exception("Debe ingresar la nueva contraseña.");

            // Traemos al usuario de la base de datos
            Usuario usuarioExistente = _usuarioRepo.GetById(idUsuario);
            if (usuarioExistente == null) throw new Exception("El usuario no existe.");

            // ¡Aplicamos el Hash MD5 a la nueva contraseña!
            usuarioExistente.Password = Services.Facade.CryptographyService.HashMd5(nuevaContraseñaClara);

            // Guardamos el cambio
            _usuarioRepo.Update(usuarioExistente);

            // Auditoría
            BitácoraBll bitacora = new BitácoraBll();
            bitacora.RegistrarLog($"Se modificó la contraseña del usuario: {usuarioExistente.Nombre}", Criticidad.Warning);
        }

    }
}

