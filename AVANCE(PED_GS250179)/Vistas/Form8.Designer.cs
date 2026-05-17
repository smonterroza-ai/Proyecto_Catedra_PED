namespace AVANCE_PED_GS250179_.Vistas
{
    partial class Form8
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form8));
            txtBuscar = new TextBox();
            dgvRutas = new DataGridView();
            label6 = new Label();
            btnEliminar = new PictureBox();
            btnEditarM = new PictureBox();
            btnAR = new PictureBox();
            btnAtras = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dgvRutas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnEliminar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnEditarM).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnAR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnAtras).BeginInit();
            SuspendLayout();
            // 
            // txtBuscar
            // 
            txtBuscar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtBuscar.ForeColor = SystemColors.WindowText;
            txtBuscar.Location = new Point(767, 103);
            txtBuscar.Margin = new Padding(3, 4, 3, 4);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por placa";
            txtBuscar.Size = new Size(282, 29);
            txtBuscar.TabIndex = 21;
            // 
            // dgvRutas
            // 
            dgvRutas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRutas.Location = new Point(94, 144);
            dgvRutas.Name = "dgvRutas";
            dgvRutas.RowHeadersWidth = 51;
            dgvRutas.Size = new Size(955, 301);
            dgvRutas.TabIndex = 20;
            // 
            // label6
            // 
            label6.BackColor = Color.FromArgb(0, 0, 64);
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.ImageAlign = ContentAlignment.BottomCenter;
            label6.Location = new Point(94, 95);
            label6.Name = "label6";
            label6.Size = new Size(545, 37);
            label6.TabIndex = 19;
            label6.Text = "GESTION DE RUTAS";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnEliminar
            // 
            btnEliminar.Cursor = Cursors.Hand;
            btnEliminar.Image = (Image)resources.GetObject("btnEliminar.Image");
            btnEliminar.Location = new Point(967, 22);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(126, 40);
            btnEliminar.SizeMode = PictureBoxSizeMode.StretchImage;
            btnEliminar.TabIndex = 18;
            btnEliminar.TabStop = false;
            // 
            // btnEditarM
            // 
            btnEditarM.Cursor = Cursors.Hand;
            btnEditarM.Image = (Image)resources.GetObject("btnEditarM.Image");
            btnEditarM.Location = new Point(821, 22);
            btnEditarM.Name = "btnEditarM";
            btnEditarM.Size = new Size(126, 40);
            btnEditarM.SizeMode = PictureBoxSizeMode.StretchImage;
            btnEditarM.TabIndex = 17;
            btnEditarM.TabStop = false;
            // 
            // btnAR
            // 
            btnAR.Cursor = Cursors.Hand;
            btnAR.Image = (Image)resources.GetObject("btnAR.Image");
            btnAR.Location = new Point(677, 22);
            btnAR.Name = "btnAR";
            btnAR.Size = new Size(126, 40);
            btnAR.SizeMode = PictureBoxSizeMode.StretchImage;
            btnAR.TabIndex = 16;
            btnAR.TabStop = false;
            // 
            // btnAtras
            // 
            btnAtras.Cursor = Cursors.Hand;
            btnAtras.Image = (Image)resources.GetObject("btnAtras.Image");
            btnAtras.Location = new Point(46, 521);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(139, 45);
            btnAtras.SizeMode = PictureBoxSizeMode.StretchImage;
            btnAtras.TabIndex = 15;
            btnAtras.TabStop = false;
            // 
            // Form8
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondo1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1139, 589);
            Controls.Add(txtBuscar);
            Controls.Add(dgvRutas);
            Controls.Add(label6);
            Controls.Add(btnEliminar);
            Controls.Add(btnEditarM);
            Controls.Add(btnAR);
            Controls.Add(btnAtras);
            Name = "Form8";
            Text = "Form8";
            ((System.ComponentModel.ISupportInitialize)dgvRutas).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnEliminar).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnEditarM).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnAR).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnAtras).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtBuscar;
        private DataGridView dgvRutas;
        private Label label6;
        private PictureBox btnEliminar;
        private PictureBox btnEditarM;
        private PictureBox btnAR;
        private PictureBox btnAtras;
    }
}