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
    public partial class FormSolicitarOP : Form
    {
        private readonly ProductoService _productoService = new ProductoService();
        private readonly SolicitudDePedidoService _solicitudService = new SolicitudDePedidoService();
        private readonly List<SolicitudDePedidoDetalleDTO> _detalles = new List<SolicitudDePedidoDetalleDTO>();

        public FormSolicitarOP()
        {
            InitializeComponent();
            ConfigurarFecha();
            ConfigurarDGV();
        }

        private void ConfigurarFecha()
        {
            dtpFecha.Value = DateTime.Today;
            dtpFecha.Enabled = false;
        }

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

        private void CargarProductos()
        {
            cmbProducto.DataSource = _productoService.GetAllProductos();
            cmbProducto.DisplayMember = "NombreConPeso";
            cmbProducto.ValueMember = "IdProducto";
            cmbProducto.SelectedIndex = -1;
        }

        private void ActualizarDGV()
        {
            dgvSolicitarOP.DataSource = null;
            dgvSolicitarOP.DataSource = _detalles;
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LimpiarCampos()
        {
            txtbPesoNeto.Clear();
            txtbCantidad.Clear();
            cmbProducto.Focus();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // 1. Validaciones
            if (cmbProducto.SelectedItem == null)
            {
                MostrarError("Seleccione un producto de la lista.");
                cmbProducto.Focus();
                return;
            }

            if (!int.TryParse(txtbCantidad.Text, out int cantidadValue) || cantidadValue <= 0)
            {
                MostrarError("Ingrese una cantidad válida mayor a 0.");
                txtbCantidad.Focus();
                return;
            }

            var productoElegido = (ProductoDTO)cmbProducto.SelectedItem;
            var detalle = new SolicitudDePedidoDetalleDTO
            {
                IdProducto = productoElegido.IdProducto,
                Cantidad = cantidadValue,
                PesoNeto = (decimal)productoElegido.PesoNeto,
                Unidad = productoElegido.Unidad ?? "kg",
                NombreProducto = productoElegido.NombreProducto,
                Marca = productoElegido.Marca
            };
            _detalles.Add(detalle);
            cmbProducto.SelectedIndex = -1;
            txtbCantidad.Clear();

            ActualizarDGV();
        }

        private void btnGuadar_Click(object sender, EventArgs e)
        {
            if (_detalles.Count == 0)
            {
                MessageBox.Show("Agregue al menos un producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Crear solicitud de pedido con {_detalles.Count} producto(s)?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                // 1. Generar un nuevo ID único para la solicitud
                Guid nuevaSolicitudId = Guid.NewGuid();

                // 2. Asignar el nuevo ID a todos los detalles
                foreach (var detalle in _detalles)
                {
                    // Asigna el ID de la cabecera a la foreign key de los detalles
                    detalle.IdSolicitudDePedido = nuevaSolicitudId;


                    // genera un Guid único para la clave primaria del detalle.
                    if (detalle.IdSolicitudDePedidoDetalle == Guid.Empty)
                    {
                        detalle.IdSolicitudDePedidoDetalle = Guid.NewGuid();
                    }
                }

                // 3. Crear el DTO de la cabecera con el nuevo ID generado
                var solicitudDTO = new SolicitudDePedidoDTO
                {
                    //Asignar el nuevo GUID para evitar la clave duplicada (Guid.Empty)
                    IdSolicitudDePedido = nuevaSolicitudId,

                    FechaSp = DateTime.Today,
                    IdEstadoSp = 1, // Pendiente
                    SolicitudDePedidoDetalles = _detalles
                };

                Guid id = _solicitudService.CrearSolicitud(solicitudDTO);

                MessageBox.Show(
                    $"Solicitud de Pedido creada exitosamente.\nID: {id}",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void dgvSolicitarOP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvSolicitarOP.Columns[e.ColumnIndex].Name == "Eliminar")
            {
                var confirm = MessageBox.Show(
                    "¿Eliminar este producto?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    _detalles.RemoveAt(e.RowIndex);
                    ActualizarDGV();
                }
            }
        }

        private void FormSolicitarOP_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedItem != null && cmbProducto.SelectedIndex != -1)
            {
                var productoSeleccionado = (ProductoDTO)cmbProducto.SelectedItem;

                // Autocompletamos los campos
                txtbPesoNeto.Text = $"{productoSeleccionado.PesoNeto:N2} {productoSeleccionado.Unidad ?? "kg"}";
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
