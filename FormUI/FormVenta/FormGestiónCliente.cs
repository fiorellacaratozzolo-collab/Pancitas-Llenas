using Logic.Facade;
using ModelsDTO;
using System.Data;
using Services.Facade.Extensions;

namespace FormUI.FormVenta
{
    public partial class FormGestiónCliente : Form
    {
        private readonly ClienteService _clienteService = new ClienteService();
        private Guid? _clienteSeleccionadoId = null;
        private bool _viendoActivos = true;

        /// <summary>
        /// Inicializa el formulario, suscribe los eventos visuales y desencadena la carga inicial.
        /// </summary>
        public FormGestiónCliente()
        {
            InitializeComponent();
            dgvCliente.CellFormatting += dgvCliente_CellFormatting;
            dgvCliente.SelectionChanged += dgvCliente_SelectionChanged;

            ConfigurarEstadoInicial();
        }

        /// <summary>
        /// Aplica las traducciones correspondientes a todos los controles visuales del formulario al cargar.
        /// </summary>
        private void FormGestiónCliente_Load(object sender, EventArgs e)
        {
            TraductorUI.TraducirFormulario(this);
            LimpiarControles();
        }

        /// <summary>
        /// Establece la interfaz visual por defecto mostrando los clientes activos.
        /// </summary>
        private void ConfigurarEstadoInicial()
        {
            _viendoActivos = true;
            btnHabilitar.Visible = false;
            btnDeshabilitar.Visible = true;
            btnVerDeshabilitados.Text = "Ver Deshabilitados".Traducir();

            CargarDatosClientes();
        }

