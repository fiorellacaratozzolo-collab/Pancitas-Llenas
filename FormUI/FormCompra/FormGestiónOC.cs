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
using Services.Facade.Extensions;

using System.IO;

namespace FormUI.FormCompra
{
    public partial class FormGestiónOC : Form
    {
        private readonly OrdenDeCompraService _ocService;

        /// <summary>
        /// Inicializa el formulario, instancia los servicios necesarios y configura el comportamiento de solo lectura y selección de las grillas.
        /// </summary>
        public FormGestiónOC()
        {
            InitializeComponent();
            _ocService = new OrdenDeCompraService();
            dgvOrdenCompra.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrdenCompra.MultiSelect = false;
            dgvOrdenCompra.ReadOnly = true;
            dgvDetalleOC.ReadOnly = true;
        }

        /// <summary>
        /// Obtiene todas las órdenes de compra de la base de datos, aplica el filtro de estado seleccionado y actualiza la vista principal.
        /// </summary>
        private void CargarOrdenes()
        {
            var todas = _ocService.ObtenerTodas();
            List<OrdenDeCompraDTO> filtradas;

            if (cmbFiltroEstado.SelectedIndex == 0)
                filtradas = todas.Where(x => x.IdEstadoOc == 1).ToList();
            else if (cmbFiltroEstado.SelectedIndex == 1)
                filtradas = todas.Where(x => x.IdEstadoOc == 2).ToList();
            else if (cmbFiltroEstado.SelectedIndex == 2)
                filtradas = todas.Where(x => x.IdEstadoOc == 3).ToList();
            else
                filtradas = todas.ToList();

            dgvOrdenCompra.DataSource = filtradas;
            FormatearGridPrincipal();

            bool sonPendientes = cmbFiltroEstado.SelectedIndex == 0;
            btnAlta.Enabled = sonPendientes;
            btnBaja.Enabled = sonPendientes;
        }

        /// <summary>
        /// Evento que recarga y refresca la lista de órdenes de compra en pantalla.
        /// </summary>
        private void btnVer_Click(object sender, EventArgs e)
        {
            CargarOrdenes();
        }

