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
            txtbNombreProd.ReadOnly = false;
            txtbNombreProd.Enabled = true;
            txtbNombreProd.TabStop = true;
            txtbNombreProd.AutoCompleteMode = AutoCompleteMode.None;
            txtbNombreProd.AutoCompleteSource = AutoCompleteSource.None;

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


        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtbNombreProd.Text))
            {
                MessageBox.Show("Ingrese el nombre del producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtbNombreProd.Focus();
                return false;
            }

            if (!int.TryParse(txtbCantidad.Text, out int cant) || cant <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida mayor a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtbCantidad.Focus();
                return false;
            }

            if (!decimal.TryParse(txtbPesoNeto.Text, out decimal peso) || peso <= 0m)
            {
                MessageBox.Show("Ingrese un Peso Neto válido mayor a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtbPesoNeto.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarCamposExceptoProducto()
        {
            txtbPesoNeto.Clear();
            txtbCantidad.Clear();
            txtbCantidad.Focus();
        }

        private void ActualizarDGV()
        {
            var dataSource = _detalles.Select(d => new
            {
                NombreProducto = d.NombreProducto,
                PesoNeto = $"{d.PesoNeto:N2} {d.Unidad}",
                Cantidad = d.Cantidad
            }).ToList();

            dgvSolicitarOP.DataSource = null;
            dgvSolicitarOP.DataSource = dataSource;
            dgvSolicitarOP.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LimpiarCampos()
        {
            txtbNombreProd.Clear();
            txtbPesoNeto.Clear();
            txtbCantidad.Clear();
            txtbNombreProd.Focus();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return; // Valida que el peso también sea un decimal > 0

            string nombre = txtbNombreProd.Text.Trim();

            var producto = _productoService.GetAllProductos()
                .FirstOrDefault(p => p.NombreProducto.Equals(nombre, StringComparison.OrdinalIgnoreCase));

            if (producto == null)
            {
                MostrarError("Producto no encontrado. Verifique el nombre.");
                txtbNombreProd.Focus();
                txtbNombreProd.SelectAll();
                return;
            }

            // 1. Obtener valores parseados
            int.TryParse(txtbCantidad.Text, out int cantidadValue);
            decimal.TryParse(txtbPesoNeto.Text, out decimal pesoNetoValue);
            // 2. Mostrar peso neto        
            txtbPesoNeto.Text = $"{pesoNetoValue:N2} {producto.Unidad ?? "kg"}";
            // 3. Crear detalle
            var detalle = new SolicitudDePedidoDetalleDTO
            {
                IdProducto = producto.IdProducto,
                Cantidad = cantidadValue,
                PesoNeto = pesoNetoValue,

                Unidad = producto.Unidad ?? "kg",
                NombreProducto = producto.NombreProducto
            };

            _detalles.Add(detalle);
            LimpiarCamposExceptoProducto();
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

                this.Close();
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

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
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

        }
    }
}
