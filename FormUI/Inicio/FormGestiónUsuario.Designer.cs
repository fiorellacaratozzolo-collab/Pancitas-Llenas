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
            btnHabilitar = new Button();
            btnModificarContra = new Button();
            dgvUsuarios = new DataGridView();
            btnDeshabilitar = new Button();
            btnActualizar = new Button();
            btnBuscar = new Button();
            groupBox1 = new GroupBox();
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
            label1.Location = new Point(151, 46);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(172, 15);
            label1.TabIndex = 15;
            label1.Text = "Dar de Alta a un Nuevo Usuario";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnHabilitar);
            groupBox2.Controls.Add(btnModificarContra);
            groupBox2.Controls.Add(dgvUsuarios);
            groupBox2.Controls.Add(btnDeshabilitar);
            groupBox2.Controls.Add(btnActualizar);
            groupBox2.Controls.Add(btnBuscar);
            groupBox2.Location = new Point(482, 24);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(421, 386);
            groupBox2.TabIndex = 14;
            groupBox2.TabStop = false;
            groupBox2.Text = "Gestionar Usuario";
            // 
            // btnHabilitar
            // 
            btnHabilitar.Location = new Point(312, 277);
            btnHabilitar.Margin = new Padding(4, 3, 4, 3);
            btnHabilitar.Name = "btnHabilitar";
            btnHabilitar.Size = new Size(90, 44);
            btnHabilitar.TabIndex = 11;
            btnHabilitar.Text = "Habilitar";
            btnHabilitar.UseVisualStyleBackColor = true;
            btnHabilitar.Click += btnHabilitar_Click;
            // 
            // btnModificarContra
            // 
            btnModificarContra.Location = new Point(110, 327);
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
            dgvUsuarios.Location = new Point(10, 22);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.Size = new Size(401, 249);
            dgvUsuarios.TabIndex = 9;
            dgvUsuarios.CellClick += dgvUsuarios_CellClick;
            // 
            // btnDeshabilitar
            // 
            btnDeshabilitar.Location = new Point(214, 277);
            btnDeshabilitar.Margin = new Padding(4, 3, 4, 3);
            btnDeshabilitar.Name = "btnDeshabilitar";
            btnDeshabilitar.Size = new Size(90, 44);
            btnDeshabilitar.TabIndex = 8;
            btnDeshabilitar.Text = "Deshabilitar";
            btnDeshabilitar.UseVisualStyleBackColor = true;
            btnDeshabilitar.Click += btnDeshabilitar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(120, 277);
            btnActualizar.Margin = new Padding(4, 3, 4, 3);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(86, 44);
            btnActualizar.TabIndex = 7;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(16, 277);
            btnBuscar.Margin = new Padding(4, 3, 4, 3);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(96, 44);
            btnBuscar.TabIndex = 6;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtbContraseña);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtbEmail);
            groupBox1.Controls.Add(txtbNombreUsuario);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(btnAgregarUsuario);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(18, 72);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(446, 215);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Usuario";
            // 
            // txtbContraseña
            // 
            txtbContraseña.Location = new Point(124, 114);
            txtbContraseña.Margin = new Padding(4, 3, 4, 3);
            txtbContraseña.Name = "txtbContraseña";
            txtbContraseña.Size = new Size(304, 23);
            txtbContraseña.TabIndex = 12;
            txtbContraseña.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(46, 117);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 11;
            label4.Text = "Contraseña:";
            // 
            // txtbEmail
            // 
            txtbEmail.Location = new Point(122, 65);
            txtbEmail.Margin = new Padding(4, 3, 4, 3);
            txtbEmail.Name = "txtbEmail";
            txtbEmail.Size = new Size(304, 23);
            txtbEmail.TabIndex = 7;
            // 
            // txtbNombreUsuario
            // 
            txtbNombreUsuario.Location = new Point(122, 22);
            txtbNombreUsuario.Margin = new Padding(4, 3, 4, 3);
            txtbNombreUsuario.Name = "txtbNombreUsuario";
            txtbNombreUsuario.Size = new Size(304, 23);
            txtbNombreUsuario.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(82, 73);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 1;
            label3.Text = "Email:";
            // 
            // btnAgregarUsuario
            // 
            btnAgregarUsuario.Location = new Point(21, 171);
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
            label2.Location = new Point(62, 30);
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
            ClientSize = new Size(915, 422);
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
        private Button btnDeshabilitar;
        private Button btnActualizar;
        private Button btnBuscar;
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
        private Button btnHabilitar;
    }
}