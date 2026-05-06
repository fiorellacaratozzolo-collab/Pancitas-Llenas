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

        /// <summary>Elimina o deshabilita un registro de proveedor existente.</summary>
        void Delete(Guid id);

        /// <summary>Recupera el listado completo de proveedores en el sistema.</summary>
        List<Proveedor> GetAll();

        /// <summary>Obtiene un proveedor específico utilizando su número de CUIT, retornando nulo si no se encuentra.</summary>
        Proveedor? GetByCuit(int cuit);

        /// <summary>Obtiene un proveedor específico utilizando su identificador único.</summary>
        Proveedor? GetById(Guid id);
    }
}