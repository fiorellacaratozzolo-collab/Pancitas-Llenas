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
        private Guid? _proveedorSeleccionadoId = null;
        private bool _viendoActivos = true;

        /// <summary>
        /// Inicializa el formulario y enlaza los eventos principales.
        /// </summary>
        public FormGestiónProveedor()
        {
            InitializeComponent();
            dgvProveedor.SelectionChanged += dgvProveedor_SelectionChanged;
            ConfigurarEstadoInicial();
        }
        /// <summary>
        /// Establece la interfaz visual por defecto mostrando los proveedores activos.
        /// </summary>
        private void ConfigurarEstadoInicial()
        {
            _viendoActivos = true;
            btnHabilitar.Visible = false;
            btnDeshabilitar.Visible = true;
            btnVerDeshabilitados.Text = "Ver Deshabilitados".Traducir();

            CargarDatosProveedores();
        }
        /// <summary>
        /// Obtiene la lista de proveedores filtrando por el estado actual de la vista (_viendoActivos).
        /// </summary>
        private void CargarDatosProveedores()
        {
            try
            {
                List<ProveedorDTO> proveedores = _viendoActivos
                    ? _proveedorService.ObtenerActivos()
                    : _proveedorService.ObtenerDeshabilitados();

                dgvProveedor.DataSource = null;
                dgvProveedor.DataSource = proveedores;

                ConfigurarColumnasDataGridView();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar los proveedores: {0}".Traducir(), ex.Message), "Error de Conexión".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Oculta columnas técnicas e IDs irrelevantes para el usuario, aplica traducciones y bloquea la grilla.
        /// </summary>
        private void ConfigurarColumnasDataGridView()
        {
            dgvProveedor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dgvProveedor.DataSource == null) return;

            dgvProveedor.ReadOnly = true;
            dgvProveedor.AllowUserToAddRows = false;
            dgvProveedor.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            string[] columnasOcultas = { "IdProveedor", "ProveedorProductos", "NombreConPeso", "StockPorSucursal", "VentaDetalles" };

            foreach (var col in columnasOcultas)
            {
                if (dgvProveedor.Columns.Contains(col)) dgvProveedor.Columns[col].Visible = false;
            }

            if (dgvProveedor.Columns.Contains("NombreProveedor")) dgvProveedor.Columns["NombreProveedor"].HeaderText = "Nombre".Traducir();
            if (dgvProveedor.Columns.Contains("Cuit")) dgvProveedor.Columns["Cuit"].HeaderText = "CUIT".Traducir();
            if (dgvProveedor.Columns.Contains("Telefono")) dgvProveedor.Columns["Telefono"].HeaderText = "Teléfono".Traducir();
            if (dgvProveedor.Columns.Contains("Direccion")) dgvProveedor.Columns["Direccion"].HeaderText = "Dirección".Traducir();

            dgvProveedor.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }
        /// <summary>
        /// Evento de carga del formulario que aplica el sistema de internacionalización a los controles visuales.
        /// </summary>
        private void FormGestiónProveedor_Load(object sender, EventArgs e)
        {
            TraductorUI.TraducirFormulario(this);
            LimpiarControles();
        }
        /// <summary>
        /// Valida los datos ingresados en el formulario y registra un nuevo proveedor (Alta).
        /// </summary>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            var nuevoProveedor = CrearDTO();

            try
            {
                _proveedorService.CreateProveedor(nuevoProveedor);
                MessageBox.Show("Proveedor agregado exitosamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatosProveedores();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrió un error al intentar agregar el proveedor: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Captura los datos de los TextBox y actualiza el proveedor seleccionado (Modificación).
        /// </summary>
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (_proveedorSeleccionadoId == null || !ValidarCampos()) return;

            var provActualizado = CrearDTO();
            provActualizado.IdProveedor = _proveedorSeleccionadoId.Value;

            try
            {
                // NOTA: Asegurate de tener UpdateProveedor en tu Service/Logic
                _proveedorService.UpdateProveedor(provActualizado);
                MessageBox.Show("Los datos del proveedor han sido actualizados correctamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatosProveedores();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al actualizar el proveedor: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Solicita confirmación y ejecuta la baja lógica del proveedor y sus productos en cascada.
        /// </summary>
        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (_proveedorSeleccionadoId == null) return;

            string nombre = txtbNombreProv.Text;

            string mensaje = string.Format("¿Está seguro de deshabilitar a {0}?\n\nATENCIÓN: Esto también deshabilitará automáticamente todos los productos exclusivos de este proveedor del catálogo.".Traducir(), nombre);

            if (MessageBox.Show(mensaje, "Confirmar Baja".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _proveedorService.DeshabilitarProveedor(_proveedorSeleccionadoId.Value);
                    MessageBox.Show("Proveedor y sus productos asociados fueron deshabilitados exitosamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatosProveedores();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al deshabilitar el Proveedor: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Botón que restablece manualmente los campos para preparar un alta nueva.
        /// </summary>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
        }
        /// <summary>
        /// Alterna la vista de la grilla entre proveedores Activos e Inactivos.
        /// </summary>
        private void btnVerDeshabilitados_Click(object sender, EventArgs e)
        {
            _viendoActivos = !_viendoActivos;

            btnVerDeshabilitados.Text = _viendoActivos ? "Ver Deshabilitados".Traducir() : "Ver Activos".Traducir();
            btnHabilitar.Visible = !_viendoActivos;
            btnDeshabilitar.Visible = _viendoActivos;

            CargarDatosProveedores();
        }
        /// <summary>
        /// Solicita confirmación y vuelve a activar un proveedor deshabilitado.
        /// </summary>
        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            if (_proveedorSeleccionadoId == null) return;

            if (MessageBox.Show("¿Desea reactivar este proveedor?".Traducir(), "Confirmar".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _proveedorService.HabilitarProveedor(_proveedorSeleccionadoId.Value);
                    CargarDatosProveedores();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al habilitar: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Escucha el clic explícito en la grilla, carga los datos en los TextBoxes y maneja la Máquina de Estados de los botones.
        /// </summary>
        private void dgvProveedor_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProveedor.CurrentRow != null && dgvProveedor.CurrentRow.Selected && dgvProveedor.DataSource != null)
            {
                _proveedorSeleccionadoId = (Guid?)dgvProveedor.CurrentRow.Cells["IdProveedor"].Value;

                txtbNombreProv.Text = dgvProveedor.CurrentRow.Cells["NombreProveedor"].Value?.ToString();
                txtbCuitProv.Text = dgvProveedor.CurrentRow.Cells["Cuit"].Value?.ToString();
                txtbTelefonoProv.Text = dgvProveedor.CurrentRow.Cells["Telefono"].Value?.ToString();
                txtbDireccionProv.Text = dgvProveedor.CurrentRow.Cells["Direccion"].Value?.ToString();

                btnAgregar.Enabled = false;
                btnActualizar.Enabled = true;
                btnDeshabilitar.Enabled = _viendoActivos;
                btnHabilitar.Enabled = !_viendoActivos;
            }
        }
        /// <summary>
        /// Vacía los campos de texto y resetea los botones al estado de seguridad inicial.
        /// </summary>
        private void LimpiarControles()
        {
            _proveedorSeleccionadoId = null;
            txtbNombreProv.Text = string.Empty;
            txtbCuitProv.Text = string.Empty;
            txtbTelefonoProv.Text = string.Empty;
            txtbDireccionProv.Text = string.Empty;

            if (dgvProveedor.DataSource != null) dgvProveedor.ClearSelection();

            btnAgregar.Enabled = _viendoActivos;
            btnActualizar.Enabled = false;
            btnDeshabilitar.Enabled = false;
            btnHabilitar.Enabled = false;

            txtbNombreProv.Focus();
        }

        /// <summary>
        /// Valida que los campos requeridos y formatos numéricos sean correctos.
        /// </summary>
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtbNombreProv.Text))
            {
                MessageBox.Show("El campo Nombre es obligatorio.".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!int.TryParse(txtbCuitProv.Text, out _) || !int.TryParse(txtbTelefonoProv.Text, out _))
            {
                MessageBox.Show("CUIT y Teléfono deben ser números válidos.".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Genera un objeto ProveedorDTO con los valores actuales del formulario.
        /// </summary>
        private ProveedorDTO CrearDTO()
        {
            return new ProveedorDTO
            {
                NombreProveedor = txtbNombreProv.Text.Trim(),
                Cuit = int.Parse(txtbCuitProv.Text),
                Telefono = int.Parse(txtbTelefonoProv.Text),
                Direccion = txtbDireccionProv.Text.Trim(),
                Activo = _viendoActivos
            };
        }
    }

}

