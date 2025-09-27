using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IProveedorRepository
    {
        /// <summary>
        /// Interfaz para las operaciones de acceso a datos relacionadas con los proveedores.
        /// Define los métodos CRUD básicos para trabajar con la entidad Proveedor.
        /// </summary>
        Guid Create(Proveedor proveedor);
        void Delete(Guid id);
        List<Proveedor> GetAll();
        public Proveedor? GetByCuit(int cuit);
        Proveedor? GetById(Guid id);
    }
}
