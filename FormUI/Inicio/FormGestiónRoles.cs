using Services.Facade.Extensions;

namespace FormUI.Inicio
{
    public partial class FormGestiónRoles : Form
    {
        /// <summary>
        /// Inicializa el formulario y sus componentes visuales predeterminados.
        /// </summary>
        public FormGestiónRoles()
        {
            InitializeComponent();
            clbPermisos.FormattingEnabled = true;
            clbPermisos.Format += ClbPermisos_Format;
        }

        /// <summary>
        /// Evento de carga inicial que obtiene las listas de usuarios y roles (Familias) para poblar los controles.
        /// </summary>
        private void FormGestiónRoles_Load(object sender, EventArgs e)
        {
            try
            {
                Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();
                cmbUsuarios.DataSource = usuarioBll.ListarTodos().ToList();
                cmbUsuarios.DisplayMember = "Nombre";
                cmbUsuarios.ValueMember = "IdUsuario";

                Services.Bll.PermisosBll permisosBll = new Services.Bll.PermisosBll();

                clbRoles.DataSource = permisosBll.GetAllFamilias().ToList();
                clbRoles.DisplayMember = "Nombre";
                clbRoles.ValueMember = "Id";

                cmbUsuarios.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar los catálogos: {0}".Traducir(), ex.Message));
            }
            TraductorUI.TraducirFormulario(this);
        }

        /// <summary>
        /// Detecta la selección de un usuario, limpia los roles previos y tilda automáticamente los roles asignados a él en la base de datos.
        /// </summary>
        private void cmbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsuarios.SelectedItem == null || cmbUsuarios.SelectedIndex == -1)
            {
                for (int i = 0; i < clbRoles.Items.Count; i++) clbRoles.SetItemChecked(i, false);
                clbPermisos.DataSource = null;
                clbPermisos.Items.Clear();
                return;
            }

