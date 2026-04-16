using Logic.Facade;
using ModelsDTO;
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

namespace FormUI.FormVenta
{
    public partial class FormHistorialVentas : Form
    {
        private readonly VentaService _ventaService;
        private List<VentumDTO> _todasLasVentas = new List<VentumDTO>();
        public FormHistorialVentas()
        {
            InitializeComponent();
            _ventaService = new VentaService();
        }

        private void FormHistorialVentas_Load(object sender, EventArgs e)
        {

        }

        private void CargarVentas()
        {
            try
            {
                if (!SessionManager.Current.IdSucursalActual.HasValue)
                {
                    MessageBox.Show("Error: No se detectó una sucursal logueada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Guid miSucursal = SessionManager.Current.IdSucursalActual.Value;

                // Traemos TODAS las ventas
                _todasLasVentas = _ventaService.ObtenerVentasPorSucursal(miSucursal);

                // ¡Acá está el cambio! En lugar de filtrar, mostramos todo de una.
                dgvHistorialVentas.DataSource = null;
                dgvHistorialVentas.DataSource = _todasLasVentas;

                ConfigurarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el historial: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bntBuscar_Click(object sender, EventArgs e)
        {
            FiltrarPorFecha();
        }

        private void FiltrarPorFecha()
        {

            if (_todasLasVentas == null || _todasLasVentas.Count == 0) return;

            // Tomamos solo el DÍA, MES y AÑO (ignoramos la hora)
            DateTime fechaElegida = dtpFecha.Value.Date;

            // Filtramos con cuidado por los nulos
            var ventasFiltradas = _todasLasVentas
            .Where(v => v.FechaVenta.Date == fechaElegida)
            .ToList();

            dgvHistorialVentas.DataSource = null;
            dgvHistorialVentas.DataSource = ventasFiltradas;

            ConfigurarGrilla();

            if (ventasFiltradas.Count == 0)
            {
                MessageBox.Show("No se encontraron ventas para la fecha seleccionada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ConfigurarGrilla()
        {
            dgvHistorialVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Ocultar IDs (Ajustá los nombres a los de tu DTO)
            if (dgvHistorialVentas.Columns.Contains("IdVenta")) dgvHistorialVentas.Columns["IdVenta"].Visible = false;
            if (dgvHistorialVentas.Columns.Contains("IdCliente")) dgvHistorialVentas.Columns["IdCliente"].Visible = false;
            if (dgvHistorialVentas.Columns.Contains("IdSucursal")) dgvHistorialVentas.Columns["IdSucursal"].Visible = false;
            if (dgvHistorialVentas.Columns.Contains("IdClienteNavigation")) dgvHistorialVentas.Columns["IdClienteNavigation"].Visible = false;
            if (dgvHistorialVentas.Columns.Contains("VentaDetalles")) dgvHistorialVentas.Columns["VentaDetalles"].Visible = false;
            if (dgvHistorialVentas.Columns.Contains("NumeroVenta")) dgvHistorialVentas.Columns["NumeroVenta"].Visible = false;
            if (dgvHistorialVentas.Columns.Contains("MontoDescuento")) dgvHistorialVentas.Columns["MontoDescuento"].Visible = false;
            if (dgvHistorialVentas.Columns.Contains("EsMayorista")) dgvHistorialVentas.Columns["EsMayorista"].Visible = false;


            // Formatear columnas
            if (dgvHistorialVentas.Columns.Contains("FechaVenta"))
            {
                dgvHistorialVentas.Columns["FechaVenta"].HeaderText = "Fecha";
                dgvHistorialVentas.Columns["FechaVenta"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvHistorialVentas.Columns["FechaVenta"].DisplayIndex = 0;
            }

            if (dgvHistorialVentas.Columns.Contains("NombreCliente"))
            {
                dgvHistorialVentas.Columns["NombreCliente"].HeaderText = "Cliente";
                dgvHistorialVentas.Columns["NombreCliente"].DisplayIndex = 1;
            }

            if (dgvHistorialVentas.Columns.Contains("Total"))
            {
                dgvHistorialVentas.Columns["Total"].HeaderText = "Total ($)";
                dgvHistorialVentas.Columns["Total"].DefaultCellStyle.Format = "C2";
                dgvHistorialVentas.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvHistorialVentas.Columns["Total"].DisplayIndex = 2;
            }
        }

        private void btnVerTodas_Click(object sender, EventArgs e)
        {
            dgvHistorialVentas.DataSource = null;
            dgvHistorialVentas.DataSource = _todasLasVentas;
            ConfigurarGrilla();
        }

        private void FormHistorialVentas_Load_1(object sender, EventArgs e)
        {
            CargarVentas();
            dtpFecha.Value = DateTime.Today;
        }
    }
}
