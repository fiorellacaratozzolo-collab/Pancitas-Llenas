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
            try
            {
                Logic.SucursalLogic sucursalLogic = new Logic.SucursalLogic();
                var sucursalesDb = sucursalLogic.ObtenerTodasLasSucursales();               
                var listaCombo = new List<object>();
                listaCombo.Add(new { Id = Guid.Empty, Texto = "ADMINISTRADOR (Sin Sucursal Fija)" });
                if (sucursalesDb != null)
                {
                    foreach (var s in sucursalesDb)
                    {
                        listaCombo.Add(new { Id = s.IdSucursal, Texto = s.Direccion });
                    }
                }

                cmbSucursales.DataSource = listaCombo;
                cmbSucursales.DisplayMember = "Texto";
                cmbSucursales.ValueMember = "Id";
                cmbSucursales.SelectedIndex = 0;

                CargarGrillaUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar el formulario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarGrillaUsuarios()
        {
            try
            {
                Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();
                Logic.SucursalLogic sucursalLogic = new Logic.SucursalLogic();

                var usuarios = usuarioBll.ListarTodos();
                var sucursales = sucursalLogic.ObtenerTodasLasSucursales();

                if (usuarios == null) return;

                // AQUÍ ESTÁ LA MAGIA: Usamos la nueva clase "UsuarioGrillaVisual"
                var datosGrilla = usuarios.Select(u => new UsuarioGrillaVisual
                {
                    IdUsuario = u.IdUsuario,
                    Nombre = u.Nombre ?? "",
                    Email = u.Email ?? "",
                    Habilitado = u.Habilitado,
                    IdSucursal = u.IdSucursal,

                    SucursalAsignada = u.IdSucursal.HasValue && u.IdSucursal.Value != Guid.Empty && sucursales != null
                        ? sucursales.FirstOrDefault(s => s.IdSucursal == u.IdSucursal.Value)?.Direccion ?? "Sucursal Eliminada"
                        : "ADMINISTRADOR (Sin Sucursal Fija)"
                }).ToList();

                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = datosGrilla;

                // RED DE SEGURIDAD PARA EVITAR EL ERROR DE "OBJECT REFERENCE"
                if (dgvUsuarios.Columns.Count > 0)
                {
                    if (dgvUsuarios.Columns.Contains("IdUsuario")) dgvUsuarios.Columns["IdUsuario"].Visible = false;
                    if (dgvUsuarios.Columns.Contains("IdSucursal")) dgvUsuarios.Columns["IdSucursal"].Visible = false;

                    if (dgvUsuarios.Columns.Contains("Habilitado")) dgvUsuarios.Columns["Habilitado"].Width = 70;

                    if (dgvUsuarios.Columns.Contains("SucursalAsignada"))
                    {
                        dgvUsuarios.Columns["SucursalAsignada"].HeaderText = "Sucursal";
                        dgvUsuarios.Columns["SucursalAsignada"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    }

                    if (dgvUsuarios.Columns.Contains("Email"))
                        dgvUsuarios.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    // Bloqueamos todas las columnas EXCEPTO el CheckBox
                    foreach (DataGridViewColumn col in dgvUsuarios.Columns)
                    {
                        col.ReadOnly = (col.Name != "Habilitado");
                    }
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
                Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();

                Guid? sucursalSeleccionada = null;
                if (cmbSucursales.SelectedValue != null && Guid.TryParse(cmbSucursales.SelectedValue.ToString(), out Guid idParseado) && idParseado != Guid.Empty)
                {
                    sucursalSeleccionada = idParseado;
                }

                usuarioBll.RegistrarUsuario(
                    txtbNombreUsuario.Text,
                    txtbEmail.Text,
                    txtbContraseña.Text,
                    sucursalSeleccionada
                );

                MessageBox.Show("Usuario creado con éxito.", "Alta Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtbNombreUsuario.Clear();
                txtbEmail.Clear();
                txtbContraseña.Clear();
                cmbSucursales.SelectedIndex = 0;

                CargarGrillaUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvUsuarios.Rows[e.RowIndex];

                // Soluciona la advertencia del Guid.Parse
                if (fila.Cells["IdUsuario"].Value != null)
                {
                    _idUsuarioSeleccionado = Guid.Parse(fila.Cells["IdUsuario"].Value.ToString()!);
                }

                txtbNombreUsuario.Text = fila.Cells["Nombre"].Value?.ToString() ?? "";
                txtbEmail.Text = fila.Cells["Email"].Value?.ToString() ?? "";
                txtbContraseña.Clear();

                if (fila.Cells["IdSucursal"].Value != null && fila.Cells["IdSucursal"].Value != DBNull.Value)
                {
                    cmbSucursales.SelectedValue = Guid.Parse(fila.Cells["IdSucursal"].Value.ToString()!);
                }
                else
                {
                    cmbSucursales.SelectedValue = Guid.Empty;
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idUsuarioSeleccionado == Guid.Empty)
                {
                    MessageBox.Show("Seleccione un usuario de la lista primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Forzamos a la grilla a terminar cualquier edición pendiente
                dgvUsuarios.EndEdit();

                Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();

                // 1. OBTENER SUCURSAL (Protegido contra nulos)
                Guid? sucursalSeleccionada = null;
                string valorCombo = cmbSucursales.SelectedValue?.ToString() ?? "";

                if (!string.IsNullOrEmpty(valorCombo) && Guid.TryParse(valorCombo, out Guid idParseado) && idParseado != Guid.Empty)
                {
                    sucursalSeleccionada = idParseado;
                }

                // 2. ACTUALIZAR TEXTOS Y SUCURSAL (Protegido contra nulos de la UI)
                string nombreSeguro = txtbNombreUsuario.Text ?? "";
                string emailSeguro = txtbEmail.Text ?? "";

                usuarioBll.ActualizarUsuario(
                    _idUsuarioSeleccionado,
                    nombreSeguro,
                    emailSeguro,
                    sucursalSeleccionada
                );

                // 3. ACTUALIZAR ESTADO (Usando var para permitir el nulo que devuelve FirstOrDefault)
                // Y evitamos el casteo (Guid) comparando directamente los strings seguros
                var filaActual = dgvUsuarios.Rows.Cast<DataGridViewRow>()
                    .FirstOrDefault(r => r.Cells["IdUsuario"].Value?.ToString() == _idUsuarioSeleccionado.ToString());

                if (filaActual != null && filaActual.Cells["Habilitado"].Value != null)
                {
                    bool estaHabilitado = Convert.ToBoolean(filaActual.Cells["Habilitado"].Value);

                    if (estaHabilitado)
                        usuarioBll.HabilitarUsuario(_idUsuarioSeleccionado);
                    else
                        usuarioBll.DeshabilitarUsuario(_idUsuarioSeleccionado);
                }

                MessageBox.Show("Usuario actualizado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _idUsuarioSeleccionado = Guid.Empty;
                txtbNombreUsuario.Clear();
                txtbEmail.Clear();
                cmbSucursales.SelectedIndex = 0;

                CargarGrillaUsuarios();
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
            TextBox txtClave = new TextBox() { Left = 20, Top = 50, Width = 290, UseSystemPasswordChar = true };
            Button btnAceptar = new Button() { Text = "Aceptar", Left = 210, Width = 100, Top = 80, DialogResult = DialogResult.OK };
            Button btnCancelar = new Button() { Text = "Cancelar", Left = 100, Width = 100, Top = 80, DialogResult = DialogResult.Cancel };

            prompt.Controls.Add(lblTexto);
            prompt.Controls.Add(txtClave);
            prompt.Controls.Add(btnAceptar);
            prompt.Controls.Add(btnCancelar);
            prompt.AcceptButton = btnAceptar;
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

                // 1. Abrimos el cartel
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

    }

    public class UsuarioGrillaVisual
    {
        public Guid IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public bool Habilitado { get; set; } 
        public Guid? IdSucursal { get; set; }
        public string SucursalAsignada { get; set; }
    }

}
