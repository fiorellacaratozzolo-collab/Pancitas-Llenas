using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services.Facade.Extensions;

namespace FormUI.Inicio
{
    public partial class FormBitácora : Form
    {
        /// <summary>
        /// Inicializa el formulario y sus componentes visuales base.
        /// </summary>
        public FormBitácora()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Consulta los registros de auditoría del sistema y los muestra en la grilla principal, ocultando las columnas de identificadores internos.
        /// </summary>
        private void btnVer_Click(object sender, EventArgs e)
        {
            try
            {
                Services.Bll.BitácoraBll bitacoraBll = new Services.Bll.BitácoraBll();
                dgvBitácora.DataSource = bitacoraBll.ListarBitacora();

                if (dgvBitácora.Columns.Count > 0)
                {
                    if (dgvBitácora.Columns["IdBitacora"] != null)
                        dgvBitácora.Columns["IdBitacora"].Visible = false;

                    if (dgvBitácora.Columns["IdUsuario"] != null)
                        dgvBitácora.Columns["IdUsuario"].Visible = false;

                    if (dgvBitácora.Columns["Mensaje"] != null)
                    {
                        dgvBitácora.Columns["Mensaje"].HeaderText = "Mensaje".Traducir();
                        dgvBitácora.Columns["Mensaje"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar la bitácora: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Evento de carga inicial del formulario que aplica las traducciones al idioma seleccionado por el usuario.
        /// </summary>
        private void FormBitácora_Load(object sender, EventArgs e)
        {
            TraductorUI.TraducirFormulario(this);
        }
    }
}
