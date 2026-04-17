using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IHistorialIngresoStockRepository
    {
        void Create(HistorialIngresoStock historial);
        List<HistorialIngresoStock> GetAll();
    }
}
