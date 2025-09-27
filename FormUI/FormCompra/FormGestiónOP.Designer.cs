namespace FormUI.FormCompra
{
    partial class FormGestiónOP
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
            btnActualizar = new Button();
            groupBox1 = new GroupBox();
            btnBaja = new Button();
            btnAlta = new Button();
            dataGridView1 = new DataGridView();
            btnVolver = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(56, 40);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(128, 15);
            label1.TabIndex = 0;
            label1.Text = "Ver Solicitud de Pedido";
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(200, 33);
            btnActualizar.Margin = new Padding(4, 3, 4, 3);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(78, 29);
            btnActualizar.TabIndex = 1;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnBaja);
            groupBox1.Controls.Add(btnAlta);
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Controls.Add(btnActualizar);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(14, 24);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(955, 542);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Orden de Pedido";
            //groupBox1.Enter += this.groupBox1_Enter_1;
            // 
            // btnBaja
            // 
            btnBaja.Location = new Point(523, 463);
            btnBaja.Margin = new Padding(4, 3, 4, 3);
            btnBaja.Name = "btnBaja";
            btnBaja.Size = new Size(200, 52);
            btnBaja.TabIndex = 5;
            btnBaja.Text = "Dar de Baja";
            btnBaja.UseVisualStyleBackColor = true;
            // 
            // btnAlta
            // 
            btnAlta.Location = new Point(281, 463);
            btnAlta.Margin = new Padding(4, 3, 4, 3);
            btnAlta.Name = "btnAlta";
            btnAlta.Size = new Size(200, 52);
            btnAlta.TabIndex = 4;
            btnAlta.Text = "Dar de Alta";
            btnAlta.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(22, 70);
            dataGridView1.Margin = new Padding(4, 3, 4, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(912, 385);
            dataGridView1.TabIndex = 3;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(890, 573);
            btnVolver.Margin = new Padding(4, 3, 4, 3);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(79, 33);
            btnVolver.TabIndex = 4;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            // 
            // FormGestiónOP
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(983, 617);
            Controls.Add(btnVolver);
            Controls.Add(groupBox1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormGestiónOP";
            Text = "Gestión de Orden de Pedido";
            Load += FormGestiónOP_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnAlta;
        private System.Windows.Forms.Button btnBaja;
    }
}