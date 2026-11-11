using Services.Bll.CustomExceptions;
using System.Text.RegularExpressions;


namespace Services.Facade
{
    /// <summary>
    /// Clase utilitaria estática para centralizar las validaciones de formato de datos en toda la aplicación.
    /// </summary>
    public static class ValidadorDatos
    {
        /// <summary>
        /// Valida que el email no esté vacío y termine estrictamente en @gmail.com.
        /// </summary>
        public static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ValidacionDatosException("El correo electrónico no puede estar vacío.");

            // StringComparison.OrdinalIgnoreCase hace que acepte "@GMAIL.COM" o "@gmail.com"
            if (!email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
                throw new ValidacionDatosException("El correo electrónico debe ser una cuenta de @gmail.com.");
        }

        /// <summary>
        /// Valida que el nombre solo contenga letras (incluyendo acentos y ñ) y espacios. Ni números ni símbolos.
        /// </summary>
        public static void ValidarSoloLetras(string texto, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(texto))
                throw new ValidacionDatosException(string.Format("El campo {0} es obligatorio.", nombreCampo));

            // Expresión regular: solo letras (mayúsculas, minúsculas, acentos, ñ) y espacios.
            if (!Regex.IsMatch(texto, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                throw new ValidacionDatosException(string.Format("El campo {0} solo puede contener letras y espacios. No se permiten números ni caracteres especiales como puntos o asteriscos.", nombreCampo));
        }

        /// <summary>
        /// Valida que la cadena contenga pura y exclusivamente números (sin letras, puntos, guiones ni espacios).
        /// </summary>
        public static void ValidarSoloNumeros(string texto, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(texto))
                throw new ValidacionDatosException(string.Format("El campo {0} es obligatorio.", nombreCampo));

            // Expresión regular: \d+ significa "uno o más dígitos" (del 0 al 9)
            if (!Regex.IsMatch(texto, @"^\d+$"))
                throw new ValidacionDatosException(string.Format("El campo {0} debe contener únicamente números.", nombreCampo));
        }
    }
}
