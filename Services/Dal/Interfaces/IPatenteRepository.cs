using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.DomainModel.Composite;

namespace Services.Dal.Interfaces
{
    public interface IPatenteRepository : IGenericRepository<Patente>
    {
        // Espacio para métodos exclusivos de Patente si fueran necesarios
    }
}