        /// <summary>
        /// Consulta el servicio para obtener los registros (Activos o Inactivos) y vincula los datos a la grilla.
        /// Acepta una lista opcional para cuando se usa el botón de búsqueda.
        /// </summary>
        private void CargarDatosClientes(List<ClienteDTO>? clientes = null)
        {
            try
            {
                if (clientes == null)
                {
                    clientes = _viendoActivos
                        ? _clienteService.ObtenerActivos()
                        : _clienteService.ObtenerDeshabilitados();
                }

                dgvCliente.DataSource = null;
                dgvCliente.DataSource = clientes;

                ConfigurarColumnasDataGridView();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar los clientes: {0}".Traducir(), ex.Message), "Error de Conexión".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Oculta columnas técnicas e IDs, y aplica traducciones a los encabezados visibles en la grilla.
        /// </summary>
        private void ConfigurarColumnasDataGridView()
        {
            if (dgvCliente.DataSource == null) return;

            dgvCliente.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCliente.AllowUserToAddRows = false;
            dgvCliente.ReadOnly = true;
            dgvCliente.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            string[] columnasOcultas = { "IdCliente", "VentaDetalles", "IdTipoClienteNavigation", "Venta", "Activo" };

            foreach (var col in columnasOcultas)
            {
                if (dgvCliente.Columns.Contains(col)) dgvCliente.Columns[col].Visible = false;
            }

            if (dgvCliente.Columns.Contains("NombreCliente")) dgvCliente.Columns["NombreCliente"].HeaderText = "Nombre".Traducir();
            if (dgvCliente.Columns.Contains("Dni")) dgvCliente.Columns["Dni"].HeaderText = "D.N.I.".Traducir();
            if (dgvCliente.Columns.Contains("IdTipoCliente")) dgvCliente.Columns["IdTipoCliente"].HeaderText = "Tipo Cliente".Traducir();

            dgvCliente.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        /// <summary>
        /// Valida los datos de entrada, determina el tipo de cliente y registra un nuevo cliente.
        /// </summary>
        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            var nuevoCliente = CrearDTO();

            try
            {
                _clienteService.CreateCliente(nuevoCliente);

                MessageBox.Show("Cliente agregado exitosamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatosClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrió un error al intentar agregar el cliente: {0}".Traducir(), ex.Message), "Error Inesperado".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Captura los datos de los controles y actualiza el cliente seleccionado en la base de datos.
        /// </summary>
        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            if (_clienteSeleccionadoId == null)
            {
                MessageBox.Show("Por favor, seleccione un cliente válido de la lista para modificar.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCampos()) return;

            var clienteEditado = CrearDTO();
            clienteEditado.IdCliente = _clienteSeleccionadoId.Value;

            try
            {
                _clienteService.UpdateCliente(clienteEditado);

                MessageBox.Show("Los cambios del cliente se guardaron correctamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatosClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al guardar los cambios: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Solicita confirmación y ejecuta la baja lógica del cliente seleccionado.
        /// </summary>
        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (_clienteSeleccionadoId == null) return;

            string nombre = txtbNombreCliente.Text;

            DialogResult dialogResult = MessageBox.Show(string.Format("¿Está seguro de deshabilitar a {0}?".Traducir(), nombre), "Confirmar Acción".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    _clienteService.DeshabilitarCliente(_clienteSeleccionadoId.Value);
                    MessageBox.Show("Cliente deshabilitado exitosamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatosClientes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al deshabilitar el cliente: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Abre un cuadro de diálogo para ingresar un ID de tipo de cliente y filtra los resultados mostrados en la grilla.
        /// </summary>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el ID del Tipo de Cliente ( 0=Minorista, 1=Mayorista) a buscar:".Traducir(), "Buscar Cliente".Traducir(), "");

            if (int.TryParse(input, out int idTipoCliente))
            {
                try
                {
                    List<ClienteDTO> listaFiltrada = _clienteService.BuscarClientesPorTipo(idTipoCliente)
                                                                    .Where(c => c.Activo == _viendoActivos).ToList();

                    CargarDatosClientes(listaFiltrada);
                    MessageBox.Show(string.Format("Se encontraron {0} clientes para el ID Tipo {1}.".Traducir(), listaFiltrada.Count, idTipoCliente), "Búsqueda Exitosa".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error durante la búsqueda: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (!string.IsNullOrEmpty(input))
            {
                MessageBox.Show("El ID del Tipo de Cliente debe ser un número entero válido.".Traducir(), "Error de Entrada".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Intercepta el dibujado de las celdas para transformar el valor numérico del Tipo de Cliente en una etiqueta legible.
        /// </summary>
        private void dgvCliente_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvCliente.Columns[e.ColumnIndex].Name == "IdTipoCliente" && e.Value != null)
            {
                if (e.Value is int idTipo)
                {
                    e.Value = idTipo == 1 ? "Mayorista".Traducir() : "Minorista".Traducir();
                    e.FormattingApplied = true;
                }
            }
        }

        /// <summary>
        /// Escucha el clic en la grilla, carga los datos en TextBoxes/RadioButtons y maneja los botones.
        /// </summary>
        private void dgvCliente_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvCliente.CurrentRow != null && dgvCliente.CurrentRow.Selected && dgvCliente.DataSource != null)
            {
                _clienteSeleccionadoId = (Guid?)dgvCliente.CurrentRow.Cells["IdCliente"].Value;

                txtbNombreCliente.Text = dgvCliente.CurrentRow.Cells["NombreCliente"].Value?.ToString();
                txtbDNI.Text = dgvCliente.CurrentRow.Cells["Dni"].Value?.ToString();

                if (dgvCliente.CurrentRow.Cells["IdTipoCliente"].Value is int idTipo)
                {
                    if (idTipo == 1) rbtnMayorista.Checked = true;
                    else if (idTipo == 0) rbtnMinorista.Checked = true;
                }

                btnAgregarCliente.Enabled = false;
                btnActualizar.Enabled = true;
                btnDeshabilitar.Enabled = _viendoActivos;
                btnHabilitar.Enabled = !_viendoActivos;
            }
        }

        /// <summary>
        /// Solicita confirmación y vuelve a habilitar un cliente inactivo.
        /// </summary>
        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            if (_clienteSeleccionadoId == null) return;

            if (MessageBox.Show("¿Desea reactivar este cliente?".Traducir(), "Confirmar".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _clienteService.HabilitarCliente(_clienteSeleccionadoId.Value);
                    MessageBox.Show("Cliente habilitado exitosamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatosClientes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al habilitar: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Alterna la vista de la grilla entre clientes Activos e Inactivos.
        /// </summary>
        private void btnVerDeshabilitados_Click(object sender, EventArgs e)
        {
            _viendoActivos = !_viendoActivos;

            btnVerDeshabilitados.Text = _viendoActivos ? "Ver Deshabilitados".Traducir() : "Ver Activos".Traducir();
            btnHabilitar.Visible = !_viendoActivos;
            btnDeshabilitar.Visible = _viendoActivos;

            CargarDatosClientes();
        }

        /// <summary>
        /// Limpia los campos llamando al método centralizado.
        /// </summary>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
        }

        /// <summary>
        /// Restablece los campos de texto, radio buttons y el estado de los botones a su valor predeterminado.
        /// </summary>
        private void LimpiarControles()
        {
            _clienteSeleccionadoId = null;
            txtbNombreCliente.Text = string.Empty;
            txtbDNI.Text = string.Empty;
            rbtnMayorista.Checked = false;
            rbtnMinorista.Checked = false;

            if (dgvCliente.DataSource != null) dgvCliente.ClearSelection();

            btnAgregarCliente.Enabled = _viendoActivos;
            btnActualizar.Enabled = false;
            btnDeshabilitar.Enabled = false;
            btnHabilitar.Enabled = false;

            txtbNombreCliente.Focus();
        }

        /// <summary>
        /// Valida que los datos ingresados en el formulario sean correctos.
        /// </summary>
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtbNombreCliente.Text))
            {
                MessageBox.Show("El campo Nombre es obligatorio.".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!int.TryParse(txtbDNI.Text, out _))
            {
                MessageBox.Show("El DNI debe ser un número válido.".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!rbtnMayorista.Checked && !rbtnMinorista.Checked)
            {
                MessageBox.Show("Debe seleccionar un Tipo de Cliente (Mayorista o Minorista).".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Crea un ClienteDTO leyendo los controles del formulario.
        /// </summary>
        private ClienteDTO CrearDTO()
        {
            return new ClienteDTO
            {
                NombreCliente = txtbNombreCliente.Text.Trim(),
                Dni = int.Parse(txtbDNI.Text),
                IdTipoCliente = rbtnMayorista.Checked ? 1 : 0,
                Activo = _viendoActivos
            };
        }
    }
}