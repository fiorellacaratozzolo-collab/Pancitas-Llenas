using DataAccess.Models;
using Logic.Facade;
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
        public FormVerStockDisponible()
        {
            InitializeComponent();
            //CargarStockEnDGV();
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
        /// Carga el stock de todas las sucursales en el DataGridView.
        /// </summary>
        //private void CargarStockEnDGV()
        //{
        //    try
        //    {
        //        // 1. Obtener todos los registros de stock de la fachada
        //        List<StockPorSucursal> listaStock = _inventarioLogic.ObtenerTodoElStock();

        //        // 2. Proyectar a un tipo anónimo o ViewModel para el DataGridView
        //        var dataSource = listaStock.Select(s => new
        //        {
        //            // Se asume que las propiedades de navegación están cargadas
        //            IdEstadoStock = s.IdEstadoStock,
        //            Producto = s.IdProductoNavigation.NombreProducto,
        //            Sucursal = s.IdSucursalNavigation.NombreSucursal,
        //            StockActual = s.StockActual,
        //            StockDeseado = s.StockDeseado,
        //            Estado = s.IdEstadoStockNavigation.Descripcion // Descripción del estado (Verde/Amarillo/Rojo)
        //        }).ToList();

        //        // 3. Asignar la fuente de datos
        //        dgvStock.DataSource = null;
        //        dgvStock.DataSource = dataSource;

        //        // 4. Configurar el DGV y aplicar el semáforo
        //        ConfigurarDGVStock();
        //        AplicarSemaforoDGV();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error al cargar el stock: {ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

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

        /// <summary>
        /// Configura la visibilidad y encabezados de las columnas del DataGridView.
        /// </summary>
        private void ConfigurarDGVStock()
        {
            // Ocultar la columna del ID de estado (solo la usamos para el color)
            if (dgvStock.Columns.Contains("IdEstadoStock"))
                dgvStock.Columns["IdEstadoStock"].Visible = false;

            // Renombrar y dimensionar las columnas
            dgvStock.Columns["Producto"].HeaderText = "Producto";
            dgvStock.Columns["Sucursal"].HeaderText = "Ubicación";
            dgvStock.Columns["StockActual"].HeaderText = "Stock Actual";
            dgvStock.Columns["StockDeseado"].HeaderText = "Stock Deseado";
            dgvStock.Columns["Estado"].HeaderText = "Estado (Semáforo)";

            // Ajustar el ancho de las columnas (opcional)
            dgvStock.Columns["Producto"].Width = 200;
        }

        private void FormVerStockDisponible_Load(object sender, EventArgs e)
        {

        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            // Si tienes un botón "Ver" para refrescar la lista
            //CargarStockEnDGV();
        }
    }
}
