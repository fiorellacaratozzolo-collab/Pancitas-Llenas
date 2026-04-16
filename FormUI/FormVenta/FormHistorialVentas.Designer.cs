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
            btnVerTodas = new Button();
            bntBuscar = new Button();
            label2 = new Label();
            dtpFecha = new DateTimePicker();
            dgvHistorialVentas = new DataGridView();
            label1 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistorialVentas).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnVerTodas);
            groupBox1.Controls.Add(bntBuscar);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(dtpFecha);
            groupBox1.Controls.Add(dgvHistorialVentas);
            groupBox1.Location = new Point(9, 28);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(983, 659);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            // 
            // btnVerTodas
            // 
            btnVerTodas.Location = new Point(113, 31);
            btnVerTodas.Name = "btnVerTodas";
            btnVerTodas.Size = new Size(95, 40);
            btnVerTodas.TabIndex = 12;
            btnVerTodas.Text = "Ver Todas";
            btnVerTodas.UseVisualStyleBackColor = true;
            btnVerTodas.Click += btnVerTodas_Click;
            // 
            // bntBuscar
            // 
            bntBuscar.Location = new Point(603, 34);
            bntBuscar.Margin = new Padding(4, 3, 4, 3);
            bntBuscar.Name = "bntBuscar";
            bntBuscar.Size = new Size(53, 27);
            bntBuscar.TabIndex = 11;
            bntBuscar.Text = "Buscar";
            bntBuscar.UseVisualStyleBackColor = true;
            bntBuscar.Click += bntBuscar_Click;
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
            // dtpFecha
            // 
            dtpFecha.Location = new Point(346, 38);
            dtpFecha.Margin = new Padding(4, 3, 4, 3);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(249, 23);
            dtpFecha.TabIndex = 9;
            // 
            // dgvHistorialVentas
            // 
            dgvHistorialVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistorialVentas.Location = new Point(18, 80);
            dgvHistorialVentas.Margin = new Padding(4, 3, 4, 3);
            dgvHistorialVentas.Name = "dgvHistorialVentas";
            dgvHistorialVentas.Size = new Size(957, 568);
            dgvHistorialVentas.TabIndex = 6;
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
            Load += FormHistorialVentas_Load_1;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistorialVentas).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bntBuscar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.DataGridView dgvHistorialVentas;
        private System.Windows.Forms.Label label1;
        private Button btnVerTodas;
    }
}