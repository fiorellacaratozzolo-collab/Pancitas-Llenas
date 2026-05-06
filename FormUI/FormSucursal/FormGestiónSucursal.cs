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

        /// <summary>
        /// Inicializa el formulario, instancia el servicio necesario y carga los datos iniciales de las grillas y combos.
        /// </summary>
        public FormGestiónSucursal()
        {
            InitializeComponent();
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
                    sucursales = _sucursalService.GetAllSucursales();
                }
                dgvSucursal.DataSource = sucursales;
                ConfigurarColumnasDataGridView();
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
            dgvSucursal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dgvSucursal.Columns.Contains("IdSucursal"))
                dgvSucursal.Columns["IdSucursal"].Visible = false;
            if (dgvSucursal.Columns.Contains("EncargadoSucursals"))
                dgvSucursal.Columns["EncargadoSucursals"].Visible = false;
            if (dgvSucursal.Columns.Contains("IdTipoSucursalNavigation"))
                dgvSucursal.Columns["IdTipoSucursalNavigation"].Visible = false;
            if (dgvSucursal.Columns.Contains("IdTipoSucursal"))
                dgvSucursal.Columns["IdTipoSucursal"].Visible = false;
            if (dgvSucursal.Columns.Contains("StockPorSucursals"))
                dgvSucursal.Columns["StockPorSucursals"].Visible = false;
            if (dgvSucursal.Columns.Contains("NombreSucursal"))
                dgvSucursal.Columns["NombreSucursal"].HeaderText = "Nombre de Sucursal".Traducir();
        }
        /// <summary>
        /// Obtiene todas las sucursales y formatea su información para poblar el menú desplegable de selección rápida.
        /// </summary>
        private void CargarDireccionesEnComboBox()
        {
            try
            {
                var sucursales = _sucursalService.GetAllSucursales();

                var dataSource = sucursales
                    .Select(s => new {
                        IdSucursal = s.IdSucursal,
                        DireccionCompleta = s.Direccion + " - " + s.NombreSucursal
                    })
                    .ToList();

                dataSource.Insert(0, new { IdSucursal = Guid.Empty, DireccionCompleta = "--- Mostrar Todas las Sucursales ---".Traducir() });

                cmbSeleccionSucursal.DataSource = dataSource;
                cmbSeleccionSucursal.DisplayMember = "DireccionCompleta";
                cmbSeleccionSucursal.ValueMember = "IdSucursal";
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
        }
        /// <summary>
        /// Valida los datos ingresados en el formulario y registra una nueva sucursal en el sistema.
        /// </summary>
        private void btnAltaSucursal_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtbNombreSucursal.Text) || string.IsNullOrWhiteSpace(txtbDireccionSucursal.Text))
            {
                MessageBox.Show("Nombre y Dirección son obligatorios.".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtbTelefonoSucursal.Text, out int telefonoValue))
            {
                MessageBox.Show("Teléfono debe ser un número válido.".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idTipoSucursal = 0;

            if (rbtnVenta.Checked)
            {
                idTipoSucursal = 1;
            }
            else if (rbtnDepositoVenta.Checked)
            {
                idTipoSucursal = 2;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Tipo de Sucursal (Venta o Depósito-Venta).".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var nuevaSucursalDTO = new SucursalDTO
            {
                NombreSucursal = txtbNombreSucursal.Text.Trim(),
                Direccion = txtbDireccionSucursal.Text.Trim(),
                Telefono = telefonoValue,
                IdTipoSucursal = idTipoSucursal
            };

            try
            {
                _sucursalService.CreateSucursal(nuevaSucursalDTO);

                MessageBox.Show("Sucursal creada exitosamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarControles();
                CargarDatosSucursales();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al crear la sucursal: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Aplica y guarda en la base de datos las modificaciones realizadas a la sucursal actualmente seleccionada en la grilla.
        /// </summary>
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvSucursal.CurrentRow != null && !dgvSucursal.CurrentRow.IsNewRow && dgvSucursal.CurrentRow.DataBoundItem is SucursalDTO sucursalEditada)
            {
                try
                {
                    _sucursalService.UpdateSucursal(sucursalEditada);

                    MessageBox.Show("Los cambios se guardaron correctamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatosSucursales();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al guardar los cambios: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione la sucursal que desea modificar de la lista.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// Solicita confirmación y ejecuta la baja lógica de la sucursal seleccionada en el sistema.
        /// </summary>
        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (dgvSucursal.CurrentRow != null && !dgvSucursal.CurrentRow.IsNewRow)
            {
                Guid sucursalId = (Guid)(dgvSucursal.CurrentRow.Cells["IdSucursal"].Value ?? Guid.Empty);
                string nombre = dgvSucursal.CurrentRow.Cells["NombreSucursal"].Value?.ToString() ?? "[Desconocido]".Traducir();

                if (sucursalId == Guid.Empty || MessageBox.Show(string.Format("¿Desea deshabilitar {0}?".Traducir(), nombre), "Confirmar".Traducir(), MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                try
                {
                    _sucursalService.DisableSucursal(sucursalId);
                    CargarDatosSucursales();
                    MessageBox.Show("Sucursal deshabilitada.".Traducir(), "Éxito".Traducir());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error: {0}".Traducir(), ex.Message), "Error".Traducir());
                }
            }
        }
        /// <summary>
        /// Restablece los campos de texto y botones de opción del formulario a su estado original vacío.
        /// </summary>
        private void LimpiarControles()
        {
            txtbNombreSucursal.Text = string.Empty;
            txtbDireccionSucursal.Text = string.Empty;
            txtbTelefonoSucursal.Text = string.Empty;

            rbtnVenta.Checked = false;
            rbtnDepositoVenta.Checked = false;

            txtbNombreSucursal.Focus();
        }
        /// <summary>
        /// Captura la selección del menú desplegable y filtra la grilla para mostrar la información de una sucursal específica o todas.
        /// </summary>
        private void cmbSeleccionSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSeleccionSucursal.SelectedValue is Guid idSucursal)
            {
                try
                {
                    if (idSucursal == Guid.Empty)
                    {
                        CargarDatosSucursales(_sucursalService.GetAllSucursales());
                    }
                    else
                    {
                        SucursalDTO? sucursalUnicaDTO = _sucursalService.GetById(idSucursal);

                        if (sucursalUnicaDTO != null)
                        {
                            CargarDatosSucursales(new List<SucursalDTO> { sucursalUnicaDTO });
                        }
                        else
                        {
                            CargarDatosSucursales(new List<SucursalDTO>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al filtrar por dirección: {0}".Traducir(), ex.Message), "Error de Filtrado".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
