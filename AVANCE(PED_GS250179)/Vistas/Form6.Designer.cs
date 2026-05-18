namespace AVANCE_PED_GS250179_
{
    partial class Form6
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form6));
            btnAtras = new PictureBox();
            AñadirU = new PictureBox();
            EditarU = new PictureBox();
            EliminarU = new PictureBox();
            dgvUnidades = new DataGridView();
            label1 = new Label();
            txtBuscar = new TextBox();
            ((System.ComponentModel.ISupportInitialize)btnAtras).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AñadirU).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EditarU).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EliminarU).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvUnidades).BeginInit();
            SuspendLayout();
            // 
            // btnAtras
            // 
            btnAtras.Cursor = Cursors.Hand;
            btnAtras.Image = (Image)resources.GetObject("btnAtras.Image");
            btnAtras.Location = new Point(33, 434);
            btnAtras.Margin = new Padding(3, 2, 3, 2);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(122, 34);
            btnAtras.SizeMode = PictureBoxSizeMode.StretchImage;
            btnAtras.TabIndex = 7;
            btnAtras.TabStop = false;
            btnAtras.Click += btnAtras_Click;
            // 
            // AñadirU
            // 
            AñadirU.Cursor = Cursors.Hand;
            AñadirU.Image = (Image)resources.GetObject("AñadirU.Image");
            AñadirU.Location = new Point(514, 21);
            AñadirU.Margin = new Padding(3, 2, 3, 2);
            AñadirU.Name = "AñadirU";
            AñadirU.Size = new Size(122, 33);
            AñadirU.SizeMode = PictureBoxSizeMode.StretchImage;
            AñadirU.TabIndex = 8;
            AñadirU.TabStop = false;
            AñadirU.Click += Añadir_U_Click;
            // 
            // EditarU
            // 
            EditarU.Cursor = Cursors.Hand;
            EditarU.Image = (Image)resources.GetObject("EditarU.Image");
            EditarU.Location = new Point(641, 21);
            EditarU.Margin = new Padding(3, 2, 3, 2);
            EditarU.Name = "EditarU";
            EditarU.Size = new Size(122, 33);
            EditarU.SizeMode = PictureBoxSizeMode.StretchImage;
            EditarU.TabIndex = 9;
            EditarU.TabStop = false;
            EditarU.Click += EditarU_Click;
            // 
            // EliminarU
            // 
            EliminarU.Cursor = Cursors.Hand;
            EliminarU.Image = (Image)resources.GetObject("EliminarU.Image");
            EliminarU.Location = new Point(768, 21);
            EliminarU.Margin = new Padding(3, 2, 3, 2);
            EliminarU.Name = "EliminarU";
            EliminarU.Size = new Size(135, 33);
            EliminarU.SizeMode = PictureBoxSizeMode.StretchImage;
            EliminarU.TabIndex = 10;
            EliminarU.TabStop = false;
            EliminarU.Click += EliminarU_Click;
            // 
            // dgvUnidades
            // 
            dgvUnidades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUnidades.Location = new Point(78, 146);
            dgvUnidades.Name = "dgvUnidades";
            dgvUnidades.Size = new Size(782, 238);
            dgvUnidades.TabIndex = 11;
            // 
            // label1
            // 
            label1.BackColor = Color.MidnightBlue;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(78, 94);
            label1.Name = "label1";
            label1.Size = new Size(371, 29);
            label1.TabIndex = 12;
            label1.Text = "GESTIÓN DE UNIDADES";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtBuscar
            // 
            txtBuscar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtBuscar.ForeColor = SystemColors.WindowText;
            txtBuscar.Location = new Point(613, 98);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por placa";
            txtBuscar.Size = new Size(247, 25);
            txtBuscar.TabIndex = 13;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // Form6
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondo1;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(936, 479);
            Controls.Add(txtBuscar);
            Controls.Add(label1);
            Controls.Add(dgvUnidades);
            Controls.Add(EliminarU);
            Controls.Add(EditarU);
            Controls.Add(AñadirU);
            Controls.Add(btnAtras);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form6";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form6";
            Load += Form6_Load;
            ((System.ComponentModel.ISupportInitialize)btnAtras).EndInit();
            ((System.ComponentModel.ISupportInitialize)AñadirU).EndInit();
            ((System.ComponentModel.ISupportInitialize)EditarU).EndInit();
            ((System.ComponentModel.ISupportInitialize)EliminarU).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvUnidades).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox btnAtras;
        private PictureBox AñadirU;
        private PictureBox pictureBox1;
        private PictureBox EliminarU;
        private PictureBox EditarU;
        private DataGridView dgvUnidades;
        private Label label1;
        private TextBox txtBuscar;
    }
}