            try
            {
                if (cmbUsuarios.SelectedItem is Services.DomainModel.Composite.Usuario usuarioBasico)
                {
                    for (int i = 0; i < clbRoles.Items.Count; i++) clbRoles.SetItemChecked(i, false);
                    clbPermisos.DataSource = null;
                    clbPermisos.Items.Clear();

                    Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();
                    var usuarioCompleto = usuarioBll.GetById(usuarioBasico.IdUsuario);

                    if (usuarioCompleto.Privilegios == null) return;
                    List<Services.DomainModel.Composite.Component> patentesDelRol = new List<Services.DomainModel.Composite.Component>();

                    foreach (var permiso in usuarioCompleto.Privilegios)
                    {
                        if (permiso is Services.DomainModel.Composite.Familia familia)
                        {
                            MarcarItemEnLista(clbRoles, familia.Id);
                            ObtenerPatentesRecursivo(familia, patentesDelRol);
                        }
                        else if (permiso is Services.DomainModel.Composite.Patente patente)
                        {
                            if (!patentesDelRol.Any(p => p.Id == patente.Id))
                                patentesDelRol.Add(patente);
                        }
                    }
                    clbPermisos.DataSource = patentesDelRol;
                    clbPermisos.DisplayMember = "Nombre";
                    clbPermisos.ValueMember = "Id";

                    for (int i = 0; i < clbPermisos.Items.Count; i++)
                    {
                        clbPermisos.SetItemChecked(i, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar los permisos del usuario: {0}".Traducir(), ex.Message));
            }
        }

        /// <summary>
        /// Método de asistencia que recorre el CheckedListBox buscando un ID específico para marcar su casilla correspondiente.
        /// </summary>
        private void MarcarItemEnLista(CheckedListBox lista, Guid idPermisoBuscado)
        {
            for (int i = 0; i < lista.Items.Count; i++)
            {
                var item = (Services.DomainModel.Composite.Component)lista.Items[i];
                if (item.Id == idPermisoBuscado)
                {
                    lista.SetItemChecked(i, true);
                    break;
                }
            }
        }

        /// <summary>
        /// Recolecta todos los roles actualmente tildados en la interfaz y ejecuta la actualización para el usuario seleccionado.
        /// </summary>
        private void btnGuardarRol_Click(object sender, EventArgs e)
        {
            if (cmbUsuarios.SelectedItem == null || cmbUsuarios.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un usuario de la lista superior primero.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var usuarioSeleccionado = (Services.DomainModel.Composite.Usuario)cmbUsuarios.SelectedItem;

                List<Guid> familiasTildadas = new List<Guid>();
                foreach (var item in clbRoles.CheckedItems)
                {
                    var familia = (Services.DomainModel.Composite.Familia)item;
                    familiasTildadas.Add(familia.Id);
                }

                Services.Bll.PermisosBll permisosBll = new Services.Bll.PermisosBll();
                permisosBll.GuardarPermisosUsuario(usuarioSeleccionado.IdUsuario, familiasTildadas, new List<Guid>());

                MessageBox.Show("¡Los permisos se han guardado exitosamente!".Traducir(), "Operación Exitosa".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al guardar los permisos: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Método recursivo para recorrer el árbol del Patrón Composite y extraer todas las patentes dentro de una Familia.
        /// </summary>
        private void ObtenerPatentesRecursivo(Services.DomainModel.Composite.Component componente, List<Services.DomainModel.Composite.Component> listaResultado)
        {
            if (componente is Services.DomainModel.Composite.Patente)
            {
                if (!listaResultado.Any(p => p.Id == componente.Id))
                    listaResultado.Add(componente);
            }
            else if (componente is Services.DomainModel.Composite.Familia familia)
            {
                if (familia.Hijos != null)
                {
                    foreach (var hijo in familia.Hijos)
                    {
                        ObtenerPatentesRecursivo(hijo, listaResultado);
                    }
                }
            }
        }

        /// <summary>
        /// Intercepta la visualización del CheckedListBox para transformar nombres técnicos en textos formales.
        /// </summary>
        private void ClbPermisos_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is Services.DomainModel.Composite.Component permiso)
            {
                string nombreTecnico = permiso.Nombre;

                switch (nombreTecnico)
                {
                    case "tsmCompras":
                        e.Value = "Módulo de Compras";
                        break;
                    case "FormGestiónOP":
                        e.Value = "Gestionar Orden de Pedido";
                        break;
                    case "FormGestiónProducto":
                        e.Value = "Gestionar Productos";
                        break;
                    case "FormGestiónOC":
                        e.Value = "Gestionar Orden de Compra";
                        break;
                    case "FormGestiónProveedor":
                        e.Value = "Gestionar Proveedores";
                        break;
                    case "FormGestiónSP":
                        e.Value = "Gestionar Solicitud de Pedido";
                        break;
                    case "FormGestiónCliente":
                        e.Value = "Gestionar Clientes";
                        break;
                    case "FormGestiónSucursal":
                        e.Value = "Gestionar Sucursales";
                        break;
                    case "FormGestiónVenta":
                        e.Value = "Gestionar Ventas";
                        break;
                    case "FormSeleccionSucursal":
                        e.Value = "Selección de Sucursal";
                        break;
                    case "FormGenerarVenta":
                        e.Value = "Generar una Venta";
                        break;
                    case "FormListaPrecios":
                        e.Value = "Ver Lista de Precios";
                        break;
                    case "FormHistorialVentas":
                        e.Value = "Ver Historial de Ventas";
                        break;
                    case "FormSolicitarTraspasoProductoSucursales":
                        e.Value = "Solicitar un Traspaso de Producto a Sucursal";
                        break;
                    case "FormHistorialMovimientos":
                        e.Value = "Ver Historial de Movimientos";
                        break;
                    case "FormVerStockDisponible":
                        e.Value = "Ver Stock Disponible";
                        break;
                    case "FormSolicitarOP":
                        e.Value = "Solicitar Orden de Pedido";
                        break;
                    case "FormAgregarStock":
                        e.Value = "Agregar Stock";
                        break;
                    case "FormTraspasoProcutoSucursal":
                        e.Value = "Traspaso de Procucto a Sucursal";
                        break;
                    case "Anular_Ventas":
                        e.Value = "Anular una Venta";
                        break;
                    default:
                        // Si se agrega un form nuevo en el futuro y olvidas ponerlo aquí, 
                        // esto lo limpia de forma genérica quitándole la palabra "Form" o "tsm"
                        e.Value = nombreTecnico.Replace("FormGestión", "Gestionar ")
                                               .Replace("Form", "")
                                               .Replace("tsm", "Módulo ");
                        break;
                }
            }
        }
    }
}
