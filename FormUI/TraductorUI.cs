using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Services.Facade.Extensions;
using System.Threading.Tasks;

namespace FormUI
{
    /// <summary>
    /// Clase estática utilitaria encargada de recorrer y aplicar el sistema de internacionalización a los componentes visuales.
    /// </summary>
    public static class TraductorUI
    {
        /// <summary>
        /// Recorre recursivamente un control contenedor (como un Formulario o Panel) y traduce dinámicamente el texto visible de sus elementos de interfaz gráfica y encabezados.
        /// </summary>
        public static void TraducirFormulario(Control controlPadre)
        {
            if (controlPadre is Form form && !string.IsNullOrWhiteSpace(form.Text))
            {
                form.Text = form.Text.Traducir();
            }

            foreach (Control control in controlPadre.Controls)
            {
                if (control is Label || control is Button || control is CheckBox || control is RadioButton || control is GroupBox)
                {
                    if (!string.IsNullOrWhiteSpace(control.Text))
                    {
                        control.Text = control.Text.Traducir();
                    }
                }

                if (control is DataGridView dgv)
                {
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        if (!string.IsNullOrWhiteSpace(col.HeaderText))
                        {
                            col.HeaderText = col.HeaderText.Traducir();
                        }
                    }
                }

                if (control.HasChildren)
                {
                    TraducirFormulario(control);
                }
            }
        }
    }
}