using DataAccess.EntityFramework;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementations.SqlServer
{
    public class ProveedorRepository: IProveedorRepository
    {

        private readonly PetShopDBContext _context;

        public ProveedorRepository()
        {
            _context = new PetShopDBContext(); // Debe coincidir con la cadena en App.config
        }

        public Guid Create(Proveedor proveedor)
        {
            if (proveedor == null)
            {
                throw new ArgumentNullException(nameof(proveedor), "El proveedor no puede ser nulo.");
            }

            proveedor.IdProveedor = Guid.NewGuid(); // Asigna un nuevo ID al proveedor
            _context.Proveedors.Add(proveedor);
            _context.SaveChanges(); // Guarda el proveedor en la base de datos

            return proveedor.IdProveedor; // Retorna el ID generado
        }

        public List<Proveedor> GetAll()
        {
            // Usa ToList() para ejecutar la consulta de Entity Framework
            return _context.Proveedors.ToList();
        }

        public void Delete(Guid id)
        {
            var proveedor = _context.Proveedors.Find(id);
            if (proveedor != null)
            {
                // Borrado físico (NO RECOMENDADO para producción)
                _context.Proveedors.Remove(proveedor);

                // LÓGICA DE BORRADO LÓGICO (Ideal):
                // cliente.Activo = false; // Se necesita agregar 'public bool Activo { get; set; }' al modelo Cliente
                // _context.Entry(cliente).State = EntityState.Modified; 

                _context.SaveChanges();
            }
        }

        public Proveedor? GetByCuit(int cuit)
        {
            // 'Cuit' en la clase Proveedor es de tipo int (según tu código de alta anterior)
            // Usamos FirstOrDefault() que devolverá la primera coincidencia o null.
            return _context.Proveedors.FirstOrDefault(p => p.Cuit == cuit);
        }

        public Proveedor? GetById(Guid id)
        {
            // DbSet<Proveedor>.Find(id) busca por clave primaria y devuelve null si no existe.
            return _context.Proveedors.Find(id);
        }

    }

}
