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

namespace WinUI.WinForms
{
    public partial class Principal: Form
    {
        public Usuario Usuario {get; set;}
        public Principal(Usuario user)
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

        private void administradosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void gestiónToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tmsListarVentas_Click(object sender, EventArgs e)
        {

        }

        private void verStockSucursalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void solicitudDeOrdenDePedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
