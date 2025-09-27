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
            rbtnSucursal1 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            label1 = new Label();
            btnSiguiente = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // rbtnSucursal1
            // 
            rbtnSucursal1.AutoSize = true;
            rbtnSucursal1.Location = new Point(67, 320);
            rbtnSucursal1.Name = "rbtnSucursal1";
            rbtnSucursal1.Size = new Size(221, 19);
            rbtnSucursal1.TabIndex = 0;
            rbtnSucursal1.TabStop = true;
            rbtnSucursal1.Text = "Villa Adellina, Jorge Washington 4087";
            rbtnSucursal1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(67, 370);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(181, 19);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "San Isidro, Av. Centenario 820";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(67, 423);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(158, 19);
            radioButton3.TabIndex = 2;
            radioButton3.TabStop = true;
            radioButton3.Text = "Martínez, Corrientes 1672";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(48, 270);
            label1.Name = "label1";
            label1.Size = new Size(240, 15);
            label1.TabIndex = 3;
            label1.Text = "Seleccione la Sucursal donde desea ingresar:";
            // 
            // btnSiguiente
            // 
            btnSiguiente.Location = new Point(120, 497);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(108, 32);
            btnSiguiente.TabIndex = 4;
            btnSiguiente.Text = "Siguiente";
            btnSiguiente.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(36, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(271, 243);
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // FormSeleccionSucursal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(340, 550);
            Controls.Add(pictureBox1);
            Controls.Add(btnSiguiente);
            Controls.Add(label1);
            Controls.Add(radioButton3);
            Controls.Add(radioButton2);
            Controls.Add(rbtnSucursal1);
            Name = "FormSeleccionSucursal";
            Text = "Seleccionar Sucursal";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton rbtnSucursal1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private Label label1;
        private Button btnSiguiente;
        private PictureBox pictureBox1;
    }
}