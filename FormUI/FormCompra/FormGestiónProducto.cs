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
using Services.Facade.Extensions;

namespace FormUI.FormCompra
{
    public partial class FormGestiónProducto : Form
    {
        private readonly ProductoService _productoService = new ProductoService();
        private readonly ProveedorService _proveedorService = new ProveedorService();
        private Guid? _productoSeleccionadoId = null;
        private bool _viendoActivos = true;

        /// <summary>
        /// Inicializa el formulario, bloquea el campo de unidad por defecto en "Kg" y ejecuta la carga inicial.
        /// </summary>
        public FormGestiónProducto()
        {
            InitializeComponent();

            txtbUnidad.Text = "Kg";
            txtbUnidad.ReadOnly = true;

            dgvProducto.SelectionChanged += dgvProducto_SelectionChanged;

            CargarProveedores();
            ConfigurarEstadoInicial();
        }

        /// <summary>
        /// Establece la interfaz visual por defecto mostrando los productos activos.
        /// </summary>
        private void ConfigurarEstadoInicial()
        {
            _viendoActivos = true;
            btnHabilitar.Visible = false;
            btnDeshabilitar.Visible = true;
            btnVerDeshabilitados.Text = "Ver Deshabilitados".Traducir();

            CargarDatosProductos();
        }

