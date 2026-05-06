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

namespace FormUI.Inicio
{
    public partial class FormGestiónUsuario : Form
    {
        private Guid _idUsuarioSeleccionado = Guid.Empty;

        /// <summary>
        /// Inicializa el formulario y sus componentes visuales predeterminados.
        /// </summary>
        public FormGestiónUsuario()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Evento de carga inicial que obtiene las sucursales, configura el menú desplegable, carga la grilla de usuarios y traduce la interfaz.
        /// </summary>
        private void FormGestiónUsuario_Load(object sender, EventArgs e)
        {
            try
            {
                Logic.SucursalLogic sucursalLogic = new Logic.SucursalLogic();
                var sucursalesDb = sucursalLogic.ObtenerTodasLasSucursales();
                var listaCombo = new List<object>();

                listaCombo.Add(new { Id = Guid.Empty, Texto = "ADMINISTRADOR (Sin Sucursal Fija)".Traducir() });

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
                MessageBox.Show(string.Format("Error al inicializar el formulario: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            TraductorUI.TraducirFormulario(this);
        }
        /// <summary>
        /// Consulta los usuarios del sistema, cruza los datos con sus sucursales asignadas y configura la grilla principal.
        /// </summary>
        private void CargarGrillaUsuarios()
        {
            try
            {
                Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();
                Logic.SucursalLogic sucursalLogic = new Logic.SucursalLogic();

                var usuarios = usuarioBll.ListarTodos();
                var sucursales = sucursalLogic.ObtenerTodasLasSucursales();

                if (usuarios == null) return;

                var datosGrilla = usuarios.Select(u => new UsuarioGrillaVisual
                {
                    IdUsuario = u.IdUsuario,
                    Nombre = u.Nombre ?? "",
                    Email = u.Email ?? "",
                    Habilitado = u.Habilitado,
                    IdSucursal = u.IdSucursal,
                    SucursalAsignada = u.IdSucursal.HasValue && u.IdSucursal.Value != Guid.Empty && sucursales != null
                        ? sucursales.FirstOrDefault(s => s.IdSucursal == u.IdSucursal.Value)?.Direccion ?? "Sucursal Eliminada".Traducir()
                        : "ADMINISTRADOR (Sin Sucursal Fija)".Traducir()
                }).ToList();

                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = datosGrilla;

                if (dgvUsuarios.Columns.Count > 0)
                {
                    if (dgvUsuarios.Columns.Contains("IdUsuario")) dgvUsuarios.Columns["IdUsuario"].Visible = false;
                    if (dgvUsuarios.Columns.Contains("IdSucursal")) dgvUsuarios.Columns["IdSucursal"].Visible = false;

                    if (dgvUsuarios.Columns.Contains("Habilitado")) dgvUsuarios.Columns["Habilitado"].Width = 70;

                    if (dgvUsuarios.Columns.Contains("SucursalAsignada"))
                    {
                        dgvUsuarios.Columns["SucursalAsignada"].HeaderText = "Sucursal".Traducir();
                        dgvUsuarios.Columns["SucursalAsignada"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    }

                    if (dgvUsuarios.Columns.Contains("Email"))
                        dgvUsuarios.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    if (dgvUsuarios.Columns.Contains("Nombre")) dgvUsuarios.Columns["Nombre"].HeaderText = "Nombre".Traducir();
                    if (dgvUsuarios.Columns.Contains("Email")) dgvUsuarios.Columns["Email"].HeaderText = "Email".Traducir();
                    if (dgvUsuarios.Columns.Contains("Habilitado")) dgvUsuarios.Columns["Habilitado"].HeaderText = "Habilitado".Traducir();

                    foreach (DataGridViewColumn col in dgvUsuarios.Columns)
                    {
                        col.ReadOnly = (col.Name != "Habilitado");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar la lista de usuarios: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Recopila los datos ingresados en el formulario y solicita a la capa de negocios el registro de un nuevo usuario.
        /// </summary>
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

                MessageBox.Show("Usuario creado con éxito.".Traducir(), "Alta Exitosa".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtbNombreUsuario.Clear();
                txtbEmail.Clear();
                txtbContraseña.Clear();
                cmbSucursales.SelectedIndex = 0;

                CargarGrillaUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Traducir(), "Atención".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// Detecta la selección de una fila en la grilla y carga los datos del usuario en los campos de edición correspondientes.
        /// </summary>
        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvUsuarios.Rows[e.RowIndex];

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
        /// <summary>
        /// Recopila las modificaciones realizadas a un usuario existente y las envía a la base de datos, gestionando también su habilitación/deshabilitación.
        /// </summary>
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idUsuarioSeleccionado == Guid.Empty)
                {
                    MessageBox.Show("Seleccione un usuario de la lista primero.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dgvUsuarios.EndEdit();

                Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();

                Guid? sucursalSeleccionada = null;
                string valorCombo = cmbSucursales.SelectedValue?.ToString() ?? "";

                if (!string.IsNullOrEmpty(valorCombo) && Guid.TryParse(valorCombo, out Guid idParseado) && idParseado != Guid.Empty)
                {
                    sucursalSeleccionada = idParseado;
                }

                string nombreSeguro = txtbNombreUsuario.Text ?? "";
                string emailSeguro = txtbEmail.Text ?? "";

                usuarioBll.ActualizarUsuario(
                    _idUsuarioSeleccionado,
                    nombreSeguro,
                    emailSeguro,
                    sucursalSeleccionada
                );

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

                MessageBox.Show("Usuario actualizado con éxito.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);

                _idUsuarioSeleccionado = Guid.Empty;
                txtbNombreUsuario.Clear();
                txtbEmail.Clear();
                cmbSucursales.SelectedIndex = 0;

                CargarGrillaUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// Genera y muestra un cuadro de diálogo dinámico que solicita y retorna una nueva contraseña.
        /// </summary>
        private string PedirNuevaContraseñaCartel()
        {
            Form prompt = new Form()
            {
                Width = 350,
                Height = 160,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Modificar Contraseña".Traducir(),
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblTexto = new Label() { Left = 20, Top = 20, Text = "Ingrese la nueva contraseña:".Traducir(), Width = 300 };
            TextBox txtClave = new TextBox() { Left = 20, Top = 50, Width = 290, UseSystemPasswordChar = true };
            Button btnAceptar = new Button() { Text = "Aceptar".Traducir(), Left = 210, Width = 100, Top = 80, DialogResult = DialogResult.OK };
            Button btnCancelar = new Button() { Text = "Cancelar".Traducir(), Left = 100, Width = 100, Top = 80, DialogResult = DialogResult.Cancel };

            prompt.Controls.Add(lblTexto);
            prompt.Controls.Add(txtClave);
            prompt.Controls.Add(btnAceptar);
            prompt.Controls.Add(btnCancelar);
            prompt.AcceptButton = btnAceptar;

            return prompt.ShowDialog(this) == DialogResult.OK ? txtClave.Text : "";
        }
        /// <summary>
        /// Dispara el proceso de cambio de contraseña, solicita validación de seguridad y actualiza la clave en la base de datos.
        /// </summary>
        private void btnModificarContra_Click(object sender, EventArgs e)
        {
            try
            {
                if (_idUsuarioSeleccionado == Guid.Empty)
                {
                    MessageBox.Show("Seleccione un usuario de la lista primero.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nuevaClave = PedirNuevaContraseñaCartel();

                if (string.IsNullOrWhiteSpace(nuevaClave))
                {
                    return;
                }

                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea sobrescribir la contraseña del usuario seleccionado?".Traducir(), "Confirmar Cambio".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();
                    usuarioBll.ModificarContraseña(_idUsuarioSeleccionado, nuevaClave);

                    MessageBox.Show("Contraseña actualizada con éxito.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _idUsuarioSeleccionado = Guid.Empty;
                    txtbNombreUsuario.Clear();
                    txtbEmail.Clear();
                    txtbContraseña.Clear();

                    CargarGrillaUsuarios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

    /// <summary>
    /// Clase modelo auxiliar (DTO visual) utilizada para representar y formatear los datos de un usuario dentro de la grilla.
    /// </summary>
    public class UsuarioGrillaVisual
    {
        public Guid IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Habilitado { get; set; }
        public Guid? IdSucursal { get; set; }
        public string SucursalAsignada { get; set; } = string.Empty;
    }

}
