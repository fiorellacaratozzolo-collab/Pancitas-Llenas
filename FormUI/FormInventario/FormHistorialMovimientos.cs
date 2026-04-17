using Logic.Facade;
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

namespace FormUI.FormInventario
{
    public partial class FormHistorialMovimientos : Form
    {
        private readonly TraspasoService _traspasoService = new TraspasoService();
        private readonly InventarioService _inventarioService = new InventarioService();
        public FormHistorialMovimientos()
        {
            InitializeComponent();
        }

        private void FormHistorialMovimientos_Load(object sender, EventArgs e)
        {
            CargarTraspasos();
            CargarEntregas();
        }

        private void btnVerTraspasos_Click(object sender, EventArgs e)
        {
            CargarTraspasos();
        }

        private void CargarTraspasos()
        {
            try
            {
                // 1. Verificamos que haya una sucursal logueada
                if (SessionManager.Current.IdSucursalActual == null)
                {
                    MessageBox.Show("Error: No se detectó una sucursal logueada.", "Error de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Guid miSucursal = SessionManager.Current.IdSucursalActual.Value;

                // 2. Traemos el historial usando la lógica que creamos
                var historialTraspasos = _traspasoService.ObtenerHistorialTraspasos(miSucursal);

                // 3. Lo pegamos a la grilla
                dgvTraspasoProductos.DataSource = null; // Limpiamos por las dudas
                dgvTraspasoProductos.DataSource = historialTraspasos;

                // 4. Ponemos linda la grilla
                ConfigurarGrillaTraspasos();

                if (historialTraspasos.Count == 0)
                {
                    MessageBox.Show("No hay movimientos de traspasos registrados para esta sucursal.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los traspasos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrillaTraspasos()
        {
            // Hacemos que las columnas ocupen todo el ancho disponible
            dgvTraspasoProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Renombramos los encabezados para que se vean más limpios
            if (dgvTraspasoProductos.Columns.Contains("Fecha"))
                dgvTraspasoProductos.Columns["Fecha"].HeaderText = "Fecha y Hora";

            if (dgvTraspasoProductos.Columns.Contains("TipoMovimiento"))
                dgvTraspasoProductos.Columns["TipoMovimiento"].HeaderText = "Movimiento";

            if (dgvTraspasoProductos.Columns.Contains("SucursalInvolucrada"))
                dgvTraspasoProductos.Columns["SucursalInvolucrada"].HeaderText = "Origen / Destino";
        }

        private void btnVerEntrega_Click(object sender, EventArgs e)
        {
            CargarEntregas();
        }

        private void CargarEntregas()
        {
            try
            {
                if (SessionManager.Current.IdSucursalActual == null)
                {
                    MessageBox.Show("Error: No se detectó una sucursal logueada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Guid miSucursal = SessionManager.Current.IdSucursalActual.Value;
                var historialEntregas = _inventarioService.ObtenerHistorialEntregas(miSucursal);
                dgvEntregaProductos.DataSource = null;
                dgvEntregaProductos.DataSource = historialEntregas;

                ConfigurarGrillaEntregas();

                if (historialEntregas.Count == 0)
                {
                    MessageBox.Show("No hay ingresos de mercadería registrados para esta sucursal.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las entregas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrillaEntregas()
        {
            dgvEntregaProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvEntregaProductos.Columns.Contains("Fecha"))
                dgvEntregaProductos.Columns["Fecha"].HeaderText = "Fecha de Ingreso";

            if (dgvEntregaProductos.Columns.Contains("Cantidad"))
                dgvEntregaProductos.Columns["Cantidad"].HeaderText = "Cant. Agregada";

            if (dgvEntregaProductos.Columns.Contains("Marca"))
                dgvEntregaProductos.Columns["Marca"].HeaderText = "Marca";

            if (dgvEntregaProductos.Columns.Contains("PesoUnitario"))
            {
                dgvEntregaProductos.Columns["PesoUnitario"].HeaderText = "Peso Unit.";
                dgvEntregaProductos.Columns["PesoUnitario"].DefaultCellStyle.Format = "N2";
            }
        }
    }
}
