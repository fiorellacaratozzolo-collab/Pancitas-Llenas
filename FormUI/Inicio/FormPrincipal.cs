using Services.DomainModel.Composite;
using Services.Bll;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
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
                    // 1. Leemos el Tag
                    string tag = menuItem.Tag?.ToString() ?? "";

                    // 2. ¿Tiene permiso? (Si no hay Tag, por ahora lo dejamos ver para no trabarte)
                    bool tienePermiso = string.IsNullOrEmpty(tag) || SessionManager.Current.TienePermiso(tag);
                    menuItem.Visible = tienePermiso;

                    // 3. RECURSIVIDAD: Si tiene sub-elementos, entramos a evaluarlos primero
                    if (menuItem.HasDropDownItems)
                    {
                        EvaluarPermisosMenu(menuItem.DropDownItems);

                        // Lógica de "Padre Visible": Si el padre tiene permiso, se queda.
                        // Si el padre no tiene Tag, pero algún hijo es visible, el padre se muestra.
                        bool algunHijoVisible = false;
                        foreach (ToolStripItem sub in menuItem.DropDownItems)
                        {
                            if (sub.Visible) { algunHijoVisible = true; break; }
                        }

                        if (!string.IsNullOrEmpty(tag))
                            menuItem.Visible = tienePermiso;
                        else
                            menuItem.Visible = algunHijoVisible;
                    }

                }
            }
        }

        private void MenuPatente_Click(object sender, EventArgs e)
        {
            

            if (sender is not ToolStripMenuItem itemClickeado) return;

            string nombreForm = itemClickeado.Tag?.ToString() ?? "";

            if (string.IsNullOrEmpty(nombreForm))
            {
                MessageBox.Show("Falta el Tag en el diseñador.", "Aviso");
                return;
            }

            try
            {
                var tipoForm = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => t.Name == nombreForm && typeof(Form).IsAssignableFrom(t));

                if (tipoForm != null)
                {
                    Form formulario = (Form)Activator.CreateInstance(tipoForm)!;
                    formulario.ShowDialog();
                }
                else
                {
                    MessageBox.Show($"No se encontró la clase '{nombreForm}'.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir: {ex.Message}");
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
                EvaluarPermisosMenu(menuStrip.Items);

                Services.Bll.BitácoraBll bitacora = new Services.Bll.BitácoraBll();
                bitacora.RegistrarLog(
                    mensaje: $"El usuario {usuario.Nombre} ingresó al sistema.",
                    criticidad: Services.DomainModel.Logging.Criticidad.Info,
                    idUsuario: usuario.IdUsuario);
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
            // Registramos en la bitácora que el usuario cerró sesión (si es que había uno logueado)
            var usuario = SessionManager.Current.UsuarioLogueado;
            if (usuario != null)
            {
                Services.Bll.BitácoraBll bitacora = new Services.Bll.BitácoraBll();
                bitacora.RegistrarLog(
                    mensaje: $"El usuario {usuario.Nombre} cerró sesión manualmente.",
                    criticidad: Services.DomainModel.Logging.Criticidad.Info,
                    idUsuario: usuario.IdUsuario
                );
            }

            SessionManager.Current.Logout();
            this.Close(); // Cierra el menú principal y te devuelve al Login
        }
    }
}
