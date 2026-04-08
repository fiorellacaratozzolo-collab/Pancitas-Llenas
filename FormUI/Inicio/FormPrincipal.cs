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

namespace FormUI.Inicio
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Recorre el menú estático de WinForms y oculta lo que el usuario no puede ver.
        /// </summary>
        /// 


        private void EvaluarPermisosMenu(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    // 1. Leemos cómo se llama este botón en el diseñador visual (Ej: "MenuGestiónUsuario")
                    string nombrePermiso = menuItem.Name;

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
                MessageBox.Show("Falta el Tag en el diseñador para saber qué pantalla abrir.", "Aviso");
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
                    MessageBox.Show($"No se encontró la clase '{nombreForm}'.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir: {ex.Message}");
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
            var usuario = SessionManager.Current.UsuarioLogueado;

            if (usuario != null)
            {
                // 1. Damos la bienvenida
                this.Text = $"PetShop - Bienvenido {usuario.Nombre}";

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
                            lblInfoSucursal.Text = $"📍 Sucursal: {sucursalActual.NombreSucursal} | 🏠 Dir: {sucursalActual.Direccion}";
                        }
                        else
                        {
                            lblInfoSucursal.Text = "📍 Sucursal: No encontrada";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblInfoSucursal.Text = "📍 Error al cargar sucursal";
                    }
                }
                // --- FIN: CARGAR DATOS DE LA SUCURSAL ---

                // 2. Disparamos la validación del menú de forma invisible
                EvaluarPermisosMenu(menuStrip.Items);

                // 3. Registramos en la bitácora
                Services.Bll.BitácoraBll bitacora = new Services.Bll.BitácoraBll();
                bitacora.RegistrarLog(
                    mensaje: $"El usuario {usuario.Nombre} ingresó al sistema.",
                    criticidad: Services.DomainModel.Logging.Criticidad.Info,
                    idUsuario: usuario.IdUsuario);
            }

        }

        private Form formularioActivo = null;
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



        private void compraToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void sucursalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Registramos en la bitácora que el usuario cerró sesión
            var usuario = SessionManager.Current.UsuarioLogueado;
            if (usuario != null)
            {
                Services.Bll.BitácoraBll bitacora = new Services.Bll.BitácoraBll();
                bitacora.RegistrarLog(
                    mensaje: $"El usuario {usuario.Nombre} cerró sesión manualmente.",
                    criticidad: Services.DomainModel.Logging.Criticidad.Info,
                    idUsuario: usuario.IdUsuario
                );
            }

            SessionManager.Current.Logout();
            Application.Restart();
        }
    }
}
