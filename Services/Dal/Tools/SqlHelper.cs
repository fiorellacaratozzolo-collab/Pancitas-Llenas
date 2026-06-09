using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Services.Dal.Tools
{
    /// <summary>
    /// Clase de utilidad estática para centralizar y simplificar la ejecución de comandos SQL en la base de datos.
    /// </summary>
    internal static class SqlHelper
    {
        readonly static string conString;

        /// <summary>
        /// Inicializa la cadena de conexión estática leyendo el archivo de configuración de la aplicación.
        /// </summary>
        static SqlHelper()
        {
            conString = ConfigurationManager.ConnectionStrings["SecurityString"].ConnectionString;
        }

        /// <summary>
        /// Ejecuta un comando SQL que no devuelve filas (como INSERT, UPDATE o DELETE) y retorna el número de filas afectadas.
        /// </summary>
        public static Int32 ExecuteNonQuery(String commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            CheckNullables(parameters);

            using (SqlConnection conn = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Revisa los parámetros SQL y reemplaza los valores nulos de .NET por DBNull.Value para evitar excepciones en el motor de base de datos.
        /// </summary>
        private static void CheckNullables(SqlParameter[] parameters)
        {
            foreach (SqlParameter item in parameters)
            {
                if (item.SqlValue == null)
                {
                    item.SqlValue = DBNull.Value;
                }
            }
        }

        /// <summary>
        /// Ejecuta un comando SQL y retorna la primera columna de la primera fila del conjunto de resultados.
        /// </summary>
        public static Object ExecuteScalar(String commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            CheckNullables(parameters);

            using (SqlConnection conn = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// Ejecuta un comando SQL que devuelve datos y retorna un SqlDataReader. La conexión se cerrará automáticamente al descartar el lector.
        /// </summary>
        public static SqlDataReader ExecuteReader(String commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            CheckNullables(parameters);

            SqlConnection conn = new SqlConnection(conString);

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(parameters);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                return reader;
            }
        }
    }
}
