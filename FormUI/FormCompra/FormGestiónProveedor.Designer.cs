namespace FormUI.FormCompra
{
    partial class FormGestiónProveedor
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
            txtbDireccionProv = new TextBox();
            txtbTelefonoProv = new TextBox();
            txtbCuitProv = new TextBox();
            btnAgregar = new Button();
            txtbNombreProv = new TextBox();
            label4 = new Label();
            lalbel5 = new Label();
            label3 = new Label();
            label2 = new Label();
            btnVolver = new Button();
            dgvProveedor = new DataGridView();
            groupBox2 = new GroupBox();
            btnDeshabilitar = new Button();
            btnActualizar = new Button();
            label1 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProveedor).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtbDireccionProv);
            groupBox1.Controls.Add(txtbTelefonoProv);
            groupBox1.Controls.Add(txtbCuitProv);
            groupBox1.Controls.Add(btnAgregar);
            groupBox1.Controls.Add(txtbNombreProv);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(lalbel5);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(40, 436);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(429, 211);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Proveedor";
            // 
            // txtbDireccionProv
            // 
            txtbDireccionProv.Location = new Point(102, 126);
            txtbDireccionProv.Margin = new Padding(4, 3, 4, 3);
            txtbDireccionProv.Name = "txtbDireccionProv";
            txtbDireccionProv.Size = new Size(304, 23);
            txtbDireccionProv.TabIndex = 9;
            // 
            // txtbTelefonoProv
            // 
            txtbTelefonoProv.Location = new Point(102, 96);
            txtbTelefonoProv.Margin = new Padding(4, 3, 4, 3);
            txtbTelefonoProv.Name = "txtbTelefonoProv";
            txtbTelefonoProv.Size = new Size(156, 23);
            txtbTelefonoProv.TabIndex = 8;
            // 
            // txtbCuitProv
            // 
            txtbCuitProv.Location = new Point(102, 66);
            txtbCuitProv.Margin = new Padding(4, 3, 4, 3);
            txtbCuitProv.Name = "txtbCuitProv";
            txtbCuitProv.Size = new Size(304, 23);
            txtbCuitProv.TabIndex = 7;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(8, 173);
            btnAgregar.Margin = new Padding(4, 3, 4, 3);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(416, 32);
            btnAgregar.TabIndex = 2;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // txtbNombreProv
            // 
            txtbNombreProv.Location = new Point(102, 36);
            txtbNombreProv.Margin = new Padding(4, 3, 4, 3);
            txtbNombreProv.Name = "txtbNombreProv";
            txtbNombreProv.Size = new Size(304, 23);
            txtbNombreProv.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(31, 134);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(60, 15);
            label4.TabIndex = 3;
            label4.Text = "Dirección:";
            // 
            // lalbel5
            // 
            lalbel5.AutoSize = true;
            lalbel5.Location = new Point(35, 104);
            lalbel5.Margin = new Padding(4, 0, 4, 0);
            lalbel5.Name = "lalbel5";
            lalbel5.Size = new Size(56, 15);
            lalbel5.TabIndex = 2;
            lalbel5.Text = "Teléfono:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(55, 74);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 1;
            label3.Text = "CUIT:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(41, 44);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 0;
            label2.Text = "Nombre:";
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(381, 670);
            btnVolver.Margin = new Padding(4, 3, 4, 3);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(88, 27);
            btnVolver.TabIndex = 3;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            // 
            // dgvProveedor
            // 
            dgvProveedor.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProveedor.Location = new Point(5, 22);
            dgvProveedor.Margin = new Padding(4, 3, 4, 3);
            dgvProveedor.Name = "dgvProveedor";
            dgvProveedor.Size = new Size(419, 273);
            dgvProveedor.TabIndex = 5;
            dgvProveedor.CellContentClick += dgvProveedor_CellContentClick;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnDeshabilitar);
            groupBox2.Controls.Add(btnActualizar);
            groupBox2.Controls.Add(dgvProveedor);
            groupBox2.Location = new Point(40, 27);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(429, 378);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Gestionar Proveedor";
            // 
            // btnDeshabilitar
            // 
            btnDeshabilitar.Location = new Point(230, 313);
            btnDeshabilitar.Margin = new Padding(4, 3, 4, 3);
            btnDeshabilitar.Name = "btnDeshabilitar";
            btnDeshabilitar.Size = new Size(104, 44);
            btnDeshabilitar.TabIndex = 8;
            btnDeshabilitar.Text = "Deshabilitar";
            btnDeshabilitar.UseVisualStyleBackColor = true;
            btnDeshabilitar.Click += btnDeshabilitar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(88, 313);
            btnActualizar.Margin = new Padding(4, 3, 4, 3);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(117, 44);
            btnActualizar.TabIndex = 7;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(174, 418);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(164, 15);
            label1.TabIndex = 7;
            label1.Text = "Agregar un Nuevo Proveedor:";
            // 
            // FormGestiónProveedor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(509, 709);
            Controls.Add(label1);
            Controls.Add(groupBox2);
            Controls.Add(btnVolver);
            Controls.Add(groupBox1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormGestiónProveedor";
            Text = "Gestión de Proveedores";
            Load += FormGestiónProveedor_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProveedor).EndInit();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtbDireccionProv;
        private System.Windows.Forms.TextBox txtbTelefonoProv;
        private System.Windows.Forms.TextBox txtbCuitProv;
        private System.Windows.Forms.TextBox txtbNombreProv;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lalbel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.DataGridView dgvProveedor;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDeshabilitar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label label1;
    }
}