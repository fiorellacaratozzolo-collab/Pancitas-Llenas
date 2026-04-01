using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Dal.Tools;
using Services.DomainModel.Composite;
using Services.Dal.Implementations;
using Services.Dal.Interfaces;

namespace Services.Dal.Implementations
{
    internal class UsuarioPatenteRepository : IJoinRepository<Usuario>
    {
        public IList<Component> GetByObject(Usuario obj)
        {
            List<Component> patentes = new List<Component>();

            string query = "SELECT IdPatente FROM UsuarioPatente WHERE IdUsuario = @IdUsuario";
            SqlParameter param = new SqlParameter("@IdUsuario", obj.IdUsuario);

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
