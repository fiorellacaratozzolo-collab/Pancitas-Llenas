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
            clbPermisos = new CheckedListBox();
            clbRoles = new CheckedListBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            btnGuardarRol = new Button();
            label2 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // cmbUsuarios
            // 
            cmbUsuarios.FormattingEnabled = true;
            cmbUsuarios.Location = new Point(66, 48);
            cmbUsuarios.Name = "cmbUsuarios";
            cmbUsuarios.Size = new Size(188, 23);
            cmbUsuarios.TabIndex = 0;
            cmbUsuarios.SelectedIndexChanged += cmbUsuarios_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(94, 30);
            label1.Name = "label1";
            label1.Size = new Size(126, 15);
            label1.TabIndex = 1;
            label1.Text = "Seleccione un Usuario:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(clbPermisos);
            groupBox1.Controls.Add(clbRoles);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(btnGuardarRol);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(cmbUsuarios);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(814, 573);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Enter += groupBox1_Enter;
            // 
            // clbPermisos
            // 
            clbPermisos.FormattingEnabled = true;
            clbPermisos.Location = new Point(18, 348);
            clbPermisos.Name = "clbPermisos";
            clbPermisos.Size = new Size(279, 202);
            clbPermisos.TabIndex = 17;
            // 
            // clbRoles
            // 
            clbRoles.FormattingEnabled = true;
            clbRoles.Location = new Point(18, 125);
            clbRoles.Name = "clbRoles";
            clbRoles.Size = new Size(279, 184);
            clbRoles.TabIndex = 16;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(389, 273);
            label7.Name = "label7";
            label7.Size = new Size(305, 30);
            label7.TabIndex = 15;
            label7.Text = "3.Control Total,\"Puede hacer todo lo anterior,\r\nademás de eliminar registros o realizar acciones críticas.\"";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(389, 240);
            label6.Name = "label6";
            label6.Size = new Size(364, 15);
            label6.TabIndex = 14;
            label6.Text = "2.\tEscritura,\t\"Puede ver, crear nuevos registros y editar los existentes.\"";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(389, 192);
            label5.Name = "label5";
            label5.Size = new Size(370, 30);
            label5.TabIndex = 13;
            label5.Text = "1. Lectura,\"El usuario puede abrir el Form y ver los datos,\r\npero los botones de \"\"Guardar\"\" o \"\"Eliminar\"\" están deshabilitados.\"\r\n";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(510, 145);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 12;
            label4.Text = "Referencias:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(66, 330);
            label3.Name = "label3";
            label3.Size = new Size(166, 15);
            label3.TabIndex = 7;
            label3.Text = "Permisos Extras / Excepciones:";
            // 
            // btnGuardarRol
            // 
            btnGuardarRol.Location = new Point(357, 314);
            btnGuardarRol.Name = "btnGuardarRol";
            btnGuardarRol.Size = new Size(407, 33);
            btnGuardarRol.TabIndex = 6;
            btnGuardarRol.Text = "Guardar";
            btnGuardarRol.UseVisualStyleBackColor = true;
            btnGuardarRol.Click += btnGuardarRol_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(94, 107);
            label2.Name = "label2";
            label2.Size = new Size(86, 15);
            label2.TabIndex = 5;
            label2.Text = "Rol de Usuario:";
            // 
            // FormGestiónRoles
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(841, 597);
            Controls.Add(groupBox1);
            Name = "FormGestiónRoles";
            Text = "Gestión de Roles";
            Load += FormGestiónRoles_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cmbUsuarios;
        private Label label1;
        private GroupBox groupBox1;
        private Label label2;
        private Label label3;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Button btnGuardarRol;
        private CheckedListBox clbPermisos;
        private CheckedListBox clbRoles;
    }
}