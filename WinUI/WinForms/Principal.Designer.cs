namespace WinUI.WinForms
{
    partial class Principal
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmAdministrador = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiGestionarRol = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmVentas = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNuevaVenta = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiGestionarVenta = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiGestionarCliente = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiListaPrecios = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCompras = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiGestionSolicitudOP = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiGestionOP = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiGestionOC = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGestionProducto = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiGestionProveedor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmInventario = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiVerStockDisponible = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiAgregarStock = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHistorialMovimientos = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiTraspasarProdSucursal = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiSolicitarOP = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsSucursal = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiGestionSucursal = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiSolicitarTraspasoProd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmInformes = new System.Windows.Forms.ToolStripMenuItem();
            this.tmsiGestionInformes = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.msiHistorialVentas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAdministrador,
            this.tsmVentas,
            this.tsmCompras,
            this.tsmInventario,
            this.tmsSucursal,
            this.tsmInformes});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(450, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "msMenu";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // tsmAdministrador
            // 
            this.tsmAdministrador.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsiGestionarRol});
            this.tsmAdministrador.Name = "tsmAdministrador";
            this.tsmAdministrador.Size = new System.Drawing.Size(95, 20);
            this.tsmAdministrador.Text = "Administrador";
            this.tsmAdministrador.Visible = false;
            this.tsmAdministrador.Click += new System.EventHandler(this.administradosToolStripMenuItem_Click);
            // 
            // tmsiGestionarRol
            // 
            this.tmsiGestionarRol.Name = "tmsiGestionarRol";
            this.tmsiGestionarRol.Size = new System.Drawing.Size(180, 22);
            this.tmsiGestionarRol.Text = "Gestionar Roles";
            // 
            // tsmVentas
            // 
            this.tsmVentas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNuevaVenta,
            this.tmsiGestionarVenta,
            this.tmsiGestionarCliente,
            this.tmsiListaPrecios,
            this.msiHistorialVentas});
            this.tsmVentas.Name = "tsmVentas";
            this.tsmVentas.Size = new System.Drawing.Size(53, 20);
            this.tsmVentas.Text = "Ventas";
            this.tsmVentas.Visible = false;
            // 
            // tsmiNuevaVenta
            // 
            this.tsmiNuevaVenta.Name = "tsmiNuevaVenta";
            this.tsmiNuevaVenta.Size = new System.Drawing.Size(180, 22);
            this.tsmiNuevaVenta.Text = "Nueva Venta";
            // 
            // tmsiGestionarVenta
            // 
            this.tmsiGestionarVenta.Name = "tmsiGestionarVenta";
            this.tmsiGestionarVenta.Size = new System.Drawing.Size(180, 22);
            this.tmsiGestionarVenta.Text = "Gestionar Venta";
            // 
            // tmsiGestionarCliente
            // 
            this.tmsiGestionarCliente.Name = "tmsiGestionarCliente";
            this.tmsiGestionarCliente.Size = new System.Drawing.Size(180, 22);
            this.tmsiGestionarCliente.Text = "Gestionar Cliente";
            // 
            // tmsiListaPrecios
            // 
            this.tmsiListaPrecios.Name = "tmsiListaPrecios";
            this.tmsiListaPrecios.Size = new System.Drawing.Size(180, 22);
            this.tmsiListaPrecios.Text = "Lista de Precios";
            // 
            // tsmCompras
            // 
            this.tsmCompras.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsiGestionSolicitudOP,
            this.tmsiGestionOP,
            this.tmsiGestionOC,
            this.tsmiGestionProducto,
            this.tmsiGestionProveedor});
            this.tsmCompras.Name = "tsmCompras";
            this.tsmCompras.Size = new System.Drawing.Size(67, 20);
            this.tsmCompras.Text = "Compras";
            this.tsmCompras.Visible = false;
            // 
            // tmsiGestionSolicitudOP
            // 
            this.tmsiGestionSolicitudOP.Name = "tmsiGestionSolicitudOP";
            this.tmsiGestionSolicitudOP.Size = new System.Drawing.Size(229, 22);
            this.tmsiGestionSolicitudOP.Text = "Gestionar Solicitud de Pedido";
            // 
            // tmsiGestionOP
            // 
            this.tmsiGestionOP.Name = "tmsiGestionOP";
            this.tmsiGestionOP.Size = new System.Drawing.Size(229, 22);
            this.tmsiGestionOP.Text = "Gestionar Orden de Pedido";
            // 
            // tmsiGestionOC
            // 
            this.tmsiGestionOC.Name = "tmsiGestionOC";
            this.tmsiGestionOC.Size = new System.Drawing.Size(229, 22);
            this.tmsiGestionOC.Text = "Gestionar Orden de Compra";
            // 
            // tsmiGestionProducto
            // 
            this.tsmiGestionProducto.Name = "tsmiGestionProducto";
            this.tsmiGestionProducto.Size = new System.Drawing.Size(229, 22);
            this.tsmiGestionProducto.Text = "Gestionar Productos";
            // 
            // tmsiGestionProveedor
            // 
            this.tmsiGestionProveedor.Name = "tmsiGestionProveedor";
            this.tmsiGestionProveedor.Size = new System.Drawing.Size(229, 22);
            this.tmsiGestionProveedor.Text = "Gestionar Proveedores";
            // 
            // tsmInventario
            // 
            this.tsmInventario.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsiVerStockDisponible,
            this.tmsiAgregarStock,
            this.tsmiHistorialMovimientos,
            this.tmsiTraspasarProdSucursal,
            this.tmsiSolicitarOP});
            this.tsmInventario.Name = "tsmInventario";
            this.tsmInventario.Size = new System.Drawing.Size(72, 20);
            this.tsmInventario.Text = "Inventario";
            this.tsmInventario.Visible = false;
            // 
            // tmsiVerStockDisponible
            // 
            this.tmsiVerStockDisponible.Name = "tmsiVerStockDisponible";
            this.tmsiVerStockDisponible.Size = new System.Drawing.Size(236, 22);
            this.tmsiVerStockDisponible.Text = "Ver Stock Disponible";
            // 
            // tmsiAgregarStock
            // 
            this.tmsiAgregarStock.Name = "tmsiAgregarStock";
            this.tmsiAgregarStock.Size = new System.Drawing.Size(236, 22);
            this.tmsiAgregarStock.Text = "Agregar Stock";
            this.tmsiAgregarStock.Click += new System.EventHandler(this.verStockSucursalToolStripMenuItem_Click);
            // 
            // tsmiHistorialMovimientos
            // 
            this.tsmiHistorialMovimientos.Name = "tsmiHistorialMovimientos";
            this.tsmiHistorialMovimientos.Size = new System.Drawing.Size(236, 22);
            this.tsmiHistorialMovimientos.Text = "Historial de Movimientos";
            // 
            // tmsiTraspasarProdSucursal
            // 
            this.tmsiTraspasarProdSucursal.Name = "tmsiTraspasarProdSucursal";
            this.tmsiTraspasarProdSucursal.Size = new System.Drawing.Size(236, 22);
            this.tmsiTraspasarProdSucursal.Text = "Traspasar Productos a Sucursal";
            // 
            // tmsiSolicitarOP
            // 
            this.tmsiSolicitarOP.Name = "tmsiSolicitarOP";
            this.tmsiSolicitarOP.Size = new System.Drawing.Size(236, 22);
            this.tmsiSolicitarOP.Text = "Solicitar Orden de Pedido";
            this.tmsiSolicitarOP.Click += new System.EventHandler(this.solicitudDeOrdenDePedidoToolStripMenuItem_Click);
            // 
            // tmsSucursal
            // 
            this.tmsSucursal.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsiGestionSucursal,
            this.tmsiSolicitarTraspasoProd});
            this.tmsSucursal.Name = "tmsSucursal";
            this.tmsSucursal.Size = new System.Drawing.Size(63, 20);
            this.tmsSucursal.Text = "Sucursal";
            this.tmsSucursal.Visible = false;
            // 
            // tmsiGestionSucursal
            // 
            this.tmsiGestionSucursal.Name = "tmsiGestionSucursal";
            this.tmsiGestionSucursal.Size = new System.Drawing.Size(238, 22);
            this.tmsiGestionSucursal.Text = "Gestionar Sucursal";
            // 
            // tmsiSolicitarTraspasoProd
            // 
            this.tmsiSolicitarTraspasoProd.Name = "tmsiSolicitarTraspasoProd";
            this.tmsiSolicitarTraspasoProd.Size = new System.Drawing.Size(238, 22);
            this.tmsiSolicitarTraspasoProd.Text = "Solicitar Traspaso de Productos";
            // 
            // tsmInformes
            // 
            this.tsmInformes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmsiGestionInformes});
            this.tsmInformes.Name = "tsmInformes";
            this.tsmInformes.Size = new System.Drawing.Size(66, 20);
            this.tsmInformes.Text = "Informes";
            this.tsmInformes.Visible = false;
            // 
            // tmsiGestionInformes
            // 
            this.tmsiGestionInformes.Name = "tmsiGestionInformes";
            this.tmsiGestionInformes.Size = new System.Drawing.Size(180, 22);
            this.tmsiGestionInformes.Text = "Gestión de Informes";
            this.tmsiGestionInformes.Click += new System.EventHandler(this.gestiónToolStripMenuItem_Click);
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Location = new System.Drawing.Point(350, 411);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(88, 28);
            this.btnCerrarSesion.TabIndex = 1;
            this.btnCerrarSesion.Text = "Cerrar Sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = true;
            // 
            // msiHistorialVentas
            // 
            this.msiHistorialVentas.Name = "msiHistorialVentas";
            this.msiHistorialVentas.Size = new System.Drawing.Size(180, 22);
            this.msiHistorialVentas.Text = "Historial de Ventas";
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 451);
            this.Controls.Add(this.btnCerrarSesion);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Principal";
            this.Load += new System.EventHandler(this.Principal_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmVentas;
        private System.Windows.Forms.ToolStripMenuItem tsmiNuevaVenta;
        private System.Windows.Forms.ToolStripMenuItem tmsiGestionarVenta;
        private System.Windows.Forms.ToolStripMenuItem tsmInformes;
        private System.Windows.Forms.ToolStripMenuItem tsmAdministrador;
        private System.Windows.Forms.ToolStripMenuItem tsmCompras;
        private System.Windows.Forms.ToolStripMenuItem tsmInventario;
        private System.Windows.Forms.ToolStripMenuItem tmsiGestionInformes;
        private System.Windows.Forms.ToolStripMenuItem tmsiGestionarCliente;
        private System.Windows.Forms.ToolStripMenuItem tmsiVerStockDisponible;
        private System.Windows.Forms.ToolStripMenuItem tmsiAgregarStock;
        private System.Windows.Forms.ToolStripMenuItem tsmiHistorialMovimientos;
        private System.Windows.Forms.ToolStripMenuItem tmsiTraspasarProdSucursal;
        private System.Windows.Forms.ToolStripMenuItem tmsiSolicitarOP;
        private System.Windows.Forms.ToolStripMenuItem tmsiGestionSolicitudOP;
        private System.Windows.Forms.ToolStripMenuItem tmsiGestionOC;
        private System.Windows.Forms.ToolStripMenuItem tmsiGestionOP;
        private System.Windows.Forms.ToolStripMenuItem tsmiGestionProducto;
        private System.Windows.Forms.ToolStripMenuItem tmsiGestionProveedor;
        private System.Windows.Forms.ToolStripMenuItem tmsSucursal;
        private System.Windows.Forms.ToolStripMenuItem tmsiGestionSucursal;
        private System.Windows.Forms.ToolStripMenuItem tmsiGestionarRol;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.ToolStripMenuItem tmsiSolicitarTraspasoProd;
        private System.Windows.Forms.ToolStripMenuItem tmsiListaPrecios;
        private System.Windows.Forms.ToolStripMenuItem msiHistorialVentas;
    }
}