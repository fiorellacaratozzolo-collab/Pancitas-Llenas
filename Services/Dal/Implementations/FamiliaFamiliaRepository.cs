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
    /// Repositorio de unión encargado de recuperar las jerarquías anidadas recuperando las familias hijas que pertenecen a una familia padre.
    /// </summary>
    internal class FamiliaFamiliaRepository : IJoinRepository<Familia>
    {
        /// <summary>
        /// Obtiene la lista de componentes tipo Familia asociados como hijos a la Familia padre proporcionada.
        /// </summary>
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