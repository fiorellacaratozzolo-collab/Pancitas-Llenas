using Services.Dal.Implementations.Adapters;
using Services.Dal.Interfaces;
using Services.Dal.Tools;
using Services.DomainModel.Composite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dal.Implementations
{
  
        internal class FamiliaRepository : IFamiliaRepository
        {
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

            public void Add(Familia obj)
            {
                string query = "INSERT INTO [dbo].[Familia] (IdFamilia, Nombre) VALUES (@IdFamilia, @Nombre)";
                SqlHelper.ExecuteNonQuery(query, CommandType.Text,
                    new SqlParameter("@IdFamilia", obj.Id),
                    new SqlParameter("@Nombre", obj.Nombre));
            }

            public void Update(Familia obj)
            {
                string query = "UPDATE [dbo].[Familia] SET Nombre = @Nombre WHERE IdFamilia = @IdFamilia";
                SqlHelper.ExecuteNonQuery(query, CommandType.Text,
                    new SqlParameter("@IdFamilia", obj.Id),
                    new SqlParameter("@Nombre", obj.Nombre));
            }

            public void Remove(Guid id)
            {
                // Nota: Primero se tiene que borrar las dependencias en FamiliaFamilia y FamiliaPatente
                string query = "DELETE FROM [dbo].[Familia] WHERE IdFamilia = @IdFamilia";
                SqlHelper.ExecuteNonQuery(query, CommandType.Text, new SqlParameter("@IdFamilia", id));
            }
        }
}

