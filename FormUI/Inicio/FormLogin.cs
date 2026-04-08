using FormUI.Inicio;
using Services.DomainModel.Composite;
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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
        private void btnIngresar_Click(object sender, EventArgs e)
        {
        }


        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnIngresar_Click_1(object sender, EventArgs e)
        {
            try
            {
                Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();
                var usuarioValidado = usuarioBll.ValidarCredenciales(txtbUsuario.Text, txtbContraseña.Text);
                SessionManager.Current.Login(usuarioValidado);

                // Verificamos si es Admin (sucursal en null)
                if (usuarioValidado.IdSucursal == null)
                {
                    // OJO: Cambia el nombre por tu form real
                    FormSeleccionSucursal formSucursal = new FormSeleccionSucursal();

                    // Usamos ShowDialog para pausar el código hasta que la ventana se cierre
                    DialogResult resultado = formSucursal.ShowDialog();

                    if (resultado == DialogResult.OK)
                    {
                        // ¡Todo perfecto! Eligió la sucursal y le dio a Aceptar
                        FormPrincipal formPrincipal = new FormPrincipal();
                        formPrincipal.Show();
                        this.Hide();
                    }
                    else
                    {
                        // El admin apretó Cancelar o la "X" roja sin elegir nada
                        // Cerramos la sesión por seguridad y lo dejamos en la pantalla de Login
                        SessionManager.Current.Logout();
                        MessageBox.Show("Operación cancelada. Debe seleccionar una sucursal para poder ingresar al sistema.", "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    // Es empleado normal, pasa directo
                    FormPrincipal formPrincipal = new FormPrincipal();
                    formPrincipal.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}

