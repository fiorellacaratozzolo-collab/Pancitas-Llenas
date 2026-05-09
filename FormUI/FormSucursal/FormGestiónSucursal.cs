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

namespace FormUI.FormSucursal
{
    public partial class FormGestiónSucursal : Form
    {
        private readonly SucursalService _sucursalService = new SucursalService();
        private Guid? _sucursalSeleccionadaId = null;
        private bool _viendoActivas = true;

        /// <summary>
        /// Inicializa el formulario, instancia el servicio necesario y carga los datos iniciales de las grillas y combos.
        /// </summary>
        public FormGestiónSucursal()
        {
            InitializeComponent();
            dgvSucursal.SelectionChanged += dgvSucursal_SelectionChanged;
            dgvSucursal.CellFormatting += dgvSucursal_CellFormatting;

            ConfigurarEstadoInicial();
        }

        /// <summary>
        /// Establece la interfaz visual por defecto mostrando las sucursales activas.
        /// </summary>
        private void ConfigurarEstadoInicial()
        {
            _viendoActivas = true;
            btnHabilitar.Visible = false;
            btnDeshabilitar.Visible = true;
            btnVerDeshabilitados.Text = "Ver Deshabilitadas".Traducir();

            CargarDatosSucursales();
            CargarDireccionesEnComboBox();
        }

