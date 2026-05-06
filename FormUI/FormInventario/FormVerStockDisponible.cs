using DataAccess.Models;
using Logic.Facade;
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

namespace FormUI.FormInventario
{
    public partial class FormVerStockDisponible : Form
    {
        private readonly InventarioService _stockService;

        /// <summary>
        /// Inicializa el formulario y el servicio de inventario necesario para consultar las existencias.
        /// </summary>
        public FormVerStockDisponible()
        {
            InitializeComponent();
            _stockService = new InventarioService();
        }
        /// <summary>
        /// Traduce el ID del estado de stock (semáforo) a un color de System.Drawing.
        /// </summary>
        private Color ObtenerColorSemaforo(int idEstadoStock)
        {
            // IDs: 1=Verde, 2=Amarillo, 3=Rojo (Basado en la lógica definida)
            switch (idEstadoStock)
            {
                case 1:
                    return Color.LightGreen; 
                case 2:
                    return Color.Yellow;    
                case 3:
                    return Color.Red;       
                default:
                    return Color.White;     
            }
        }
        /// <summary>
        /// Recorre las filas de la grilla y aplica los colores del semáforo según el estado del stock de cada producto.
        /// </summary>
        private void AplicarSemaforoDGV()
        {
            if (dgvStock.Rows.Count == 0) return;

            foreach (DataGridViewRow row in dgvStock.Rows)
            {
                if (row.Cells["IdEstadoStock"].Value is int idEstado)
                {
                    Color color = ObtenerColorSemaforo(idEstado);

                    row.DefaultCellStyle.BackColor = color;

                    if (color == Color.Red)
                    {
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                }
            }
        }
        /// <summary>
        /// Evento de carga inicial que dispara la consulta automática del stock y aplica las traducciones de interfaz.
        /// </summary>
        private void FormVerStockDisponible_Load(object sender, EventArgs e)
        {
            btnVer_Click(this, EventArgs.Empty);
            TraductorUI.TraducirFormulario(this);
        }
        /// <summary>
        /// Consulta el stock disponible en la base de datos para la sucursal actual y lo vincula a la grilla en pantalla.
        /// </summary>
        private void btnVer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SessionManager.Current.IdSucursalActual.HasValue)
                {
                    MessageBox.Show("No hay una sucursal seleccionada en la sesión actual para ver el stock.".Traducir(), "Advertencia".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Guid miSucursal = SessionManager.Current.IdSucursalActual.Value;

                var miStock = _stockService.ObtenerStockPorSucursal(miSucursal);
                dgvStock.DataSource = null;
                dgvStock.DataSource = miStock;

                if (miStock.Count == 0)
                {
                    MessageBox.Show("No hay stock registrado para esta sucursal.".Traducir(), "Información".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ConfigurarGrillaStock();
                AplicarSemaforoDGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al cargar el stock: {0}".Traducir(), ex.Message), "Error".Traducir(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Oculta columnas técnicas irrelevantes y asigna traducciones, orden y alineación a los encabezados visibles de la grilla.
        /// </summary>
        private void ConfigurarGrillaStock()
        {
            dgvStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvStock.Columns.Contains("IdStockPorSucursal")) dgvStock.Columns["IdStockPorSucursal"].Visible = false;
            if (dgvStock.Columns.Contains("IdSucursal")) dgvStock.Columns["IdSucursal"].Visible = false;
            if (dgvStock.Columns.Contains("IdProducto")) dgvStock.Columns["IdProducto"].Visible = false;
            if (dgvStock.Columns.Contains("IdSucursalNavigation")) dgvStock.Columns["IdSucursalNavigation"].Visible = false;
            if (dgvStock.Columns.Contains("IdProductoNavigation")) dgvStock.Columns["IdProductoNavigation"].Visible = false;
            if (dgvStock.Columns.Contains("IdEstadoStockNavigation")) dgvStock.Columns["IdEstadoStockNavigation"].Visible = false;
            if (dgvStock.Columns.Contains("IdStockSucursal")) dgvStock.Columns["IdStockSucursal"].Visible = false;
            if (dgvStock.Columns.Contains("IdEstadoStock")) dgvStock.Columns["IdEstadoStock"].Visible = false;

            if (dgvStock.Columns.Contains("NombreProducto"))
            {
                dgvStock.Columns["NombreProducto"].HeaderText = "Producto".Traducir();
                dgvStock.Columns["NombreProducto"].DisplayIndex = 0;
            }

            if (dgvStock.Columns.Contains("Marca"))
            {
                dgvStock.Columns["Marca"].HeaderText = "Marca".Traducir();
                dgvStock.Columns["Marca"].DisplayIndex = 1;
            }

            if (dgvStock.Columns.Contains("PesoNeto"))
            {
                dgvStock.Columns["PesoNeto"].HeaderText = "Peso Neto".Traducir();
                dgvStock.Columns["PesoNeto"].DisplayIndex = 2;
            }

            if (dgvStock.Columns.Contains("Unidad"))
            {
                dgvStock.Columns["Unidad"].HeaderText = "Unidad".Traducir();
                dgvStock.Columns["Unidad"].DisplayIndex = 3;
            }

            if (dgvStock.Columns.Contains("Cantidad"))
            {
                dgvStock.Columns["Cantidad"].HeaderText = "Stock Disponible".Traducir();
                dgvStock.Columns["Cantidad"].DisplayIndex = 4;
                dgvStock.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
    }
}
