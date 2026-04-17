using Logic.Facade;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormUI.FormCompra
{
    public partial class FormGestiónOC : Form
    {
        private readonly OrdenDeCompraService _ocService;

        public FormGestiónOC()
        {
            InitializeComponent();
            _ocService = new OrdenDeCompraService();
            dgvOrdenCompra.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrdenCompra.MultiSelect = false;
            dgvOrdenCompra.ReadOnly = true;
            dgvDetalleOC.ReadOnly = true;
        }

        private void FormGestiónOC_Load(object sender, EventArgs e)
        {
           
        }

        private void dgvOrdenCompra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void CargarOrdenes()
        {
            // Traemos todas las órdenes de la base de datos
            var todas = _ocService.ObtenerTodas();
            List<OrdenDeCompraDTO> filtradas;

            // Filtramos según lo que eligió el usuario
            if (cmbFiltroEstado.SelectedIndex == 0) // Pendientes
                filtradas = todas.Where(x => x.IdEstadoOc == 1).ToList();
            else if (cmbFiltroEstado.SelectedIndex == 1) // Aprobadas
                filtradas = todas.Where(x => x.IdEstadoOc == 2).ToList();
            else if (cmbFiltroEstado.SelectedIndex == 2) // Rechazadas
                filtradas = todas.Where(x => x.IdEstadoOc == 3).ToList();
            else // Todas
                filtradas = todas.ToList();

            // Actualizamos la grilla
            dgvOrdenCompra.DataSource = filtradas;
            FormatearGridPrincipal();
            // Solo habilitamos los botones de Aprobar/Rechazar si estamos viendo las Pendientes
            bool sonPendientes = cmbFiltroEstado.SelectedIndex == 0;
            btnAlta.Enabled = sonPendientes;
            btnBaja.Enabled = sonPendientes;
        }
        private void btnVer_Click(object sender, EventArgs e)
        {
            CargarOrdenes();
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            if (dgvOrdenCompra.CurrentRow?.DataBoundItem is not OrdenDeCompraDTO oc)
            {
                MessageBox.Show("Seleccione una orden válida.");
                return;
            }

            if (MessageBox.Show($"¿Finalizar OC #{oc.IdOrdenDeCompra}?", "Confirmar",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    _ocService.FinalizarOrden(oc.IdOrdenDeCompra);
                    MessageBox.Show("Operación exitosa.");

                    // Refresco seguro sin pasar nulls
                    btnVer_Click(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            if (dgvOrdenCompra.CurrentRow?.DataBoundItem is not OrdenDeCompraDTO oc) return;

            if (MessageBox.Show("¿Rechazar esta orden?", "Cuidado",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _ocService.RechazarOrden(oc.IdOrdenDeCompra);

                    // Refresco seguro
                    btnVer_Click(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dgvOrdenCompra_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrdenCompra.CurrentRow == null || dgvOrdenCompra.CurrentRow.DataBoundItem == null)
            {
                dgvDetalleOC.DataSource = null;
                return;
            }

            try
            {
                var ocSeleccionada = (OrdenDeCompraDTO)dgvOrdenCompra.CurrentRow.DataBoundItem;
                var detalles = _ocService.ObtenerDetallesPorOrden(ocSeleccionada.IdOrdenDeCompra);
                dgvDetalleOC.DataSource = null;
                dgvDetalleOC.DataSource = detalles;
                FormatearGridDetalle();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en vista previa: " + ex.Message);
            }
        }

        private void FormatearGridPrincipal()
        {
            if (dgvOrdenCompra.Columns.Count > 0)
            {
                // Ocultar columnas técnicas y el Id del Proveedor
                string[] ocultar = { "IdOrdenDeCompra", "IdOrdenDePedido",
                             "IdOrdenDePedidoOrigen", "IdProveedorNavigation",
                             "IdEstadoOcNavigation", "OrdenDeCompraDetalles", "IdProveedor","IdEstadoOc" };

                foreach (var col in ocultar)
                    if (dgvOrdenCompra.Columns[col] != null) dgvOrdenCompra.Columns[col].Visible = false;

                if (dgvOrdenCompra.Columns["NombreProveedor"] != null) dgvOrdenCompra.Columns["NombreProveedor"].HeaderText = "Proveedor";
                if (dgvOrdenCompra.Columns["FechaOc"] != null) dgvOrdenCompra.Columns["FechaOc"].HeaderText = "Fecha";
                if (dgvOrdenCompra.Columns["Total"] != null) dgvOrdenCompra.Columns["Total"].DefaultCellStyle.Format = "C2";
                if (dgvOrdenCompra.Columns["EstadoTexto"] != null) dgvOrdenCompra.Columns["EstadoTexto"].HeaderText = "Estado";
                dgvOrdenCompra.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void FormatearGridDetalle()
        {
            if (dgvDetalleOC.Columns.Count > 0)
            {
                string[] ocultar = { "IdOrdenDeCompraDetalle", "IdOrdenDeCompra", "IdOrdenDeCompraNavigation", "IdProducto", "IdProductoNavigation" };

                foreach (var col in ocultar)
                    if (dgvDetalleOC.Columns[col] != null) dgvDetalleOC.Columns[col].Visible = false;

                if (dgvDetalleOC.Columns["NombreProducto"] != null) dgvDetalleOC.Columns["NombreProducto"].HeaderText = "Producto";
                if (dgvDetalleOC.Columns["PesoNeto"] != null) dgvDetalleOC.Columns["PesoNeto"].HeaderText = "Peso (Kg)";
                if (dgvDetalleOC.Columns["PrecioUnitario"] != null) dgvDetalleOC.Columns["PrecioUnitario"].DefaultCellStyle.Format = "C2";
                if (dgvDetalleOC.Columns["Subtotal"] != null) dgvDetalleOC.Columns["Subtotal"].DefaultCellStyle.Format = "C2";

                dgvDetalleOC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dgvDetalleOC.Columns.Contains("NombreProducto"))
                {
                    dgvDetalleOC.Columns["NombreProducto"].HeaderText = "Producto";
                    dgvDetalleOC.Columns["NombreProducto"].DisplayIndex = 0;
                }
                if (dgvDetalleOC.Columns.Contains("Marca"))
                {
                    dgvDetalleOC.Columns["Marca"].HeaderText = "Marca";
                    dgvDetalleOC.Columns["Marca"].DisplayIndex = 1;
                }
                if (dgvDetalleOC.Columns.Contains("PesoNeto"))
                {
                    dgvDetalleOC.Columns["PesoNeto"].HeaderText = "Peso Neto";
                    dgvDetalleOC.Columns["PesoNeto"].DisplayIndex = 2;
                }
                if (dgvDetalleOC.Columns.Contains("Unidad"))
                {
                    dgvDetalleOC.Columns["Unidad"].HeaderText = "Unidad";
                    dgvDetalleOC.Columns["Unidad"].DisplayIndex = 3;
                }
                if (dgvDetalleOC.Columns.Contains("PrecioUnitario"))
                {
                    dgvDetalleOC.Columns["PrecioUnitario"].HeaderText = "Precio Unitario";
                    dgvDetalleOC.Columns["PrecioUnitario"].DisplayIndex = 4;
                }
                if (dgvDetalleOC.Columns.Contains("Subtotal"))
                {
                    dgvDetalleOC.Columns["Cantidad"].HeaderText = "Cantidad";
                    dgvDetalleOC.Columns["Cantidad"].DisplayIndex = 5;
                }
                if (dgvDetalleOC.Columns.Contains("Subtotal"))
                {
                    dgvDetalleOC.Columns["Subtotal"].HeaderText = "Subtotal";
                    dgvDetalleOC.Columns["Subtotal"].DisplayIndex = 6;
                }
            }
        }

        private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarOrdenes();
        }

        private void FormGestiónOC_Load_1(object sender, EventArgs e)
        {
            // Cargamos las opciones del filtro
            cmbFiltroEstado.Items.Add("Pendientes"); // Índice 0
            cmbFiltroEstado.Items.Add("Aprobadas");  // Índice 1
            cmbFiltroEstado.Items.Add("Rechazadas"); // Índice 2
            cmbFiltroEstado.Items.Add("Todas");      // Índice 3

            // Al poner el Index en 0 ("Pendientes"), automáticamente dispara 
            // el evento de cambio y carga la grilla por primera vez.
            cmbFiltroEstado.SelectedIndex = 0;
        }
    }
}
