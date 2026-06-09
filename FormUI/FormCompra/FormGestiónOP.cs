using Logic.CustomExceptions;
using Logic.Facade;
using ModelsDTO;
using System.Data;
using Services.Facade.Extensions;

namespace FormUI.FormCompra
{
    public partial class FormGestiónOP : Form
    {
        private readonly OrdenDePedidoService _ordenService;
        private const int ESTADO_PENDIENTE = 1;
        /// <summary>
        /// Inicializa el formulario, instancia el servicio de órdenes de pedido y configura la grilla principal.
        /// </summary>
        public FormGestiónOP()
        {
            InitializeComponent();
            _ordenService = new OrdenDePedidoService();
            ConfigurarDataGridView();
        }
        /// <summary>
        /// Configura las columnas manualmente para la grilla principal de órdenes de pedido, asignando formatos y traducciones.
        /// </summary>
        private void ConfigurarDataGridView()
        {
            dgvOrdenDePedido.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrdenDePedido.AutoGenerateColumns = false;

            dgvOrdenDePedido.Columns.Add("IdOrdenDePedido", "ID Orden".Traducir());
            dgvOrdenDePedido.Columns["IdOrdenDePedido"].DataPropertyName = "IdOrdenDePedido";
            dgvOrdenDePedido.Columns["IdOrdenDePedido"].Visible = false;

            dgvOrdenDePedido.Columns.Add("FechaOp", "Fecha".Traducir());
            dgvOrdenDePedido.Columns["FechaOp"].DataPropertyName = "FechaOp";

            dgvOrdenDePedido.Columns.Add("Total", "Total".Traducir());
            dgvOrdenDePedido.Columns["Total"].DataPropertyName = "Total";
            if (dgvOrdenDePedido.Columns["Total"] != null) dgvOrdenDePedido.Columns["Total"].DefaultCellStyle.Format = "C2";

            dgvOrdenDePedido.Columns.Add("EstadoTexto", "Estado".Traducir());
            dgvOrdenDePedido.Columns["EstadoTexto"].DataPropertyName = "EstadoTexto";
        }
        /// <summary>
        /// Evento de carga inicial que llena las opciones de filtro de estado y traduce los componentes de la interfaz.
        /// </summary>
        private void FormGestiónOP_Load(object sender, EventArgs e)
        {
            cmbFiltroEstado.Items.Add("Pendientes".Traducir());
            cmbFiltroEstado.Items.Add("Aprobadas".Traducir());
            cmbFiltroEstado.Items.Add("Rechazadas".Traducir());
            cmbFiltroEstado.Items.Add("Todas".Traducir());
            cmbFiltroEstado.SelectedIndex = 0;
            TraductorUI.TraducirFormulario(this);
        }
        /// <summary>
        /// Filtra y muestra explícitamente las órdenes de pedido que se encuentran en estado pendiente.
        /// </summary>
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvDetalleOP.DataSource = null;
                List<OrdenDePedidoDTO> todasLasOrdenes = _ordenService.ObtenerTodas();

                List<OrdenDePedidoDTO> ordenesPendientes = todasLasOrdenes
                    .Where(o => o.IdEstadoOp == ESTADO_PENDIENTE)
                    .ToList();

                dgvOrdenDePedido.DataSource = ordenesPendientes;

