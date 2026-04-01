using Services.Dal.Tools;
using Services.DomainModel;
using Services.DomainModel.Composite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Dal.Interfaces;

namespace Services.Dal.Implementations
{
    internal class UsuarioFamiliaRepository : IJoinRepository<Usuario>
    {
        public IList<Component> GetByObject(Usuario obj)
        {
            List<Component> familias = new List<Component>();

            string query = "SELECT IdFamilia FROM UsuarioFamilia WHERE IdUsuario = @IdUsuario";
            SqlParameter param = new SqlParameter("@IdUsuario", obj.IdUsuario);

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
