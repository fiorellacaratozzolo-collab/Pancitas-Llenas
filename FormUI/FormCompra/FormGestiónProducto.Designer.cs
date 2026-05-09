namespace FormUI.FormCompra
{
    partial class FormGestiónProducto
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
            btnHabilitar = new Button();
            btnVerDeshabilitados = new Button();
            btnDeshabilitar = new Button();
            btnActualizar = new Button();
            btnBuscar = new Button();
            dgvProducto = new DataGridView();
            btnAgregar = new Button();
            groupBox2 = new GroupBox();
            label6 = new Label();
            btnLimpiar = new Button();
            cmbProveedor = new ComboBox();
            txtbDescripcion = new TextBox();
            txtbPrecioNeto = new TextBox();
            txtbUnidad = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label1 = new Label();
            label3 = new Label();
            txtbPesoNeto = new TextBox();
            txtbMarca = new TextBox();
            txtbNombreProd = new TextBox();
            label2 = new Label();
            label9 = new Label();
            label8 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProducto).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnHabilitar);
            groupBox1.Controls.Add(btnVerDeshabilitados);
            groupBox1.Controls.Add(btnDeshabilitar);
            groupBox1.Controls.Add(btnActualizar);
            groupBox1.Controls.Add(btnBuscar);
            groupBox1.Controls.Add(dgvProducto);
            groupBox1.Location = new Point(473, 12);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(683, 437);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = "Gestionar Producto";
            // 
            // btnHabilitar
            // 
            btnHabilitar.Location = new Point(564, 402);
            btnHabilitar.Name = "btnHabilitar";
            btnHabilitar.Size = new Size(93, 29);
            btnHabilitar.TabIndex = 5;
            btnHabilitar.Text = "Habilitar";
            btnHabilitar.UseVisualStyleBackColor = true;
            btnHabilitar.Click += btnHabilitar_Click;
            // 
            // btnVerDeshabilitados
            // 
            btnVerDeshabilitados.Location = new Point(414, 402);
            btnVerDeshabilitados.Name = "btnVerDeshabilitados";
            btnVerDeshabilitados.Size = new Size(121, 29);
            btnVerDeshabilitados.TabIndex = 4;
            btnVerDeshabilitados.Text = "Ver Deshabilitados";
            btnVerDeshabilitados.UseVisualStyleBackColor = true;
            btnVerDeshabilitados.Click += btnVerDeshabilitados_Click;
            // 
            // btnDeshabilitar
            // 
            btnDeshabilitar.Location = new Point(297, 402);
            btnDeshabilitar.Margin = new Padding(4, 3, 4, 3);
            btnDeshabilitar.Name = "btnDeshabilitar";
            btnDeshabilitar.Size = new Size(91, 29);
            btnDeshabilitar.TabIndex = 3;
            btnDeshabilitar.Text = "Deshabilitar";
            btnDeshabilitar.UseVisualStyleBackColor = true;
            btnDeshabilitar.Click += btnDeshabilitar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(177, 402);
            btnActualizar.Margin = new Padding(4, 3, 4, 3);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(91, 29);
            btnActualizar.TabIndex = 2;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(52, 402);
            btnBuscar.Margin = new Padding(4, 3, 4, 3);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(91, 29);
            btnBuscar.TabIndex = 1;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // dgvProducto
            // 
            dgvProducto.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducto.Location = new Point(7, 22);
            dgvProducto.Margin = new Padding(4, 3, 4, 3);
            dgvProducto.Name = "dgvProducto";
            dgvProducto.Size = new Size(668, 374);
            dgvProducto.TabIndex = 0;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(105, 308);
            btnAgregar.Margin = new Padding(4, 3, 4, 3);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(284, 28);
            btnAgregar.TabIndex = 13;
            btnAgregar.Text = "Argregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(btnLimpiar);
            groupBox2.Controls.Add(cmbProveedor);
            groupBox2.Controls.Add(txtbDescripcion);
            groupBox2.Controls.Add(txtbPrecioNeto);
            groupBox2.Controls.Add(txtbUnidad);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(btnAgregar);
            groupBox2.Controls.Add(txtbPesoNeto);
            groupBox2.Controls.Add(txtbMarca);
            groupBox2.Controls.Add(txtbNombreProd);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label8);
            groupBox2.Location = new Point(13, 34);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(438, 388);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "Agregar un Producto";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(116, 31);
            label6.Name = "label6";
            label6.Size = new Size(234, 15);
            label6.TabIndex = 21;
            label6.Text = "Ingrese los siguientes datos de el Producto:";
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(356, 357);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(75, 23);
            btnLimpiar.TabIndex = 16;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // cmbProveedor
            // 
            cmbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProveedor.FormattingEnabled = true;
            cmbProveedor.Location = new Point(116, 258);
            cmbProveedor.Name = "cmbProveedor";
            cmbProveedor.Size = new Size(298, 23);
            cmbProveedor.TabIndex = 20;
            // 
            // txtbDescripcion
            // 
            txtbDescripcion.Location = new Point(116, 221);
            txtbDescripcion.Margin = new Padding(4, 3, 4, 3);
            txtbDescripcion.Name = "txtbDescripcion";
            txtbDescripcion.Size = new Size(298, 23);
            txtbDescripcion.TabIndex = 19;
            // 
            // txtbPrecioNeto
            // 
            txtbPrecioNeto.Location = new Point(116, 190);
            txtbPrecioNeto.Margin = new Padding(4, 3, 4, 3);
            txtbPrecioNeto.Name = "txtbPrecioNeto";
            txtbPrecioNeto.Size = new Size(298, 23);
            txtbPrecioNeto.TabIndex = 18;
            // 
            // txtbUnidad
            // 
            txtbUnidad.Location = new Point(116, 159);
            txtbUnidad.Margin = new Padding(4, 3, 4, 3);
            txtbUnidad.Name = "txtbUnidad";
            txtbUnidad.Size = new Size(100, 23);
            txtbUnidad.TabIndex = 17;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(35, 199);
            label5.Name = "label5";
            label5.Size = new Size(72, 15);
            label5.TabIndex = 16;
            label5.Text = "Precio Neto:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(59, 168);
            label4.Name = "label4";
            label4.Size = new Size(48, 15);
            label4.TabIndex = 15;
            label4.Text = "Unidad:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(44, 137);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 14;
            label1.Text = "Peso Neto:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 266);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(93, 15);
            label3.TabIndex = 6;
            label3.Text = "CUIT Proveedor:";
            // 
            // txtbPesoNeto
            // 
            txtbPesoNeto.Location = new Point(116, 128);
            txtbPesoNeto.Margin = new Padding(4, 3, 4, 3);
            txtbPesoNeto.Name = "txtbPesoNeto";
            txtbPesoNeto.Size = new Size(100, 23);
            txtbPesoNeto.TabIndex = 5;
            // 
            // txtbMarca
            // 
            txtbMarca.Location = new Point(116, 98);
            txtbMarca.Margin = new Padding(4, 3, 4, 3);
            txtbMarca.Name = "txtbMarca";
            txtbMarca.Size = new Size(298, 23);
            txtbMarca.TabIndex = 4;
            // 
            // txtbNombreProd
            // 
            txtbNombreProd.Location = new Point(116, 65);
            txtbNombreProd.Margin = new Padding(4, 3, 4, 3);
            txtbNombreProd.Name = "txtbNombreProd";
            txtbNombreProd.Size = new Size(298, 23);
            txtbNombreProd.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(35, 230);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(72, 15);
            label2.TabIndex = 2;
            label2.Text = "Descripción:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(64, 107);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(43, 15);
            label9.TabIndex = 1;
            label9.Text = "Marca:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(53, 74);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(54, 15);
            label8.TabIndex = 0;
            label8.Text = "Nombre:";
            // 
            // FormGestiónProducto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1169, 461);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormGestiónProducto";
            Text = "Gestión de Productos";
            Load += FormGestiónProducto_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProducto).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDeshabilitar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvProducto;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtbPesoNeto;
        private System.Windows.Forms.TextBox txtbMarca;
        private System.Windows.Forms.TextBox txtbNombreProd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private Label label5;
        private Label label4;
        private Label label1;
        private TextBox txtbDescripcion;
        private TextBox txtbPrecioNeto;
        private TextBox txtbUnidad;
        private ComboBox cmbProveedor;
        private Button btnLimpiar;
        private Label label6;
        private Button btnHabilitar;
        private Button btnVerDeshabilitados;
    }
}
