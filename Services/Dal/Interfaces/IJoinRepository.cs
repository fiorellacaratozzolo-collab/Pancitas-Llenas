using Services.DomainModel.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dal.Interfaces
{
    public interface IJoinRepository<T>
    {
        /// <summary>
        /// Obtiene todos los componentes hijos asociados a un objeto padre.
        /// </summary>
        /// <param name="parent">El objeto padre (Usuario o Familia).</param>
        /// <returns>Lista de componentes (Familias o Patentes).</returns>
        IList<Component> GetByObject(T parent);
    }
}
