namespace FormUI.Inicio
{
    partial class FormBitácora
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
            btnVer = new Button();
            dgvBitácora = new DataGridView();
            btnExportarBitacora = new Button();
            dtpFiltro = new DateTimePicker();
            cmbFiltroFecha = new ComboBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvBitácora).BeginInit();
            SuspendLayout();
            // 
            // btnVer
            // 
            btnVer.Location = new Point(12, 51);
            btnVer.Name = "btnVer";
            btnVer.Size = new Size(123, 39);
            btnVer.TabIndex = 0;
            btnVer.Text = "Ver Bitácora";
            btnVer.UseVisualStyleBackColor = true;
            btnVer.Click += btnVer_Click;
            // 
            // dgvBitácora
            // 
            dgvBitácora.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBitácora.Location = new Point(12, 96);
            dgvBitácora.Name = "dgvBitácora";
            dgvBitácora.Size = new Size(960, 453);
            dgvBitácora.TabIndex = 2;
            // 
            // btnExportarBitacora
            // 
            btnExportarBitacora.Location = new Point(879, 62);
            btnExportarBitacora.Name = "btnExportarBitacora";
            btnExportarBitacora.Size = new Size(93, 28);
            btnExportarBitacora.TabIndex = 3;
            btnExportarBitacora.Text = "Exportar";
            btnExportarBitacora.UseVisualStyleBackColor = true;
            btnExportarBitacora.Click += btnExportarBitacora_Click;
            // 
            // dtpFiltro
            // 
            dtpFiltro.Location = new Point(353, 57);
            dtpFiltro.Name = "dtpFiltro";
            dtpFiltro.Size = new Size(256, 23);
            dtpFiltro.TabIndex = 4;
            // 
            // cmbFiltroFecha
            // 
            cmbFiltroFecha.FormattingEnabled = true;
            cmbFiltroFecha.Location = new Point(354, 19);
            cmbFiltroFecha.Name = "cmbFiltroFecha";
            cmbFiltroFecha.Size = new Size(256, 23);
            cmbFiltroFecha.TabIndex = 5;
            cmbFiltroFecha.SelectedIndexChanged += cmbFiltroFecha_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(287, 22);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 6;
            label1.Text = "Filtrar por:";
            // 
            // FormBitácora
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(label1);
            Controls.Add(cmbFiltroFecha);
            Controls.Add(dtpFiltro);
            Controls.Add(btnExportarBitacora);
            Controls.Add(dgvBitácora);
            Controls.Add(btnVer);
            Name = "FormBitácora";
            Text = "Bitácora";
            Load += FormBitácora_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBitácora).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnVer;
        private DataGridView dgvBitácora;
        private Button btnExportarBitacora;
        private DateTimePicker dtpFiltro;
        private ComboBox cmbFiltroFecha;
        private Label label1;
    }
}