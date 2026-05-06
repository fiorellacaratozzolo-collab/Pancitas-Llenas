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
        //private ProductoDTO? _productoSeleccionado = null;
        private BindingList<VentaDetalleDTO> _carrito = new BindingList<VentaDetalleDTO>();
       
        /// <summary>
        /// Inicializa el formulario y sus componentes visuales base.
        /// </summary>
        public FormGenerarVenta()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Oculta columnas técnicas e IDs, aplica formatos numéricos, traduce los encabezados y define el orden visual de la grilla del carrito.
        /// </summary>
        private void ConfigurarColumnasGrilla()
        {
            if (dgvVentas.Columns.Contains("IdVentaDetalle")) dgvVentas.Columns["IdVentaDetalle"].Visible = false;
            if (dgvVentas.Columns.Contains("IdVenta")) dgvVentas.Columns["IdVenta"].Visible = false;
            if (dgvVentas.Columns.Contains("IdProducto")) dgvVentas.Columns["IdProducto"].Visible = false;
            if (dgvVentas.Columns.Contains("IdProductoNavigation")) dgvVentas.Columns["IdProductoNavigation"].Visible = false;
            if (dgvVentas.Columns.Contains("IdVentaNavigation")) dgvVentas.Columns["IdVentaNavigation"].Visible = false;
            if (dgvVentas.Columns.Contains("Unidad")) dgvVentas.Columns["Unidad"].Visible = false;

            if (dgvVentas.Columns.Contains("Producto")) dgvVentas.Columns["Producto"].HeaderText = "Producto".Traducir();
            if (dgvVentas.Columns.Contains("PesoNeto")) dgvVentas.Columns["PesoNeto"].HeaderText = "Peso Neto".Traducir();
            if (dgvVentas.Columns.Contains("Cantidad")) dgvVentas.Columns["Cantidad"].HeaderText = "Cantidad".Traducir();

            if (dgvVentas.Columns.Contains("PrecioUnitario"))
            {
                dgvVentas.Columns["PrecioUnitario"].HeaderText = "Precio Unit.".Traducir();
                dgvVentas.Columns["PrecioUnitario"].DefaultCellStyle.Format = "N2";
            }

            if (dgvVentas.Columns.Contains("Subtotal"))
            {
                dgvVentas.Columns["Subtotal"].HeaderText = "Subtotal".Traducir();
                dgvVentas.Columns["Subtotal"].DefaultCellStyle.Format = "N2";
            }

            if (dgvVentas.Columns.Contains("Producto")) dgvVentas.Columns["Producto"].DisplayIndex = 0;
            if (dgvVentas.Columns.Contains("PesoNeto")) dgvVentas.Columns["PesoNeto"].DisplayIndex = 1;
            if (dgvVentas.Columns.Contains("PrecioUnitario")) dgvVentas.Columns["PrecioUnitario"].DisplayIndex = 2;
            if (dgvVentas.Columns.Contains("Cantidad")) dgvVentas.Columns["Cantidad"].DisplayIndex = 3;
            if (dgvVentas.Columns.Contains("Subtotal")) dgvVentas.Columns["Subtotal"].DisplayIndex = 4;

            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        /// <summary>
        /// Vacía el carrito de compras, restablece los totales, limpia los controles de entrada y devuelve el foco al selector de clientes.
        /// </summary>
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
        /// <summary>
        /// Obtiene el catálogo completo de productos y lo vincula al menú desplegable para su selección.
        /// </summary>
        private void CargarProductosEnCombo()
        {
            Logic.Facade.ProductoService productoService = new Logic.Facade.ProductoService();
            cmbProducto.DataSource = productoService.GetAllProductos();
            cmbProducto.DisplayMember = "NombreConPeso";
            cmbProducto.ValueMember = "IdProducto";
            cmbProducto.SelectedIndex = -1;
        }
        /// <summary>
        /// Evento de carga inicial que vincula el carrito a la grilla, carga catálogos, valida permisos de sucursal y aplica traducciones.
        /// </summary>
        private void FormGenerarVenta_Load_1(object sender, EventArgs e)
        {
            try
            {
                dgvVentas.DataSource = _carrito;
                ConfigurarColumnasGrilla();
                CargarProductosEnCombo();

                Logic.Facade.ClienteService clienteService = new Logic.Facade.ClienteService();

                var listaClientes = clienteService.GetAllClientes()
                                                  .Where(c => !string.IsNullOrWhiteSpace(c.NombreCliente))
                                                  .ToList();

                cmbCliente.DataSource = listaClientes;
                cmbCliente.DisplayMember = "NombreCliente";
                cmbCliente.ValueMember = "IdCliente";

                var consumidorFinal = listaClientes.FirstOrDefault(c =>
                !string.IsNullOrEmpty(c.NombreCliente) &&
                c.NombreCliente.ToLower().Contains("consumidor final"));

                if (consumidorFinal != null)
                {
                    cmbCliente.SelectedValue = consumidorFinal.IdCliente;
                }
                else
                {
                    cmbCliente.SelectedIndex = -1;
                }

                cmbPago.Items.Clear();
                cmbPago.Items.Add("Efectivo".Traducir());
                cmbPago.Items.Add("Transferencia".Traducir());
                cmbPago.Items.Add("Tarjeta de Débito".Traducir());
                cmbPago.Items.Add("Tarjeta de Crédito".Traducir());
                cmbPago.SelectedIndex = 0;

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
                MessageBox.Show(string.Format("Error al inicializar la pantalla: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            TraductorUI.TraducirFormulario(this);
        }
        /// <summary>
        /// Detecta la selección de un producto y autocompleta los campos visuales de precio y peso neto.
        /// </summary>
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
        /// <summary>
        /// Solicita al usuario un texto de búsqueda, filtra el catálogo en memoria y actualiza el menú desplegable con las coincidencias.
        /// </summary>
        private void btnBuscarProd_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nombre (o parte del nombre) del producto a buscar:".Traducir(), "Buscar Producto".Traducir(), "");

            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }

            try
            {
                Logic.Facade.ProductoService productoService = new Logic.Facade.ProductoService();
                List<ProductoDTO> todosLosProductos = productoService.GetAllProductos();

                List<ProductoDTO> listaFiltrada = todosLosProductos
                    .Where(p => p.NombreProducto.ToLower().Contains(input.ToLower()))
                    .ToList();

                if (listaFiltrada.Count == 0)
                {
                    MessageBox.Show(string.Format("No se encontró ningún producto que contenga '{0}'.".Traducir(), input), "Sin resultados".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show(string.Format("Error durante la búsqueda: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Valida la entrada del usuario, calcula los subtotales y añade el producto seleccionado como una nueva línea en el carrito.
        /// </summary>
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
            string unidadSegura = string.IsNullOrWhiteSpace(productoSeleccionado.Unidad) ? "Unidad".Traducir() : productoSeleccionado.Unidad;

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
        /// <summary>
        /// Elimina el producto seleccionado actualmente en la grilla del carrito y recalcula los totales de la venta.
        /// </summary>
        private void btnDeshacer_Click(object sender, EventArgs e)
        {
            if (dgvVentas.CurrentRow != null)
            {
                var itemAQuitar = (VentaDetalleDTO)dgvVentas.CurrentRow.DataBoundItem;
                _carrito.Remove(itemAQuitar);
                CalcularTotal();
            }
        }
        /// <summary>
        /// Detecta cambios en la opción de venta mayorista para recalcular inmediatamente los descuentos y el total final.
        /// </summary>
        private void chkMayorista_CheckedChanged(object sender, EventArgs e)
        {
            CalcularTotal();
        }
        /// <summary>
        /// Suma los importes del carrito, aplica descuentos de negocio y actualiza las etiquetas de totales en pantalla.
        /// </summary>
        private void CalcularTotal()
        {
            decimal subtotalVenta = _carrito.Sum(item => item.Subtotal);
            decimal descuento = 0;

            if (chkMayorista.Checked)
            {
                descuento = subtotalVenta * 0.35m;
            }

            decimal totalFinal = subtotalVenta - descuento;

            lblSubtotal.Text = string.Format("Subtotal: $ {0:N2}".Traducir(), subtotalVenta);
            lblDescuento.Text = string.Format("Descuento: $ {0:N2}".Traducir(), descuento);
            lblTotal.Text = string.Format("TOTAL: $ {0:N2}".Traducir(), totalFinal);
        }
        /// <summary>
        /// Realiza las validaciones finales de la venta, registra la operación en la base de datos y genera el ticket.
        /// </summary>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
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

            if (cmbPago.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un método de pago.".Traducir(), "Aviso".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPago.Focus();
                return;
            }

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
                string metodoPagoSeleccionado = cmbPago.SelectedItem.ToString() ?? "Efectivo".Traducir();

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

                MessageBox.Show(string.Format("¡Venta registrada con éxito!\nSe descontó el stock correctamente.\nN° de Ticket: {0}".Traducir(), idVentaGenerada), "Venta Exitosa".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarPantalla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.Traducir(), "Error al procesar la venta".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Solicita confirmación al usuario para anular la venta en curso y descartar todos los productos del carrito temporal.
        /// </summary>
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
