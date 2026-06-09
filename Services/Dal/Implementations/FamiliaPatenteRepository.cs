using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Services.Dal.Interfaces;
using Services.Dal.Tools;
using Services.DomainModel.Composite;

namespace Services.Dal.Implementations
{
    /// <summary>
    /// Repositorio de unión encargado de recuperar las patentes (permisos individuales) contenidas dentro de una familia (rol).
    /// </summary>
    internal class FamiliaPatenteRepository : IJoinRepository<Familia>
    {
        /// <summary>
        /// Obtiene la lista de componentes tipo Patente asociados a la Familia proporcionada.
        /// </summary>
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