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
using Services.Facade.Extensions;

namespace FormUI.FormVenta
{
    public partial class FormGestiónVenta : Form
    {
        private readonly VentaService _ventaService = new VentaService();

        /// <summary>
        /// Inicializa el formulario y los componentes visuales principales.
        /// </summary>
        public FormGestiónVenta()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Consulta las ventas realizadas en la sucursal actual para una fecha específica y actualiza la grilla principal.
        /// </summary>
        private void CargarVentasFiltradas(DateTime fecha)
        {
            Guid? idSucursal = SessionManager.Current.IdSucursalActual;
            if (idSucursal == null) return;
            List<VentumDTO> listaVentas = _ventaService.GetVentasPorSucursalYFecha(idSucursal.Value, fecha);

            dgvVentasRealizadas.DataSource = listaVentas;
            ConfigurarColumnasGrilla();
        }
        /// <summary>
        /// Oculta identificadores internos, aplica formatos monetarios y traducciones a los encabezados de la grilla de detalles de la venta.
        /// </summary>
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
                dgvDetallesVenta.Columns["NombreProducto"].HeaderText = "Producto".Traducir();
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
        /// <summary>
        /// Oculta datos técnicos, da formato de hora a la fecha de venta y aplica traducciones a la grilla principal de historial.
        /// </summary>
        private void ConfigurarColumnasGrilla()
        {
            if (dgvVentasRealizadas.Columns.Contains("IdVenta")) dgvVentasRealizadas.Columns["IdVenta"].Visible = false;
            if (dgvVentasRealizadas.Columns.Contains("IdSucursal")) dgvVentasRealizadas.Columns["IdSucursal"].Visible = false;
            if (dgvVentasRealizadas.Columns.Contains("IdCliente")) dgvVentasRealizadas.Columns["IdCliente"].Visible = false;
            if (dgvVentasRealizadas.Columns.Contains("EsMayorista")) dgvVentasRealizadas.Columns["EsMayorista"].Visible = false;
            if (dgvVentasRealizadas.Columns.Contains("IdClienteNavigation")) dgvVentasRealizadas.Columns["IdClienteNavigation"].Visible = false;
            if (dgvVentasRealizadas.Columns.Contains("VentaDetalles")) dgvVentasRealizadas.Columns["VentaDetalles"].Visible = false;
            if (dgvVentasRealizadas.Columns.Contains("MontoDescuento")) dgvVentasRealizadas.Columns["MontoDescuento"].Visible = false;

            if (dgvVentasRealizadas.Columns.Contains("FechaVenta"))
            {
                dgvVentasRealizadas.Columns["FechaVenta"].HeaderText = "Hora".Traducir();
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
        /// <summary>
        /// Detecta el cambio en el selector de fechas y dispara automáticamente la recarga del historial filtrado.
        /// </summary>
        private void dateTimePickerVenta_ValueChanged(object sender, EventArgs e)
        {
            CargarVentasFiltradas(dateTimePickerVenta.Value.Date);
        }
        /// <summary>
        /// Fuerza una actualización manual de la grilla de ventas ejecutando nuevamente la consulta a la base de datos.
        /// </summary>
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarVentasFiltradas(dateTimePickerVenta.Value.Date);
            MessageBox.Show("Lista actualizada.".Traducir(), "Información".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Detecta cuando el usuario selecciona una venta y recupera sus detalles de productos para mostrarlos en la grilla inferior.
        /// </summary>
        private void dgvVentasRealizadas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVentasRealizadas.CurrentRow != null && dgvVentasRealizadas.CurrentRow.DataBoundItem is VentumDTO ventaElegida)
            {
                var detalles = _ventaService.GetDetallesDeVenta(ventaElegida.IdVenta);

                dgvDetallesVenta.DataSource = detalles;
                ConfigurarGrillaDetalles();
            }
        }
        /// <summary>
        /// Solicita confirmación de extrema seguridad y ejecuta la anulación de la venta seleccionada, restaurando el stock a la sucursal.
        /// </summary>
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvVentasRealizadas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una venta de la lista para anular.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            VentumDTO ventaElegida = (VentumDTO)dgvVentasRealizadas.CurrentRow.DataBoundItem;
            Guid? idSucursal = SessionManager.Current.IdSucursalActual;

            if (idSucursal == null) return;

            DialogResult confirmacion = MessageBox.Show(
                string.Format("¿Está ABSOLUTAMENTE SEGURO de anular esta venta por un total de $ {0:N2}?\n\nLos productos regresarán al stock de la sucursal.".Traducir(), ventaElegida.Total),
                "Confirmar Anulación de Venta".Traducir(),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button2);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    _ventaService.AnularVenta(ventaElegida.IdVenta, idSucursal.Value);

                    MessageBox.Show("Venta anulada correctamente. El stock ha sido devuelto.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarVentasFiltradas(dateTimePickerVenta.Value.Date);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al anular la venta: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Evento de carga inicial del formulario que valida permisos dinámicos, establece la fecha actual, carga los datos y traduce la interfaz.
        /// </summary>
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
                MessageBox.Show(string.Format("Error al cargar la pantalla: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            TraductorUI.TraducirFormulario(this);
        }
    }   
}
