using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define el contrato de persistencia para auditar los ingresos de mercadería al inventario.
    /// </summary>
    public interface IHistorialIngresoStockRepository
    {
        /// <summary>Registra una nueva entrada en el historial de ingresos de stock.</summary>
        void Create(HistorialIngresoStock historial);

        /// <summary>Recupera el historial completo de ingresos de mercadería del sistema.</summary>
        List<HistorialIngresoStock> GetAll();
    }
}
