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
            btnDardeBaja = new Button();
            btnGenerarOC = new Button();
            dgvOrdenDePedido = new DataGridView();
            btnVolver = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrdenDePedido).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(56, 40);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(131, 15);
            label1.TabIndex = 0;
            label1.Text = "Ver Ordenes de Pedidos";
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
            btnActualizar.Click += btnActualizar_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnDardeBaja);
            groupBox1.Controls.Add(btnGenerarOC);
            groupBox1.Controls.Add(dgvOrdenDePedido);
            groupBox1.Controls.Add(btnActualizar);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(14, 24);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(382, 525);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Orden de Pedido";
            // 
            // btnDardeBaja
            // 
            btnDardeBaja.Location = new Point(262, 463);
            btnDardeBaja.Margin = new Padding(4, 3, 4, 3);
            btnDardeBaja.Name = "btnDardeBaja";
            btnDardeBaja.Size = new Size(100, 52);
            btnDardeBaja.TabIndex = 5;
            btnDardeBaja.Text = "Dar de Baja";
            btnDardeBaja.UseVisualStyleBackColor = true;
            btnDardeBaja.Click += btnDardeBaja_Click;
            // 
            // btnGenerarOC
            // 
            btnGenerarOC.Location = new Point(22, 463);
            btnGenerarOC.Margin = new Padding(4, 3, 4, 3);
            btnGenerarOC.Name = "btnGenerarOC";
            btnGenerarOC.Size = new Size(177, 52);
            btnGenerarOC.TabIndex = 4;
            btnGenerarOC.Text = "Generar Orden de Compra";
            btnGenerarOC.UseVisualStyleBackColor = true;
            btnGenerarOC.Click += btnGenerarOC_Click;
            // 
            // dgvOrdenDePedido
            // 
            dgvOrdenDePedido.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrdenDePedido.Location = new Point(22, 70);
            dgvOrdenDePedido.Margin = new Padding(4, 3, 4, 3);
            dgvOrdenDePedido.Name = "dgvOrdenDePedido";
            dgvOrdenDePedido.Size = new Size(340, 385);
            dgvOrdenDePedido.TabIndex = 3;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(317, 572);
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
            ClientSize = new Size(415, 617);
            Controls.Add(btnVolver);
            Controls.Add(groupBox1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormGestiónOP";
            Text = "Gestión de Orden de Pedido";
            Load += FormGestiónOP_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrdenDePedido).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvOrdenDePedido;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnGenerarOC;
        private System.Windows.Forms.Button btnDardeBaja;
    }
}