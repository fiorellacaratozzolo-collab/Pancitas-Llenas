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
        public void CargarPrivilegios(Services.DomainModel.Composite.Usuario usuario)
        {
            // Inicializamos la mochila vacía para evitar errores de nulos
            usuario.Privilegios = new List<Services.DomainModel.Composite.Component>();

            // 1. CARGAR PATENTES ASIGNADAS DIRECTAMENTE AL USUARIO
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

            // 2. CARGAR LAS FAMILIAS (ROLES) DEL USUARIO
            string queryFamilias = "SELECT f.IdFamilia, f.Nombre FROM [dbo].[Familia] f INNER JOIN [dbo].[UsuarioFamilia] uf ON f.IdFamilia = uf.IdFamilia WHERE uf.IdUsuario = @IdUsuario";

            using (SqlDataReader readerFamilias = SqlHelper.ExecuteReader(queryFamilias, CommandType.Text, new SqlParameter("@IdUsuario", usuario.IdUsuario)))
            {
                while (readerFamilias.Read())
                {
                    object[] dataFam = new object[readerFamilias.FieldCount];
                    readerFamilias.GetValues(dataFam);
                    var familia = FamiliaAdapter.Current.Get(dataFam);

                    // 3. POR CADA FAMILIA, BUSCAMOS QUÉ PATENTES TIENE ADENTRO
                    string queryPatFamilia = "SELECT p.IdPatente, p.DataKey, p.TipoAcceso FROM [dbo].[Patente] p INNER JOIN [dbo].[FamiliaPatente] fp ON p.IdPatente = fp.IdPatente WHERE fp.IdFamilia = @IdFamilia";

                    using (SqlDataReader readerPatFam = SqlHelper.ExecuteReader(queryPatFamilia, CommandType.Text, new SqlParameter("@IdFamilia", familia.Id)))
                    {
                        while (readerPatFam.Read())
                        {
                            object[] dataPat = new object[readerPatFam.FieldCount];
                            readerPatFam.GetValues(dataPat);

                            // 1. Instanciamos la patente
                            var nuevaPatente = PatenteAdapter.Current.Get(dataPat);

                            // 2. Usamos el método propio de la clase Familia en lugar de tocar la lista Hijos directamente
                            familia.Add(nuevaPatente);
                        }
                    }

                    // Finalmente, guardamos la familia (ya llena) en la mochila del usuario
                    usuario.Privilegios.Add(familia);
                }
            }
        }

        public List<Services.DomainModel.Composite.Familia> GetAllFamilias()
        {
            var lista = new List<Services.DomainModel.Composite.Familia>();
            string query = "SELECT IdFamilia, Nombre FROM [dbo].[Familia]";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);

                    // Usamos tu adaptador para convertir la fila de SQL a objeto Familia
                    var familia = (Services.DomainModel.Composite.Familia)FamiliaAdapter.Current.Get(data);
                    lista.Add(familia);
                }
            }
            return lista;
        }

        public List<Services.DomainModel.Composite.Patente> GetAllPatentes()
        {
            var lista = new List<Services.DomainModel.Composite.Patente>();
            // Incluimos TipoAcceso porque tu adaptador de Patente seguramente lo espera
            string query = "SELECT IdPatente, DataKey, TipoAcceso FROM [dbo].[Patente]";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);

                    // Usamos tu adaptador para convertir la fila de SQL a objeto Patente
                    var patente = (Services.DomainModel.Composite.Patente)PatenteAdapter.Current.Get(data);
                    lista.Add(patente);
                }
            }
            return lista;
        }

        public void GuardarPermisosUsuario(Guid idUsuario, List<Guid> familiasIds, List<Guid> patentesIds)
        {
            // 1. BARREMOS LA MOCHILA VIEJA (Eliminamos todo)
            string deleteFamilias = "DELETE FROM [dbo].[UsuarioFamilia] WHERE IdUsuario = @IdUsuario";
            string deletePatentes = "DELETE FROM [dbo].[UsuarioPatente] WHERE IdUsuario = @IdUsuario";

            // Nota: Si usas SqlHelper o similar, ajústalo a tu método exacto
            SqlHelper.ExecuteNonQuery(deleteFamilias, CommandType.Text, new SqlParameter("@IdUsuario", idUsuario));
            SqlHelper.ExecuteNonQuery(deletePatentes, CommandType.Text, new SqlParameter("@IdUsuario", idUsuario));

            // 2. AMUEBLAMOS CON LO NUEVO: Insertamos las Familias tildadas
            foreach (Guid idFamilia in familiasIds)
            {
                string insertFamilia = "INSERT INTO [dbo].[UsuarioFamilia] (IdUsuario, IdFamilia) VALUES (@IdUsuario, @IdFamilia)";
                SqlHelper.ExecuteNonQuery(insertFamilia, CommandType.Text,
                    new SqlParameter("@IdUsuario", idUsuario),
                    new SqlParameter("@IdFamilia", idFamilia));
            }

            // 3. AMUEBLAMOS CON LO NUEVO: Insertamos las Patentes sueltas tildadas
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
