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
            FormGestiónRoles = new ToolStripMenuItem();
            FormGestiónUsuario = new ToolStripMenuItem();
            FormBitácora = new ToolStripMenuItem();
            tsmCompras = new ToolStripMenuItem();
            FormGestiónSP = new ToolStripMenuItem();
            FormGestiónOP = new ToolStripMenuItem();
            FormGestiónOC = new ToolStripMenuItem();
            FormGestiónProveedor = new ToolStripMenuItem();
            FormGestiónProducto = new ToolStripMenuItem();
            tsmInventario = new ToolStripMenuItem();
            FormSolicitarOP = new ToolStripMenuItem();
            FormAgregarStock = new ToolStripMenuItem();
            FormHistorialMovimientos = new ToolStripMenuItem();
            FormTraspasoProductosSucursal = new ToolStripMenuItem();
            FormVerStockDisponible = new ToolStripMenuItem();
            tsmSucursales = new ToolStripMenuItem();
            FormGestiónSucursal = new ToolStripMenuItem();
            FormSolicitarTraspasoProductoSucursales = new ToolStripMenuItem();
            FormGestiónTraspaso = new ToolStripMenuItem();
            tsmVenta = new ToolStripMenuItem();
            FormGenerarVenta = new ToolStripMenuItem();
            FormGestiónCliente = new ToolStripMenuItem();
            FormGestiónVenta = new ToolStripMenuItem();
            FormHistorialVentas = new ToolStripMenuItem();
            FormListaPrecios = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            panelContenedor = new Panel();
            lblInfoSucursal = new Label();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.Location = new Point(748, 827);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(102, 35);
            btnCerrarSesion.TabIndex = 0;
            btnCerrarSesion.Text = "Cerrar Sesión";
            btnCerrarSesion.UseVisualStyleBackColor = true;
            btnCerrarSesion.Click += button1_Click;
            // 
            // menuStrip
            // 
            menuStrip.BackColor = SystemColors.InactiveCaption;
            menuStrip.Font = new Font("Arial", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuStrip.Items.AddRange(new ToolStripItem[] { tsmAdministrador, tsmCompras, tsmInventario, tsmSucursales, tsmVenta });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1613, 30);
            menuStrip.TabIndex = 1;
            menuStrip.Tag = "MenuStrip";
            menuStrip.Text = "menuStrip1";
            menuStrip.ItemClicked += menuStrip1_ItemClicked;
            // 
            // tsmAdministrador
            // 
            tsmAdministrador.DropDownItems.AddRange(new ToolStripItem[] { FormGestiónRoles, FormGestiónUsuario, FormBitácora });
            tsmAdministrador.Name = "tsmAdministrador";
            tsmAdministrador.Size = new Size(139, 26);
            tsmAdministrador.Tag = "tsmAdministrador";
            tsmAdministrador.Text = "Administrador";
            // 
            // FormGestiónRoles
            // 
            FormGestiónRoles.Name = "FormGestiónRoles";
            FormGestiónRoles.Size = new Size(242, 26);
            FormGestiónRoles.Tag = "FormGestiónRoles";
            FormGestiónRoles.Text = "Gestión de Roles";
            FormGestiónRoles.Click += MenuPatente_Click;
            // 
            // FormGestiónUsuario
            // 
            FormGestiónUsuario.Name = "FormGestiónUsuario";
            FormGestiónUsuario.Size = new Size(242, 26);
            FormGestiónUsuario.Tag = "FormGestiónUsuario";
            FormGestiónUsuario.Text = "Gestión de Usuario";
            FormGestiónUsuario.Click += MenuPatente_Click;
            // 
            // FormBitácora
            // 
            FormBitácora.Name = "FormBitácora";
            FormBitácora.Size = new Size(242, 26);
            FormBitácora.Tag = "FormBitácora";
            FormBitácora.Text = "Bitácora";
            FormBitácora.Click += MenuPatente_Click;
            // 
            // tsmCompras
            // 
            tsmCompras.DropDownItems.AddRange(new ToolStripItem[] { FormGestiónSP, FormGestiónOP, FormGestiónOC, FormGestiónProveedor, FormGestiónProducto });
            tsmCompras.Name = "tsmCompras";
            tsmCompras.Size = new Size(100, 26);
            tsmCompras.Tag = "tsmCompras";
            tsmCompras.Text = "Compras";
            tsmCompras.Click += compraToolStripMenuItem_Click;
            // 
            // FormGestiónSP
            // 
            FormGestiónSP.Name = "FormGestiónSP";
            FormGestiónSP.Size = new Size(316, 26);
            FormGestiónSP.Tag = "FormGestiónSP";
            FormGestiónSP.Text = "Gestión Solicitud de Pedido";
            FormGestiónSP.Click += MenuPatente_Click;
            // 
            // FormGestiónOP
            // 
            FormGestiónOP.Name = "FormGestiónOP";
            FormGestiónOP.Size = new Size(316, 26);
            FormGestiónOP.Tag = "FormGestiónOP";
            FormGestiónOP.Text = "Gestión Orden de Pedido";
            FormGestiónOP.Click += MenuPatente_Click;
            // 
            // FormGestiónOC
            // 
            FormGestiónOC.Name = "FormGestiónOC";
            FormGestiónOC.Size = new Size(316, 26);
            FormGestiónOC.Tag = "FormGestiónOC";
            FormGestiónOC.Text = "Gestión Orden de Compra";
            FormGestiónOC.Click += MenuPatente_Click;
            // 
            // FormGestiónProveedor
            // 
            FormGestiónProveedor.Name = "FormGestiónProveedor";
            FormGestiónProveedor.Size = new Size(316, 26);
            FormGestiónProveedor.Tag = "FormGestiónProveedor";
            FormGestiónProveedor.Text = "Gestión de Proveedores";
            FormGestiónProveedor.Click += MenuPatente_Click;
            // 
            // FormGestiónProducto
            // 
            FormGestiónProducto.Name = "FormGestiónProducto";
            FormGestiónProducto.Size = new Size(316, 26);
            FormGestiónProducto.Tag = "FormGestiónProducto";
            FormGestiónProducto.Text = "Gestión de Producto";
            FormGestiónProducto.Click += MenuPatente_Click;
            // 
            // tsmInventario
            // 
            tsmInventario.DropDownItems.AddRange(new ToolStripItem[] { FormSolicitarOP, FormAgregarStock, FormHistorialMovimientos, FormTraspasoProductosSucursal, FormVerStockDisponible });
            tsmInventario.Name = "tsmInventario";
            tsmInventario.Size = new Size(104, 26);
            tsmInventario.Tag = "tsmInventario";
            tsmInventario.Text = "Inventario";
            // 
            // FormSolicitarOP
            // 
            FormSolicitarOP.Name = "FormSolicitarOP";
            FormSolicitarOP.Size = new Size(371, 26);
            FormSolicitarOP.Tag = "FormSolicitarOP";
            FormSolicitarOP.Text = "Solicitar Orden de Pedido";
            FormSolicitarOP.Click += MenuPatente_Click;
            // 
            // FormAgregarStock
            // 
            FormAgregarStock.Name = "FormAgregarStock";
            FormAgregarStock.Size = new Size(371, 26);
            FormAgregarStock.Tag = "FormAgregarStock";
            FormAgregarStock.Text = "Agregar Stock";
            FormAgregarStock.Click += MenuPatente_Click;
            // 
            // FormHistorialMovimientos
            // 
            FormHistorialMovimientos.Name = "FormHistorialMovimientos";
            FormHistorialMovimientos.Size = new Size(371, 26);
            FormHistorialMovimientos.Tag = "FormHistorialMovimientos";
            FormHistorialMovimientos.Text = "Historial de Movimientos";
            FormHistorialMovimientos.Click += MenuPatente_Click;
            // 
            // FormTraspasoProductosSucursal
            // 
            FormTraspasoProductosSucursal.Name = "FormTraspasoProductosSucursal";
            FormTraspasoProductosSucursal.Size = new Size(371, 26);
            FormTraspasoProductosSucursal.Tag = "FormTraspasoProductosSucursal";
            FormTraspasoProductosSucursal.Text = "Traspaso de Productos a Sucursal";
            FormTraspasoProductosSucursal.Click += MenuPatente_Click;
            // 
            // FormVerStockDisponible
            // 
            FormVerStockDisponible.Name = "FormVerStockDisponible";
            FormVerStockDisponible.Size = new Size(371, 26);
            FormVerStockDisponible.Tag = "FormVerStockDisponible";
            FormVerStockDisponible.Text = "Ver Stock Disponible";
            FormVerStockDisponible.Click += MenuPatente_Click;
            // 
            // tsmSucursales
            // 
            tsmSucursales.DropDownItems.AddRange(new ToolStripItem[] { FormGestiónSucursal, FormSolicitarTraspasoProductoSucursales, FormGestiónTraspaso });
            tsmSucursales.Name = "tsmSucursales";
            tsmSucursales.Size = new Size(116, 26);
            tsmSucursales.Tag = "tsmSucursales";
            tsmSucursales.Text = "Sucursales";
            tsmSucursales.Click += sucursalesToolStripMenuItem_Click;
            // 
            // FormGestiónSucursal
            // 
            FormGestiónSucursal.Name = "FormGestiónSucursal";
            FormGestiónSucursal.Size = new Size(443, 26);
            FormGestiónSucursal.Tag = "FormGestiónSucursal";
            FormGestiónSucursal.Text = "Gestión de Sucursal";
            FormGestiónSucursal.Click += MenuPatente_Click;
            // 
            // FormSolicitarTraspasoProductoSucursales
            // 
            FormSolicitarTraspasoProductoSucursales.Name = "FormSolicitarTraspasoProductoSucursales";
            FormSolicitarTraspasoProductoSucursales.Size = new Size(443, 26);
            FormSolicitarTraspasoProductoSucursales.Tag = "FormSolicitarTraspasoProductoSucursales";
            FormSolicitarTraspasoProductoSucursales.Text = "Solicitar Traspaso de Productos a Sucursal";
            FormSolicitarTraspasoProductoSucursales.Click += MenuPatente_Click;
            // 
            // FormGestiónTraspaso
            // 
            FormGestiónTraspaso.Name = "FormGestiónTraspaso";
            FormGestiónTraspaso.Size = new Size(443, 26);
            FormGestiónTraspaso.Tag = "FormGestiónTraspaso";
            FormGestiónTraspaso.Text = "Gestión de Traspaso";
            FormGestiónTraspaso.Click += MenuPatente_Click;
            // 
            // tsmVenta
            // 
            tsmVenta.DropDownItems.AddRange(new ToolStripItem[] { FormGenerarVenta, FormGestiónCliente, FormGestiónVenta, FormHistorialVentas, FormListaPrecios });
            tsmVenta.Name = "tsmVenta";
            tsmVenta.Size = new Size(70, 26);
            tsmVenta.Tag = "tsmVenta";
            tsmVenta.Text = "Venta";
            // 
            // FormGenerarVenta
            // 
            FormGenerarVenta.Name = "FormGenerarVenta";
            FormGenerarVenta.Size = new Size(237, 26);
            FormGenerarVenta.Tag = "FormGenerarVenta";
            FormGenerarVenta.Text = "Generar una Venta";
            FormGenerarVenta.Click += MenuPatente_Click;
            // 
            // FormGestiónCliente
            // 
            FormGestiónCliente.Name = "FormGestiónCliente";
            FormGestiónCliente.Size = new Size(237, 26);
            FormGestiónCliente.Tag = "FormGestiónCliente";
            FormGestiónCliente.Text = "Gestión de Cliente";
            FormGestiónCliente.Click += MenuPatente_Click;
            // 
            // FormGestiónVenta
            // 
            FormGestiónVenta.Name = "FormGestiónVenta";
            FormGestiónVenta.Size = new Size(237, 26);
            FormGestiónVenta.Tag = "FormGestiónVenta";
            FormGestiónVenta.Text = "Gestión de Ventas";
            FormGestiónVenta.Click += MenuPatente_Click;
            // 
            // FormHistorialVentas
            // 
            FormHistorialVentas.Name = "FormHistorialVentas";
            FormHistorialVentas.Size = new Size(237, 26);
            FormHistorialVentas.Tag = "FormHistorialVentas";
            FormHistorialVentas.Text = "Historial de Ventas";
            FormHistorialVentas.Click += MenuPatente_Click;
            // 
            // FormListaPrecios
            // 
            FormListaPrecios.Name = "FormListaPrecios";
            FormListaPrecios.Size = new Size(237, 26);
            FormListaPrecios.Tag = "FormListaPrecios";
            FormListaPrecios.Text = "Lista de Precios";
            FormListaPrecios.Click += MenuPatente_Click;
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
            // panelContenedor
            // 
            panelContenedor.BackColor = SystemColors.ControlLight;
            panelContenedor.Location = new Point(0, 55);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(1613, 766);
            panelContenedor.TabIndex = 2;
            // 
            // lblInfoSucursal
            // 
            lblInfoSucursal.AutoSize = true;
            lblInfoSucursal.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInfoSucursal.Location = new Point(805, 32);
            lblInfoSucursal.Name = "lblInfoSucursal";
            lblInfoSucursal.Size = new Size(149, 20);
            lblInfoSucursal.TabIndex = 3;
            lblInfoSucursal.Text = "Cargando sucursal...";
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1613, 874);
            Controls.Add(lblInfoSucursal);
            Controls.Add(panelContenedor);
            Controls.Add(btnCerrarSesion);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "FormPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
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
        private ToolStripMenuItem FormGestiónSP;
        private ToolStripMenuItem FormGestiónOP;
        private ToolStripMenuItem FormGestiónOC;
        private ToolStripMenuItem FormGestiónProveedor;
        private ToolStripMenuItem FormGestiónProducto;
        private ToolStripMenuItem FormSolicitarOP;
        private ToolStripMenuItem FormAgregarStock;
        private ToolStripMenuItem FormHistorialMovimientos;
        private ToolStripMenuItem FormTraspasoProductosSucursal;
        private ToolStripMenuItem FormVerStockDisponible;
        private ToolStripMenuItem FormGestiónSucursal;
        private ToolStripMenuItem FormSolicitarTraspasoProductoSucursales;
        private ToolStripMenuItem FormBitácora;
        private ToolStripMenuItem FormGestiónRoles;
        private ToolStripMenuItem FormGenerarVenta;
        private ToolStripMenuItem FormGestiónCliente;
        private ToolStripMenuItem FormGestiónVenta;
        private ToolStripMenuItem FormHistorialVentas;
        private ToolStripMenuItem FormListaPrecios;
        private ToolStripMenuItem FormGestiónTraspaso;
        private ToolStripMenuItem FormGestiónUsuario;
        private Panel panelContenedor;
        private Label lblInfoSucursal;
    }
}