using Services.DomainModel.Composite;
using Services.Bll;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services.Facade.Extensions;

namespace FormUI.Inicio
{
    public partial class FormPrincipal : Form
    {
        private Form? formularioActivo = null;

        /// <summary>
        /// Inicializa el formulario principal y sus componentes visuales.
        /// </summary>
        public FormPrincipal()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Recorre recursivamente los elementos del menú y evalúa los permisos del usuario activo para determinar la visibilidad de cada opción.
        /// </summary>
        private void EvaluarPermisosMenu(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    string nombrePermiso = menuItem.Name ?? string.Empty;
                    bool tienePermiso = false;

                    if (SessionManager.Current.TienePermiso(nombrePermiso))
                    {
                        tienePermiso = true;
                    }

                    menuItem.Visible = tienePermiso;

                    if (menuItem.HasDropDownItems)
                    {
                        EvaluarPermisosMenu(menuItem.DropDownItems);

                        bool algunHijoVisible = false;
                        foreach (ToolStripItem sub in menuItem.DropDownItems)
                        {
                            if (sub.Visible)
                            {
                                algunHijoVisible = true;
                                break;
                            }
                        }

                        if (algunHijoVisible)
                        {
                            menuItem.Visible = true;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Captura el clic en las opciones del menú, obtiene el nombre del formulario desde el Tag y lo instancia dinámicamente mediante reflexión.
        /// </summary>
        private void MenuPatente_Click(object sender, EventArgs e)
        {
            if (sender is not ToolStripMenuItem itemClickeado) return;

            string nombreForm = itemClickeado.Tag?.ToString() ?? "";

            if (string.IsNullOrEmpty(nombreForm))
            {
                MessageBox.Show("Falta el Tag en el diseñador para saber qué pantalla abrir.".Traducir(), "Aviso".Traducir());
                return;
            }

            try
            {
                var tipoForm = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => t.Name == nombreForm && typeof(Form).IsAssignableFrom(t));

                if (tipoForm != null)
                {
                    Form formulario = (Form)Activator.CreateInstance(tipoForm)!;
                    AbrirFormularioCentrado(formulario);
                }
                else
                {
                    MessageBox.Show(string.Format("No se encontró la clase '{0}'.".Traducir(), nombreForm), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al abrir: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Evento de carga inicial que configura el entorno, valida accesos, registra el inicio de sesión en la bitácora y traduce la interfaz principal.
        /// </summary>
        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            CargarComboIdiomas();
            var usuario = SessionManager.Current.UsuarioLogueado;

            if (usuario != null)
            {
                this.Text = string.Format("PetShop - Bienvenido {0}".Traducir(), usuario.Nombre);

                if (SessionManager.Current.IdSucursalActual != null)
                {
                    try
                    {
                        Logic.SucursalLogic sucursalBll = new Logic.SucursalLogic();
                        var sucursalActual = sucursalBll.GetById(SessionManager.Current.IdSucursalActual.Value);

                        if (sucursalActual != null)
                        {
                            lblInfoSucursal.Text = string.Format("📍 Sucursal: {0} | 🏠 Dir: {1}".Traducir(), sucursalActual.NombreSucursal, sucursalActual.Direccion);
                        }
                        else
                        {
                            lblInfoSucursal.Text = "📍 Sucursal: No encontrada".Traducir();
                        }
                    }
                    catch (Exception)
                    {
                        lblInfoSucursal.Text = "📍 Error al cargar sucursal".Traducir();
                    }
                }

                EvaluarPermisosMenu(menuStrip.Items);

                Services.Bll.BitácoraBll bitacora = new Services.Bll.BitácoraBll();
                bitacora.RegistrarLog(
                    mensaje: string.Format("El usuario {0} ingresó al sistema.".Traducir(), usuario.Nombre),
                    criticidad: Services.DomainModel.Logging.Criticidad.Info,
                    idUsuario: usuario.IdUsuario);
            }
            TraductorUI.TraducirFormulario(this);
        }
        /// <summary>
        /// Cierra cualquier ventana hija activa y carga un nuevo formulario embebido, asegurando su centrado absoluto dentro del panel contenedor.
        /// </summary>
        private void AbrirFormularioCentrado(Form formularioHijo)
        {
            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }

            formularioActivo = formularioHijo;

            formularioHijo.TopLevel = false;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;
            panelContenedor.Controls.Add(formularioHijo);

            formularioHijo.Location = new Point(
                (panelContenedor.Width - formularioHijo.Width) / 2,
                (panelContenedor.Height - formularioHijo.Height) / 2
            );

            formularioHijo.Anchor = AnchorStyles.None;
            formularioHijo.BringToFront();
            formularioHijo.Show();
        }
        /// <summary>
        /// Consulta los idiomas instalados en el sistema y selecciona en el menú desplegable el idioma preferido del usuario activo.
        /// </summary>
        private void CargarComboIdiomas()
        {
            try
            {
                var idiomas = Services.Bll.IdiomaService.ObtenerIdiomas();

                cmbIdioma.DataSource = idiomas;
                cmbIdioma.DisplayMember = "DisplayName";
                cmbIdioma.ValueMember = "Name";

                if (SessionManager.Current.UsuarioLogueado != null)
                {
                    string idiomaGuardado = SessionManager.Current.UsuarioLogueado.IdiomaPredeterminado;

                    if (!string.IsNullOrEmpty(idiomaGuardado))
                    {
                        cmbIdioma.SelectedValue = idiomaGuardado;
                    }
                    else
                    {
                        cmbIdioma.SelectedValue = "es-AR";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar idiomas: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Registra la salida en la bitácora de auditoría, destruye la sesión activa y reinicia la aplicación para volver a la pantalla de login.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            var usuario = SessionManager.Current.UsuarioLogueado;
            if (usuario != null)
            {
                Services.Bll.BitácoraBll bitacora = new Services.Bll.BitácoraBll();
                bitacora.RegistrarLog(
                    mensaje: string.Format("El usuario {0} cerró sesión manualmente.".Traducir(), usuario.Nombre),
                    criticidad: Services.DomainModel.Logging.Criticidad.Info,
                    idUsuario: usuario.IdUsuario
                );
            }

            SessionManager.Current.Logout();
            Application.Restart();
        }
        /// <summary>
        /// Detecta el cambio manual de idioma, persiste la nueva preferencia en la base de datos y solicita al usuario reiniciar la sesión.
        /// </summary>
        private void cmbIdioma_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbIdioma.SelectedValue == null) return;

            string idiomaSeleccionado = cmbIdioma.SelectedValue?.ToString() ?? string.Empty;
            var usuarioActual = SessionManager.Current.UsuarioLogueado;

            if (usuarioActual != null && usuarioActual.IdiomaPredeterminado != idiomaSeleccionado)
            {
                try
                {
                    Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();
                    usuarioBll.ActualizarIdiomaPredeterminado(usuarioActual.IdUsuario, idiomaSeleccionado);

                    usuarioActual.IdiomaPredeterminado = idiomaSeleccionado;

                    MessageBox.Show(
                        "El idioma predeterminado ha sido actualizado. Por favor, reinicie sesión para aplicar los cambios en todo el sistema.".Traducir(),
                        "Configuración de Idioma".Traducir(),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al guardar la preferencia en la base de datos: {0}".Traducir(), ex.Message), "Error de Sistema".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
