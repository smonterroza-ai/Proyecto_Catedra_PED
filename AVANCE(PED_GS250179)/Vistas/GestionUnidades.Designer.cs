namespace AVANCE_PED_GS250179_
{
    partial class GestionUnidades
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GestionUnidades));
            EliminarU = new PictureBox();
            EditarU = new PictureBox();
            btnAñadir = new PictureBox();
            btnAtras = new PictureBox();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)EliminarU).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EditarU).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnAñadir).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnAtras).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // EliminarU
            // 
            EliminarU.Cursor = Cursors.Hand;
            EliminarU.Image = (Image)resources.GetObject("EliminarU.Image");
            EliminarU.Location = new Point(791, 11);
            EliminarU.Margin = new Padding(3, 2, 3, 2);
            EliminarU.Name = "EliminarU";
            EliminarU.Size = new Size(135, 33);
            EliminarU.SizeMode = PictureBoxSizeMode.StretchImage;
            EliminarU.TabIndex = 14;
            EliminarU.TabStop = false;
            // 
            // EditarU
            // 
            EditarU.Cursor = Cursors.Hand;
            EditarU.Image = (Image)resources.GetObject("EditarU.Image");
            EditarU.Location = new Point(664, 11);
            EditarU.Margin = new Padding(3, 2, 3, 2);
            EditarU.Name = "EditarU";
            EditarU.Size = new Size(122, 33);
            EditarU.SizeMode = PictureBoxSizeMode.StretchImage;
            EditarU.TabIndex = 13;
            EditarU.TabStop = false;
            EditarU.Click += this.EditarU_Click;
            // 
            // btnAñadir
            // 
            btnAñadir.Cursor = Cursors.Hand;
            btnAñadir.Image = (Image)resources.GetObject("btnAñadir.Image");
            btnAñadir.Location = new Point(537, 11);
            btnAñadir.Margin = new Padding(3, 2, 3, 2);
            btnAñadir.Name = "btnAñadir";
            btnAñadir.Size = new Size(122, 33);
            btnAñadir.SizeMode = PictureBoxSizeMode.StretchImage;
            btnAñadir.TabIndex = 12;
            btnAñadir.TabStop = false;
            btnAñadir.Click += this.EditarU_Click;
            // 
            // btnAtras
            // 
            btnAtras.Cursor = Cursors.Hand;
            btnAtras.Image = (Image)resources.GetObject("btnAtras.Image");
            btnAtras.Location = new Point(32, 443);
            btnAtras.Margin = new Padding(3, 2, 3, 2);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(122, 34);
            btnAtras.SizeMode = PictureBoxSizeMode.StretchImage;
            btnAtras.TabIndex = 11;
            btnAtras.TabStop = false;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(83, 130);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(781, 268);
            dataGridView1.TabIndex = 15;
            // 
            // label1
            // 
            label1.BackColor = Color.MidnightBlue;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(269, 88);
            label1.Name = "label1";
            label1.Size = new Size(401, 26);
            label1.TabIndex = 16;
            label1.Text = "Gestión de unidades";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GestionUnidades
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondo1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(957, 488);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(EliminarU);
            Controls.Add(EditarU);
            Controls.Add(btnAñadir);
            Controls.Add(btnAtras);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "GestionUnidades";
            Text = "Gestion de unidades";
            ((System.ComponentModel.ISupportInitialize)EliminarU).EndInit();
            ((System.ComponentModel.ISupportInitialize)EditarU).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnAñadir).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnAtras).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox EliminarU;
        private PictureBox EditarU;
        private PictureBox btnAñadir;
        private PictureBox btnAtras;
        private DataGridView dataGridView1;
        private Label label1;
    }
}