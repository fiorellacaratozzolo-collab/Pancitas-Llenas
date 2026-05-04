using Logic;
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

namespace FormUI.FormVenta
{
    public partial class FormGenerarVenta : Form
    {
        private ProductoDTO? _productoSeleccionado = null;
        private BindingList<VentaDetalleDTO> _carrito = new BindingList<VentaDetalleDTO>();

        private void ConfigurarColumnasGrilla()
        {
            // 1. Ocultamos TODAS las columnas técnicas y claves foráneas
            if (dgvVentas.Columns.Contains("IdVentaDetalle")) dgvVentas.Columns["IdVentaDetalle"].Visible = false;
            if (dgvVentas.Columns.Contains("IdVenta")) dgvVentas.Columns["IdVenta"].Visible = false;
            if (dgvVentas.Columns.Contains("IdProducto")) dgvVentas.Columns["IdProducto"].Visible = false;
            if (dgvVentas.Columns.Contains("IdProductoNavigation")) dgvVentas.Columns["IdProductoNavigation"].Visible = false;
            if (dgvVentas.Columns.Contains("IdVentaNavigation")) dgvVentas.Columns["IdVentaNavigation"].Visible = false;
            if (dgvVentas.Columns.Contains("Unidad")) dgvVentas.Columns["Unidad"].Visible = false; // Por tu imagen, veo que Unidad también sobra

            // 2. Renombramos las columnas visibles
            if (dgvVentas.Columns.Contains("Producto")) dgvVentas.Columns["Producto"].HeaderText = "Producto";
            if (dgvVentas.Columns.Contains("PesoNeto")) dgvVentas.Columns["PesoNeto"].HeaderText = "Peso Neto";
            if (dgvVentas.Columns.Contains("Cantidad")) dgvVentas.Columns["Cantidad"].HeaderText = "Cantidad";

            if (dgvVentas.Columns.Contains("PrecioUnitario"))
            {
                dgvVentas.Columns["PrecioUnitario"].HeaderText = "Precio Unit.";
                dgvVentas.Columns["PrecioUnitario"].DefaultCellStyle.Format = "N2";
            }

            if (dgvVentas.Columns.Contains("Subtotal"))
            {
                dgvVentas.Columns["Subtotal"].HeaderText = "Subtotal";
                dgvVentas.Columns["Subtotal"].DefaultCellStyle.Format = "N2";
            }

            // 3. ORDENAMOS LAS COLUMNAS (DisplayIndex)
            if (dgvVentas.Columns.Contains("Producto")) dgvVentas.Columns["Producto"].DisplayIndex = 0;
            if (dgvVentas.Columns.Contains("PesoNeto")) dgvVentas.Columns["PesoNeto"].DisplayIndex = 1;
            if (dgvVentas.Columns.Contains("PrecioUnitario")) dgvVentas.Columns["PrecioUnitario"].DisplayIndex = 2;
            if (dgvVentas.Columns.Contains("Cantidad")) dgvVentas.Columns["Cantidad"].DisplayIndex = 3;
            if (dgvVentas.Columns.Contains("Subtotal")) dgvVentas.Columns["Subtotal"].DisplayIndex = 4;

            // 4. Ajuste visual para que ocupen todo el ancho
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public FormGenerarVenta()
        {
            InitializeComponent();
        }

        private void LimpiarPantalla()
        {
            _carrito = new BindingList<VentaDetalleDTO>();
            dgvVentas.DataSource = _carrito;
            CalcularTotal();

            cmbCliente.SelectedIndex = -1;
            if (cmbPago.Items.Count > 0) cmbPago.SelectedIndex = 0;
            chkMayorista.Checked = false;

            cmbProducto.SelectedIndex = -1;
            txtbPrecioProd.Clear();
            txtbPesoNeto.Clear();
            txtbCantidadProd.Clear();

            cmbCliente.Focus();
        }

        private void CargarProductosEnCombo()
        {
            Logic.Facade.ProductoService productoService = new Logic.Facade.ProductoService();
            cmbProducto.DataSource = productoService.GetAllProductos();
            cmbProducto.DisplayMember = "NombreConPeso";
            cmbProducto.ValueMember = "IdProducto";       // El ID oculto
            cmbProducto.SelectedIndex = -1; // Que arranque vacío
        }

        private void FormGenerarVenta_Load_1(object sender, EventArgs e)
        {
            try
            {
                // --- 1. CONFIGURACIÓN DEL CARRITO ---
                dgvVentas.DataSource = _carrito; // Enlazamos la grilla al carrito
                ConfigurarColumnasGrilla();
                CargarProductosEnCombo();

                // --- 2. CARGAMOS LOS CLIENTES EN EL COMBOBOX ---
                Logic.Facade.ClienteService clienteService = new Logic.Facade.ClienteService();

                // Traemos los clientes, pero filtramos y DESCARTAMOS los que tengan el nombre vacío
                var listaClientes = clienteService.GetAllClientes()
                                                  .Where(c => !string.IsNullOrWhiteSpace(c.NombreCliente))
                                                  .ToList();

                cmbCliente.DataSource = listaClientes;
                cmbCliente.DisplayMember = "NombreCliente";
                cmbCliente.ValueMember = "IdCliente";
                // Buscamos al "Consumidor Final" real en la lista que trajimos de la BD
                var consumidorFinal = listaClientes.FirstOrDefault(c =>
                !string.IsNullOrEmpty(c.NombreCliente) &&
                c.NombreCliente.ToLower().Contains("consumidor final"));

                if (consumidorFinal != null)
                {
                    // Si lo encuentra, lo dejamos seleccionado por defecto usando su ID real
                    cmbCliente.SelectedValue = consumidorFinal.IdCliente;
                }
                else
                {
                    // Si por alguna razón no existe, lo dejamos vacío
                    cmbCliente.SelectedIndex = -1;
                }

                // --- 3. CONFIGURAMOS MÉTODOS DE PAGO ---
                cmbPago.Items.Clear();
                cmbPago.Items.Add("Efectivo");
                cmbPago.Items.Add("Transferencia");
                cmbPago.Items.Add("Tarjeta de Débito");
                cmbPago.Items.Add("Tarjeta de Crédito");
                cmbPago.SelectedIndex = 0;

                // --- 4. VALIDACIÓN DE SUCURSAL Y MAYORISTA ---
                Guid? idSucursalActual = SessionManager.Current.IdSucursalActual;

                if (idSucursalActual == null)
                {
                    MessageBox.Show("Error crítico: No hay una sucursal activa en la sesión.".Traducir(), "Seguridad".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close(); return;
                }

                SucursalLogic sucursalBll = new SucursalLogic();
                var sucursal = sucursalBll.GetById(idSucursalActual.Value);

                if (sucursal != null)
                {
                    // 2 es el ID para "Venta-Deposito" (Mayorista)
                    if (sucursal.IdTipoSucursal == 2)
                    {
                        chkMayorista.Visible = true;
                    }
                    else
                    {
                        chkMayorista.Visible = false;
                        chkMayorista.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar la pantalla: ".Traducir() + ex.Message, "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            TraductorUI.TraducirFormulario(this);
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedItem is ProductoDTO productoElegido)
            {
                decimal precioSeguro = (decimal)(productoElegido.PrecioNeto ?? 0);
                txtbPrecioProd.Text = precioSeguro.ToString("N2");
                decimal pesoSeguro = (decimal)(productoElegido.PesoNeto ?? 0);
                txtbPesoNeto.Text = $"{pesoSeguro} kg";
            }
            else
            {
                txtbPrecioProd.Clear();
                txtbPesoNeto.Clear();
            }
        }

        private void btnBuscarProd_Click(object sender, EventArgs e)
        {
            // 1. Solicitar el nombre del producto al usuario
            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nombre (o parte del nombre) del producto a buscar:".Traducir(), "Buscar Producto".Traducir(), "");

            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }

            try
            {
                // 2. Traer todos los productos y filtrarlos en memoria (es ultra rápido)
                Logic.Facade.ProductoService productoService = new Logic.Facade.ProductoService();
                List<ProductoDTO> todosLosProductos = productoService.GetAllProductos();

                // Filtramos usando LINQ ignorando mayúsculas/minúsculas
                List<ProductoDTO> listaFiltrada = todosLosProductos
                    .Where(p => p.NombreProducto.ToLower().Contains(input.ToLower()))
                    .ToList();

                // 3. Validar si encontramos algo
                if (listaFiltrada.Count == 0)
                {
                    MessageBox.Show($"No se encontró ningún producto que contenga '{input}'.".Traducir(), "Sin resultados".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                cmbProducto.DataSource = null;
                cmbProducto.DataSource = listaFiltrada;
                cmbProducto.DisplayMember = "NombreProducto";
                cmbProducto.ValueMember = "IdProducto";
                cmbProducto.SelectedIndex = -1;
                cmbProducto.DroppedDown = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error durante la búsqueda: {ex.Message}".Traducir(), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un producto primero.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtbCantidadProd.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida mayor a cero.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var productoSeleccionado = (ProductoDTO)cmbProducto.SelectedItem;
            decimal precioSeguro = (decimal)(productoSeleccionado.PrecioNeto ?? 0);
            decimal pesoSeguro = (decimal)(productoSeleccionado.PesoNeto ?? 0);
            string unidadSegura = string.IsNullOrWhiteSpace(productoSeleccionado.Unidad) ? "Unidad" : productoSeleccionado.Unidad;
            VentaDetalleDTO nuevoDetalle = new VentaDetalleDTO
            {
                IdProducto = productoSeleccionado.IdProducto,
                NombreProducto = productoSeleccionado.NombreProducto,
                Cantidad = cantidad,
                PrecioUnitario = precioSeguro,
                Subtotal = cantidad * precioSeguro,
                PesoNeto = pesoSeguro,
                Unidad = unidadSegura
            };

            _carrito.Add(nuevoDetalle);
            cmbProducto.SelectedIndex = -1;
            txtbCantidadProd.Clear();
            cmbProducto.Focus(); 
            CalcularTotal();
        }

        private void btnDeshacer_Click(object sender, EventArgs e)
        {
            if (dgvVentas.CurrentRow != null)
            {
                var itemAQuitar = (VentaDetalleDTO)dgvVentas.CurrentRow.DataBoundItem;
                _carrito.Remove(itemAQuitar);
                CalcularTotal();
            }
        }

        private void chkMayorista_CheckedChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }

        private void CalcularTotal()
        {
            // Sumamos los subtotales de todo lo que hay en la grilla
            decimal subtotalVenta = _carrito.Sum(item => item.Subtotal);
            decimal descuento = 0;

            // Regla de Negocio: 35% de descuento si es Venta Mayorista
            if (chkMayorista.Checked)
            {
                descuento = subtotalVenta * 0.35m;
            }

            decimal totalFinal = subtotalVenta - descuento;
            lblSubtotal.Text = $"Subtotal: $ {subtotalVenta:N2}";
            lblDescuento.Text = $"Descuento: $ {descuento:N2}";
            lblTotal.Text = $"TOTAL: $ {totalFinal:N2}";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            // VALIDACIONES INICIALES
            if (_carrito.Count == 0)
            {
                MessageBox.Show("El carrito está vacío. Agregue productos antes de cobrar.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCliente.SelectedIndex == -1 || cmbCliente.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un Cliente válido de la lista antes de registrar la venta.".Traducir(), "Cliente Faltante".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCliente.Focus();
                return;
            }

            // VALIDACIÓN DE MÉTODO DE PAGO
            if (cmbPago.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un método de pago.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPago.Focus();
                return;
            }

            // VALIDACIÓN DE SUCURSAL
            if (!SessionManager.Current.IdSucursalActual.HasValue)
            {
                MessageBox.Show("No hay una sucursal seleccionada en la sesión actual para registrar la venta.".Traducir(), "Advertencia".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                decimal subtotal = _carrito.Sum(item => item.Subtotal);
                decimal descuento = chkMayorista.Checked ? subtotal * 0.35m : 0;
                Guid idSucursalActual = SessionManager.Current.IdSucursalActual.Value;
                string metodoPagoSeleccionado = cmbPago.SelectedItem.ToString() ?? "Efectivo";

                VentumDTO nuevaVenta = new VentumDTO
                {
                    IdCliente = (Guid)cmbCliente.SelectedValue,
                    FechaVenta = DateTime.Now,
                    MetodoPago = metodoPagoSeleccionado,
                    EsMayorista = chkMayorista.Checked,
                    MontoDescuento = descuento,
                    Total = subtotal - descuento,
                    IdSucursal = idSucursalActual
                };

                List<VentaDetalleDTO> listaDetalles = _carrito.ToList();

                Logic.Facade.VentaService ventaService = new Logic.Facade.VentaService();
                Guid idVentaGenerada = ventaService.RegistrarVenta(nuevaVenta, listaDetalles, idSucursalActual);

                MessageBox.Show($"¡Venta registrada con éxito!\nSe descontó el stock correctamente.\nN° de Ticket: {idVentaGenerada}".Traducir(), "Venta Exitosa".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarPantalla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Traducir(), "Error al procesar la venta".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }       

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (_carrito.Count == 0 && cmbCliente.SelectedIndex == -1 && txtbCantidadProd.Text == "")
            {
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro que desea cancelar la venta en curso? Se vaciará el carrito.".Traducir(),
                "Confirmar Cancelación".Traducir(),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                LimpiarPantalla();
            }
        }
    }
}
