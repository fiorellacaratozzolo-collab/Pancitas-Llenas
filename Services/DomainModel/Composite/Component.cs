using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainModel.Composite
{
    /// <summary>
    /// Clase base abstracta del patrón Composite que establece el contrato común tanto para los permisos individuales (Patentes) como para los roles agrupados (Familias).
    /// </summary>
    public abstract class Component
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }

        /// <summary>
        /// Establece el contrato para agregar un componente hijo a la jerarquía.
        /// </summary>
        public abstract void Add(Component c);

        /// <summary>
        /// Establece el contrato para remover un componente hijo de la jerarquía.
        /// </summary>
        public abstract void Remove(Component c);

        /// <summary>
        /// Establece el contrato para contar los elementos hijos directos contenidos en el nodo.
        /// </summary>
        public abstract int GetCount();
    }
}