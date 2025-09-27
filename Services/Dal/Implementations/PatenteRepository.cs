using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Dal.Tools;
using Services.DomainModel.Composite;
using Services.Dal.Implementations.Adapters;
using Services.Dal.Interfaces;

namespace Services.Dal.Implementations
{
    internal class PatenteRepository : IPatenteRepository
    {
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
                    return null; // or throw an exception if not found
                }
            }
        }
    }
}
