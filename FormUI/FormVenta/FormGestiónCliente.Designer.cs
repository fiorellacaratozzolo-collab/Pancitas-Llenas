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
            btnDeshabilitar = new Button();
            btnActualizar = new Button();
            btnBuscar = new Button();
            dgvCliente = new DataGridView();
            btnVolver = new Button();
            btnAgregarCliente = new Button();
            groupBox1 = new GroupBox();
            label4 = new Label();
            rbtnMinorista = new RadioButton();
            rbtnMayorista = new RadioButton();
            txtbDNI = new TextBox();
            txtbNombreCliente = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCliente).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnDeshabilitar);
            groupBox2.Controls.Add(btnActualizar);
            groupBox2.Controls.Add(btnBuscar);
            groupBox2.Controls.Add(dgvCliente);
            groupBox2.Location = new Point(498, 14);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(419, 418);
            groupBox2.TabIndex = 10;
            groupBox2.TabStop = false;
            groupBox2.Text = "Gestionar Cliente";
            groupBox2.Enter += groupBox2_Enter;
            // 
            // btnDeshabilitar
            // 
            btnDeshabilitar.Location = new Point(301, 368);
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
            btnActualizar.Location = new Point(157, 368);
            btnActualizar.Margin = new Padding(4, 3, 4, 3);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(117, 44);
            btnActualizar.TabIndex = 7;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click_1;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(10, 368);
            btnBuscar.Margin = new Padding(4, 3, 4, 3);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(128, 44);
            btnBuscar.TabIndex = 6;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // dgvCliente
            // 
            dgvCliente.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCliente.Location = new Point(7, 30);
            dgvCliente.Margin = new Padding(4, 3, 4, 3);
            dgvCliente.Name = "dgvCliente";
            dgvCliente.Size = new Size(404, 332);
            dgvCliente.TabIndex = 5;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(215, 405);
            btnVolver.Margin = new Padding(4, 3, 4, 3);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(88, 27);
            btnVolver.TabIndex = 9;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            // 
            // btnAgregarCliente
            // 
            btnAgregarCliente.Location = new Point(21, 202);
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
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(rbtnMinorista);
            groupBox1.Controls.Add(rbtnMayorista);
            groupBox1.Controls.Add(txtbDNI);
            groupBox1.Controls.Add(txtbNombreCliente);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(btnAgregarCliente);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(32, 70);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(446, 245);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Cliente";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(21, 109);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(90, 15);
            label4.TabIndex = 11;
            label4.Text = "Tipo de Cliente:";
            // 
            // rbtnMinorista
            // 
            rbtnMinorista.AutoSize = true;
            rbtnMinorista.Location = new Point(122, 152);
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
            rbtnMayorista.Location = new Point(122, 127);
            rbtnMayorista.Name = "rbtnMayorista";
            rbtnMayorista.Size = new Size(77, 19);
            rbtnMayorista.TabIndex = 9;
            rbtnMayorista.TabStop = true;
            rbtnMayorista.Text = "Mayorista";
            rbtnMayorista.UseVisualStyleBackColor = true;
            // 
            // txtbDNI
            // 
            txtbDNI.Location = new Point(122, 65);
            txtbDNI.Margin = new Padding(4, 3, 4, 3);
            txtbDNI.Name = "txtbDNI";
            txtbDNI.Size = new Size(304, 23);
            txtbDNI.TabIndex = 7;
            // 
            // txtbNombreCliente
            // 
            txtbNombreCliente.Location = new Point(122, 22);
            txtbNombreCliente.Margin = new Padding(4, 3, 4, 3);
            txtbNombreCliente.Name = "txtbNombreCliente";
            txtbNombreCliente.Size = new Size(304, 23);
            txtbNombreCliente.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(81, 73);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(30, 15);
            label3.TabIndex = 1;
            label3.Text = "DNI:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(61, 30);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 0;
            label2.Text = "Nombre:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(165, 44);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(169, 15);
            label1.TabIndex = 11;
            label1.Text = "Dar de Alta a un Nuevo Cliente";
            // 
            // FormGestiónCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(948, 444);
            Controls.Add(label1);
            Controls.Add(groupBox2);
            Controls.Add(btnVolver);
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
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDeshabilitar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvCliente;
        private System.Windows.Forms.Button btnVolver;
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
    }
}