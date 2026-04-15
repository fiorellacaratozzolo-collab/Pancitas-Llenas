namespace FormUI
{
    partial class FormLogin
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
            label2 = new Label();
            btnIngresar = new Button();
            txtbUsuario = new TextBox();
            txtbContraseña = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(51, 63);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 0;
            label1.Text = "Usuario";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 120);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 1;
            label2.Text = "Contraseña";
            // 
            // btnIngresar
            // 
            btnIngresar.Location = new Point(102, 190);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(117, 28);
            btnIngresar.TabIndex = 3;
            btnIngresar.Text = "Iniciar Sesión";
            btnIngresar.UseVisualStyleBackColor = true;
            btnIngresar.Click += btnIngresar_Click_1;
            // 
            // txtbUsuario
            // 
            txtbUsuario.Location = new Point(116, 60);
            txtbUsuario.Name = "txtbUsuario";
            txtbUsuario.Size = new Size(152, 23);
            txtbUsuario.TabIndex = 4;
            // 
            // txtbContraseña
            // 
            txtbContraseña.Location = new Point(116, 117);
            txtbContraseña.Name = "txtbContraseña";
            txtbContraseña.Size = new Size(152, 23);
            txtbContraseña.TabIndex = 5;
            txtbContraseña.UseSystemPasswordChar = true;
            // 
            // FormLogin
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(335, 253);
            Controls.Add(txtbContraseña);
            Controls.Add(txtbUsuario);
            Controls.Add(btnIngresar);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            Load += FormLogin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button btnIngresar;
        private TextBox txtbUsuario;
        private TextBox txtbContraseña;
    }
}