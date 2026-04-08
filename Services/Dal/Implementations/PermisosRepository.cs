using Services.Dal.Implementations.Adapters;
using Services.Dal.Tools;
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
    }
}
