namespace Services.Facade.Extensions
{
    /// <summary>
    /// Proporciona métodos de extensión para facilitar la traducción de cadenas de texto en toda la aplicación.
    /// </summary>
    public static class IdiomaExtension
    {
        /// <summary>
        /// Extiende la clase String para traducir el texto invocando al servicio de idiomas correspondiente.
        /// </summary>
        public static string Traducir(this string texto)
        {
            return Bll.IdiomaService.Traducir(texto);
        }
    }
}
