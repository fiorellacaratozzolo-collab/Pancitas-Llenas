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
using Services.Facade.Extensions;

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
            TraductorUI.TraducirFormulario(this);
        }

        private void btnIngresar_Click_1(object sender, EventArgs e)
        {
            try
            {
                Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();
                var usuarioValidado = usuarioBll.ValidarCredenciales(txtbUsuario.Text, txtbContraseña.Text);
                // Leemos el idioma del usuario (si está vacío en la BD, forzamos español de Argentina)
                string culturaNombre = string.IsNullOrEmpty(usuarioValidado.IdiomaPredeterminado)
                                       ? "es-AR"
                                       : usuarioValidado.IdiomaPredeterminado;

                var cultura = new System.Globalization.CultureInfo(culturaNombre);
                System.Threading.Thread.CurrentThread.CurrentCulture = cultura;
                System.Threading.Thread.CurrentThread.CurrentUICulture = cultura;

                SessionManager.Current.Login(usuarioValidado);

                // Verificamos si es Admin (sucursal en null)
                if (usuarioValidado.IdSucursal == null)
                {
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
                        SessionManager.Current.Logout();
                        MessageBox.Show("Operación cancelada. Debe seleccionar una sucursal para poder ingresar al sistema.".Traducir(), "Seguridad".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    FormPrincipal formPrincipal = new FormPrincipal();
                    formPrincipal.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de autenticación".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}

