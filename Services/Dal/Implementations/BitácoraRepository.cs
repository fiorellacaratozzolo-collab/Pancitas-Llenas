using Services.Dal.Tools;
using Services.DomainModel.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dal.Implementations
{
    public class BitácoraRepository
    {
        public void Insertar(Bitácora log)
        {
            // No insertamos la Fecha ni el IdBitacora porque SQL lo hace solo (IDENTITY y GETDATE)
            string query = @"INSERT INTO Bitacora (IdUsuario, Mensaje, Criticidad) 
                             VALUES (@IdUsuario, @Mensaje, @Criticidad)";

            SqlParameter[] parametros = new SqlParameter[]
            {
                // Si el IdUsuario viene vacío (null), mandamos DBNull a SQL
                new SqlParameter("@IdUsuario", log.IdUsuario.HasValue ? (object)log.IdUsuario.Value : DBNull.Value),
                new SqlParameter("@Mensaje", log.Mensaje),
                // Guardamos la criticidad como texto (ej: "Info", "Error")
                new SqlParameter("@Criticidad", log.Criticidad.ToString())
            };

            // Llamamos a tu SqlHelper mágico
            SqlHelper.ExecuteNonQuery(query, CommandType.Text, parametros);
        }

        public List<Bitácora> ListarTodos()
        {
            List<Bitácora> lista = new List<Bitácora>();

            // Hacemos un LEFT JOIN por si hay logs del sistema que no tienen usuario
            string query = @"SELECT b.IdBitacora, b.Fecha, b.IdUsuario, u.Nombre AS NombreUsuario, b.Mensaje, b.Criticidad 
                     FROM Bitacora b
                     LEFT JOIN Usuario u ON b.IdUsuario = u.IdUsuario
                     ORDER BY b.Fecha DESC"; //--Ordenamos para ver lo más reciente primero

    // Asumiendo que tu SqlHelper tiene un método ExecuteReader que devuelve un SqlDataReader
    using (SqlDataReader reader = SqlHelper.ExecuteReader(query, CommandType.Text))
            {
                while (reader.Read())
                {
                    Bitácora log = new Bitácora();
                    log.IdBitacora = Convert.ToInt32(reader["IdBitacora"]);
                    log.Fecha = Convert.ToDateTime(reader["Fecha"]);
                    log.Mensaje = reader["Mensaje"].ToString();

                    // Parseamos el Enum de criticidad
                    log.Criticidad = (Criticidad)Enum.Parse(typeof(Criticidad), reader["Criticidad"].ToString());

                    // Validamos si el IdUsuario y el Nombre vienen nulos desde SQL
                    if (reader["IdUsuario"] != DBNull.Value)
                    {
                        log.IdUsuario = Guid.Parse(reader["IdUsuario"].ToString());
                        log.NombreUsuario = reader["NombreUsuario"].ToString();
                    }
                    else
                    {
                        log.NombreUsuario = "Sistema"; // Si no hay usuario, fue el sistema
                    }

                    lista.Add(log);
                }
            }

            return lista;
        }
    }
}

