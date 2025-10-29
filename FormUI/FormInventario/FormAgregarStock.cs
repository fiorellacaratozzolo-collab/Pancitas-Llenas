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

        //ID de sucursal dinámico
        private readonly Guid ID_SUCURSAL_ACTUAL;

        public FormAgregarStock(Guid idSucursal)
        {
            ID_SUCURSAL_ACTUAL = idSucursal;
            InitializeComponent();
            CargarProveedores();
            CargarProductosEnDGV(null);
        }

        // Constructor por defecto
        public FormAgregarStock() : this(new Guid("959819F3-9675-4FF9-A265-1CCC1883D951")) { }

        private void FormAgregarStock_Load(object sender, EventArgs e)
        {
            // Asegurar que el DGV no genere columnas automáticas
            dgvAgregarStock.AutoGenerateColumns = false;
        }

        private void ConfigurarDGV()
        {
            dgvAgregarStock.Columns.Clear();

            // === COLUMNA: IdProducto (oculta) ===
            var colId = new DataGridViewTextBoxColumn
            {
                Name = "IdProducto",
                HeaderText = "IdProducto",
                DataPropertyName = "IdProducto",  // <-- MAPEAR
                Visible = false,
                ReadOnly = true
            };
            dgvAgregarStock.Columns.Add(colId);

            // === COLUMNA: Producto ===
            var colProducto = new DataGridViewTextBoxColumn
            {
                Name = "NombreProducto",
                HeaderText = "Producto",
                DataPropertyName = "NombreProducto",  // <-- MAPEAR
                ReadOnly = true
            };
            dgvAgregarStock.Columns.Add(colProducto);

            // === COLUMNA: Peso Neto ===
            dgvAgregarStock.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PesoNeto",
                HeaderText = "Peso Neto",
                DataPropertyName = "PesoNeto",
                ReadOnly = true,
                DefaultCellStyle = {
            Format = "N2",  // 2 decimales
            Alignment = DataGridViewContentAlignment.MiddleRight
        },
                Width = 90
            });

            // === COLUMNA: Proveedor ===
            var colProveedor = new DataGridViewTextBoxColumn
            {
                Name = "NombreProveedor",
                HeaderText = "Proveedor",
                DataPropertyName = "NombreProveedor",  // <-- MAPEAR
                ReadOnly = true
            };
            dgvAgregarStock.Columns.Add(colProveedor);

            // === COLUMNA: Stock Actual ===
            var colActual = new DataGridViewTextBoxColumn
            {
                Name = "StockActual",
                HeaderText = "Stock Actual",
                DataPropertyName = "StockActual",  // <-- MAPEAR
                ReadOnly = true,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            };
            dgvAgregarStock.Columns.Add(colActual);

            // === COLUMNA: Stock Deseado ===
            var colDeseado = new DataGridViewTextBoxColumn
            {
                Name = "StockDeseado",
                HeaderText = "Stock Deseado",
                DataPropertyName = "StockDeseado",  // <-- MAPEAR
                ReadOnly = true,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            };
            dgvAgregarStock.Columns.Add(colDeseado);

            // === COLUMNA: Stock a Añadir (editable) ===
            var colAgregar = new DataGridViewTextBoxColumn
            {
                Name = "StockAAgregar",
                HeaderText = "Stock a Añadir",
                DataPropertyName = "StockAAgregar",  // <-- Aunque no exista en dataSource, lo usamos para edición
                ReadOnly = false,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            };
            dgvAgregarStock.Columns.Add(colAgregar);

            dgvAgregarStock.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvAgregarStock.ReadOnly = false;
        }

        private void CargarProveedores()
        {
            try
            {
                List<ProveedorDTO> proveedores = _proveedorService.GetAllProveedores();
                proveedores.Insert(0, new ProveedorDTO { IdProveedor = Guid.Empty, NombreProveedor = "--- Mostrar Todos ---" });

                cmbProveedor.DataSource = proveedores;
                cmbProveedor.DisplayMember = "NombreProveedor";
                cmbProveedor.ValueMember = "IdProveedor";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProductosEnDGV(Guid? idProveedor)
        {
            try
            {
                List<ProductoDTO> productos = idProveedor.HasValue && idProveedor.Value != Guid.Empty
                    ? _productoService.GetProductosByProveedor(idProveedor.Value)
                    : _productoService.GetAllProductos();

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
                        PesoNeto = p.PesoNeto,
                        NombreProveedor = vinculo?.IdProveedorNavigation?.NombreProveedor ?? "N/A",
                        StockActual = stock?.StockActual ?? 0,
                        StockDeseado = stock?.StockDeseado ?? 0
                    };
                }).ToList();

                ConfigurarDGV();
                dgvAgregarStock.DataSource = dataSource;

                //Resaltar productos con stock bajo
                ResaltarStockBajo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Resaltar filas con stock actual < deseado
        private void ResaltarStockBajo()
        {
            foreach (DataGridViewRow row in dgvAgregarStock.Rows)
            {
                if (row.Cells["StockActual"].Value is int actual &&
                    row.Cells["StockDeseado"].Value is int deseado)
                {
                    if (actual < deseado)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row.Cells["StockAAgregar"].ToolTipText = "¡Stock por debajo del deseado!";
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            int cambiosPendientes = 0;
            foreach (DataGridViewRow row in dgvAgregarStock.Rows)
            {
                if (row.IsNewRow || row.Cells["IdProducto"].Value == null) continue;
                if (row.Cells["StockAAgregar"].Value != null &&
                    int.TryParse(row.Cells["StockAAgregar"].Value.ToString(), out int cant) && cant > 0)
                {
                    cambiosPendientes++;
                }
            }

            if (cambiosPendientes == 0)
            {
                MessageBox.Show("No se detectaron cantidades positivas para agregar stock.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmación antes de guardar
            var confirmacion = MessageBox.Show(
                $"Se actualizarán {cambiosPendientes} producto(s). ¿Continuar?",
                "Confirmar actualización",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes) return;

            int cambiosAplicados = 0;
            int errores = 0;

            foreach (DataGridViewRow row in dgvAgregarStock.Rows)
            {
                if (row.IsNewRow || row.Cells["IdProducto"].Value == null) continue;

                object valorStockAAgregar = row.Cells["StockAAgregar"].Value;

                if (valorStockAAgregar != null &&
                    int.TryParse(valorStockAAgregar.ToString(), out int cantidadAAgregar) &&
                    cantidadAAgregar > 0)
                {
                    try
                    {
                        Guid idProducto = (Guid)row.Cells["IdProducto"].Value;
                        int stockDeseado = 0;
                        int.TryParse(row.Cells["StockDeseado"].Value?.ToString(), out stockDeseado);

                        _inventarioService.AgregarStock(ID_SUCURSAL_ACTUAL, idProducto, cantidadAAgregar, stockDeseado);

                        cambiosAplicados++;
                        row.Cells["StockAAgregar"].Value = null;
                        row.Cells["StockAAgregar"].ErrorText = ""; // Limpiar error
                    }
                    catch (Exception ex)
                    {
                        errores++;
                        row.Cells["StockAAgregar"].ErrorText = "Error: " + ex.Message;
                    }
                }
            }

            // Reporte detallado de resultados
            string mensaje = $"Stock actualizado.\n" +
                            $"Cambios aplicados: {cambiosAplicados}\n" +
                            (errores > 0 ? $"Errores: {errores}\n" : "") +
                            "La tabla se ha actualizado.";

            MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK,
                errores > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

            CargarProductosEnDGV(cmbProveedor.SelectedValue is Guid id ? (Guid?)id : null);
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedValue is Guid idProveedor)
            {
                CargarProductosEnDGV(idProveedor);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

