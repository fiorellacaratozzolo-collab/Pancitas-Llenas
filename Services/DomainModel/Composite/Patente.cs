using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Services.DomainModel.Composite
{
    /// <summary>
    /// This class (a) represents leaf objects in the composition, and (b) defines
    /// behaviour for primitive objects in the composition.
    /// </summary>
    public class Patente : Component
    {

        public string DataKey { get; set; }

        public TipoAcceso TipoAcceso { get; set; }

        public Patente()
        {

        }


        /// 
        /// <param name="component"></param>
        public override void Add(Component component)
        {
            throw new Exception("No se pueden agregar elementos en un hijo tipo hoja");
        }

        /// 
        /// <param name="component"></param>
        public override void Remove(Component component)
        {
            throw new Exception("No se pueden eliminar elementos en un hijo tipo hoja");
        }

    }//end Patente

    public enum TipoAcceso
    {
        Pantalla,
        CasoUso,
        Servicio,
        SP,
        Tabla
    }
}
