using Logic.Facade;
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

namespace FormUI.FormInventario
{
    public partial class FormHistorialMovimientos : Form
    {
        private readonly TraspasoService _traspasoService = new TraspasoService();
        private readonly InventarioService _inventarioService = new InventarioService();
        /// <summary>
        /// Inicializa el formulario y los servicios necesarios para consultar el historial de movimientos.
        /// </summary>
        public FormHistorialMovimientos()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Evento de carga inicial que desencadena la consulta de historiales y aplica las traducciones de la interfaz.
        /// </summary>
        private void FormHistorialMovimientos_Load(object sender, EventArgs e)
        {
            CargarTraspasos();
            CargarEntregas();
            TraductorUI.TraducirFormulario(this);
        }
        /// <summary>
        /// Fuerza la actualización manual de la grilla de historial de traspasos.
        /// </summary>
        private void btnVerTraspasos_Click(object sender, EventArgs e)
        {
            CargarTraspasos();
        }
        /// <summary>
        /// Verifica la sesión activa, obtiene el historial de traspasos de la sucursal actual y lo vincula a la grilla correspondiente.
        /// </summary>
        private void CargarTraspasos()
        {
            try
            {
                if (SessionManager.Current.IdSucursalActual == null)
                {
                    MessageBox.Show("Error: No se detectó una sucursal logueada.".Traducir(), "Error de Sesión".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Guid miSucursal = SessionManager.Current.IdSucursalActual.Value;
                var historialTraspasos = _traspasoService.ObtenerHistorialTraspasos(miSucursal);

                dgvTraspasoProductos.DataSource = null;
                dgvTraspasoProductos.DataSource = historialTraspasos;

                ConfigurarGrillaTraspasos();

                if (historialTraspasos.Count == 0)
                {
                    MessageBox.Show("No hay movimientos de traspasos registrados para esta sucursal.".Traducir(), "Información".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar los traspasos: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Oculta columnas técnicas, reordena los campos y aplica traducciones a los encabezados de la grilla de traspasos.
        /// </summary>
        private void ConfigurarGrillaTraspasos()
        {
            dgvTraspasoProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvTraspasoProductos.Columns.Contains("UsuarioResponsable"))
            {
                dgvTraspasoProductos.Columns["UsuarioResponsable"].Visible = false;
            }

            if (dgvTraspasoProductos.Columns.Contains("Fecha"))
            {
                dgvTraspasoProductos.Columns["Fecha"].HeaderText = "Fecha y Hora".Traducir();
                dgvTraspasoProductos.Columns["Fecha"].DisplayIndex = 0;
            }

            if (dgvTraspasoProductos.Columns.Contains("TipoMovimiento"))
            {
                dgvTraspasoProductos.Columns["TipoMovimiento"].HeaderText = "Movimiento".Traducir();
                dgvTraspasoProductos.Columns["TipoMovimiento"].DisplayIndex = 1;
            }

            if (dgvTraspasoProductos.Columns.Contains("SucursalInvolucrada"))
            {
                dgvTraspasoProductos.Columns["SucursalInvolucrada"].HeaderText = "Origen / Destino".Traducir();
                dgvTraspasoProductos.Columns["SucursalInvolucrada"].DisplayIndex = 2;
            }

            if (dgvTraspasoProductos.Columns.Contains("Producto"))
            {
                dgvTraspasoProductos.Columns["Producto"].HeaderText = "Producto".Traducir();
                dgvTraspasoProductos.Columns["Producto"].DisplayIndex = 3;
            }

            if (dgvTraspasoProductos.Columns.Contains("Marca"))
            {
                dgvTraspasoProductos.Columns["Marca"].HeaderText = "Marca".Traducir();
                dgvTraspasoProductos.Columns["Marca"].DisplayIndex = 4;
            }

            if (dgvTraspasoProductos.Columns.Contains("PesoNeto"))
            {
                dgvTraspasoProductos.Columns["PesoNeto"].HeaderText = "Peso Neto".Traducir();
                dgvTraspasoProductos.Columns["PesoNeto"].DisplayIndex = 5;
            }

            if (dgvTraspasoProductos.Columns.Contains("Unidad"))
            {
                dgvTraspasoProductos.Columns["Unidad"].HeaderText = "Unidad".Traducir();
                dgvTraspasoProductos.Columns["Unidad"].DisplayIndex = 6;
            }

            if (dgvTraspasoProductos.Columns.Contains("Cantidad"))
            {
                dgvTraspasoProductos.Columns["Cantidad"].HeaderText = "Cantidad".Traducir();
                dgvTraspasoProductos.Columns["Cantidad"].DisplayIndex = 7;


            }
        }
        /// <summary>
        /// Fuerza la actualización manual de la grilla de historial de entregas (ingresos de mercadería).
        /// </summary>
        private void btnVerEntrega_Click(object sender, EventArgs e)
        {
            CargarEntregas();
        }
        /// <summary>
        /// Valida la sesión actual, obtiene el historial de ingresos de inventario de la sucursal y lo muestra en pantalla.
        /// </summary>
        private void CargarEntregas()
        {
            try
            {
                if (SessionManager.Current.IdSucursalActual == null)
                {
                    MessageBox.Show("Error: No se detectó una sucursal logueada.".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Guid miSucursal = SessionManager.Current.IdSucursalActual.Value;
                var historialEntregas = _inventarioService.ObtenerHistorialEntregas(miSucursal);

                dgvEntregaProductos.DataSource = null;
                dgvEntregaProductos.DataSource = historialEntregas;

                ConfigurarGrillaEntregas();

                if (historialEntregas.Count == 0)
                {
                    MessageBox.Show("No hay ingresos de mercadería registrados para esta sucursal.".Traducir(), "Información".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar las entregas: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Aplica formatos visuales, redondeo de decimales y traducciones a las columnas de la grilla de entregas.
        /// </summary>
        private void ConfigurarGrillaEntregas()
        {
            dgvEntregaProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvEntregaProductos.Columns.Contains("Fecha"))
                dgvEntregaProductos.Columns["Fecha"].HeaderText = "Fecha de Ingreso".Traducir();

            if (dgvEntregaProductos.Columns.Contains("Cantidad"))
                dgvEntregaProductos.Columns["Cantidad"].HeaderText = "Cant. Agregada".Traducir();

            if (dgvEntregaProductos.Columns.Contains("Marca"))
                dgvEntregaProductos.Columns["Marca"].HeaderText = "Marca".Traducir();

            if (dgvEntregaProductos.Columns.Contains("PesoUnitario"))
            {
                dgvEntregaProductos.Columns["PesoUnitario"].HeaderText = "Peso Unit.".Traducir();
                dgvEntregaProductos.Columns["PesoUnitario"].DefaultCellStyle.Format = "N2";
            }
        }
    }
}
