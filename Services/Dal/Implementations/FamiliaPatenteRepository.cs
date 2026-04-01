using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Dal.Interfaces;
using Services.Dal.Tools;
using Services.DomainModel.Composite;

namespace Services.Dal.Implementations
{
    internal class FamiliaPatenteRepository : IJoinRepository<Familia>
    {
        public IList<Component> GetByObject(Familia obj)
        {
            List<Component> patentes = new List<Component>();

            string query = "SELECT IdPatente FROM FamiliaPatente WHERE IdFamilia = @IdFamilia";
            SqlParameter param = new SqlParameter("@IdFamilia", obj.Id);

            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text, param))
            {
                while (reader.Read())
                {
                    Guid idPatente = reader.GetGuid(0);
                    patentes.Add(new PatenteRepository().GetById(idPatente));
                }
            }

            return patentes;
        }
    }
}
