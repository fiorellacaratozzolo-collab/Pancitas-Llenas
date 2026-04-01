namespace FormUI.Inicio
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnCerrarSesion = new Button();
            menuStrip1 = new MenuStrip();
            tsmAdministrador = new ToolStripMenuItem();
            bitácoraToolStripMenuItem = new ToolStripMenuItem();
            gestiónDeRolesToolStripMenuItem = new ToolStripMenuItem();
            tsmCompras = new ToolStripMenuItem();
            gestiónSolicitudDePedidoToolStripMenuItem = new ToolStripMenuItem();
            gestiónOrdenDePedidoToolStripMenuItem = new ToolStripMenuItem();
            gestiónOrdenDeCompraToolStripMenuItem = new ToolStripMenuItem();
            gestiónDeProveedoresToolStripMenuItem = new ToolStripMenuItem();
            gestiónDeProductoToolStripMenuItem = new ToolStripMenuItem();
            tsmInventario = new ToolStripMenuItem();
            solicitarOrdenDePedidoToolStripMenuItem = new ToolStripMenuItem();
            agregarStockToolStripMenuItem = new ToolStripMenuItem();
            historialDeMovimientosToolStripMenuItem = new ToolStripMenuItem();
            traspasoDeProductosASucursalToolStripMenuItem = new ToolStripMenuItem();
            verStockDisponibleToolStripMenuItem = new ToolStripMenuItem();
            tmsSucursales = new ToolStripMenuItem();
            gestiónDeSucursalToolStripMenuItem = new ToolStripMenuItem();
            solicitarTraspasoDeProductosToolStripMenuItem = new ToolStripMenuItem();
            gestiónDeTraspasoToolStripMenuItem = new ToolStripMenuItem();
            tsmVenta = new ToolStripMenuItem();
            generarUnaVentaToolStripMenuItem = new ToolStripMenuItem();
            gestiónDeClienteToolStripMenuItem = new ToolStripMenuItem();
            gestiónDeVentasToolStripMenuItem = new ToolStripMenuItem();
            historialDeVentasToolStripMenuItem = new ToolStripMenuItem();
            listaDePreciosToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.Location = new Point(304, 418);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(154, 35);
            btnCerrarSesion.TabIndex = 0;
            btnCerrarSesion.Text = "Cerrar Sesión";
            btnCerrarSesion.UseVisualStyleBackColor = true;
            btnCerrarSesion.Click += button1_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { tsmAdministrador, tsmCompras, tsmInventario, tmsSucursales, tsmVenta });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(470, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // tsmAdministrador
            // 
            tsmAdministrador.DropDownItems.AddRange(new ToolStripItem[] { bitácoraToolStripMenuItem, gestiónDeRolesToolStripMenuItem });
            tsmAdministrador.Name = "tsmAdministrador";
            tsmAdministrador.Size = new Size(95, 20);
            tsmAdministrador.Text = "Administrador";
            // 
            // bitácoraToolStripMenuItem
            // 
            bitácoraToolStripMenuItem.Name = "bitácoraToolStripMenuItem";
            bitácoraToolStripMenuItem.Size = new Size(180, 22);
            bitácoraToolStripMenuItem.Text = "Bitácora";
            // 
            // gestiónDeRolesToolStripMenuItem
            // 
            gestiónDeRolesToolStripMenuItem.Name = "gestiónDeRolesToolStripMenuItem";
            gestiónDeRolesToolStripMenuItem.Size = new Size(180, 22);
            gestiónDeRolesToolStripMenuItem.Text = "Gestión de Roles";
            // 
            // tsmCompras
            // 
            tsmCompras.DropDownItems.AddRange(new ToolStripItem[] { gestiónSolicitudDePedidoToolStripMenuItem, gestiónOrdenDePedidoToolStripMenuItem, gestiónOrdenDeCompraToolStripMenuItem, gestiónDeProveedoresToolStripMenuItem, gestiónDeProductoToolStripMenuItem });
            tsmCompras.Name = "tsmCompras";
            tsmCompras.Size = new Size(67, 20);
            tsmCompras.Text = "Compras";
            tsmCompras.Click += compraToolStripMenuItem_Click;
            // 
            // gestiónSolicitudDePedidoToolStripMenuItem
            // 
            gestiónSolicitudDePedidoToolStripMenuItem.Name = "gestiónSolicitudDePedidoToolStripMenuItem";
            gestiónSolicitudDePedidoToolStripMenuItem.Size = new Size(219, 22);
            gestiónSolicitudDePedidoToolStripMenuItem.Tag = "FormGestiónSP";
            gestiónSolicitudDePedidoToolStripMenuItem.Text = "Gestión Solicitud de Pedido";
            // 
            // gestiónOrdenDePedidoToolStripMenuItem
            // 
            gestiónOrdenDePedidoToolStripMenuItem.Name = "gestiónOrdenDePedidoToolStripMenuItem";
            gestiónOrdenDePedidoToolStripMenuItem.Size = new Size(219, 22);
            gestiónOrdenDePedidoToolStripMenuItem.Tag = "FormGestiónOP";
            gestiónOrdenDePedidoToolStripMenuItem.Text = "Gestión Orden de Pedido";
            // 
            // gestiónOrdenDeCompraToolStripMenuItem
            // 
            gestiónOrdenDeCompraToolStripMenuItem.Name = "gestiónOrdenDeCompraToolStripMenuItem";
            gestiónOrdenDeCompraToolStripMenuItem.Size = new Size(219, 22);
            gestiónOrdenDeCompraToolStripMenuItem.Tag = "FormGestiónOC";
            gestiónOrdenDeCompraToolStripMenuItem.Text = "Gestión Orden de Compra";
            // 
            // gestiónDeProveedoresToolStripMenuItem
            // 
            gestiónDeProveedoresToolStripMenuItem.Name = "gestiónDeProveedoresToolStripMenuItem";
            gestiónDeProveedoresToolStripMenuItem.Size = new Size(219, 22);
            gestiónDeProveedoresToolStripMenuItem.Tag = "FormGestiónProveedor";
            gestiónDeProveedoresToolStripMenuItem.Text = "Gestión de Proveedores";
            // 
            // gestiónDeProductoToolStripMenuItem
            // 
            gestiónDeProductoToolStripMenuItem.Name = "gestiónDeProductoToolStripMenuItem";
            gestiónDeProductoToolStripMenuItem.Size = new Size(219, 22);
            gestiónDeProductoToolStripMenuItem.Tag = "FormGestiónProducto";
            gestiónDeProductoToolStripMenuItem.Text = "Gestión de Producto";
            // 
            // tsmInventario
            // 
            tsmInventario.DropDownItems.AddRange(new ToolStripItem[] { solicitarOrdenDePedidoToolStripMenuItem, agregarStockToolStripMenuItem, historialDeMovimientosToolStripMenuItem, traspasoDeProductosASucursalToolStripMenuItem, verStockDisponibleToolStripMenuItem });
            tsmInventario.Name = "tsmInventario";
            tsmInventario.Size = new Size(72, 20);
            tsmInventario.Text = "Inventario";
            // 
            // solicitarOrdenDePedidoToolStripMenuItem
            // 
            solicitarOrdenDePedidoToolStripMenuItem.Name = "solicitarOrdenDePedidoToolStripMenuItem";
            solicitarOrdenDePedidoToolStripMenuItem.Size = new Size(249, 22);
            solicitarOrdenDePedidoToolStripMenuItem.Tag = "FormSolicitarOP";
            solicitarOrdenDePedidoToolStripMenuItem.Text = "Solicitar Orden de Pedido";
            // 
            // agregarStockToolStripMenuItem
            // 
            agregarStockToolStripMenuItem.Name = "agregarStockToolStripMenuItem";
            agregarStockToolStripMenuItem.Size = new Size(249, 22);
            agregarStockToolStripMenuItem.Tag = "FormAgregarStock";
            agregarStockToolStripMenuItem.Text = "Agregar Stock";
            // 
            // historialDeMovimientosToolStripMenuItem
            // 
            historialDeMovimientosToolStripMenuItem.Name = "historialDeMovimientosToolStripMenuItem";
            historialDeMovimientosToolStripMenuItem.Size = new Size(249, 22);
            historialDeMovimientosToolStripMenuItem.Tag = "FormHistorialMovimientos";
            historialDeMovimientosToolStripMenuItem.Text = "Historial de Movimientos";
            // 
            // traspasoDeProductosASucursalToolStripMenuItem
            // 
            traspasoDeProductosASucursalToolStripMenuItem.Name = "traspasoDeProductosASucursalToolStripMenuItem";
            traspasoDeProductosASucursalToolStripMenuItem.Size = new Size(249, 22);
            traspasoDeProductosASucursalToolStripMenuItem.Tag = "FormTraspasoProductosSucursal";
            traspasoDeProductosASucursalToolStripMenuItem.Text = "Traspaso de Productos a Sucursal";
            // 
            // verStockDisponibleToolStripMenuItem
            // 
            verStockDisponibleToolStripMenuItem.Name = "verStockDisponibleToolStripMenuItem";
            verStockDisponibleToolStripMenuItem.Size = new Size(249, 22);
            verStockDisponibleToolStripMenuItem.Tag = "FormVerStockDisponible";
            verStockDisponibleToolStripMenuItem.Text = "Ver Stock Disponible";
            // 
            // tmsSucursales
            // 
            tmsSucursales.DropDownItems.AddRange(new ToolStripItem[] { gestiónDeSucursalToolStripMenuItem, solicitarTraspasoDeProductosToolStripMenuItem, gestiónDeTraspasoToolStripMenuItem });
            tmsSucursales.Name = "tmsSucursales";
            tmsSucursales.Size = new Size(77, 20);
            tmsSucursales.Text = " Sucursales";
            tmsSucursales.Click += sucursalesToolStripMenuItem_Click;
            // 
            // gestiónDeSucursalToolStripMenuItem
            // 
            gestiónDeSucursalToolStripMenuItem.Name = "gestiónDeSucursalToolStripMenuItem";
            gestiónDeSucursalToolStripMenuItem.Size = new Size(294, 22);
            gestiónDeSucursalToolStripMenuItem.Tag = "FormGestiónSucursal";
            gestiónDeSucursalToolStripMenuItem.Text = "Gestión de Sucursal";
            // 
            // solicitarTraspasoDeProductosToolStripMenuItem
            // 
            solicitarTraspasoDeProductosToolStripMenuItem.Name = "solicitarTraspasoDeProductosToolStripMenuItem";
            solicitarTraspasoDeProductosToolStripMenuItem.Size = new Size(294, 22);
            solicitarTraspasoDeProductosToolStripMenuItem.Tag = "FormSolicitarTraspasoProductoSucursal";
            solicitarTraspasoDeProductosToolStripMenuItem.Text = "Solicitar Traspaso de Productos a Sucursal";
            // 
            // gestiónDeTraspasoToolStripMenuItem
            // 
            gestiónDeTraspasoToolStripMenuItem.Name = "gestiónDeTraspasoToolStripMenuItem";
            gestiónDeTraspasoToolStripMenuItem.Size = new Size(294, 22);
            gestiónDeTraspasoToolStripMenuItem.Tag = "FormGestiónTraspaso";
            gestiónDeTraspasoToolStripMenuItem.Text = "Gestión de Traspaso";
            // 
            // tsmVenta
            // 
            tsmVenta.DropDownItems.AddRange(new ToolStripItem[] { generarUnaVentaToolStripMenuItem, gestiónDeClienteToolStripMenuItem, gestiónDeVentasToolStripMenuItem, historialDeVentasToolStripMenuItem, listaDePreciosToolStripMenuItem });
            tsmVenta.Name = "tsmVenta";
            tsmVenta.Size = new Size(48, 20);
            tsmVenta.Text = "Venta";
            // 
            // generarUnaVentaToolStripMenuItem
            // 
            generarUnaVentaToolStripMenuItem.Name = "generarUnaVentaToolStripMenuItem";
            generarUnaVentaToolStripMenuItem.Size = new Size(180, 22);
            generarUnaVentaToolStripMenuItem.Tag = "FormGenerarVenta";
            generarUnaVentaToolStripMenuItem.Text = "Generar una Venta";
            // 
            // gestiónDeClienteToolStripMenuItem
            // 
            gestiónDeClienteToolStripMenuItem.Name = "gestiónDeClienteToolStripMenuItem";
            gestiónDeClienteToolStripMenuItem.Size = new Size(180, 22);
            gestiónDeClienteToolStripMenuItem.Tag = "FormGestiónCliente";
            gestiónDeClienteToolStripMenuItem.Text = "Gestión de Cliente";
            // 
            // gestiónDeVentasToolStripMenuItem
            // 
            gestiónDeVentasToolStripMenuItem.Name = "gestiónDeVentasToolStripMenuItem";
            gestiónDeVentasToolStripMenuItem.Size = new Size(180, 22);
            gestiónDeVentasToolStripMenuItem.Tag = "FormGestiónVenta";
            gestiónDeVentasToolStripMenuItem.Text = "Gestión de Ventas";
            // 
            // historialDeVentasToolStripMenuItem
            // 
            historialDeVentasToolStripMenuItem.Name = "historialDeVentasToolStripMenuItem";
            historialDeVentasToolStripMenuItem.Size = new Size(180, 22);
            historialDeVentasToolStripMenuItem.Tag = "FormHistorialVentas";
            historialDeVentasToolStripMenuItem.Text = "Historial de Ventas";
            // 
            // listaDePreciosToolStripMenuItem
            // 
            listaDePreciosToolStripMenuItem.Name = "listaDePreciosToolStripMenuItem";
            listaDePreciosToolStripMenuItem.Size = new Size(180, 22);
            listaDePreciosToolStripMenuItem.Tag = "FormListaPrecios";
            listaDePreciosToolStripMenuItem.Text = "Lista de Precios";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem2 });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(12, 20);
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(67, 22);
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(470, 465);
            Controls.Add(btnCerrarSesion);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FormPrincipal";
            Text = "Menú";
            Load += FormPrincipal_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCerrarSesion;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem tsmAdministrador;
        private ToolStripMenuItem tsmCompras;
        private ToolStripMenuItem tsmVenta;
        private ToolStripMenuItem tmsSucursales;
        private ToolStripMenuItem tsmInventario;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem gestiónSolicitudDePedidoToolStripMenuItem;
        private ToolStripMenuItem gestiónOrdenDePedidoToolStripMenuItem;
        private ToolStripMenuItem gestiónOrdenDeCompraToolStripMenuItem;
        private ToolStripMenuItem gestiónDeProveedoresToolStripMenuItem;
        private ToolStripMenuItem gestiónDeProductoToolStripMenuItem;
        private ToolStripMenuItem solicitarOrdenDePedidoToolStripMenuItem;
        private ToolStripMenuItem agregarStockToolStripMenuItem;
        private ToolStripMenuItem historialDeMovimientosToolStripMenuItem;
        private ToolStripMenuItem traspasoDeProductosASucursalToolStripMenuItem;
        private ToolStripMenuItem verStockDisponibleToolStripMenuItem;
        private ToolStripMenuItem gestiónDeSucursalToolStripMenuItem;
        private ToolStripMenuItem solicitarTraspasoDeProductosToolStripMenuItem;
        private ToolStripMenuItem bitácoraToolStripMenuItem;
        private ToolStripMenuItem gestiónDeRolesToolStripMenuItem;
        private ToolStripMenuItem generarUnaVentaToolStripMenuItem;
        private ToolStripMenuItem gestiónDeClienteToolStripMenuItem;
        private ToolStripMenuItem gestiónDeVentasToolStripMenuItem;
        private ToolStripMenuItem historialDeVentasToolStripMenuItem;
        private ToolStripMenuItem listaDePreciosToolStripMenuItem;
        private ToolStripMenuItem gestiónDeTraspasoToolStripMenuItem;
    }
}