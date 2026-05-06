using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Dal.Implementations;
using Services.Dal.Interfaces;
using Services.DomainModel.Composite;

namespace Services.Dal.Implementations.Adapters
{
    /// <summary>
    /// Adaptador responsable de convertir los arreglos de datos provenientes de la base de datos en instancias completas de la entidad Usuario, incluyendo sus relaciones.
    /// </summary>
    internal class UsuarioAdapter : IAdapter<Usuario>
    {
        #region Singleton
        private readonly static UsuarioAdapter _instance = new UsuarioAdapter();

        /// <summary>
        /// Obtiene la instancia única del adaptador.
        /// </summary>
        public static UsuarioAdapter Current
        {
            get { return _instance; }
        }

        private UsuarioAdapter() { }
        #endregion

        /// <summary>
        /// Convierte un arreglo de valores genéricos en un objeto Usuario, resolviendo relaciones nulas, asignando idiomas predeterminados y construyendo su árbol de privilegios.
        /// </summary>
        public Usuario Get(object[] values)
        {
            Guid? idSucursal = null;
            if (values.Length > 5 && values[5] != DBNull.Value)
            {
                idSucursal = Guid.Parse(values[5].ToString());
            }

            Usuario usuario = new Usuario(
                Guid.Parse(values[0].ToString()),
                values[1].ToString(),
                values[2].ToString(),
                values[3].ToString(),
                Convert.ToBoolean(values[4].ToString()),
                idSucursal
            );

            if (values.Length > 6 && values[6] != DBNull.Value)
            {
                usuario.IdiomaPredeterminado = values[6].ToString();
            }
            else
            {
                usuario.IdiomaPredeterminado = "es-AR";
            }

            usuario.Privilegios.AddRange(new UsuarioFamiliaRepository().GetByObject(usuario));
            usuario.Privilegios.AddRange(new UsuarioPatenteRepository().GetByObject(usuario));

            return usuario;
        }
    }
}

