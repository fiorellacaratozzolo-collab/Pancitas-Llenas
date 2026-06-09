using Logic.Facade;
using ModelsDTO;
using System.Data;
using Services.Facade.Extensions;

namespace FormUI.FormVenta
{
    public partial class FormListaPrecios : Form
    {
        private readonly ProductoService _productoService = new ProductoService();
        private List<ProductoDTO> _todosLosProductos = new List<ProductoDTO>();

        /// <summary>
        /// Inicializa el formulario y los componentes visuales principales.
        /// </summary>
        public FormListaPrecios()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Evento de carga inicial del formulario que obtiene los productos, los vincula a la grilla y aplica el idioma seleccionado.
        /// </summary>
        private void FormListaPrecios_Load(object sender, EventArgs e)
        {
            try
            {
                CargarListaPrecios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar la lista de precios: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            TraductorUI.TraducirFormulario(this);
        }
        /// <summary>
        /// Consulta la base de datos para traer el catálogo completo de productos y lo almacena en la memoria global del formulario.
        /// </summary>
        private void CargarListaPrecios()
        {
            _todosLosProductos = _productoService.ObtenerActivos();
            dgvProductoPrecio.DataSource = _todosLosProductos;
            ConfigurarGrilla();
        }
        /// <summary>
        /// Oculta columnas técnicas irrelevantes, y aplica formato, orden y traducciones a los encabezados visibles de la grilla.
        /// </summary>
        private void ConfigurarGrilla()
        {
            if (dgvProductoPrecio.Columns.Contains("IdProducto")) dgvProductoPrecio.Columns["IdProducto"].Visible = false;
            if (dgvProductoPrecio.Columns.Contains("IdCategoria")) dgvProductoPrecio.Columns["IdCategoria"].Visible = false;
            if (dgvProductoPrecio.Columns.Contains("IdMarca")) dgvProductoPrecio.Columns["IdMarca"].Visible = false;
            if (dgvProductoPrecio.Columns.Contains("NombreConPeso")) dgvProductoPrecio.Columns["NombreConPeso"].Visible = false;
            if (dgvProductoPrecio.Columns.Contains("ProveedorProductos")) dgvProductoPrecio.Columns["ProveedorProductos"].Visible = false;
            if (dgvProductoPrecio.Columns.Contains("SolicitudDePedidoDetalles")) dgvProductoPrecio.Columns["SolicitudDePedidoDetalles"].Visible = false;
            if (dgvProductoPrecio.Columns.Contains("SolicitudDeTraspasoDeProductosDetalles")) dgvProductoPrecio.Columns["SolicitudDeTraspasoDeProductosDetalles"].Visible = false;
            if (dgvProductoPrecio.Columns.Contains("StockPorSucursals")) dgvProductoPrecio.Columns["StockPorSucursals"].Visible = false;
            if (dgvProductoPrecio.Columns.Contains("VentaDetalles")) dgvProductoPrecio.Columns["VentaDetalles"].Visible = false;
            if (dgvProductoPrecio.Columns.Contains("Activo")) dgvProductoPrecio.Columns["Activo"].Visible = false;

            if (dgvProductoPrecio.Columns.Contains("NombreProducto"))
            {
                dgvProductoPrecio.Columns["NombreProducto"].HeaderText = "Producto".Traducir();
                dgvProductoPrecio.Columns["NombreProducto"].DisplayIndex = 0;
            }

            if (dgvProductoPrecio.Columns.Contains("Marca"))
            {
                dgvProductoPrecio.Columns["Marca"].HeaderText = "Marca".Traducir();
                dgvProductoPrecio.Columns["Marca"].DisplayIndex = 1;
            }

            if (dgvProductoPrecio.Columns.Contains("PesoNeto"))
            {
                dgvProductoPrecio.Columns["PesoNeto"].HeaderText = "Peso Neto".Traducir();
                dgvProductoPrecio.Columns["PesoNeto"].DisplayIndex = 2;
            }

            if (dgvProductoPrecio.Columns.Contains("Unidad"))
            {
                dgvProductoPrecio.Columns["Unidad"].HeaderText = "Unidad".Traducir();
                dgvProductoPrecio.Columns["Unidad"].DisplayIndex = 3;
            }

            if (dgvProductoPrecio.Columns.Contains("PrecioNeto"))
            {
                dgvProductoPrecio.Columns["PrecioNeto"].HeaderText = "Precio".Traducir();
                dgvProductoPrecio.Columns["PrecioNeto"].DefaultCellStyle.Format = "N2";
                dgvProductoPrecio.Columns["PrecioNeto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvProductoPrecio.Columns["PrecioNeto"].DisplayIndex = 4;
            }

            dgvProductoPrecio.ReadOnly = true;
            dgvProductoPrecio.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductoPrecio.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductoPrecio.AllowUserToAddRows = false;
        }
        /// <summary>
        /// Filtra la lista global almacenada en memoria de acuerdo al texto ingresado y refresca instantáneamente la grilla de resultados.
        /// </summary>
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (_todosLosProductos == null || _todosLosProductos.Count == 0) return;

            string filtro = txtBuscar.Text.ToLower().Trim();

            var listaFiltrada = _todosLosProductos
                .Where(p => p.NombreProducto != null && p.NombreProducto.ToLower().Contains(filtro))
                .ToList();

            dgvProductoPrecio.DataSource = null;
            dgvProductoPrecio.DataSource = listaFiltrada;
            ConfigurarGrilla();
        }
    }
}
