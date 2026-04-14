namespace FormUI.FormVenta
{
    partial class FormListaPrecios
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
            dgvProductoPrecio = new DataGridView();
            label1 = new Label();
            groupBox1 = new GroupBox();
            txtBuscar = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvProductoPrecio).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvProductoPrecio
            // 
            dgvProductoPrecio.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductoPrecio.Location = new Point(7, 81);
            dgvProductoPrecio.Margin = new Padding(4, 3, 4, 3);
            dgvProductoPrecio.Name = "dgvProductoPrecio";
            dgvProductoPrecio.Size = new Size(977, 627);
            dgvProductoPrecio.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(319, 42);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 1;
            label1.Text = "Buscar:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtBuscar);
            groupBox1.Controls.Add(dgvProductoPrecio);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(991, 721);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Lista de precios:";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(380, 39);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(180, 23);
            txtBuscar.TabIndex = 2;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // FormListaPrecios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1015, 745);
            Controls.Add(groupBox1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormListaPrecios";
            Text = "Lista de Precios";
            Load += FormListaPrecios_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProductoPrecio).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProductoPrecio;
        private System.Windows.Forms.Label label1;
        private GroupBox groupBox1;
        private TextBox txtBuscar;
    }
}