using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cliente
    {
        public Guid IdCliente {  get; set; }

        public string NombreCliente { get; set; }

        public int DNI { get; set; }

        public int IdTipoCliente { get; set; }  //Clave Foránea a TipoClienteEnum     

        public TipoClienteEnum TipoCliente { get; set; } //Propiedad de navegación

        public virtual TipoClienteEnum TipoClienteEnum { get; set; }

    }
}
