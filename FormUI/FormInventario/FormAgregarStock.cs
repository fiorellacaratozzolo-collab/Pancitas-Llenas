using DataAccess.Models;
using Logic.Facade;
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

namespace FormUI.FormInventario
{
    public partial class FormAgregarStock : Form
    {
        private readonly ProveedorService _proveedorService = new ProveedorService();
        private readonly ProductoService _productoService = new ProductoService();
        private readonly InventarioService _inventarioService = new InventarioService();
        private readonly Guid ID_SUCURSAL_ACTUAL;

        /// <summary>
        /// Inicializa el formulario, valida la sesión activa de la sucursal y carga los datos iniciales.
        /// </summary>
        public FormAgregarStock()
        {
            if (SessionManager.Current.IdSucursalActual == null)
            {
                MessageBox.Show("Error crítico: No se detectó una sucursal logueada.".Traducir(), "Error de Sesión".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Enabled = false;
                return;
            }

            ID_SUCURSAL_ACTUAL = SessionManager.Current.IdSucursalActual.Value;
            InitializeComponent();
            CargarProveedores();
            CargarProductosEnDGV(null);
        }
        /// <summary>
        /// Evento de carga inicial que previene la autogeneración de columnas en la grilla y aplica las traducciones de UI.
        /// </summary>
        private void FormAgregarStock_Load(object sender, EventArgs e)
        {
            dgvAgregarStock.AutoGenerateColumns = false;
            TraductorUI.TraducirFormulario(this);
        }
        /// <summary>
        /// Define manualmente la estructura, comportamiento, formato y traducciones de las columnas de la grilla de stock.
        /// </summary>
        private void ConfigurarDGV()
        {
            dgvAgregarStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAgregarStock.Columns.Clear();

            if (dgvAgregarStock.Columns.Contains("IdProducto"))
                dgvAgregarStock.Columns["IdProducto"].Visible = false;

            var colId = new DataGridViewTextBoxColumn
            {
                Name = "IdProducto",
                HeaderText = "IdProducto".Traducir(),
                DataPropertyName = "IdProducto",
                Visible = false,
                ReadOnly = true
            };
            dgvAgregarStock.Columns.Add(colId);

            var colProducto = new DataGridViewTextBoxColumn
            {
                Name = "NombreProducto",
                HeaderText = "Producto".Traducir(),
                DataPropertyName = "NombreProducto",
                ReadOnly = true
            };
            dgvAgregarStock.Columns.Add(colProducto);

            dgvAgregarStock.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PesoNeto",
                HeaderText = "Peso Neto".Traducir(),
                DataPropertyName = "PesoNeto",
                ReadOnly = true,
                DefaultCellStyle = {
                    Format = "N2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                },
                Width = 90
            });

            var colMarca = new DataGridViewTextBoxColumn
            {
                Name = "Marca",
                HeaderText = "Marca".Traducir(),
                DataPropertyName = "Marca",
                ReadOnly = true
            };
            dgvAgregarStock.Columns.Add(colMarca);

            var colProveedor = new DataGridViewTextBoxColumn
            {
                Name = "NombreProveedor",
                HeaderText = "Proveedor".Traducir(),
                DataPropertyName = "NombreProveedor",
                ReadOnly = true
            };
            dgvAgregarStock.Columns.Add(colProveedor);

            var colActual = new DataGridViewTextBoxColumn
            {
                Name = "StockActual",
                HeaderText = "Stock Actual".Traducir(),
                DataPropertyName = "StockActual",
                ReadOnly = true,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            };
            dgvAgregarStock.Columns.Add(colActual);

            var colDeseado = new DataGridViewTextBoxColumn
            {
                Name = "StockDeseado",
                HeaderText = "Stock Deseado".Traducir(),
                DataPropertyName = "StockDeseado",
                ReadOnly = true,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            };
            dgvAgregarStock.Columns.Add(colDeseado);

            var colAgregar = new DataGridViewTextBoxColumn
            {
                Name = "StockAAgregar",
                HeaderText = "Stock a Añadir".Traducir(),
                DataPropertyName = "StockAAgregar",
                ReadOnly = false,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            };
            dgvAgregarStock.Columns.Add(colAgregar);

            dgvAgregarStock.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvAgregarStock.ReadOnly = false;
        }
        /// <summary>
        /// Obtiene la lista de proveedores registrados y los asigna al menú desplegable para su uso como filtro.
        /// </summary>
        private void CargarProveedores()
        {
            try
            {
                List<ProveedorDTO> proveedores = _proveedorService.GetAllProveedores();
                proveedores.Insert(0, new ProveedorDTO { IdProveedor = Guid.Empty, NombreProveedor = "--- Mostrar Todos ---".Traducir() });

                cmbProveedor.DataSource = proveedores;
                cmbProveedor.DisplayMember = "NombreProveedor";
                cmbProveedor.ValueMember = "IdProveedor";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: ".Traducir() + ex.Message, "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Consulta la base de datos para obtener los productos y sus existencias actuales, aplicando opcionalmente un filtro por proveedor.
        /// </summary>
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
                        Marca = p.Marca,
                        NombreProveedor = vinculo?.IdProveedorNavigation?.NombreProveedor ?? "N/A",
                        StockActual = stock?.StockActual ?? 0,
                        StockDeseado = stock?.StockDeseado ?? 0
                    };
                }).ToList();

