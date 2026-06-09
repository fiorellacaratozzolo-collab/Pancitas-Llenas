using Logic.Facade;
using ModelsDTO;
using Services.Facade;
using System.Data;
using Services.Facade.Extensions;

namespace FormUI.FormVenta
{
    public partial class FormHistorialVentas : Form
    {
        private readonly VentaService _ventaService;
        private List<VentumDTO> _todasLasVentas = new List<VentumDTO>();

        /// <summary>
        /// Inicializa el formulario y el servicio necesario para la gestión del historial de ventas.
        /// </summary>
        public FormHistorialVentas()
        {
            InitializeComponent();
            dgvHistorialVentas.CellFormatting += dgvHistorialVentas_CellFormatting;
            _ventaService = new VentaService();
        }
        /// <summary>
        /// Verifica la sesión actual, obtiene el historial completo de ventas de la sucursal y lo carga en la grilla.
        /// </summary>
        private void CargarVentas()
        {
            try
            {
                if (!SessionManager.Current.IdSucursalActual.HasValue)
                {
                    MessageBox.Show("Error: No se detectó una sucursal logueada.".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Guid miSucursal = SessionManager.Current.IdSucursalActual.Value;
                _todasLasVentas = _ventaService.ObtenerVentasPorSucursal(miSucursal);
                dgvHistorialVentas.DataSource = null;
                dgvHistorialVentas.DataSource = _todasLasVentas;

                ConfigurarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar el historial: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Ejecuta el proceso de filtrado de ventas basándose en la fecha seleccionada por el usuario.
        /// </summary>
        private void bntBuscar_Click(object sender, EventArgs e)
        {
            FiltrarPorFecha();
        }
        /// <summary>
        /// Filtra la lista de ventas en memoria para mostrar únicamente aquellas que coincidan con la fecha exacta del selector.
        /// </summary>
        private void FiltrarPorFecha()
        {
            if (_todasLasVentas == null || _todasLasVentas.Count == 0) return;

            DateTime fechaElegida = dtpFecha.Value.Date;

            var ventasFiltradas = _todasLasVentas
            .Where(v => v.FechaVenta.Date == fechaElegida)
            .ToList();

            dgvHistorialVentas.DataSource = null;
            dgvHistorialVentas.DataSource = ventasFiltradas;

            ConfigurarGrilla();

            if (ventasFiltradas.Count == 0)
            {
                MessageBox.Show("No se encontraron ventas para la fecha seleccionada.".Traducir(), "Información".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Oculta identificadores internos y aplica formatos visuales y traducciones a las columnas de la grilla principal.
        /// </summary>
        private void ConfigurarGrilla()
        {
            dgvHistorialVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvHistorialVentas.Columns.Contains("IdVenta")) dgvHistorialVentas.Columns["IdVenta"].Visible = false;
            if (dgvHistorialVentas.Columns.Contains("IdCliente")) dgvHistorialVentas.Columns["IdCliente"].Visible = false;
            if (dgvHistorialVentas.Columns.Contains("IdSucursal")) dgvHistorialVentas.Columns["IdSucursal"].Visible = false;
            if (dgvHistorialVentas.Columns.Contains("IdClienteNavigation")) dgvHistorialVentas.Columns["IdClienteNavigation"].Visible = false;
            if (dgvHistorialVentas.Columns.Contains("VentaDetalles")) dgvHistorialVentas.Columns["VentaDetalles"].Visible = false;
            if (dgvHistorialVentas.Columns.Contains("NumeroVenta")) dgvHistorialVentas.Columns["NumeroVenta"].Visible = false;
            if (dgvHistorialVentas.Columns.Contains("MontoDescuento")) dgvHistorialVentas.Columns["MontoDescuento"].Visible = false;

            if (dgvHistorialVentas.Columns.Contains("FechaVenta"))
            {
                dgvHistorialVentas.Columns["FechaVenta"].HeaderText = "Fecha".Traducir();
                dgvHistorialVentas.Columns["FechaVenta"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvHistorialVentas.Columns["FechaVenta"].DisplayIndex = 0;
            }

            if (dgvHistorialVentas.Columns.Contains("NombreCliente"))
            {
                dgvHistorialVentas.Columns["NombreCliente"].HeaderText = "Cliente".Traducir();
                dgvHistorialVentas.Columns["NombreCliente"].DisplayIndex = 1;
            }

            if (dgvHistorialVentas.Columns.Contains("EsMayorista"))
            {
                if (dgvHistorialVentas.Columns["EsMayorista"] is DataGridViewCheckBoxColumn)
                {
                    int indiceAnterior = dgvHistorialVentas.Columns["EsMayorista"].Index;
                    dgvHistorialVentas.Columns.Remove("EsMayorista");
                    DataGridViewTextBoxColumn colTexto = new DataGridViewTextBoxColumn();
                    colTexto.Name = "EsMayorista";
                    colTexto.DataPropertyName = "EsMayorista";
                    dgvHistorialVentas.Columns.Insert(indiceAnterior, colTexto);
                }
                dgvHistorialVentas.Columns["EsMayorista"].HeaderText = "Tipo de Venta".Traducir();
                dgvHistorialVentas.Columns["EsMayorista"].DisplayIndex = 2;
                dgvHistorialVentas.Columns["EsMayorista"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvHistorialVentas.Columns.Contains("Total"))
            {
                dgvHistorialVentas.Columns["Total"].HeaderText = "Total ($)".Traducir();
                dgvHistorialVentas.Columns["Total"].DefaultCellStyle.Format = "C2";
                dgvHistorialVentas.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvHistorialVentas.Columns["Total"].DisplayIndex = 3;
            }
        }        
        /// <summary>
        /// Intercepta el dibujado de las celdas para transformar el valor booleano 'EsMayorista' en una etiqueta legible.
        /// </summary>
        private void dgvHistorialVentas_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvHistorialVentas.Columns[e.ColumnIndex].Name == "EsMayorista" && e.Value != null)
            {
                if (e.Value is bool esMayorista)
                {
                    e.Value = esMayorista ? "Mayorista".Traducir() : "Minorista".Traducir();
                    e.FormattingApplied = true;
                }
            }
        }
        /// <summary>
        /// Restablece la vista de la grilla para mostrar el historial completo de ventas sin filtros aplicados.
        /// </summary>
        private void btnVerTodas_Click(object sender, EventArgs e)
        {
            dgvHistorialVentas.DataSource = null;
            dgvHistorialVentas.DataSource = _todasLasVentas;
            ConfigurarGrilla();
        }
        /// <summary>
        /// Evento de carga inicial del formulario que obtiene los datos históricos, ajusta la fecha y traduce la interfaz.
        /// </summary>
        private void FormHistorialVentas_Load_1(object sender, EventArgs e)
        {
            CargarVentas();
            dtpFecha.Value = DateTime.Today;
            TraductorUI.TraducirFormulario(this);
        }
    }
}
