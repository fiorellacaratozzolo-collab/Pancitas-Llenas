using Services.Dal.Tools;
using Services.DomainModel.Composite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Services.Dal.Interfaces;

namespace Services.Dal.Implementations
{
    /// <summary>
    /// Repositorio de unión encargado de recuperar las familias (roles) directamente asignadas a un usuario específico.
    /// </summary>
    internal class UsuarioFamiliaRepository : IJoinRepository<Usuario>
    {
        /// <summary>
        /// Obtiene una lista de componentes tipo Familia asociados al usuario proporcionado.
        /// </summary>
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
