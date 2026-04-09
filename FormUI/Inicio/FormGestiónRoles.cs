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
    public partial class FormGestiónRoles : Form
    {
        public FormGestiónRoles()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FormGestiónRoles_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Cargar el ComboBox de Usuarios
                Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();
                cmbUsuarios.DataSource = usuarioBll.ListarTodos(); // Ajusta el nombre de tu método
                cmbUsuarios.DisplayMember = "Nombre";
                cmbUsuarios.ValueMember = "IdUsuario";

                // 2. Instanciamos la BLL de Permisos (Ajusta al nombre real de tu clase)
                Services.Bll.PermisosBll permisosBll = new Services.Bll.PermisosBll();

                // 3. Cargar el CheckedListBox de Roles (Familias)
                // OJO: Cast explícito a IList para evitar bugs de DataBinding con el CheckedListBox
                clbRoles.DataSource = permisosBll.GetAllFamilias().ToList();
                clbRoles.DisplayMember = "Nombre";
                clbRoles.ValueMember = "Id"; // O como se llame la propiedad de ID de tu Componente

                // 4. Cargar el CheckedListBox de Permisos Sueltos (Patentes)
                clbPermisos.DataSource = permisosBll.GetAllPatentes().ToList();
                clbPermisos.DisplayMember = "Nombre"; // O "DataKey", el que sea más legible para el admin
                clbPermisos.ValueMember = "Id";

                // Dejamos el ComboBox sin selección inicial para que no dispare eventos por accidente
                cmbUsuarios.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los catálogos: " + ex.Message);
            }
        }

        private void cmbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Si no hay nada seleccionado, no hacemos nada
            if (cmbUsuarios.SelectedItem == null || cmbUsuarios.SelectedIndex == -1) return;

            try
            {
                // 1. Obtenemos el usuario básico del ComboBox
                if (cmbUsuarios.SelectedItem is Services.DomainModel.Composite.Usuario usuarioBasico)
                {
                    // 2. Limpiamos todos los tildes previos
                    for (int i = 0; i < clbRoles.Items.Count; i++) clbRoles.SetItemChecked(i, false);
                    for (int i = 0; i < clbPermisos.Items.Count; i++) clbPermisos.SetItemChecked(i, false);

                    // 3. ¡AQUÍ USAMOS TU MÉTODO! Traemos al usuario con la mochila llena
                    Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();
                    var usuarioCompleto = usuarioBll.GetById(usuarioBasico.IdUsuario);

                    // Si el usuario no tiene permisos, terminamos acá
                    if (usuarioCompleto.Privilegios == null) return;

                    // 4. Recorremos la mochila y tildamos lo que corresponda
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
                MessageBox.Show("Error al cargar los permisos del usuario: " + ex.Message);
            }
        }

        // Método auxiliar para buscar por ID en el CheckedListBox y poner el tilde
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


        private void btnGuardarRol_Click(object sender, EventArgs e)
        {
            // 1. Validamos que haya un usuario seleccionado
            if (cmbUsuarios.SelectedItem == null || cmbUsuarios.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un usuario de la lista superior primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Extraemos el usuario seleccionado
                var usuarioSeleccionado = (Services.DomainModel.Composite.Usuario)cmbUsuarios.SelectedItem;

                // 3. Recopilamos los IDs de todo lo que esté TILDADO en las Familias
                List<Guid> familiasTildadas = new List<Guid>();
                foreach (var item in clbRoles.CheckedItems)
                {
                    var familia = (Services.DomainModel.Composite.Familia)item;
                    familiasTildadas.Add(familia.Id);
                }

                // 4. Recopilamos los IDs de todo lo que esté TILDADO en las Patentes extra
                List<Guid> patentesTildadas = new List<Guid>();
                foreach (var item in clbPermisos.CheckedItems)
                {
                    var patente = (Services.DomainModel.Composite.Patente)item;
                    patentesTildadas.Add(patente.Id);
                }

                // 5. Mandamos todo a la BLL
                Services.Bll.PermisosBll permisosBll = new Services.Bll.PermisosBll();
                permisosBll.GuardarPermisosUsuario(usuarioSeleccionado.IdUsuario, familiasTildadas, patentesTildadas);

                MessageBox.Show("¡Los permisos se han guardado exitosamente!", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los permisos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