        /// <summary>
        /// Genera un archivo de texto físico en el Escritorio del usuario con el detalle formal de la Orden de Compra aprobada.
        /// </summary>
        private void ImprimirOrdenDeCompraTXT(OrdenDeCompraDTO oc)
        {
            try
            {
                // Detalles de la orden para armar la factura
                var detalles = _ocService.ObtenerDetallesPorOrden(oc.IdOrdenDeCompra);

                // Ruta de guardado en el Escritorio del usuario
                string escritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string nombreArchivo = string.Format("OrdenDeCompra_{0}.txt", oc.IdOrdenDeCompra.ToString().Substring(0, 8));
                string rutaCompleta = Path.Combine(escritorio, nombreArchivo);

                using (StreamWriter writer = new StreamWriter(rutaCompleta, false))
                {
                    writer.WriteLine("==================================================");
                    writer.WriteLine("              PANCITAS LLENAS PETSHOP             ");
                    writer.WriteLine("==================================================");
                    writer.WriteLine("DOCUMENTO: ORDEN DE COMPRA");
                    writer.WriteLine(string.Format("NRO DE ORDEN: {0}", oc.IdOrdenDeCompra));
                    writer.WriteLine(string.Format("FECHA:        {0}", oc.FechaOc.ToString("dd/MM/yyyy")));
                    writer.WriteLine(string.Format("PROVEEDOR:    {0}", oc.NombreProveedor));
                    writer.WriteLine("ESTADO:       APROBADA");
                    writer.WriteLine("==================================================");
                    writer.WriteLine("DETALLE DE MERCADERIA SOLICITADA:");
                    writer.WriteLine("--------------------------------------------------");

                    writer.WriteLine(string.Format("{0,-25} | {1,-5} | {2,-10} | {3,-10}", "PRODUCTO", "CANT.", "P. UNIT", "SUBTOTAL"));
                    writer.WriteLine("--------------------------------------------------");

                    foreach (var det in detalles)
                    {
                        string nombreReal = det.NombreProducto ?? "Sin Nombre";
                        string nombreProd = nombreReal.Length > 24 ? nombreReal.Substring(0, 24) : nombreReal;

                        writer.WriteLine(string.Format("{0,-25} | {1,-5} | {2,-10:C2} | {3,-10:C2}",
                            nombreProd,
                            det.Cantidad,
                            det.PrecioUnitario,
                            det.Subtotal));
                    }

                    writer.WriteLine("--------------------------------------------------");
                    writer.WriteLine(string.Format("TOTAL A ABONAR: {0:C2}", oc.Total));
                    writer.WriteLine("==================================================");
                    writer.WriteLine("Firma Autorizada: ___________________________");
                }

                MessageBox.Show(string.Format("La Orden de Compra ha sido exportada a:\n{0}", rutaCompleta).Traducir(), "Impresión Exitosa".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al generar el documento impreso: {0}", ex.Message).Traducir(), "Error de Impresión".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Inicia el proceso de validación y finalización (aprobación) de la orden de compra seleccionada, actualizando su estado e imprimiendo el documento.
        /// </summary>
        private void btnAlta_Click(object sender, EventArgs e)
        {
            if (dgvOrdenCompra.CurrentRow?.DataBoundItem is not OrdenDeCompraDTO oc)
            {
                MessageBox.Show("Seleccione una orden válida.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(string.Format("¿Finalizar OC #{0}?".Traducir(), oc.IdOrdenDeCompra), "Confirmar".Traducir(),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _ocService.FinalizarOrden(oc.IdOrdenDeCompra);

                    // Disparamos la impresión física al aprobar
                    ImprimirOrdenDeCompraTXT(oc);

                    MessageBox.Show("Operación exitosa.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnVer_Click(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Inicia el proceso de rechazo (baja) de la orden de compra seleccionada tras la confirmación del usuario.
        /// </summary>
        private void btnBaja_Click(object sender, EventArgs e)
        {
            if (dgvOrdenCompra.CurrentRow?.DataBoundItem is not OrdenDeCompraDTO oc) return;

            if (MessageBox.Show("¿Rechazar esta orden?".Traducir(), "Cuidado".Traducir(),
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _ocService.RechazarOrden(oc.IdOrdenDeCompra);
                    btnVer_Click(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Detecta el cambio de selección en la grilla principal y carga automáticamente los detalles de la orden correspondiente en la grilla inferior.
        /// </summary>
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
                Console.WriteLine("Error en vista previa: ".Traducir() + ex.Message);
            }
        }

        /// <summary>
        /// Oculta columnas técnicas, formatea valores monetarios y renombra los encabezados de la grilla principal de órdenes.
        /// </summary>
        private void FormatearGridPrincipal()
        {
            if (dgvOrdenCompra.Columns.Count > 0)
            {
                string[] ocultar = { "IdOrdenDeCompra", "IdOrdenDePedido",
                             "IdOrdenDePedidoOrigen", "IdProveedorNavigation",
                             "IdEstadoOcNavigation", "OrdenDeCompraDetalles", "IdProveedor","IdEstadoOc" };

                foreach (var col in ocultar)
                    if (dgvOrdenCompra.Columns[col] != null) dgvOrdenCompra.Columns[col].Visible = false;

                if (dgvOrdenCompra.Columns["NombreProveedor"] != null) dgvOrdenCompra.Columns["NombreProveedor"].HeaderText = "Proveedor".Traducir();
                if (dgvOrdenCompra.Columns["FechaOc"] != null) dgvOrdenCompra.Columns["FechaOc"].HeaderText = "Fecha".Traducir();
                if (dgvOrdenCompra.Columns["Total"] != null) dgvOrdenCompra.Columns["Total"].DefaultCellStyle.Format = "C2";
                if (dgvOrdenCompra.Columns["EstadoTexto"] != null) dgvOrdenCompra.Columns["EstadoTexto"].HeaderText = "Estado".Traducir();

                dgvOrdenCompra.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        /// <summary>
        /// Oculta columnas innecesarias, aplica formato de moneda y ajusta el orden visual de los campos en la grilla de detalles de la orden.
        /// </summary>
        private void FormatearGridDetalle()
        {
            if (dgvDetalleOC.Columns.Count > 0)
            {
                string[] ocultar = { "IdOrdenDeCompraDetalle", "IdOrdenDeCompra", "IdOrdenDeCompraNavigation", "IdProducto", "IdProductoNavigation" };

                foreach (var col in ocultar)
                    if (dgvDetalleOC.Columns[col] != null) dgvDetalleOC.Columns[col].Visible = false;

                if (dgvDetalleOC.Columns["NombreProducto"] != null) dgvDetalleOC.Columns["NombreProducto"].HeaderText = "Producto".Traducir();
                if (dgvDetalleOC.Columns["PesoNeto"] != null) dgvDetalleOC.Columns["PesoNeto"].HeaderText = "Peso (Kg)".Traducir();
                if (dgvDetalleOC.Columns["PrecioUnitario"] != null) dgvDetalleOC.Columns["PrecioUnitario"].DefaultCellStyle.Format = "C2";
                if (dgvDetalleOC.Columns["Subtotal"] != null) dgvDetalleOC.Columns["Subtotal"].DefaultCellStyle.Format = "C2";

                dgvDetalleOC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dgvDetalleOC.Columns.Contains("NombreProducto"))
                {
                    dgvDetalleOC.Columns["NombreProducto"].HeaderText = "Producto".Traducir();
                    dgvDetalleOC.Columns["NombreProducto"].DisplayIndex = 0;
                }
                if (dgvDetalleOC.Columns.Contains("Marca"))
                {
                    dgvDetalleOC.Columns["Marca"].HeaderText = "Marca".Traducir();
                    dgvDetalleOC.Columns["Marca"].DisplayIndex = 1;
                }
                if (dgvDetalleOC.Columns.Contains("PesoNeto"))
                {
                    dgvDetalleOC.Columns["PesoNeto"].HeaderText = "Peso Neto".Traducir();
                    dgvDetalleOC.Columns["PesoNeto"].DisplayIndex = 2;
                }
                if (dgvDetalleOC.Columns.Contains("Unidad"))
                {
                    dgvDetalleOC.Columns["Unidad"].HeaderText = "Unidad".Traducir();
                    dgvDetalleOC.Columns["Unidad"].DisplayIndex = 3;
                }
                if (dgvDetalleOC.Columns.Contains("PrecioUnitario"))
                {
                    dgvDetalleOC.Columns["PrecioUnitario"].HeaderText = "Precio Unitario".Traducir();
                    dgvDetalleOC.Columns["PrecioUnitario"].DisplayIndex = 4;
                }
                if (dgvDetalleOC.Columns.Contains("Cantidad"))
                {
                    dgvDetalleOC.Columns["Cantidad"].HeaderText = "Cantidad".Traducir();
                    dgvDetalleOC.Columns["Cantidad"].DisplayIndex = 5;
                }
                if (dgvDetalleOC.Columns.Contains("Subtotal"))
                {
                    dgvDetalleOC.Columns["Subtotal"].HeaderText = "Subtotal".Traducir();
                    dgvDetalleOC.Columns["Subtotal"].DisplayIndex = 6;
                }
            }
        }

        /// <summary>
        /// Recarga la grilla principal de órdenes de compra cada vez que el usuario cambia la opción del filtro de estados.
        /// </summary>
        private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarOrdenes();
        }

        /// <summary>
        /// Evento de carga inicial del formulario que llena las opciones del filtro de estados, selecciona la opción por defecto y traduce la interfaz.
        /// </summary>
        private void FormGestiónOC_Load_1(object sender, EventArgs e)
        {
            cmbFiltroEstado.Items.Add("Pendientes".Traducir());
            cmbFiltroEstado.Items.Add("Aprobadas".Traducir());
            cmbFiltroEstado.Items.Add("Rechazadas".Traducir());
            cmbFiltroEstado.Items.Add("Todas".Traducir());

            cmbFiltroEstado.SelectedIndex = 0;
            TraductorUI.TraducirFormulario(this);
        }
    }
}
