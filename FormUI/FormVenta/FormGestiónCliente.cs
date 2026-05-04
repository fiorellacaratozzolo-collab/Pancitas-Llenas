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
using Services.Facade.Extensions;

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
            TraductorUI.TraducirFormulario(this);
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
                MessageBox.Show($"Error al cargar los clientes: {ex.Message}".Traducir(), "Error de Conexión".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ajusta la visibilidad y el encabezado de las columnas generadas.
        /// </summary>
        private void ConfigurarColumnasDataGridView()
        {
            dgvCliente.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
                dgvCliente.Columns["IdTipoClienteNavigation"].Visible = false;
            }
            if (dgvCliente.Columns.Contains("Venta"))
            {
                dgvCliente.Columns["Venta"].Visible = false;
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
            dgvCliente.AllowUserToAddRows = false;
        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            // 1. Obtención y Validación de Datos
            if (string.IsNullOrWhiteSpace(txtbNombreCliente.Text))
            {
                MessageBox.Show("El campo Nombre es obligatorio.".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtbDNI.Text, out int dniValue))
            {
                MessageBox.Show("El DNI debe ser un número válido.".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idTipoCliente;
            if (rbtnMayorista.Checked)
            {
                idTipoCliente = 1;
            }
            else if (rbtnMinorista.Checked)
            {
                idTipoCliente = 0;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Tipo de Cliente (Mayorista o Minorista).".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var nuevoCliente = new ClienteDTO
            {
                NombreCliente = txtbNombreCliente.Text.Trim(),
                Dni = dniValue,
                IdTipoCliente = idTipoCliente,
            };
            try
            {
                Guid newClientId = _clienteService.CreateCliente(nuevoCliente);

                MessageBox.Show($"Cliente agregado exitosamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarControles();
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"Error de datos: {ex.Message}".Traducir(), "Error de Alta".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al intentar agregar el cliente: {ex.Message}".Traducir(), "Error Inesperado".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            // 1. Verificamos que haya fila, que NO sea la fila vacía del final, y que sea un Cliente
            if (dgvCliente.CurrentRow != null && !dgvCliente.CurrentRow.IsNewRow && dgvCliente.CurrentRow.DataBoundItem is ClienteDTO clienteEditado)
            {
                // 2.Verificamos que no sea un "cliente fantasma" con ID vacío
                if (clienteEditado.IdCliente == Guid.Empty)
                {
                    MessageBox.Show("El cliente seleccionado no es válido o está incompleto.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    // Si pasó los dos escudos, guardamos tranquilos
                    _clienteService.UpdateCliente(clienteEditado);

                    MessageBox.Show("Los cambios del cliente se guardaron correctamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatosClientes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar los cambios: {ex.Message}".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Si tocó la nada misma o el renglón vacío, le avisamos:
                MessageBox.Show("Por favor, seleccione un cliente válido de la lista para modificar.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (dgvCliente.CurrentRow != null)
            {
                Guid clienteId = (Guid)dgvCliente.CurrentRow.Cells["IdCliente"].Value;
                string nombre = dgvCliente.CurrentRow.Cells["NombreCliente"].Value?.ToString() ?? string.Empty;

                // 2. Confirmación del usuario.
                DialogResult dialogResult = MessageBox.Show($"¿Está seguro de deshabilitar a {nombre}?".Traducir(), "Confirmar Acción".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        // 3. Llamar al servicio para deshabilitar el cliente.
                        _clienteService.DeshabilitarCliente(clienteId);

                        // 4. Recargar el DataGridView.
                        CargarDatosClientes();
                        MessageBox.Show("Cliente deshabilitado exitosamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al deshabilitar el cliente: {ex.Message}".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila para deshabilitar.".Traducir(), "Advertencia".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el ID del Tipo de Cliente ( 0=Minorista, 1=Mayorista) a buscar:".Traducir());

            if (int.TryParse(input, out int idTipoCliente))
            {
                try
                {
                    // 1. Llamar al servicio para obtener la lista filtrada.
                    List<ClienteDTO> listaFiltrada = _clienteService.BuscarClientesPorTipo(idTipoCliente);

                    // 2. Asignar la lista filtrada como nueva fuente de datos.
                    dgvCliente.DataSource = listaFiltrada;
                    ConfigurarColumnasDataGridView();

                    MessageBox.Show($"Se encontraron {listaFiltrada.Count} clientes para el ID Tipo {idTipoCliente}.".Traducir(), "Búsqueda Exitosa".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error durante la búsqueda: {ex.Message}".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (!string.IsNullOrEmpty(input))
            {
                MessageBox.Show("El ID del Tipo de Cliente debe ser un número entero válido.".Traducir(), "Error de Entrada".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

