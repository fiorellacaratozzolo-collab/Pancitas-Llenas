using Services.DomainModel.Composite;
using System.Collections.Generic;

namespace Services.Dal.Interfaces
{
    /// <summary>
    /// Define un contrato genérico para operaciones de lectura jerárquica o relacional utilizando el patrón Composite.
    /// </summary>
    public interface IJoinRepository<T>
    {
        /// <summary>
        /// Obtiene todos los componentes hijos asociados a un objeto padre.
        /// </summary>
        /// <param name="parent">El objeto padre (Usuario o Familia).</param>
        /// <returns>Lista de componentes hijos (Familias o Patentes).</returns>
        IList<Component> GetByObject(T parent);
    }
}