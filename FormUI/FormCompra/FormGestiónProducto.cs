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

        /// <summary>
        /// Inicializa el formulario, instancia los servicios necesarios y ejecuta la carga inicial de productos.
        /// </summary>
        public FormGestiónProducto()
        {
            InitializeComponent();
            CargarDatosProductos();
        }

        /// <summary>
        /// Obtiene y muestra los productos en la grilla principal. Permite recibir una lista filtrada o consultar todos los registros por defecto.
        /// </summary>
        private void CargarDatosProductos(List<ProductoDTO>? productos = null)
        {
            try
            {
                productos ??= _productoService.GetAllProductos();

                // Forzamos el refresco visual de la grilla vaciándola primero (Solución Punto 4)
                dgvProducto.DataSource = null;
                dgvProducto.DataSource = productos;

                ConfigurarColumnasDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar los productos: {0}".Traducir(), ex.Message), "Error de Conexión".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Oculta columnas técnicas, colecciones de navegación y aplica traducciones a los encabezados visibles de la grilla de productos.
        /// </summary>
        private void ConfigurarColumnasDataGridView()
        {
            dgvProducto.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            if (dgvProducto.DataSource == null) return;

            string[] columnasOcultas = { "IdProducto", "ProveedorProductos", "InventarioProductos",
                                         "SolicitudDePedidoDetalles", "SolicitudDeTraspasoDeProductosDetalles",
                                         "NombreConPeso", "StockPorSucursals", "VentaDetalles" };

            foreach (var col in columnasOcultas)
            {
                if (dgvProducto.Columns.Contains(col))
                    dgvProducto.Columns[col].Visible = false;
            }

            if (dgvProducto.Columns.Contains("NombreProducto"))
                dgvProducto.Columns["NombreProducto"].HeaderText = "Producto".Traducir();
            if (dgvProducto.Columns.Contains("PesoNeto"))
                dgvProducto.Columns["PesoNeto"].HeaderText = "Peso Neto".Traducir();
            if (dgvProducto.Columns.Contains("PrecioNeto"))
                dgvProducto.Columns["PrecioNeto"].HeaderText = "Precio Neto ($)".Traducir();
        }

        /// <summary>
        /// Evento de carga del formulario que aplica las traducciones de interfaz al idioma seleccionado.
        /// </summary>
        private void FormGestiónProducto_Load(object sender, EventArgs e)
        {
            TraductorUI.TraducirFormulario(this);
        }

        /// <summary>
        /// Valida la información ingresada, busca al proveedor correspondiente por CUIT y registra un nuevo producto en la base de datos.
        /// </summary>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtbNombreProd.Text) || string.IsNullOrWhiteSpace(txtbMarca.Text) || string.IsNullOrWhiteSpace(txtbProveedor.Text))
            {
                MessageBox.Show("Nombre, Marca y CUIT del Proveedor son obligatorios.".Traducir(), "Error de Validación".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtbPesoNeto.Text, out decimal pesoNetoValue) || !decimal.TryParse(txtbPrecioNeto.Text, out decimal precioNetoValue))
            {
                MessageBox.Show("Peso Neto y Precio Neto deben ser valores numéricos válidos.".Traducir(), "Error de Validación".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtbProveedor.Text, out int cuitValue))
            {
                MessageBox.Show("El CUIT del Proveedor debe ser un número entero válido.".Traducir(), "Error de Validación".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ProveedorDTO? proveedorEncontradoDTO = null;

            try
            {
                proveedorEncontradoDTO = _proveedorService.GetByCuit(cuitValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al buscar el proveedor: {0}".Traducir(), ex.Message), "Error de Búsqueda".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (proveedorEncontradoDTO == null)
            {
                MessageBox.Show(string.Format("No se encontró un proveedor con el CUIT {0}. Verifique los datos.".Traducir(), cuitValue), "Proveedor No Encontrado".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Guid idProveedor = proveedorEncontradoDTO.IdProveedor;

            var nuevoProductoDTO = new ProductoDTO
            {
                NombreProducto = txtbNombreProd.Text.Trim(),
                Marca = txtbMarca.Text.Trim(),
                PesoNeto = pesoNetoValue,
                Unidad = txtbUnidad.Text.Trim(),
                PrecioNeto = precioNetoValue,
                Descripcion = txtbDescripcion.Text.Trim()
            };

            try
            {
                Guid newProdId = _productoService.CrearProductoConProveedor(nuevoProductoDTO, idProveedor);

                MessageBox.Show(string.Format("Producto '{0}' agregado y vinculado al proveedor {1}.".Traducir(), nuevoProductoDTO.NombreProducto, proveedorEncontradoDTO.NombreProveedor), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarControles();
                CargarDatosProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al agregar el producto: {0}".Traducir(), ex.Message), "Error de Persistencia".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Restablece la grilla mostrando la totalidad de los productos almacenados en el sistema.
        /// </summary>
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarDatosProductos();
            MessageBox.Show("Lista de productos actualizada.".Traducir(), "Información".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Solicita un CUIT mediante una ventana emergente y filtra la grilla para mostrar únicamente los productos vinculados a dicho proveedor.
        /// </summary>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el CUIT del Proveedor para filtrar:".Traducir(), "Buscar Productos por Proveedor".Traducir(), "");

            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            if (!int.TryParse(input, out int cuitBusqueda))
            {
                MessageBox.Show("Debe ingresar un CUIT numérico válido.".Traducir(), "Error de Entrada".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                ProveedorDTO? proveedorEncontradoDTO = _proveedorService.GetByCuit(cuitBusqueda);

                if (proveedorEncontradoDTO == null)
                {
                    MessageBox.Show(string.Format("No se encontró ningún proveedor con el CUIT {0}.".Traducir(), cuitBusqueda), "Proveedor No Encontrado".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Guid idProveedorBusqueda = proveedorEncontradoDTO.IdProveedor;

                List<ProductoDTO> listaFiltrada = _productoService.GetByProveedor(idProveedorBusqueda);

                CargarDatosProductos(listaFiltrada);

                MessageBox.Show(string.Format("Se encontraron {0} productos del proveedor {1}.".Traducir(), listaFiltrada.Count, proveedorEncontradoDTO.NombreProveedor), "Búsqueda Exitosa".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error durante la búsqueda: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Solicita confirmación al usuario y deshabilita (baja física controlada) el producto actualmente seleccionado en la grilla.
        /// </summary>
        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (dgvProducto.CurrentRow != null)
            {
                Guid productoId = (Guid)(dgvProducto.CurrentRow.Cells["IdProducto"].Value ?? Guid.Empty);
                string nombre = dgvProducto.CurrentRow.Cells["NombreProducto"].Value?.ToString() ?? "[Nombre Desconocido]".Traducir();

                if (productoId == Guid.Empty)
                {
                    MessageBox.Show("Error: No se pudo obtener el ID del producto seleccionado.".Traducir(), "Error de Datos".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult dialogResult = MessageBox.Show(string.Format("¿Está seguro de eliminar el producto {0}? Esta acción borrará sus vínculos con proveedores.".Traducir(), nombre), "Confirmar Acción".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        _productoService.DeshabilitarProducto(productoId);
                        CargarDatosProductos();
                        MessageBox.Show("Producto eliminado exitosamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        string errorReal = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                        string mensajeAmigable = string.Format("No se puede eliminar el producto porque ya posee historial asociado (Ventas, Pedidos, etc.) que no puede ser borrado.\n\nDetalle técnico: {0}", errorReal);

                        MessageBox.Show(mensajeAmigable.Traducir(), "Restricción de Integridad".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila para eliminar.".Traducir(), "Advertencia".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Vacía todos los campos de texto del formulario dejándolos listos para el ingreso de un nuevo registro.
        /// </summary>
        private void LimpiarControles()
        {
            txtbNombreProd.Text = string.Empty;
            txtbMarca.Text = string.Empty;
            txtbPesoNeto.Text = string.Empty;
            txtbUnidad.Text = string.Empty;
            txtbPrecioNeto.Text = string.Empty;
            txtbDescripcion.Text = string.Empty;
            txtbProveedor.Text = string.Empty;
            txtbNombreProd.Focus();
        }
    }
}
