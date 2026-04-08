namespace FormUI.Inicio
{
    partial class FormGestiónUsuario
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
            label1 = new Label();
            groupBox2 = new GroupBox();
            btnModificarContra = new Button();
            dgvUsuarios = new DataGridView();
            btnActualizar = new Button();
            groupBox1 = new GroupBox();
            cmbSucursales = new ComboBox();
            label5 = new Label();
            txtbContraseña = new TextBox();
            label4 = new Label();
            txtbEmail = new TextBox();
            txtbNombreUsuario = new TextBox();
            label3 = new Label();
            btnAgregarUsuario = new Button();
            label2 = new Label();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(241, 9);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(172, 15);
            label1.TabIndex = 15;
            label1.Text = "Dar de Alta a un Nuevo Usuario";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnModificarContra);
            groupBox2.Controls.Add(dgvUsuarios);
            groupBox2.Controls.Add(btnActualizar);
            groupBox2.Location = new Point(32, 216);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(641, 328);
            groupBox2.TabIndex = 14;
            groupBox2.TabStop = false;
            groupBox2.Text = "Gestionar Usuario";
            // 
            // btnModificarContra
            // 
            btnModificarContra.Location = new Point(16, 277);
            btnModificarContra.Name = "btnModificarContra";
            btnModificarContra.Size = new Size(211, 44);
            btnModificarContra.TabIndex = 10;
            btnModificarContra.Text = "Modificar Contraseña";
            btnModificarContra.UseVisualStyleBackColor = true;
            btnModificarContra.Click += btnModificarContra_Click;
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Location = new Point(16, 22);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.Size = new Size(617, 249);
            dgvUsuarios.TabIndex = 9;
            dgvUsuarios.CellClick += dgvUsuarios_CellClick;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(547, 277);
            btnActualizar.Margin = new Padding(4, 3, 4, 3);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(86, 44);
            btnActualizar.TabIndex = 7;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cmbSucursales);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(txtbContraseña);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtbEmail);
            groupBox1.Controls.Add(txtbNombreUsuario);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(btnAgregarUsuario);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(116, 27);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(422, 183);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            // 
            // cmbSucursales
            // 
            cmbSucursales.FormattingEnabled = true;
            cmbSucursales.Location = new Point(96, 105);
            cmbSucursales.Name = "cmbSucursales";
            cmbSucursales.Size = new Size(304, 23);
            cmbSucursales.TabIndex = 14;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(34, 108);
            label5.Name = "label5";
            label5.Size = new Size(54, 15);
            label5.TabIndex = 13;
            label5.Text = "Sucursal:";
            // 
            // txtbContraseña
            // 
            txtbContraseña.Location = new Point(96, 72);
            txtbContraseña.Margin = new Padding(4, 3, 4, 3);
            txtbContraseña.Name = "txtbContraseña";
            txtbContraseña.Size = new Size(304, 23);
            txtbContraseña.TabIndex = 12;
            txtbContraseña.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(18, 80);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 11;
            label4.Text = "Contraseña:";
            // 
            // txtbEmail
            // 
            txtbEmail.Location = new Point(96, 43);
            txtbEmail.Margin = new Padding(4, 3, 4, 3);
            txtbEmail.Name = "txtbEmail";
            txtbEmail.Size = new Size(304, 23);
            txtbEmail.TabIndex = 7;
            // 
            // txtbNombreUsuario
            // 
            txtbNombreUsuario.Location = new Point(96, 14);
            txtbNombreUsuario.Margin = new Padding(4, 3, 4, 3);
            txtbNombreUsuario.Name = "txtbNombreUsuario";
            txtbNombreUsuario.Size = new Size(304, 23);
            txtbNombreUsuario.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(49, 51);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 1;
            label3.Text = "Email:";
            // 
            // btnAgregarUsuario
            // 
            btnAgregarUsuario.Location = new Point(9, 141);
            btnAgregarUsuario.Margin = new Padding(4, 3, 4, 3);
            btnAgregarUsuario.Name = "btnAgregarUsuario";
            btnAgregarUsuario.Size = new Size(405, 32);
            btnAgregarUsuario.TabIndex = 8;
            btnAgregarUsuario.Text = "Agregar";
            btnAgregarUsuario.UseVisualStyleBackColor = true;
            btnAgregarUsuario.Click += btnAgregarUsuario_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(34, 22);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 0;
            label2.Text = "Nombre:";
            // 
            // FormGestiónUsuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(704, 555);
            Controls.Add(label1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "FormGestiónUsuario";
            Text = "Gestión de Usuario";
            Load += FormGestiónUsuario_Load;
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private GroupBox groupBox2;
        private Button btnActualizar;
        //private DataGridView dgvUsuario;
        private GroupBox groupBox1;
        private Label label4;
        private TextBox txtbEmail;
        private TextBox txtbNombreUsuario;
        private Label label3;
        private Button btnAgregarUsuario;
        private Label label2;
        private TextBox txtbContraseña;
        private DataGridView dgvUsuarios;
        private Button btnModificarContra;
        private ComboBox cmbSucursales;
        private Label label5;
    }
}