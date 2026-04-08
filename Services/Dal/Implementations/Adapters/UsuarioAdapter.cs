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
            // Si el valor es DBNull, lo dejamos como null. Si tiene un dato, lo convertimos a Guid.
            Guid? idSucursal = null;

            // Verificamos que el arreglo tenga al menos 6 posiciones y que no sea nulo en BD
            if (values.Length > 5 && values[5] != DBNull.Value)
            {
                idSucursal = Guid.Parse(values[5].ToString());
            }

            // 2. Instanciamos el Usuario
            Usuario usuario = new Usuario(
                Guid.Parse(values[0].ToString()),
                values[1].ToString(),
                values[2].ToString(),
                values[3].ToString(),
                Convert.ToBoolean(values[4].ToString()),
                idSucursal 
            );

            // Buscamos los roles (familias) y excepciones (patentes) asignadas al usuario
            usuario.Privilegios.AddRange(new UsuarioFamiliaRepository().GetByObject(usuario));
            usuario.Privilegios.AddRange(new UsuarioPatenteRepository().GetByObject(usuario));

            return usuario;
        }
    }
}

