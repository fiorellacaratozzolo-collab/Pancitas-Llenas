using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services.DomainModel.Composite;

namespace FormUI.Inicio
{
    public partial class FormPrincipal : Form
    {
        public Usuario Usuario { get; set; }
        public FormPrincipal(Usuario user)
        {
            Usuario = user;
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            this.Text = $"Bienvenido {Usuario.Nombre}";
            //Cargamos los privilegios del usuario en el menú
            foreach (var item in Usuario.Patentes)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(item.DataKey);
                menuStrip1.Items.Add(menuItem);
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
    }
}
