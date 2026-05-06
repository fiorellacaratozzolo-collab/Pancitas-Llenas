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
    public partial class FormGestiónRoles : Form
    {
        /// <summary>
        /// Inicializa el formulario y sus componentes visuales predeterminados.
        /// </summary>
        public FormGestiónRoles()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Evento de carga inicial que obtiene las listas de usuarios, roles (Familias) y permisos individuales (Patentes) para poblar los controles.
        /// Aplica un cast explícito a IList en los CheckedListBox para prevenir errores de DataBinding.
        /// </summary>
        private void FormGestiónRoles_Load(object sender, EventArgs e)
        {
            try
            {
                Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();
                cmbUsuarios.DataSource = usuarioBll.ListarTodos();
                cmbUsuarios.DisplayMember = "Nombre";
                cmbUsuarios.ValueMember = "IdUsuario";

                Services.Bll.PermisosBll permisosBll = new Services.Bll.PermisosBll();

                clbRoles.DataSource = permisosBll.GetAllFamilias().ToList();
                clbRoles.DisplayMember = "Nombre";
                clbRoles.ValueMember = "Id";

                clbPermisos.DataSource = permisosBll.GetAllPatentes().ToList();
                clbPermisos.DisplayMember = "Nombre";
                clbPermisos.ValueMember = "Id";

                cmbUsuarios.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar los catálogos: {0}".Traducir(), ex.Message));
            }
            TraductorUI.TraducirFormulario(this);
        }
        /// <summary>
        /// Detecta la selección de un usuario, limpia los permisos previos y tilda automáticamente los roles y patentes asignados a él en la base de datos.
        /// </summary>
        private void cmbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsuarios.SelectedItem == null || cmbUsuarios.SelectedIndex == -1) return;

            try
            {
                if (cmbUsuarios.SelectedItem is Services.DomainModel.Composite.Usuario usuarioBasico)
                {
                    for (int i = 0; i < clbRoles.Items.Count; i++) clbRoles.SetItemChecked(i, false);
                    for (int i = 0; i < clbPermisos.Items.Count; i++) clbPermisos.SetItemChecked(i, false);

                    Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();
                    var usuarioCompleto = usuarioBll.GetById(usuarioBasico.IdUsuario);

                    if (usuarioCompleto.Privilegios == null) return;

                    foreach (var permiso in usuarioCompleto.Privilegios)
                    {
                        if (permiso is Services.DomainModel.Composite.Familia familia)
                        {
                            MarcarItemEnLista(clbRoles, familia.Id);
                        }
                        else if (permiso is Services.DomainModel.Composite.Patente patente)
                        {
                            MarcarItemEnLista(clbPermisos, patente.Id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar los permisos del usuario: {0}".Traducir(), ex.Message));
            }
        }
        /// <summary>
        /// Método de asistencia que recorre un CheckedListBox buscando un ID específico para marcar su casilla correspondiente.
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
        /// Recolecta todos los permisos y roles actualmente tildados en la interfaz y ejecuta la actualización para el usuario seleccionado.
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

                List<Guid> patentesTildadas = new List<Guid>();
                foreach (var item in clbPermisos.CheckedItems)
                {
                    var patente = (Services.DomainModel.Composite.Patente)item;
                    patentesTildadas.Add(patente.Id);
                }

                Services.Bll.PermisosBll permisosBll = new Services.Bll.PermisosBll();
                permisosBll.GuardarPermisosUsuario(usuarioSeleccionado.IdUsuario, familiasTildadas, patentesTildadas);

                MessageBox.Show("¡Los permisos se han guardado exitosamente!".Traducir(), "Operación Exitosa".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al guardar los permisos: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
