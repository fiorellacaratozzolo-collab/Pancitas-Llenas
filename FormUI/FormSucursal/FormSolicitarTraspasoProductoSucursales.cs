using DataAccess.Implementations.SqlServer;
using Logic;
using Logic.MappingProfiles;
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

namespace FormUI.FormSucursal
{
    public partial class FormSolicitarTraspasoProductoSucursales : Form
    {
        private readonly TraspasoLogic _traspasoLogic;
        private readonly SucursalLogic _sucursalLogic;
        private readonly ProductoLogic _productoLogic;
        private BindingList<SolicitudDeTraspasoDeProductosDetalleDTO> _listaDetalles;

        public FormSolicitarTraspasoProductoSucursales()
        {
            InitializeComponent();         
            _traspasoLogic = new TraspasoLogic();           
            _sucursalLogic = new SucursalLogic();
            _productoLogic = new ProductoLogic();
            _listaDetalles = new BindingList<SolicitudDeTraspasoDeProductosDetalleDTO>();
            ConfigurarGrilla();
        }

        private void FormSolicitarTraspasoProductoSucursales_Load(object sender, EventArgs e)
        {
            cmbSucursalOrigen.DataSource = _sucursalLogic.ObtenerTodasLasSucursales();
            cmbSucursalOrigen.DisplayMember = "NombreSucursal";
            cmbSucursalOrigen.ValueMember = "IdSucursal";

            cmbProductos.DataSource = _productoLogic.ObtenerTodos();
            cmbProductos.DisplayMember = "NombreProducto";
            cmbProductos.ValueMember = "IdProducto";

            dgvItemsSolicitados.DataSource = _listaDetalles;
        }

        private void ConfigurarGrilla()
        {
            dgvItemsSolicitados.AutoGenerateColumns = true;
            // Esperar a que se asigne el DataSource para ocultar
            dgvItemsSolicitados.DataBindingComplete += (s, e) =>
            {
                string[] ocultar = { "IdSolicitudDeTraspasoDeProductosDetalle", "IdSolicitudDeTraspasoDeProductos", "IdProducto", "IdSolicitudDeTraspasoDeProductosNavigation", "IdProductoNavigation" };
                foreach (var col in ocultar)
                    if (dgvItemsSolicitados.Columns[col] != null) dgvItemsSolicitados.Columns[col].Visible = false;
            };
        }

        private void cmbSucursalOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedItem is ProductoDTO prod)
            {
                txtbPesoNeto.Text = prod.PesoNeto.ToString();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedItem is not ProductoDTO prod) return;
            if (!int.TryParse(txtbCantidad.Text, out int cant) || cant <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida");
                return;
            }

            var detalle = new SolicitudDeTraspasoDeProductosDetalleDTO
            {
                IdProducto = prod.IdProducto,
                NombreProducto = prod.NombreProducto, // Para mostrar en el grid
                Cantidad = cant,
                PesoNeto = prod.PesoNeto ?? 0m,
                Unidad = "KG" // O la unidad que corresponda
            };

            _listaDetalles.Add(detalle);
        }

        private void btnSolicitarTraspaso_Click(object sender, EventArgs e)
        {
            if (cmbSucursalOrigen.SelectedValue is Guid idOrigen)
            {
                var nuevaSolicitud = new SolicitudDeTraspasoDeProductoDTO
                {
                    IdSucursalOrigen = idOrigen, // El depósito seleccionado en el combo

                    // LA SUCURSAL DESTINO ES LA NUESTRA:
                    IdSucursalDestino = GlobalSettings.SucursalActualId,

                    SolicitudDeTraspasoDeProductosDetalles = _listaDetalles.ToList()
                };

                _traspasoLogic.CrearSolicitud(nuevaSolicitud);
                MessageBox.Show($"Solicitud enviada desde {GlobalSettings.NombreSucursal}");
            }
        }

        private void FormSolicitarTraspasoProductoSucursales_Load_1(object sender, EventArgs e)
        {

        }
    }
}
