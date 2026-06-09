using Logic.CustomExceptions;
using Logic.Facade;
using ModelsDTO;
using Services.Bll.CustomExceptions;
using Services.Facade;
using System.ComponentModel;
using System.Data;
using Services.Facade.Extensions;

namespace FormUI.FormSucursal
{
    public partial class FormSolicitarTraspasoProductoSucursales : Form
    {
        private BindingList<SolicitudDeTraspasoDeProductosDetalleDTO> _listaProductos = new BindingList<SolicitudDeTraspasoDeProductosDetalleDTO>();

        /// <summary>
        /// Inicializa el formulario y la lista vinculante temporal para los productos del traspaso.
        /// </summary>
        public FormSolicitarTraspasoProductoSucursales()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Oculta identificadores internos, ajusta el orden visual y aplica traducciones a los encabezados de la grilla temporal de productos.
        /// </summary>
        private void ConfigurarColumnasGrilla()
        {
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvProductos.Columns.Contains("IdSolicitudDeTraspasoDeProductosDetalle"))
                dgvProductos.Columns["IdSolicitudDeTraspasoDeProductosDetalle"].Visible = false;

            if (dgvProductos.Columns.Contains("IdSolicitudDeTraspasoDeProductos"))
                dgvProductos.Columns["IdSolicitudDeTraspasoDeProductos"].Visible = false;

            if (dgvProductos.Columns.Contains("IdProducto"))
                dgvProductos.Columns["IdProducto"].Visible = false;

            if (dgvProductos.Columns.Contains("IdProductoNavigation"))
                dgvProductos.Columns["IdProductoNavigation"].Visible = false;

            if (dgvProductos.Columns.Contains("IdSolicitudDeTraspasoDeProductosNavigation"))
                dgvProductos.Columns["IdSolicitudDeTraspasoDeProductosNavigation"].Visible = false;

            if (dgvProductos.Columns.Contains("NombreProducto"))
                dgvProductos.Columns["NombreProducto"].HeaderText = "Producto".Traducir();
            if (dgvProductos.Columns.Contains("PesoNeto"))
                dgvProductos.Columns["PesoNeto"].HeaderText = "Peso Neto".Traducir();

            if (dgvProductos.Columns.Contains("NombreProducto"))
                dgvProductos.Columns["NombreProducto"].DisplayIndex = 0;

            if (dgvProductos.Columns.Contains("Marca"))
                dgvProductos.Columns["Marca"].DisplayIndex = 1;

            if (dgvProductos.Columns.Contains("Cantidad"))
                dgvProductos.Columns["Cantidad"].DisplayIndex = 2;

            if (dgvProductos.Columns.Contains("Unidad"))
                dgvProductos.Columns["Unidad"].DisplayIndex = 3;
        }
        /// <summary>
        /// Consulta las sucursales habilitadas como origen (tipo Depósito-Venta) y las carga en el menú desplegable correspondiente.
        /// </summary>
        private void CargarSucursales()
        {
            SucursalService sucursalService = new SucursalService();
            var todasLasSucursales = sucursalService.GetAllSucursales();
            var sucursalesOrigen = todasLasSucursales
                .Where(s => s.IdTipoSucursal == 2)
                .ToList();

            cmbSucursalOrigen.DataSource = sucursalService.ObtenerActivas();
            cmbSucursalOrigen.DataSource = sucursalesOrigen;
            cmbSucursalOrigen.DisplayMember = "Direccion";
            cmbSucursalOrigen.ValueMember = "IdSucursal";
            cmbSucursalOrigen.SelectedIndex = -1;
        }
        /// <summary>
        /// Obtiene el catálogo de productos ACTIVOS y lo vincula al menú desplegable de selección.
        /// </summary>
        private void CargarProductos()
        {
            ProductoService productoService = new ProductoService();
            cmbProducto.DataSource = productoService.ObtenerActivos();
            cmbProducto.DisplayMember = "NombreConPeso";
            cmbProducto.ValueMember = "IdProducto";
            cmbProducto.SelectedIndex = -1;
        }
        /// <summary>
        /// Valida la entrada, agrega el producto a la lista temporal de traspasos o incrementa su cantidad si ya existía en la grilla.
        /// </summary>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un producto.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtbCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida mayor a cero.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var productoElegido = (ProductoDTO)cmbProducto.SelectedItem;

