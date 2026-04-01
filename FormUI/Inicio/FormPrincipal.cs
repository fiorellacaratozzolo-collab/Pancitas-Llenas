using Services.DomainModel.Composite;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormUI.Inicio
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Recorre el menú estático de WinForms y oculta lo que el usuario no puede ver.
        /// </summary>
        private void EvaluarPermisosMenu(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    // 1. ¿Es una categoría principal que tiene sub-opciones? (Ej: "Compras")
                    if (menuItem.HasDropDownItems)
                    {
                        // Llamada recursiva para evaluar a los "hijos" primero
                        EvaluarPermisosMenu(menuItem.DropDownItems);

                        // Si después de evaluar a los hijos, quedó al menos UNO visible, mostramos al padre.
                        // Si todos los hijos se ocultaron, ocultamos la categoría completa.
                        bool mostrarPadre = false;
                        foreach (ToolStripItem subItem in menuItem.DropDownItems)
                        {
                            if (subItem.Visible)
                            {
                                mostrarPadre = true;
                                break;
                            }
                        }
                        menuItem.Visible = mostrarPadre;
                    }
                    // 2. Es un botón de acción final (Ej: "Gestión Orden de Compra")
                    else
                    {
                        // Leemos el DataKey que configuraste en la propiedad Tag del diseñador
                        string dataKeyDelBoton = menuItem.Tag?.ToString();

                        if (!string.IsNullOrEmpty(dataKeyDelBoton))
                        {
                            // Consultamos al SessionManager si en su árbol recursivo existe este permiso
                            menuItem.Visible = SessionManager.Current.TienePermiso(dataKeyDelBoton);
                        }
                        else
                        {
                            // Si te olvidaste de ponerle el Tag en Visual Studio, lo ocultamos por seguridad
                            // (Opcional: puedes ponerlo en 'true' si quieres que botones sin tag sean públicos)
                            menuItem.Visible = false;
                        }
                    }
                }
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }
        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            var usuario = SessionManager.Current.UsuarioLogueado;

            if (usuario != null)
            {
                this.Text = $"PetShop - Bienvenido {usuario.Nombre}";

                // Disparamos la validación de todo el menú
                EvaluarPermisosMenu(menuStrip1.Items);
            }
        }    
        private void compraToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void sucursalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SessionManager.Current.Logout();
            this.Close(); // Cierra el menú principal y te devolverá al Login
        }
    }
}
