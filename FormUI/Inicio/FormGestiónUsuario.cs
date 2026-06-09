using System.Data;
using Services.Facade.Extensions;

namespace FormUI.Inicio
{
    public partial class FormGestiónUsuario : Form
    {
        private Guid? _idUsuarioSeleccionado = null;
        private bool _viendoActivos = true;

        /// <summary>
        /// Inicializa el formulario y sus componentes visuales predeterminados.
        /// </summary>
        public FormGestiónUsuario()
        {
            InitializeComponent();
            dgvUsuarios.SelectionChanged += dgvUsuarios_SelectionChanged;
            dgvUsuarios.DataBindingComplete += dgvUsuarios_DataBindingComplete;
        }

        /// <summary>
        /// Evento de carga inicial que obtiene las sucursales, configura el menú desplegable y traduce la interfaz.
        /// </summary>
        private void FormGestiónUsuario_Load(object sender, EventArgs e)
        {
            try
            {
                Logic.SucursalLogic sucursalLogic = new Logic.SucursalLogic();
                var sucursalesDb = sucursalLogic.ObtenerActivas();
                var listaCombo = new List<object>();
                listaCombo.Add(new { Id = Guid.Empty, Texto = "--- Seleccione Sucursal ---".Traducir() });
                listaCombo.Add(new { Id = Guid.NewGuid(), Texto = "ADMINISTRADOR (Sin Sucursal Fija)".Traducir() });

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

                ConfigurarEstadoInicial();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al inicializar el formulario: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            TraductorUI.TraducirFormulario(this);
        }

        /// <summary>
        /// Establece la interfaz visual por defecto mostrando los usuarios activos.
        /// </summary>
        private void ConfigurarEstadoInicial()
        {
            _viendoActivos = true;
            btnHabilitar.Visible = false;
            btnDeshabilitar.Visible = true;
            btnVerDeshabilitados.Text = "Ver Deshabilitados".Traducir();

            CargarGrillaUsuarios();
        }

        /// <summary>
        /// Consulta los usuarios del sistema filtrados por estado, cruza los datos con sus sucursales y configura la grilla.
        /// </summary>
        private void CargarGrillaUsuarios()
        {
            try
            {
                Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();
                Logic.SucursalLogic sucursalLogic = new Logic.SucursalLogic();

                // Traemos los usuarios según la vista actual
                var usuarios = _viendoActivos ? usuarioBll.ObtenerActivos() : usuarioBll.ObtenerDeshabilitados();

                // Traemos todas las sucursales (incluso las inhabilitadas) para poder traducir el ID a Dirección
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

                ConfigurarColumnasDataGridView();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar la lista de usuarios: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Protege la grilla contra edición directa, oculta columnas técnicas y aplica traducciones.
        /// </summary>
        private void ConfigurarColumnasDataGridView()
        {
            if (dgvUsuarios.Columns.Count > 0)
            {
                // Grilla de solo lectura y selección de fila completa
                dgvUsuarios.ReadOnly = true;
                dgvUsuarios.AllowUserToAddRows = false;
                dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Ocultamos las columnas técnicas y el estado (que se maneja por botones)
                string[] columnasOcultas = { "IdUsuario", "IdSucursal", "Habilitado" };
                foreach (var col in columnasOcultas)
                {
                    if (dgvUsuarios.Columns.Contains(col)) dgvUsuarios.Columns[col].Visible = false;
                }

                if (dgvUsuarios.Columns.Contains("SucursalAsignada"))
                {
                    dgvUsuarios.Columns["SucursalAsignada"].HeaderText = "Sucursal".Traducir();
                    dgvUsuarios.Columns["SucursalAsignada"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }

                if (dgvUsuarios.Columns.Contains("Email"))
                {
                    dgvUsuarios.Columns["Email"].HeaderText = "Email".Traducir();
                    dgvUsuarios.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                if (dgvUsuarios.Columns.Contains("Nombre")) dgvUsuarios.Columns["Nombre"].HeaderText = "Nombre".Traducir();
            }
        }

        /// <summary>
        /// Recopila los datos ingresados en el formulario y solicita el registro de un nuevo usuario.
        /// </summary>
        private void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposBasicos() || string.IsNullOrWhiteSpace(txtbContraseña.Text))
            {
                MessageBox.Show("Nombre, Email y Contraseña son obligatorios para un nuevo usuario.".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();

                Guid? sucursalSeleccionada = null;

                if (cmbSucursales.SelectedValue != null && Guid.TryParse(cmbSucursales.SelectedValue.ToString(), out Guid idParseado))
                {
                    if (idParseado == Guid.Empty)
                    {
                        MessageBox.Show("Debe seleccionar una sucursal o el rol de Administrador.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    sucursalSeleccionada = (cmbSucursales.Text.Contains("ADMINISTRADOR")) ? null : idParseado;
                }
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
        /// Recopila las modificaciones realizadas a un usuario y las actualiza en la base de datos (No toca la contraseña ni el estado).
        /// </summary>
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (_idUsuarioSeleccionado == null || _idUsuarioSeleccionado == Guid.Empty) return;

            if (!ValidarCamposBasicos())
            {
                MessageBox.Show("Nombre y Email son obligatorios.".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();

                Guid? sucursalSeleccionada = null;
                string valorCombo = cmbSucursales.SelectedValue?.ToString() ?? "";

                if (!string.IsNullOrEmpty(valorCombo) && Guid.TryParse(valorCombo, out Guid idParseado))
                {
                    if (idParseado == Guid.Empty)
                    {
                        MessageBox.Show("Debe seleccionar una sucursal o el rol de Administrador.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    sucursalSeleccionada = (cmbSucursales.Text.Contains("ADMINISTRADOR")) ? null : idParseado;
                }

                MessageBox.Show("Datos del usuario actualizados con éxito.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (_idUsuarioSeleccionado == null || _idUsuarioSeleccionado == Guid.Empty) return;

                string nuevaClave = PedirNuevaContraseñaCartel();

                if (string.IsNullOrWhiteSpace(nuevaClave)) return;

                DialogResult respuesta = MessageBox.Show("¿Está seguro que desea sobrescribir la contraseña del usuario seleccionado?".Traducir(), "Confirmar Cambio".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();
                    usuarioBll.ModificarContraseña(_idUsuarioSeleccionado.Value, nuevaClave);

                    MessageBox.Show("Contraseña actualizada con éxito.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrillaUsuarios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Detecta la selección de una fila, carga los datos en los TextBoxes y actualiza el estado de los botones.
        /// </summary>
        private void dgvUsuarios_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow != null && dgvUsuarios.CurrentRow.Selected && dgvUsuarios.DataSource != null)
            {
                _idUsuarioSeleccionado = Guid.Parse(dgvUsuarios.CurrentRow.Cells["IdUsuario"].Value.ToString()!);

                txtbNombreUsuario.Text = dgvUsuarios.CurrentRow.Cells["Nombre"].Value?.ToString() ?? "";
                txtbEmail.Text = dgvUsuarios.CurrentRow.Cells["Email"].Value?.ToString() ?? "";
                txtbContraseña.Clear();
                txtbContraseña.Enabled = false;

                if (dgvUsuarios.CurrentRow.Cells["IdSucursal"].Value != null && dgvUsuarios.CurrentRow.Cells["IdSucursal"].Value != DBNull.Value)
                {
                    cmbSucursales.SelectedValue = Guid.Parse(dgvUsuarios.CurrentRow.Cells["IdSucursal"].Value.ToString()!);
                }
                else
                {
                    cmbSucursales.SelectedValue = Guid.Empty;
                }

                btnAgregarUsuario.Enabled = false;
                btnActualizar.Enabled = true;
                btnModificarContra.Enabled = true;
                btnDeshabilitar.Enabled = _viendoActivos;
                btnHabilitar.Enabled = !_viendoActivos;
            }
        }

        /// <summary>
        /// Solicita confirmación y deshabilita lógicamente al usuario seleccionado.
        /// </summary>
        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (_idUsuarioSeleccionado == null || _idUsuarioSeleccionado == Guid.Empty) return;

            if (MessageBox.Show(string.Format("¿Desea deshabilitar al usuario {0}?".Traducir(), txtbNombreUsuario.Text), "Confirmar".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();
                    usuarioBll.DeshabilitarUsuario(_idUsuarioSeleccionado.Value);

                    MessageBox.Show("Usuario deshabilitado correctamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrillaUsuarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al deshabilitar: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Solicita confirmación y rehabilita a un usuario inactivo.
        /// </summary>
        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            if (_idUsuarioSeleccionado == null || _idUsuarioSeleccionado == Guid.Empty) return;

            if (MessageBox.Show(string.Format("¿Desea reactivar al usuario {0}?".Traducir(), txtbNombreUsuario.Text), "Confirmar".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();
                    usuarioBll.HabilitarUsuario(_idUsuarioSeleccionado.Value);

                    MessageBox.Show("Usuario rehabilitado correctamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarGrillaUsuarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al habilitar: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Alterna la vista de la grilla entre usuarios Activos e Inactivos.
        /// </summary>
        private void btnVerDeshabilitados_Click(object sender, EventArgs e)
        {
            _viendoActivos = !_viendoActivos;

            btnVerDeshabilitados.Text = _viendoActivos ? "Ver Deshabilitados".Traducir() : "Ver Activos".Traducir();
            btnHabilitar.Visible = !_viendoActivos;
            btnDeshabilitar.Visible = _viendoActivos;

            CargarGrillaUsuarios();
        }

        /// <summary>
        /// Restablece el formulario a su estado de preparación de alta nueva.
        /// </summary>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
        }

        /// <summary>
        /// Intercepta el momento en que la grilla termina de cargar sus datos para forzar la deselección automática de Windows Forms.
        /// </summary>
        private void dgvUsuarios_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvUsuarios.ClearSelection();
            LimpiarControles();
        }

        /// <summary>
        /// Restablece los campos de texto y devuelve los botones al estado de Alta.
        /// </summary>
        private void LimpiarControles()
        {
            _idUsuarioSeleccionado = null;
            txtbNombreUsuario.Clear();
            txtbEmail.Clear();
            txtbContraseña.Clear();
            txtbContraseña.Enabled = true;

            if (cmbSucursales.Items.Count > 0) cmbSucursales.SelectedIndex = 0;

            if (dgvUsuarios.DataSource != null) dgvUsuarios.ClearSelection();

            btnAgregarUsuario.Enabled = _viendoActivos;
            btnActualizar.Enabled = false;
            btnModificarContra.Enabled = false;
            btnDeshabilitar.Enabled = false;
            btnHabilitar.Enabled = false;

            txtbNombreUsuario.Focus();
        }

        /// <summary>
        /// Valida los campos de texto.
        /// </summary>
        private bool ValidarCamposBasicos()
        {
            return !string.IsNullOrWhiteSpace(txtbNombreUsuario.Text) &&
                   !string.IsNullOrWhiteSpace(txtbEmail.Text);
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
}