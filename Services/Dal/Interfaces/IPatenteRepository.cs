using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.DomainModel.Composite;

namespace Services.Dal.Interfaces
{
    /// <summary>
    /// Define el contrato para las operaciones de acceso a datos de los permisos individuales (Patentes).
    /// </summary>
    public interface IPatenteRepository : IGenericRepository<Patente>
    {
    }
}
