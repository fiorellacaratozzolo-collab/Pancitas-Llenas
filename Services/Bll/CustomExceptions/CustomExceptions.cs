
namespace Services.Bll.CustomExceptions
{
    /// <summary>
    /// Excepción lanzada cuando los datos ingresados por el usuario no cumplen con el formato o las reglas del negocio.
    /// </summary>
    public class ValidacionDatosException : Exception
    {
        public ValidacionDatosException(string mensaje) : base(mensaje) { }
    }
}
