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
            btnBuscar = new Button();
            datetimeVenta = new DateTimePicker();
            dataGridView1 = new DataGridView();
            groupBox1 = new GroupBox();
            radioButton1 = new RadioButton();
            txtbDescuento = new TextBox();
            groupBox3 = new GroupBox();
            txtbCliente = new TextBox();
            cmbMediodePago = new ComboBox();
            btnCancelar = new Button();
            label10 = new Label();
            btnAceptar = new Button();
            label11 = new Label();
            groupBox2 = new GroupBox();
            txtbPrecio = new TextBox();
            btnEliminar = new Button();
            btnAgregar = new Button();
            btnBuscarArt = new Button();
            label9 = new Label();
            txtbCantidad = new TextBox();
            txtbProducto = new TextBox();
            label8 = new Label();
            label7 = new Label();
            label5 = new Label();
            label6 = new Label();
            btnVolver = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(194, 20);
            btnBuscar.Margin = new Padding(4, 3, 4, 3);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(66, 27);
            btnBuscar.TabIndex = 2;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            // 
            // datetimeVenta
            // 
            datetimeVenta.Location = new Point(172, 22);
            datetimeVenta.Margin = new Padding(4, 3, 4, 3);
            datetimeVenta.Name = "datetimeVenta";
            datetimeVenta.Size = new Size(259, 23);
            datetimeVenta.TabIndex = 4;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(15, 346);
            dataGridView1.Margin = new Padding(4, 3, 4, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(572, 246);
            dataGridView1.TabIndex = 5;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Controls.Add(txtbDescuento);
            groupBox1.Controls.Add(btnCancelar);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Controls.Add(groupBox3);
            groupBox1.Controls.Add(cmbMediodePago);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(btnAceptar);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(datetimeVenta);
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Location = new Point(19, 25);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(610, 669);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Ventas";
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(20, 61);
            radioButton1.Margin = new Padding(4, 3, 4, 3);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(109, 19);
            radioButton1.TabIndex = 23;
            radioButton1.TabStop = true;
            radioButton1.Text = "Venta mayorista";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // txtbDescuento
            // 
            txtbDescuento.Location = new Point(468, 153);
            txtbDescuento.Margin = new Padding(4, 3, 4, 3);
            txtbDescuento.Name = "txtbDescuento";
            txtbDescuento.Size = new Size(116, 23);
            txtbDescuento.TabIndex = 14;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(btnBuscar);
            groupBox3.Controls.Add(txtbCliente);
            groupBox3.Location = new Point(20, 87);
            groupBox3.Margin = new Padding(4, 3, 4, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4, 3, 4, 3);
            groupBox3.Size = new Size(277, 53);
            groupBox3.TabIndex = 22;
            groupBox3.TabStop = false;
            groupBox3.Text = "Cliente";
            // 
            // txtbCliente
            // 
            txtbCliente.Location = new Point(8, 22);
            txtbCliente.Margin = new Padding(4, 3, 4, 3);
            txtbCliente.Name = "txtbCliente";
            txtbCliente.Size = new Size(178, 23);
            txtbCliente.TabIndex = 9;
            // 
            // cmbMediodePago
            // 
            cmbMediodePago.FormattingEnabled = true;
            cmbMediodePago.Location = new Point(468, 189);
            cmbMediodePago.Margin = new Padding(4, 3, 4, 3);
            cmbMediodePago.Name = "cmbMediodePago";
            cmbMediodePago.Size = new Size(119, 23);
            cmbMediodePago.TabIndex = 21;
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
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(390, 156);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(66, 15);
            label10.TabIndex = 15;
            label10.Text = "Descuento:";
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(259, 610);
            btnAceptar.Margin = new Padding(4, 3, 4, 3);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(209, 37);
            btnAceptar.TabIndex = 19;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(51, 621);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(78, 15);
            label11.TabIndex = 16;
            label11.Text = "Total a Pagar:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtbPrecio);
            groupBox2.Controls.Add(btnEliminar);
            groupBox2.Controls.Add(btnAgregar);
            groupBox2.Controls.Add(btnBuscarArt);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(txtbCantidad);
            groupBox2.Controls.Add(txtbProducto);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label7);
            groupBox2.Location = new Point(16, 153);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(328, 183);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "Producto";
            // 
            // txtbPrecio
            // 
            txtbPrecio.Location = new Point(99, 98);
            txtbPrecio.Margin = new Padding(4, 3, 4, 3);
            txtbPrecio.Name = "txtbPrecio";
            txtbPrecio.Size = new Size(116, 23);
            txtbPrecio.TabIndex = 18;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(185, 141);
            btnEliminar.Margin = new Padding(4, 3, 4, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(128, 33);
            btnEliminar.TabIndex = 17;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(23, 141);
            btnAgregar.Margin = new Padding(4, 3, 4, 3);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(128, 33);
            btnAgregar.TabIndex = 16;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            // 
            // btnBuscarArt
            // 
            btnBuscarArt.Location = new Point(223, 33);
            btnBuscarArt.Margin = new Padding(4, 3, 4, 3);
            btnBuscarArt.Name = "btnBuscarArt";
            btnBuscarArt.Size = new Size(59, 27);
            btnBuscarArt.TabIndex = 9;
            btnBuscarArt.Text = "Buscar";
            btnBuscarArt.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(41, 106);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(43, 15);
            label9.TabIndex = 14;
            label9.Text = "Precio:";
            // 
            // txtbCantidad
            // 
            txtbCantidad.Location = new Point(100, 69);
            txtbCantidad.Margin = new Padding(4, 3, 4, 3);
            txtbCantidad.Name = "txtbCantidad";
            txtbCantidad.Size = new Size(116, 23);
            txtbCantidad.TabIndex = 13;
            // 
            // txtbProducto
            // 
            txtbProducto.Location = new Point(99, 36);
            txtbProducto.Margin = new Padding(4, 3, 4, 3);
            txtbProducto.Name = "txtbProducto";
            txtbProducto.Size = new Size(116, 23);
            txtbProducto.TabIndex = 12;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(26, 77);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(58, 15);
            label8.TabIndex = 11;
            label8.Text = "Cantidad:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(30, 45);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(54, 15);
            label7.TabIndex = 10;
            label7.Text = "Nombre:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(365, 193);
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
            // btnVolver
            // 
            btnVolver.Location = new Point(19, 700);
            btnVolver.Margin = new Padding(4, 3, 4, 3);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(78, 28);
            btnVolver.TabIndex = 21;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            // 
            // FormGenerarVenta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(645, 739);
            Controls.Add(btnVolver);
            Controls.Add(groupBox1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormGenerarVenta";
            Text = "Registrar una nueva Venta";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DateTimePicker datetimeVenta;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnBuscarArt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtbCantidad;
        private System.Windows.Forms.TextBox txtbProducto;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtbCliente;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtbDescuento;
        private System.Windows.Forms.TextBox txtbPrecio;
        private System.Windows.Forms.ComboBox cmbMediodePago;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}