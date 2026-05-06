using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainModel.Composite
{
    /// <summary>
    /// Representa un rol o agrupación de permisos en el sistema. Funciona como un nodo "rama" dentro del patrón Composite.
    /// </summary>
    public class Familia : Component
    {
        private List<Component> _hijos = new List<Component>();

        /// <summary>
        /// Agrega un nuevo componente (Patente u otra Familia) a la jerarquía de este rol.
        /// </summary>
        public override void Add(Component c)
        {
            _hijos.Add(c);
        }

        /// <summary>
        /// Elimina un componente existente de la jerarquía basándose en su identificador único.
        /// </summary>
        public override void Remove(Component c)
        {
            _hijos.RemoveAll(x => x.Id == c.Id);
        }

        /// <summary>
        /// Retorna la cantidad de permisos directos o roles que cuelgan de esta familia.
        /// </summary>
        public override int GetCount()
        {
            return _hijos.Count;
        }

        /// <summary>
        /// Agrega un bloque completo de componentes hijos a la jerarquía de una sola vez.
        /// </summary>
        public void AddRange(IEnumerable<Component> componentes)
        {
            if (componentes != null)
            {
                _hijos.AddRange(componentes);
            }
        }

        /// <summary>
        /// Obtiene la colección de componentes hijos en formato de solo lectura para evitar modificaciones no controladas desde el exterior.
        /// </summary>
        public IList<Component> Hijos
        {
            get { return _hijos.AsReadOnly(); }
        }
    }
}
