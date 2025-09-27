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
            btnVolver = new Button();
            groupBox1 = new GroupBox();
            bntBuscar = new Button();
            label2 = new Label();
            btnEliminar = new Button();
            dataGridView1 = new DataGridView();
            datetimeEliminarVenta = new DateTimePicker();
            btnActualizar = new Button();
            btnModificar = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
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
            // btnVolver
            // 
            btnVolver.Location = new Point(359, 741);
            btnVolver.Margin = new Padding(4, 3, 4, 3);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(88, 27);
            btnVolver.TabIndex = 9;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(bntBuscar);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnEliminar);
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Controls.Add(datetimeEliminarVenta);
            groupBox1.Controls.Add(btnActualizar);
            groupBox1.Controls.Add(btnModificar);
            groupBox1.Location = new Point(31, 40);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(730, 693);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            // 
            // bntBuscar
            // 
            bntBuscar.Location = new Point(294, 52);
            bntBuscar.Margin = new Padding(4, 3, 4, 3);
            bntBuscar.Name = "bntBuscar";
            bntBuscar.Size = new Size(59, 27);
            bntBuscar.TabIndex = 8;
            bntBuscar.Text = "Buscar";
            bntBuscar.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(34, 27);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(104, 15);
            label2.TabIndex = 7;
            label2.Text = "Seleccionar Fecha:";
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(491, 72);
            btnEliminar.Margin = new Padding(4, 3, 4, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(88, 27);
            btnEliminar.TabIndex = 7;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(18, 115);
            dataGridView1.Margin = new Padding(4, 3, 4, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(694, 562);
            dataGridView1.TabIndex = 6;
            // 
            // datetimeEliminarVenta
            // 
            datetimeEliminarVenta.Location = new Point(37, 55);
            datetimeEliminarVenta.Margin = new Padding(4, 3, 4, 3);
            datetimeEliminarVenta.Name = "datetimeEliminarVenta";
            datetimeEliminarVenta.Size = new Size(249, 23);
            datetimeEliminarVenta.TabIndex = 5;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(586, 72);
            btnActualizar.Margin = new Padding(4, 3, 4, 3);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(88, 27);
            btnActualizar.TabIndex = 3;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(397, 72);
            btnModificar.Margin = new Padding(4, 3, 4, 3);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(88, 27);
            btnModificar.TabIndex = 2;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            // 
            // FormGestiónVenta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(797, 786);
            Controls.Add(label1);
            Controls.Add(btnVolver);
            Controls.Add(groupBox1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormGestiónVenta";
            Text = "Gestiona de Ventas";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bntBuscar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker datetimeEliminarVenta;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnModificar;
    }
}