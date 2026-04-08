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
            btnVerTraspaso = new Button();
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
            gpEntregaProductos.Size = new Size(564, 655);
            gpEntregaProductos.TabIndex = 0;
            gpEntregaProductos.TabStop = false;
            gpEntregaProductos.Text = "Entrega de Productos";
            // 
            // btnVerEntrega
            // 
            btnVerEntrega.Location = new Point(208, 620);
            btnVerEntrega.Margin = new Padding(4, 3, 4, 3);
            btnVerEntrega.Name = "btnVerEntrega";
            btnVerEntrega.Size = new Size(88, 27);
            btnVerEntrega.TabIndex = 4;
            btnVerEntrega.Text = "Ver";
            btnVerEntrega.UseVisualStyleBackColor = true;
            // 
            // dgvEntregaProductos
            // 
            dgvEntregaProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEntregaProductos.Location = new Point(7, 22);
            dgvEntregaProductos.Margin = new Padding(4, 3, 4, 3);
            dgvEntregaProductos.Name = "dgvEntregaProductos";
            dgvEntregaProductos.Size = new Size(528, 592);
            dgvEntregaProductos.TabIndex = 0;
            // 
            // gbTraspasoProductos
            // 
            gbTraspasoProductos.Controls.Add(btnVerTraspaso);
            gbTraspasoProductos.Controls.Add(dgvTraspasoProductos);
            gbTraspasoProductos.Location = new Point(633, 12);
            gbTraspasoProductos.Margin = new Padding(4, 3, 4, 3);
            gbTraspasoProductos.Name = "gbTraspasoProductos";
            gbTraspasoProductos.Padding = new Padding(4, 3, 4, 3);
            gbTraspasoProductos.Size = new Size(616, 655);
            gbTraspasoProductos.TabIndex = 1;
            gbTraspasoProductos.TabStop = false;
            gbTraspasoProductos.Text = "Traspaso de Productos";
            // 
            // btnVerTraspaso
            // 
            btnVerTraspaso.Location = new Point(285, 620);
            btnVerTraspaso.Margin = new Padding(4, 3, 4, 3);
            btnVerTraspaso.Name = "btnVerTraspaso";
            btnVerTraspaso.Size = new Size(88, 27);
            btnVerTraspaso.TabIndex = 4;
            btnVerTraspaso.Text = "Ver";
            btnVerTraspaso.UseVisualStyleBackColor = true;
            // 
            // dgvTraspasoProductos
            // 
            dgvTraspasoProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTraspasoProductos.Location = new Point(19, 32);
            dgvTraspasoProductos.Margin = new Padding(4, 3, 4, 3);
            dgvTraspasoProductos.Name = "dgvTraspasoProductos";
            dgvTraspasoProductos.Size = new Size(580, 582);
            dgvTraspasoProductos.TabIndex = 0;
            // 
            // FormHistorialMovimientos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1262, 680);
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
        private System.Windows.Forms.Button btnVerTraspaso;
    }
}