using DataAccess.Models;
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

namespace FormUI.FormInventario
{
    public partial class FormAgregarStock : Form
    {
        private readonly ProveedorService _proveedorService = new ProveedorService();
        private readonly ProductoService _productoService = new ProductoService();
        private readonly InventarioService _inventarioService = new InventarioService();

        // IMPORTANTE: ESTE ID DEBE VENIR DE LA SESIÓN DE USUARIO ACTUAL
        // Reemplaza con el ID de la sucursal donde se está operando.
        private readonly Guid ID_SUCURSAL_ACTUAL = new Guid("959819F3-9675-4FF9-A265-1CCC1883D951");

        public FormAgregarStock()
        {
            InitializeComponent();
            CargarProveedores();
            CargarProductosEnDGV(null);
        }

        private void FormAgregarStock_Load(object sender, EventArgs e)
        {

        }

        private void ConfigurarDGV()
        {
            // 1. Agregar la columna de entrada manual para el usuario
            if (dgvAgregarStock.Columns.Contains("StockAAgregar") == false)
            {
                DataGridViewTextBoxColumn txtStock = new DataGridViewTextBoxColumn();
                txtStock.HeaderText = "Stock a Añadir";
                txtStock.Name = "StockAAgregar";
                txtStock.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvAgregarStock.Columns.Add(txtStock);
            }

            // 2. Ocultar IDs y configurar encabezados
            // NOTA: Estas columnas DEBEN existir en el objeto anónimo (ViewModel) del dataSource.
            if (dgvAgregarStock.Columns.Contains("IdProducto"))
                dgvAgregarStock.Columns["IdProducto"].Visible = false;

            if (dgvAgregarStock.Columns.Contains("NombreProducto"))
                dgvAgregarStock.Columns["NombreProducto"].HeaderText = "Producto";

            if (dgvAgregarStock.Columns.Contains("NombreProveedor"))
                dgvAgregarStock.Columns["NombreProveedor"].HeaderText = "Proveedor";

            if (dgvAgregarStock.Columns.Contains("StockActual"))
                dgvAgregarStock.Columns["StockActual"].HeaderText = "Stock Actual";

            if (dgvAgregarStock.Columns.Contains("StockDeseado"))
                dgvAgregarStock.Columns["StockDeseado"].HeaderText = "Stock Deseado";

            // 3. Permitir edición solo en la columna de entrada
            dgvAgregarStock.ReadOnly = true;
            dgvAgregarStock.EditMode = DataGridViewEditMode.EditOnEnter;
            foreach (DataGridViewColumn col in dgvAgregarStock.Columns)
            {
                col.ReadOnly = (col.Name != "StockAAgregar");
            }
        }

        private void CargarProveedores()
        {
            try
            {
                List<ProveedorDTO> proveedores = _proveedorService.GetAllProveedores();

                // Opción para ver todos los productos
                proveedores.Insert(0, new ProveedorDTO { IdProveedor = Guid.Empty, NombreProveedor = "--- Mostrar Todos ---" });

                cmbProveedor.DataSource = proveedores;
                cmbProveedor.DisplayMember = "NombreProveedor";
                cmbProveedor.ValueMember = "IdProveedor";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message);
            }
        }

        private void CargarProductosEnDGV(Guid? idProveedor)
        {
            try
            {
                // 1. OBTENER PRODUCTOS (USANDO LA LÓGICA CORREGIDA PARA N:M)
                List<ProductoDTO> productos;
                if (idProveedor.HasValue && idProveedor.Value != Guid.Empty)
                {
                    // Usa el método que resuelve la relación N:M (GetProductosByProveedor)
                    productos = _productoService.GetProductosByProveedor(idProveedor.Value);
                }
                else
                {
                    // Carga todos los productos
                    productos = _productoService.GetAllProductos();
                }

                // 2. Obtener el stock actual de la sucursal
                List<StockPorSucursalDTO> stocks = _inventarioService.ObtenerStockPorSucursal(ID_SUCURSAL_ACTUAL);

                var todosLosVinculos = _productoService.GetTodosLosVinculosProveedorProducto();
                var dataSource = productos.Select(p =>
                {
                    var stock = stocks.FirstOrDefault(s => s.IdProducto == p.IdProducto);
                    var vinculo = todosLosVinculos.FirstOrDefault(pp => pp.IdProducto == p.IdProducto);

                    return new
                    {
                        IdProducto = p.IdProducto,
                        NombreProducto = p.NombreProducto,

                        NombreProveedor = vinculo?.IdProveedorNavigation?.NombreProveedor ?? "N/A",

                        StockActual = stock?.StockActual ?? 0,
                        StockDeseado = stock?.StockDeseado ?? 0,
                        StockAAgregar = null as string
                    };
                }).ToList();

                dgvAgregarStock.DataSource = dataSource;
                ConfigurarDGV();

                // Añadir columna StockAAgregar como editable
                if (!dgvAgregarStock.Columns.Contains("StockAAgregar"))
                {
                    DataGridViewTextBoxColumn txtStock = new DataGridViewTextBoxColumn();
                    txtStock.HeaderText = "Stock a Añadir";
                    txtStock.Name = "StockAAgregar";
                    txtStock.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvAgregarStock.Columns.Add(txtStock);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                int cambiosAplicados = 0;

                foreach (DataGridViewRow row in dgvAgregarStock.Rows)
                {
                    // Salta filas nuevas o sin datos
                    if (row.IsNewRow || row.Cells["IdProducto"].Value == null) continue;

                    // 1. Obtener la cantidad de la columna de entrada del usuario
                    object valorStockAAgregar = row.Cells["StockAAgregar"].Value;

                    // 2. Validar y parsear la entrada
                    if (valorStockAAgregar != null &&
                        int.TryParse(valorStockAAgregar.ToString(), out int cantidadAAgregar) &&
                        cantidadAAgregar > 0)
                    {
                        // Obtener IDs y Stock Deseado
                        Guid idProducto = (Guid)row.Cells["IdProducto"].Value;
                        int stockDeseado = 0;
                        int.TryParse(row.Cells["StockDeseado"].Value?.ToString(), out stockDeseado);

                        // 3. Llamar a la lógica de negocio para actualizar/crear el stock
                        _inventarioService.AgregarStock(ID_SUCURSAL_ACTUAL, idProducto, cantidadAAgregar, stockDeseado);

                        cambiosAplicados++;

                        // Limpiar la celda de entrada
                        row.Cells["StockAAgregar"].Value = null;
                    }
                }

                if (cambiosAplicados > 0)
                {
                    MessageBox.Show($"Stock actualizado exitosamente. Se aplicaron {cambiosAplicados} cambios.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // 4. Refrescar el DGV para ver el stock actualizado
                    CargarProductosEnDGV(cmbProveedor.SelectedValue is Guid idProveedor ? (Guid?)idProveedor : null);
                }
                else
                {
                    MessageBox.Show("No se detectaron cantidades positivas para agregar stock.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar stock: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Se llama al método de carga con el ID seleccionado del ComboBox.
            if (cmbProveedor.SelectedValue is Guid idProveedor)
            {
                CargarProductosEnDGV(idProveedor);
            }
        }
    }
}

