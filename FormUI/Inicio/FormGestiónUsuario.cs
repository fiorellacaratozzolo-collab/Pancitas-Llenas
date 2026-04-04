using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormUI.Inicio
{
    public partial class FormGestiónUsuario : Form
    {
        // Esta variable recordará a quién le hicimos clic en la grilla
        private Guid _idUsuarioSeleccionado = Guid.Empty;

        public FormGestiónUsuario()
        {
            InitializeComponent();
        }

        private void FormGestiónUsuario_Load(object sender, EventArgs e)
        {

        }

        private void CargarGrillaUsuarios()
        {
            try
            {
                Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();

                // Asignamos la lista de usuarios al DataGridView
                dgvUsuarios.DataSource = null; // Reseteamos por si había algo antes
                dgvUsuarios.DataSource = usuarioBll.ListarTodos();

                // Emprolijamos las columnas
                if (dgvUsuarios.Columns.Count > 0)
                {
                    // Ocultamos el ID interno
                    if (dgvUsuarios.Columns["IdUsuario"] != null)
                        dgvUsuarios.Columns["IdUsuario"].Visible = false;

                    // Ocultamos la lista interna del Composite
                    if (dgvUsuarios.Columns["Privilegios"] != null)
                        dgvUsuarios.Columns["Privilegios"].Visible = false;

                    // ¡MUY IMPORTANTE! Ocultamos el Hash de la contraseña
                    if (dgvUsuarios.Columns["Password"] != null)
                        dgvUsuarios.Columns["Password"].Visible = false;

                    // Ajustamos el email para que ocupe el resto del espacio
                    if (dgvUsuarios.Columns["Email"] != null)
                        dgvUsuarios.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Instanciamos la BLL
                Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();

                // 2. Llamamos al método pasándole los TextBox de la pantalla
                usuarioBll.RegistrarUsuario(
                    txtbNombreUsuario.Text,
                    txtbEmail.Text,
                    txtbContraseña.Text
                );

                // 3. Avisamos al administrador
                MessageBox.Show("Usuario creado con éxito.", "Alta Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 4. Limpiamos los campos para dejarlo listo para otro usuario
                txtbNombreUsuario.Clear();
                txtbEmail.Clear();
                txtbContraseña.Clear();
                txtbNombreUsuario.Focus(); // Llevamos el cursor al primer textbox

                // 5. ¡Recargamos la grilla para ver al nuevo usuario en vivo!
                CargarGrillaUsuarios();
            }
            catch (Exception ex)
            {
                // Si hay un error (ej. campos vacíos), lo atrapamos acá sin que explote el sistema
                MessageBox.Show(ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificamos que no hayan hecho clic en los títulos de las columnas (índice -1)
            if (e.RowIndex >= 0)
            {
                // Agarramos la fila seleccionada
                DataGridViewRow fila = dgvUsuarios.Rows[e.RowIndex];

                // Guardamos el ID en nuestra variable secreta
                _idUsuarioSeleccionado = Guid.Parse(fila.Cells["IdUsuario"].Value.ToString());

                // Llenamos los TextBox para que el usuario pueda editar
                txtbNombreUsuario.Text = fila.Cells["Nombre"].Value.ToString();
                txtbEmail.Text = fila.Cells["Email"].Value.ToString();

                // La clave NUNCA se baja a la vista, la dejamos vacía
                txtbContraseña.Clear();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();

                usuarioBll.ActualizarUsuario(_idUsuarioSeleccionado, txtbNombreUsuario.Text, txtbEmail.Text);

                MessageBox.Show("Usuario actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiamos todo
                _idUsuarioSeleccionado = Guid.Empty;
                txtbNombreUsuario.Clear();
                txtbEmail.Clear();

                CargarGrillaUsuarios(); // Recargamos para ver los cambios
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idUsuarioSeleccionado == Guid.Empty)
                {
                    MessageBox.Show("Seleccione un usuario de la lista primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Le preguntamos si está seguro (Buenas prácticas)
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea deshabilitar este usuario?", "Confirmar Baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();
                    usuarioBll.DeshabilitarUsuario(_idUsuarioSeleccionado);

                    MessageBox.Show("Usuario deshabilitado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _idUsuarioSeleccionado = Guid.Empty;
                    txtbNombreUsuario.Clear();
                    txtbEmail.Clear();

                    CargarGrillaUsuarios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();

                // Buscamos usando lo que escribieron en el txtbNombreUsuario
                var resultados = usuarioBll.BuscarUsuarios(txtbNombreUsuario.Text);

                // Actualizamos la grilla
                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = resultados;

                // Volvemos a emprolijar las columnas (ocultar ID y Password)
                if (dgvUsuarios.Columns.Count > 0)
                {
                    if (dgvUsuarios.Columns["IdUsuario"] != null) dgvUsuarios.Columns["IdUsuario"].Visible = false;
                    if (dgvUsuarios.Columns["Privilegios"] != null) dgvUsuarios.Columns["Privilegios"].Visible = false;
                    if (dgvUsuarios.Columns["Password"] != null) dgvUsuarios.Columns["Password"].Visible = false;
                    if (dgvUsuarios.Columns["Email"] != null) dgvUsuarios.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string PedirNuevaContraseñaCartel()
        {
            Form prompt = new Form()
            {
                Width = 350,
                Height = 160,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Modificar Contraseña",
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblTexto = new Label() { Left = 20, Top = 20, Text = "Ingrese la nueva contraseña:", Width = 300 };
            // ¡Aquí aplicamos la seguridad visual para que se vean los puntitos!
            TextBox txtClave = new TextBox() { Left = 20, Top = 50, Width = 290, UseSystemPasswordChar = true };
            Button btnAceptar = new Button() { Text = "Aceptar", Left = 210, Width = 100, Top = 80, DialogResult = DialogResult.OK };
            Button btnCancelar = new Button() { Text = "Cancelar", Left = 100, Width = 100, Top = 80, DialogResult = DialogResult.Cancel };

            prompt.Controls.Add(lblTexto);
            prompt.Controls.Add(txtClave);
            prompt.Controls.Add(btnAceptar);
            prompt.Controls.Add(btnCancelar);
            prompt.AcceptButton = btnAceptar;

            // Mostramos el cartel y, si apreta Aceptar, devolvemos lo que escribió
            return prompt.ShowDialog(this) == DialogResult.OK ? txtClave.Text : "";
        }

        private void btnModificarContra_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idUsuarioSeleccionado == Guid.Empty)
                {
                    MessageBox.Show("Seleccione un usuario de la lista primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. Abrimos el cartel mágico
                string nuevaClave = PedirNuevaContraseñaCartel();

                // Si el usuario presionó Cancelar o lo dejó vacío, frenamos todo
                if (string.IsNullOrWhiteSpace(nuevaClave))
                {
                    return;
                }

                // 2. Pedimos la confirmación de seguridad
                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea sobrescribir la contraseña del usuario seleccionado?", "Confirmar Cambio", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();

                    // 3. Mandamos la nueva clave a encriptar y guardar
                    usuarioBll.ModificarContraseña(_idUsuarioSeleccionado, nuevaClave);

                    // 4. Mostramos el éxito
                    MessageBox.Show("Contraseña actualizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _idUsuarioSeleccionado = Guid.Empty;
                    txtbNombreUsuario.Clear();
                    txtbEmail.Clear();
                    txtbContraseña.Clear();

                    CargarGrillaUsuarios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idUsuarioSeleccionado == Guid.Empty)
                {
                    MessageBox.Show("Seleccione un usuario de la lista primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea volver a habilitar a este usuario?", "Confirmar Habilitación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();
                    usuarioBll.HabilitarUsuario(_idUsuarioSeleccionado);

                    MessageBox.Show("Usuario habilitado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _idUsuarioSeleccionado = Guid.Empty;
                    txtbNombreUsuario.Clear();
                    txtbEmail.Clear();

                    CargarGrillaUsuarios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
