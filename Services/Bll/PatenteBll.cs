using Services.Dal.Implementations;
using Services.DomainModel.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bll
{
    public class PatenteBll
    {
        private readonly PatenteRepository _patenteRepo;

        public PatenteBll()
        {
            _patenteRepo = new PatenteRepository();
        }

        public List<Patente> ObtenerTodas()
        {
            return _patenteRepo.GetAll().ToList();
        }
    }
}
