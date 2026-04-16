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

namespace FormUI.FormInventario
{
    public partial class FormVerStockDisponible : Form
    {
        private readonly InventarioService _stockService;
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
                    return Color.LightGreen; // Verde claro
                case 2:
                    return Color.Yellow;    // Amarillo
                case 3:
                    return Color.Red;       // Rojo
                default:
                    return Color.White;     // Estado desconocido
            }
        }

        /// <summary>
        /// Aplica formato y colores de semáforo a las filas del DataGridView.
        /// </summary>
        private void AplicarSemaforoDGV()
        {
            // Asegúrate de que las filas se hayan generado antes de intentar acceder a ellas
            if (dgvStock.Rows.Count == 0) return;

            // Itera sobre todas las filas del DGV
            foreach (DataGridViewRow row in dgvStock.Rows)
            {
                // La columna 'IdEstadoStock' debe existir y ser de tipo int
                if (row.Cells["IdEstadoStock"].Value is int idEstado)
                {
                    Color color = ObtenerColorSemaforo(idEstado);

                    // Aplicar color a toda la fila
                    row.DefaultCellStyle.BackColor = color;

                    // Si es Rojo, cambiar el texto a blanco para que sea legible
                    if (color == Color.Red)
                    {
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                }
            }
        }

        private void FormVerStockDisponible_Load(object sender, EventArgs e)
        {
            btnVer_Click(this, EventArgs.Empty);
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            try
            {
                Guid miSucursal = SessionManager.Current.IdSucursalActual.Value;
                var miStock = _stockService.ObtenerStockPorSucursal(miSucursal);
                dgvStock.DataSource = null;
                dgvStock.DataSource = miStock;

                if (miStock.Count == 0)
                {
                    MessageBox.Show("No hay stock registrado para esta sucursal.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ConfigurarGrillaStock();
                AplicarSemaforoDGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el stock: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

            // Renombrar y ordenar
            if (dgvStock.Columns.Contains("NombreProducto"))
            {
                dgvStock.Columns["NombreProducto"].HeaderText = "Producto";
                dgvStock.Columns["NombreProducto"].DisplayIndex = 0;
            }

            if (dgvStock.Columns.Contains("Marca"))
            {
                dgvStock.Columns["Marca"].HeaderText = "Marca";
                dgvStock.Columns["Marca"].DisplayIndex = 1;
            }
            if (dgvStock.Columns.Contains("PesoNeto"))
            {
                dgvStock.Columns["PesoNeto"].HeaderText = "Peso Neto";
                dgvStock.Columns["PesoNeto"].DisplayIndex = 2;
            }
            if (dgvStock.Columns.Contains("Unidad"))
            {
                dgvStock.Columns["Unidad"].HeaderText = "Unidad";
                dgvStock.Columns["Unidad"].DisplayIndex = 3;
            }
            if (dgvStock.Columns.Contains("Cantidad"))
            {
                dgvStock.Columns["Cantidad"].HeaderText = "Stock Disponible";
                dgvStock.Columns["Cantidad"].DisplayIndex = 4;
                dgvStock.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

    }
}
