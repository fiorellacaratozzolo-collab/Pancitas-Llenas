using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Services.Facade.Extensions;
using System.Threading.Tasks;

namespace FormUI
{
    public static class TraductorUI
    {
        public static void TraducirFormulario(Control controlPadre)
        {
            // 1. Traducimos el título de la ventana si es un Form
            if (controlPadre is Form form && !string.IsNullOrWhiteSpace(form.Text))
            {
                form.Text = form.Text.Traducir();
            }

            foreach (Control control in controlPadre.Controls)
            {
                // 2. Solo traducimos el texto si el control es puramente de Interfaz Gráfica
                if (control is Label || control is Button || control is CheckBox || control is RadioButton || control is GroupBox)
                {
                    if (!string.IsNullOrWhiteSpace(control.Text))
                    {
                        control.Text = control.Text.Traducir();
                    }
                }

                // 3. Si es una grilla, solo le tocamos los Encabezados (HeaderText), NUNCA los datos de adentro
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

                // 4. Si el control tiene otros controles adentro (ej. un Panel), usamos recursividad
                if (control.HasChildren)
                {
                    TraducirFormulario(control);
                }
            }
        }
    }
}
