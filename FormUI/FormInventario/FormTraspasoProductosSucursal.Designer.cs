namespace FormUI.FormInventario
{
    partial class FormTraspasoProductosSucursal
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
            cmbSucursal = new ComboBox();
            cmbDeposito = new ComboBox();
            label5 = new Label();
            label2 = new Label();
            txtbPesoNeto = new TextBox();
            label1 = new Label();
            btnGenerarTraspasoProd = new Button();
            dateTimePicker1 = new DateTimePicker();
            dataGridView1 = new DataGridView();
            txtbProd = new TextBox();
            label3 = new Label();
            txtbCantidad = new TextBox();
            btnAgregar = new Button();
            label4 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cmbSucursal);
            groupBox1.Controls.Add(cmbDeposito);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtbPesoNeto);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnGenerarTraspasoProd);
            groupBox1.Controls.Add(dateTimePicker1);
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Controls.Add(txtbProd);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtbCantidad);
            groupBox1.Controls.Add(btnAgregar);
            groupBox1.Controls.Add(label4);
            groupBox1.Location = new Point(14, 14);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(1077, 555);
            groupBox1.TabIndex = 18;
            groupBox1.TabStop = false;
            groupBox1.Text = "Productos a Solicitar";
            // 
            // cmbSucursal
            // 
            cmbSucursal.FormattingEnabled = true;
            cmbSucursal.Location = new Point(295, 126);
            cmbSucursal.Margin = new Padding(4, 3, 4, 3);
            cmbSucursal.Name = "cmbSucursal";
            cmbSucursal.Size = new Size(140, 23);
            cmbSucursal.TabIndex = 19;
            // 
            // cmbDeposito
            // 
            cmbDeposito.FormattingEnabled = true;
            cmbDeposito.Location = new Point(295, 89);
            cmbDeposito.Margin = new Padding(4, 3, 4, 3);
            cmbDeposito.Name = "cmbDeposito";
            cmbDeposito.Size = new Size(140, 23);
            cmbDeposito.TabIndex = 18;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(194, 92);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(93, 15);
            label5.TabIndex = 17;
            label5.Text = "Sucursal Origen:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(172, 129);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(115, 15);
            label2.TabIndex = 16;
            label2.Text = "Sucursal a Traspasar:";
            // 
            // txtbPesoNeto
            // 
            txtbPesoNeto.Location = new Point(577, 98);
            txtbPesoNeto.Margin = new Padding(4, 3, 4, 3);
            txtbPesoNeto.Name = "txtbPesoNeto";
            txtbPesoNeto.Size = new Size(102, 23);
            txtbPesoNeto.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(500, 102);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 14;
            label1.Text = "Peso Neto:";
            // 
            // btnGenerarTraspasoProd
            // 
            btnGenerarTraspasoProd.Location = new Point(279, 503);
            btnGenerarTraspasoProd.Margin = new Padding(4, 3, 4, 3);
            btnGenerarTraspasoProd.Name = "btnGenerarTraspasoProd";
            btnGenerarTraspasoProd.Size = new Size(495, 44);
            btnGenerarTraspasoProd.TabIndex = 13;
            btnGenerarTraspasoProd.Text = "Generar Traspaso de Productos";
            btnGenerarTraspasoProd.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(29, 45);
            dateTimePicker1.Margin = new Padding(4, 3, 4, 3);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(233, 23);
            dateTimePicker1.TabIndex = 4;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(10, 174);
            dataGridView1.Margin = new Padding(4, 3, 4, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1061, 323);
            dataGridView1.TabIndex = 10;
            // 
            // txtbProd
            // 
            txtbProd.Enabled = false;
            txtbProd.Location = new Point(577, 60);
            txtbProd.Margin = new Padding(4, 3, 4, 3);
            txtbProd.Name = "txtbProd";
            txtbProd.Size = new Size(296, 23);
            txtbProd.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(462, 63);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(106, 15);
            label3.TabIndex = 6;
            label3.Text = "Nombre Producto:";
            // 
            // txtbCantidad
            // 
            txtbCantidad.Location = new Point(577, 134);
            txtbCantidad.Margin = new Padding(4, 3, 4, 3);
            txtbCantidad.Name = "txtbCantidad";
            txtbCantidad.Size = new Size(102, 23);
            txtbCantidad.TabIndex = 9;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(719, 126);
            btnAgregar.Margin = new Padding(4, 3, 4, 3);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(129, 36);
            btnAgregar.TabIndex = 7;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(503, 137);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(58, 15);
            label4.TabIndex = 8;
            label4.Text = "Cantidad:";
            // 
            // FormTraspasoProductosSucursal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1104, 574);
            Controls.Add(groupBox1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormTraspasoProductosSucursal";
            Text = "Traspaso de Productos a Sucursal";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtbPesoNeto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerarTraspasoProd;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtbProd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtbCantidad;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSucursal;
        private System.Windows.Forms.ComboBox cmbDeposito;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
    }
}