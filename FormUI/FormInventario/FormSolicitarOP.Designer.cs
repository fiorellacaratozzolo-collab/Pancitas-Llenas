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
            btnGuadar = new Button();
            dtpFecha = new DateTimePicker();
            txtbPesoNeto = new TextBox();
            dgvSolicitarOP = new DataGridView();
            txtbNombreProd = new TextBox();
            label2 = new Label();
            label3 = new Label();
            txtbCantidad = new TextBox();
            btnAgregar = new Button();
            label4 = new Label();
            btnLimpiar = new Button();
            btnVolver = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSolicitarOP).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnGuadar);
            groupBox1.Controls.Add(dtpFecha);
            groupBox1.Controls.Add(txtbPesoNeto);
            groupBox1.Controls.Add(dgvSolicitarOP);
            groupBox1.Controls.Add(txtbNombreProd);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtbCantidad);
            groupBox1.Controls.Add(btnAgregar);
            groupBox1.Controls.Add(label4);
            groupBox1.Location = new Point(33, 25);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(510, 705);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Productos a Solicitar";
            // 
            // btnGuadar
            // 
            btnGuadar.Location = new Point(7, 654);
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
            dtpFecha.Location = new Point(21, 44);
            dtpFecha.Margin = new Padding(4, 3, 4, 3);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(233, 23);
            dtpFecha.TabIndex = 4;
            // 
            // txtbPesoNeto
            // 
            txtbPesoNeto.Location = new Point(133, 130);
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
            dgvSolicitarOP.Size = new Size(496, 423);
            dgvSolicitarOP.TabIndex = 10;
            dgvSolicitarOP.CellContentClick += dgvSolicitarOP_CellContentClick;
            // 
            // txtbNombreProd
            // 
            txtbNombreProd.Enabled = false;
            txtbNombreProd.Location = new Point(133, 92);
            txtbNombreProd.Margin = new Padding(4, 3, 4, 3);
            txtbNombreProd.Name = "txtbNombreProd";
            txtbNombreProd.Size = new Size(238, 23);
            txtbNombreProd.TabIndex = 3;            
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(60, 133);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(64, 15);
            label2.TabIndex = 5;
            label2.Text = "Peso Neto:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 96);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(106, 15);
            label3.TabIndex = 6;
            label3.Text = "Nombre Producto:";
            // 
            // txtbCantidad
            // 
            txtbCantidad.Location = new Point(133, 166);
            txtbCantidad.Margin = new Padding(4, 3, 4, 3);
            txtbCantidad.Name = "txtbCantidad";
            txtbCantidad.Size = new Size(102, 23);
            txtbCantidad.TabIndex = 9;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(271, 158);
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
            label4.Location = new Point(60, 170);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(58, 15);
            label4.TabIndex = 8;
            label4.Text = "Cantidad:";
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(259, 736);
            btnLimpiar.Margin = new Padding(4, 3, 4, 3);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(62, 36);
            btnLimpiar.TabIndex = 10;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(455, 749);
            btnVolver.Margin = new Padding(4, 3, 4, 3);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(88, 27);
            btnVolver.TabIndex = 16;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            btnVolver.Click += btnVolver_Click;
            // 
            // FormSolicitarOP
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(556, 789);
            Controls.Add(btnLimpiar);
            Controls.Add(groupBox1);
            Controls.Add(btnVolver);
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
        private System.Windows.Forms.TextBox txtbNombreProd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtbCantidad;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnVolver;
    }
}