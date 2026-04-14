namespace FormUI.FormVenta
{
    partial class FormGestiónVenta
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
            label1 = new Label();
            groupBox1 = new GroupBox();
            label2 = new Label();
            btnEliminar = new Button();
            dgvVentasRealizadas = new DataGridView();
            dateTimePickerVenta = new DateTimePicker();
            btnActualizar = new Button();
            groupBox2 = new GroupBox();
            dgvDetallesVenta = new DataGridView();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVentasRealizadas).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetallesVenta).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(322, 10);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(94, 15);
            label1.TabIndex = 10;
            label1.Text = "Gestionar Ventas";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnEliminar);
            groupBox1.Controls.Add(dgvVentasRealizadas);
            groupBox1.Controls.Add(dateTimePickerVenta);
            groupBox1.Controls.Add(btnActualizar);
            groupBox1.Location = new Point(13, 28);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(640, 705);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(135, 46);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(104, 15);
            label2.TabIndex = 7;
            label2.Text = "Seleccionar Fecha:";
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(436, 74);
            btnEliminar.Margin = new Padding(4, 3, 4, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(88, 27);
            btnEliminar.TabIndex = 7;
            btnEliminar.Tag = "Anular_Venta";
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // dgvVentasRealizadas
            // 
            dgvVentasRealizadas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVentasRealizadas.Location = new Point(18, 115);
            dgvVentasRealizadas.Margin = new Padding(4, 3, 4, 3);
            dgvVentasRealizadas.Name = "dgvVentasRealizadas";
            dgvVentasRealizadas.Size = new Size(602, 562);
            dgvVentasRealizadas.TabIndex = 6;
            dgvVentasRealizadas.CellDoubleClick += dgvVentasRealizadas_CellDoubleClick;
            dgvVentasRealizadas.SelectionChanged += dgvVentasRealizadas_SelectionChanged;
            dgvVentasRealizadas.DoubleClick += dgvVentasRealizadas_DoubleClick;
            // 
            // dateTimePickerVenta
            // 
            dateTimePickerVenta.Location = new Point(138, 74);
            dateTimePickerVenta.Margin = new Padding(4, 3, 4, 3);
            dateTimePickerVenta.Name = "dateTimePickerVenta";
            dateTimePickerVenta.Size = new Size(249, 23);
            dateTimePickerVenta.TabIndex = 5;
            dateTimePickerVenta.ValueChanged += dateTimePickerVenta_ValueChanged;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(532, 74);
            btnActualizar.Margin = new Padding(4, 3, 4, 3);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(88, 27);
            btnActualizar.TabIndex = 3;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvDetallesVenta);
            groupBox2.Location = new Point(660, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(464, 721);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            groupBox2.Text = "Detalle";
            // 
            // dgvDetallesVenta
            // 
            dgvDetallesVenta.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetallesVenta.Location = new Point(6, 22);
            dgvDetallesVenta.Name = "dgvDetallesVenta";
            dgvDetallesVenta.Size = new Size(452, 694);
            dgvDetallesVenta.TabIndex = 0;
            // 
            // FormGestiónVenta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1136, 740);
            Controls.Add(groupBox2);
            Controls.Add(label1);
            Controls.Add(groupBox1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormGestiónVenta";
            Text = "Gestiona de Ventas";
            Load += FormGestiónVenta_Load_1;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVentasRealizadas).EndInit();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDetallesVenta).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bntBuscar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.DataGridView dgvVentasRealizadas;
        private System.Windows.Forms.DateTimePicker dateTimePickerVenta;
        private System.Windows.Forms.Button btnActualizar;
        private GroupBox groupBox2;
        private DataGridView dgvDetallesVenta;
    }
}