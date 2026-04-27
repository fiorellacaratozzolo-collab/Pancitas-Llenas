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

namespace FormUI.FormVenta
{
    public partial class FormGenerarVenta : Form
    {
        private ProductoDTO _productoSeleccionado = null;
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
                    MessageBox.Show("Error crítico: No hay una sucursal activa en la sesión.", "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Error al inicializar la pantalla: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                txtbPesoNeto.Clear(); // Limpiamos si no hay nada seleccionado
            }
        }

        private void btnBuscarProd_Click(object sender, EventArgs e)
        {
            // 1. Solicitar el nombre del producto al usuario
            string input = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nombre (o parte del nombre) del producto a buscar:", "Buscar Producto", "");

            if (string.IsNullOrWhiteSpace(input))
            {
                return; // Usuario canceló o no ingresó nada
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
                    MessageBox.Show($"No se encontró ningún producto que contenga '{input}'.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 4. ¡LA MAGIA! Reemplazamos la lista del ComboBox con la lista filtrada
                cmbProducto.DataSource = null; // Limpiamos la anterior
                cmbProducto.DataSource = listaFiltrada;
                cmbProducto.DisplayMember = "NombreProducto";
                cmbProducto.ValueMember = "IdProducto";
                cmbProducto.SelectedIndex = -1; // Lo dejamos vacío para que el usuario elija

                // 5. Desplegamos el ComboBox automáticamente para que vea los resultados
                cmbProducto.DroppedDown = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error durante la búsqueda: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un producto primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtbCantidadProd.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida mayor a cero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var productoSeleccionado = (ProductoDTO)cmbProducto.SelectedItem;

            // Extraemos el valor de forma segura (si es null, lo tomamos como 0)
            // El (decimal) asegura la conversión si en tu BD es de tipo float/double
            decimal precioSeguro = (decimal)(productoSeleccionado.PrecioNeto ?? 0);
            decimal pesoSeguro = (decimal)(productoSeleccionado.PesoNeto ?? 0);
            string unidadSegura = string.IsNullOrWhiteSpace(productoSeleccionado.Unidad) ? "Unidad" : productoSeleccionado.Unidad;
            // Creamos el renglón
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

            // Lo agregamos a la lista observable (La grilla se actualiza sola)
            _carrito.Add(nuevoDetalle);

            // Limpiamos los controles para el siguiente producto
            cmbProducto.SelectedIndex = -1;
            txtbCantidadProd.Clear();
            cmbProducto.Focus(); // Volvemos al combo para que siga tipeando

            // Recalculamos la plata
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

            // Mostramos en los labels (Ajusta los nombres a tus labels reales de la UI)
            lblSubtotal.Text = $"Subtotal: $ {subtotalVenta:N2}";
            lblDescuento.Text = $"Descuento: $ {descuento:N2}";
            lblTotal.Text = $"TOTAL: $ {totalFinal:N2}";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            if (_carrito.Count == 0)
            {
                MessageBox.Show("El carrito está vacío. Agregue productos antes de cobrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCliente.SelectedIndex == -1 || cmbCliente.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un Cliente válido de la lista antes de registrar la venta.", "Cliente Faltante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCliente.Focus();
                return;
            }

            try
            {
                // 2. RECOPILAR DATOS DE LA VENTA (La cabecera)
                decimal subtotal = _carrito.Sum(item => item.Subtotal);
                decimal descuento = chkMayorista.Checked ? subtotal * 0.35m : 0;

                VentumDTO nuevaVenta = new VentumDTO
                {
                    IdCliente = (Guid)cmbCliente.SelectedValue,
                    FechaVenta = DateTime.Now,
                    MetodoPago = cmbPago.SelectedItem.ToString(),
                    EsMayorista = chkMayorista.Checked,
                    MontoDescuento = descuento,
                    Total = subtotal - descuento,
                    IdSucursal = SessionManager.Current.IdSucursalActual.Value
                };

                // 3. PREPARAR EL DETALLE
                // Tu BLL espera una List, así que convertimos la BindingList
                List<VentaDetalleDTO> listaDetalles = _carrito.ToList();

                // 4. OBTENER SUCURSAL ACTUAL (Para descontar el stock ahí)
                Guid idSucursalActual = SessionManager.Current.IdSucursalActual.Value;

                // 5. ¡MANDAR A GUARDAR! 
                // Llamamos a tu servicio (Facade) que ejecuta la transacción atómica
                Logic.Facade.VentaService ventaService = new Logic.Facade.VentaService();
                Guid idVentaGenerada = ventaService.RegistrarVenta(nuevaVenta, listaDetalles, idSucursalActual);

                // 6. ÉXITO
                MessageBox.Show($"¡Venta registrada con éxito!\nSe descontó el stock correctamente.\nN° de Ticket: {idVentaGenerada}", "Venta Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 7. LIMPIEZA
                // Usamos el método que creamos para el botón Cancelar y dejamos todo en blanco
                LimpiarPantalla();
            }
            catch (Exception ex)
            {
                // Si no hay stock suficiente, el UnitOfWork tira tu mensaje de error y cae aquí:
                MessageBox.Show(ex.Message, "Error al procesar la venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Validamos si realmente hay algo que valga la pena cancelar
            if (_carrito.Count == 0 && cmbCliente.SelectedIndex == -1 && txtbCantidadProd.Text == "")
            {
                return; // La pantalla ya está limpia, no molestamos al usuario
            }

            // Pedimos confirmación de seguridad
            DialogResult confirmacion = MessageBox.Show(
                "¿Está seguro que desea cancelar la venta en curso? Se vaciará el carrito.",
                "Confirmar Cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                LimpiarPantalla();
            }
        }
    }
}
