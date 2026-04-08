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
                // 1. CARGAR USUARIOS EN EL COMBOBOX
                Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();
                var listaUsuarios = usuarioBll.ListarTodos(); // Asegúrate de usar tu método real

                cmbUsuarios.DataSource = listaUsuarios;
                cmbUsuarios.DisplayMember = "Nombre";    // Lo que ve el admin
                cmbUsuarios.ValueMember = "IdUsuario";   // El ID oculto
                cmbUsuarios.SelectedIndex = -1;          // Que arranque vacío


                // 2. CARGAR FAMILIAS (ROLES) EN LA PRIMERA GRILLA
                Services.Bll.FamiliaBll familiaBll = new Services.Bll.FamiliaBll();
                var listaFamilias = familiaBll.ObtenerTodas();

                dgvRoles.DataSource = listaFamilias;

                // Emprolijamos la grilla de roles (ocultamos IDs y cosas raras)
                if (dgvRoles.Columns.Count > 0)
                {
                    if (dgvRoles.Columns["Id"] != null) dgvRoles.Columns["Id"].Visible = false;
                    if (dgvRoles.Columns["Hijos"] != null) dgvRoles.Columns["Hijos"].Visible = false;

                    // Ajusta el nombre de la columna según cómo se llame en tu clase Familia
                    if (dgvRoles.Columns["Nombre"] != null) dgvRoles.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                if (!dgvRoles.Columns.Contains("Asignado"))
                {
                    DataGridViewCheckBoxColumn colCheckRoles = new DataGridViewCheckBoxColumn();
                    colCheckRoles.Name = "Asignado";
                    colCheckRoles.HeaderText = "Asignado";
                    colCheckRoles.Width = 60;
                    dgvRoles.Columns.Insert(0, colCheckRoles); // La ponemos primera
                }

                // 3. CARGAR PATENTES (PERMISOS) EN LA SEGUNDA GRILLA
                Services.Bll.PatenteBll patenteBll = new Services.Bll.PatenteBll();
                var listaPatentes = patenteBll.ObtenerTodas();

                dgvPermisos.DataSource = listaPatentes;

                // Emprolijamos la grilla de patentes
                if (dgvPermisos.Columns.Count > 0)
                {
                    if (dgvPermisos.Columns["Id"] != null) dgvPermisos.Columns["Id"].Visible = false;
                    if (dgvPermisos.Columns["Hijos"] != null) dgvPermisos.Columns["Hijos"].Visible = false;

                    if (dgvPermisos.Columns["DataKey"] != null) dgvPermisos.Columns["DataKey"].HeaderText = "Permiso";
                    if (dgvPermisos.Columns["TipoAcceso"] != null) dgvPermisos.Columns["TipoAcceso"].HeaderText = "Acceso";
                }

                if (!dgvPermisos.Columns.Contains("Asignado"))
                {
                    DataGridViewCheckBoxColumn colCheckPermisos = new DataGridViewCheckBoxColumn();
                    colCheckPermisos.Name = "Asignado";
                    colCheckPermisos.HeaderText = "Asignado";
                    colCheckPermisos.Width = 60;
                    dgvPermisos.Columns.Insert(0, colCheckPermisos); // La ponemos primera
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar el formulario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // 1. Validamos que haya un usuario real seleccionado
                if (cmbUsuarios.SelectedIndex == -1 || cmbUsuarios.SelectedValue == null) return;

                // Limpiamos los tildes anteriores por si estábamos viendo a otro usuario
                foreach (DataGridViewRow row in dgvRoles.Rows) row.Cells["Asignado"].Value = false;
                foreach (DataGridViewRow row in dgvPermisos.Rows) row.Cells["Asignado"].Value = false;

                // 2. Intentamos leer el ID del usuario seleccionado
                Guid idUsuarioSeleccionado;
                if (Guid.TryParse(cmbUsuarios.SelectedValue.ToString(), out idUsuarioSeleccionado))
                {
                    // 3. Traemos al usuario fresco desde la base de datos (con su lista de Privilegios ya cargada)
                    Services.Bll.UsuarioBll usuarioBll = new Services.Bll.UsuarioBll();

                    // Asumo que tienes un método como ObtenerPorId o GetById en tu BLL
                    var usuario = usuarioBll.GetById(idUsuarioSeleccionado);

                    if (usuario != null && usuario.Privilegios != null)
                    {
                        // 4. Recorremos la grilla de ROLES (Familias)
                        foreach (DataGridViewRow row in dgvRoles.Rows)
                        {
                            // Asumo que la columna ID oculta se llama "Id"
                            Guid idRolGrilla = (Guid)row.Cells["Id"].Value;

                            // Si el ID de la grilla está en la lista de privilegios del usuario, ¡lo tildamos!
                            if (usuario.Privilegios.Any(p => p.Id == idRolGrilla))
                            {
                                row.Cells["Asignado"].Value = true;
                            }
                        }

                        // 5. Recorremos la grilla de PERMISOS (Patentes)
                        foreach (DataGridViewRow row in dgvPermisos.Rows)
                        {
                            Guid idPermisoGrilla = (Guid)row.Cells["Id"].Value;

                            if (usuario.Privilegios.Any(p => p.Id == idPermisoGrilla))
                            {
                                row.Cells["Asignado"].Value = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lo ponemos silencioso por si falla al cargar la ventana por primera vez
                Console.WriteLine(ex.Message);
            }
        }
    }
}