            var itemExistente = _listaProductos.FirstOrDefault(p => p.IdProducto == productoElegido.IdProducto);
            if (itemExistente != null)
            {
                itemExistente.Cantidad += cantidad;
                dgvProductos.Refresh();
            }
            else
            {
                _listaProductos.Add(new SolicitudDeTraspasoDeProductosDetalleDTO
                {
                    IdProducto = productoElegido.IdProducto,
                    NombreProducto = productoElegido.NombreProducto,
                    PesoNeto = (decimal)(productoElegido.PesoNeto ?? 0),
                    Marca = productoElegido.Marca,
                    Cantidad = cantidad,
                    Unidad = "KG"
                });
            }
            cmbProducto.SelectedIndex = -1;
            txtbCantidad.Clear();
            txtbPesoNeto.Clear();
            txtbMarca.Clear();
        }
        /// <summary>
        /// Verifica los datos de origen y destino, ensambla la solicitud de traspaso completa y la envía a la base de datos.
        /// </summary>
        private void btnSolicitarTraspaso_Click(object sender, EventArgs e)
        {
            if (_listaProductos.Count == 0)
            {
                MessageBox.Show("Agregue al menos un producto a la solicitud.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbSucursalOrigen.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una sucursal de destino.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!SessionManager.Current.IdSucursalActual.HasValue)
            {
                MessageBox.Show("No hay una sucursal seleccionada en la sesión actual.".Traducir(), "Advertencia".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Guid idSucursalOrigen = (Guid)(cmbSucursalOrigen.SelectedValue ?? Guid.Empty);
                Guid idSucursalDestino = SessionManager.Current.IdSucursalActual.Value;

                var nuevaSolicitud = new SolicitudDeTraspasoDeProductoDTO
                {
                    FechaStp = dtpFecha.Value.Date,
                    IdSucursalOrigen = idSucursalOrigen,
                    IdSucursalDestino = idSucursalDestino
                };

                var listaDetalles = _listaProductos.ToList();
                var service = new Logic.Facade.TraspasoService();
                service.GenerarSolicitud(nuevaSolicitud, listaDetalles);

                MessageBox.Show("¡Solicitud de traspaso generada con éxito!".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);

                _listaProductos.Clear();
                cmbSucursalOrigen.SelectedIndex = -1;
                dtpFecha.Value = DateTime.Today;
            }
            catch (SesionExpiradaException ex)
            {
                MessageBox.Show(ex.Message.Traducir(), "Sesión Expirada".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Restart();
            }
            catch (TraspasoMismaSucursalException ex)
            {
                MessageBox.Show(ex.Message.Traducir(), "Error de Destino".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbSucursalOrigen.Focus();
            }
            catch (CantidadInvalidaException ex)
            {
                MessageBox.Show(ex.Message.Traducir(), "Error en los datos".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ocurrió un error inesperado al procesar la solicitud: {0}".Traducir(), ex.Message), "Error de Sistema".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Evento de carga inicial que asocia la grilla, preconfigura el destino, llena las opciones desplegables y traduce el formulario.
        /// </summary>
        private void FormSolicitarTraspasoProductoSucursales_Load_1(object sender, EventArgs e)
        {
            try
            {
                txtbSucursalDestino.Text = SessionManager.Current.NombreSucursalActual;
                dgvProductos.DataSource = _listaProductos;
                ConfigurarColumnasGrilla();
                CargarSucursales();
                CargarProductos();
                ConfigurarFecha();
                dtpFecha.Value = DateTime.Today;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar la pantalla: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            TraductorUI.TraducirFormulario(this);
        }
        /// <summary>
        /// Asigna la fecha actual al selector de fechas y lo bloquea para evitar modificaciones.
        /// </summary>
        private void ConfigurarFecha()
        {
            dtpFecha.Value = DateTime.Today;
            dtpFecha.Enabled = false;
        }
        /// <summary>
        /// Detecta la selección de un producto en el menú desplegable y rellena automáticamente los campos visuales vinculados.
        /// </summary>
        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedItem != null && cmbProducto.SelectedIndex != -1)
            {
                var productoSeleccionado = (ProductoDTO)cmbProducto.SelectedItem;
                txtbPesoNeto.Text = productoSeleccionado.PesoNeto.ToString();
                txtbMarca.Text = productoSeleccionado.Marca;
            }
            else
            {
                txtbPesoNeto.Clear();
                txtbMarca.Clear();
            }
        }
    }
}