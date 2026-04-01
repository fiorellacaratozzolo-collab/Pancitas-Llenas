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
                // 1. Validamos las credenciales y creamos la sesión global
                LoginService.Login(txtbUsuario.Text, txtbContraseña.Text);

                // 2. Si pasó la línea anterior sin errores, recuperamos el usuario de la sesión actual
                Usuario usuario = SessionManager.Current.UsuarioLogueado;

                MessageBox.Show($"¡Bienvenido {usuario.Nombre} al sistema PetShop!", "Inicio Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Opcional: Mostramos la cantidad de permisos base (Familias/Patentes) que trajo de la BD para confirmar que el Composite funcionó
                MessageBox.Show($"Se han cargado {usuario.Privilegios.Count} roles/permisos principales de tu perfil.", "Diagnóstico de Seguridad");

                // 3. Ocultamos el Login y abrimos el Menú Principal
                this.Hide();

                // NOTA: FormPrincipal ya no necesita recibir (usuario) porque lo saca del SessionManager
                new Inicio.FormPrincipal().ShowDialog();

                this.Show(); // Cuando se cierre el FormPrincipal, vuelve a aparecer el Login

                // Limpiamos los campos por seguridad
                txtbUsuario.Clear();
                txtbContraseña.Clear();
            }
            catch (Exception ex)
            {
                // Si la BLL lanza una excepción (ej: "Usuario incorrecto"), cae aquí
                MessageBox.Show(ex.Message, "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}

