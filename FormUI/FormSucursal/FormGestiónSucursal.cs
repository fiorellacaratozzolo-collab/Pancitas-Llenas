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

namespace FormUI.FormSucursal
{
    public partial class FormGestiónSucursal : Form
    {
        private readonly SucursalService _sucursalService = new SucursalService();
        public FormGestiónSucursal()
        {
            InitializeComponent();
            CargarDatosSucursales();
            CargarDireccionesEnComboBox();
        }

        private void CargarDatosSucursales(List<Sucursal>? sucursales = null)
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
                MessageBox.Show($"Error al cargar las sucursales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnasDataGridView()
        {
            if (dgvSucursal.Columns.Contains("IdSucursal"))
                dgvSucursal.Columns["IdSucursal"].Visible = false;
            if (dgvSucursal.Columns.Contains("EncargadoSucursals"))
                dgvSucursal.Columns["EncargadoSucursals"].Visible = false;
            if (dgvSucursal.Columns.Contains("IdTipoSucursalNavigation"))
                dgvSucursal.Columns["IdTipoSucursalNavigation"].Visible = false;
            if (dgvSucursal.Columns.Contains("IdTipoSucursal"))
                dgvSucursal.Columns["IdTipoSucursal"].Visible = false;
            if (dgvSucursal.Columns.Contains("NombreSucursal"))
                dgvSucursal.Columns["NombreSucursal"].HeaderText = "Nombre";

            dgvSucursal.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void CargarDireccionesEnComboBox()
        {
            try
            {
                // 1. Obtener todas las sucursales
                var sucursales = _sucursalService.GetAllSucursales();

                // 2. Crear una lista o DataTable para vincular al ComboBox
                // Usamos la lista de Sucursales directamente o un tipo anónimo si solo queremos Nombre/Direccion.

                // Creamos una lista de objetos que solo contengan los datos relevantes
                var dataSource = sucursales
                    .Select(s => new {
                        IdSucursal = s.IdSucursal,
                        DireccionCompleta = s.Direccion + " - " + s.NombreSucursal // Formato amigable para el usuario
                    })
                    .ToList();

                // Agregar una opción para "Mostrar Todas"
                dataSource.Insert(0, new { IdSucursal = Guid.Empty, DireccionCompleta = "--- Mostrar Todas las Sucursales ---" });

                cmbSeleccionSucursal.DataSource = dataSource;
                cmbSeleccionSucursal.DisplayMember = "DireccionCompleta"; // Lo que se ve
                cmbSeleccionSucursal.ValueMember = "IdSucursal";         // El valor asociado

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el selector de direcciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormGestiónSucursal_Load(object sender, EventArgs e)
        {

        }

        private void FormGestiónSucursal_Load_1(object sender, EventArgs e)
        {

        }

        private void btnAltaSucursal_Click(object sender, EventArgs e)
        {
            // 1. Validación y obtención de datos
            if (string.IsNullOrWhiteSpace(txtbNombreSucursal.Text) || string.IsNullOrWhiteSpace(txtbDireccionSucursal.Text))
            {
                MessageBox.Show("Nombre y Dirección son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtbTelefonoSucursal.Text, out int telefonoValue))
            {
                // Si el teléfono es opcional (int?), puedes omitir esta validación si el campo está vacío.
                MessageBox.Show("Teléfono debe ser un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Determinar el IdTipoSucursal basado en los RadioButtons
            int idTipoSucursal = 0;
            // ASUMIMOS: 1 = Venta, 2 = Deposito-Venta
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
                MessageBox.Show("Debe seleccionar un Tipo de Sucursal (Venta o Depósito-Venta).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Creación del objeto
            var nuevaSucursal = new Sucursal
            {
                NombreSucursal = txtbNombreSucursal.Text.Trim(),
                Direccion = txtbDireccionSucursal.Text.Trim(),
                Telefono = telefonoValue,
                IdTipoSucursal = idTipoSucursal
            };

            // 3. Llamada al servicio
            try
            {
                _sucursalService.CreateSucursal(nuevaSucursal);

                MessageBox.Show("Sucursal creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarControles();

                // Recargar la lista para mostrar el nuevo registro
                CargarDatosSucursales();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear la sucursal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            CargarDatosSucursales();
            MessageBox.Show("Lista de sucursales actualizada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (dgvSucursal.CurrentRow != null)
            {
                Guid sucursalId = (Guid)(dgvSucursal.CurrentRow.Cells["IdSucursal"].Value ?? Guid.Empty);
                string nombre = dgvSucursal.CurrentRow.Cells["NombreSucursal"].Value?.ToString() ?? "[Desconocido]";

                if (sucursalId == Guid.Empty || MessageBox.Show($"¿Desea deshabilitar {nombre}?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                try
                {
                    _sucursalService.DisableSucursal(sucursalId);
                    CargarDatosSucursales();
                    MessageBox.Show("Sucursal deshabilitada.", "Éxito");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error");
                }
            }
        }

        private void LimpiarControles()
        {
            // Campos de Sucursal
            txtbNombreSucursal.Text = string.Empty;
            txtbDireccionSucursal.Text = string.Empty;
            txtbTelefonoSucursal.Text = string.Empty;

            // Controles de Tipo (RadioButton)
            rbtnVenta.Checked = false;
            rbtnDepositoVenta.Checked = false;

            // Campos del Encargado (si los usas para el alta)
            txtbNombreEncargado.Text = string.Empty;
            txtbDniEncargado.Text = string.Empty;

            // Foco en el primer campo
            txtbNombreSucursal.Focus();
        }

        private void cmbSeleccionSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el ID de la sucursal seleccionada
            if (cmbSeleccionSucursal.SelectedValue is Guid idSucursal)
            {
                try
                {
                    if (idSucursal == Guid.Empty)
                    {
                        // Opción "Mostrar Todas" seleccionada
                        CargarDatosSucursales(_sucursalService.GetAllSucursales());
                    }
                    else
                    {
                        // Opción de una Sucursal específica: cargamos solo esa sucursal
                        Sucursal? sucursalUnica = _sucursalService.GetById(idSucursal);

                        if (sucursalUnica != null)
                        {
                            // Convertimos la sucursal única en una lista para el DataGridView
                            CargarDatosSucursales(new List<Sucursal> { sucursalUnica });
                        }
                        else
                        {
                            // Si por alguna razón el ID es válido pero el objeto no existe.
                            CargarDatosSucursales(new List<Sucursal>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al filtrar por dirección: {ex.Message}", "Error de Filtrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
