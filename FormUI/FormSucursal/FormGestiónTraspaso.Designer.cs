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
            dgvSolicitudesPendientes = new DataGridView();
            dgvDetalle = new DataGridView();
            btnAprobar = new Button();
            btnRechazar = new Button();
            groupBox1 = new GroupBox();
            btnActualizar = new Button();
            groupBox2 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dgvSolicitudesPendientes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // dgvSolicitudesPendientes
            // 
            dgvSolicitudesPendientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSolicitudesPendientes.Location = new Point(6, 41);
            dgvSolicitudesPendientes.Name = "dgvSolicitudesPendientes";
            dgvSolicitudesPendientes.Size = new Size(386, 333);
            dgvSolicitudesPendientes.TabIndex = 0;
            dgvSolicitudesPendientes.SelectionChanged += dgvSolicitudesPendientes_SelectionChanged;
            // 
            // dgvDetalle
            // 
            dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalle.Location = new Point(6, 25);
            dgvDetalle.Name = "dgvDetalle";
            dgvDetalle.Size = new Size(414, 443);
            dgvDetalle.TabIndex = 1;
            // 
            // btnAprobar
            // 
            btnAprobar.Location = new Point(6, 380);
            btnAprobar.Name = "btnAprobar";
            btnAprobar.Size = new Size(168, 46);
            btnAprobar.TabIndex = 2;
            btnAprobar.Text = "Aprobar";
            btnAprobar.UseVisualStyleBackColor = true;
            btnAprobar.Click += btnAprobar_Click_1;
            // 
            // btnRechazar
            // 
            btnRechazar.Location = new Point(224, 380);
            btnRechazar.Name = "btnRechazar";
            btnRechazar.Size = new Size(168, 46);
            btnRechazar.TabIndex = 3;
            btnRechazar.Text = "Rechazar";
            btnRechazar.UseVisualStyleBackColor = true;
            btnRechazar.Click += btnRechazar_Click_1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnActualizar);
            groupBox1.Controls.Add(dgvSolicitudesPendientes);
            groupBox1.Controls.Add(btnAprobar);
            groupBox1.Controls.Add(btnRechazar);
            groupBox1.Location = new Point(12, 48);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(398, 432);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Solicitudes de Traspaso Pendientes";
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(293, 12);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(75, 23);
            btnActualizar.TabIndex = 7;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvDetalle);
            groupBox2.Location = new Point(425, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(426, 474);
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
            Load += FormGestiónTraspaso_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSolicitudesPendientes).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvSolicitudesPendientes;
        private DataGridView dgvDetalle;
        private Button btnAprobar;
        private Button btnRechazar;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button btnActualizar;
    }
}