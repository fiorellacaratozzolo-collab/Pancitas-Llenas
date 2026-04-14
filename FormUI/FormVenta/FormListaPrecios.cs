using Logic.Facade;
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

namespace FormUI.FormVenta
{
    public partial class FormListaPrecios : Form
    {
        private readonly ProductoService _productoService = new ProductoService();
        private List<ProductoDTO> _todosLosProductos = new List<ProductoDTO>();
        public FormListaPrecios()
        {
            InitializeComponent();
        }

        private void FormListaPrecios_Load(object sender, EventArgs e)
        {
            try
            {
                CargarListaPrecios();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la lista de precios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarListaPrecios()
        {
            // Traemos TODOS los productos directo de la base de datos (sin importar inventario/stock)
            var listaProductos = _productoService.GetAllProductos();

            // Enlazamos a la grilla
            dgvProductoPrecio.DataSource = listaProductos;
            ConfigurarGrilla();
        }

        private void ConfigurarGrilla()
        {
            // 1. Ocultamos las columnas técnicas y claves foráneas
            if (dgvProductoPrecio.Columns.Contains("IdProducto")) dgvProductoPrecio.Columns["IdProducto"].Visible = false;
            if (dgvProductoPrecio.Columns.Contains("IdCategoria")) dgvProductoPrecio.Columns["IdCategoria"].Visible = false;
            if (dgvProductoPrecio.Columns.Contains("IdMarca")) dgvProductoPrecio.Columns["IdMarca"].Visible = false;
            if (dgvProductoPrecio.Columns.Contains("NombreConPeso")) dgvProductoPrecio.Columns["NombreConPeso"].Visible = false;
            if (dgvProductoPrecio.Columns.Contains("ProveedorProductos")) dgvProductoPrecio.Columns["ProveedorProductos"].Visible = false;
            if (dgvProductoPrecio.Columns.Contains("SolicitudDePedidoDetalles")) dgvProductoPrecio.Columns["SolicitudDePedidoDetalles"].Visible = false;
            if (dgvProductoPrecio.Columns.Contains("SolicitudDeTraspasoDeProductosDetalles")) dgvProductoPrecio.Columns["SolicitudDeTraspasoDeProductosDetalles"].Visible = false;
            if (dgvProductoPrecio.Columns.Contains("StockPorSucursals")) dgvProductoPrecio.Columns["StockPorSucursals"].Visible = false;
            if (dgvProductoPrecio.Columns.Contains("VentaDetalles")) dgvProductoPrecio.Columns["VentaDetalles"].Visible = false;


            // 2. Formateamos las columnas que SÍ queremos ver
            if (dgvProductoPrecio.Columns.Contains("NombreProducto"))
            {
                dgvProductoPrecio.Columns["NombreProducto"].HeaderText = "Producto";
                dgvProductoPrecio.Columns["NombreProducto"].DisplayIndex = 0;
            }

            if (dgvProductoPrecio.Columns.Contains("PesoNeto"))
            {
                dgvProductoPrecio.Columns["PesoNeto"].HeaderText = "Peso Neto";
                dgvProductoPrecio.Columns["PesoNeto"].DisplayIndex = 1;
            }

            if (dgvProductoPrecio.Columns.Contains("Unidad"))
            {
                dgvProductoPrecio.Columns["Unidad"].HeaderText = "Unidad";
                dgvProductoPrecio.Columns["Unidad"].DisplayIndex = 2;
            }

            if (dgvProductoPrecio.Columns.Contains("PrecioNeto"))
            {
                dgvProductoPrecio.Columns["PrecioNeto"].HeaderText = "Precio";
                dgvProductoPrecio.Columns["PrecioNeto"].DefaultCellStyle.Format = "N2";
                dgvProductoPrecio.Columns["PrecioNeto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvProductoPrecio.Columns["PrecioNeto"].DisplayIndex = 3;
            }

            // 3. Ajustes visuales de seguridad (Solo Lectura)
            dgvProductoPrecio.ReadOnly = true; // Nadie puede editar los precios desde acá
            dgvProductoPrecio.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductoPrecio.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductoPrecio.AllowUserToAddRows = false; // Quita la fila vacía del final
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // Si no hay lista original, no hacemos nada
            if (_todosLosProductos == null || _todosLosProductos.Count == 0) return;

            string filtro = txtBuscar.Text.ToLower().Trim();

            // 4. Filtramos la lista en memoria (Es ultra rápido)
            var listaFiltrada = _todosLosProductos
                .Where(p => p.NombreProducto != null && p.NombreProducto.ToLower().Contains(filtro))
                .ToList();

            // 5. Actualizamos la grilla con los resultados y reaplicamos los formatos
            dgvProductoPrecio.DataSource = null; // Limpiamos el origen anterior
            dgvProductoPrecio.DataSource = listaFiltrada;
            ConfigurarGrilla();
        }
    }
}
