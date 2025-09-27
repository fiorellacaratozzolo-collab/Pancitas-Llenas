namespace FormUI.FormSucursal
{
    partial class FormGestiónSucursal
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
            btnModificar = new Button();
            cmbSeleccionSucursal = new ComboBox();
            txtbNombreEncargado = new TextBox();
            dgvSucursal = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            gbModificarSucursal = new GroupBox();
            btnDeshabilitar = new Button();
            groupBox2 = new GroupBox();
            txtbTelefonoSucursal = new TextBox();
            label10 = new Label();
            btnAltaSucursal = new Button();
            label9 = new Label();
            txtbDireccionSucursal = new TextBox();
            label8 = new Label();
            rbtnVenta = new RadioButton();
            rbtnDepositoVenta = new RadioButton();
            txtbNombreSucursal = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            txtbDniEncargado = new TextBox();
            btnVolver = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvSucursal).BeginInit();
            gbModificarSucursal.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(104, 357);
            btnModificar.Margin = new Padding(4, 3, 4, 3);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(127, 35);
            btnModificar.TabIndex = 0;
            btnModificar.Text = "Actualizar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // cmbSeleccionSucursal
            // 
            cmbSeleccionSucursal.DisplayMember = "1,2";
            cmbSeleccionSucursal.FormattingEnabled = true;
            cmbSeleccionSucursal.Location = new Point(166, 37);
            cmbSeleccionSucursal.Margin = new Padding(4, 3, 4, 3);
            cmbSeleccionSucursal.Name = "cmbSeleccionSucursal";
            cmbSeleccionSucursal.Size = new Size(297, 23);
            cmbSeleccionSucursal.TabIndex = 1;
            cmbSeleccionSucursal.ValueMember = "1,2";
            cmbSeleccionSucursal.SelectedIndexChanged += cmbSeleccionSucursal_SelectedIndexChanged;
            // 
            // txtbNombreEncargado
            // 
            txtbNombreEncargado.Location = new Point(188, 80);
            txtbNombreEncargado.Margin = new Padding(4, 3, 4, 3);
            txtbNombreEncargado.Name = "txtbNombreEncargado";
            txtbNombreEncargado.Size = new Size(229, 23);
            txtbNombreEncargado.TabIndex = 2;
            // 
            // dgvSucursal
            // 
            dgvSucursal.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSucursal.Location = new Point(68, 79);
            dgvSucursal.Margin = new Padding(4, 3, 4, 3);
            dgvSucursal.Name = "dgvSucursal";
            dgvSucursal.Size = new Size(364, 272);
            dgvSucursal.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(213, 9);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(121, 15);
            label1.TabIndex = 4;
            label1.Text = "Gestión de Sucursales";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 40);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(136, 15);
            label2.TabIndex = 5;
            label2.Text = "Seleccione una Sucursal:";
            // 
            // gbModificarSucursal
            // 
            gbModificarSucursal.Controls.Add(btnDeshabilitar);
            gbModificarSucursal.Controls.Add(cmbSeleccionSucursal);
            gbModificarSucursal.Controls.Add(dgvSucursal);
            gbModificarSucursal.Controls.Add(label2);
            gbModificarSucursal.Controls.Add(btnModificar);
            gbModificarSucursal.Location = new Point(17, 50);
            gbModificarSucursal.Margin = new Padding(4, 3, 4, 3);
            gbModificarSucursal.Name = "gbModificarSucursal";
            gbModificarSucursal.Padding = new Padding(4, 3, 4, 3);
            gbModificarSucursal.Size = new Size(486, 413);
            gbModificarSucursal.TabIndex = 6;
            gbModificarSucursal.TabStop = false;
            gbModificarSucursal.Text = "Modificar Sucursal";
            // 
            // btnDeshabilitar
            // 
            btnDeshabilitar.Location = new Point(260, 357);
            btnDeshabilitar.Margin = new Padding(4, 3, 4, 3);
            btnDeshabilitar.Name = "btnDeshabilitar";
            btnDeshabilitar.Size = new Size(127, 35);
            btnDeshabilitar.TabIndex = 8;
            btnDeshabilitar.Text = "Deshabilitar";
            btnDeshabilitar.UseVisualStyleBackColor = true;
            btnDeshabilitar.Click += btnDeshabilitar_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtbTelefonoSucursal);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(btnAltaSucursal);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(txtbDireccionSucursal);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(rbtnVenta);
            groupBox2.Controls.Add(rbtnDepositoVenta);
            groupBox2.Controls.Add(txtbNombreSucursal);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(txtbDniEncargado);
            groupBox2.Controls.Add(txtbNombreEncargado);
            groupBox2.Location = new Point(39, 481);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(446, 444);
            groupBox2.TabIndex = 7;
            groupBox2.TabStop = false;
            groupBox2.Text = "Agregar una Sucursal";
            // 
            // txtbTelefonoSucursal
            // 
            txtbTelefonoSucursal.Location = new Point(188, 255);
            txtbTelefonoSucursal.Margin = new Padding(4, 3, 4, 3);
            txtbTelefonoSucursal.Name = "txtbTelefonoSucursal";
            txtbTelefonoSucursal.Size = new Size(229, 23);
            txtbTelefonoSucursal.TabIndex = 21;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(33, 258);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(131, 15);
            label10.TabIndex = 20;
            label10.Text = "Teléfono de la Sucursal:";
            // 
            // btnAltaSucursal
            // 
            btnAltaSucursal.Location = new Point(19, 400);
            btnAltaSucursal.Margin = new Padding(4, 3, 4, 3);
            btnAltaSucursal.Name = "btnAltaSucursal";
            btnAltaSucursal.Size = new Size(420, 37);
            btnAltaSucursal.TabIndex = 19;
            btnAltaSucursal.Text = "Dar de Alta";
            btnAltaSucursal.UseVisualStyleBackColor = true;
            btnAltaSucursal.Click += btnAltaSucursal_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(15, 157);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(229, 15);
            label9.TabIndex = 18;
            label9.Text = "Ingrese los siguientes datos de la Sucursal:";
            // 
            // txtbDireccionSucursal
            // 
            txtbDireccionSucursal.Location = new Point(188, 220);
            txtbDireccionSucursal.Margin = new Padding(4, 3, 4, 3);
            txtbDireccionSucursal.Name = "txtbDireccionSucursal";
            txtbDireccionSucursal.Size = new Size(229, 23);
            txtbDireccionSucursal.TabIndex = 17;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(33, 224);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(135, 15);
            label8.TabIndex = 16;
            label8.Text = "Dirección de la Sucursal:";
            // 
            // rbtnVenta
            // 
            rbtnVenta.AutoSize = true;
            rbtnVenta.Location = new Point(148, 331);
            rbtnVenta.Margin = new Padding(4, 3, 4, 3);
            rbtnVenta.Name = "rbtnVenta";
            rbtnVenta.Size = new Size(54, 19);
            rbtnVenta.TabIndex = 15;
            rbtnVenta.TabStop = true;
            rbtnVenta.Text = "Venta";
            rbtnVenta.UseVisualStyleBackColor = true;
            // 
            // rbtnDepositoVenta
            // 
            rbtnDepositoVenta.AutoSize = true;
            rbtnDepositoVenta.Location = new Point(148, 358);
            rbtnDepositoVenta.Margin = new Padding(4, 3, 4, 3);
            rbtnDepositoVenta.Name = "rbtnDepositoVenta";
            rbtnDepositoVenta.Size = new Size(106, 19);
            rbtnDepositoVenta.TabIndex = 14;
            rbtnDepositoVenta.TabStop = true;
            rbtnDepositoVenta.Text = "Deposito-Venta";
            rbtnDepositoVenta.UseVisualStyleBackColor = true;
            // 
            // txtbNombreSucursal
            // 
            txtbNombreSucursal.Location = new Point(188, 187);
            txtbNombreSucursal.Margin = new Padding(4, 3, 4, 3);
            txtbNombreSucursal.Name = "txtbNombreSucursal";
            txtbNombreSucursal.Size = new Size(229, 23);
            txtbNombreSucursal.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(145, 300);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(97, 15);
            label7.TabIndex = 12;
            label7.Text = "Tipo de Sucursal:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(42, 190);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(129, 15);
            label6.TabIndex = 11;
            label6.Text = "Nombre de la Sucursal:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(145, 113);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(30, 15);
            label5.TabIndex = 10;
            label5.Text = "DNI:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(68, 83);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(110, 15);
            label4.TabIndex = 9;
            label4.Text = "Nombre y Apellido:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 42);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(295, 15);
            label3.TabIndex = 8;
            label3.Text = "Ingrese los siguientes datos del Encargado de Sucursal:";
            // 
            // txtbDniEncargado
            // 
            txtbDniEncargado.Location = new Point(188, 113);
            txtbDniEncargado.Margin = new Padding(4, 3, 4, 3);
            txtbDniEncargado.Name = "txtbDniEncargado";
            txtbDniEncargado.Size = new Size(229, 23);
            txtbDniEncargado.TabIndex = 8;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(13, 938);
            btnVolver.Margin = new Padding(4, 3, 4, 3);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(75, 31);
            btnVolver.TabIndex = 8;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            // 
            // FormGestiónSucursal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(523, 981);
            Controls.Add(btnVolver);
            Controls.Add(groupBox2);
            Controls.Add(gbModificarSucursal);
            Controls.Add(label1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormGestiónSucursal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestion de Sucursal";
            Load += FormGestiónSucursal_Load_1;
            ((System.ComponentModel.ISupportInitialize)dgvSucursal).EndInit();
            gbModificarSucursal.ResumeLayout(false);
            gbModificarSucursal.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.ComboBox cmbSeleccionSucursal;
        private System.Windows.Forms.TextBox txtbNombreEncargado;
        private System.Windows.Forms.DataGridView dgvSucursal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbModificarSucursal;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDeshabilitar;
        private System.Windows.Forms.TextBox txtbDniEncargado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbtnVenta;
        private System.Windows.Forms.RadioButton rbtnDepositoVenta;
        private System.Windows.Forms.TextBox txtbNombreSucursal;
        private System.Windows.Forms.Button btnAltaSucursal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtbDireccionSucursal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.TextBox txtbTelefonoSucursal;
        private System.Windows.Forms.Label label10;
    }
}