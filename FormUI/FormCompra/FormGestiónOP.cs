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
    public partial class FormGestiónOP : Form
    {
        // Instancia del servicio que gestiona la lógica de OP
        private readonly OrdenDePedidoService _ordenService;
        // Constante para el estado "Pendiente de Gestión"
        private const int ESTADO_PENDIENTE = 1;

        public FormGestiónOP()
        {
            InitializeComponent();
            _ordenService = new OrdenDePedidoService();
            ConfigurarDataGridView();
        }

        private void ConfigurarDataGridView()
        {
            dgvOrdenDePedido.AutoGenerateColumns = false;

            dgvOrdenDePedido.Columns.Add("IdOrdenDePedido", "ID Orden");
            dgvOrdenDePedido.Columns["IdOrdenDePedido"].DataPropertyName = "IdOrdenDePedido";
            dgvOrdenDePedido.Columns["IdOrdenDePedido"].Visible = false;

            dgvOrdenDePedido.Columns.Add("FechaOp", "Fecha");
            dgvOrdenDePedido.Columns["FechaOp"].DataPropertyName = "FechaOp";

            dgvOrdenDePedido.Columns.Add("Total", "Total");
            dgvOrdenDePedido.Columns["Total"].DataPropertyName = "Total";

            dgvOrdenDePedido.Columns.Add("IdEstadoOp", "Estado (ID)");
            dgvOrdenDePedido.Columns["IdEstadoOp"].DataPropertyName = "IdEstadoOp";
        }

        private void FormGestiónOP_Load(object sender, EventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Obtener todas las órdenes
                List<OrdenDePedidoDTO> todasLasOrdenes = _ordenService.ObtenerTodas();

                // 2. Filtrar por estado "Pendiente de Gestión" (IdEstadoOp = 1)
                List<OrdenDePedidoDTO> ordenesPendientes = todasLasOrdenes
                    .Where(o => o.IdEstadoOp == ESTADO_PENDIENTE)
                    .ToList();

                // 3. Asignar al DataGridView
                dgvOrdenDePedido.DataSource = ordenesPendientes;

                if (ordenesPendientes.Count == 0)
                {
                    MessageBox.Show("No hay Órdenes de Pedido pendientes para gestionar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las órdenes de pedido: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDardeBaja_Click(object sender, EventArgs e)
        {
            if (dgvOrdenDePedido.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar una Orden de Pedido para dar de baja.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro que desea dar de baja (rechazar) la Orden de Pedido seleccionada? Esto CANCELARÁ la solicitud.",
                                                  "Confirmar Baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 1. Obtener el ID
                    Guid idSeleccionado = (Guid)dgvOrdenDePedido.CurrentRow.Cells["IdOrdenDePedido"].Value;

                    // 2. Llamar a la lógica para rechazar (estado 3)
                    _ordenService.RechazarOrden(idSeleccionado);

                    MessageBox.Show("Orden de Pedido dada de baja (rechazada) correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 3. Actualizar la lista (usando la corrección de sender/e)
                    btnActualizar_Click(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al dar de baja la Orden de Pedido: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnGenerarOC_Click(object sender, EventArgs e)
        {
            if (dgvOrdenDePedido.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar una Orden de Pedido para generar la Orden de Compra.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("¿Está seguro que desea APROBAR esta Orden de Pedido y generar la Orden de Compra?",
                                                  "Confirmar Generación OC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 1. Obtener el ID
                    Guid idSeleccionado = (Guid)dgvOrdenDePedido.CurrentRow.Cells["IdOrdenDePedido"].Value;

                    // 2. Llamar al método de transición (marcará OP como 5 y creará OC)
                    Guid nuevaOCId = _ordenService.AprobarYSolicitarOrdenDeCompra(idSeleccionado);

                    MessageBox.Show($"¡Orden de Compra generada con éxito!\nID de la nueva OC: {nuevaOCId}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 3. Actualizar la lista
                    btnActualizar_Click(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al generar la Orden de Compra: {ex.Message}", "Error de Transición", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
