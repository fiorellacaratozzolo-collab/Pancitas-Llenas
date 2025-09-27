using DataAccess.Models;
using Logic.Facade;
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
    public partial class FormGestiónProveedor : Form
    {
        public FormGestiónProveedor()
        {
            InitializeComponent();
            CargarDatosProveedores();
        }

        // Instantiate the service (can also be done in the constructor)
        private readonly ProveedorService _proveedorService = new ProveedorService();


        // =================================================================
        // MÉTODO DE CARGA DE DATOS PARA EL DATAGRIDVIEW
        // =================================================================

        /// <summary>
        /// Obtiene la lista de clientes y la asigna al DataGridView (dgvClientes).
        /// </summary>
        private void CargarDatosProveedores()
        {
            try
            {
                // 1. Obtener la lista de clientes de la capa de servicio/lógica.
                // Asumiendo que has agregado este método en la capa Logic/Service:
                List<Proveedor> listaProveedor = _proveedorService.GetAllProveedores();

                // 2. Asignar la lista como fuente de datos del DataGridView.
                dgvProveedor.DataSource = listaProveedor;

                // 3. Configurar la apariencia de las columnas.
                ConfigurarColumnasDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los proveedores: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ajusta la visibilidad y el encabezado de las columnas generadas.
        /// </summary>
        private void ConfigurarColumnasDataGridView()
        {
            // Ocultar las propiedades de navegación y el ID que no son relevantes para el usuario
            if (dgvProveedor.Columns.Contains("IdProveedor"))
            {
                dgvProveedor.Columns["IdProveedor"].Visible = false;
            }
            if (dgvProveedor.Columns.Contains("ProveedorProductos"))
            {
                dgvProveedor.Columns["ProveedorProductos"].Visible = false;
            }
            // Renombrar columnas para la visualización del usuario
            if (dgvProveedor.Columns.Contains("NombreProveedor"))
            {
                dgvProveedor.Columns["NombreProveedor"].HeaderText = "Nombre";
            }
            if (dgvProveedor.Columns.Contains("Cuit"))
            {
                dgvProveedor.Columns["Cuit"].HeaderText = "CUIT";
            }
            if (dgvProveedor.Columns.Contains("Telefono"))
            {
                dgvProveedor.Columns["Telefono"].HeaderText = "Telefóno";
            }
            if (dgvProveedor.Columns.Contains("Direccion"))
            {
                dgvProveedor.Columns["Direccion"].HeaderText = "Dirección";
            }

            // Ajustar el ancho de las columnas
            dgvProveedor.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
        private void GestiónProveedorForm_Load(object sender, EventArgs e)
        {

        }

        private void FormGestiónProveedor_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // 1. Obtención y Validación de Datos
            if (string.IsNullOrWhiteSpace(txtbNombreProv.Text))
            {
                MessageBox.Show("El campo Nombre es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtbCuitProv.Text, out int cuitValue) || !int.TryParse(txtbTelefonoProv.Text, out int telefonoValue))
            {
                MessageBox.Show("Debe ser un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Creación del Objeto Modelo
            var nuevoProveedor = new Proveedor
            {
                // IdProveedor is generated in the Repository layer (ProveedorRepository.Create)
                NombreProveedor = txtbNombreProv.Text.Trim(),
                Cuit = cuitValue,
                Telefono = telefonoValue,
                Direccion = txtbDireccionProv.Text.Trim(),
            };

            // 3. Llamada a la Lógica de Negocio y Manejo de Excepciones
            try
            {
                // Call the service layer method to persist the client
                Guid newProveedorId = _proveedorService.CreateProveedor(nuevoProveedor);

                MessageBox.Show($"Proveedor agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show($"Ocurrió un error al intentar agregar el proveedor: {ex.Message}", "Error Inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LimpiarControles()
        {
            txtbNombreProv.Text = string.Empty;
            txtbCuitProv.Text = string.Empty;
            txtbTelefonoProv.Text = string.Empty;
            txtbDireccionProv.Text = string.Empty;
            txtbNombreProv.Focus();
        }


        private void btnActualizar_Click(object sender, EventArgs e)
        {
            // Llama al método existente para recargar la lista completa.
            CargarDatosProveedores();
            MessageBox.Show("Lista de proveedoes actualizada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (dgvProveedor.CurrentRow != null)
            {
                // 1. Obtener la clave primaria del proveedor seleccionado.
                // Se asume que la columna IdProveedor existe, aunque esté oculta.
                Guid proveedorId = (Guid)dgvProveedor.CurrentRow.Cells["IdProveedor"].Value;
                string nombre = dgvProveedor.CurrentRow.Cells["NombreProveedor"].Value?.ToString() ?? string.Empty;

                // 2. Confirmación del usuario.
                DialogResult dialogResult = MessageBox.Show($"¿Está seguro de deshabilitar a {nombre}?", "Confirmar Acción", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        // 3. Llamar al servicio para deshabilitar el cliente.
                        _proveedorService.DeshabilitarProveedor(proveedorId);

                        // 4. Recargar el DataGridView.
                        CargarDatosProveedores();
                        MessageBox.Show("Proveedor deshabilitado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al deshabilitar el Proveedor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila para deshabilitar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvProveedor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
