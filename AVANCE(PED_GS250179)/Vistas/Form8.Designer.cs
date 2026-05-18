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
            txtBuscar.Location = new Point(671, 77);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por placa";
            txtBuscar.Size = new Size(247, 25);
            txtBuscar.TabIndex = 21;
            // 
            // dgvRutas
            // 
            dgvRutas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRutas.Location = new Point(82, 108);
            dgvRutas.Margin = new Padding(3, 2, 3, 2);
            dgvRutas.Name = "dgvRutas";
            dgvRutas.RowHeadersWidth = 51;
            dgvRutas.Size = new Size(836, 226);
            dgvRutas.TabIndex = 20;
            // 
            // label6
            // 
            label6.BackColor = Color.FromArgb(0, 0, 64);
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.ImageAlign = ContentAlignment.BottomCenter;
            label6.Location = new Point(82, 71);
            label6.Name = "label6";
            label6.Size = new Size(477, 28);
            label6.TabIndex = 19;
            label6.Text = "GESTION DE CONDUCTORES";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnEliminar
            // 
            btnEliminar.Cursor = Cursors.Hand;
            btnEliminar.Image = (Image)resources.GetObject("btnEliminar.Image");
            btnEliminar.Location = new Point(846, 16);
            btnEliminar.Margin = new Padding(3, 2, 3, 2);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(110, 30);
            btnEliminar.SizeMode = PictureBoxSizeMode.StretchImage;
            btnEliminar.TabIndex = 18;
            btnEliminar.TabStop = false;
            // 
            // btnEditarM
            // 
            btnEditarM.Cursor = Cursors.Hand;
            btnEditarM.Image = (Image)resources.GetObject("btnEditarM.Image");
            btnEditarM.Location = new Point(718, 16);
            btnEditarM.Margin = new Padding(3, 2, 3, 2);
            btnEditarM.Name = "btnEditarM";
            btnEditarM.Size = new Size(110, 30);
            btnEditarM.SizeMode = PictureBoxSizeMode.StretchImage;
            btnEditarM.TabIndex = 17;
            btnEditarM.TabStop = false;
            // 
            // btnAR
            // 
            btnAR.Cursor = Cursors.Hand;
            btnAR.Image = (Image)resources.GetObject("btnAR.Image");
            btnAR.Location = new Point(592, 16);
            btnAR.Margin = new Padding(3, 2, 3, 2);
            btnAR.Name = "btnAR";
            btnAR.Size = new Size(110, 30);
            btnAR.SizeMode = PictureBoxSizeMode.StretchImage;
            btnAR.TabIndex = 16;
            btnAR.TabStop = false;
            // 
            // btnAtras
            // 
            btnAtras.Cursor = Cursors.Hand;
            btnAtras.Image = (Image)resources.GetObject("btnAtras.Image");
            btnAtras.Location = new Point(32, 397);
            btnAtras.Margin = new Padding(3, 2, 3, 2);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(122, 34);
            btnAtras.SizeMode = PictureBoxSizeMode.StretchImage;
            btnAtras.TabIndex = 15;
            btnAtras.TabStop = false;
            // 
            // Form8
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondo1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(997, 442);
            ControlBox = false;
            Controls.Add(txtBuscar);
            Controls.Add(dgvRutas);
            Controls.Add(label6);
            Controls.Add(btnEliminar);
            Controls.Add(btnEditarM);
            Controls.Add(btnAR);
            Controls.Add(btnAtras);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
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