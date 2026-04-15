namespace FormUI.FormInventario
{
    partial class FormSolicitarOP
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
            cmbProducto = new ComboBox();
            txtbMarca = new TextBox();
            label1 = new Label();
            btnLimpiar = new Button();
            btnGuadar = new Button();
            dtpFecha = new DateTimePicker();
            txtbPesoNeto = new TextBox();
            dgvSolicitarOP = new DataGridView();
            label2 = new Label();
            label3 = new Label();
            txtbCantidad = new TextBox();
            btnAgregar = new Button();
            label4 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSolicitarOP).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cmbProducto);
            groupBox1.Controls.Add(txtbMarca);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnLimpiar);
            groupBox1.Controls.Add(btnGuadar);
            groupBox1.Controls.Add(dtpFecha);
            groupBox1.Controls.Add(txtbPesoNeto);
            groupBox1.Controls.Add(dgvSolicitarOP);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtbCantidad);
            groupBox1.Controls.Add(btnAgregar);
            groupBox1.Controls.Add(label4);
            groupBox1.Location = new Point(13, 12);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(731, 547);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Productos a Solicitar";
            // 
            // cmbProducto
            // 
            cmbProducto.FormattingEnabled = true;
            cmbProducto.Location = new Point(276, 97);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(266, 23);
            cmbProducto.TabIndex = 16;
            cmbProducto.SelectedIndexChanged += cmbProducto_SelectedIndexChanged;
            // 
            // txtbMarca
            // 
            txtbMarca.Location = new Point(275, 128);
            txtbMarca.Name = "txtbMarca";
            txtbMarca.Size = new Size(100, 23);
            txtbMarca.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(217, 131);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 14;
            label1.Text = "Marca:";
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(661, 174);
            btnLimpiar.Margin = new Padding(4, 3, 4, 3);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(62, 36);
            btnLimpiar.TabIndex = 10;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnGuadar
            // 
            btnGuadar.Location = new Point(113, 497);
            btnGuadar.Margin = new Padding(4, 3, 4, 3);
            btnGuadar.Name = "btnGuadar";
            btnGuadar.Size = new Size(495, 44);
            btnGuadar.TabIndex = 13;
            btnGuadar.Text = "Guardar";
            btnGuadar.UseVisualStyleBackColor = true;
            btnGuadar.Click += btnGuadar_Click;
            // 
            // dtpFecha
            // 
            dtpFecha.Location = new Point(166, 48);
            dtpFecha.Margin = new Padding(4, 3, 4, 3);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(233, 23);
            dtpFecha.TabIndex = 4;
            // 
            // txtbPesoNeto
            // 
            txtbPesoNeto.Location = new Point(275, 154);
            txtbPesoNeto.Margin = new Padding(4, 3, 4, 3);
            txtbPesoNeto.Name = "txtbPesoNeto";
            txtbPesoNeto.Size = new Size(102, 23);
            txtbPesoNeto.TabIndex = 2;
            // 
            // dgvSolicitarOP
            // 
            dgvSolicitarOP.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSolicitarOP.Location = new Point(7, 224);
            dgvSolicitarOP.Margin = new Padding(4, 3, 4, 3);
            dgvSolicitarOP.Name = "dgvSolicitarOP";
            dgvSolicitarOP.Size = new Size(716, 267);
            dgvSolicitarOP.TabIndex = 10;
            dgvSolicitarOP.CellContentClick += dgvSolicitarOP_CellContentClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(202, 157);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(64, 15);
            label2.TabIndex = 5;
            label2.Text = "Peso Neto:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(163, 100);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(106, 15);
            label3.TabIndex = 6;
            label3.Text = "Nombre Producto:";
            // 
            // txtbCantidad
            // 
            txtbCantidad.Location = new Point(275, 181);
            txtbCantidad.Margin = new Padding(4, 3, 4, 3);
            txtbCantidad.Name = "txtbCantidad";
            txtbCantidad.Size = new Size(102, 23);
            txtbCantidad.TabIndex = 9;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(416, 162);
            btnAgregar.Margin = new Padding(4, 3, 4, 3);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(100, 36);
            btnAgregar.TabIndex = 7;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(202, 185);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(58, 15);
            label4.TabIndex = 8;
            label4.Text = "Cantidad:";
            // 
            // FormSolicitarOP
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(757, 608);
            Controls.Add(groupBox1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormSolicitarOP";
            Text = "Solicitar Orden de Pedido";
            Load += FormSolicitarOP_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSolicitarOP).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnGuadar;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.TextBox txtbPesoNeto;
        private System.Windows.Forms.DataGridView dgvSolicitarOP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtbCantidad;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label4;
        private TextBox txtbMarca;
        private Label label1;
        private ComboBox cmbProducto;
    }
}