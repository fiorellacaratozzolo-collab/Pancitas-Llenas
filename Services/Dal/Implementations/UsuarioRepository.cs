using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Services.DomainModel.Composite;
using Services.Dal.Tools;
using Services.Dal.Implementations.Adapters;
using Services.Dal.Interfaces;

namespace Services.Dal.Implementations
{
    /// <summary>
    /// Repositorio encargado de gestionar las operaciones CRUD y validaciones específicas para la entidad Usuario en la base de datos.
    /// </summary>
    internal class UsuarioRepository : IUsuarioRepository
    {
        /// <summary>
        /// Recupera un usuario de la base de datos utilizando su nombre de usuario, disparando la carga de sus relaciones.
        /// </summary>
        public Usuario GetByUserName(string username)
        {
            string query = "SELECT * FROM Usuario WHERE Nombre = @Nombre";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text, new SqlParameter("@Nombre", username)))
            {
                if (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    return UsuarioAdapter.Current.Get(data);
                }
                return null;
            }
        }

        /// <summary>
        /// Recupera un usuario específico de la base de datos a partir de su identificador único.
        /// </summary>
        public Usuario GetById(Guid id)
        {
            string query = "SELECT * FROM Usuario WHERE IdUsuario = @IdUsuario";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text, new SqlParameter("@IdUsuario", id)))
            {
                if (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    return UsuarioAdapter.Current.Get(data);
                }
                return null;
            }
        }

        /// <summary>
        /// Inserta un nuevo registro de usuario en la base de datos generando un nuevo identificador único.
        /// </summary>
        public void Add(Usuario obj)
        {
            obj.IdUsuario = Guid.NewGuid();
            string commandText = "INSERT INTO Usuario (IdUsuario, Nombre, Password, Email, Habilitado, IdSucursal, IdiomaPredeterminado) VALUES (@IdUsuario, @Nombre, @Password, @Email, @Habilitado, @IdSucursal, @IdiomaPredeterminado)";

            SqlHelper.ExecuteNonQuery(commandText, CommandType.Text,
                new SqlParameter("@IdUsuario", obj.IdUsuario),
                new SqlParameter("@Nombre", obj.Nombre),
                new SqlParameter("@Password", obj.Password),
                new SqlParameter("@Email", obj.Email),
                new SqlParameter("@Habilitado", obj.Habilitado),
                new SqlParameter("@IdSucursal", (object)obj.IdSucursal ?? DBNull.Value),
                new SqlParameter("@IdiomaPredeterminado", (object)obj.IdiomaPredeterminado ?? DBNull.Value)
            );
        }

        /// <summary>
        /// Actualiza los datos de un usuario existente en la base de datos.
        /// </summary>
        public void Update(Usuario obj)
        {
            string query = "UPDATE Usuario SET Nombre = @Nombre, Password = @Password, Email = @Email, Habilitado = @Habilitado, IdSucursal = @IdSucursal, IdiomaPredeterminado = @IdiomaPredeterminado WHERE IdUsuario = @IdUsuario";

            SqlHelper.ExecuteNonQuery(query, CommandType.Text,
                new SqlParameter("@IdUsuario", obj.IdUsuario),
                new SqlParameter("@Nombre", obj.Nombre),
                new SqlParameter("@Password", obj.Password),
                new SqlParameter("@Email", obj.Email),
                new SqlParameter("@Habilitado", obj.Habilitado),
                new SqlParameter("@IdSucursal", (object)obj.IdSucursal ?? DBNull.Value),
                new SqlParameter("@IdiomaPredeterminado", (object)obj.IdiomaPredeterminado ?? DBNull.Value)
            );
        }

        /// <summary>
        /// Elimina un registro de usuario de la base de datos utilizando su identificador único.
        /// </summary>
        public void Remove(Guid id)
        {
            string query = "DELETE FROM Usuario WHERE IdUsuario = @IdUsuario";
            SqlHelper.ExecuteNonQuery(query, CommandType.Text, new SqlParameter("@IdUsuario", id));
        }

        /// <summary>
        /// Obtiene una colección completa con todos los usuarios registrados en el sistema.
        /// </summary>
        public IEnumerable<Usuario> GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();
            string query = "SELECT * FROM Usuario";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    usuarios.Add(UsuarioAdapter.Current.Get(data));
                }
            }
            return usuarios;
        }

        /// <summary>
        /// Valida la existencia de un usuario mediante una coincidencia exacta de su nombre de usuario y su contraseña previamente encriptada.
        /// </summary>
        public Usuario GetByCredentials(string user, string password)
        {
            string commandText = "SELECT * FROM Usuario WHERE Nombre = @Nombre AND Password = @Password";

            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(commandText, CommandType.Text,
                new SqlParameter("@Nombre", user),
                new SqlParameter("@Password", password)))
            {
                if (dataReader.Read())
                {
                    object[] data = new object[dataReader.FieldCount];
                    dataReader.GetValues(data);
                    return UsuarioAdapter.Current.Get(data);
                }
                return null;
            }
        }
    }
}
