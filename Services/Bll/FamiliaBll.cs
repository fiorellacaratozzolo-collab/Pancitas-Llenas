using Services.Dal.Implementations;
using Services.DomainModel.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bll
{
    public class FamiliaBll
    {
        // Instanciamos el repositorio que me pasaste
        private readonly FamiliaRepository _familiaRepo;

        public FamiliaBll()
        {
            _familiaRepo = new FamiliaRepository();
        }

        public List<Familia> ObtenerTodas()
        {
            // Traemos el IEnumerable y lo convertimos a List para que la grilla lo lea mejor
            return _familiaRepo.GetAll().ToList();
        }
    }
}
