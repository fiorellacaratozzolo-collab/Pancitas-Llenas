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
        private void CargarSolicitudes()
        {
            try
            {
                // 1. Limpiamos la grilla de detalle inferior para que no quede info vieja
                dgvDetalle.DataSource = null;

                Guid miSucursal = SessionManager.Current.IdSucursalActual.Value;

                // 2. Llamamos al NUEVO método de la Logic que trae TODAS (sin filtrar por estado)
                var todas = _traspasoService.ObtenerTodasPorSucursal(miSucursal);
                List<SolicitudDeTraspasoDeProductoDTO> filtradas;

                // 3. Aplicamos el filtro mágico del ComboBox
                if (cmbFiltroEstado.SelectedIndex == 0) // Pendientes
                    filtradas = todas.Where(x => x.IdEstadoStp == 1).ToList();
                else if (cmbFiltroEstado.SelectedIndex == 1) // Aprobadas
                    filtradas = todas.Where(x => x.IdEstadoStp == 2).ToList();
                else if (cmbFiltroEstado.SelectedIndex == 2) // Rechazadas
                    filtradas = todas.Where(x => x.IdEstadoStp == 3).ToList();
                else // Todas
                    filtradas = todas.ToList();

                // 4. Se lo pasamos a la grilla
                dgvSolicitudesPendientes.DataSource = filtradas;
                dgvSolicitudesPendientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // 5. MANTENEMOS TODO TU FORMATO ORIGINAL INTACTO
                if (dgvSolicitudesPendientes.Columns.Contains("DireccionSucursalDestino"))
                    dgvSolicitudesPendientes.Columns["DireccionSucursalDestino"].HeaderText = "Dirección Destino";
                if (dgvSolicitudesPendientes.Columns.Contains("IdSolicitudDeTraspasoDeProductos"))
                    dgvSolicitudesPendientes.Columns["IdSolicitudDeTraspasoDeProductos"].Visible = false;
                if (dgvSolicitudesPendientes.Columns.Contains("IdSucursalOrigen"))
                    dgvSolicitudesPendientes.Columns["IdSucursalOrigen"].Visible = false;
                if (dgvSolicitudesPendientes.Columns.Contains("IdSucursalDestino"))
                    dgvSolicitudesPendientes.Columns["IdSucursalDestino"].Visible = false;
                if (dgvSolicitudesPendientes.Columns.Contains("NombreSucursalOrigen"))
                    dgvSolicitudesPendientes.Columns["IdSucursalOrigen"].Visible = false;
                if (dgvSolicitudesPendientes.Columns.Contains("IdEstadoStpNavigation"))
                    dgvSolicitudesPendientes.Columns["IdEstadoStpNavigation"].Visible = false;
                if (dgvSolicitudesPendientes.Columns.Contains("SolicitudDeTraspasoDeProductosDetalles"))
                    dgvSolicitudesPendientes.Columns["SolicitudDeTraspasoDeProductosDetalles"].Visible = false;
                if (dgvSolicitudesPendientes.Columns.Contains("NombreSucursalOrigen"))
                    dgvSolicitudesPendientes.Columns["NombreSucursalOrigen"].Visible = false;
                if (dgvSolicitudesPendientes.Columns.Contains("IdEstadoStp"))
                    dgvSolicitudesPendientes.Columns["IdEstadoStp"].Visible = false;

                // Cambiamos los títulos para el usuario
                if (dgvSolicitudesPendientes.Columns.Contains("FechaStp"))
                    dgvSolicitudesPendientes.Columns["FechaStp"].HeaderText = "Fecha";
                if (dgvSolicitudesPendientes.Columns.Contains("EstadoTexto"))
                    dgvSolicitudesPendientes.Columns["EstadoTexto"].HeaderText = "Estado";
                if (dgvSolicitudesPendientes.Columns.Contains("NombreSucursal"))
                    dgvSolicitudesPendientes.Columns["NombreSucursal"].HeaderText = "Nombre de Sucursal";
                if (dgvSolicitudesPendientes.Columns.Contains("NombreSucursalDestino"))
                    dgvSolicitudesPendientes.Columns["NombreSucursalDestino"].HeaderText = "Sucursal Destino";

                bool sonPendientes = (cmbFiltroEstado.SelectedIndex == 0);

                btnAprobar.Enabled = sonPendientes;
                btnRechazar.Enabled = sonPendientes;
                if (filtradas.Count == 0 && sonPendientes)
                {
                    MessageBox.Show("No hay Solicitudes de Traspaso pendientes.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
            cmbFiltroEstado.Items.Add("Pendientes"); // Índice 0
            cmbFiltroEstado.Items.Add("Aprobadas");  // Índice 1
            cmbFiltroEstado.Items.Add("Rechazadas"); // Índice 2
            cmbFiltroEstado.Items.Add("Todas");      // Índice 3

            // Al seleccionar el 0, dispara automáticamente el evento y carga la grilla
            cmbFiltroEstado.SelectedIndex = 0;
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
                ConfigurarColumnasGrillaDetalle();
            }
        }

        private void ConfigurarColumnasGrillaDetalle()
        {
            dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dgvDetalle.Columns.Contains("IdSolicitudDeTraspasoDeProductos"))
                dgvDetalle.Columns["IdSolicitudDeTraspasoDeProductos"].Visible = false;
            if (dgvDetalle.Columns.Contains("IdSolicitudDeTraspasoDeProductosDetalle"))
                dgvDetalle.Columns["IdSolicitudDeTraspasoDeProductosDetalle"].Visible = false;
            if (dgvDetalle.Columns.Contains("IdProducto"))
                dgvDetalle.Columns["IdProducto"].Visible = false;
            if (dgvDetalle.Columns.Contains("IdProductoNavigation"))
                dgvDetalle.Columns["IdProductoNavigation"].Visible = false;
            if (dgvDetalle.Columns.Contains("IdSolicitudDeTraspasoDeProductosNavigation"))
                dgvDetalle.Columns["IdSolicitudDeTraspasoDeProductosNavigation"].Visible = false;

            if (dgvDetalle.Columns.Contains("NombreProducto"))
                dgvDetalle.Columns["NombreProducto"].HeaderText = "Producto";
            if (dgvDetalle.Columns.Contains("PesoNeto"))
                dgvDetalle.Columns["PesoNeto"].HeaderText = "Peso Neto";

            if (dgvDetalle.Columns.Contains("NombreProducto"))
                dgvDetalle.Columns["NombreProducto"].DisplayIndex = 0;
            if (dgvDetalle.Columns.Contains("Marca"))
                dgvDetalle.Columns["Marca"].DisplayIndex = 1;
            if (dgvDetalle.Columns.Contains("PesoNeto"))
                dgvDetalle.Columns["PesoNeto"].DisplayIndex = 2;
            if (dgvDetalle.Columns.Contains("Cantidad"))
                dgvDetalle.Columns["Cantidad"].DisplayIndex = 4;
            if (dgvDetalle.Columns.Contains("Unidad"))
                dgvDetalle.Columns["Unidad"].DisplayIndex = 3;
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
            CargarSolicitudes();
        }

        private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarSolicitudes();
        }        
    }
}