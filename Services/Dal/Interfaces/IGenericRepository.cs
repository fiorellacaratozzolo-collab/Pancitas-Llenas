using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dal.Interfaces
{
    /// <summary>
    /// Define el contrato base estándar para operaciones CRUD (Crear, Leer, Actualizar, Borrar) en los repositorios de datos.
    /// </summary>
    public interface IGenericRepository<T>
    {
        /// <summary>Inserta un nuevo registro en la base de datos.</summary>
        void Add(T obj);

        /// <summary>Actualiza un registro existente en la base de datos.</summary>
        void Update(T obj);

        /// <summary>Elimina un registro de la base de datos basándose en su identificador único.</summary>
        void Remove(Guid id);

        /// <summary>Recupera un único registro a partir de su identificador único.</summary>
        T GetById(Guid id);

        /// <summary>Recupera todos los registros existentes de la entidad.</summary>
        IEnumerable<T> GetAll();
    }
}