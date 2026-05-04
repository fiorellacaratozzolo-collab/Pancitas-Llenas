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
    internal class UsuarioAdapter : IAdapter<Usuario>
    {
        #region Singleton
        private readonly static UsuarioAdapter _instance = new UsuarioAdapter();

        public static UsuarioAdapter Current
        {
            get { return _instance; }
        }

        private UsuarioAdapter() { }
        #endregion

        public Usuario Get(object[] values)
        {
            // 1. Lógica para la Sucursal (se mantiene igual, es la posición 5)
            Guid? idSucursal = null;
            if (values.Length > 5 && values[5] != DBNull.Value)
            {
                idSucursal = Guid.Parse(values[5].ToString());
            }

            // 2. Instanciamos el Usuario (con el constructor que ya tenés)
            Usuario usuario = new Usuario(
                Guid.Parse(values[0].ToString()),
                values[1].ToString(),
                values[2].ToString(),
                values[3].ToString(),
                Convert.ToBoolean(values[4].ToString()),
                idSucursal
            );

            // 3. NUEVO: Asignamos el idioma predeterminado (Posición 6)
            // Usamos un salvavidas: si en la BD es NULL, le asignamos "es-AR" por defecto
            if (values.Length > 6 && values[6] != DBNull.Value)
            {
                usuario.IdiomaPredeterminado = values[6].ToString();
            }
            else
            {
                usuario.IdiomaPredeterminado = "es-AR";
            }

            // 4. Cargamos privilegios (se mantiene igual)
            usuario.Privilegios.AddRange(new UsuarioFamiliaRepository().GetByObject(usuario));
            usuario.Privilegios.AddRange(new UsuarioPatenteRepository().GetByObject(usuario));

            return usuario;
        }
    }
}

