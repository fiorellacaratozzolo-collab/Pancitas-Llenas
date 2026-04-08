namespace FormUI
{
    partial class FormSeleccionSucursal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSeleccionSucursal));
            label1 = new Label();
            btnSiguiente = new Button();
            pictureBox1 = new PictureBox();
            cmbSucursales = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(88, 270);
            label1.Name = "label1";
            label1.Size = new Size(240, 15);
            label1.TabIndex = 3;
            label1.Text = "Seleccione la Sucursal donde desea ingresar:";
            // 
            // btnSiguiente
            // 
            btnSiguiente.Location = new Point(147, 351);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(108, 32);
            btnSiguiente.TabIndex = 4;
            btnSiguiente.Text = "Siguiente";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(67, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(271, 243);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // cmbSucursales
            // 
            cmbSucursales.FormattingEnabled = true;
            cmbSucursales.Items.AddRange(new object[] { "Villa Adellina, Jorge Washington 4087", "San Isidro, Av. Centenario 820", "Martínez, Corrientes 1672" });
            cmbSucursales.Location = new Point(36, 298);
            cmbSucursales.Name = "cmbSucursales";
            cmbSucursales.Size = new Size(332, 23);
            cmbSucursales.TabIndex = 7;
            // 
            // FormSeleccionSucursal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(414, 411);
            Controls.Add(cmbSucursales);
            Controls.Add(pictureBox1);
            Controls.Add(btnSiguiente);
            Controls.Add(label1);
            Name = "FormSeleccionSucursal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Seleccionar Sucursal";
            Load += FormSeleccionSucursal_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Button btnSiguiente;
        private PictureBox pictureBox1;
        private ComboBox cmbSucursales;
    }
}