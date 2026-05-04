using FormUI.Inicio;
using ModelsDTO;
using Services.Facade;
using Services.Facade.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormUI
{
    public partial class FormSeleccionSucursal : Form
    {
        public FormSeleccionSucursal()
        {
            InitializeComponent();
        }

        private void FormSeleccionSucursal_Load(object sender, EventArgs e)
        {
            try
            {
                Logic.SucursalLogic sucursalLogic = new Logic.SucursalLogic();
                var listaSucursales = sucursalLogic.ObtenerTodasLasSucursales().ToList();
                var sucursalPlaceholder = new SucursalDTO
                {
                    IdSucursal = Guid.Empty,
                    Direccion = "--- Seleccione una sucursal ---".Traducir()
                };

                listaSucursales.Insert(0, sucursalPlaceholder);
                cmbSucursales.DataSource = listaSucursales;
                cmbSucursales.DisplayMember = "Direccion";
                cmbSucursales.ValueMember = "IdSucursal";
                cmbSucursales.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las sucursales: {ex.Message}".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            TraductorUI.TraducirFormulario(this);
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                Guid idElegido = (Guid)(cmbSucursales.SelectedValue ?? Guid.Empty);
                if (cmbSucursales.SelectedItem == null || idElegido == Guid.Empty)
                {
                    MessageBox.Show("Por favor, elija una sucursal primero.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SessionManager.Current.IdSucursalActual = idElegido;
                SessionManager.Current.NombreSucursalActual = cmbSucursales.Text;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al intentar ingresar: {ex.Message}".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
