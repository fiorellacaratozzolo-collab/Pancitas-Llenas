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
            menuStrip = new MenuStrip();
            tsmAdministrador = new ToolStripMenuItem();
            bitácoraToolStripMenuItem = new ToolStripMenuItem();
            gestiónDeRolesToolStripMenuItem = new ToolStripMenuItem();
            seleccionarSucursalToolStripMenuItem = new ToolStripMenuItem();
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
            tsmSucursales = new ToolStripMenuItem();
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
            gestiónDeUsuarioToolStripMenuItem = new ToolStripMenuItem();
            menuStrip.SuspendLayout();
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
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { tsmAdministrador, tsmCompras, tsmInventario, tsmSucursales, tsmVenta });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(470, 24);
            menuStrip.TabIndex = 1;
            menuStrip.Tag = "MenuStrip";
            menuStrip.Text = "menuStrip1";
            menuStrip.ItemClicked += menuStrip1_ItemClicked;
            // 
            // tsmAdministrador
            // 
            tsmAdministrador.DropDownItems.AddRange(new ToolStripItem[] { seleccionarSucursalToolStripMenuItem, gestiónDeRolesToolStripMenuItem, gestiónDeUsuarioToolStripMenuItem, bitácoraToolStripMenuItem });
            tsmAdministrador.Name = "tsmAdministrador";
            tsmAdministrador.Size = new Size(95, 20);
            tsmAdministrador.Tag = "tsmAdministrador";
            tsmAdministrador.Text = "Administrador";
            // 
            // bitácoraToolStripMenuItem
            // 
            bitácoraToolStripMenuItem.Name = "bitácoraToolStripMenuItem";
            bitácoraToolStripMenuItem.Size = new Size(181, 22);
            bitácoraToolStripMenuItem.Tag = "FormBitácora";
            bitácoraToolStripMenuItem.Text = "Bitácora";
            bitácoraToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // gestiónDeRolesToolStripMenuItem
            // 
            gestiónDeRolesToolStripMenuItem.Name = "gestiónDeRolesToolStripMenuItem";
            gestiónDeRolesToolStripMenuItem.Size = new Size(181, 22);
            gestiónDeRolesToolStripMenuItem.Tag = "FormGestiónRoles";
            gestiónDeRolesToolStripMenuItem.Text = "Gestión de Roles";
            gestiónDeRolesToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // seleccionarSucursalToolStripMenuItem
            // 
            seleccionarSucursalToolStripMenuItem.Name = "seleccionarSucursalToolStripMenuItem";
            seleccionarSucursalToolStripMenuItem.Size = new Size(181, 22);
            seleccionarSucursalToolStripMenuItem.Tag = "FormSeleccionSucursal";
            seleccionarSucursalToolStripMenuItem.Text = "Seleccionar Sucursal";
            seleccionarSucursalToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // tsmCompras
            // 
            tsmCompras.DropDownItems.AddRange(new ToolStripItem[] { gestiónSolicitudDePedidoToolStripMenuItem, gestiónOrdenDePedidoToolStripMenuItem, gestiónOrdenDeCompraToolStripMenuItem, gestiónDeProveedoresToolStripMenuItem, gestiónDeProductoToolStripMenuItem });
            tsmCompras.Name = "tsmCompras";
            tsmCompras.Size = new Size(67, 20);
            tsmCompras.Tag = "tsmCompras";
            tsmCompras.Text = "Compras";
            tsmCompras.Click += compraToolStripMenuItem_Click;
            // 
            // gestiónSolicitudDePedidoToolStripMenuItem
            // 
            gestiónSolicitudDePedidoToolStripMenuItem.Name = "gestiónSolicitudDePedidoToolStripMenuItem";
            gestiónSolicitudDePedidoToolStripMenuItem.Size = new Size(219, 22);
            gestiónSolicitudDePedidoToolStripMenuItem.Tag = "FormGestiónSP";
            gestiónSolicitudDePedidoToolStripMenuItem.Text = "Gestión Solicitud de Pedido";
            gestiónSolicitudDePedidoToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // gestiónOrdenDePedidoToolStripMenuItem
            // 
            gestiónOrdenDePedidoToolStripMenuItem.Name = "gestiónOrdenDePedidoToolStripMenuItem";
            gestiónOrdenDePedidoToolStripMenuItem.Size = new Size(219, 22);
            gestiónOrdenDePedidoToolStripMenuItem.Tag = "FormGestiónOP";
            gestiónOrdenDePedidoToolStripMenuItem.Text = "Gestión Orden de Pedido";
            gestiónOrdenDePedidoToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // gestiónOrdenDeCompraToolStripMenuItem
            // 
            gestiónOrdenDeCompraToolStripMenuItem.Name = "gestiónOrdenDeCompraToolStripMenuItem";
            gestiónOrdenDeCompraToolStripMenuItem.Size = new Size(219, 22);
            gestiónOrdenDeCompraToolStripMenuItem.Tag = "FormGestiónOC";
            gestiónOrdenDeCompraToolStripMenuItem.Text = "Gestión Orden de Compra";
            gestiónOrdenDeCompraToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // gestiónDeProveedoresToolStripMenuItem
            // 
            gestiónDeProveedoresToolStripMenuItem.Name = "gestiónDeProveedoresToolStripMenuItem";
            gestiónDeProveedoresToolStripMenuItem.Size = new Size(219, 22);
            gestiónDeProveedoresToolStripMenuItem.Tag = "FormGestiónProveedor";
            gestiónDeProveedoresToolStripMenuItem.Text = "Gestión de Proveedores";
            gestiónDeProveedoresToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // gestiónDeProductoToolStripMenuItem
            // 
            gestiónDeProductoToolStripMenuItem.Name = "gestiónDeProductoToolStripMenuItem";
            gestiónDeProductoToolStripMenuItem.Size = new Size(219, 22);
            gestiónDeProductoToolStripMenuItem.Tag = "FormGestiónProducto";
            gestiónDeProductoToolStripMenuItem.Text = "Gestión de Producto";
            gestiónDeProductoToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // tsmInventario
            // 
            tsmInventario.DropDownItems.AddRange(new ToolStripItem[] { solicitarOrdenDePedidoToolStripMenuItem, agregarStockToolStripMenuItem, historialDeMovimientosToolStripMenuItem, traspasoDeProductosASucursalToolStripMenuItem, verStockDisponibleToolStripMenuItem });
            tsmInventario.Name = "tsmInventario";
            tsmInventario.Size = new Size(72, 20);
            tsmInventario.Tag = "tsmInventario";
            tsmInventario.Text = "Inventario";
            // 
            // solicitarOrdenDePedidoToolStripMenuItem
            // 
            solicitarOrdenDePedidoToolStripMenuItem.Name = "solicitarOrdenDePedidoToolStripMenuItem";
            solicitarOrdenDePedidoToolStripMenuItem.Size = new Size(249, 22);
            solicitarOrdenDePedidoToolStripMenuItem.Tag = "FormSolicitarOP";
            solicitarOrdenDePedidoToolStripMenuItem.Text = "Solicitar Orden de Pedido";
            solicitarOrdenDePedidoToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // agregarStockToolStripMenuItem
            // 
            agregarStockToolStripMenuItem.Name = "agregarStockToolStripMenuItem";
            agregarStockToolStripMenuItem.Size = new Size(249, 22);
            agregarStockToolStripMenuItem.Tag = "FormAgregarStock";
            agregarStockToolStripMenuItem.Text = "Agregar Stock";
            agregarStockToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // historialDeMovimientosToolStripMenuItem
            // 
            historialDeMovimientosToolStripMenuItem.Name = "historialDeMovimientosToolStripMenuItem";
            historialDeMovimientosToolStripMenuItem.Size = new Size(249, 22);
            historialDeMovimientosToolStripMenuItem.Tag = "FormHistorialMovimientos";
            historialDeMovimientosToolStripMenuItem.Text = "Historial de Movimientos";
            historialDeMovimientosToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // traspasoDeProductosASucursalToolStripMenuItem
            // 
            traspasoDeProductosASucursalToolStripMenuItem.Name = "traspasoDeProductosASucursalToolStripMenuItem";
            traspasoDeProductosASucursalToolStripMenuItem.Size = new Size(249, 22);
            traspasoDeProductosASucursalToolStripMenuItem.Tag = "FormTraspasoProductosSucursal";
            traspasoDeProductosASucursalToolStripMenuItem.Text = "Traspaso de Productos a Sucursal";
            traspasoDeProductosASucursalToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // verStockDisponibleToolStripMenuItem
            // 
            verStockDisponibleToolStripMenuItem.Name = "verStockDisponibleToolStripMenuItem";
            verStockDisponibleToolStripMenuItem.Size = new Size(249, 22);
            verStockDisponibleToolStripMenuItem.Tag = "FormVerStockDisponible";
            verStockDisponibleToolStripMenuItem.Text = "Ver Stock Disponible";
            verStockDisponibleToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // tsmSucursales
            // 
            tsmSucursales.DropDownItems.AddRange(new ToolStripItem[] { gestiónDeSucursalToolStripMenuItem, solicitarTraspasoDeProductosToolStripMenuItem, gestiónDeTraspasoToolStripMenuItem });
            tsmSucursales.Name = "tsmSucursales";
            tsmSucursales.Size = new Size(74, 20);
            tsmSucursales.Tag = "tsmSucursales";
            tsmSucursales.Text = "Sucursales";
            tsmSucursales.Click += sucursalesToolStripMenuItem_Click;
            // 
            // gestiónDeSucursalToolStripMenuItem
            // 
            gestiónDeSucursalToolStripMenuItem.Name = "gestiónDeSucursalToolStripMenuItem";
            gestiónDeSucursalToolStripMenuItem.Size = new Size(294, 22);
            gestiónDeSucursalToolStripMenuItem.Tag = "FormGestiónSucursal";
            gestiónDeSucursalToolStripMenuItem.Text = "Gestión de Sucursal";
            gestiónDeSucursalToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // solicitarTraspasoDeProductosToolStripMenuItem
            // 
            solicitarTraspasoDeProductosToolStripMenuItem.Name = "solicitarTraspasoDeProductosToolStripMenuItem";
            solicitarTraspasoDeProductosToolStripMenuItem.Size = new Size(294, 22);
            solicitarTraspasoDeProductosToolStripMenuItem.Tag = "FormSolicitarTraspasoProductoSucursales";
            solicitarTraspasoDeProductosToolStripMenuItem.Text = "Solicitar Traspaso de Productos a Sucursal";
            solicitarTraspasoDeProductosToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // gestiónDeTraspasoToolStripMenuItem
            // 
            gestiónDeTraspasoToolStripMenuItem.Name = "gestiónDeTraspasoToolStripMenuItem";
            gestiónDeTraspasoToolStripMenuItem.Size = new Size(294, 22);
            gestiónDeTraspasoToolStripMenuItem.Tag = "FormGestiónTraspaso";
            gestiónDeTraspasoToolStripMenuItem.Text = "Gestión de Traspaso";
            gestiónDeTraspasoToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // tsmVenta
            // 
            tsmVenta.DropDownItems.AddRange(new ToolStripItem[] { generarUnaVentaToolStripMenuItem, gestiónDeClienteToolStripMenuItem, gestiónDeVentasToolStripMenuItem, historialDeVentasToolStripMenuItem, listaDePreciosToolStripMenuItem });
            tsmVenta.Name = "tsmVenta";
            tsmVenta.Size = new Size(48, 20);
            tsmVenta.Tag = "tsmVenta";
            tsmVenta.Text = "Venta";
            // 
            // generarUnaVentaToolStripMenuItem
            // 
            generarUnaVentaToolStripMenuItem.Name = "generarUnaVentaToolStripMenuItem";
            generarUnaVentaToolStripMenuItem.Size = new Size(171, 22);
            generarUnaVentaToolStripMenuItem.Tag = "FormGenerarVenta";
            generarUnaVentaToolStripMenuItem.Text = "Generar una Venta";
            generarUnaVentaToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // gestiónDeClienteToolStripMenuItem
            // 
            gestiónDeClienteToolStripMenuItem.Name = "gestiónDeClienteToolStripMenuItem";
            gestiónDeClienteToolStripMenuItem.Size = new Size(171, 22);
            gestiónDeClienteToolStripMenuItem.Tag = "FormGestiónCliente";
            gestiónDeClienteToolStripMenuItem.Text = "Gestión de Cliente";
            gestiónDeClienteToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // gestiónDeVentasToolStripMenuItem
            // 
            gestiónDeVentasToolStripMenuItem.Name = "gestiónDeVentasToolStripMenuItem";
            gestiónDeVentasToolStripMenuItem.Size = new Size(171, 22);
            gestiónDeVentasToolStripMenuItem.Tag = "FormGestiónVenta";
            gestiónDeVentasToolStripMenuItem.Text = "Gestión de Ventas";
            gestiónDeVentasToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // historialDeVentasToolStripMenuItem
            // 
            historialDeVentasToolStripMenuItem.Name = "historialDeVentasToolStripMenuItem";
            historialDeVentasToolStripMenuItem.Size = new Size(171, 22);
            historialDeVentasToolStripMenuItem.Tag = "FormHistorialVentas";
            historialDeVentasToolStripMenuItem.Text = "Historial de Ventas";
            historialDeVentasToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // listaDePreciosToolStripMenuItem
            // 
            listaDePreciosToolStripMenuItem.Name = "listaDePreciosToolStripMenuItem";
            listaDePreciosToolStripMenuItem.Size = new Size(171, 22);
            listaDePreciosToolStripMenuItem.Tag = "FormListaPrecios";
            listaDePreciosToolStripMenuItem.Text = "Lista de Precios";
            listaDePreciosToolStripMenuItem.Click += MenuPatente_Click;
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
            // gestiónDeUsuarioToolStripMenuItem
            // 
            gestiónDeUsuarioToolStripMenuItem.Name = "gestiónDeUsuarioToolStripMenuItem";
            gestiónDeUsuarioToolStripMenuItem.Size = new Size(181, 22);
            gestiónDeUsuarioToolStripMenuItem.Tag = "FormGestiónUsuario";
            gestiónDeUsuarioToolStripMenuItem.Text = "Gestión de Usuario";
            gestiónDeUsuarioToolStripMenuItem.Click += MenuPatente_Click;
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(470, 465);
            Controls.Add(btnCerrarSesion);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "FormPrincipal";
            Text = "Menú";
            Load += FormPrincipal_Load;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCerrarSesion;
        private MenuStrip menuStrip;
        private ToolStripMenuItem tsmAdministrador;
        private ToolStripMenuItem tsmCompras;
        private ToolStripMenuItem tsmVenta;
        private ToolStripMenuItem tsmSucursales;
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
        private ToolStripMenuItem seleccionarSucursalToolStripMenuItem;
        private ToolStripMenuItem gestiónDeUsuarioToolStripMenuItem;
    }
}