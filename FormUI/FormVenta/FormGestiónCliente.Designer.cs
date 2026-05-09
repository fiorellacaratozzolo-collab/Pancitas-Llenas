namespace FormUI.FormVenta
{
    partial class FormGestiónCliente
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
            groupBox2 = new GroupBox();
            btnHabilitar = new Button();
            btnVerDeshabilitados = new Button();
            btnBuscar = new Button();
            dgvCliente = new DataGridView();
            btnDeshabilitar = new Button();
            btnActualizar = new Button();
            btnAgregarCliente = new Button();
            groupBox1 = new GroupBox();
            btnLimpiar = new Button();
            label1 = new Label();
            label4 = new Label();
            rbtnMinorista = new RadioButton();
            rbtnMayorista = new RadioButton();
            txtbDNI = new TextBox();
            txtbNombreCliente = new TextBox();
            label3 = new Label();
            label2 = new Label();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCliente).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnHabilitar);
            groupBox2.Controls.Add(btnVerDeshabilitados);
            groupBox2.Controls.Add(btnBuscar);
            groupBox2.Controls.Add(dgvCliente);
            groupBox2.Controls.Add(btnDeshabilitar);
            groupBox2.Controls.Add(btnActualizar);
            groupBox2.Location = new Point(498, 14);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(658, 435);
            groupBox2.TabIndex = 10;
            groupBox2.TabStop = false;
            groupBox2.Text = "Gestionar Cliente";
            // 
            // btnHabilitar
            // 
            btnHabilitar.Location = new Point(541, 400);
            btnHabilitar.Name = "btnHabilitar";
            btnHabilitar.Size = new Size(93, 29);
            btnHabilitar.TabIndex = 10;
            btnHabilitar.Text = "Habilitar";
            btnHabilitar.UseVisualStyleBackColor = true;
            btnHabilitar.Click += btnHabilitar_Click;
            // 
            // btnVerDeshabilitados
            // 
            btnVerDeshabilitados.Location = new Point(399, 400);
            btnVerDeshabilitados.Name = "btnVerDeshabilitados";
            btnVerDeshabilitados.Size = new Size(121, 29);
            btnVerDeshabilitados.TabIndex = 9;
            btnVerDeshabilitados.Text = "Ver Deshabilitados";
            btnVerDeshabilitados.UseVisualStyleBackColor = true;
            btnVerDeshabilitados.Click += btnVerDeshabilitados_Click;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(8, 400);
            btnBuscar.Margin = new Padding(4, 3, 4, 3);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(108, 29);
            btnBuscar.TabIndex = 6;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // dgvCliente
            // 
            dgvCliente.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCliente.Location = new Point(7, 21);
            dgvCliente.Margin = new Padding(4, 3, 4, 3);
            dgvCliente.Name = "dgvCliente";
            dgvCliente.Size = new Size(643, 373);
            dgvCliente.TabIndex = 5;
            dgvCliente.SelectionChanged += dgvCliente_SelectionChanged;
            // 
            // btnDeshabilitar
            // 
            btnDeshabilitar.Location = new Point(271, 400);
            btnDeshabilitar.Margin = new Padding(4, 3, 4, 3);
            btnDeshabilitar.Name = "btnDeshabilitar";
            btnDeshabilitar.Size = new Size(108, 29);
            btnDeshabilitar.TabIndex = 8;
            btnDeshabilitar.Text = "Deshabilitar";
            btnDeshabilitar.UseVisualStyleBackColor = true;
            btnDeshabilitar.Click += btnDeshabilitar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(139, 400);
            btnActualizar.Margin = new Padding(4, 3, 4, 3);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(108, 29);
            btnActualizar.TabIndex = 7;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click_1;
            // 
            // btnAgregarCliente
            // 
            btnAgregarCliente.Location = new Point(21, 248);
            btnAgregarCliente.Margin = new Padding(4, 3, 4, 3);
            btnAgregarCliente.Name = "btnAgregarCliente";
            btnAgregarCliente.Size = new Size(405, 32);
            btnAgregarCliente.TabIndex = 8;
            btnAgregarCliente.Text = "Agregar";
            btnAgregarCliente.UseVisualStyleBackColor = true;
            btnAgregarCliente.Click += btnAgregarCliente_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnLimpiar);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(rbtnMinorista);
            groupBox1.Controls.Add(rbtnMayorista);
            groupBox1.Controls.Add(txtbDNI);
            groupBox1.Controls.Add(txtbNombreCliente);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(btnAgregarCliente);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(32, 69);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(446, 327);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Agregar a un Cliente";
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(364, 298);
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
            label1.Location = new Point(122, 29);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(222, 15);
            label1.TabIndex = 11;
            label1.Text = "Ingrese los siguientes datos de el Cliente:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(21, 155);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(90, 15);
            label4.TabIndex = 11;
            label4.Text = "Tipo de Cliente:";
            // 
            // rbtnMinorista
            // 
            rbtnMinorista.AutoSize = true;
            rbtnMinorista.Location = new Point(122, 198);
            rbtnMinorista.Name = "rbtnMinorista";
            rbtnMinorista.Size = new Size(75, 19);
            rbtnMinorista.TabIndex = 10;
            rbtnMinorista.TabStop = true;
            rbtnMinorista.Text = "Minorista";
            rbtnMinorista.UseVisualStyleBackColor = true;
            // 
            // rbtnMayorista
            // 
            rbtnMayorista.AutoSize = true;
            rbtnMayorista.Location = new Point(122, 173);
            rbtnMayorista.Name = "rbtnMayorista";
            rbtnMayorista.Size = new Size(77, 19);
            rbtnMayorista.TabIndex = 9;
            rbtnMayorista.TabStop = true;
            rbtnMayorista.Text = "Mayorista";
            rbtnMayorista.UseVisualStyleBackColor = true;
            // 
            // txtbDNI
            // 
            txtbDNI.Location = new Point(122, 111);
            txtbDNI.Margin = new Padding(4, 3, 4, 3);
            txtbDNI.Name = "txtbDNI";
            txtbDNI.Size = new Size(304, 23);
            txtbDNI.TabIndex = 7;
            // 
            // txtbNombreCliente
            // 
            txtbNombreCliente.Location = new Point(122, 68);
            txtbNombreCliente.Margin = new Padding(4, 3, 4, 3);
            txtbNombreCliente.Name = "txtbNombreCliente";
            txtbNombreCliente.Size = new Size(304, 23);
            txtbNombreCliente.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(61, 119);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(30, 15);
            label3.TabIndex = 1;
            label3.Text = "DNI:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(61, 76);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 0;
            label2.Text = "Nombre:";
            // 
            // FormGestiónCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1169, 461);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormGestiónCliente";
            Text = "Gestión de Cliente";
            Load += FormGestiónCliente_Load;
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCliente).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDeshabilitar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvCliente;
        private System.Windows.Forms.Button btnAgregarCliente;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtbDNI;
        private System.Windows.Forms.TextBox txtbNombreCliente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private RadioButton rbtnMinorista;
        private RadioButton rbtnMayorista;
        private Label label4;
        private Button btnHabilitar;
        private Button btnVerDeshabilitados;
        private Button btnLimpiar;
    }
}