using DataAccess.Models;
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

namespace FormUI.FormVenta
{
    public partial class FormGestiónCliente : Form
    {
        public FormGestiónCliente()
        {
            InitializeComponent();
            CargarDatosClientes();
        }

        private void FormGestiónCliente_Load(object sender, EventArgs e)
        {

        }

        private readonly ClienteService _clienteService = new ClienteService();

        /// <summary>
        /// Obtiene la lista de clientes y la asigna al DataGridView (dgvClientes).
        /// </summary>
        private void CargarDatosClientes()
        {
            try
            {
                // 1. Obtener la lista de clientes de la capa de servicio/lógica.
                // Asumiendo que has agregado este método en la capa Logic/Service:
                List<ClienteDTO> listaClientes = _clienteService.GetAllClientes();

                // 2. Asignar la lista como fuente de datos del DataGridView.
                dgvCliente.DataSource = listaClientes;

                // 3. Configurar la apariencia de las columnas.
                ConfigurarColumnasDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los clientes: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ajusta la visibilidad y el encabezado de las columnas generadas.
        /// </summary>
        private void ConfigurarColumnasDataGridView()
        {
            // Ocultar las propiedades de navegación y el ID que no son relevantes para el usuario
            if (dgvCliente.Columns.Contains("IdCliente"))
            {
                dgvCliente.Columns["IdCliente"].Visible = false;
            }
            if (dgvCliente.Columns.Contains("VentaDetalles"))
            {
                dgvCliente.Columns["VentaDetalles"].Visible = false;
            }
            if (dgvCliente.Columns.Contains("IdTipoClienteNavigation"))
            {
                // Si usaste Eager Loading, puedes mostrar el nombre del tipo:
                // dgvClientes.Columns["IdTipoClienteNavigation"].HeaderText = "Tipo Cliente";
                dgvCliente.Columns["IdTipoClienteNavigation"].Visible = false;
            }

            // Renombrar columnas para la visualización del usuario
            if (dgvCliente.Columns.Contains("NombreCliente"))
            {
                dgvCliente.Columns["NombreCliente"].HeaderText = "Nombre";
            }
            if (dgvCliente.Columns.Contains("Dni"))
            {
                dgvCliente.Columns["Dni"].HeaderText = "D.N.I.";
            }
            if (dgvCliente.Columns.Contains("IdTipoCliente"))
            {
                dgvCliente.Columns["IdTipoCliente"].HeaderText = "Tipo Cliente";
            }

            // Ajustar el ancho de las columnas
            dgvCliente.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }


        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            // 1. Obtención y Validación de Datos
            if (string.IsNullOrWhiteSpace(txtbNombreCliente.Text))
            {
                MessageBox.Show("El campo Nombre es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtbDNI.Text, out int dniValue))
            {
                MessageBox.Show("El DNI debe ser un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Determinar IdTipoCliente basado en el radio button seleccionado
            int idTipoCliente;
            if (rbtnMayorista.Checked)
            {
                // ASUMPTION: Mayorista's ID is 1. This value should ideally be retrieved 
                // from the DB or a configuration constant.
                idTipoCliente = 1;
            }
            else if (rbtnMinorista.Checked)
            {
                // ASUMPTION: Minorista's ID is 0.
                idTipoCliente = 0;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Tipo de Cliente (Mayorista o Minorista).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Creación del Objeto Modelo
            var nuevoCliente = new ClienteDTO
            {
                // IdCliente is generated in the Repository layer (ClienteRepository.Create)
                NombreCliente = txtbNombreCliente.Text.Trim(),
                Dni = dniValue,
                IdTipoCliente = idTipoCliente,
                // Navigation properties are managed by the data layer
            };

            // 3. Llamada a la Lógica de Negocio y Manejo de Excepciones
            try
            {
                // Call the service layer method to persist the client
                Guid newClientId = _clienteService.CreateCliente(nuevoCliente);

                MessageBox.Show($"Cliente agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarControles();
            }
            catch (ArgumentNullException ex)
            {
                // Handle validation/null checks from the Repository
                MessageBox.Show($"Error de datos: {ex.Message}", "Error de Alta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Handle generic errors (DB connection, unique constraints, etc.)
                MessageBox.Show($"Ocurrió un error al intentar agregar el cliente: {ex.Message}", "Error Inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarControles()
        {
            txtbNombreCliente.Text = string.Empty;
            txtbDNI.Text = string.Empty;
            rbtnMayorista.Checked = false;
            rbtnMinorista.Checked = false;
            txtbNombreCliente.Focus();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            // Llama al método existente para recargar la lista completa.
            CargarDatosClientes();
            MessageBox.Show("Lista de clientes actualizada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (dgvCliente.CurrentRow != null)
            {
                // 1. Obtener la clave primaria del cliente seleccionado.
                // Se asume que la columna IdCliente existe, aunque esté oculta.
                Guid clienteId = (Guid)dgvCliente.CurrentRow.Cells["IdCliente"].Value;
                string nombre = dgvCliente.CurrentRow.Cells["NombreCliente"].Value?.ToString() ?? string.Empty;

                // 2. Confirmación del usuario.
                DialogResult dialogResult = MessageBox.Show($"¿Está seguro de deshabilitar a {nombre}?", "Confirmar Acción", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        // 3. Llamar al servicio para deshabilitar el cliente.
                        _clienteService.DeshabilitarCliente(clienteId);

                        // 4. Recargar el DataGridView.
                        CargarDatosClientes();
                        MessageBox.Show("Cliente deshabilitado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al deshabilitar el cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila para deshabilitar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el ID del Tipo de Cliente ( 0=Minorista, 1=Mayorista) a buscar:");

            if (int.TryParse(input, out int idTipoCliente))
            {
                try
                {
                    // 1. Llamar al servicio para obtener la lista filtrada.
                    List<ClienteDTO> listaFiltrada = _clienteService.BuscarClientesPorTipo(idTipoCliente);

                    // 2. Asignar la lista filtrada como nueva fuente de datos.
                    dgvCliente.DataSource = listaFiltrada;
                    ConfigurarColumnasDataGridView();

                    MessageBox.Show($"Se encontraron {listaFiltrada.Count} clientes para el ID Tipo {idTipoCliente}.", "Búsqueda Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error durante la búsqueda: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (!string.IsNullOrEmpty(input))
            {
                MessageBox.Show("El ID del Tipo de Cliente debe ser un número entero válido.", "Error de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Si el usuario cancela (input es vacío), no hace nada.
        }
    }
}

