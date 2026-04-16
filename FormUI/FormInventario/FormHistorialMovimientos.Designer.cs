namespace FormUI.FormInventario
{
    partial class FormHistorialMovimientos
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
            gpEntregaProductos = new GroupBox();
            btnVerEntrega = new Button();
            dgvEntregaProductos = new DataGridView();
            gbTraspasoProductos = new GroupBox();
            btnVerTraspasos = new Button();
            dgvTraspasoProductos = new DataGridView();
            gpEntregaProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEntregaProductos).BeginInit();
            gbTraspasoProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTraspasoProductos).BeginInit();
            SuspendLayout();
            // 
            // gpEntregaProductos
            // 
            gpEntregaProductos.Controls.Add(btnVerEntrega);
            gpEntregaProductos.Controls.Add(dgvEntregaProductos);
            gpEntregaProductos.Location = new Point(25, 12);
            gpEntregaProductos.Margin = new Padding(4, 3, 4, 3);
            gpEntregaProductos.Name = "gpEntregaProductos";
            gpEntregaProductos.Padding = new Padding(4, 3, 4, 3);
            gpEntregaProductos.Size = new Size(564, 626);
            gpEntregaProductos.TabIndex = 0;
            gpEntregaProductos.TabStop = false;
            gpEntregaProductos.Text = "Entrega de Productos";
            // 
            // btnVerEntrega
            // 
            btnVerEntrega.Location = new Point(210, 590);
            btnVerEntrega.Margin = new Padding(4, 3, 4, 3);
            btnVerEntrega.Name = "btnVerEntrega";
            btnVerEntrega.Size = new Size(88, 27);
            btnVerEntrega.TabIndex = 4;
            btnVerEntrega.Text = "Ver";
            btnVerEntrega.UseVisualStyleBackColor = true;
            btnVerEntrega.Click += btnVerEntrega_Click;
            // 
            // dgvEntregaProductos
            // 
            dgvEntregaProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEntregaProductos.Location = new Point(7, 22);
            dgvEntregaProductos.Margin = new Padding(4, 3, 4, 3);
            dgvEntregaProductos.Name = "dgvEntregaProductos";
            dgvEntregaProductos.Size = new Size(549, 562);
            dgvEntregaProductos.TabIndex = 0;
            // 
            // gbTraspasoProductos
            // 
            gbTraspasoProductos.Controls.Add(btnVerTraspasos);
            gbTraspasoProductos.Controls.Add(dgvTraspasoProductos);
            gbTraspasoProductos.Location = new Point(633, 12);
            gbTraspasoProductos.Margin = new Padding(4, 3, 4, 3);
            gbTraspasoProductos.Name = "gbTraspasoProductos";
            gbTraspasoProductos.Padding = new Padding(4, 3, 4, 3);
            gbTraspasoProductos.Size = new Size(616, 626);
            gbTraspasoProductos.TabIndex = 1;
            gbTraspasoProductos.TabStop = false;
            gbTraspasoProductos.Text = "Traspaso de Productos";
            // 
            // btnVerTraspasos
            // 
            btnVerTraspasos.Location = new Point(282, 590);
            btnVerTraspasos.Margin = new Padding(4, 3, 4, 3);
            btnVerTraspasos.Name = "btnVerTraspasos";
            btnVerTraspasos.Size = new Size(88, 27);
            btnVerTraspasos.TabIndex = 4;
            btnVerTraspasos.Text = "Ver";
            btnVerTraspasos.UseVisualStyleBackColor = true;
            btnVerTraspasos.Click += btnVerTraspasos_Click;
            // 
            // dgvTraspasoProductos
            // 
            dgvTraspasoProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTraspasoProductos.Location = new Point(8, 22);
            dgvTraspasoProductos.Margin = new Padding(4, 3, 4, 3);
            dgvTraspasoProductos.Name = "dgvTraspasoProductos";
            dgvTraspasoProductos.Size = new Size(600, 562);
            dgvTraspasoProductos.TabIndex = 0;
            // 
            // FormHistorialMovimientos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1262, 646);
            Controls.Add(gbTraspasoProductos);
            Controls.Add(gpEntregaProductos);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormHistorialMovimientos";
            Text = "Historial de Movimientos";
            Load += FormHistorialMovimientos_Load;
            gpEntregaProductos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvEntregaProductos).EndInit();
            gbTraspasoProductos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTraspasoProductos).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpEntregaProductos;
        private System.Windows.Forms.GroupBox gbTraspasoProductos;
        private System.Windows.Forms.DataGridView dgvTraspasoProductos;
        private System.Windows.Forms.DataGridView dgvEntregaProductos;
        private System.Windows.Forms.Button btnVerEntrega;
        private System.Windows.Forms.Button btnVerTraspasos;
    }
}