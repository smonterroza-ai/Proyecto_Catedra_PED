namespace AVANCE_PED_GS250179_
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            btnAtras = new PictureBox();
            btnAR = new PictureBox();
            btnEditarR = new PictureBox();
            btnEliminarRuta = new PictureBox();
            label3 = new Label();
            label6 = new Label();
            dgvRutas = new DataGridView();
            txtBuscar = new TextBox();
            ((System.ComponentModel.ISupportInitialize)btnAtras).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnAR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnEditarR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnEliminarRuta).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRutas).BeginInit();
            SuspendLayout();
            // 
            // btnAtras
            // 
            btnAtras.Cursor = Cursors.Hand;
            btnAtras.Image = (Image)resources.GetObject("btnAtras.Image");
            btnAtras.Location = new Point(26, 558);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(139, 45);
            btnAtras.SizeMode = PictureBoxSizeMode.StretchImage;
            btnAtras.TabIndex = 3;
            btnAtras.TabStop = false;
            btnAtras.Click += btnAtras_Click;
            // 
            // btnAR
            // 
            btnAR.Cursor = Cursors.Hand;
            btnAR.Image = (Image)resources.GetObject("btnAR.Image");
            btnAR.Location = new Point(657, 59);
            btnAR.Name = "btnAR";
            btnAR.Size = new Size(126, 40);
            btnAR.SizeMode = PictureBoxSizeMode.StretchImage;
            btnAR.TabIndex = 4;
            btnAR.TabStop = false;
            btnAR.Click += btnAR_Click;
            // 
            // btnEditarR
            // 
            btnEditarR.Cursor = Cursors.Hand;
            btnEditarR.Image = (Image)resources.GetObject("btnEditarR.Image");
            btnEditarR.Location = new Point(801, 59);
            btnEditarR.Name = "btnEditarR";
            btnEditarR.Size = new Size(126, 40);
            btnEditarR.SizeMode = PictureBoxSizeMode.StretchImage;
            btnEditarR.TabIndex = 5;
            btnEditarR.TabStop = false;
            btnEditarR.Click += btnEditarR_Click;
            // 
            // btnEliminarRuta
            // 
            btnEliminarRuta.Cursor = Cursors.Hand;
            btnEliminarRuta.Image = (Image)resources.GetObject("btnEliminarRuta.Image");
            btnEliminarRuta.Location = new Point(947, 59);
            btnEliminarRuta.Name = "btnEliminarRuta";
            btnEliminarRuta.Size = new Size(126, 40);
            btnEliminarRuta.SizeMode = PictureBoxSizeMode.StretchImage;
            btnEliminarRuta.TabIndex = 6;
            btnEliminarRuta.TabStop = false;
            btnEliminarRuta.Click += btnEliminarRuta_Click;
            // 
            // label3
            // 
            label3.BackColor = SystemColors.ActiveCaptionText;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.ImageAlign = ContentAlignment.BottomCenter;
            label3.Location = new Point(-2, -1);
            label3.Name = "label3";
            label3.Size = new Size(1115, 32);
            label3.TabIndex = 9;
            // 
            // label6
            // 
            label6.BackColor = Color.FromArgb(0, 0, 64);
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.ImageAlign = ContentAlignment.BottomCenter;
            label6.Location = new Point(74, 132);
            label6.Name = "label6";
            label6.Size = new Size(545, 37);
            label6.TabIndex = 12;
            label6.Text = "GESTION DE RUTAS";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvRutas
            // 
            dgvRutas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRutas.Location = new Point(74, 181);
            dgvRutas.Name = "dgvRutas";
            dgvRutas.RowHeadersWidth = 51;
            dgvRutas.Size = new Size(955, 301);
            dgvRutas.TabIndex = 13;
            // 
            // txtBuscar
            // 
            txtBuscar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtBuscar.ForeColor = SystemColors.WindowText;
            txtBuscar.Location = new Point(747, 140);
            txtBuscar.Margin = new Padding(3, 4, 3, 4);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por placa";
            txtBuscar.Size = new Size(282, 29);
            txtBuscar.TabIndex = 14;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondo1;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(1112, 639);
            Controls.Add(txtBuscar);
            Controls.Add(dgvRutas);
            Controls.Add(label6);
            Controls.Add(label3);
            Controls.Add(btnEliminarRuta);
            Controls.Add(btnEditarR);
            Controls.Add(btnAR);
            Controls.Add(btnAtras);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form3";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Rutas";
            Load += Form3_Load;
            ((System.ComponentModel.ISupportInitialize)btnAtras).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnAR).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnEditarR).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnEliminarRuta).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRutas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox btnAtras;
        private PictureBox btnAR;
        private PictureBox btnEditarR;
        private PictureBox btnEliminarRuta;
        private Label label3;
        private Label label6;
        private DataGridView dgvRutas;
        private TextBox txtBuscar;
    }
}