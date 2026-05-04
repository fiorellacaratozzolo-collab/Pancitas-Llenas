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

        public FormGestiónProducto()
        {
            InitializeComponent();
            CargarDatosProductos();
        }

        private void CargarDatosProductos(List<ProductoDTO>? productos = null)
        {
            try
            {
                // Si la lista es null, obtenemos todos los productos (carga inicial/actualizar)
                productos ??= _productoService.GetAllProductos();

                dgvProducto.DataSource = productos;
                ConfigurarColumnasDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los productos: {ex.Message}".Traducir(), "Error de Conexión".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnasDataGridView()
        {
            // La asignación del DataSource debe ocurrir antes de este método.
            if (dgvProducto.DataSource == null) return;

            // Ocultar la clave primaria y las colecciones de navegación
            if (dgvProducto.Columns.Contains("IdProducto"))
                dgvProducto.Columns["IdProducto"].Visible = false;

            // Ocultar las colecciones grandes de Entity Framework
            if (dgvProducto.Columns.Contains("ProveedorProductos"))
                dgvProducto.Columns["ProveedorProductos"].Visible = false;
            if (dgvProducto.Columns.Contains("InventarioProductos"))
                dgvProducto.Columns["InventarioProductos"].Visible = false;
            if (dgvProducto.Columns.Contains("SolicitudDePedidoDetalles"))
                dgvProducto.Columns["SolicitudDePedidoDetalles"].Visible = false;
            if (dgvProducto.Columns.Contains("SolicitudDeTraspasoDeProductosDetalles"))
                dgvProducto.Columns["SolicitudDeTraspasoDeProductosDetalles"].Visible = false;
            if (dgvProducto.Columns.Contains("NombreConPeso"))
                dgvProducto.Columns["NombreConPeso"].Visible = false;
            if (dgvProducto.Columns.Contains("StockPorSucursals"))
                dgvProducto.Columns["StockPorSucursals"].Visible = false;
            if (dgvProducto.Columns.Contains("VentaDetalles"))
                dgvProducto.Columns["VentaDetalles"].Visible = false;

            // Renombrar columnas
            if (dgvProducto.Columns.Contains("NombreProducto"))
                dgvProducto.Columns["NombreProducto"].HeaderText = "Producto";
            if (dgvProducto.Columns.Contains("PesoNeto"))
                dgvProducto.Columns["PesoNeto"].HeaderText = "Peso Neto";
            if (dgvProducto.Columns.Contains("PrecioNeto"))
                dgvProducto.Columns["PrecioNeto"].HeaderText = "Precio Neto ($)";

            dgvProducto.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void FormGestiónProducto_Load(object sender, EventArgs e)
        {
            TraductorUI.TraducirFormulario(this);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // 1. Validación de entradas
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

            // Validar y obtener el CUIT del proveedor
            if (!int.TryParse(txtbProveedor.Text, out int cuitValue))
            {
                MessageBox.Show("El CUIT del Proveedor debe ser un número entero válido.".Traducir(), "Error de Validación".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Buscar el Proveedor por CUIT
            ProveedorDTO? proveedorEncontradoDTO = null;
            try
            {
                proveedorEncontradoDTO = _proveedorService.GetByCuit(cuitValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar el proveedor: {ex.Message}".Traducir(), "Error de Búsqueda".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (proveedorEncontradoDTO == null)
            {
                MessageBox.Show($"No se encontró un proveedor con el CUIT {cuitValue}. Verifique los datos.".Traducir(), "Proveedor No Encontrado".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // El ID del proveedor ya lo tenemos:
            Guid idProveedor = proveedorEncontradoDTO.IdProveedor;

            // 3. Creación del Objeto Modelo
            var nuevoProductoDTO = new ProductoDTO
            {
                NombreProducto = txtbNombreProd.Text.Trim(),
                Marca = txtbMarca.Text.Trim(),
                PesoNeto = pesoNetoValue,
                Unidad = txtbUnidad.Text.Trim(),
                PrecioNeto = precioNetoValue,
                Descripcion = txtbDescripcion.Text.Trim()
            };

            // 4. Llamada al Servicio (Crea Producto y la relación ProveedorProducto)
            try
            {
                Guid newProdId = _productoService.CrearProductoConProveedor(nuevoProductoDTO, idProveedor);

                MessageBox.Show($"Producto '{nuevoProductoDTO.NombreProducto}' agregado y vinculado al proveedor {proveedorEncontradoDTO.NombreProveedor}.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarControles();
                CargarDatosProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el producto: {ex.Message}".Traducir(), "Error de Persistencia".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarDatosProductos(); // Carga todos los productos
            MessageBox.Show("Lista de productos actualizada.".Traducir(), "Información".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // 1. Solicitar el CUIT al usuario
            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el CUIT del Proveedor para filtrar:".Traducir(), "Buscar Productos por Proveedor".Traducir(), "");

            if (string.IsNullOrEmpty(input))
            {
                return; // Usuario canceló o no ingresó nada
            }

            // 2. Validar que el input sea un número (CUIT)
            if (!int.TryParse(input, out int cuitBusqueda))
            {
                MessageBox.Show("Debe ingresar un CUIT numérico válido.".Traducir(), "Error de Entrada".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 3. Buscar el Proveedor por CUIT para obtener su ID (GUID)
                ProveedorDTO? proveedorEncontradoDTO = _proveedorService.GetByCuit(cuitBusqueda);

                if (proveedorEncontradoDTO == null)
                {
                    MessageBox.Show($"No se encontró ningún proveedor con el CUIT {cuitBusqueda}.".Traducir(), "Proveedor No Encontrado".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // El ID del proveedor ya lo tenemos
                Guid idProveedorBusqueda = proveedorEncontradoDTO.IdProveedor;

                // 4. Obtener lista filtrada por el ID del Proveedor
                List<ProductoDTO> listaFiltrada = _productoService.GetByProveedor(idProveedorBusqueda);

                // 5. Reemplazar la fuente de datos
                CargarDatosProductos(listaFiltrada);

                MessageBox.Show($"Se encontraron {listaFiltrada.Count} productos del proveedor {proveedorEncontradoDTO.NombreProveedor}.".Traducir(), "Búsqueda Exitosa".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error durante la búsqueda: {ex.Message}".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            if (dgvProducto.CurrentRow != null)
            {
                // Se obtiene el GUID de la columna IdProducto (aunque esté oculta)
                Guid productoId = (Guid)(dgvProducto.CurrentRow.Cells["IdProducto"].Value ?? Guid.Empty);
                string nombre = dgvProducto.CurrentRow.Cells["NombreProducto"].Value?.ToString() ?? "[Nombre Desconocido]";

                if (productoId == Guid.Empty)
                {
                    MessageBox.Show("Error: No se pudo obtener el ID del producto seleccionado.".Traducir(), "Error de Datos".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult dialogResult = MessageBox.Show($"¿Está seguro de deshabilitar el producto {nombre}?".Traducir(), "Confirmar Acción".Traducir(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        _productoService.DeshabilitarProducto(productoId);
                        CargarDatosProductos(); // Recargar para reflejar el cambio
                        MessageBox.Show("Producto deshabilitado exitosamente.".Traducir(), "Éxito".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al deshabilitar el producto: {ex.Message}".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila para deshabilitar.".Traducir(), "Advertencia".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

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
