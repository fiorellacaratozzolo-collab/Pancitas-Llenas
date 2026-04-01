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
            btnVolver = new Button();
            groupBox1 = new GroupBox();
            cmbProductos = new ComboBox();
            cmbSucursalOrigen = new ComboBox();
            label2 = new Label();
            txtbPesoNeto = new TextBox();
            label1 = new Label();
            btnSolicitarTraspaso = new Button();
            dateTimePicker1 = new DateTimePicker();
            dgvItemsSolicitados = new DataGridView();
            label3 = new Label();
            txtbCantidad = new TextBox();
            btnAgregar = new Button();
            label4 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItemsSolicitados).BeginInit();
            SuspendLayout();
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(609, 825);
            btnVolver.Margin = new Padding(4, 3, 4, 3);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(88, 27);
            btnVolver.TabIndex = 21;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cmbProductos);
            groupBox1.Controls.Add(cmbSucursalOrigen);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtbPesoNeto);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnSolicitarTraspaso);
            groupBox1.Controls.Add(dateTimePicker1);
            groupBox1.Controls.Add(dgvItemsSolicitados);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtbCantidad);
            groupBox1.Controls.Add(btnAgregar);
            groupBox1.Controls.Add(label4);
            groupBox1.Location = new Point(14, 14);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(682, 804);
            groupBox1.TabIndex = 20;
            groupBox1.TabStop = false;
            groupBox1.Text = "Productos a Solicitar";
            // 
            // cmbProductos
            // 
            cmbProductos.FormattingEnabled = true;
            cmbProductos.Location = new Point(216, 145);
            cmbProductos.Name = "cmbProductos";
            cmbProductos.Size = new Size(256, 23);
            cmbProductos.TabIndex = 20;
            // 
            // cmbSucursalOrigen
            // 
            cmbSucursalOrigen.FormattingEnabled = true;
            cmbSucursalOrigen.Location = new Point(327, 83);
            cmbSucursalOrigen.Margin = new Padding(4, 3, 4, 3);
            cmbSucursalOrigen.Name = "cmbSucursalOrigen";
            cmbSucursalOrigen.Size = new Size(140, 23);
            cmbSucursalOrigen.TabIndex = 19;
            cmbSucursalOrigen.SelectedIndexChanged += cmbSucursalOrigen_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(190, 87);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(115, 15);
            label2.TabIndex = 16;
            label2.Text = "Sucursal a Traspasar:";
            // 
            // txtbPesoNeto
            // 
            txtbPesoNeto.Location = new Point(216, 182);
            txtbPesoNeto.Margin = new Padding(4, 3, 4, 3);
            txtbPesoNeto.Name = "txtbPesoNeto";
            txtbPesoNeto.Size = new Size(102, 23);
            txtbPesoNeto.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(139, 186);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 14;
            label1.Text = "Peso Neto:";
            // 
            // btnSolicitarTraspaso
            // 
            btnSolicitarTraspaso.Location = new Point(92, 753);
            btnSolicitarTraspaso.Margin = new Padding(4, 3, 4, 3);
            btnSolicitarTraspaso.Name = "btnSolicitarTraspaso";
            btnSolicitarTraspaso.Size = new Size(495, 44);
            btnSolicitarTraspaso.TabIndex = 13;
            btnSolicitarTraspaso.Text = "Solicitar Traspaso de Productos";
            btnSolicitarTraspaso.UseVisualStyleBackColor = true;
            btnSolicitarTraspaso.Click += btnSolicitarTraspaso_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(216, 22);
            dateTimePicker1.Margin = new Padding(4, 3, 4, 3);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(256, 23);
            dateTimePicker1.TabIndex = 4;
            // 
            // dgvItemsSolicitados
            // 
            dgvItemsSolicitados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItemsSolicitados.Location = new Point(7, 278);
            dgvItemsSolicitados.Margin = new Padding(4, 3, 4, 3);
            dgvItemsSolicitados.Name = "dgvItemsSolicitados";
            dgvItemsSolicitados.Size = new Size(668, 468);
            dgvItemsSolicitados.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(100, 148);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(106, 15);
            label3.TabIndex = 6;
            label3.Text = "Nombre Producto:";
            // 
            // txtbCantidad
            // 
            txtbCantidad.Location = new Point(216, 218);
            txtbCantidad.Margin = new Padding(4, 3, 4, 3);
            txtbCantidad.Name = "txtbCantidad";
            txtbCantidad.Size = new Size(102, 23);
            txtbCantidad.TabIndex = 9;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(359, 201);
            btnAgregar.Margin = new Padding(4, 3, 4, 3);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(217, 36);
            btnAgregar.TabIndex = 7;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(141, 222);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(58, 15);
            label4.TabIndex = 8;
            label4.Text = "Cantidad:";
            // 
            // FormSolicitarTraspasoProductoSucursales
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(708, 865);
            Controls.Add(btnVolver);
            Controls.Add(groupBox1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormSolicitarTraspasoProductoSucursales";
            Text = "Solicitar el Traspaso de Productos a otra Sucursal";
            Load += FormSolicitarTraspasoProductoSucursales_Load_1;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItemsSolicitados).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbSucursalOrigen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbPesoNeto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSolicitarTraspaso;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridView dgvItemsSolicitados;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtbCantidad;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label4;
        private ComboBox cmbProductos;
    }
}