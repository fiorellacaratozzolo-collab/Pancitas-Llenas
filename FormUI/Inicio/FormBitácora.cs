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
    public partial class FormBitácora : Form
    {
        public FormBitácora()
        {
            InitializeComponent();
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Instanciamos la BLL
                Services.Bll.BitácoraBll bitacoraBll = new Services.Bll.BitácoraBll();

                // 2. Le asignamos la lista a la grilla
                dgvBitácora.DataSource = bitacoraBll.ListarBitacora();

                // 3. (Opcional pero recomendado) Emprolijar la grilla
                if (dgvBitácora.Columns.Count > 0)
                {
                    // Ocultamos el ID de la Bitácora
                    if (dgvBitácora.Columns["IdBitacora"] != null)
                        dgvBitácora.Columns["IdBitacora"].Visible = false;

                    // Ocultamos la columna del ID del usuario porque al Admin no le sirve ver un GUID
                    if (dgvBitácora.Columns["IdUsuario"] != null)
                        dgvBitácora.Columns["IdUsuario"].Visible = false;

                    // Ajustamos el tamaño de la columna del mensaje para que se lea bien
                    if (dgvBitácora.Columns["Mensaje"] != null)
                        dgvBitácora.Columns["Mensaje"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
    }
}
