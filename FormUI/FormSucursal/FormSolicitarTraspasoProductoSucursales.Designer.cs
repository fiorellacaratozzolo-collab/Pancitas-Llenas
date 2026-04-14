namespace FormUI.FormSucursal
{
    partial class FormSolicitarTraspasoProductoSucursales
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
            groupBox1 = new GroupBox();
            txtbSucursalDestino = new TextBox();
            txtbMarca = new TextBox();
            label5 = new Label();
            cmbProducto = new ComboBox();
            cmbSucursalOrigen = new ComboBox();
            label2 = new Label();
            txtbPesoNeto = new TextBox();
            label1 = new Label();
            btnSolicitarTraspaso = new Button();
            dtpFechaSolicitud = new DateTimePicker();
            dgvProductos = new DataGridView();
            label3 = new Label();
            txtbCantidad = new TextBox();
            btnAgregar = new Button();
            label4 = new Label();
            label6 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(txtbSucursalDestino);
            groupBox1.Controls.Add(txtbMarca);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(cmbProducto);
            groupBox1.Controls.Add(cmbSucursalOrigen);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtbPesoNeto);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnSolicitarTraspaso);
            groupBox1.Controls.Add(dtpFechaSolicitud);
            groupBox1.Controls.Add(dgvProductos);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtbCantidad);
            groupBox1.Controls.Add(btnAgregar);
            groupBox1.Controls.Add(label4);
            groupBox1.Location = new Point(13, 6);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(592, 693);
            groupBox1.TabIndex = 20;
            groupBox1.TabStop = false;
            groupBox1.Text = "Productos a Solicitar";
            // 
            // txtbSucursalDestino
            // 
            txtbSucursalDestino.Location = new Point(174, 63);
            txtbSucursalDestino.Name = "txtbSucursalDestino";
            txtbSucursalDestino.ReadOnly = true;
            txtbSucursalDestino.Size = new Size(275, 23);
            txtbSucursalDestino.TabIndex = 23;
            // 
            // txtbMarca
            // 
            txtbMarca.Location = new Point(179, 204);
            txtbMarca.Name = "txtbMarca";
            txtbMarca.Size = new Size(102, 23);
            txtbMarca.TabIndex = 22;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(123, 212);
            label5.Name = "label5";
            label5.Size = new Size(43, 15);
            label5.TabIndex = 21;
            label5.Text = "Marca:";
            // 
            // cmbProducto
            // 
            cmbProducto.FormattingEnabled = true;
            cmbProducto.Location = new Point(176, 169);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(256, 23);
            cmbProducto.TabIndex = 20;
            cmbProducto.SelectedIndexChanged += cmbProducto_SelectedIndexChanged;
            // 
            // cmbSucursalOrigen
            // 
            cmbSucursalOrigen.FormattingEnabled = true;
            cmbSucursalOrigen.Location = new Point(174, 107);
            cmbSucursalOrigen.Margin = new Padding(4, 3, 4, 3);
            cmbSucursalOrigen.Name = "cmbSucursalOrigen";
            cmbSucursalOrigen.Size = new Size(275, 23);
            cmbSucursalOrigen.TabIndex = 19;
            cmbSucursalOrigen.SelectedIndexChanged += cmbSucursalOrigen_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(73, 110);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(93, 15);
            label2.TabIndex = 16;
            label2.Text = "Sucursal Origen:";
            // 
            // txtbPesoNeto
            // 
            txtbPesoNeto.Location = new Point(179, 236);
            txtbPesoNeto.Margin = new Padding(4, 3, 4, 3);
            txtbPesoNeto.Name = "txtbPesoNeto";
            txtbPesoNeto.Size = new Size(102, 23);
            txtbPesoNeto.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(102, 244);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 14;
            label1.Text = "Peso Neto:";
            // 
            // btnSolicitarTraspaso
            // 
            btnSolicitarTraspaso.Location = new Point(41, 639);
            btnSolicitarTraspaso.Margin = new Padding(4, 3, 4, 3);
            btnSolicitarTraspaso.Name = "btnSolicitarTraspaso";
            btnSolicitarTraspaso.Size = new Size(495, 44);
            btnSolicitarTraspaso.TabIndex = 13;
            btnSolicitarTraspaso.Text = "Solicitar Traspaso de Productos";
            btnSolicitarTraspaso.UseVisualStyleBackColor = true;
            btnSolicitarTraspaso.Click += btnSolicitarTraspaso_Click;
            // 
            // dtpFechaSolicitud
            // 
            dtpFechaSolicitud.Location = new Point(328, 22);
            dtpFechaSolicitud.Margin = new Padding(4, 3, 4, 3);
            dtpFechaSolicitud.Name = "dtpFechaSolicitud";
            dtpFechaSolicitud.Size = new Size(256, 23);
            dtpFechaSolicitud.TabIndex = 4;
            // 
            // dgvProductos
            // 
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Location = new Point(6, 314);
            dgvProductos.Margin = new Padding(4, 3, 4, 3);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.Size = new Size(578, 319);
            dgvProductos.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(60, 172);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(106, 15);
            label3.TabIndex = 6;
            label3.Text = "Nombre Producto:";
            // 
            // txtbCantidad
            // 
            txtbCantidad.Location = new Point(179, 271);
            txtbCantidad.Margin = new Padding(4, 3, 4, 3);
            txtbCantidad.Name = "txtbCantidad";
            txtbCantidad.Size = new Size(102, 23);
            txtbCantidad.TabIndex = 9;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(307, 263);
            btnAgregar.Margin = new Padding(4, 3, 4, 3);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(146, 36);
            btnAgregar.TabIndex = 7;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(108, 274);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(58, 15);
            label4.TabIndex = 8;
            label4.Text = "Cantidad:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(71, 71);
            label6.Name = "label6";
            label6.Size = new Size(97, 15);
            label6.TabIndex = 24;
            label6.Text = "Sucursal Destino:";
            // 
            // FormSolicitarTraspasoProductoSucursales
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(614, 706);
            Controls.Add(groupBox1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormSolicitarTraspasoProductoSucursales";
            Text = "Solicitar el Traspaso de Productos a otra Sucursal";
            Load += FormSolicitarTraspasoProductoSucursales_Load_1;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbSucursalOrigen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbPesoNeto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSolicitarTraspaso;
        private System.Windows.Forms.DateTimePicker dtpFechaSolicitud;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtbCantidad;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label4;
        private ComboBox cmbProducto;
        private TextBox txtbMarca;
        private Label label5;
        private TextBox txtbSucursalDestino;
        private Label label6;
    }
}