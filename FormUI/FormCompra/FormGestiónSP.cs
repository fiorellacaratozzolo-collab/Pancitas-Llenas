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
    public partial class FormGestiónSP : Form
    {
        // Instancia del servicio que gestiona la lógica de SP
        private readonly SolicitudDePedidoService _solicitudService;

        // Constante para el estado "Pendiente"
        private const int ESTADO_PENDIENTE = 1;

        public FormGestiónSP()
        {
            InitializeComponent();
            _solicitudService = new SolicitudDePedidoService();
            ConfigurarDataGridView();
        }

        private void ConfigurarDataGridView()
        {
            dgvSolicitudDePedido.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Esto previene la generación automática de columnas
            dgvSolicitudDePedido.AutoGenerateColumns = false;

            // Aquí defines las columnas que quieres mostrar (ejemplos)
            dgvSolicitudDePedido.Columns.Add("IdSolicitudDePedido", "ID Solicitud");
            dgvSolicitudDePedido.Columns["IdSolicitudDePedido"].DataPropertyName = "IdSolicitudDePedido";
            dgvSolicitudDePedido.Columns["IdSolicitudDePedido"].Visible = false;

            dgvSolicitudDePedido.Columns.Add("FechaSp", "Fecha");
            dgvSolicitudDePedido.Columns["FechaSp"].DataPropertyName = "FechaSp";

            // Muestra el ID del estado (o podrías mapear la descripción del estado)
            dgvSolicitudDePedido.Columns.Add("EstadoTexto", "Estado");
            dgvSolicitudDePedido.Columns["EstadoTexto"].DataPropertyName = "EstadoTexto";
        }

        private void FormGestiónSP_Load(object sender, EventArgs e)
        {
            // Cargamos las opciones del filtro
            cmbFiltroEstado.Items.Add("Pendientes"); // Índice 0
            cmbFiltroEstado.Items.Add("Aprobadas");  // Índice 1
            cmbFiltroEstado.Items.Add("Rechazadas"); // Índice 2
            cmbFiltroEstado.Items.Add("Todas");      // Índice 3

            // Al seleccionar el 0, dispara automáticamente el evento y carga la grilla
            cmbFiltroEstado.SelectedIndex = 0;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarSolicitudes();
        }

        private void btnGenerarOP_Click(object sender, EventArgs e)
        {
            if (dgvSolicitudDePedido.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar una Solicitud de Pedido para generar la Orden de Pedido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Confirmación
            DialogResult result = MessageBox.Show("¿Está seguro que desea APROBAR esta Solicitud y generar la Orden de Pedido?",
                                                  "Confirmar Aprobación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 2. Obtener el ID y llamar al método de transición
                    Guid idSeleccionado = (Guid)dgvSolicitudDePedido.CurrentRow.Cells["IdSolicitudDePedido"].Value;

                    Guid nuevaOPId = _solicitudService.AprobarYSolicitarOrdenDePedido(idSeleccionado);

                    MessageBox.Show($"¡Orden de Pedido generada con éxito!\nID de la nueva OP: {nuevaOPId}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 3. Actualizar la lista (la SP debería desaparecer de los "Pendientes")
                    btnActualizar_Click(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al generar la Orden de Pedido: {ex.Message}", "Error de Transición", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnDardeBaja_Click(object sender, EventArgs e)
        {
            if (dgvSolicitudDePedido.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar una Solicitud de Pedido para dar de baja.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Confirmación al usuario
            DialogResult result = MessageBox.Show("¿Está seguro que desea dar de baja (rechazar) la Solicitud de Pedido seleccionada?",
                                                  "Confirmar Baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 2. Obtener el ID y llamar al método de lógica
                    Guid idSeleccionado = (Guid)dgvSolicitudDePedido.CurrentRow.Cells["IdSolicitudDePedido"].Value;
                    _solicitudService.RechazarSolicitud(idSeleccionado);

                    MessageBox.Show("Solicitud de Pedido dada de baja correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 3. Actualizar la lista (debería desaparecer de los "Pendientes")
                    btnActualizar_Click(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al dar de baja la solicitud: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvSolicitudDePedido_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSolicitudDePedido.CurrentRow != null)
            {
                try
                {
                    // 1. Obtenemos el ID de la fila seleccionada
                    Guid idSeleccionado = (Guid)dgvSolicitudDePedido.CurrentRow.Cells["IdSolicitudDePedido"].Value;

                    // 2. Buscamos los detalles usando tu servicio 
                    // (Asegurate de tener este método creado en tu SolicitudDePedidoService/Logic)
                    var detalles = _solicitudService.ObtenerDetallesPorSolicitud(idSeleccionado);

                    // 3. Llenamos la grilla de detalles
                    dgvDetalleSP.DataSource = detalles;

                    // 4. La ponemos linda
                    ConfigurarDataGridViewDetalle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar el detalle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ConfigurarDataGridViewDetalle()
        {
            dgvDetalleSP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 1. Ocultar IDs y navegaciones de Entity Framework
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

            // 2. Renombrar las columnas (Asumiendo que tenés estas propiedades en tu DTO)
            if (dgvDetalleSP.Columns.Contains("NombreProducto"))
                dgvDetalleSP.Columns["NombreProducto"].HeaderText = "Producto";

            if (dgvDetalleSP.Columns.Contains("PesoNeto"))
                dgvDetalleSP.Columns["PesoNeto"].HeaderText = "Peso Neto";

            if (dgvDetalleSP.Columns.Contains("Cantidad"))
                dgvDetalleSP.Columns["Cantidad"].HeaderText = "Cant. Solicitada";

            // 3. (Opcional) Ordenar las columnas
            if (dgvDetalleSP.Columns.Contains("NombreProducto"))
                dgvDetalleSP.Columns["NombreProducto"].DisplayIndex = 0;

            if (dgvDetalleSP.Columns.Contains("Marca"))
                dgvDetalleSP.Columns["Marca"].DisplayIndex = 1;

            if (dgvDetalleSP.Columns.Contains("PesoNeto"))
                dgvDetalleSP.Columns["PesoNeto"].DisplayIndex = 2;

            if (dgvDetalleSP.Columns.Contains("Unidad"))
                dgvDetalleSP.Columns["Unidad"].DisplayIndex = 3;

            if (dgvDetalleSP.Columns.Contains("Cantidad"))
                dgvDetalleSP.Columns["Cantidad"].DisplayIndex = 4;
        }

        private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarSolicitudes();
        }

        private void CargarSolicitudes()
        {
            try
            {
                // Limpiamos el detalle para que no quede pegada información de la vista anterior
                dgvDetalleSP.DataSource = null;

                // 1. Obtener todas las solicitudes
                List<SolicitudDePedidoDTO> todasLasSolicitudes = _solicitudService.ObtenerTodas();
                List<SolicitudDePedidoDTO> filtradas;

                // 2. Filtramos según lo que eligió el usuario en el ComboBox
                if (cmbFiltroEstado.SelectedIndex == 0) // Pendientes (Estado 1)
                    filtradas = todasLasSolicitudes.Where(s => s.IdEstadoSp == 1).ToList();
                else if (cmbFiltroEstado.SelectedIndex == 1) // Aprobadas (Estado 2)
                    filtradas = todasLasSolicitudes.Where(s => s.IdEstadoSp == 2).ToList();
                else if (cmbFiltroEstado.SelectedIndex == 2) // Rechazadas (Estado 3)
                    filtradas = todasLasSolicitudes.Where(s => s.IdEstadoSp == 3).ToList();
                else // Todas
                    filtradas = todasLasSolicitudes.ToList();

                // 3. Asignar al DataGridView
                dgvSolicitudDePedido.DataSource = filtradas;

                // Solo mostramos el cartel de "vacío" si están buscando las pendientes (UX limpia)
                if (filtradas.Count == 0 && cmbFiltroEstado.SelectedIndex == 0)
                {
                    MessageBox.Show("No hay Solicitudes de Pedido pendientes para gestionar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // 🌟 MEDIDA DE SEGURIDAD UX: 
                // Apagamos los botones de acción si están mirando el historial (Aprobadas/Rechazadas/Todas)
                bool sonPendientes = (cmbFiltroEstado.SelectedIndex == 0);

                btnGenerarOP.Enabled = sonPendientes;
                btnDardeBaja.Enabled = sonPendientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las solicitudes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
