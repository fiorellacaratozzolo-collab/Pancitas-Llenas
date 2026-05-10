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

namespace FormUI.FormCompra
{
    public partial class FormGestiónSP : Form
    {
        private readonly SolicitudDePedidoService _solicitudService;
        private const int ESTADO_PENDIENTE = 1;

        /// <summary>
        /// Inicializa el formulario, instancia el servicio de solicitudes y configura las columnas de la grilla principal.
        /// </summary>
        public FormGestiónSP()
        {
            InitializeComponent();
            _solicitudService = new SolicitudDePedidoService();
            ConfigurarDataGridView();
        }
        /// <summary>
        /// Define manualmente la estructura, visibilidad y los títulos traducidos de las columnas para la grilla de solicitudes.
        /// </summary>
        private void ConfigurarDataGridView()
        {
            dgvSolicitudDePedido.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSolicitudDePedido.AutoGenerateColumns = false;

            dgvSolicitudDePedido.Columns.Add("IdSolicitudDePedido", "ID Solicitud".Traducir());
            dgvSolicitudDePedido.Columns["IdSolicitudDePedido"].DataPropertyName = "IdSolicitudDePedido";
            dgvSolicitudDePedido.Columns["IdSolicitudDePedido"].Visible = false;

            dgvSolicitudDePedido.Columns.Add("FechaSp", "Fecha".Traducir());
            dgvSolicitudDePedido.Columns["FechaSp"].DataPropertyName = "FechaSp";

            dgvSolicitudDePedido.Columns.Add("EstadoTexto", "Estado".Traducir());
            dgvSolicitudDePedido.Columns["EstadoTexto"].DataPropertyName = "EstadoTexto";
        }
        /// <summary>
        /// Evento de carga inicial del formulario que llena el filtro de estados, lo selecciona por defecto y traduce toda la interfaz.
        /// </summary>
        private void FormGestiónSP_Load(object sender, EventArgs e)
        {
            cmbFiltroEstado.Items.Add("Pendientes".Traducir());
            cmbFiltroEstado.Items.Add("Aprobadas".Traducir());
            cmbFiltroEstado.Items.Add("Rechazadas".Traducir());
            cmbFiltroEstado.Items.Add("Todas".Traducir());

            cmbFiltroEstado.SelectedIndex = 0;
            TraductorUI.TraducirFormulario(this);
        }
        /// <summary>
        /// Fuerzala recarga de las solicitudes respetando el filtro de estado actualmente seleccionado.
        /// </summary>
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarSolicitudes();
        }
        /// <summary>
        /// Valida y ejecuta la aprobación de la solicitud seleccionada, generando su correspondiente Orden de Pedido en el sistema.
        /// </summary>
        private void btnGenerarOP_Click(object sender, EventArgs e)
        {
            if (dgvSolicitudDePedido.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar una Solicitud de Pedido para generar la Orden de Pedido.".Traducir(), "Advertencia".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro que desea APROBAR esta Solicitud y generar la Orden de Pedido?".Traducir(),
                                                  "Confirmar Aprobación".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    Guid idSeleccionado = (Guid)dgvSolicitudDePedido.CurrentRow.Cells["IdSolicitudDePedido"].Value;
                    Guid nuevaOPId = _solicitudService.AprobarYSolicitarOrdenDePedido(idSeleccionado);
                    string codigoCorto = nuevaOPId.ToString().Substring(0, 8).ToUpper();
                    MessageBox.Show(string.Format("¡Orden de Pedido generada con éxito!\nN° de Referencia: {0}".Traducir(), codigoCorto), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnActualizar_Click(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al generar la Orden de Pedido: {0}".Traducir(), ex.Message), "Error de Transición".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Solicita confirmación y ejecuta la baja lógica (rechazo) de la solicitud de pedido seleccionada en la grilla.
        /// </summary>
        private void btnDardeBaja_Click(object sender, EventArgs e)
        {
            if (dgvSolicitudDePedido.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar una Solicitud de Pedido para dar de baja.".Traducir(), "Advertencia".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro que desea dar de baja (rechazar) la Solicitud de Pedido seleccionada?".Traducir(),
                                                  "Confirmar Baja".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    Guid idSeleccionado = (Guid)dgvSolicitudDePedido.CurrentRow.Cells["IdSolicitudDePedido"].Value;
                    _solicitudService.RechazarSolicitud(idSeleccionado);

                    MessageBox.Show("Solicitud de Pedido dada de baja correctamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnActualizar_Click(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al dar de baja la solicitud: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Detecta el cambio de selección en la grilla principal y carga los productos solicitados correspondientes en la grilla de detalles.
        /// </summary>
        private void dgvSolicitudDePedido_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSolicitudDePedido.CurrentRow != null)
            {
                try
                {
                    Guid idSeleccionado = (Guid)dgvSolicitudDePedido.CurrentRow.Cells["IdSolicitudDePedido"].Value;
                    var detalles = _solicitudService.ObtenerDetallesPorSolicitud(idSeleccionado);

                    dgvDetalleSP.DataSource = detalles;
                    ConfigurarDataGridViewDetalle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al cargar el detalle: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Oculta identificadores internos y aplica nombres traducidos a las columnas visibles de la grilla de detalles.
        /// </summary>
        private void ConfigurarDataGridViewDetalle()
        {
            dgvDetalleSP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvDetalleSP.Columns.Contains("IdSolicitudDePedidoDetalle"))
                dgvDetalleSP.Columns["IdSolicitudDePedidoDetalle"].Visible = false;

            if (dgvDetalleSP.Columns.Contains("IdSolicitudDePedido"))
                dgvDetalleSP.Columns["IdSolicitudDePedido"].Visible = false;

            if (dgvDetalleSP.Columns.Contains("IdProducto"))
                dgvDetalleSP.Columns["IdProducto"].Visible = false;

            if (dgvDetalleSP.Columns.Contains("IdProductoNavigation"))
                dgvDetalleSP.Columns["IdProductoNavigation"].Visible = false;

            if (dgvDetalleSP.Columns.Contains("IdSolicitudDePedidoNavigation"))
                dgvDetalleSP.Columns["IdSolicitudDePedidoNavigation"].Visible = false;

            if (dgvDetalleSP.Columns.Contains("NombreProducto"))
                dgvDetalleSP.Columns["NombreProducto"].HeaderText = "Producto".Traducir();

            if (dgvDetalleSP.Columns.Contains("PesoNeto"))
                dgvDetalleSP.Columns["PesoNeto"].HeaderText = "Peso Neto".Traducir();

            if (dgvDetalleSP.Columns.Contains("Cantidad"))
                dgvDetalleSP.Columns["Cantidad"].HeaderText = "Cant. Solicitada".Traducir();

            if (dgvDetalleSP.Columns.Contains("NombreProducto"))
                dgvDetalleSP.Columns["NombreProducto"].DisplayIndex = 0;

            if (dgvDetalleSP.Columns.Contains("Marca"))
            {
                dgvDetalleSP.Columns["Marca"].HeaderText = "Marca".Traducir();
                dgvDetalleSP.Columns["Marca"].DisplayIndex = 1;
            }

            if (dgvDetalleSP.Columns.Contains("PesoNeto"))
                dgvDetalleSP.Columns["PesoNeto"].DisplayIndex = 2;

            if (dgvDetalleSP.Columns.Contains("Unidad"))
            {
                dgvDetalleSP.Columns["Unidad"].HeaderText = "Unidad".Traducir();
                dgvDetalleSP.Columns["Unidad"].DisplayIndex = 3;
            }

            if (dgvDetalleSP.Columns.Contains("Cantidad"))
                dgvDetalleSP.Columns["Cantidad"].DisplayIndex = 4;
        }
        /// <summary>
        /// Recarga la grilla principal de solicitudes automáticamente cada vez que el usuario cambia la opción del filtro de estados.
        /// </summary>
        private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarSolicitudes();
        }
        /// <summary>
        /// Consulta la base de datos para recuperar y filtrar las solicitudes de pedido, actualizando la grilla principal y habilitando/deshabilitando los botones de acción según el estado visualizado.
        /// </summary>
        private void CargarSolicitudes()
        {
            try
            {
                dgvDetalleSP.DataSource = null;

                List<SolicitudDePedidoDTO> todasLasSolicitudes = _solicitudService.ObtenerTodas();
                List<SolicitudDePedidoDTO> filtradas;

                if (cmbFiltroEstado.SelectedIndex == 0)
                    filtradas = todasLasSolicitudes.Where(s => s.IdEstadoSp == 1).ToList();
                else if (cmbFiltroEstado.SelectedIndex == 1)
                    filtradas = todasLasSolicitudes.Where(s => s.IdEstadoSp == 2).ToList();
                else if (cmbFiltroEstado.SelectedIndex == 2)
                    filtradas = todasLasSolicitudes.Where(s => s.IdEstadoSp == 3).ToList();
                else
                    filtradas = todasLasSolicitudes.ToList();

                dgvSolicitudDePedido.DataSource = filtradas;

                if (filtradas.Count == 0 && cmbFiltroEstado.SelectedIndex == 0)
                {
                    MessageBox.Show("No hay Solicitudes de Pedido pendientes para gestionar.".Traducir(), "Información".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                bool sonPendientes = (cmbFiltroEstado.SelectedIndex == 0);

                btnGenerarOP.Enabled = sonPendientes;
                btnDardeBaja.Enabled = sonPendientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar las solicitudes: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}