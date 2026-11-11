using Microsoft.Data.SqlClient;
using System.Configuration;


namespace Services.Facade
{
    /// <summary>
    /// Servicio transversal encargado de gestionar las copias de seguridad de las bases de datos.
    /// </summary>
    public class BackupService
    {
        public void GenerarBackups(string carpetaDestino)
        {
            // Creamos el timestamp una sola vez
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            // Armamos las rutas
            string rutaDbSecurity = Path.Combine(carpetaDestino, $"SecurityDB_DB1_{timestamp}.bak");
            string rutaDbNegocio = Path.Combine(carpetaDestino, $"PetShop_DB2_{timestamp}.bak");

            // 1. Buscamos las conexiones con los nombres EXACTOS de tu App.config
            var settingsSecurity = ConfigurationManager.ConnectionStrings["SecurityString"];
            var settingsNegocio = ConfigurationManager.ConnectionStrings["Services.Properties.Settings.PetShopDBConnectionString"];

            // 2. Defensas: Validamos que las haya encontrado
            if (settingsSecurity == null) throw new Exception("No se encontró la conexión 'SecurityString' en el App.config.");
            if (settingsNegocio == null) throw new Exception("No se encontró la conexión 'PetShopDBConnectionString' en el App.config.");

            EjecutarComandoBackup("SecurityDB", rutaDbSecurity, settingsSecurity.ConnectionString);
            EjecutarComandoBackup("PetShopDB", rutaDbNegocio, settingsNegocio.ConnectionString);
        }

        private void EjecutarComandoBackup(string nombreBaseDatos, string rutaArchivoDestino, string connectionString)
        {
            string query = $"BACKUP DATABASE [{nombreBaseDatos}] TO DISK = '{rutaArchivoDestino}' WITH FORMAT, INIT, NAME = '{nombreBaseDatos}-Full Backup';";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.CommandTimeout = 300; // 5 minutos de tiempo de espera
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}
