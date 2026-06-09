using System;
using Services.Dal.Interfaces;
using Services.DomainModel.Composite;

namespace Services.Dal.Implementations.Adapters
{
    /// <summary>
    /// Adaptador responsable de convertir los arreglos de datos de la base de datos en instancias de permisos individuales (Patente).
    /// </summary>
    internal class PatenteAdapter : IAdapter<Patente>
    {
        #region Singleton
        private readonly static PatenteAdapter _instance = new PatenteAdapter();

        /// <summary>
        /// Obtiene la instancia única del adaptador.
        /// </summary>
        public static PatenteAdapter Current
        {
            get { return _instance; }
        }

        private PatenteAdapter() { }
        #endregion

        /// <summary>
        /// Mapea un arreglo de valores a un objeto Patente, transformando sus datos primitivos y el enumerador de tipo de acceso.
        /// </summary>
        public Patente Get(object[] values)
        {
            Patente patente = new Patente();
            patente.Id = Guid.Parse(values[0].ToString());
            patente.DataKey = values[1].ToString();
            patente.Nombre = values[1].ToString();
            patente.TipoAcceso = (TipoAcceso)Enum.Parse(typeof(TipoAcceso), values[2].ToString());

            return patente;
        }
    }
}
