using FormUI.Inicio;
using Services.Facade;
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
                // 1. Instanciamos tu lógica de sucursales
                Logic.SucursalLogic sucursalLogic = new Logic.SucursalLogic();

                // 2. Traemos la lista de DTOs usando tu método
                var listaSucursales = sucursalLogic.ObtenerTodasLasSucursales();

                // 3. Atamos la lista al ComboBox
                cmbSucursales.DataSource = listaSucursales;

                // 4. Le decimos qué propiedad del DTO mostrar al usuario
                // (Asegúrate de que la propiedad en tu SucursalDTO se llame exactamente así)
                cmbSucursales.DisplayMember = "Direccion";

                // 5. Le decimos qué propiedad del DTO guardar como "Valor real"
                cmbSucursales.ValueMember = "IdSucursal";

                // Para que arranque vacío y obligue al Admin a elegir
                cmbSucursales.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las sucursales: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSucursales.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, elija una sucursal primero.");
                    return;
                }

                Guid idElegido = (Guid)cmbSucursales.SelectedValue; 
                SessionManager.Current.IdSucursalActual = idElegido;
                SessionManager.Current.NombreSucursalActual = cmbSucursales.Text;
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar ingresar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
