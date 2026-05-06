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
        private readonly ClienteService _clienteService = new ClienteService();

        /// <summary>
        /// Inicializa el formulario, suscribe los eventos de formato visual y desencadena la carga inicial de los clientes en la grilla.
        /// </summary>
        public FormGestiónCliente()
        {
            InitializeComponent();
            dgvCliente.CellFormatting += dgvCliente_CellFormatting;
            CargarDatosClientes();
        }

        /// <summary>
        /// Aplica las traducciones correspondientes a todos los controles visuales del formulario al cargar.
        /// </summary>
        private void FormGestiónCliente_Load(object sender, EventArgs e)
        {
            TraductorUI.TraducirFormulario(this);
        }

        /// <summary>
        /// Consulta el servicio de clientes para obtener todos los registros y vincula los datos a la grilla principal.
        /// </summary>
        private void CargarDatosClientes()
        {
            try
            {
                List<ClienteDTO> listaClientes = _clienteService.GetAllClientes();
                dgvCliente.DataSource = null;
                dgvCliente.DataSource = listaClientes;
                ConfigurarColumnasDataGridView();
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

            if (dgvCliente.Columns.Contains("NombreCliente"))
            {
                dgvCliente.Columns["NombreCliente"].HeaderText = "Nombre".Traducir();
            }
            if (dgvCliente.Columns.Contains("Dni"))
            {
                dgvCliente.Columns["Dni"].HeaderText = "D.N.I.".Traducir();
            }
            if (dgvCliente.Columns.Contains("IdTipoCliente"))
            {
                dgvCliente.Columns["IdTipoCliente"].HeaderText = "Tipo Cliente".Traducir();
            }

            dgvCliente.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dgvCliente.AllowUserToAddRows = false;
        }

        /// <summary>
        /// Valida los datos de entrada, determina el tipo de cliente y registra un nuevo cliente en la base de datos.
        /// </summary>
        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
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

                MessageBox.Show("Cliente agregado exitosamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarControles();
                CargarDatosClientes();
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(string.Format("Error de datos: {0}".Traducir(), ex.Message), "Error de Alta".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrió un error al intentar agregar el cliente: {0}".Traducir(), ex.Message), "Error Inesperado".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Restablece los campos de texto y botones de selección a su estado predeterminado.
        /// </summary>
        private void LimpiarControles()
        {
            txtbNombreCliente.Text = string.Empty;
            txtbDNI.Text = string.Empty;
            rbtnMayorista.Checked = false;
            rbtnMinorista.Checked = false;
            txtbNombreCliente.Focus();
        }

        /// <summary>
        /// Verifica la selección en la grilla y envía los datos del cliente modificado al servicio para su actualización.
        /// </summary>
        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            if (dgvCliente.CurrentRow != null && !dgvCliente.CurrentRow.IsNewRow && dgvCliente.CurrentRow.DataBoundItem is ClienteDTO clienteEditado)
            {
                if (clienteEditado.IdCliente == Guid.Empty)
                {
                    MessageBox.Show("El cliente seleccionado no es válido o está incompleto.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

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
            else
            {
                MessageBox.Show("Por favor, seleccione un cliente válido de la lista para modificar.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Solicita confirmación y ejecuta la baja lógica del cliente seleccionado en el sistema.
        /// </summary>
        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (dgvCliente.CurrentRow != null && !dgvCliente.CurrentRow.IsNewRow)
            {
                Guid clienteId = (Guid)dgvCliente.CurrentRow.Cells["IdCliente"].Value;
                string nombre = dgvCliente.CurrentRow.Cells["NombreCliente"].Value?.ToString() ?? string.Empty;

                DialogResult dialogResult = MessageBox.Show(string.Format("¿Está seguro de deshabilitar a {0}?".Traducir(), nombre), "Confirmar Acción".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        _clienteService.DeshabilitarCliente(clienteId);
                        CargarDatosClientes();
                        MessageBox.Show("Cliente deshabilitado exitosamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Error al deshabilitar el cliente: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila para deshabilitar.".Traducir(), "Advertencia".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    List<ClienteDTO> listaFiltrada = _clienteService.BuscarClientesPorTipo(idTipoCliente);

                    dgvCliente.DataSource = null;
                    dgvCliente.DataSource = listaFiltrada;
                    ConfigurarColumnasDataGridView();

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
        /// Intercepta el dibujado de las celdas para transformar el valor numérico del Tipo de Cliente en una etiqueta legible para el usuario.
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
    }
}