namespace FormUI.FormVenta
{
    partial class FormHistorialVentas
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
            bntBuscar = new Button();
            label2 = new Label();
            datetimeHistorialVenta = new DateTimePicker();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(bntBuscar);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(datetimeHistorialVenta);
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Location = new Point(9, 28);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(983, 659);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            // 
            // bntBuscar
            // 
            bntBuscar.Location = new Point(603, 38);
            bntBuscar.Margin = new Padding(4, 3, 4, 3);
            bntBuscar.Name = "bntBuscar";
            bntBuscar.Size = new Size(53, 27);
            bntBuscar.TabIndex = 11;
            bntBuscar.Text = "Buscar";
            bntBuscar.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(229, 19);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(104, 15);
            label2.TabIndex = 10;
            label2.Text = "Seleccionar Fecha:";
            // 
            // datetimeHistorialVenta
            // 
            datetimeHistorialVenta.Location = new Point(346, 38);
            datetimeHistorialVenta.Margin = new Padding(4, 3, 4, 3);
            datetimeHistorialVenta.Name = "datetimeHistorialVenta";
            datetimeHistorialVenta.Size = new Size(249, 23);
            datetimeHistorialVenta.TabIndex = 9;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(18, 80);
            dataGridView1.Margin = new Padding(4, 3, 4, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(957, 568);
            dataGridView1.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(435, 10);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(104, 15);
            label1.TabIndex = 4;
            label1.Text = "Historial de Ventas";
            // 
            // FormHistorialVentas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1005, 698);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormHistorialVentas";
            Text = "Historial de Ventas";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bntBuscar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker datetimeHistorialVenta;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
    }
}