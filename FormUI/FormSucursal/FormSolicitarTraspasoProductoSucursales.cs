using DataAccess.Implementations.SqlServer;
using Logic;
using Logic.Facade;
using Logic.MappingProfiles;
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

namespace FormUI.FormSucursal
{
    public partial class FormSolicitarTraspasoProductoSucursales : Form
    {
        private BindingList<SolicitudDeTraspasoDeProductosDetalleDTO> _listaProductos = new BindingList<SolicitudDeTraspasoDeProductosDetalleDTO>();

        public FormSolicitarTraspasoProductoSucursales()
        {
            InitializeComponent();

        }

        private void FormSolicitarTraspasoProductoSucursales_Load(object sender, EventArgs e)
        {
        }

        private void ConfigurarColumnasGrilla()
        {
            // Ocultar IDs y acomodar visualmente como hiciste en Ventas
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // ... (Ocultar IdProducto, etc.)
        }
        private void CargarSucursales()
        {
            SucursalService sucursalService = new SucursalService();
            var todasLasSucursales = sucursalService.GetAllSucursales();
            var sucursalesOrigen = todasLasSucursales
                .Where(s => s.IdTipoSucursal == 2) 
                .ToList();

            cmbSucursalOrigen.DataSource = sucursalesOrigen;
            cmbSucursalOrigen.DisplayMember = "Direccion";
            cmbSucursalOrigen.ValueMember = "IdSucursal";
            cmbSucursalOrigen.SelectedIndex = -1;
        }

        private void CargarProductos()
        {
            Logic.Facade.ProductoService productoService = new Logic.Facade.ProductoService();
            cmbProducto.DataSource = productoService.GetAllProductos();
            cmbProducto.DisplayMember = "NombreConPeso";
            cmbProducto.ValueMember = "Marca";
            cmbProducto.ValueMember = "IdProducto";
            cmbProducto.SelectedIndex = -1;
        }

        private void cmbSucursalOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un producto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtbCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida mayor a cero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var productoElegido = (ProductoDTO)cmbProducto.SelectedItem;

            // Validar si el producto ya está en la grilla para sumar la cantidad en vez de repetir renglón (Opcional)
            var itemExistente = _listaProductos.FirstOrDefault(p => p.IdProducto == productoElegido.IdProducto);
            if (itemExistente != null)
            {
                itemExistente.Cantidad += cantidad;
                dgvProductos.Refresh();
            }
            else
            {
                _listaProductos.Add(new SolicitudDeTraspasoDeProductosDetalleDTO
                {
                    IdProducto = productoElegido.IdProducto,
                    NombreProducto = productoElegido.NombreProducto,
                    PesoNeto = (decimal)productoElegido.PesoNeto,
                    Cantidad = cantidad
                });
            }
            cmbProducto.SelectedIndex = -1;
            txtbCantidad.Clear();
            txtbPesoNeto.Clear();
            txtbMarca.Clear();
        }

        private void btnSolicitarTraspaso_Click(object sender, EventArgs e)
        {
            // 1. Validaciones básicas
            if (_listaProductos.Count == 0)
            {
                MessageBox.Show("Agregue al menos un producto a la solicitud.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbSucursalOrigen.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una sucursal de destino.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Guid idSucursalOrigen = (Guid)cmbSucursalOrigen.SelectedValue;
                // La sucursal que recibe es la tuya (la que está logueada pidiendo la mercadería)
                Guid idSucursalDestino = SessionManager.Current.IdSucursalActual.Value;

                var nuevaSolicitud = new SolicitudDeTraspasoDeProductoDTO
                {
                    FechaStp = dtpFechaSolicitud.Value.Date,
                    IdSucursalOrigen = idSucursalOrigen,
                    IdSucursalDestino = idSucursalDestino
                };
                var listaDetalles = _listaProductos.ToList();
                var service = new Logic.Facade.TraspasoService();
                service.GenerarSolicitud(nuevaSolicitud, listaDetalles);

                MessageBox.Show("¡Solicitud de traspaso generada con éxito!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _listaProductos.Clear();
                cmbSucursalOrigen.SelectedIndex = -1;
                dtpFechaSolicitud.Value = DateTime.Today;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al procesar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormSolicitarTraspasoProductoSucursales_Load_1(object sender, EventArgs e)
        {
            try
            {
                txtbSucursalDestino.Text = SessionManager.Current.NombreSucursalActual;
                dgvProductos.DataSource = _listaProductos;
                ConfigurarColumnasGrilla();
                CargarSucursales();
                CargarProductos();
                dtpFechaSolicitud.Value = DateTime.Today;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la pantalla: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedItem != null && cmbProducto.SelectedIndex != -1)
            {
                var productoSeleccionado = (ProductoDTO)cmbProducto.SelectedItem;
                txtbPesoNeto.Text = productoSeleccionado.PesoNeto.ToString();
                txtbMarca.Text = productoSeleccionado.Marca;
            }
            else
            {
                txtbPesoNeto.Clear();
            }
        }
    }
}
