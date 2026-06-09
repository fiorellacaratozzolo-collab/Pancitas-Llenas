using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Services.Dal.Tools;
using Services.DomainModel.Composite;
using Services.Dal.Implementations.Adapters;
using Services.Dal.Interfaces;

namespace Services.Dal.Implementations
{
    /// <summary>
    /// Repositorio encargado de gestionar las operaciones CRUD de los permisos individuales (Patentes) en la base de datos.
    /// </summary>
    internal class PatenteRepository : IPatenteRepository
    {
        /// <summary>
        /// Recupera una patente específica utilizando su identificador único.
        /// </summary>
        public Patente GetById(Guid id)
        {
            string SelectByIdStatement = "SELECT IdPatente, DataKey, TipoAcceso FROM [dbo].[Patente] WHERE IdPatente = @IdPatente";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SelectByIdStatement,
                                                                 CommandType.Text,
                                                                 new SqlParameter[] { new SqlParameter("@IdPatente", id) }))
            {
                if (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    return PatenteAdapter.Current.Get(data);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Obtiene una colección completa con todas las patentes registradas en el sistema.
        /// </summary>
        public IEnumerable<Patente> GetAll()
        {
            List<Patente> patentes = new List<Patente>();
            string query = "SELECT IdPatente, DataKey, TipoAcceso FROM [dbo].[Patente]";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    patentes.Add(PatenteAdapter.Current.Get(data));
                }
            }
            return patentes;
        }

        /// <summary>
        /// Inserta una nueva patente en la base de datos.
        /// </summary>
        public void Add(Patente obj)
        {
            string query = "INSERT INTO [dbo].[Patente] (IdPatente, DataKey, TipoAcceso) VALUES (@IdPatente, @DataKey, @TipoAcceso)";
            SqlHelper.ExecuteNonQuery(query, CommandType.Text,
                new SqlParameter("@IdPatente", obj.Id),
                new SqlParameter("@DataKey", obj.DataKey),
                new SqlParameter("@TipoAcceso", (int)obj.TipoAcceso));
        }

        /// <summary>
        /// Actualiza la información de una patente existente.
        /// </summary>
        public void Update(Patente obj)
        {
            string query = "UPDATE [dbo].[Patente] SET DataKey = @DataKey, TipoAcceso = @TipoAcceso WHERE IdPatente = @IdPatente";
            SqlHelper.ExecuteNonQuery(query, CommandType.Text,
                new SqlParameter("@IdPatente", obj.Id),
                new SqlParameter("@DataKey", obj.DataKey),
                new SqlParameter("@TipoAcceso", (int)obj.TipoAcceso));
        }

        /// <summary>
        /// Elimina una patente de la base de datos.
        /// </summary>
        public void Remove(Guid id)
        {
            string query = "DELETE FROM [dbo].[Patente] WHERE IdPatente = @IdPatente";
            SqlHelper.ExecuteNonQuery(query, CommandType.Text, new SqlParameter("@IdPatente", id));
        }
    }
}