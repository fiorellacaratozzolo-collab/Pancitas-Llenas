using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementations.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        // 1. Exponer todos los Repositorios que maneja
        IProductoRepository Productos { get; }
        IProveedorRepository Proveedores { get; }
        ISucursalRepository Sucursales { get; }
        IClienteRepository Clientes { get; }

        // 2. Método para confirmar la transacción
        int Complete();
    }
}