                if (ordenesPendientes.Count == 0)
                {
                    MessageBox.Show("No hay Órdenes de Pedido pendientes para gestionar.".Traducir(), "Información".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las órdenes de pedido: {ex.Message}".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Inicia el proceso de baja o rechazo de la orden de pedido seleccionada en la grilla tras la confirmación del usuario.
        /// </summary>
        private void btnDardeBaja_Click(object sender, EventArgs e)
        {
            if (dgvOrdenDePedido.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar una Orden de Pedido para dar de baja.".Traducir(), "Advertencia".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro que desea dar de baja (rechazar) la Orden de Pedido seleccionada? Esto CANCELARÁ la solicitud.".Traducir(),
                                                  "Confirmar Baja".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    Guid idSeleccionado = (Guid)dgvOrdenDePedido.CurrentRow.Cells["IdOrdenDePedido"].Value;
                    _ordenService.RechazarOrden(idSeleccionado);

                    MessageBox.Show("Orden de Pedido dada de baja (rechazada) correctamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarOrdenes();
                }
                catch (TransicionEstadoInvalidaException ex)
                {
                    MessageBox.Show(ex.Message.Traducir(), "Operación no permitida".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CargarOrdenes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al dar de baja la Orden de Pedido: {ex.Message}".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Valida y aprueba la orden de pedido seleccionada para generar la correspondiente orden de compra, previa confirmación del usuario.
        /// </summary>
        private void btnGenerarOC_Click(object sender, EventArgs e)
        {
            if (dgvOrdenDePedido.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar una Orden de Pedido para generar la Orden de Compra.".Traducir(), "Advertencia".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro que desea APROBAR esta Orden de Pedido y generar la Orden de Compra?".Traducir(),
                                                  "Confirmar Generación de Orden de Compra".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    Guid idSeleccionado = (Guid)dgvOrdenDePedido.CurrentRow.Cells["IdOrdenDePedido"].Value;
                    ResultadoGeneracionOCsDTO resultado = _ordenService.AprobarYGenerarOCs(idSeleccionado);

                    if (resultado.Exito)
                    {
                        MessageBox.Show("¡La Orden de Compra ha sido generada exitosamente!".Traducir(), "Generación Exitosa".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Fallo: {resultado.Mensaje}".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    CargarOrdenes();
                }
                catch (TransicionEstadoInvalidaException ex)
                {
                    MessageBox.Show(ex.Message.Traducir(), "Operación no permitida".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CargarOrdenes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al generar la Orden de Compra: {ex.Message}".Traducir(), "Error de Transición".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Carga y muestra los detalles específicos de la orden de pedido al seleccionarla en la grilla principal.
        /// </summary>
        private void dgvOrdenDePedido_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrdenDePedido.CurrentRow != null)
            {
                try
                {
                    Guid idSeleccionado = (Guid)dgvOrdenDePedido.CurrentRow.Cells["IdOrdenDePedido"].Value;
                    var detalles = _ordenService.ObtenerDetallesPorOrden(idSeleccionado);
                    dgvDetalleOP.DataSource = detalles;
                    ConfigurarDataGridViewDetalle();
                }
                catch (Exception ex)
                {
                    if (dgvOrdenDePedido.DataSource != null)
                    {
                        MessageBox.Show($"Error al cargar el detalle: {ex.Message}".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        /// <summary>
        /// Oculta columnas técnicas, aplica traducciones a los encabezados y establece el formato monetario en la grilla de detalles.
        /// </summary>
        private void ConfigurarDataGridViewDetalle()
        {
            dgvDetalleOP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvDetalleOP.Columns.Contains("IdOrdenDePedidoDetalle")) dgvDetalleOP.Columns["IdOrdenDePedidoDetalle"].Visible = false;
            if (dgvDetalleOP.Columns.Contains("IdOrdenDePedido")) dgvDetalleOP.Columns["IdOrdenDePedido"].Visible = false;
            if (dgvDetalleOP.Columns.Contains("IdProducto")) dgvDetalleOP.Columns["IdProducto"].Visible = false;
            if (dgvDetalleOP.Columns.Contains("IdOrdenDePedidoNavigation")) dgvDetalleOP.Columns["IdOrdenDePedidoNavigation"].Visible = false;
            if (dgvDetalleOP.Columns.Contains("IdProductoNavigation")) dgvDetalleOP.Columns["IdProductoNavigation"].Visible = false;
            if (dgvDetalleOP.Columns.Contains("PrecioNeto")) dgvDetalleOP.Columns["PrecioNeto"].Visible = false;

            if (dgvDetalleOP.Columns["Subtotal"] != null) dgvDetalleOP.Columns["Subtotal"].DefaultCellStyle.Format = "N2";

            if (dgvDetalleOP.Columns.Contains("NombreProducto"))
            {
                dgvDetalleOP.Columns["NombreProducto"].HeaderText = "Producto".Traducir();
                dgvDetalleOP.Columns["NombreProducto"].DisplayIndex = 0;
            }

            if (dgvDetalleOP.Columns.Contains("Marca"))
            {
                dgvDetalleOP.Columns["Marca"].HeaderText = "Marca".Traducir();
                dgvDetalleOP.Columns["Marca"].DisplayIndex = 1;
            }

            if (dgvDetalleOP.Columns.Contains("PesoNeto"))
            {
                dgvDetalleOP.Columns["PesoNeto"].HeaderText = "Peso Neto".Traducir();
                dgvDetalleOP.Columns["PesoNeto"].DisplayIndex = 2;
            }

            if (dgvDetalleOP.Columns.Contains("Unidad"))
            {
                dgvDetalleOP.Columns["Unidad"].HeaderText = "Unidad".Traducir();
                dgvDetalleOP.Columns["Unidad"].DisplayIndex = 3;
            }

            if (dgvDetalleOP.Columns.Contains("Cantidad"))
            {
                dgvDetalleOP.Columns["Cantidad"].HeaderText = "Cantidad".Traducir();
                dgvDetalleOP.Columns["Cantidad"].DisplayIndex = 4;
            }

            if (dgvDetalleOP.Columns.Contains("PrecioUnitario"))
            {
                dgvDetalleOP.Columns["PrecioUnitario"].HeaderText = "Precio Unit.".Traducir();
                dgvDetalleOP.Columns["PrecioUnitario"].DisplayIndex = 5;
            }
        }
        /// <summary>
        /// Actualiza la lista de órdenes mostradas al cambiar la selección en el combo box de filtros.
        /// </summary>
        private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarOrdenes();
        }
        /// <summary>
        /// Consulta la base de datos para recuperar y filtrar las órdenes de pedido, actualizando la grilla principal y el estado de los botones.
        /// </summary>
        private void CargarOrdenes()
        {
            try
            {
                dgvDetalleOP.DataSource = null;
                List<OrdenDePedidoDTO> todasLasOrdenes = _ordenService.ObtenerTodas();
                List<OrdenDePedidoDTO> filtradas;

                if (cmbFiltroEstado.SelectedIndex == 0)
                    filtradas = todasLasOrdenes.Where(o => o.IdEstadoOp == 1).ToList();
                else if (cmbFiltroEstado.SelectedIndex == 1)
                    filtradas = todasLasOrdenes.Where(o => o.IdEstadoOp == 2).ToList();
                else if (cmbFiltroEstado.SelectedIndex == 2)
                    filtradas = todasLasOrdenes.Where(o => o.IdEstadoOp == 3).ToList();
                else
                    filtradas = todasLasOrdenes.ToList();

                dgvOrdenDePedido.DataSource = filtradas;

                if (filtradas.Count == 0 && cmbFiltroEstado.SelectedIndex == 0)
                {
                    MessageBox.Show("No hay Órdenes de Pedido pendientes para gestionar.".Traducir(), "Información".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                bool sonPendientes = (cmbFiltroEstado.SelectedIndex == 0);

                btnGenerarOC.Enabled = sonPendientes;
                btnDardeBaja.Enabled = sonPendientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las órdenes de pedido: {ex.Message}".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
