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

namespace FormUI.FormCompra
{
    public partial class FormGestiónProveedor : Form
    {
        private readonly ProveedorService _proveedorService = new ProveedorService();

        /// <summary>
        /// Inicializa el formulario, instancia los servicios necesarios y ejecuta la carga inicial de proveedores.
        /// </summary>
        public FormGestiónProveedor()
        {
            InitializeComponent();
            CargarDatosProveedores();
        }
        /// <summary>
        /// Obtiene la lista de proveedores desde la capa de servicios y actualiza la fuente de datos de la grilla.
        /// </summary>
        private void CargarDatosProveedores()
        {
            try
            {
                List<ProveedorDTO> listaProveedor = _proveedorService.GetAllProveedores();
                dgvProveedor.DataSource = listaProveedor;
                ConfigurarColumnasDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar los proveedores: {0}".Traducir(), ex.Message), "Error de Conexión".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Oculta columnas técnicas e IDs irrelevantes para el usuario y aplica traducciones a los encabezados visibles.
        /// </summary>
        private void ConfigurarColumnasDataGridView()
        {
            dgvProveedor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvProveedor.Columns.Contains("IdProveedor"))
                dgvProveedor.Columns["IdProveedor"].Visible = false;

            if (dgvProveedor.Columns.Contains("ProveedorProductos"))
                dgvProveedor.Columns["ProveedorProductos"].Visible = false;

            if (dgvProveedor.Columns.Contains("NombreConPeso"))
                dgvProveedor.Columns["NombreConPeso"].Visible = false;

            if (dgvProveedor.Columns.Contains("StockPorSucursal"))
                dgvProveedor.Columns["StockPorSucursal"].Visible = false;

            if (dgvProveedor.Columns.Contains("VentaDetalles"))
                dgvProveedor.Columns["VentaDetalles"].Visible = false;

            if (dgvProveedor.Columns.Contains("NombreProveedor"))
                dgvProveedor.Columns["NombreProveedor"].HeaderText = "Nombre".Traducir();

            if (dgvProveedor.Columns.Contains("Cuit"))
                dgvProveedor.Columns["Cuit"].HeaderText = "CUIT".Traducir();

            if (dgvProveedor.Columns.Contains("Telefono"))
                dgvProveedor.Columns["Telefono"].HeaderText = "Teléfono".Traducir(); // Corregido tilde visual

            if (dgvProveedor.Columns.Contains("Direccion"))
                dgvProveedor.Columns["Direccion"].HeaderText = "Dirección".Traducir();

            dgvProveedor.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
        /// <summary>
        /// Evento de carga del formulario que aplica el sistema de internacionalización a los controles visuales.
        /// </summary>
        private void FormGestiónProveedor_Load(object sender, EventArgs e)
        {
            TraductorUI.TraducirFormulario(this);
        }
        /// <summary>
        /// Valida los datos ingresados en el formulario y registra un nuevo proveedor en la base de datos.
        /// </summary>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtbNombreProv.Text))
            {
                MessageBox.Show("El campo Nombre es obligatorio.".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtbCuitProv.Text, out int cuitValue) || !int.TryParse(txtbTelefonoProv.Text, out int telefonoValue))
            {
                MessageBox.Show("Debe ser un número válido.".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var nuevoProveedorDTO = new ProveedorDTO
            {
                NombreProveedor = txtbNombreProv.Text.Trim(),
                Cuit = cuitValue,
                Telefono = telefonoValue,
                Direccion = txtbDireccionProv.Text.Trim(),
            };

            try
            {
                Guid newProveedorId = _proveedorService.CreateProveedor(nuevoProveedorDTO);
                MessageBox.Show("Proveedor agregado exitosamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarControles();
                CargarDatosProveedores();
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(string.Format("Error de datos: {0}".Traducir(), ex.Message), "Error de Alta".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrió un error al intentar agregar el proveedor: {0}".Traducir(), ex.Message), "Error Inesperado".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Vacía los campos de texto del formulario preparándolos para un nuevo ingreso de datos.
        /// </summary>
        private void LimpiarControles()
        {
            txtbNombreProv.Text = string.Empty;
            txtbCuitProv.Text = string.Empty;
            txtbTelefonoProv.Text = string.Empty;
            txtbDireccionProv.Text = string.Empty;
            txtbNombreProv.Focus();
        }
        /// <summary>
        /// Fuerza la recarga de los datos en la grilla desde la base de datos y avisa al usuario.
        /// </summary>
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarDatosProveedores();
            MessageBox.Show("Lista de proveedores actualizada.".Traducir(), "Información".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Solicita confirmación y ejecuta la baja lógica del proveedor seleccionado en la grilla principal.
        /// </summary>
        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (dgvProveedor.CurrentRow != null && !dgvProveedor.CurrentRow.IsNewRow)
            {
                Guid proveedorId = (Guid)dgvProveedor.CurrentRow.Cells["IdProveedor"].Value;
                string nombre = dgvProveedor.CurrentRow.Cells["NombreProveedor"].Value?.ToString() ?? string.Empty;

                DialogResult dialogResult = MessageBox.Show(string.Format("¿Está seguro de deshabilitar a {0}?".Traducir(), nombre), "Confirmar Acción".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        _proveedorService.DeshabilitarProveedor(proveedorId);
                        CargarDatosProveedores();
                        MessageBox.Show("Proveedor deshabilitado exitosamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Error al deshabilitar el Proveedor: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila para deshabilitar.".Traducir(), "Advertencia".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
