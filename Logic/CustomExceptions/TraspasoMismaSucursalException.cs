using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.CustomExceptions
{
    public class TraspasoMismaSucursalException : Exception
    {
        public TraspasoMismaSucursalException()
            : base("Operación inválida: La sucursal de origen y la sucursal de destino no pueden ser la misma.")
        {
        }
    }
}
