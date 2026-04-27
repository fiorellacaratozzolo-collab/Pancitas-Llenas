using Logic.CustomExceptions;
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
        private readonly OrdenDePedidoService _ordenService;
        private const int ESTADO_PENDIENTE = 1;

        public FormGestiónOP()
        {
            InitializeComponent();
            _ordenService = new OrdenDePedidoService();
            ConfigurarDataGridView();
        }

        private void ConfigurarDataGridView()
        {
            dgvOrdenDePedido.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrdenDePedido.AutoGenerateColumns = false;

            dgvOrdenDePedido.Columns.Add("IdOrdenDePedido", "ID Orden");
            dgvOrdenDePedido.Columns["IdOrdenDePedido"].DataPropertyName = "IdOrdenDePedido";
            dgvOrdenDePedido.Columns["IdOrdenDePedido"].Visible = false;

            dgvOrdenDePedido.Columns.Add("FechaOp", "Fecha");
            dgvOrdenDePedido.Columns["FechaOp"].DataPropertyName = "FechaOp";

            dgvOrdenDePedido.Columns.Add("Total", "Total");
            dgvOrdenDePedido.Columns["Total"].DataPropertyName = "Total";
            if (dgvOrdenDePedido.Columns["Total"] != null) dgvOrdenDePedido.Columns["Total"].DefaultCellStyle.Format = "C2";

            dgvOrdenDePedido.Columns.Add("EstadoTexto", "Estado");
            dgvOrdenDePedido.Columns["EstadoTexto"].DataPropertyName = "EstadoTexto";
        }

        private void FormGestiónOP_Load(object sender, EventArgs e)
        {
            // Cargamos las opciones del filtro
            cmbFiltroEstado.Items.Add("Pendientes"); 
            cmbFiltroEstado.Items.Add("Aprobadas");  
            cmbFiltroEstado.Items.Add("Rechazadas"); 
            cmbFiltroEstado.Items.Add("Todas");      
            cmbFiltroEstado.SelectedIndex = 0;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvDetalleOP.DataSource = null;
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

                    // 3. Actualizar la lista
                    CargarOrdenes();
                }
                catch (TransicionEstadoInvalidaException ex) 
                {
                    MessageBox.Show(ex.Message, "Operación no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CargarOrdenes(); // Refrescamos la grilla porque el estado real es distinto al que veía el usuario
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

                    // 2. Llamar al método de transición
                    ResultadoGeneracionOCsDTO resultado = _ordenService.AprobarYGenerarOCs(idSeleccionado);

                    if (resultado.Exito)
                    {
                        MessageBox.Show(resultado.Mensaje, "Generación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Fallo: {resultado.Mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // 3. Actualizar la lista de OPs
                    CargarOrdenes();
                }
                catch (TransicionEstadoInvalidaException ex) 
                {
                    MessageBox.Show(ex.Message, "Operación no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CargarOrdenes();
                }
                catch (Exception ex)
                {
                    // Error de la UI o un error no capturado en la capa de servicio
                    MessageBox.Show($"Error al generar la Orden de Compra: {ex.Message}", "Error de Transición", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvOrdenDePedido_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrdenDePedido.CurrentRow != null)
            {
                try
                {
                    // 1. Obtenemos el ID de la OP seleccionada
                    Guid idSeleccionado = (Guid)dgvOrdenDePedido.CurrentRow.Cells["IdOrdenDePedido"].Value;

                    // 2. Vamos a buscar los detalles (¡Acordate de armar este método en tu Service/Logic/Repo!)
                    var detalles = _ordenService.ObtenerDetallesPorOrden(idSeleccionado);

                    // 3. Llenamos la segunda grilla
                    dgvDetalleOP.DataSource = detalles;

                    // 4. Acomodamos las columnas
                    ConfigurarDataGridViewDetalle();
                }
                catch (Exception ex)
                {
                    // Silenciamos el error si justo se está vaciando la grilla
                    if (dgvOrdenDePedido.DataSource != null)
                    {
                        MessageBox.Show($"Error al cargar el detalle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ConfigurarDataGridViewDetalle()
        {
            dgvDetalleOP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 1. Ocultar IDs y navegaciones
            if (dgvDetalleOP.Columns.Contains("IdOrdenDePedidoDetalle")) dgvDetalleOP.Columns["IdOrdenDePedidoDetalle"].Visible = false;
            if (dgvDetalleOP.Columns.Contains("IdOrdenDePedido")) dgvDetalleOP.Columns["IdOrdenDePedido"].Visible = false;
            if (dgvDetalleOP.Columns.Contains("IdProducto")) dgvDetalleOP.Columns["IdProducto"].Visible = false;
            if (dgvDetalleOP.Columns.Contains("IdOrdenDePedidoNavigation")) dgvDetalleOP.Columns["IdOrdenDePedidoNavigation"].Visible = false;
            if (dgvDetalleOP.Columns.Contains("IdProductoNavigation")) dgvDetalleOP.Columns["IdProductoNavigation"].Visible = false;
            if (dgvDetalleOP.Columns.Contains("PrecioNeto")) dgvDetalleOP.Columns["PrecioNeto"].Visible = false;
            if (dgvDetalleOP.Columns["Subtotal"] != null) dgvDetalleOP.Columns["Subtotal"].DefaultCellStyle.Format = "C2";
            // 2. Renombrar y Ordenar (Asumiendo que traemos Nombre, Marca, Cantidad y Precio)
            if (dgvDetalleOP.Columns.Contains("NombreProducto"))
            {
                dgvDetalleOP.Columns["NombreProducto"].HeaderText = "Producto";
                dgvDetalleOP.Columns["NombreProducto"].DisplayIndex = 0;
            }

            if (dgvDetalleOP.Columns.Contains("Marca"))
            {
                dgvDetalleOP.Columns["Marca"].HeaderText = "Marca";
                dgvDetalleOP.Columns["Marca"].DisplayIndex = 1;
            }

            if (dgvDetalleOP.Columns.Contains("PesoNeto"))
            {
                dgvDetalleOP.Columns["PesoNeto"].HeaderText = "PesoNeto";
                dgvDetalleOP.Columns["PesoNeto"].DisplayIndex = 2;
            }

            if (dgvDetalleOP.Columns.Contains("Unidad"))
            {
                dgvDetalleOP.Columns["Unidad"].HeaderText = "Unidad";
                dgvDetalleOP.Columns["Unidad"].DisplayIndex = 3;
            }

            if (dgvDetalleOP.Columns.Contains("Cantidad"))
            {
                dgvDetalleOP.Columns["Cantidad"].HeaderText = "Cantidad";
                dgvDetalleOP.Columns["Cantidad"].DisplayIndex = 4;
            }

            if (dgvDetalleOP.Columns.Contains("PrecioUnitario"))
            {
                dgvDetalleOP.Columns["PrecioUnitario"].HeaderText = "Precio Unit.";
                dgvDetalleOP.Columns["PrecioUnitario"].DisplayIndex = 5;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarOrdenes();
        }

        private void CargarOrdenes()
        {
            try
            {
                dgvDetalleOP.DataSource = null;

                // Traemos todas las órdenes
                List<OrdenDePedidoDTO> todasLasOrdenes = _ordenService.ObtenerTodas();
                List<OrdenDePedidoDTO> filtradas;

                // Filtramos según el ComboBox
                if (cmbFiltroEstado.SelectedIndex == 0) // Pendientes (1)
                    filtradas = todasLasOrdenes.Where(o => o.IdEstadoOp == 1).ToList();
                else if (cmbFiltroEstado.SelectedIndex == 1) // Aprobadas (2)
                    filtradas = todasLasOrdenes.Where(o => o.IdEstadoOp == 2).ToList();
                else if (cmbFiltroEstado.SelectedIndex == 2) // Rechazadas (3)
                    filtradas = todasLasOrdenes.Where(o => o.IdEstadoOp == 3).ToList();
                else // Todas
                    filtradas = todasLasOrdenes.ToList();

                // Asignamos al DataGridView
                dgvOrdenDePedido.DataSource = filtradas;

                if (filtradas.Count == 0 && cmbFiltroEstado.SelectedIndex == 0)
                {
                    // Solo mostramos el cartel si estamos buscando pendientes y no hay
                    MessageBox.Show("No hay Órdenes de Pedido pendientes para gestionar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                bool sonPendientes = (cmbFiltroEstado.SelectedIndex == 0);

                // Reemplazá los nombres si tus botones se llaman distinto
                btnGenerarOC.Enabled = sonPendientes;
                btnDardeBaja.Enabled = sonPendientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las órdenes de pedido: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