        /// <summary>
        /// Consulta la base de datos o recibe una lista filtrada para poblar la grilla principal de sucursales.
        /// </summary>
        private void CargarDatosSucursales(List<SucursalDTO>? sucursales = null)
        {
            try
            {
                if (sucursales == null)
                {
                    sucursales = _viendoActivas
                        ? _sucursalService.ObtenerActivas()
                        : _sucursalService.ObtenerDeshabilitadas();
                }

                dgvSucursal.DataSource = null;
                dgvSucursal.DataSource = sucursales;

                ConfigurarColumnasDataGridView();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar las sucursales: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Oculta columnas técnicas e IDs de la grilla de sucursales y aplica traducciones a los encabezados visibles.
        /// </summary>
        private void ConfigurarColumnasDataGridView()
        {
            if (dgvSucursal.DataSource == null) return;

            dgvSucursal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSucursal.AllowUserToAddRows = false;
            dgvSucursal.ReadOnly = true;
            dgvSucursal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            string[] columnasOcultas = { "IdSucursal", "EncargadoSucursals", "IdTipoSucursalNavigation", "StockPorSucursals", "Activo" };

            foreach (var col in columnasOcultas)
            {
                if (dgvSucursal.Columns.Contains(col)) dgvSucursal.Columns[col].Visible = false;
            }

            if (dgvSucursal.Columns.Contains("NombreSucursal")) dgvSucursal.Columns["NombreSucursal"].HeaderText = "Nombre de Sucursal".Traducir();
            if (dgvSucursal.Columns.Contains("Direccion")) dgvSucursal.Columns["Direccion"].HeaderText = "Dirección".Traducir();
            if (dgvSucursal.Columns.Contains("Telefono")) dgvSucursal.Columns["Telefono"].HeaderText = "Teléfono".Traducir();
            if (dgvSucursal.Columns.Contains("IdTipoSucursal")) dgvSucursal.Columns["IdTipoSucursal"].HeaderText = "Tipo de Sucursal".Traducir();

            dgvSucursal.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        /// <summary>
        /// Obtiene todas las sucursales y formatea su información para poblar el menú desplegable de selección rápida.
        /// </summary>
        private void CargarDireccionesEnComboBox()
        {
            try
            {
                // El combobox de búsqueda siempre trae las activas
                var sucursales = _sucursalService.ObtenerActivas();

                var dataSource = sucursales
                    .Select(s => new
                    {
                        IdSucursal = s.IdSucursal,
                        DireccionCompleta = s.Direccion + " - " + s.NombreSucursal
                    })
                    .ToList();

                dataSource.Insert(0, new { IdSucursal = Guid.Empty, DireccionCompleta = "--- Mostrar Todas las Sucursales ---".Traducir() });

                cmbSeleccionSucursal.SelectedIndexChanged -= cmbSeleccionSucursal_SelectedIndexChanged; // Previene disparo accidental
                cmbSeleccionSucursal.DataSource = dataSource;
                cmbSeleccionSucursal.DisplayMember = "DireccionCompleta";
                cmbSeleccionSucursal.ValueMember = "IdSucursal";
                cmbSeleccionSucursal.SelectedIndexChanged += cmbSeleccionSucursal_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar el selector de direcciones: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento de carga del formulario que aplica las traducciones de interfaz según el idioma del usuario.
        /// </summary>
        private void FormGestiónSucursal_Load_1(object sender, EventArgs e)
        {
            TraductorUI.TraducirFormulario(this);
            LimpiarControles();
        }

        /// <summary>
        /// Valida los datos ingresados en el formulario y registra una nueva sucursal.
        /// </summary>
        private void btnAltaSucursal_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            var nuevaSucursal = CrearDTO();

            try
            {
                _sucursalService.CreateSucursal(nuevaSucursal);
                MessageBox.Show("Sucursal creada exitosamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatosSucursales();
                CargarDireccionesEnComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al crear la sucursal: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Solicita confirmación y ejecuta la baja lógica de la sucursal seleccionada.
        /// </summary>
        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (_sucursalSeleccionadaId == null) return;

            string nombre = txtbNombreSucursal.Text;

            if (MessageBox.Show(string.Format("¿Desea deshabilitar la sucursal {0}?".Traducir(), nombre), "Confirmar Baja".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _sucursalService.DisableSucursal(_sucursalSeleccionadaId.Value);
                    MessageBox.Show("Sucursal deshabilitada.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatosSucursales();
                    CargarDireccionesEnComboBox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al deshabilitar: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Restablece los campos de texto y botones de opción del formulario a su estado original.
        /// </summary>
        private void LimpiarControles()
        {
            _sucursalSeleccionadaId = null;
            txtbNombreSucursal.Text = string.Empty;
            txtbDireccionSucursal.Text = string.Empty;
            txtbTelefonoSucursal.Text = string.Empty;
            rbtnVenta.Checked = false;
            rbtnDepositoVenta.Checked = false;

            if (dgvSucursal.DataSource != null) dgvSucursal.ClearSelection();

            btnAltaSucursal.Enabled = _viendoActivas;
            btnActualizar.Enabled = false;
            btnDeshabilitar.Enabled = false;
            btnHabilitar.Enabled = false;

            txtbNombreSucursal.Focus();
        }

        /// <summary>
        /// Captura la selección del menú desplegable y filtra la grilla para mostrar una sucursal específica.
        /// </summary>
        private void cmbSeleccionSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSeleccionSucursal.SelectedValue is Guid idSucursal)
            {
                try
                {
                    if (idSucursal == Guid.Empty)
                    {
                        // Resetea a la vista normal
                        CargarDatosSucursales();
                    }
                    else
                    {
                        SucursalDTO? sucursalUnica = _sucursalService.GetById(idSucursal);
                        if (sucursalUnica != null)
                        {
                            CargarDatosSucursales(new List<SucursalDTO> { sucursalUnica });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al filtrar por dirección: {0}".Traducir(), ex.Message), "Error de Filtrado".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Captura los datos de los controles y actualiza la sucursal seleccionada.
        /// </summary>
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (_sucursalSeleccionadaId == null)
            {
                MessageBox.Show("Seleccione una sucursal para actualizar.".Traducir(), "Advertencia".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCampos()) return;

            var sucursalEditada = CrearDTO();
            sucursalEditada.IdSucursal = _sucursalSeleccionadaId.Value;

            try
            {
                _sucursalService.UpdateSucursal(sucursalEditada);
                MessageBox.Show("Sucursal actualizada correctamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatosSucursales();
                CargarDireccionesEnComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al actualizar la sucursal: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Intercepta el dibujado de celdas para que el Tipo de Sucursal (1 o 2) se muestre con un texto legible.
        /// </summary>
        private void dgvSucursal_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvSucursal.Columns[e.ColumnIndex].Name == "IdTipoSucursal" && e.Value != null)
            {
                if (e.Value is int idTipo)
                {
                    e.Value = idTipo == 1 ? "Venta".Traducir() : "Depósito-Venta".Traducir();
                    e.FormattingApplied = true;
                }
            }
        }

        /// <summary>
        /// Escucha el clic en la grilla, carga los datos en TextBoxes/RadioButtons y maneja los botones.
        /// </summary>
        private void dgvSucursal_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSucursal.CurrentRow != null && dgvSucursal.CurrentRow.Selected && dgvSucursal.DataSource != null)
            {
                _sucursalSeleccionadaId = (Guid?)dgvSucursal.CurrentRow.Cells["IdSucursal"].Value;

                txtbNombreSucursal.Text = dgvSucursal.CurrentRow.Cells["NombreSucursal"].Value?.ToString();
                txtbDireccionSucursal.Text = dgvSucursal.CurrentRow.Cells["Direccion"].Value?.ToString();
                txtbTelefonoSucursal.Text = dgvSucursal.CurrentRow.Cells["Telefono"].Value?.ToString();

                if (dgvSucursal.CurrentRow.Cells["IdTipoSucursal"].Value is int idTipo)
                {
                    if (idTipo == 1) rbtnVenta.Checked = true;
                    else if (idTipo == 2) rbtnDepositoVenta.Checked = true;
                }

                btnAltaSucursal.Enabled = false;
                btnActualizar.Enabled = true;
                btnDeshabilitar.Enabled = _viendoActivas;
                btnHabilitar.Enabled = !_viendoActivas;
            }
        }

        /// <summary>
        /// Solicita confirmación y vuelve a habilitar una sucursal inactiva.
        /// </summary>
        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            if (_sucursalSeleccionadaId == null) return;

            if (MessageBox.Show("¿Desea reactivar esta sucursal?".Traducir(), "Confirmar".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _sucursalService.HabilitarSucursal(_sucursalSeleccionadaId.Value);
                    MessageBox.Show("Sucursal habilitada exitosamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatosSucursales();
                    CargarDireccionesEnComboBox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al habilitar: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Alterna la vista de la grilla entre sucursales Activas e Inactivas.
        /// </summary>
        private void btnVerDeshabilitados_Click(object sender, EventArgs e)
        {
            _viendoActivas = !_viendoActivas;

            btnVerDeshabilitados.Text = _viendoActivas ? "Ver Deshabilitadas".Traducir() : "Ver Activas".Traducir();
            btnHabilitar.Visible = !_viendoActivas;
            btnDeshabilitar.Visible = _viendoActivas;

            CargarDatosSucursales();
        }

        /// <summary>
        /// Limpia los campos llamando al método centralizado.
        /// </summary>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
        }

        /// <summary>
        /// Valida que los datos ingresados en el formulario sean correctos.
        /// </summary>
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtbNombreSucursal.Text) || string.IsNullOrWhiteSpace(txtbDireccionSucursal.Text))
            {
                MessageBox.Show("Nombre y Dirección son obligatorios.".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!int.TryParse(txtbTelefonoSucursal.Text, out _))
            {
                MessageBox.Show("Teléfono debe ser un número válido.".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!rbtnVenta.Checked && !rbtnDepositoVenta.Checked)
            {
                MessageBox.Show("Debe seleccionar un Tipo de Sucursal (Venta o Depósito-Venta).".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Crea un SucursalDTO leyendo los controles del formulario y respetando la vista actual.
        /// </summary>
        private SucursalDTO CrearDTO()
        {
            return new SucursalDTO
            {
                NombreSucursal = txtbNombreSucursal.Text.Trim(),
                Direccion = txtbDireccionSucursal.Text.Trim(),
                Telefono = int.Parse(txtbTelefonoSucursal.Text),
                IdTipoSucursal = rbtnVenta.Checked ? 1 : 2,
                Activo = _viendoActivas
            };
        }
    }
}
