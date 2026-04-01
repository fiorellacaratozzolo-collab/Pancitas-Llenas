using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.DomainModel.Composite;
using Services.Dal.Tools;
using Services.Dal.Implementations.Adapters;
using Services.Dal.Interfaces;

namespace Services.Dal.Implementations
{
    internal class UsuarioRepository : IUsuarioRepository
    {
        // Este es el método que usará tu LoginService
        public Usuario GetByUserName(string username)
        {
            string query = "SELECT * FROM Usuario WHERE Nombre = @Nombre";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text, new SqlParameter("@Nombre", username)))
            {
                if (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    // Esto dispara toda la carga recursiva (Familias y Patentes)
                    return UsuarioAdapter.Current.Get(data);
                }
                return null;
            }
        }

        // El GetById es obligatorio por la interfaz IGenericRepository
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

        public void Add(Usuario obj)
        {
            // Reutilizando tu lógica de RegistrarUsuario
            obj.IdUsuario = Guid.NewGuid();
            string commandText = "INSERT INTO Usuario (IdUsuario, Nombre, Password, Email, Habilitado) VALUES (@IdUsuario, @Nombre, @Password, @Email, @Habilitado)";
            SqlHelper.ExecuteNonQuery(commandText, CommandType.Text,
                new SqlParameter("@IdUsuario", obj.IdUsuario),
                new SqlParameter("@Nombre", obj.Nombre),
                new SqlParameter("@Password", obj.Password),
                new SqlParameter("@Email", obj.Email),
                new SqlParameter("@Habilitado", obj.Habilitado)
            );
        }

        public void Update(Usuario obj)
        {
            string query = "UPDATE Usuario SET Nombre = @Nombre, Password = @Password, Email = @Email, Habilitado = @Habilitado WHERE IdUsuario = @IdUsuario";
            SqlHelper.ExecuteNonQuery(query, CommandType.Text,
                new SqlParameter("@IdUsuario", obj.IdUsuario),
                new SqlParameter("@Nombre", obj.Nombre),
                new SqlParameter("@Password", obj.Password),
                new SqlParameter("@Email", obj.Email),
                new SqlParameter("@Habilitado", obj.Habilitado)
            );
        }

        public void Remove(Guid id)
        {
            string query = "DELETE FROM Usuario WHERE IdUsuario = @IdUsuario";
            SqlHelper.ExecuteNonQuery(query, CommandType.Text, new SqlParameter("@IdUsuario", id));
        }

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

        // Mantengo tu método original GetByCredentials por si lo prefieres en lugar de GetByUserName
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
