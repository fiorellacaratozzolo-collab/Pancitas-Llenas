using Logic.Facade;
using Services.Facade;
using Services.Facade.Extensions;
using System.Configuration; 
using System.IO;            
using System.Data;
using System.Linq;

namespace FormUI.FormInventario
{
    public partial class FormHistorialMovimientos : Form
    {
        private readonly TraspasoService _traspasoService = new TraspasoService();
        private readonly InventarioService _inventarioService = new InventarioService();
        /// <summary>
        /// Inicializa el formulario y los servicios necesarios para consultar el historial de movimientos.
        /// </summary>
        public FormHistorialMovimientos()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Evento de carga inicial que desencadena la consulta de historiales y aplica las traducciones de la interfaz.
        /// </summary>
        private void FormHistorialMovimientos_Load(object sender, EventArgs e)
        {
            CargarTraspasos();
            CargarEntregas();
            TraductorUI.TraducirFormulario(this);
        }
        /// <summary>
        /// Fuerza la actualización manual de la grilla de historial de traspasos.
        /// </summary>
        private void btnVerTraspasos_Click(object sender, EventArgs e)
        {
            CargarTraspasos();
        }
        /// <summary>
        /// Verifica la sesión activa, obtiene el historial de traspasos de la sucursal actual y lo vincula a la grilla correspondiente.
        /// </summary>
        private void CargarTraspasos()
        {
            try
            {
                if (SessionManager.Current.IdSucursalActual == null)
                {
                    MessageBox.Show("Error: No se detectó una sucursal logueada.".Traducir(), "Error de Sesión".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Guid miSucursal = SessionManager.Current.IdSucursalActual.Value;
                var historialTraspasos = _traspasoService.ObtenerHistorialTraspasos(miSucursal);

                dgvTraspasoProductos.DataSource = null;
                dgvTraspasoProductos.DataSource = historialTraspasos;

                ConfigurarGrillaTraspasos();

                if (historialTraspasos.Count == 0)
                {
                    MessageBox.Show("No hay movimientos de traspasos registrados para esta sucursal.".Traducir(), "Información".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar los traspasos: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Oculta columnas técnicas, reordena los campos y aplica traducciones a los encabezados de la grilla de traspasos.
        /// </summary>
        private void ConfigurarGrillaTraspasos()
        {
            dgvTraspasoProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvTraspasoProductos.Columns.Contains("UsuarioResponsable"))
            {
                dgvTraspasoProductos.Columns["UsuarioResponsable"].Visible = false;
            }

            if (dgvTraspasoProductos.Columns.Contains("Fecha"))
            {
                dgvTraspasoProductos.Columns["Fecha"].HeaderText = "Fecha y Hora".Traducir();
                dgvTraspasoProductos.Columns["Fecha"].DisplayIndex = 0;
            }

            if (dgvTraspasoProductos.Columns.Contains("TipoMovimiento"))
            {
                dgvTraspasoProductos.Columns["TipoMovimiento"].HeaderText = "Movimiento".Traducir();
                dgvTraspasoProductos.Columns["TipoMovimiento"].DisplayIndex = 1;
            }

            if (dgvTraspasoProductos.Columns.Contains("SucursalInvolucrada"))
            {
                dgvTraspasoProductos.Columns["SucursalInvolucrada"].HeaderText = "Origen / Destino".Traducir();
                dgvTraspasoProductos.Columns["SucursalInvolucrada"].DisplayIndex = 2;
            }

            if (dgvTraspasoProductos.Columns.Contains("Producto"))
            {
                dgvTraspasoProductos.Columns["Producto"].HeaderText = "Producto".Traducir();
                dgvTraspasoProductos.Columns["Producto"].DisplayIndex = 3;
            }

            if (dgvTraspasoProductos.Columns.Contains("Marca"))
            {
                dgvTraspasoProductos.Columns["Marca"].HeaderText = "Marca".Traducir();
                dgvTraspasoProductos.Columns["Marca"].DisplayIndex = 4;
            }

            if (dgvTraspasoProductos.Columns.Contains("PesoNeto"))
            {
                dgvTraspasoProductos.Columns["PesoNeto"].HeaderText = "Peso Neto".Traducir();
                dgvTraspasoProductos.Columns["PesoNeto"].DisplayIndex = 5;
            }

            if (dgvTraspasoProductos.Columns.Contains("Unidad"))
            {
                dgvTraspasoProductos.Columns["Unidad"].HeaderText = "Unidad".Traducir();
                dgvTraspasoProductos.Columns["Unidad"].DisplayIndex = 6;
            }

            if (dgvTraspasoProductos.Columns.Contains("Cantidad"))
            {
                dgvTraspasoProductos.Columns["Cantidad"].HeaderText = "Cantidad".Traducir();
                dgvTraspasoProductos.Columns["Cantidad"].DisplayIndex = 7;


            }
        }
        /// <summary>
        /// Fuerza la actualización manual de la grilla de historial de entregas (ingresos de mercadería).
        /// </summary>
        private void btnVerEntrega_Click(object sender, EventArgs e)
        {
            CargarEntregas();
        }
        /// <summary>
        /// Valida la sesión actual, obtiene el historial de ingresos de inventario de la sucursal y lo muestra en pantalla.
        /// </summary>
        private void CargarEntregas()
        {
            try
            {
                if (SessionManager.Current.IdSucursalActual == null)
                {
                    MessageBox.Show("Error: No se detectó una sucursal logueada.".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Guid miSucursal = SessionManager.Current.IdSucursalActual.Value;
                var historialEntregas = _inventarioService.ObtenerHistorialEntregas(miSucursal);

                dgvEntregaProductos.DataSource = null;
                dgvEntregaProductos.DataSource = historialEntregas;

                ConfigurarGrillaEntregas();

                if (historialEntregas.Count == 0)
                {
                    MessageBox.Show("No hay ingresos de mercadería registrados para esta sucursal.".Traducir(), "Información".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar las entregas: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Aplica formatos visuales, redondeo de decimales y traducciones a las columnas de la grilla de entregas.
        /// </summary>
        private void ConfigurarGrillaEntregas()
        {
            dgvEntregaProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvEntregaProductos.Columns.Contains("Fecha"))
                dgvEntregaProductos.Columns["Fecha"].HeaderText = "Fecha de Ingreso".Traducir();

            if (dgvEntregaProductos.Columns.Contains("Cantidad"))
                dgvEntregaProductos.Columns["Cantidad"].HeaderText = "Cant. Agregada".Traducir();

            if (dgvEntregaProductos.Columns.Contains("Marca"))
                dgvEntregaProductos.Columns["Marca"].HeaderText = "Marca".Traducir();

            if (dgvEntregaProductos.Columns.Contains("PesoUnitario"))
            {
                dgvEntregaProductos.Columns["PesoUnitario"].HeaderText = "Peso Unit.".Traducir();
                dgvEntregaProductos.Columns["PesoUnitario"].DefaultCellStyle.Format = "N2";
            }
        }
        /// <summary>
        /// Exporta la grilla de Traspasos a formato Excel.
        /// </summary>
        private void btnExportarTraspasos_Click(object sender, EventArgs e)
        {
            ExportarDataGridViewAExcel(dgvTraspasoProductos, "Historial_Traspasos");
        }
        /// <summary>
        /// Exporta la grilla de Entregas a formato Excel.
        /// </summary>
        private void btnExportarEntrega_Click(object sender, EventArgs e)
        {
            ExportarDataGridViewAExcel(dgvEntregaProductos, "Historial_Entregas");
        }
        /// <summary>
        /// Método genérico que exporta el contenido visible de un DataGridView a un archivo CSV compatible con Excel.
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

                string? rutaBase = ConfigurationManager.AppSettings["RutaHistorialMovimientos"];

                // Fallback de seguridad al Escritorio si no está en el App.config
                if (string.IsNullOrWhiteSpace(rutaBase))
                {
                    rutaBase = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                }

                //Crea la carpeta si no existe
                if (!Directory.Exists(rutaBase))
                {
                    Directory.CreateDirectory(rutaBase);
                }

                //Crea el nombre del archivo con Fecha y Hora
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
                                           .Select(c => c.Value != null ? c.Value.ToString()?.Replace(";", ",") : ""); // Reemplazamos ';' por ',' en los datos para no romper las columnas

                            sw.WriteLine(string.Join(";", cells));
                        }
                    }
                }

                MessageBox.Show(string.Format("Los datos han sido exportados exitosamente a:\n{0}", rutaCompleta).Traducir(), "Exportación Exitosa".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al intentar exportar los datos: {0}", ex.Message).Traducir(), "Error de Exportación".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}