using FormUI.Inicio;
using Services.Facade;
using Services.Facade.Extensions;

namespace FormUI
{
    public partial class FormLogin : Form
    {
        /// <summary>
        /// Inicializa el formulario de inicio de sesión y sus componentes visuales.
        /// </summary>
        public FormLogin()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Evento de carga inicial que aplica las traducciones predeterminadas de la interfaz antes de que el usuario inicie sesión.
        /// </summary>
        private void FormLogin_Load(object sender, EventArgs e)
        {
            TraductorUI.TraducirFormulario(this);
        }
        /// <summary>
        /// Captura las credenciales ingresadas, valida al usuario, establece su idioma de preferencia en el hilo actual y lo redirige a la pantalla principal o al selector de sucursales según sus permisos.
        /// </summary>
        private void btnIngresar_Click_1(object sender, EventArgs e)
        {
            try
            {
                Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();
                var usuarioValidado = usuarioBll.ValidarCredenciales(txtbUsuario.Text, txtbContraseña.Text);

                string culturaNombre = string.IsNullOrEmpty(usuarioValidado.IdiomaPredeterminado)
                                       ? "es-AR"
                                       : usuarioValidado.IdiomaPredeterminado;

                var cultura = new System.Globalization.CultureInfo(culturaNombre);
                System.Threading.Thread.CurrentThread.CurrentCulture = cultura;
                System.Threading.Thread.CurrentThread.CurrentUICulture = cultura;

                SessionManager.Current.Login(usuarioValidado);

                if (usuarioValidado.IdSucursal == null)
                {
                    FormSeleccionSucursal formSucursal = new FormSeleccionSucursal();
                    DialogResult resultado = formSucursal.ShowDialog();

                    if (resultado == DialogResult.OK)
                    {
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

