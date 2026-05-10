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
            btnLimpiar = new Button();
            label1 = new Label();
            txtbDireccionProv = new TextBox();
            txtbTelefonoProv = new TextBox();
            txtbCuitProv = new TextBox();
            btnAgregar = new Button();
            txtbNombreProv = new TextBox();
            label4 = new Label();
            lalbel5 = new Label();
            label3 = new Label();
            label2 = new Label();
            dgvProveedor = new DataGridView();
            groupBox2 = new GroupBox();
            btnHabilitar = new Button();
            btnVerDeshabilitados = new Button();
            btnDeshabilitar = new Button();
            btnActualizar = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProveedor).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnLimpiar);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtbDireccionProv);
            groupBox1.Controls.Add(txtbTelefonoProv);
            groupBox1.Controls.Add(txtbCuitProv);
            groupBox1.Controls.Add(btnAgregar);
            groupBox1.Controls.Add(txtbNombreProv);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(lalbel5);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(13, 57);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(429, 358);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Agregar a un Proveedor";
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(349, 329);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(75, 23);
            btnLimpiar.TabIndex = 17;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(102, 46);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(239, 15);
            label1.TabIndex = 7;
            label1.Text = "Ingrese los siguientes datos de el Proveedor:";
            // 
            // txtbDireccionProv
            // 
            txtbDireccionProv.Location = new Point(102, 213);
            txtbDireccionProv.Margin = new Padding(4, 3, 4, 3);
            txtbDireccionProv.Name = "txtbDireccionProv";
            txtbDireccionProv.Size = new Size(304, 23);
            txtbDireccionProv.TabIndex = 9;
            // 
            // txtbTelefonoProv
            // 
            txtbTelefonoProv.Location = new Point(102, 173);
            txtbTelefonoProv.Margin = new Padding(4, 3, 4, 3);
            txtbTelefonoProv.Name = "txtbTelefonoProv";
            txtbTelefonoProv.Size = new Size(156, 23);
            txtbTelefonoProv.TabIndex = 8;
            // 
            // txtbCuitProv
            // 
            txtbCuitProv.Location = new Point(102, 134);
            txtbCuitProv.Margin = new Padding(4, 3, 4, 3);
            txtbCuitProv.Name = "txtbCuitProv";
            txtbCuitProv.Size = new Size(304, 23);
            txtbCuitProv.TabIndex = 7;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(8, 282);
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
            txtbNombreProv.Location = new Point(102, 90);
            txtbNombreProv.Margin = new Padding(4, 3, 4, 3);
            txtbNombreProv.Name = "txtbNombreProv";
            txtbNombreProv.Size = new Size(304, 23);
            txtbNombreProv.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(31, 221);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(60, 15);
            label4.TabIndex = 3;
            label4.Text = "Dirección:";
            // 
            // lalbel5
            // 
            lalbel5.AutoSize = true;
            lalbel5.Location = new Point(35, 181);
            lalbel5.Margin = new Padding(4, 0, 4, 0);
            lalbel5.Name = "lalbel5";
            lalbel5.Size = new Size(56, 15);
            lalbel5.TabIndex = 2;
            lalbel5.Text = "Teléfono:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(31, 142);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 1;
            label3.Text = "CUIT:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(41, 98);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 0;
            label2.Text = "Nombre:";
            // 
            // dgvProveedor
            // 
            dgvProveedor.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProveedor.Location = new Point(5, 22);
            dgvProveedor.Margin = new Padding(4, 3, 4, 3);
            dgvProveedor.Name = "dgvProveedor";
            dgvProveedor.Size = new Size(685, 372);
            dgvProveedor.TabIndex = 5;
            dgvProveedor.SelectionChanged += dgvProveedor_SelectionChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnHabilitar);
            groupBox2.Controls.Add(btnVerDeshabilitados);
            groupBox2.Controls.Add(btnDeshabilitar);
            groupBox2.Controls.Add(btnActualizar);
            groupBox2.Controls.Add(dgvProveedor);
            groupBox2.Location = new Point(458, 12);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(698, 437);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Gestionar Proveedor";
            // 
            // btnHabilitar
            // 
            btnHabilitar.Location = new Point(451, 401);
            btnHabilitar.Name = "btnHabilitar";
            btnHabilitar.Size = new Size(93, 29);
            btnHabilitar.TabIndex = 10;
            btnHabilitar.Text = "Habilitar";
            btnHabilitar.UseVisualStyleBackColor = true;
            btnHabilitar.Click += btnHabilitar_Click;
            // 
            // btnVerDeshabilitados
            // 
            btnVerDeshabilitados.Location = new Point(301, 401);
            btnVerDeshabilitados.Name = "btnVerDeshabilitados";
            btnVerDeshabilitados.Size = new Size(121, 29);
            btnVerDeshabilitados.TabIndex = 9;
            btnVerDeshabilitados.Text = "Ver Deshabilitados";
            btnVerDeshabilitados.UseVisualStyleBackColor = true;
            btnVerDeshabilitados.Click += btnVerDeshabilitados_Click;
            // 
            // btnDeshabilitar
            // 
            btnDeshabilitar.Location = new Point(170, 400);
            btnDeshabilitar.Margin = new Padding(4, 3, 4, 3);
            btnDeshabilitar.Name = "btnDeshabilitar";
            btnDeshabilitar.Size = new Size(100, 31);
            btnDeshabilitar.TabIndex = 8;
            btnDeshabilitar.Text = "Deshabilitar";
            btnDeshabilitar.UseVisualStyleBackColor = true;
            btnDeshabilitar.Click += btnDeshabilitar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(37, 400);
            btnActualizar.Margin = new Padding(4, 3, 4, 3);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(101, 31);
            btnActualizar.TabIndex = 7;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // FormGestiónProveedor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1169, 461);
            Controls.Add(groupBox2);
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
        private System.Windows.Forms.DataGridView dgvProveedor;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDeshabilitar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label label1;
        private Button btnHabilitar;
        private Button btnVerDeshabilitados;
        private Button btnLimpiar;
    }
}