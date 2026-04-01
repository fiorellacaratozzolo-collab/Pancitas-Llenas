using DataAccess.Implementations.SqlServer;
using Logic;
using Logic.MappingProfiles;
using ModelsDTO;
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
        private readonly TraspasoLogic _traspasoLogic;

        public FormGestiónTraspaso()
        {
            InitializeComponent();

            // Cambio clave: Ya no necesitas instanciar el UnitOfWork aquí, 
            // la Logic ya lo hace por defecto siguiendo tu nuevo patrón.
            _traspasoLogic = new TraspasoLogic();
        }

        private void FormGestionarTraspaso_Load(object sender, EventArgs e)
        {
            RefrescarGrilla();
        }

        private void RefrescarGrilla()
        {
            try
            {
                // Mantenemos la lógica de filtrado: 
                // Solo vemos lo que viene HACIA nuestra sucursal y está PENDIENTE (Estado 1)
                var lista = _traspasoLogic.ObtenerTodas()
                            .Where(x => x.IdSucursalOrigen == GlobalSettings.SucursalActualId && x.IdEstadoStp == 1)
                            .ToList();

                dgvSolicitudes.DataSource = lista;

                // Si la lista está vacía, limpiamos el detalle
                if (lista.Count == 0) dgvDetalles.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar solicitudes: {ex.Message}");
            }
        }

        private void dgvSolicitudes_SelectionChanged(object sender, EventArgs e)
        {
            // Al cambiar de fila, el DTO ya trae sus detalles mapeados
            if (dgvSolicitudes.CurrentRow?.DataBoundItem is SolicitudDeTraspasoDeProductoDTO solicitud)
            {
                dgvDetalles.DataSource = solicitud.SolicitudDeTraspasoDeProductosDetalles;
            }
        }

        private void btnAprobar_Click(object sender, EventArgs e)
        {
            if (dgvSolicitudes.CurrentRow?.DataBoundItem is SolicitudDeTraspasoDeProductoDTO selected)
            {
                var confirm = MessageBox.Show("¿Confirma la aprobación? Se descontará stock del depósito y se sumará a la sucursal destino.",
                                            "Confirmar Operación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        // Llamamos al nuevo método robusto que creamos en la Logic
                        _traspasoLogic.AprobarYProcesarStock(selected.IdSolicitudDeTraspasoDeProductos);

                        MessageBox.Show("Traspaso procesado con éxito. El inventario ha sido actualizado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        RefrescarGrilla();
                        dgvDetalles.DataSource = null;
                    }
                    catch (Exception ex)
                    {
                        // Aquí capturaremos, por ejemplo, el error de "Stock Insuficiente"
                        MessageBox.Show($"No se pudo procesar: {ex.Message}", "Error de Inventario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            if (dgvSolicitudes.CurrentRow?.DataBoundItem is SolicitudDeTraspasoDeProductoDTO selected)
            {
                var confirm = MessageBox.Show("¿Está seguro de rechazar esta solicitud?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    _traspasoLogic.RechazarSolicitud(selected.IdSolicitudDeTraspasoDeProductos);
                    RefrescarGrilla();
                    dgvDetalles.DataSource = null;
                }
            }
        }
    }
}