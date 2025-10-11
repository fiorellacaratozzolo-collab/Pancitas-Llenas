namespace FormUI.FormInventario
{
    partial class FormAgregarStock
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
            groupBox1 = new GroupBox();
            btnActualizar = new Button();
            dgvAgregarStock = new DataGridView();
            cmbProveedor = new ComboBox();
            label1 = new Label();
            btnVolver = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAgregarStock).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnActualizar);
            groupBox1.Controls.Add(dgvAgregarStock);
            groupBox1.Location = new Point(14, 111);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(737, 498);
            groupBox1.TabIndex = 18;
            groupBox1.TabStop = false;
            groupBox1.Text = "Agregar Stock";
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(300, 441);
            btnActualizar.Margin = new Padding(4, 3, 4, 3);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(115, 40);
            btnActualizar.TabIndex = 2;
            btnActualizar.Text = "Actualizar Stock";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // dgvAgregarStock
            // 
            dgvAgregarStock.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAgregarStock.Location = new Point(8, 33);
            dgvAgregarStock.Margin = new Padding(4, 3, 4, 3);
            dgvAgregarStock.Name = "dgvAgregarStock";
            dgvAgregarStock.Size = new Size(721, 400);
            dgvAgregarStock.TabIndex = 0;
            // 
            // cmbProveedor
            // 
            cmbProveedor.FormattingEnabled = true;
            cmbProveedor.Location = new Point(313, 66);
            cmbProveedor.Margin = new Padding(4, 3, 4, 3);
            cmbProveedor.Name = "cmbProveedor";
            cmbProveedor.Size = new Size(168, 23);
            cmbProveedor.TabIndex = 20;
            cmbProveedor.SelectedIndexChanged += cmbProveedor_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(168, 69);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(127, 15);
            label1.TabIndex = 21;
            label1.Text = "Seleccionar Proveedor:";
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(664, 629);
            btnVolver.Margin = new Padding(4, 3, 4, 3);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(88, 27);
            btnVolver.TabIndex = 22;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            // 
            // FormAgregarStock
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(765, 669);
            Controls.Add(btnVolver);
            Controls.Add(label1);
            Controls.Add(cmbProveedor);
            Controls.Add(groupBox1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormAgregarStock";
            Text = "Agregar Stock";
            Load += FormAgregarStock_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAgregarStock).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.DataGridView dgvAgregarStock;
        private System.Windows.Forms.ComboBox cmbProveedor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnVolver;
    }
}