                ConfigurarDGV();
                dgvAgregarStock.DataSource = dataSource;

                ResaltarStockBajo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: ".Traducir() + ex.Message, "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Evalúa fila por fila y aplica un formato visual de alerta a los productos cuyo stock actual es menor al deseado.
        /// </summary>
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
                        row.Cells["StockAAgregar"].ToolTipText = "¡Stock por debajo del deseado!".Traducir();
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }
        /// <summary>
        /// Recolecta las cantidades a añadir de la grilla, solicita confirmación y procesa la actualización masiva de stock en la base de datos.
        /// </summary>
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
                MessageBox.Show("No se detectaron cantidades positivas para agregar stock.".Traducir(), "Advertencia".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacion = MessageBox.Show(
                string.Format("Se actualizarán {0} producto(s). ¿Continuar?".Traducir(), cambiosPendientes),
                "Confirmar actualización".Traducir(),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes) return;

            Guid? idProv = cmbProveedor.SelectedValue is Guid id && id != Guid.Empty ? id : (Guid?)null;

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
                        _inventarioService.AgregarStock(ID_SUCURSAL_ACTUAL, idProducto, cantidadAAgregar, stockDeseado, idProv);

                        cambiosAplicados++;
                        row.Cells["StockAAgregar"].Value = null;
                        row.Cells["StockAAgregar"].ErrorText = "";
                    }
                    catch (Exception ex)
                    {
                        errores++;
                        row.Cells["StockAAgregar"].ErrorText = "Error: ".Traducir() + ex.Message;
                    }
                }
            }

            string mensaje = "Stock actualizado.\n".Traducir() +
                             string.Format("Cambios aplicados: {0}\n".Traducir(), cambiosAplicados) +
                             (errores > 0 ? string.Format("Errores: {0}\n".Traducir(), errores) : "") +
                             "La tabla se ha actualizado.".Traducir();

            MessageBox.Show(mensaje, "Resultado".Traducir(), MessageBoxButtons.OK,
                errores > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

            CargarProductosEnDGV(cmbProveedor.SelectedValue is Guid idCombo ? (Guid?)idCombo : null);
        }
        /// <summary>
        /// Captura el evento de cambio en la lista desplegable de proveedores y filtra la grilla de productos en consecuencia.
        /// </summary>
        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedValue is Guid idProveedor)
            {
                CargarProductosEnDGV(idProveedor);
            }
        }
    }
}

