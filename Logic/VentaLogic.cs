using DataAccess.Implementations.SqlServer;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace Logic
{
    public class VentaLogic
    {
        // Usamos la interfaz del repositorio para mantener el acoplamiento bajo
        private readonly IVentaRepository _ventaRepository;

        public VentaLogic()
        {
            // Instanciación directa.
            _ventaRepository = new VentaRepository();
        }

        public VentaLogic(IVentaRepository ventaRepository)
        {
            // Constructor para Inyección de Dependencia
            _ventaRepository = ventaRepository;
        }

        /// <summary>
        /// Procesa y registra una nueva venta, aplicando validaciones de negocio.
        /// </summary>
        /// <param name="venta">La entidad Ventum con datos principales.</param>
        /// <param name="detalles">La lista de VentaDetalle.</param>
        /// <returns>El ID (Guid) de la Venta creada.</returns>
        //public Guid RegistrarVenta(Ventum venta, List<VentaDetalle> detalles)
        //{
        //    1.Validación de Reglas de Negocio

        //    if (venta == null || detalles == null || !detalles.Any())
        //    {
        //        throw new ArgumentException("La Venta y sus Detalles no pueden ser nulos o estar vacíos.");
        //    }

        //    Ejemplo de validación de Totales:
        //    decimal totalCalculado = detalles.Sum(d => d.Subtotal);
        //    if (venta.Total != totalCalculado)
        //    {
        //    NOTA: Esto podría ser un error de redondeo, pero es una buena práctica validarlo.
        //         Aquí, el negocio decide si el total de la Venta debe ser calculado por la lógica, 
        //         o si se acepta el valor de la UI tras una validación.
        //         throw new InvalidOperationException("El total de la Venta no coincide con la suma de los subtotales.");
        //    }

        //Opcional:
        //    -Verificar Stock de cada producto.
        //         -Aplicar reglas de descuento basadas en el cliente(si es Mayorista).
        //         - Validar que IdCliente e IdTipoVenta sean válidos.

        //        // 2. Delegar la Persistencia al Repositorio
        //        try
        //    {
        //        Guid idVenta = _ventaRepository.Create(venta, detalles);
        //        return idVenta;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Loguear la excepción de persistencia antes de re-lanzarla o manejarla
        //        // throw new Exception("Fallo al guardar la venta en la base de datos.", ex);
        //        throw;
        //    }
        //}
    }
}
