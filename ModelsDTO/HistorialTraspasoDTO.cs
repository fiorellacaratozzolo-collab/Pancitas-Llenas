using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public class HistorialTraspasoDTO
    {
        public DateTime Fecha { get; set; }
        public string? TipoMovimiento { get; set; } //"INGRESO" o "EGRESO"
        public string? SucursalInvolucrada { get; set; }
        public string? Producto { get; set; }
        public int Cantidad { get; set; }
        public string? UsuarioResponsable { get; set; }
    }
}
