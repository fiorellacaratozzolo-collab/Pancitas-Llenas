using System;

namespace Services.DomainModel.Logging
{
    /// <summary>
    /// Representa un registro de auditoría almacenado en la base de datos para el seguimiento de la actividad de los usuarios y errores del sistema.
    /// </summary>
    public class Bitácora
    {
        public int IdBitacora { get; set; }
        public DateTime Fecha { get; set; }
        public Guid? IdUsuario { get; set; }
        public string Mensaje { get; set; }
        public Criticidad Criticidad { get; set; }
        public string NombreUsuario { get; set; }
    }
}