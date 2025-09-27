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
            this.gpEntregaProductos = new System.Windows.Forms.GroupBox();
            this.btnVerEntrega = new System.Windows.Forms.Button();
            this.dgvEntregaProductos = new System.Windows.Forms.DataGridView();
            this.gbTraspasoProductos = new System.Windows.Forms.GroupBox();
            this.btnVerTraspaso = new System.Windows.Forms.Button();
            this.dgvTraspasoProductos = new System.Windows.Forms.DataGridView();
            this.btnVolver = new System.Windows.Forms.Button();
            this.gpEntregaProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntregaProductos)).BeginInit();
            this.gbTraspasoProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraspasoProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // gpEntregaProductos
            // 
            this.gpEntregaProductos.Controls.Add(this.btnVerEntrega);
            this.gpEntregaProductos.Controls.Add(this.dgvEntregaProductos);
            this.gpEntregaProductos.Location = new System.Drawing.Point(22, 32);
            this.gpEntregaProductos.Name = "gpEntregaProductos";
            this.gpEntregaProductos.Size = new System.Drawing.Size(483, 673);
            this.gpEntregaProductos.TabIndex = 0;
            this.gpEntregaProductos.TabStop = false;
            this.gpEntregaProductos.Text = "Entrega de Productos";
            // 
            // btnVerEntrega
            // 
            this.btnVerEntrega.Location = new System.Drawing.Point(202, 635);
            this.btnVerEntrega.Name = "btnVerEntrega";
            this.btnVerEntrega.Size = new System.Drawing.Size(75, 23);
            this.btnVerEntrega.TabIndex = 4;
            this.btnVerEntrega.Text = "Ver";
            this.btnVerEntrega.UseVisualStyleBackColor = true;
            // 
            // dgvEntregaProductos
            // 
            this.dgvEntregaProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEntregaProductos.Location = new System.Drawing.Point(6, 19);
            this.dgvEntregaProductos.Name = "dgvEntregaProductos";
            this.dgvEntregaProductos.Size = new System.Drawing.Size(453, 610);
            this.dgvEntregaProductos.TabIndex = 0;
            // 
            // gbTraspasoProductos
            // 
            this.gbTraspasoProductos.Controls.Add(this.btnVerTraspaso);
            this.gbTraspasoProductos.Controls.Add(this.dgvTraspasoProductos);
            this.gbTraspasoProductos.Location = new System.Drawing.Point(540, 32);
            this.gbTraspasoProductos.Name = "gbTraspasoProductos";
            this.gbTraspasoProductos.Size = new System.Drawing.Size(528, 673);
            this.gbTraspasoProductos.TabIndex = 1;
            this.gbTraspasoProductos.TabStop = false;
            this.gbTraspasoProductos.Text = "Traspaso de Productos";
            // 
            // btnVerTraspaso
            // 
            this.btnVerTraspaso.Location = new System.Drawing.Point(245, 635);
            this.btnVerTraspaso.Name = "btnVerTraspaso";
            this.btnVerTraspaso.Size = new System.Drawing.Size(75, 23);
            this.btnVerTraspaso.TabIndex = 4;
            this.btnVerTraspaso.Text = "Ver";
            this.btnVerTraspaso.UseVisualStyleBackColor = true;
            // 
            // dgvTraspasoProductos
            // 
            this.dgvTraspasoProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTraspasoProductos.Location = new System.Drawing.Point(16, 28);
            this.dgvTraspasoProductos.Name = "dgvTraspasoProductos";
            this.dgvTraspasoProductos.Size = new System.Drawing.Size(497, 601);
            this.dgvTraspasoProductos.TabIndex = 0;
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(995, 740);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 23);
            this.btnVolver.TabIndex = 2;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            // 
            // FormHistorialMovimientos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 775);
            this.Controls.Add(this.btnVolver);
            this.Controls.Add(this.gbTraspasoProductos);
            this.Controls.Add(this.gpEntregaProductos);
            this.Name = "FormHistorialMovimientos";
            this.Text = "Historial de Movimientos";
            this.Load += new System.EventHandler(this.FormHistorialMovimientos_Load);
            this.gpEntregaProductos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntregaProductos)).EndInit();
            this.gbTraspasoProductos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraspasoProductos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpEntregaProductos;
        private System.Windows.Forms.GroupBox gbTraspasoProductos;
        private System.Windows.Forms.DataGridView dgvTraspasoProductos;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.DataGridView dgvEntregaProductos;
        private System.Windows.Forms.Button btnVerEntrega;
        private System.Windows.Forms.Button btnVerTraspaso;
    }
}