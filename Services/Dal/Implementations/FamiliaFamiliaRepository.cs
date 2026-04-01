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
    internal class FamiliaFamiliaRepository : IJoinRepository<Familia>
    {
        public IList<Component> GetByObject(Familia obj)
        {
            List<Component> familias = new List<Component>();

            string query = "SELECT IdFamiliaHijo FROM FamiliaFamilia WHERE IdFamiliaPadre = @IdFamiliaPadre";
            SqlParameter param = new SqlParameter("@IdFamiliaPadre", obj.Id);
            
            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text, param))
            {
                while (reader.Read())
                {                    
                    Guid idFamilia = reader.GetGuid(0);
                    familias.Add(new FamiliaRepository().GetById(idFamilia));
                }
            }

            return familias;
        }
    }
}
