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
    public partial class FormGestiónVenta : Form
    {
        private readonly VentaService _ventaService = new VentaService();
        public FormGestiónVenta()
        {
            InitializeComponent();
        }

        private void FormGestiónVenta_Load(object sender, EventArgs e)
        {

        }

        // Método centralizado para buscar ventas
        private void CargarVentasFiltradas(DateTime fecha)
        {
            Guid? idSucursal = SessionManager.Current.IdSucursalActual;
            if (idSucursal == null) return;
            List<VentumDTO> listaVentas = _ventaService.GetVentasPorSucursalYFecha(idSucursal.Value, fecha);

            dgvVentasRealizadas.DataSource = listaVentas;
            ConfigurarColumnasGrilla();
        }

        private void ConfigurarGrillaDetalles()
        {
            if (dgvDetallesVenta.Columns.Contains("IdVentaDetalle")) dgvDetallesVenta.Columns["IdVentaDetalle"].Visible = false;
            if (dgvDetallesVenta.Columns.Contains("IdVenta")) dgvDetallesVenta.Columns["IdVenta"].Visible = false;
            if (dgvDetallesVenta.Columns.Contains("IdProducto")) dgvDetallesVenta.Columns["IdProducto"].Visible = false;
            if (dgvDetallesVenta.Columns.Contains("IdProductoNavigation")) dgvDetallesVenta.Columns["IdProductoNavigation"].Visible = false;
            if (dgvDetallesVenta.Columns.Contains("IdVentaNavigation")) dgvDetallesVenta.Columns["IdVentaNavigation"].Visible = false;
            if (dgvDetallesVenta.Columns.Contains("Producto")) dgvDetallesVenta.Columns["Producto"].Visible = false;


            dgvDetallesVenta.ReadOnly = true;
            dgvDetallesVenta.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetallesVenta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvDetallesVenta.Columns.Contains("NombreProducto"))
            {
                dgvDetallesVenta.Columns["NombreProducto"].HeaderText = "Producto";
                dgvDetallesVenta.Columns["NombreProducto"].DisplayIndex = 0;
                dgvDetallesVenta.Columns["NombreProducto"].Visible = true;
            }
            if (dgvDetallesVenta.Columns.Contains("PrecioUnitario"))
            {
                dgvDetallesVenta.Columns["PrecioUnitario"].DefaultCellStyle.Format = "C2";
            }
            if (dgvDetallesVenta.Columns.Contains("Subtotal"))
            {
                dgvDetallesVenta.Columns["Subtotal"].DefaultCellStyle.Format = "C2";
            }
        }

        private void ConfigurarColumnasGrilla()
        {
            // Ocultamos todo lo técnico
            if (dgvVentasRealizadas.Columns.Contains("IdVenta")) dgvVentasRealizadas.Columns["IdVenta"].Visible = false;
            if (dgvVentasRealizadas.Columns.Contains("IdSucursal")) dgvVentasRealizadas.Columns["IdSucursal"].Visible = false;
            if (dgvVentasRealizadas.Columns.Contains("IdCliente")) dgvVentasRealizadas.Columns["IdCliente"].Visible = false;
            if (dgvVentasRealizadas.Columns.Contains("EsMayorista")) dgvVentasRealizadas.Columns["EsMayorista"].Visible = false;
            if (dgvVentasRealizadas.Columns.Contains("IdClienteNavigation")) dgvVentasRealizadas.Columns["IdClienteNavigation"].Visible = false;
            if (dgvVentasRealizadas.Columns.Contains("VentaDetalles")) dgvVentasRealizadas.Columns["VentaDetalles"].Visible = false;
            if (dgvVentasRealizadas.Columns.Contains("MontoDescuento")) dgvVentasRealizadas.Columns["MontoDescuento"].Visible = false;

            // Formatos bonitos
            if (dgvVentasRealizadas.Columns.Contains("FechaVenta"))
            {
                dgvVentasRealizadas.Columns["FechaVenta"].HeaderText = "Hora";
                dgvVentasRealizadas.Columns["FechaVenta"].DefaultCellStyle.Format = "HH:mm"; 
            }

            if (dgvVentasRealizadas.Columns.Contains("Total"))
            {
                dgvVentasRealizadas.Columns["Total"].DefaultCellStyle.Format = "N2";
            }

            dgvVentasRealizadas.ReadOnly = true;
            dgvVentasRealizadas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVentasRealizadas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dateTimePickerVenta_ValueChanged(object sender, EventArgs e)
        {
            CargarVentasFiltradas(dateTimePickerVenta.Value.Date);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarVentasFiltradas(dateTimePickerVenta.Value.Date);
            MessageBox.Show("Lista actualizada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvVentasRealizadas_DoubleClick(object sender, EventArgs e)
        {

        }

        private void dgvVentasRealizadas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvVentasRealizadas_SelectionChanged(object sender, EventArgs e)
        {
            // Verificamos que haya una fila seleccionada
            if (dgvVentasRealizadas.CurrentRow != null && dgvVentasRealizadas.CurrentRow.DataBoundItem is VentumDTO ventaElegida)
            {
                // Usamos la relación para traer los detalles de ESA venta específica
                var detalles = _ventaService.GetDetallesDeVenta(ventaElegida.IdVenta);

                // Llenamos la grilla de abajo
                dgvDetallesVenta.DataSource = detalles;
                ConfigurarGrillaDetalles();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // 1. Validar que haya una venta seleccionada
            if (dgvVentasRealizadas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una venta de la lista para anular.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Extraer datos
            VentumDTO ventaElegida = (VentumDTO)dgvVentasRealizadas.CurrentRow.DataBoundItem;
            Guid? idSucursal = SessionManager.Current.IdSucursalActual;

            if (idSucursal == null) return;

            // 3. Confirmación de Seguridad
            DialogResult confirmacion = MessageBox.Show(
                $"¿Está ABSOLUTAMENTE SEGURO de anular esta venta por un total de $ {ventaElegida.Total:N2}?\n\nLos productos regresarán al stock de la sucursal.",
                "Confirmar Anulación de Venta",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button2);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    // 4. Llamamos a la lógica
                    _ventaService.AnularVenta(ventaElegida.IdVenta, idSucursal.Value);

                    MessageBox.Show("Venta anulada correctamente. El stock ha sido devuelto.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 5. Recargamos la grilla para que desaparezca
                    CargarVentasFiltradas(dateTimePickerVenta.Value.Date);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al anular la venta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FormGestiónVenta_Load_1(object sender, EventArgs e)
        {
            try
            {
                btnEliminar.Enabled = SessionManager.Current.TienePermiso("Anular_Ventas");
                dateTimePickerVenta.Value = DateTime.Today;
                CargarVentasFiltradas(DateTime.Today);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la pantalla: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