        /// <summary>
        /// Carga la lista de proveedores en el ComboBox formateando el texto para mostrar Nombre y CUIT.
        /// </summary>
        private void CargarProveedores()
        {
            try
            {
                var proveedores = _proveedorService.GetAllProveedores();
                var listaCombo = proveedores.Select(p => new
                {
                    IdProveedor = p.IdProveedor,
                    DisplayInfo = string.Format("{0} (CUIT: {1})", p.NombreProveedor, p.Cuit)
                }).ToList();

                cmbProveedor.DataSource = listaCombo;
                cmbProveedor.DisplayMember = "DisplayInfo";
                cmbProveedor.ValueMember = "IdProveedor";
                cmbProveedor.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar proveedores: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Obtiene y muestra los productos filtrando por el estado actual de la vista (_viendoActivos).
        /// </summary>
        private void CargarDatosProductos(List<ProductoDTO>? productos = null)
        {
            try
            {
                if (productos == null)
                {
                    productos = _viendoActivos
                        ? _productoService.ObtenerActivos()
                        : _productoService.ObtenerDeshabilitados();
                }

                var vinculos = _productoService.GetTodosLosVinculosProveedorProducto();

                var dataSource = productos.Select(p =>
                {
                    var vinculoAsociado = vinculos.FirstOrDefault(v => v.IdProducto == p.IdProducto);
                    return new
                    {
                        IdProducto = p.IdProducto,
                        NombreProducto = p.NombreProducto,
                        Marca = p.Marca,
                        PesoNeto = p.PesoNeto,
                        Unidad = p.Unidad ?? "Kg",
                        PrecioNeto = p.PrecioNeto,
                        Descripcion = p.Descripcion,
                        IdProveedor = vinculoAsociado?.IdProveedor,
                        ProveedorAsociado = vinculoAsociado?.IdProveedorNavigation?.NombreProveedor ?? "Sin Proveedor".Traducir()
                    };
                }).ToList();

                dgvProducto.DataSource = null;
                dgvProducto.DataSource = dataSource;

                ConfigurarColumnasDataGridView();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar los productos: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Oculta columnas técnicas, aplica traducciones y bloquea la grilla de productos.
        /// </summary>
        private void ConfigurarColumnasDataGridView()
        {
            dgvProducto.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            if (dgvProducto.DataSource == null) return;

            dgvProducto.ReadOnly = true;
            dgvProducto.AllowUserToAddRows = false;
            dgvProducto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            string[] columnasOcultas = { "IdProducto", "IdProveedor", "ProveedorProductos", "InventarioProductos", "SolicitudDePedidoDetalles", "SolicitudDeTraspasoDeProductosDetalles", "NombreConPeso", "StockPorSucursals", "VentaDetalles", "Descripcion" };

            foreach (var col in columnasOcultas)
            {
                if (dgvProducto.Columns.Contains(col)) dgvProducto.Columns[col].Visible = false;
            }

            if (dgvProducto.Columns.Contains("NombreProducto")) dgvProducto.Columns["NombreProducto"].HeaderText = "Producto".Traducir();
            if (dgvProducto.Columns.Contains("PesoNeto")) dgvProducto.Columns["PesoNeto"].HeaderText = "Peso Neto".Traducir();
            if (dgvProducto.Columns.Contains("Unidad")) dgvProducto.Columns["Unidad"].HeaderText = "Unidad".Traducir();
            if (dgvProducto.Columns.Contains("PrecioNeto")) dgvProducto.Columns["PrecioNeto"].HeaderText = "Precio Neto ($)".Traducir();
            if (dgvProducto.Columns.Contains("ProveedorAsociado")) dgvProducto.Columns["ProveedorAsociado"].HeaderText = "Proveedor Asociado".Traducir();
        }

        /// <summary>
        /// Escucha el clic en la grilla y maneja la Máquina de Estados de los botones.
        /// </summary>
        private void dgvProducto_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvProducto.CurrentRow != null && dgvProducto.CurrentRow.Selected && dgvProducto.DataSource != null)
            {
                _productoSeleccionadoId = (Guid?)dgvProducto.CurrentRow.Cells["IdProducto"].Value;

                txtbNombreProd.Text = dgvProducto.CurrentRow.Cells["NombreProducto"].Value?.ToString();
                txtbMarca.Text = dgvProducto.CurrentRow.Cells["Marca"].Value?.ToString();
                txtbPesoNeto.Text = dgvProducto.CurrentRow.Cells["PesoNeto"].Value?.ToString();
                txtbPrecioNeto.Text = dgvProducto.CurrentRow.Cells["PrecioNeto"].Value?.ToString();
                txtbDescripcion.Text = dgvProducto.CurrentRow.Cells["Descripcion"].Value?.ToString();

                var idProv = dgvProducto.CurrentRow.Cells["IdProveedor"].Value as Guid?;
                if (idProv.HasValue && idProv.Value != Guid.Empty) cmbProveedor.SelectedValue = idProv.Value;
                else cmbProveedor.SelectedIndex = -1;

                // MÁQUINA DE ESTADOS
                btnAgregar.Enabled = false;
                btnActualizar.Enabled = true;
                btnDeshabilitar.Enabled = _viendoActivos;
                btnHabilitar.Enabled = !_viendoActivos;
            }
        }

        /// <summary>
        /// Evento de carga del formulario que aplica las traducciones de interfaz.
        /// </summary>
        private void FormGestiónProducto_Load(object sender, EventArgs e)
        {
            TraductorUI.TraducirFormulario(this);
            LimpiarControles();
        }

        /// <summary>
        /// Valida la información e inserta un nuevo producto en la base de datos.
        /// </summary>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;
            if (cmbProveedor.SelectedValue is not Guid idProveedor)
            {
                MessageBox.Show("Por favor, seleccione un proveedor válido de la lista.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nuevoProducto = CrearDTO();

            try
            {
                _productoService.CrearProductoConProveedor(nuevoProducto, idProveedor);
                MessageBox.Show("Producto agregado exitosamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatosProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Restablece la grilla mostrando la totalidad de los productos almacenados en el sistema y limpia la selección.
        /// </summary>
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (_productoSeleccionadoId == null)
            {
                MessageBox.Show("Seleccione un producto de la lista haciendo clic sobre él para modificarlo.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCampos()) return;
            if (cmbProveedor.SelectedValue is not Guid idProveedor)
            {
                MessageBox.Show("Por favor, seleccione un proveedor válido de la lista.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var productoActualizadoDTO = CrearDTO();
            productoActualizadoDTO.IdProducto = _productoSeleccionadoId.Value;

            try
            {
                _productoService.UpdateProducto(productoActualizadoDTO, idProveedor);
                MessageBox.Show("Los datos del producto han sido actualizados correctamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatosProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al actualizar el producto: {0}".Traducir(), ex.Message), "Error de Persistencia".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Busca productos de un proveedor específico respetando la vista actual (Activos/Inactivos).
        /// </summary>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un proveedor para buscar.".Traducir(), "Advertencia".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var lista = _productoService.GetProductosByProveedor((Guid)cmbProveedor.SelectedValue)
                                            .Where(p => p.Activo == _viendoActivos).ToList();

                CargarDatosProductos(lista);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error durante la búsqueda: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Solicita confirmación y deshabilita (Borrado Lógico) el producto seleccionado.
        /// </summary>
        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (_productoSeleccionadoId == null) return;

            if (MessageBox.Show("¿Está seguro de deshabilitar este producto?".Traducir(), "Confirmar".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _productoService.DeshabilitarProducto(_productoSeleccionadoId.Value);
                    CargarDatosProductos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al deshabilitar: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Vacía los campos y resetea los botones al estado inicial de seguridad.
        /// </summary>
        private void LimpiarControles()
        {
            _productoSeleccionadoId = null;
            txtbNombreProd.Text = string.Empty;
            txtbMarca.Text = string.Empty;
            txtbPesoNeto.Text = string.Empty;
            txtbPrecioNeto.Text = string.Empty;
            txtbDescripcion.Text = string.Empty;
            txtbUnidad.Text = "Kg";
            cmbProveedor.SelectedIndex = -1;

            if (dgvProducto.DataSource != null) dgvProducto.ClearSelection();
            btnAgregar.Enabled = _viendoActivos;
            btnActualizar.Enabled = false;
            btnDeshabilitar.Enabled = false;
            btnHabilitar.Enabled = false;

            txtbNombreProd.Focus();
        }

        /// <summary>
        /// Botón que restablece manualmente los campos para preparar un alta nueva.
        /// </summary>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
        }

        /// <summary>
        /// Solicita confirmación y vuelve a activar un producto deshabilitado.
        /// </summary>
        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            if (_productoSeleccionadoId == null) return;

            if (MessageBox.Show("¿Desea reactivar este producto y devolverlo al catálogo?".Traducir(), "Confirmar".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _productoService.HabilitarProducto(_productoSeleccionadoId.Value);
                    CargarDatosProductos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error al habilitar: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Alterna la vista de la grilla entre productos Activos e Inactivos.
        /// </summary>
        private void btnVerDeshabilitados_Click(object sender, EventArgs e)
        {
            _viendoActivos = !_viendoActivos;

            btnVerDeshabilitados.Text = _viendoActivos ? "Ver Deshabilitados".Traducir() : "Ver Activos".Traducir();
            btnHabilitar.Visible = !_viendoActivos;
            btnDeshabilitar.Visible = _viendoActivos;

            CargarDatosProductos();
        }

        /// <summary>
        /// Valida que los campos requeridos y formatos numéricos sean correctos.
        /// </summary>
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtbNombreProd.Text) || string.IsNullOrWhiteSpace(txtbMarca.Text))
            {
                MessageBox.Show("Nombre y Marca son obligatorios.".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!decimal.TryParse(txtbPesoNeto.Text, out _) || !decimal.TryParse(txtbPrecioNeto.Text, out _))
            {
                MessageBox.Show("Peso Neto y Precio Neto deben ser numéricos válidos.".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmbProveedor.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un proveedor de la lista.".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Genera un objeto ProductoDTO con los valores actuales del formulario.
        /// </summary>
        private ProductoDTO CrearDTO()
        {
            return new ProductoDTO
            {
                NombreProducto = txtbNombreProd.Text.Trim(),
                Marca = txtbMarca.Text.Trim(),
                PesoNeto = decimal.Parse(txtbPesoNeto.Text),
                Unidad = "Kg",
                PrecioNeto = decimal.Parse(txtbPrecioNeto.Text),
                Descripcion = txtbDescripcion.Text.Trim(),
                Activo = _viendoActivos
            };
        }
    }
}