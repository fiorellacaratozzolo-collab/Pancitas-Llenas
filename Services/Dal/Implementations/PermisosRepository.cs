using Services.Dal.Implementations.Adapters;
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
    public class PermisosRepository
    {
        /// <summary>
        /// Obtiene y ensambla el árbol completo de permisos (patentes individuales y familias) asignados a un usuario y los carga en su lista de privilegios.
        /// </summary>
        public void CargarPrivilegios(Usuario usuario)
        {
            usuario.Privilegios = new List<Component>();

            string queryPatentes = "SELECT p.IdPatente, p.DataKey, p.TipoAcceso FROM [dbo].[Patente] p INNER JOIN [dbo].[UsuarioPatente] up ON p.IdPatente = up.IdPatente WHERE up.IdUsuario = @IdUsuario";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(queryPatentes, CommandType.Text, new SqlParameter("@IdUsuario", usuario.IdUsuario)))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);
                    usuario.Privilegios.Add(PatenteAdapter.Current.Get(data));
                }
            }

            string queryFamilias = "SELECT f.IdFamilia, f.Nombre FROM [dbo].[Familia] f INNER JOIN [dbo].[UsuarioFamilia] uf ON f.IdFamilia = uf.IdFamilia WHERE uf.IdUsuario = @IdUsuario";

            using (SqlDataReader readerFamilias = SqlHelper.ExecuteReader(queryFamilias, CommandType.Text, new SqlParameter("@IdUsuario", usuario.IdUsuario)))
            {
                while (readerFamilias.Read())
                {
                    object[] dataFam = new object[readerFamilias.FieldCount];
                    readerFamilias.GetValues(dataFam);
                    var familia = FamiliaAdapter.Current.Get(dataFam);

                    string queryPatFamilia = "SELECT p.IdPatente, p.DataKey, p.TipoAcceso FROM [dbo].[Patente] p INNER JOIN [dbo].[FamiliaPatente] fp ON p.IdPatente = fp.IdPatente WHERE fp.IdFamilia = @IdFamilia";

                    using (SqlDataReader readerPatFam = SqlHelper.ExecuteReader(queryPatFamilia, CommandType.Text, new SqlParameter("@IdFamilia", familia.Id)))
                    {
                        while (readerPatFam.Read())
                        {
                            object[] dataPat = new object[readerPatFam.FieldCount];
                            readerPatFam.GetValues(dataPat);

                            var nuevaPatente = PatenteAdapter.Current.Get(dataPat);

                            familia.Add(nuevaPatente);
                        }
                    }

                    usuario.Privilegios.Add(familia);
                }
            }
        }

        /// <summary>
        /// Recupera el catálogo completo de familias (roles) disponibles en el sistema.
        /// </summary>
        public List<Familia> GetAllFamilias()
        {
            var lista = new List<Familia>();
            string query = "SELECT IdFamilia, Nombre FROM [dbo].[Familia]";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);

                    var familia = (Familia)FamiliaAdapter.Current.Get(data);
                    lista.Add(familia);
                }
            }
            return lista;
        }

        /// <summary>
        /// Recupera el catálogo completo de patentes (permisos individuales) disponibles en el sistema.
        /// </summary>
        public List<Patente> GetAllPatentes()
        {
            var lista = new List<Patente>();
            string query = "SELECT IdPatente, DataKey, TipoAcceso FROM [dbo].[Patente]";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);

                    var patente = (Patente)PatenteAdapter.Current.Get(data);
                    lista.Add(patente);
                }
            }
            return lista;
        }

        /// <summary>
        /// Reemplaza la configuración de seguridad de un usuario eliminando sus permisos actuales y registrando las nuevas familias y patentes asignadas.
        /// </summary>
        public void GuardarPermisosUsuario(Guid idUsuario, List<Guid> familiasIds, List<Guid> patentesIds)
        {
            string deleteFamilias = "DELETE FROM [dbo].[UsuarioFamilia] WHERE IdUsuario = @IdUsuario";
            string deletePatentes = "DELETE FROM [dbo].[UsuarioPatente] WHERE IdUsuario = @IdUsuario";

            SqlHelper.ExecuteNonQuery(deleteFamilias, CommandType.Text, new SqlParameter("@IdUsuario", idUsuario));
            SqlHelper.ExecuteNonQuery(deletePatentes, CommandType.Text, new SqlParameter("@IdUsuario", idUsuario));

            foreach (Guid idFamilia in familiasIds)
            {
                string insertFamilia = "INSERT INTO [dbo].[UsuarioFamilia] (IdUsuario, IdFamilia) VALUES (@IdUsuario, @IdFamilia)";
                SqlHelper.ExecuteNonQuery(insertFamilia, CommandType.Text,
                    new SqlParameter("@IdUsuario", idUsuario),
                    new SqlParameter("@IdFamilia", idFamilia));
            }

            foreach (Guid idPatente in patentesIds)
            {
                string insertPatente = "INSERT INTO [dbo].[UsuarioPatente] (IdUsuario, IdPatente) VALUES (@IdUsuario, @IdPatente)";
                SqlHelper.ExecuteNonQuery(insertPatente, CommandType.Text,
                    new SqlParameter("@IdUsuario", idUsuario),
                    new SqlParameter("@IdPatente", idPatente));
            }
        }
    }
}