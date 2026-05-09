using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define el contrato de persistencia para las operaciones de acceso a datos relacionadas con los proveedores.
    /// </summary>
    public interface IProveedorRepository
    {
        /// <summary>Inserta un nuevo proveedor y retorna su identificador único generado.</summary>
        Guid Create(Proveedor proveedor);

        /// <summary>Realiza un borrado lógico del proveedor cambiando su estado a inactivo.</summary>
        void Delete(Guid id);

        /// <summary>Reactiva un proveedor previamente deshabilitado.</summary>
        void Habilitar(Guid id);

        /// <summary>Actualiza los valores de un proveedor existente.</summary>
        void Update(Proveedor proveedor);

        /// <summary>Recupera el listado completo de proveedores en el sistema (sin filtrar por estado).</summary>
        List<Proveedor> GetAll();

        /// <summary>Obtiene un proveedor específico utilizando su número de CUIT.</summary>
        Proveedor? GetByCuit(int cuit);

        /// <summary>Obtiene un proveedor específico utilizando su identificador único.</summary>
        Proveedor? GetById(Guid id);
    }
}