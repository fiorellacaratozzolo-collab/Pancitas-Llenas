namespace FormUI.FormSucursal
{
    partial class FormGestiónTraspaso
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
            dgvSolicitudes = new DataGridView();
            dgvDetalles = new DataGridView();
            btnAprobar = new Button();
            btnRechazar = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dgvSolicitudes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalles).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // dgvSolicitudes
            // 
            dgvSolicitudes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSolicitudes.Location = new Point(18, 22);
            dgvSolicitudes.Name = "dgvSolicitudes";
            dgvSolicitudes.Size = new Size(347, 256);
            dgvSolicitudes.TabIndex = 0;
            // 
            // dgvDetalles
            // 
            dgvDetalles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalles.Location = new Point(6, 25);
            dgvDetalles.Name = "dgvDetalles";
            dgvDetalles.Size = new Size(381, 410);
            dgvDetalles.TabIndex = 1;
            // 
            // btnAprobar
            // 
            btnAprobar.Location = new Point(18, 284);
            btnAprobar.Name = "btnAprobar";
            btnAprobar.Size = new Size(168, 46);
            btnAprobar.TabIndex = 2;
            btnAprobar.Text = "Aprobar";
            btnAprobar.UseVisualStyleBackColor = true;
            // 
            // btnRechazar
            // 
            btnRechazar.Location = new Point(197, 284);
            btnRechazar.Name = "btnRechazar";
            btnRechazar.Size = new Size(168, 46);
            btnRechazar.TabIndex = 3;
            btnRechazar.Text = "Rechazar";
            btnRechazar.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgvSolicitudes);
            groupBox1.Controls.Add(btnAprobar);
            groupBox1.Controls.Add(btnRechazar);
            groupBox1.Location = new Point(20, 42);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(385, 345);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Solicitudes de Traspaso";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvDetalles);
            groupBox2.Location = new Point(433, 42);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(393, 441);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Detalle de Solicitud";
            // 
            // FormGestiónTraspaso
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(863, 498);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "FormGestiónTraspaso";
            Text = "FormGestiónTraspaso";
            ((System.ComponentModel.ISupportInitialize)dgvSolicitudes).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalles).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvSolicitudes;
        private DataGridView dgvDetalles;
        private Button btnAprobar;
        private Button btnRechazar;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
    }
}