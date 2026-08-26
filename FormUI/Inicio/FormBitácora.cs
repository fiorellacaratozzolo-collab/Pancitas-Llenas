using Services.Facade.Extensions;
using System.Configuration;
using System.IO;

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

                    if (dgvBitácora.Columns["NombreUsuario"] != null)
                    {
                        dgvBitácora.Columns["NombreUsuario"].HeaderText = "Usuario".Traducir();
                    }

                    if (dgvBitácora.Columns["Fecha"] != null)
                    {
                        dgvBitácora.Columns["Fecha"].HeaderText = "Fecha".Traducir();
                    }

                    if (dgvBitácora.Columns["Criticidad"] != null)
                    {
                        dgvBitácora.Columns["Criticidad"].HeaderText = "Criticidad".Traducir();
                    }

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
        /// <summary>
        /// Ejecuta la exportación de los datos visibles en la grilla de la bitácora hacia un archivo Excel (CSV).
        /// </summary>
        private void btnExportarBitacora_Click(object sender, EventArgs e)
        {
            ExportarDataGridViewAExcel(dgvBitácora, "Registro_Auditoria");
        }
        /// <summary>
        /// Método que exporta el contenido visible de un DataGridView a un archivo CSV compatible con Excel.
        /// </summary>
        private void ExportarDataGridViewAExcel(DataGridView dgv, string prefijoNombre)
        {
            try
            {
                if (dgv.Rows.Count == 0 || dgv.DataSource == null)
                {
                    MessageBox.Show("No hay datos en la tabla para exportar.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string? rutaBase = ConfigurationManager.AppSettings["RutaBitácora"];

                // Fallback de seguridad al Escritorio si no está configurado en el App.config
                if (string.IsNullOrWhiteSpace(rutaBase))
                {
                    rutaBase = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                }

                // Crea la carpeta si no existe
                if (!Directory.Exists(rutaBase))
                {
                    Directory.CreateDirectory(rutaBase);
                }

                string fechaStr = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string nombreArchivo = string.Format("{0}_{1}.csv", prefijoNombre, fechaStr);
                string rutaCompleta = Path.Combine(rutaBase, nombreArchivo);
                using (StreamWriter sw = new StreamWriter(rutaCompleta, false, System.Text.Encoding.UTF8))
                {
                    var headers = dgv.Columns.Cast<DataGridViewColumn>()
                                     .Where(c => c.Visible)
                                     .Select(c => c.HeaderText);

                    sw.WriteLine(string.Join(";", headers));
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            var cells = row.Cells.Cast<DataGridViewCell>()
                                           .Where(c => dgv.Columns[c.ColumnIndex].Visible)
                                           .Select(c => c.Value != null ? c.Value.ToString()?.Replace(";", ",") : ""); // Reemplazamos ';' por ',' para evitar ruptura de columnas

                            sw.WriteLine(string.Join(";", cells));
                        }
                    }
                }

                MessageBox.Show(string.Format("El registro de auditoría se ha exportado exitosamente a:\n{0}", rutaCompleta).Traducir(), "Exportación Exitosa".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al intentar exportar los datos: {0}", ex.Message).Traducir(), "Error de Exportación".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}