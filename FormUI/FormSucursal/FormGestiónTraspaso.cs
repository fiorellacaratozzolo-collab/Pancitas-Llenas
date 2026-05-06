using DataAccess.Implementations.SqlServer;
using Logic;
using Logic.CustomExceptions;
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
using Services.Facade.Extensions;

namespace FormUI.FormSucursal
{
    public partial class FormGestiónTraspaso : Form
    {
        private Logic.Facade.TraspasoService _traspasoService;

        /// <summary>
        /// Inicializa el formulario y los servicios necesarios para la gestión de traspasos.
        /// </summary>
        public FormGestiónTraspaso()
        {
            InitializeComponent();
            _traspasoService = new Logic.Facade.TraspasoService();
        }
        /// <summary>
        /// Consulta y filtra las solicitudes de traspaso de la sucursal actual para mostrarlas en la grilla principal.
        /// </summary>
        private void CargarSolicitudes()
        {
            try
            {
                dgvDetalle.DataSource = null;
                if (!SessionManager.Current.IdSucursalActual.HasValue)
                {
                    MessageBox.Show("No hay una sucursal seleccionada en la sesión actual.".Traducir(), "Advertencia".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Guid miSucursal = SessionManager.Current.IdSucursalActual.Value;
                var todas = _traspasoService.ObtenerTodasPorSucursal(miSucursal);
                List<SolicitudDeTraspasoDeProductoDTO> filtradas;

                if (cmbFiltroEstado.SelectedIndex == 0)
                    filtradas = todas.Where(x => x.IdEstadoStp == 1).ToList();
                else if (cmbFiltroEstado.SelectedIndex == 1)
                    filtradas = todas.Where(x => x.IdEstadoStp == 2).ToList();
                else if (cmbFiltroEstado.SelectedIndex == 2)
                    filtradas = todas.Where(x => x.IdEstadoStp == 3).ToList();
                else
                    filtradas = todas.ToList();

                dgvSolicitudesPendientes.DataSource = filtradas;
                dgvSolicitudesPendientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dgvSolicitudesPendientes.Columns.Contains("DireccionSucursalDestino"))
                    dgvSolicitudesPendientes.Columns["DireccionSucursalDestino"].HeaderText = "Dirección Destino".Traducir();
                if (dgvSolicitudesPendientes.Columns.Contains("IdSolicitudDeTraspasoDeProductos"))
                    dgvSolicitudesPendientes.Columns["IdSolicitudDeTraspasoDeProductos"].Visible = false;
                if (dgvSolicitudesPendientes.Columns.Contains("IdSucursalOrigen"))
                    dgvSolicitudesPendientes.Columns["IdSucursalOrigen"].Visible = false;
                if (dgvSolicitudesPendientes.Columns.Contains("IdSucursalDestino"))
                    dgvSolicitudesPendientes.Columns["IdSucursalDestino"].Visible = false;
                if (dgvSolicitudesPendientes.Columns.Contains("IdEstadoStpNavigation"))
                    dgvSolicitudesPendientes.Columns["IdEstadoStpNavigation"].Visible = false;
                if (dgvSolicitudesPendientes.Columns.Contains("SolicitudDeTraspasoDeProductosDetalles"))
                    dgvSolicitudesPendientes.Columns["SolicitudDeTraspasoDeProductosDetalles"].Visible = false;
                if (dgvSolicitudesPendientes.Columns.Contains("NombreSucursalOrigen"))
                    dgvSolicitudesPendientes.Columns["NombreSucursalOrigen"].Visible = false;
                if (dgvSolicitudesPendientes.Columns.Contains("IdEstadoStp"))
                    dgvSolicitudesPendientes.Columns["IdEstadoStp"].Visible = false;

                if (dgvSolicitudesPendientes.Columns.Contains("FechaStp"))
                    dgvSolicitudesPendientes.Columns["FechaStp"].HeaderText = "Fecha".Traducir();
                if (dgvSolicitudesPendientes.Columns.Contains("EstadoTexto"))
                    dgvSolicitudesPendientes.Columns["EstadoTexto"].HeaderText = "Estado".Traducir();
                if (dgvSolicitudesPendientes.Columns.Contains("NombreSucursal"))
                    dgvSolicitudesPendientes.Columns["NombreSucursal"].HeaderText = "Nombre de Sucursal".Traducir();
                if (dgvSolicitudesPendientes.Columns.Contains("NombreSucursalDestino"))
                    dgvSolicitudesPendientes.Columns["NombreSucursalDestino"].HeaderText = "Sucursal Destino".Traducir();

                bool sonPendientes = (cmbFiltroEstado.SelectedIndex == 0);

                btnAprobar.Enabled = sonPendientes;
                btnRechazar.Enabled = sonPendientes;

                if (filtradas.Count == 0 && sonPendientes)
                {
                    MessageBox.Show("No hay Solicitudes de Traspaso pendientes.".Traducir(), "Información".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al actualizar la lista: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Evento de carga inicial que llena el filtro de estados, lo selecciona por defecto y traduce toda la interfaz.
        /// </summary>
        private void FormGestiónTraspaso_Load(object sender, EventArgs e)
        {
            cmbFiltroEstado.Items.Add("Pendientes".Traducir());
            cmbFiltroEstado.Items.Add("Aprobadas".Traducir());
            cmbFiltroEstado.Items.Add("Rechazadas".Traducir());
            cmbFiltroEstado.Items.Add("Todas".Traducir());

            cmbFiltroEstado.SelectedIndex = 0;
            TraductorUI.TraducirFormulario(this);
        }
        /// <summary>
        /// Detecta el cambio de selección en la grilla principal y carga los productos solicitados correspondientes en la grilla de detalles.
        /// </summary>
        private void dgvSolicitudesPendientes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSolicitudesPendientes.CurrentRow != null)
            {
                var solicitudSeleccionada = (SolicitudDeTraspasoDeProductoDTO)dgvSolicitudesPendientes.CurrentRow.DataBoundItem;
                var detalles = _traspasoService.ObtenerDetallesPorSolicitud(solicitudSeleccionada.IdSolicitudDeTraspasoDeProductos);
                dgvDetalle.DataSource = detalles;
                ConfigurarColumnasGrillaDetalle();
            }
        }
        /// <summary>
        /// Oculta identificadores internos, ajusta el orden visual y aplica nombres traducidos a las columnas de la grilla de detalles.
        /// </summary>
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
            {
                dgvDetalle.Columns["NombreProducto"].HeaderText = "Producto".Traducir();
                dgvDetalle.Columns["NombreProducto"].DisplayIndex = 0;
            }
            if (dgvDetalle.Columns.Contains("Marca"))
                dgvDetalle.Columns["Marca"].DisplayIndex = 1;
            if (dgvDetalle.Columns.Contains("PesoNeto"))
            {
                dgvDetalle.Columns["PesoNeto"].HeaderText = "Peso Neto".Traducir();
                dgvDetalle.Columns["PesoNeto"].DisplayIndex = 2;
            }
            if (dgvDetalle.Columns.Contains("Unidad"))
                dgvDetalle.Columns["Unidad"].DisplayIndex = 3;
            if (dgvDetalle.Columns.Contains("Cantidad"))
                dgvDetalle.Columns["Cantidad"].DisplayIndex = 4;
        }
        /// <summary>
        /// Valida el stock disponible, solicita confirmación y aprueba la solicitud de traspaso seleccionada.
        /// </summary>
        private void btnAprobar_Click_1(object sender, EventArgs e)
        {
            if (dgvSolicitudesPendientes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una solicitud para aprobar.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var solicitudSeleccionada = (SolicitudDeTraspasoDeProductoDTO)dgvSolicitudesPendientes.CurrentRow.DataBoundItem;

            var confirmacion = MessageBox.Show("¿Está seguro que desea APROBAR esta solicitud?\nSe descontará el stock de su sucursal.".Traducir(), "Confirmar Aprobación".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    _traspasoService.AprobarTraspaso(solicitudSeleccionada.IdSolicitudDeTraspasoDeProductos);
                    MessageBox.Show("¡Éxito!".Traducir(), "Información".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnActualizar.PerformClick();
                }
                catch (StockInsuficienteException ex)
                {
                    string msj = string.Format("{0}\n\nPor favor, revise el inventario de la sucursal de origen.".Traducir(), ex.Message);
                    MessageBox.Show(msj, "Falta de Stock".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error inesperado: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Solicita confirmación y ejecuta el rechazo de la solicitud de traspaso seleccionada.
        /// </summary>
        private void btnRechazar_Click_1(object sender, EventArgs e)
        {
            if (dgvSolicitudesPendientes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una solicitud para rechazar.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var solicitudSeleccionada = (SolicitudDeTraspasoDeProductoDTO)dgvSolicitudesPendientes.CurrentRow.DataBoundItem;

            var confirmacion = MessageBox.Show("¿Está seguro que desea RECHAZAR esta solicitud?".Traducir(), "Confirmar Rechazo".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    _traspasoService.RechazarTraspaso(solicitudSeleccionada.IdSolicitudDeTraspasoDeProductos);
                    MessageBox.Show("La solicitud ha sido rechazada.".Traducir(), "Información".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnActualizar.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Fuerza la actualización manual de la grilla principal llamando a la recarga de solicitudes.
        /// </summary>
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarSolicitudes();
        }
        /// <summary>
        /// Recarga automáticamente las solicitudes mostradas al cambiar la opción en el filtro de estados.
        /// </summary>
        private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarSolicitudes();
        }        
    }
}