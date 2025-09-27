using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.DomainModel.Composite;

namespace Services.Dal.Interfaces
{
    internal interface IPatenteRepository
    {
        Patente GetById(Guid id);
    }
}
