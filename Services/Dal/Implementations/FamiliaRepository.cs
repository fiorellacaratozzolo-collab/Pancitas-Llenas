using Services.Dal.Implementations.Adapters;
using Services.Dal.Interfaces;
using Services.Dal.Tools;
using Services.DomainModel.Composite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Services.Dal.Implementations
{
    /// <summary>
    /// Repositorio encargado de gestionar las operaciones CRUD en la base de datos para la entidad Familia (Roles).
    /// </summary>
    internal class FamiliaRepository : IFamiliaRepository
    {
        /// <summary>
        /// Recupera una familia específica de la base de datos utilizando su identificador único.
        /// </summary>
        public Familia GetById(Guid id)
        {
            string SelectByIdStatement = "SELECT IdFamilia, Nombre FROM [dbo].[Familia] WHERE IdFamilia = @IdFamilia";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SelectByIdStatement,
                                                                 CommandType.Text,
                                                                 new SqlParameter[] { new SqlParameter("@IdFamilia", id) }))
            {
                if (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    return FamiliaAdapter.Current.Get(data);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Obtiene una colección completa con todas las familias registradas en el sistema.
        /// </summary>
        public IEnumerable<Familia> GetAll()
        {
            List<Familia> familias = new List<Familia>();
            string query = "SELECT IdFamilia, Nombre FROM [dbo].[Familia]";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    familias.Add(FamiliaAdapter.Current.Get(data));
                }
            }
            return familias;
        }

        /// <summary>
        /// Inserta un nuevo registro de familia en la base de datos.
        /// </summary>
        public void Add(Familia obj)
        {
            string query = "INSERT INTO [dbo].[Familia] (IdFamilia, Nombre) VALUES (@IdFamilia, @Nombre)";
            SqlHelper.ExecuteNonQuery(query, CommandType.Text,
                new SqlParameter("@IdFamilia", obj.Id),
                new SqlParameter("@Nombre", obj.Nombre));
        }

        /// <summary>
        /// Actualiza el nombre de una familia existente en la base de datos.
        /// </summary>
        public void Update(Familia obj)
        {
            string query = "UPDATE [dbo].[Familia] SET Nombre = @Nombre WHERE IdFamilia = @IdFamilia";
            SqlHelper.ExecuteNonQuery(query, CommandType.Text,
                new SqlParameter("@IdFamilia", obj.Id),
                new SqlParameter("@Nombre", obj.Nombre));
        }

        /// <summary>
        /// Elimina un registro de familia de la base de datos utilizando su identificador único.
        /// </summary>
        public void Remove(Guid id)
        {
            string query = "DELETE FROM [dbo].[Familia] WHERE IdFamilia = @IdFamilia";
            SqlHelper.ExecuteNonQuery(query, CommandType.Text, new SqlParameter("@IdFamilia", id));
        }
    }
}
