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
            button1 = new Button();
            menuStrip1 = new MenuStrip();
            administradorToolStripMenuItem = new ToolStripMenuItem();
            compraToolStripMenuItem = new ToolStripMenuItem();
            ventaToolStripMenuItem = new ToolStripMenuItem();
            sucursalesToolStripMenuItem = new ToolStripMenuItem();
            inventarioToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            gestiónOrdenDeCompraToolStripMenuItem = new ToolStripMenuItem();
            gestiónOrdenDePedidoToolStripMenuItem = new ToolStripMenuItem();
            gestiónSolicitudDePedidoToolStripMenuItem = new ToolStripMenuItem();
            gestiónDeProveedoresToolStripMenuItem = new ToolStripMenuItem();
            gestiónDeProductoToolStripMenuItem = new ToolStripMenuItem();
            solicitarOrdenDePedidoToolStripMenuItem = new ToolStripMenuItem();
            agregarStockToolStripMenuItem = new ToolStripMenuItem();
            historialDeMovimientosToolStripMenuItem = new ToolStripMenuItem();
            traspasoDeProductosASucursalToolStripMenuItem = new ToolStripMenuItem();
            verStockDisponibleToolStripMenuItem = new ToolStripMenuItem();
            gestiónDeSucursalToolStripMenuItem = new ToolStripMenuItem();
            solicitarTraspasoDeProductosToolStripMenuItem = new ToolStripMenuItem();
            generarUnaVentaToolStripMenuItem = new ToolStripMenuItem();
            gestiónDeClienteToolStripMenuItem = new ToolStripMenuItem();
            gestiónDeVentasToolStripMenuItem = new ToolStripMenuItem();
            historialDeVentasToolStripMenuItem = new ToolStripMenuItem();
            listaDePreciosToolStripMenuItem = new ToolStripMenuItem();
            bitácoraToolStripMenuItem = new ToolStripMenuItem();
            gestiónDeRolesToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(304, 418);
            button1.Name = "button1";
            button1.Size = new Size(154, 35);
            button1.TabIndex = 0;
            button1.Text = "Cerrar Sesión";
            button1.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { administradorToolStripMenuItem, compraToolStripMenuItem, inventarioToolStripMenuItem, sucursalesToolStripMenuItem, ventaToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(470, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // administradorToolStripMenuItem
            // 
            administradorToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { bitácoraToolStripMenuItem, gestiónDeRolesToolStripMenuItem });
            administradorToolStripMenuItem.Name = "administradorToolStripMenuItem";
            administradorToolStripMenuItem.Size = new Size(95, 20);
            administradorToolStripMenuItem.Text = "Administrador";
            // 
            // compraToolStripMenuItem
            // 
            compraToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { gestiónSolicitudDePedidoToolStripMenuItem, gestiónOrdenDePedidoToolStripMenuItem, gestiónOrdenDeCompraToolStripMenuItem, gestiónDeProveedoresToolStripMenuItem, gestiónDeProductoToolStripMenuItem });
            compraToolStripMenuItem.Name = "compraToolStripMenuItem";
            compraToolStripMenuItem.Size = new Size(67, 20);
            compraToolStripMenuItem.Text = "Compras";
            compraToolStripMenuItem.Click += compraToolStripMenuItem_Click;
            // 
            // ventaToolStripMenuItem
            // 
            ventaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { generarUnaVentaToolStripMenuItem, gestiónDeClienteToolStripMenuItem, gestiónDeVentasToolStripMenuItem, historialDeVentasToolStripMenuItem, listaDePreciosToolStripMenuItem });
            ventaToolStripMenuItem.Name = "ventaToolStripMenuItem";
            ventaToolStripMenuItem.Size = new Size(48, 20);
            ventaToolStripMenuItem.Text = "Venta";
            // 
            // sucursalesToolStripMenuItem
            // 
            sucursalesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { gestiónDeSucursalToolStripMenuItem, solicitarTraspasoDeProductosToolStripMenuItem });
            sucursalesToolStripMenuItem.Name = "sucursalesToolStripMenuItem";
            sucursalesToolStripMenuItem.Size = new Size(66, 20);
            sucursalesToolStripMenuItem.Text = " Sucursal";
            sucursalesToolStripMenuItem.Click += sucursalesToolStripMenuItem_Click;
            // 
            // inventarioToolStripMenuItem
            // 
            inventarioToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { solicitarOrdenDePedidoToolStripMenuItem, agregarStockToolStripMenuItem, historialDeMovimientosToolStripMenuItem, traspasoDeProductosASucursalToolStripMenuItem, verStockDisponibleToolStripMenuItem });
            inventarioToolStripMenuItem.Name = "inventarioToolStripMenuItem";
            inventarioToolStripMenuItem.Size = new Size(72, 20);
            inventarioToolStripMenuItem.Text = "Inventario";
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
            // gestiónOrdenDeCompraToolStripMenuItem
            // 
            gestiónOrdenDeCompraToolStripMenuItem.Name = "gestiónOrdenDeCompraToolStripMenuItem";
            gestiónOrdenDeCompraToolStripMenuItem.Size = new Size(219, 22);
            gestiónOrdenDeCompraToolStripMenuItem.Text = "Gestión Orden de Compra";
            // 
            // gestiónOrdenDePedidoToolStripMenuItem
            // 
            gestiónOrdenDePedidoToolStripMenuItem.Name = "gestiónOrdenDePedidoToolStripMenuItem";
            gestiónOrdenDePedidoToolStripMenuItem.Size = new Size(219, 22);
            gestiónOrdenDePedidoToolStripMenuItem.Text = "Gestión Orden de Pedido";
            // 
            // gestiónSolicitudDePedidoToolStripMenuItem
            // 
            gestiónSolicitudDePedidoToolStripMenuItem.Name = "gestiónSolicitudDePedidoToolStripMenuItem";
            gestiónSolicitudDePedidoToolStripMenuItem.Size = new Size(219, 22);
            gestiónSolicitudDePedidoToolStripMenuItem.Text = "Gestión Solicitud de Pedido";
            // 
            // gestiónDeProveedoresToolStripMenuItem
            // 
            gestiónDeProveedoresToolStripMenuItem.Name = "gestiónDeProveedoresToolStripMenuItem";
            gestiónDeProveedoresToolStripMenuItem.Size = new Size(219, 22);
            gestiónDeProveedoresToolStripMenuItem.Text = "Gestión de Proveedores";
            // 
            // gestiónDeProductoToolStripMenuItem
            // 
            gestiónDeProductoToolStripMenuItem.Name = "gestiónDeProductoToolStripMenuItem";
            gestiónDeProductoToolStripMenuItem.Size = new Size(219, 22);
            gestiónDeProductoToolStripMenuItem.Text = "Gestión de Producto";
            // 
            // solicitarOrdenDePedidoToolStripMenuItem
            // 
            solicitarOrdenDePedidoToolStripMenuItem.Name = "solicitarOrdenDePedidoToolStripMenuItem";
            solicitarOrdenDePedidoToolStripMenuItem.Size = new Size(249, 22);
            solicitarOrdenDePedidoToolStripMenuItem.Text = "Solicitar Orden de Pedido";
            // 
            // agregarStockToolStripMenuItem
            // 
            agregarStockToolStripMenuItem.Name = "agregarStockToolStripMenuItem";
            agregarStockToolStripMenuItem.Size = new Size(249, 22);
            agregarStockToolStripMenuItem.Text = "Agregar Stock";
            // 
            // historialDeMovimientosToolStripMenuItem
            // 
            historialDeMovimientosToolStripMenuItem.Name = "historialDeMovimientosToolStripMenuItem";
            historialDeMovimientosToolStripMenuItem.Size = new Size(249, 22);
            historialDeMovimientosToolStripMenuItem.Text = "Historial de Movimientos";
            // 
            // traspasoDeProductosASucursalToolStripMenuItem
            // 
            traspasoDeProductosASucursalToolStripMenuItem.Name = "traspasoDeProductosASucursalToolStripMenuItem";
            traspasoDeProductosASucursalToolStripMenuItem.Size = new Size(249, 22);
            traspasoDeProductosASucursalToolStripMenuItem.Text = "Traspaso de Productos a Sucursal";
            // 
            // verStockDisponibleToolStripMenuItem
            // 
            verStockDisponibleToolStripMenuItem.Name = "verStockDisponibleToolStripMenuItem";
            verStockDisponibleToolStripMenuItem.Size = new Size(249, 22);
            verStockDisponibleToolStripMenuItem.Text = "Ver Stock Disponible";
            // 
            // gestiónDeSucursalToolStripMenuItem
            // 
            gestiónDeSucursalToolStripMenuItem.Name = "gestiónDeSucursalToolStripMenuItem";
            gestiónDeSucursalToolStripMenuItem.Size = new Size(180, 22);
            gestiónDeSucursalToolStripMenuItem.Text = "Gestión de Sucursal";
            // 
            // solicitarTraspasoDeProductosToolStripMenuItem
            // 
            solicitarTraspasoDeProductosToolStripMenuItem.Name = "solicitarTraspasoDeProductosToolStripMenuItem";
            solicitarTraspasoDeProductosToolStripMenuItem.Size = new Size(294, 22);
            solicitarTraspasoDeProductosToolStripMenuItem.Text = "Solicitar Traspaso de Productos a Sucursal";
            // 
            // generarUnaVentaToolStripMenuItem
            // 
            generarUnaVentaToolStripMenuItem.Name = "generarUnaVentaToolStripMenuItem";
            generarUnaVentaToolStripMenuItem.Size = new Size(180, 22);
            generarUnaVentaToolStripMenuItem.Text = "Generar una Venta";
            // 
            // gestiónDeClienteToolStripMenuItem
            // 
            gestiónDeClienteToolStripMenuItem.Name = "gestiónDeClienteToolStripMenuItem";
            gestiónDeClienteToolStripMenuItem.Size = new Size(180, 22);
            gestiónDeClienteToolStripMenuItem.Text = "Gestión de Cliente";
            // 
            // gestiónDeVentasToolStripMenuItem
            // 
            gestiónDeVentasToolStripMenuItem.Name = "gestiónDeVentasToolStripMenuItem";
            gestiónDeVentasToolStripMenuItem.Size = new Size(180, 22);
            gestiónDeVentasToolStripMenuItem.Text = "Gestión de Ventas";
            // 
            // historialDeVentasToolStripMenuItem
            // 
            historialDeVentasToolStripMenuItem.Name = "historialDeVentasToolStripMenuItem";
            historialDeVentasToolStripMenuItem.Size = new Size(180, 22);
            historialDeVentasToolStripMenuItem.Text = "Historial de Ventas";
            // 
            // listaDePreciosToolStripMenuItem
            // 
            listaDePreciosToolStripMenuItem.Name = "listaDePreciosToolStripMenuItem";
            listaDePreciosToolStripMenuItem.Size = new Size(180, 22);
            listaDePreciosToolStripMenuItem.Text = "Lista de Precios";
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
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(470, 465);
            Controls.Add(button1);
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

        private Button button1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem administradorToolStripMenuItem;
        private ToolStripMenuItem compraToolStripMenuItem;
        private ToolStripMenuItem ventaToolStripMenuItem;
        private ToolStripMenuItem sucursalesToolStripMenuItem;
        private ToolStripMenuItem inventarioToolStripMenuItem;
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
    }
}