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
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void EvaluarPermisosMenu(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    // 1. Leemos cómo se llama este botón en el diseñador visual (Ej: "MenuGestiónUsuario")
                    string nombrePermiso = menuItem.Name ?? string.Empty;

                    // 2. POR DEFECTO ESTÁ PROHIBIDO (false)
                    bool tienePermiso = false;

                    // Solo le damos permiso si el SessionManager confirma que lo tiene
                    if (SessionManager.Current.TienePermiso(nombrePermiso))
                    {
                        tienePermiso = true;
                    }

                    // Aplicamos la visibilidad al botón
                    menuItem.Visible = tienePermiso;

                    // 3. RECURSIVIDAD: Revisamos los sub-menús
                    if (menuItem.HasDropDownItems)
                    {
                        // Evaluamos a los hijos primero
                        EvaluarPermisosMenu(menuItem.DropDownItems);

                        // LÓGICA DE PADRES: Si el menú principal (Ej: "Archivo") no tiene un permiso propio,
                        // pero adentro tiene un hijo visible (Ej: "Ventas"), entonces mostramos al padre.
                        bool algunHijoVisible = false;
                        foreach (ToolStripItem sub in menuItem.DropDownItems)
                        {
                            if (sub.Visible)
                            {
                                algunHijoVisible = true;
                                break;
                            }
                        }

                        // Si algún hijo es visible, el padre DEBE ser visible para poder llegar al hijo
                        if (algunHijoVisible)
                        {
                            menuItem.Visible = true;
                        }
                    }
                }
            }
        }

        private void MenuPatente_Click(object sender, EventArgs e)
        {
            if (sender is not ToolStripMenuItem itemClickeado) return;

            // Para abrir el form, seguimos usando el TAG
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
                    MessageBox.Show($"No se encontró la clase '{nombreForm}'.".Traducir(), "Error".Traducir());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir: {ex.Message}".Traducir());
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }
        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            CargarComboIdiomas();
            var usuario = SessionManager.Current.UsuarioLogueado;

            if (usuario != null)
            {
                // 1. Damos la bienvenida
                this.Text = $"PetShop - Bienvenido {usuario.Nombre}".Traducir();

                // --- INICIO: CARGAR DATOS DE LA SUCURSAL ---
                if (SessionManager.Current.IdSucursalActual != null)
                {
                    try
                    {
                        // Instanciamos tu BLL de sucursales (Ajusta el nombre si se llama distinto)
                        Logic.SucursalLogic sucursalBll = new Logic.SucursalLogic();

                        // Buscamos el objeto completo en la base de datos
                        var sucursalActual = sucursalBll.GetById(SessionManager.Current.IdSucursalActual.Value);

                        if (sucursalActual != null)
                        {
                            // Mostramos el nombre y la dirección
                            lblInfoSucursal.Text = $"📍 Sucursal: {sucursalActual.NombreSucursal} | 🏠 Dir: {sucursalActual.Direccion}".Traducir();
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
                    mensaje: $"El usuario {usuario.Nombre} ingresó al sistema.".Traducir(),
                    criticidad: Services.DomainModel.Logging.Criticidad.Info,
                    idUsuario: usuario.IdUsuario);
            }
            TraductorUI.TraducirFormulario(this);
        }

        private Form? formularioActivo = null;
        private void AbrirFormularioCentrado(Form formularioHijo)
        {
            // 1. Si ya hay una pantalla abierta, la cerramos para no encimar cosas
            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }

            formularioActivo = formularioHijo;

            // 2. Le quitamos el comportamiento de "ventana independiente"
            formularioHijo.TopLevel = false;

            // 3. Le quitamos los bordes de Windows (la X, maximizar, minimizar)
            formularioHijo.FormBorderStyle = FormBorderStyle.None;

            // 4. Lo agregamos al panel
            panelContenedor.Controls.Add(formularioHijo);

            // 5. ¡LA MAGIA DEL CENTRADO!
            // Calculamos el centro exacto del panel y ahí ponemos el formulario
            formularioHijo.Location = new Point(
                (panelContenedor.Width - formularioHijo.Width) / 2,
                (panelContenedor.Height - formularioHijo.Height) / 2
            );

            // Esto asegura que si maximizas el FormPrincipal, el form hijo se quede en el medio
            formularioHijo.Anchor = AnchorStyles.None;

            // 6. Lo mostramos al frente
            formularioHijo.BringToFront();
            formularioHijo.Show();
        }

        private void CargarComboIdiomas()
        {
            try
            {
                // 1. Obtenemos la lista de culturas desde los archivos físicos
                var idiomas = Services.Bll.IdiomaService.ObtenerIdiomas();

                cmbIdioma.DataSource = idiomas;
                cmbIdioma.DisplayMember = "DisplayName"; // Muestra "español (Argentina)"
                cmbIdioma.ValueMember = "Name";          // El valor interno será "es-AR"

                // 2. Seleccionamos el idioma del usuario, protegiéndonos de los nulos
                if (SessionManager.Current.UsuarioLogueado != null)
                {
                    string idiomaGuardado = SessionManager.Current.UsuarioLogueado.IdiomaPredeterminado;

                    // Verificamos que no sea nulo ni esté vacío
                    if (!string.IsNullOrEmpty(idiomaGuardado))
                    {
                        cmbIdioma.SelectedValue = idiomaGuardado;
                    }
                    else
                    {
                        // Si está vacío (usuario nuevo o sin configurar), forzamos español en el combo
                        cmbIdioma.SelectedValue = "es-AR";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar idiomas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Registramos en la bitácora que el usuario cerró sesión
            var usuario = SessionManager.Current.UsuarioLogueado;
            if (usuario != null)
            {
                Services.Bll.BitácoraBll bitacora = new Services.Bll.BitácoraBll();
                bitacora.RegistrarLog(
                    mensaje: $"El usuario {usuario.Nombre} cerró sesión manualmente.".Traducir(),
                    criticidad: Services.DomainModel.Logging.Criticidad.Info,
                    idUsuario: usuario.IdUsuario
                );
            }

            SessionManager.Current.Logout();
            Application.Restart();
        }

        private void cmbIdioma_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // Si por algún motivo se dispara en vacío, cortamos acá
            if (cmbIdioma.SelectedValue == null) return;

            string idiomaSeleccionado = cmbIdioma.SelectedValue?.ToString() ?? string.Empty;
            var usuarioActual = SessionManager.Current.UsuarioLogueado;

            // Solo hacemos el viaje a la BD si realmente eligió un idioma distinto al que ya tiene
            if (usuarioActual != null && usuarioActual.IdiomaPredeterminado != idiomaSeleccionado)
            {
                try
                {
                    // 1. Instanciamos la BLL y guardamos en la Base de Datos
                    Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();

                    // Pasamos el ID del usuario logueado y la nueva cultura ("es-AR" o "en-US")
                    usuarioBll.ActualizarIdiomaPredeterminado(usuarioActual.IdUsuario, idiomaSeleccionado);

                    // 2. Actualizamos la memoria de la sesión actual para que no quede desfasada
                    usuarioActual.IdiomaPredeterminado = idiomaSeleccionado;

                    // 3. Le avisamos al usuario que el cambio fue exitoso
                    MessageBox.Show(
                        "El idioma predeterminado ha sido actualizado. Por favor, reinicie sesión para aplicar los cambios en todo el sistema.".Traducir(),
                        "Configuración de Idioma".Traducir(),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar la preferencia en la base de datos: {ex.Message}".Traducir(), "Error de Sistema".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
