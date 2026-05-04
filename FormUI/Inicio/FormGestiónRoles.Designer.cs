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
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(btnGuardarRol);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(cmbUsuarios);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(332, 620);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
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
            btnGuardarRol.Location = new Point(18, 569);
            btnGuardarRol.Name = "btnGuardarRol";
            btnGuardarRol.Size = new Size(279, 33);
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
            ClientSize = new Size(359, 642);
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
        private Button btnGuardarRol;
        private CheckedListBox clbPermisos;
        private CheckedListBox clbRoles;
    }
}