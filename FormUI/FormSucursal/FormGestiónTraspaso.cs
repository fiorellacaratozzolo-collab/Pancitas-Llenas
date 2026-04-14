using DataAccess.Implementations.SqlServer;
using Logic;
using Logic.MappingProfiles;
using ModelsDTO;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormUI.FormSucursal
{
    public partial class FormGestiónTraspaso : Form
    {
        private Logic.Facade.TraspasoService _traspasoService;


        public FormGestiónTraspaso()
        {
            InitializeComponent();
            _traspasoService = new Logic.Facade.TraspasoService();
        }

        private void FormGestionarTraspaso_Load(object sender, EventArgs e)
        {

        }
        private void CargarSolicitudesPendientes()
        {
            try
            {
                Guid miSucursal = SessionManager.Current.IdSucursalActual.Value;
                var pendientes = _traspasoService.ObtenerSolicitudesPendientes(miSucursal);
                dgvSolicitudesPendientes.DataSource = pendientes;
                // dgvSolicitudesPendientes.Columns["IdSucursalOrigen"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la lista: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void dgvSolicitudes_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void btnAprobar_Click(object sender, EventArgs e)
        {

        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {

        }

        private void FormGestiónTraspaso_Load(object sender, EventArgs e)
        {
            CargarSolicitudesPendientes();
        }

        private void dgvSolicitudesPendientes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSolicitudesPendientes.CurrentRow != null)
            {
                // Obtenemos la solicitud seleccionada
                var solicitudSeleccionada = (SolicitudDeTraspasoDeProductoDTO)dgvSolicitudesPendientes.CurrentRow.DataBoundItem;

                // Buscamos sus detalles
                var detalles = _traspasoService.ObtenerDetallesPorSolicitud(solicitudSeleccionada.IdSolicitudDeTraspasoDeProductos);

                dgvDetalle.DataSource = detalles;

                // Ocultar columnas innecesarias del detalle...
            }

        }

        private void btnAprobar_Click_1(object sender, EventArgs e)
        {
            if (dgvSolicitudesPendientes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una solicitud para aprobar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var solicitudSeleccionada = (SolicitudDeTraspasoDeProductoDTO)dgvSolicitudesPendientes.CurrentRow.DataBoundItem;

            var confirmacion = MessageBox.Show($"¿Está seguro que desea APROBAR esta solicitud?\nSe descontará el stock de su sucursal.", "Confirmar Aprobación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    _traspasoService.AprobarTraspaso(solicitudSeleccionada.IdSolicitudDeTraspasoDeProductos);

                    MessageBox.Show("¡Solicitud aprobada y stock actualizado con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Truco: Llamamos al botón actualizar para refrescar la grilla y que desaparezca la solicitud que ya no está "Pendiente"
                    btnActualizar.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRechazar_Click_1(object sender, EventArgs e)
        {
            if (dgvSolicitudesPendientes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una solicitud para rechazar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var solicitudSeleccionada = (SolicitudDeTraspasoDeProductoDTO)dgvSolicitudesPendientes.CurrentRow.DataBoundItem;

            var confirmacion = MessageBox.Show($"¿Está seguro que desea RECHAZAR esta solicitud?", "Confirmar Rechazo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    _traspasoService.RechazarTraspaso(solicitudSeleccionada.IdSolicitudDeTraspasoDeProductos);

                    MessageBox.Show("La solicitud ha sido rechazada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnActualizar.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarSolicitudesPendientes();
            dgvDetalle.DataSource = null;
        }
    }
}