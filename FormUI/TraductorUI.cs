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
        public static void TraducirFormulario(Control controlPadre)
        {
            if (controlPadre is Form form && !string.IsNullOrWhiteSpace(form.Text))
            {
                form.Text = form.Text.Traducir();
            }
            TraducirControlesRecursivo(controlPadre.Controls);
        }

        /// <summary>
        /// Recorre absolutamente todos los controles, entrando en paneles, pestañas y menús.
        /// </summary>
        private static void TraducirControlesRecursivo(Control.ControlCollection controles)
        {
            foreach (Control control in controles)
            {
                if (control is Label || control is Button || control is CheckBox || control is RadioButton || control is GroupBox)
                {
                    if (!string.IsNullOrWhiteSpace(control.Text))
                    {
                        string textoOriginal = control.Text;
                        bool terminaConDosPuntos = textoOriginal.EndsWith(":");
                        string textoLimpio = terminaConDosPuntos ? textoOriginal.TrimEnd(':').Trim() : textoOriginal;

                        string textoTraducido = textoLimpio.Traducir();
                        control.Text = terminaConDosPuntos ? textoTraducido + ":" : textoTraducido;
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

                if (control is MenuStrip menuStrip)
                {
                    foreach (ToolStripItem item in menuStrip.Items)
                    {
                        TraducirMenuRecursivo(item);
                    }
                }

                if (control.HasChildren)
                {
                    TraducirControlesRecursivo(control.Controls);
                }
            }
        }

        /// <summary>
        /// Recorre de forma recursiva un botón del menú y todos sus sub-botones desplegables.
        /// </summary>
        private static void TraducirMenuRecursivo(ToolStripItem item)
        {
            if (!string.IsNullOrWhiteSpace(item.Text))
            {
                item.Text = item.Text.Traducir();
            }

            if (item is ToolStripMenuItem menuItem && menuItem.HasDropDownItems)
            {
                foreach (ToolStripItem subItem in menuItem.DropDownItems)
                {
                    TraducirMenuRecursivo(subItem);
                }
            }
        }
    }
}