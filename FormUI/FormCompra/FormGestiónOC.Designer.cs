using System.ComponentModel;

namespace FormUI.FormCompra
{
    partial class FormGestiónOC
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
            btnBaja = new Button();
            btnAlta = new Button();
            dgvOrdenCompra = new DataGridView();
            btnVer = new Button();
            btnVolver = new Button();
            groupBox2 = new GroupBox();
            dgvDetalleOC = new DataGridView();
            groupBox1.SuspendLayout();
            ((ISupportInitialize)dgvOrdenCompra).BeginInit();
            groupBox2.SuspendLayout();
            ((ISupportInitialize)dgvDetalleOC).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(82, 54);
            label1.Name = "label1";
            label1.Size = new Size(132, 15);
            label1.TabIndex = 0;
            label1.Text = "Ver Ordenes de Compra";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnBaja);
            groupBox1.Controls.Add(btnAlta);
            groupBox1.Controls.Add(dgvOrdenCompra);
            groupBox1.Controls.Add(btnVer);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(24, 22);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(397, 618);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Orden de Compra";
            // 
            // btnBaja
            // 
            btnBaja.Location = new Point(254, 547);
            btnBaja.Name = "btnBaja";
            btnBaja.Size = new Size(105, 53);
            btnBaja.TabIndex = 4;
            btnBaja.Text = "Dar de Baja";
            btnBaja.UseVisualStyleBackColor = true;
            btnBaja.Click += btnBaja_Click;
            // 
            // btnAlta
            // 
            btnAlta.Location = new Point(27, 547);
            btnAlta.Name = "btnAlta";
            btnAlta.Size = new Size(126, 53);
            btnAlta.TabIndex = 3;
            btnAlta.Text = "Dar de Alta";
            btnAlta.UseVisualStyleBackColor = true;
            btnAlta.Click += btnAlta_Click;
            // 
            // dgvOrdenCompra
            // 
            dgvOrdenCompra.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrdenCompra.Location = new Point(27, 82);
            dgvOrdenCompra.Name = "dgvOrdenCompra";
            dgvOrdenCompra.Size = new Size(332, 459);
            dgvOrdenCompra.TabIndex = 2;
            dgvOrdenCompra.CellContentClick += dgvOrdenCompra_CellContentClick;
            dgvOrdenCompra.SelectionChanged += dgvOrdenCompra_SelectionChanged;
            // 
            // btnVer
            // 
            btnVer.Location = new Point(220, 50);
            btnVer.Name = "btnVer";
            btnVer.Size = new Size(48, 23);
            btnVer.TabIndex = 1;
            btnVer.Text = "Ver";
            btnVer.UseVisualStyleBackColor = true;
            btnVer.Click += btnVer_Click;
            // 
            // btnVolver
            // 
            btnVolver.Location = new Point(358, 646);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(75, 23);
            btnVolver.TabIndex = 5;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvDetalleOC);
            groupBox2.Location = new Point(444, 22);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(399, 579);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Detalles de Orden de Compra";
            // 
            // dgvDetalleOC
            // 
            dgvDetalleOC.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalleOC.Location = new Point(20, 36);
            dgvDetalleOC.Name = "dgvDetalleOC";
            dgvDetalleOC.Size = new Size(358, 519);
            dgvDetalleOC.TabIndex = 0;
            // 
            // FormGestiónOC
            // 
            ClientSize = new Size(902, 678);
            Controls.Add(groupBox2);
            Controls.Add(btnVolver);
            Controls.Add(groupBox1);
            Name = "FormGestiónOC";
            Text = "Gestión de Orden de Compra";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((ISupportInitialize)dgvOrdenCompra).EndInit();
            groupBox2.ResumeLayout(false);
            ((ISupportInitialize)dgvDetalleOC).EndInit();
            ResumeLayout(false);


        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private DataGridView dgvOrdenCompra;
        private Button btnVer;
        private Button btnBaja;
        private Button btnAlta;
        private Button btnVolver;
        private GroupBox groupBox2;
        private DataGridView dgvDetalleOC;
    }
}