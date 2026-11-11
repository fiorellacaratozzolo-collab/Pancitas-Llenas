using Logic.Facade;
using ModelsDTO;
using Services.Facade.Extensions;
using Services.Facade;
using Services.Bll.CustomExceptions;

namespace FormUI.FormInventario
{
    public partial class FormSolicitarOP : Form
    {
        private readonly ProductoService _productoService = new ProductoService();
        private readonly SolicitudDePedidoService _solicitudService = new SolicitudDePedidoService();
        private readonly List<SolicitudDePedidoDetalleDTO> _detalles = new List<SolicitudDePedidoDetalleDTO>();

        /// <summary>
        /// Inicializa el formulario, sus componentes visuales y configura la fecha y grilla por defecto.
        /// </summary>
        public FormSolicitarOP()
        {
            InitializeComponent();
            ConfigurarFecha();
            ConfigurarDGV();
        }

        /// <summary>
        /// Asigna la fecha actual al selector de fechas y lo bloquea para evitar modificaciones.
        /// </summary>
        private void ConfigurarFecha()
        {
            dtpFecha.Value = DateTime.Today;
            dtpFecha.Enabled = false;
        }

        /// <summary>
        /// Define y estructura las columnas de la grilla de productos solicitados, aplicando traducciones y formatos.
        /// </summary>
        private void ConfigurarDGV()
        {
            dgvSolicitarOP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSolicitarOP.AutoGenerateColumns = false;
            dgvSolicitarOP.Columns.Clear();

            dgvSolicitarOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreProducto",
                HeaderText = "Producto",
                DataPropertyName = "NombreProducto",
                ReadOnly = true,
                Width = 180
            });

            dgvSolicitarOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Marca",
                HeaderText = "Marca",
                DataPropertyName = "Marca",
                ReadOnly = true,
                Width = 100
            });

            dgvSolicitarOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PesoNeto",
                HeaderText = "Peso Neto",
                DataPropertyName = "PesoNeto",
                ReadOnly = true,
                Width = 100,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvSolicitarOP.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                HeaderText = "Cantidad",
                DataPropertyName = "Cantidad",
                ReadOnly = false,
                Width = 90,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            var colEliminar = new DataGridViewButtonColumn
            {
                Name = "Eliminar",
                HeaderText = "",
                Text = "X",
                UseColumnTextForButtonValue = true,
                Width = 40
            };
            dgvSolicitarOP.Columns.Add(colEliminar);
        }

        /// <summary>
        /// Carga la lista de productos ACTIVOS disponibles en el menú desplegable.
        /// </summary>
        private void CargarProductos()
        {
            cmbProducto.DataSource = _productoService.ObtenerActivos();
            cmbProducto.DisplayMember = "NombreConPeso";
            cmbProducto.ValueMember = "IdProducto";
            cmbProducto.SelectedIndex = -1;
        }

        /// <summary>
        /// Refresca la grilla para mostrar los elementos actualmente cargados en la lista de detalles temporal.
        /// </summary>
        private void ActualizarDGV()
        {
            dgvSolicitarOP.DataSource = null;
            dgvSolicitarOP.DataSource = _detalles;
        }

        /// <summary>
        /// Muestra un cuadro de diálogo estandarizado para notificar errores al usuario.
        /// </summary>
        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Vacía los campos de texto del formulario y devuelve el foco al selector de productos.
        /// </summary>
        private void LimpiarCampos()
        {
            txtbPesoNeto.Clear();
            txtbCantidad.Clear();
            cmbProducto.Focus();
        }

        /// <summary>
        /// Valida los datos ingresados y añade el producto a la lista temporal de la solicitud de pedido.
        /// </summary>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbProducto.SelectedItem == null)
                    throw new ValidacionDatosException("Seleccione un producto de la lista.");

                ValidadorDatos.ValidarSoloNumeros(txtbCantidad.Text, "Cantidad");

                int cantidadValue = int.Parse(txtbCantidad.Text);
                if (cantidadValue <= 0)
                    throw new ValidacionDatosException("La cantidad debe ser mayor a 0.");

                var productoElegido = (ProductoDTO)cmbProducto.SelectedItem;
                var detalle = new SolicitudDePedidoDetalleDTO
                {
                    IdProducto = productoElegido.IdProducto,
                    Cantidad = cantidadValue,
                    PesoNeto = productoElegido.PesoNeto ?? 0m,
                    Unidad = productoElegido.Unidad ?? "kg",
                    NombreProducto = productoElegido.NombreProducto,
                    Marca = productoElegido.Marca
                };

                _detalles.Add(detalle);
                cmbProducto.SelectedIndex = -1;
                txtbCantidad.Clear();

                ActualizarDGV();
            }
            catch (ValidacionDatosException ex)
            {
                MessageBox.Show(ex.Message.Traducir(), "Atención".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (ex.Message.Contains("Cantidad") || ex.Message.Contains("mayor a 0"))
                    txtbCantidad.Focus();
                else
                    cmbProducto.Focus();
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        /// <summary>
        /// Solicita confirmación, ensambla la solicitud final generándole nuevos identificadores únicos y la guarda en la base de datos.
        /// </summary>
        private void btnGuadar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_detalles.Count == 0)
                    throw new ValidacionDatosException("Agregue al menos un producto a la solicitud antes de guardar.");

                var confirm = MessageBox.Show(
                    string.Format("¿Crear solicitud de pedido con {0} producto(s)?".Traducir(), _detalles.Count),
                    "Confirmar".Traducir(),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                Guid nuevaSolicitudId = Guid.NewGuid();

                foreach (var detalle in _detalles)
                {
                    detalle.IdSolicitudDePedido = nuevaSolicitudId;

                    if (detalle.IdSolicitudDePedidoDetalle == Guid.Empty)
                    {
                        detalle.IdSolicitudDePedidoDetalle = Guid.NewGuid();
                    }
                }

                var solicitudDTO = new SolicitudDePedidoDTO
                {
                    IdSolicitudDePedido = nuevaSolicitudId,
                    FechaSp = DateTime.Today,
                    IdEstadoSp = 1, // Estado inicial (Ej: Pendiente)
                    SolicitudDePedidoDetalles = _detalles
                };

                Guid id = _solicitudService.CrearSolicitud(solicitudDTO);
                string codigoCorto = id.ToString().Substring(0, 8).ToUpper();

                MessageBox.Show(
                    string.Format("Solicitud de Pedido creada exitosamente.\nN° de Referencia: {0}".Traducir(), codigoCorto),
                    "Éxito".Traducir(),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                _detalles.Clear();
                ActualizarDGV();
                LimpiarCampos();
            }
            catch (ValidacionDatosException ex)
            {
                MessageBox.Show(ex.Message.Traducir(), "Validación".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MostrarError(string.Format("Error al guardar: {0}".Traducir(), ex.Message));
            }
        }

        /// <summary>
        /// Detecta clics dentro de la grilla y procesa la eliminación de productos si el usuario presiona el botón de eliminar.
        /// </summary>
        private void dgvSolicitarOP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvSolicitarOP.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                var confirm = MessageBox.Show(
                    "¿Eliminar este producto?".Traducir(),
                    "Confirmar".Traducir(),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    _detalles.RemoveAt(e.RowIndex);
                    ActualizarDGV();
                }
            }
        }

        /// <summary>
        /// Evento de carga inicial del formulario que pobla el menú de productos y aplica traducciones a toda la vista.
        /// </summary>
        private void FormSolicitarOP_Load(object sender, EventArgs e)
        {
            CargarProductos();
            TraductorUI.TraducirFormulario(this);
        }

        /// <summary>
        /// Autocompleta automáticamente los campos de peso neto y marca de acuerdo al producto seleccionado en el menú desplegable.
        /// </summary>
        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedItem != null && cmbProducto.SelectedIndex != -1)
            {
                var productoSeleccionado = (ProductoDTO)cmbProducto.SelectedItem;

                txtbPesoNeto.Text = string.Format("{0:N2} {1}", productoSeleccionado.PesoNeto, productoSeleccionado.Unidad ?? "kg");
                txtbMarca.Text = productoSeleccionado.Marca;
            }
            else
            {
                txtbPesoNeto.Clear();
                txtbMarca.Clear();
            }
        }
    }
}