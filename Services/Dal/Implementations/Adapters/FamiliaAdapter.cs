using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Dal.Interfaces;
using Services.DomainModel.Composite;

namespace Services.Dal.Implementations.Adapters
{
    /// <summary>
    /// Adaptador responsable de instanciar roles (Familias) a partir de registros de base de datos, construyendo su estructura jerárquica subyacente.
    /// </summary>
    internal class FamiliaAdapter : IAdapter<Familia>
    {
        #region Singleton
        private readonly static FamiliaAdapter _instance = new FamiliaAdapter();

        /// <summary>
        /// Obtiene la instancia única del adaptador.
        /// </summary>
        public static FamiliaAdapter Current
        {
            get { return _instance; }
        }

        private FamiliaAdapter() { }
        #endregion

        /// <summary>
        /// Convierte un arreglo de valores en un objeto Familia, disparando la búsqueda recursiva de sus patentes y sub-familias asociadas mediante repositorios de unión.
        /// </summary>
        public Familia Get(object[] values)
        {
            Familia familia = new Familia();
            familia.Id = Guid.Parse(values[0].ToString());
            familia.Nombre = values[1].ToString();

            familia.AddRange(new FamiliaFamiliaRepository().GetByObject(familia));
            familia.AddRange(new FamiliaPatenteRepository().GetByObject(familia));

            return familia;
        }
    }
}
