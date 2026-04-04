namespace FormUI.Inicio
{
    partial class FormBitácora
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
            btnVer = new Button();
            dgvBitácora = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvBitácora).BeginInit();
            SuspendLayout();
            // 
            // btnVer
            // 
            btnVer.Location = new Point(236, 12);
            btnVer.Name = "btnVer";
            btnVer.Size = new Size(93, 35);
            btnVer.TabIndex = 0;
            btnVer.Text = "Ver Bitácora";
            btnVer.UseVisualStyleBackColor = true;
            btnVer.Click += btnVer_Click;
            // 
            // dgvBitácora
            // 
            dgvBitácora.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBitácora.Location = new Point(37, 73);
            dgvBitácora.Name = "dgvBitácora";
            dgvBitácora.Size = new Size(509, 474);
            dgvBitácora.TabIndex = 2;
            // 
            // FormBitácora
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(581, 576);
            Controls.Add(dgvBitácora);
            Controls.Add(btnVer);
            Name = "FormBitácora";
            Text = "Bitácora";
            ((System.ComponentModel.ISupportInitialize)dgvBitácora).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnVer;
        private DataGridView dgvBitácora;
    }
}