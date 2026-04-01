using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainModel.Composite
{

    public class Familia : Component
    {
        private List<Component> _hijos = new List<Component>();

        public override void Add(Component c)
        {
            _hijos.Add(c);
        }

        public override void Remove(Component c)
        {
            _hijos.RemoveAll(x => x.Id == c.Id);
        }

        public override int GetCount()
        {
            return _hijos.Count;
        }

        // Método vital para que compile tu FamiliaAdapter
        public void AddRange(IEnumerable<Component> componentes)
        {
            if (componentes != null)
            {
                _hijos.AddRange(componentes);
            }
        }

        public IList<Component> Hijos
        {
            get { return _hijos.AsReadOnly(); }
        }
    }
}
