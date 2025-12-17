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
            _ocService = new OrdenDeCompraService(); // Tu servicio que conecta con la Logic

            // Configuraciones iniciales de los Grids (Opcional)
            dgvOrdenCompra.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrdenCompra.MultiSelect = false;
            dgvOrdenCompra.ReadOnly = true; // Agregado por seguridad
            dgvDetalleOC.ReadOnly = true;
        }

        private void FormGestiónOC_Load(object sender, EventArgs e)
        {
            // Carga automática al abrir el formulario
            btnVer_Click(this, EventArgs.Empty);
        }

        private void dgvOrdenCompra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            dgvOrdenCompra.DataSource = _ocService.ObtenerTodas().Where(x => x.IdEstadoOc == 1).ToList();
            FormatearGridPrincipal(); // Aplicamos los nombres de columnas y ocultamos IDs
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
            // Validamos que haya una fila seleccionada y que tenga datos
            if (dgvOrdenCompra.CurrentRow == null || dgvOrdenCompra.CurrentRow.DataBoundItem == null)
            {
                dgvDetalleOC.DataSource = null;
                return;
            }

            try
            {
                // 1. Obtenemos el objeto de la fila actual (Casteo al DTO)
                var ocSeleccionada = (OrdenDeCompraDTO)dgvOrdenCompra.CurrentRow.DataBoundItem;

                // 2. Buscamos la OC completa usando el servicio para traer los detalles del servidor/BD
                // Usamos el ID de la OC seleccionada
                var ordenConDetalles = _ocService.ObtenerPorId(ocSeleccionada.IdOrdenDeCompra);

                if (ordenConDetalles != null && ordenConDetalles.OrdenDeCompraDetalles != null)
                {
                    // 3. Asignamos la lista de detalles al grid de abajo
                    dgvDetalleOC.DataSource = ordenConDetalles.OrdenDeCompraDetalles.ToList();

                    // 4. Formateamos el grid de detalles (opcional pero recomendado)
                    FormatearGridDetalle();
                }
            }
            catch (Exception ex)
            {
                // Errores silenciosos para no molestar al usuario mientras navega
                Console.WriteLine("Error en vista previa: " + ex.Message);
            }
        }

        private void FormatearGridPrincipal()
        {
            if (dgvOrdenCompra.Columns.Count > 0)
            {
                // Lista extendida de columnas técnicas y de navegación a ocultar
                string[] columnasAocultar = 
                {
                    "IdOrdenDeCompra",
                    "IdOrdenDePedido",
                    "IdEstadoOc",
                    "IdOrdenDePedidoOrigen",  // Nueva a ocultar
                    "IdProveedorNavigation",  // Nueva a ocultar
                    "IdEstadoOcNavigation",   // Nueva a ocultar
                    "OrdenDeCompraDetalles"   // Ocultamos la lista de la cabecera
                };

                foreach (var col in columnasAocultar)
                {
                    if (dgvOrdenCompra.Columns[col] != null)
                        dgvOrdenCompra.Columns[col].Visible = false;
                }

                // Mejorar encabezados visibles
                if (dgvOrdenCompra.Columns["FechaOc"] != null) dgvOrdenCompra.Columns["FechaOc"].HeaderText = "Fecha Emisión";
                if (dgvOrdenCompra.Columns["Total"] != null)
                {
                    dgvOrdenCompra.Columns["Total"].HeaderText = "Total Compra";
                    dgvOrdenCompra.Columns["Total"].DefaultCellStyle.Format = "C2";
                }

                dgvOrdenCompra.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void FormatearGridDetalle()
        {
            if (dgvDetalleOC.Columns.Count > 0)
            {
                // Ocultar IDs y Navegaciones del detalle
                string[] columnasDetalleOcultar = 
                {
                    "IdOrdenDeCompraDetalle",
                    "IdOrdenDeCompra",
                    "IdOrdenDeCompraNavigation",
                    "IdProductoNavigation"       
                };

                foreach (var col in columnasDetalleOcultar)
                {
                    if (dgvDetalleOC.Columns[col] != null)
                        dgvDetalleOC.Columns[col].Visible = false;
                }

                // Formatear precios y cantidades
                if (dgvDetalleOC.Columns["PrecioUnitario"] != null) dgvDetalleOC.Columns["PrecioUnitario"].DefaultCellStyle.Format = "C2";
                if (dgvDetalleOC.Columns["Subtotal"] != null) dgvDetalleOC.Columns["Subtotal"].DefaultCellStyle.Format = "C2";

                dgvDetalleOC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
    }
}
