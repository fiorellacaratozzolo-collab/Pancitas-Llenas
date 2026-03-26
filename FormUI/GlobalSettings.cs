using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormUI
{
    public static class GlobalSettings
    {
        // El ID de la sucursal donde el usuario inició sesión
        public static Guid SucursalActualId { get; set; }

        // Opcional: Nombre de la sucursal para mostrar en el título del Form
        public static string NombreSucursal { get; set; } = string.Empty;

        // Opcional: Datos del usuario logueado
        public static string UsuarioLogueado { get; set; } = string.Empty;
    }
}
