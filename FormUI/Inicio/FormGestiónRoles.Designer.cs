namespace FormUI.Inicio
{
    partial class FormGestiónRoles
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
            cmbUsuarios = new ComboBox();
            label1 = new Label();
            groupBox1 = new GroupBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            btnGuardarPermiso = new Button();
            btnEliminarPermiso = new Button();
            btnAgregarPermiso = new Button();
            dgvPermisos = new DataGridView();
            label3 = new Label();
            btnGuardarRol = new Button();
            label2 = new Label();
            btnEliminarRol = new Button();
            btnAgregarRol = new Button();
            dgvRoles = new DataGridView();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPermisos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRoles).BeginInit();
            SuspendLayout();
            // 
            // cmbUsuarios
            // 
            cmbUsuarios.FormattingEnabled = true;
            cmbUsuarios.Location = new Point(230, 46);
            cmbUsuarios.Name = "cmbUsuarios";
            cmbUsuarios.Size = new Size(188, 23);
            cmbUsuarios.TabIndex = 0;
            cmbUsuarios.SelectedIndexChanged += cmbUsuarios_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(258, 28);
            label1.Name = "label1";
            label1.Size = new Size(126, 15);
            label1.TabIndex = 1;
            label1.Text = "Seleccione un Usuario:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(btnGuardarPermiso);
            groupBox1.Controls.Add(btnEliminarPermiso);
            groupBox1.Controls.Add(btnAgregarPermiso);
            groupBox1.Controls.Add(dgvPermisos);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(btnGuardarRol);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnEliminarRol);
            groupBox1.Controls.Add(btnAgregarRol);
            groupBox1.Controls.Add(dgvRoles);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(cmbUsuarios);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(976, 641);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Enter += groupBox1_Enter;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(579, 323);
            label7.Name = "label7";
            label7.Size = new Size(305, 30);
            label7.TabIndex = 15;
            label7.Text = "3.Control Total,\"Puede hacer todo lo anterior,\r\nademás de eliminar registros o realizar acciones críticas.\"";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(579, 290);
            label6.Name = "label6";
            label6.Size = new Size(364, 15);
            label6.TabIndex = 14;
            label6.Text = "2.\tEscritura,\t\"Puede ver, crear nuevos registros y editar los existentes.\"";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(579, 242);
            label5.Name = "label5";
            label5.Size = new Size(370, 30);
            label5.TabIndex = 13;
            label5.Text = "1. Lectura,\"El usuario puede abrir el Form y ver los datos,\r\npero los botones de \"\"Guardar\"\" o \"\"Eliminar\"\" están deshabilitados.\"\r\n";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(700, 195);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 12;
            label4.Text = "Referencias:";
            // 
            // btnGuardarPermiso
            // 
            btnGuardarPermiso.Location = new Point(395, 585);
            btnGuardarPermiso.Name = "btnGuardarPermiso";
            btnGuardarPermiso.Size = new Size(75, 23);
            btnGuardarPermiso.TabIndex = 11;
            btnGuardarPermiso.Text = "Guardar";
            btnGuardarPermiso.UseVisualStyleBackColor = true;
            // 
            // btnEliminarPermiso
            // 
            btnEliminarPermiso.Location = new Point(158, 585);
            btnEliminarPermiso.Name = "btnEliminarPermiso";
            btnEliminarPermiso.Size = new Size(122, 37);
            btnEliminarPermiso.TabIndex = 10;
            btnEliminarPermiso.Text = "Eliminar Permiso";
            btnEliminarPermiso.UseVisualStyleBackColor = true;
            // 
            // btnAgregarPermiso
            // 
            btnAgregarPermiso.Location = new Point(38, 585);
            btnAgregarPermiso.Name = "btnAgregarPermiso";
            btnAgregarPermiso.Size = new Size(114, 37);
            btnAgregarPermiso.TabIndex = 9;
            btnAgregarPermiso.Text = "Agregar Permiso";
            btnAgregarPermiso.UseVisualStyleBackColor = true;
            // 
            // dgvPermisos
            // 
            dgvPermisos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPermisos.Location = new Point(16, 384);
            dgvPermisos.Name = "dgvPermisos";
            dgvPermisos.Size = new Size(496, 195);
            dgvPermisos.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(21, 366);
            label3.Name = "label3";
            label3.Size = new Size(114, 15);
            label3.TabIndex = 7;
            label3.Text = "Permisos asignados:";
            // 
            // btnGuardarRol
            // 
            btnGuardarRol.Location = new Point(395, 290);
            btnGuardarRol.Name = "btnGuardarRol";
            btnGuardarRol.Size = new Size(75, 23);
            btnGuardarRol.TabIndex = 6;
            btnGuardarRol.Text = "Guardar";
            btnGuardarRol.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(26, 82);
            label2.Name = "label2";
            label2.Size = new Size(94, 15);
            label2.TabIndex = 5;
            label2.Text = "Roles asignados:";
            // 
            // btnEliminarRol
            // 
            btnEliminarRol.Location = new Point(158, 290);
            btnEliminarRol.Name = "btnEliminarRol";
            btnEliminarRol.Size = new Size(96, 37);
            btnEliminarRol.TabIndex = 4;
            btnEliminarRol.Text = "Eliminar Rol";
            btnEliminarRol.UseVisualStyleBackColor = true;
            // 
            // btnAgregarRol
            // 
            btnAgregarRol.Location = new Point(38, 290);
            btnAgregarRol.Name = "btnAgregarRol";
            btnAgregarRol.Size = new Size(114, 37);
            btnAgregarRol.TabIndex = 3;
            btnAgregarRol.Text = "Agregar Rol";
            btnAgregarRol.UseVisualStyleBackColor = true;
            // 
            // dgvRoles
            // 
            dgvRoles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRoles.Location = new Point(21, 100);
            dgvRoles.Name = "dgvRoles";
            dgvRoles.Size = new Size(496, 184);
            dgvRoles.TabIndex = 2;
            // 
            // FormGestiónRoles
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 665);
            Controls.Add(groupBox1);
            Name = "FormGestiónRoles";
            Text = "Gestión de Roles";
            Load += FormGestiónRoles_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPermisos).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRoles).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cmbUsuarios;
        private Label label1;
        private GroupBox groupBox1;
        private Button btnEliminarRol;
        private Button btnAgregarRol;
        private DataGridView dgvRoles;
        private Button btnGuardarRol;
        private Label label2;
        private Button btnGuardarPermiso;
        private Button btnEliminarPermiso;
        private Button btnAgregarPermiso;
        private DataGridView dgvPermisos;
        private Label label3;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
    }
}