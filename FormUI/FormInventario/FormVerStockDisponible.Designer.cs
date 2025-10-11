namespace FormUI.FormInventario
{
    partial class FormVerStockDisponible
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
            btnVolver = new Button();
            groupBox1 = new GroupBox();
            btnVer = new Button();
            dgvStock = new DataGridView();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStock).BeginInit();
            SuspendLayout();
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(583, 642);
            btnVolver.Margin = new Padding(4, 3, 4, 3);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(88, 27);
            btnVolver.TabIndex = 5;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnVer);
            groupBox1.Controls.Add(dgvStock);
            groupBox1.Location = new Point(14, 14);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(659, 622);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Stock disponible";
            // 
            // btnVer
            // 
            btnVer.Location = new Point(135, 22);
            btnVer.Margin = new Padding(4, 3, 4, 3);
            btnVer.Name = "btnVer";
            btnVer.Size = new Size(392, 37);
            btnVer.TabIndex = 2;
            btnVer.Text = "Ver";
            btnVer.UseVisualStyleBackColor = true;
            btnVer.Click += btnVer_Click;
            // 
            // dgvStock
            // 
            dgvStock.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStock.Location = new Point(7, 66);
            dgvStock.Margin = new Padding(4, 3, 4, 3);
            dgvStock.Name = "dgvStock";
            dgvStock.Size = new Size(645, 542);
            dgvStock.TabIndex = 0;
            // 
            // FormVerStockDisponible
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 680);
            Controls.Add(btnVolver);
            Controls.Add(groupBox1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormVerStockDisponible";
            Text = "Ver Stock Disponible";
            Load += FormVerStockDisponible_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvStock).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnVer;
        private System.Windows.Forms.DataGridView dgvStock;
    }
}