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
            Detalle = new GroupBox();
            dgvDetalleOP = new DataGridView();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrdenDePedido).BeginInit();
            Detalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalleOP).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(81, 29);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(131, 15);
            label1.TabIndex = 0;
            label1.Text = "Ver Ordenes de Pedidos";
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(225, 22);
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
            groupBox1.Location = new Point(13, 20);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(411, 514);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Orden de Pedido";
            // 
            // btnDardeBaja
            // 
            btnDardeBaja.Location = new Point(294, 450);
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
            btnGenerarOC.Location = new Point(8, 450);
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
            dgvOrdenDePedido.Location = new Point(8, 57);
            dgvOrdenDePedido.Margin = new Padding(4, 3, 4, 3);
            dgvOrdenDePedido.Name = "dgvOrdenDePedido";
            dgvOrdenDePedido.Size = new Size(395, 387);
            dgvOrdenDePedido.TabIndex = 3;
            dgvOrdenDePedido.SelectionChanged += dgvOrdenDePedido_SelectionChanged;
            // 
            // Detalle
            // 
            Detalle.Controls.Add(dgvDetalleOP);
            Detalle.Location = new Point(431, 12);
            Detalle.Name = "Detalle";
            Detalle.Size = new Size(586, 522);
            Detalle.TabIndex = 4;
            Detalle.TabStop = false;
            Detalle.Text = "Detalle";
            // 
            // dgvDetalleOP
            // 
            dgvDetalleOP.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalleOP.Location = new Point(6, 22);
            dgvDetalleOP.Name = "dgvDetalleOP";
            dgvDetalleOP.Size = new Size(574, 494);
            dgvDetalleOP.TabIndex = 0;
            // 
            // FormGestiónOP
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1029, 547);
            Controls.Add(Detalle);
            Controls.Add(groupBox1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormGestiónOP";
            Text = "Gestión de Orden de Pedido";
            Load += FormGestiónOP_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrdenDePedido).EndInit();
            Detalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDetalleOP).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvOrdenDePedido;
        private System.Windows.Forms.Button btnGenerarOC;
        private System.Windows.Forms.Button btnDardeBaja;
        private GroupBox Detalle;
        private DataGridView dgvDetalleOP;
    }
}