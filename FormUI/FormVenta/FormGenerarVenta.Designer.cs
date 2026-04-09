namespace FormUI.FormVenta
{
    partial class FormGenerarVenta
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
            datetimeVenta = new DateTimePicker();
            dgvVentas = new DataGridView();
            groupBox1 = new GroupBox();
            chkMayorista = new CheckBox();
            lblTotal = new Label();
            lblDescuento = new Label();
            btnDeshacer = new Button();
            txtbDescuento = new TextBox();
            btnCancelar = new Button();
            groupBox2 = new GroupBox();
            txtbPesoNeto = new TextBox();
            label1 = new Label();
            cmbProducto = new ComboBox();
            txtbPrecioProd = new TextBox();
            btnAgregar = new Button();
            btnBuscarProd = new Button();
            label9 = new Label();
            txtbCantidadProd = new TextBox();
            label8 = new Label();
            label7 = new Label();
            groupBox3 = new GroupBox();
            cmbCliente = new ComboBox();
            cmbPago = new ComboBox();
            label10 = new Label();
            btnAceptar = new Button();
            lblSubtotal = new Label();
            label5 = new Label();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // datetimeVenta
            // 
            datetimeVenta.Location = new Point(172, 22);
            datetimeVenta.Margin = new Padding(4, 3, 4, 3);
            datetimeVenta.Name = "datetimeVenta";
            datetimeVenta.Size = new Size(259, 23);
            datetimeVenta.TabIndex = 4;
            // 
            // dgvVentas
            // 
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVentas.Location = new Point(15, 346);
            dgvVentas.Margin = new Padding(4, 3, 4, 3);
            dgvVentas.Name = "dgvVentas";
            dgvVentas.Size = new Size(572, 246);
            dgvVentas.TabIndex = 5;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(chkMayorista);
            groupBox1.Controls.Add(lblTotal);
            groupBox1.Controls.Add(lblDescuento);
            groupBox1.Controls.Add(btnDeshacer);
            groupBox1.Controls.Add(txtbDescuento);
            groupBox1.Controls.Add(btnCancelar);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Controls.Add(groupBox3);
            groupBox1.Controls.Add(cmbPago);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(btnAceptar);
            groupBox1.Controls.Add(lblSubtotal);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(datetimeVenta);
            groupBox1.Controls.Add(dgvVentas);
            groupBox1.Location = new Point(19, 25);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(610, 686);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Ventas";
            // 
            // chkMayorista
            // 
            chkMayorista.AutoSize = true;
            chkMayorista.Location = new Point(334, 87);
            chkMayorista.Name = "chkMayorista";
            chkMayorista.Size = new Size(110, 19);
            chkMayorista.TabIndex = 26;
            chkMayorista.Text = "Venta Mayorista";
            chkMayorista.UseVisualStyleBackColor = true;
            chkMayorista.CheckedChanged += chkMayorista_CheckedChanged;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(16, 657);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(36, 15);
            lblTotal.TabIndex = 25;
            lblTotal.Text = "Total:";
            // 
            // lblDescuento
            // 
            lblDescuento.AutoSize = true;
            lblDescuento.Location = new Point(16, 632);
            lblDescuento.Name = "lblDescuento";
            lblDescuento.Size = new Size(66, 15);
            lblDescuento.TabIndex = 24;
            lblDescuento.Text = "Descuento:";
            // 
            // btnDeshacer
            // 
            btnDeshacer.Location = new Point(425, 307);
            btnDeshacer.Margin = new Padding(4, 3, 4, 3);
            btnDeshacer.Name = "btnDeshacer";
            btnDeshacer.Size = new Size(128, 33);
            btnDeshacer.TabIndex = 17;
            btnDeshacer.Text = "Deshacer";
            btnDeshacer.UseVisualStyleBackColor = true;
            btnDeshacer.Click += btnDeshacer_Click;
            // 
            // txtbDescuento
            // 
            txtbDescuento.Location = new Point(405, 164);
            txtbDescuento.Margin = new Padding(4, 3, 4, 3);
            txtbDescuento.Name = "txtbDescuento";
            txtbDescuento.Size = new Size(116, 23);
            txtbDescuento.TabIndex = 14;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(476, 610);
            btnCancelar.Margin = new Padding(4, 3, 4, 3);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(111, 37);
            btnCancelar.TabIndex = 20;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtbPesoNeto);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(cmbProducto);
            groupBox2.Controls.Add(txtbPrecioProd);
            groupBox2.Controls.Add(btnAgregar);
            groupBox2.Controls.Add(btnBuscarProd);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(txtbCantidadProd);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label7);
            groupBox2.Location = new Point(16, 146);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(350, 190);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "Producto";
            // 
            // txtbPesoNeto
            // 
            txtbPesoNeto.Location = new Point(79, 59);
            txtbPesoNeto.Name = "txtbPesoNeto";
            txtbPesoNeto.Size = new Size(116, 23);
            txtbPesoNeto.TabIndex = 21;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 62);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 20;
            label1.Text = "Peso Neto:";
            // 
            // cmbProducto
            // 
            cmbProducto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbProducto.FormattingEnabled = true;
            cmbProducto.Location = new Point(75, 25);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(220, 23);
            cmbProducto.TabIndex = 19;
            cmbProducto.SelectedIndexChanged += cmbProducto_SelectedIndexChanged;
            // 
            // txtbPrecioProd
            // 
            txtbPrecioProd.Location = new Point(78, 117);
            txtbPrecioProd.Margin = new Padding(4, 3, 4, 3);
            txtbPrecioProd.Name = "txtbPrecioProd";
            txtbPrecioProd.Size = new Size(116, 23);
            txtbPrecioProd.TabIndex = 18;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(38, 151);
            btnAgregar.Margin = new Padding(4, 3, 4, 3);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(280, 33);
            btnAgregar.TabIndex = 16;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnBuscarProd
            // 
            btnBuscarProd.Location = new Point(306, 22);
            btnBuscarProd.Margin = new Padding(4, 3, 4, 3);
            btnBuscarProd.Name = "btnBuscarProd";
            btnBuscarProd.Size = new Size(36, 27);
            btnBuscarProd.TabIndex = 9;
            btnBuscarProd.Text = "Buscar";
            btnBuscarProd.UseVisualStyleBackColor = true;
            btnBuscarProd.Click += btnBuscarProd_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(27, 120);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(43, 15);
            label9.TabIndex = 14;
            label9.Text = "Precio:";
            // 
            // txtbCantidadProd
            // 
            txtbCantidadProd.Location = new Point(79, 88);
            txtbCantidadProd.Margin = new Padding(4, 3, 4, 3);
            txtbCantidadProd.Name = "txtbCantidadProd";
            txtbCantidadProd.Size = new Size(116, 23);
            txtbCantidadProd.TabIndex = 13;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(15, 91);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(58, 15);
            label8.TabIndex = 11;
            label8.Text = "Cantidad:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 30);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(54, 15);
            label7.TabIndex = 10;
            label7.Text = "Nombre:";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(cmbCliente);
            groupBox3.Location = new Point(20, 87);
            groupBox3.Margin = new Padding(4, 3, 4, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4, 3, 4, 3);
            groupBox3.Size = new Size(277, 53);
            groupBox3.TabIndex = 22;
            groupBox3.TabStop = false;
            groupBox3.Text = "Cliente";
            // 
            // cmbCliente
            // 
            cmbCliente.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbCliente.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbCliente.FormattingEnabled = true;
            cmbCliente.Location = new Point(16, 22);
            cmbCliente.Name = "cmbCliente";
            cmbCliente.Size = new Size(254, 23);
            cmbCliente.TabIndex = 0;
            // 
            // cmbPago
            // 
            cmbPago.FormattingEnabled = true;
            cmbPago.Location = new Point(402, 217);
            cmbPago.Margin = new Padding(4, 3, 4, 3);
            cmbPago.Name = "cmbPago";
            cmbPago.Size = new Size(119, 23);
            cmbPago.TabIndex = 21;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(402, 146);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(66, 15);
            label10.TabIndex = 15;
            label10.Text = "Descuento:";
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(299, 610);
            btnAceptar.Margin = new Padding(4, 3, 4, 3);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(169, 37);
            btnAceptar.TabIndex = 19;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // lblSubtotal
            // 
            lblSubtotal.AutoSize = true;
            lblSubtotal.Location = new Point(15, 610);
            lblSubtotal.Margin = new Padding(4, 0, 4, 0);
            lblSubtotal.Name = "lblSubtotal";
            lblSubtotal.Size = new Size(54, 15);
            lblSubtotal.TabIndex = 16;
            lblSubtotal.Text = "Subtotal:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(402, 199);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(90, 15);
            label5.TabIndex = 9;
            label5.Text = "Medio de Pago:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(116, 29);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(41, 15);
            label6.TabIndex = 9;
            label6.Text = "Fecha:";
            // 
            // FormGenerarVenta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(639, 723);
            Controls.Add(groupBox1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormGenerarVenta";
            Text = "Registrar una nueva Venta";
            Load += FormGenerarVenta_Load_1;
            ((System.ComponentModel.ISupportInitialize)dgvVentas).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DateTimePicker datetimeVenta;
        private System.Windows.Forms.DataGridView dgvVentas;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDeshacer;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnBuscarProd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtbCantidadProd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.TextBox txtbDescuento;
        private System.Windows.Forms.TextBox txtbPrecioProd;
        private System.Windows.Forms.ComboBox cmbPago;
        private System.Windows.Forms.GroupBox groupBox3;
        private ComboBox cmbCliente;
        private ComboBox cmbProducto;
        private Label lblTotal;
        private Label lblDescuento;
        private CheckBox chkMayorista;
        private TextBox txtbPesoNeto;
        private Label label1;
    }
}