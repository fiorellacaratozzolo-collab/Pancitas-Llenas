using FormUI.Inicio;
using Logic.Facade;
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
        /// <summary>
        /// Inicializa el formulario de selección de sucursal y sus componentes visuales.
        /// </summary>
        public FormSeleccionSucursal()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento de carga inicial que obtiene únicamente las sucursales ACTIVAS de la base de datos, 
        /// configura el menú desplegable con una opción por defecto y traduce la interfaz.
        /// </summary>
        private void FormSeleccionSucursal_Load(object sender, EventArgs e)
        {
            try
            {
                Logic.SucursalLogic sucursalLogic = new Logic.SucursalLogic();
                var listaSucursales = sucursalLogic.ObtenerActivas().ToList();

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
                MessageBox.Show(string.Format("Error al cargar las sucursales: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            TraductorUI.TraducirFormulario(this);
        }

        /// <summary>
        /// Valida la sucursal elegida, la asigna a la sesión activa del usuario y cierra el formulario devolviendo un resultado exitoso para continuar al menú principal.
        /// </summary>
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
                MessageBox.Show(string.Format("Ocurrió un error al intentar ingresar: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }   
}
