namespace FormUI.FormCompra
{
    partial class FormGestiónSP
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
            dgvSolicitudDePedido = new DataGridView();
            btnDardeBaja = new Button();
            btnGenerarOP = new Button();
            btnVolver = new Button();
            btnActualizar = new Button();
            label1 = new Label();
            groupBox1 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dgvSolicitudDePedido).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvSolicitudDePedido
            // 
            dgvSolicitudDePedido.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSolicitudDePedido.Location = new Point(14, 67);
            dgvSolicitudDePedido.Margin = new Padding(4, 3, 4, 3);
            dgvSolicitudDePedido.Name = "dgvSolicitudDePedido";
            dgvSolicitudDePedido.Size = new Size(341, 358);
            dgvSolicitudDePedido.TabIndex = 0;
            // 
            // btnDardeBaja
            // 
            btnDardeBaja.Location = new Point(249, 433);
            btnDardeBaja.Margin = new Padding(4, 3, 4, 3);
            btnDardeBaja.Name = "btnDardeBaja";
            btnDardeBaja.Size = new Size(106, 44);
            btnDardeBaja.TabIndex = 3;
            btnDardeBaja.Text = "Dar de Baja";
            btnDardeBaja.UseVisualStyleBackColor = true;
            btnDardeBaja.Click += btnDardeBaja_Click;
            // 
            // btnGenerarOP
            // 
            btnGenerarOP.Location = new Point(11, 435);
            btnGenerarOP.Margin = new Padding(4, 3, 4, 3);
            btnGenerarOP.Name = "btnGenerarOP";
            btnGenerarOP.Size = new Size(181, 40);
            btnGenerarOP.TabIndex = 5;
            btnGenerarOP.Text = "Generar Orden de Pedido";
            btnGenerarOP.UseVisualStyleBackColor = true;
            btnGenerarOP.Click += btnGenerarOP_Click;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(289, 534);
            btnVolver.Margin = new Padding(4, 3, 4, 3);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(91, 31);
            btnVolver.TabIndex = 6;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(235, 23);
            btnActualizar.Margin = new Padding(4, 3, 4, 3);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(82, 28);
            btnActualizar.TabIndex = 7;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(62, 30);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(147, 15);
            label1.TabIndex = 8;
            label1.Text = "Ver Solicitudes de Pedidos:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(dgvSolicitudDePedido);
            groupBox1.Controls.Add(btnActualizar);
            groupBox1.Controls.Add(btnDardeBaja);
            groupBox1.Controls.Add(btnGenerarOP);
            groupBox1.Location = new Point(12, 21);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(368, 484);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "Solicitud de Pedido";
            // 
            // FormGestiónSP
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(398, 573);
            Controls.Add(groupBox1);
            Controls.Add(btnVolver);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FormGestiónSP";
            Text = "Gestión de Solicitud de Pedido";
            Load += FormGestiónSP_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSolicitudDePedido).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSolicitudDePedido;
        private System.Windows.Forms.Button btnDardeBaja;
        private System.Windows.Forms.Button btnGenerarOP;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label label1;
        private GroupBox groupBox1;
    }
}