using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsDTO
{
    public class HistorialEntregaDTO
    {
        public DateTime Fecha { get; set; }
        public string? Producto { get; set; }
        public string? Marca { get; set; }
        public string? NombreProveedor { get; set; }
        public int Cantidad { get; set; }
        public decimal PesoUnitario { get; set; }
    }
}
