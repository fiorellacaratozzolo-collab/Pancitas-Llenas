using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EncargadoSucursal
    {
        public Guid IdEncargadoSucursal { get; set; }
        public Guid IdEncargado { get; set; }  //Clave Foránea a Encargado
        public Guid IdSucursal { get; set; } //Clave Foránea a Sucursal

        public virtual Sucursal Sucursal { get; set; } //Relacion de tabla para saber a que entidad apuntar
        public virtual Encargado Encargado { get; set; } //Relacion de tabla para saber a que entidad apuntar
    }
}
