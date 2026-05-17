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
            btnAU = new PictureBox();
            btnAtras = new PictureBox();
            AñadirU = new PictureBox();
            EditarU = new PictureBox();
            EliminarU = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)btnAU).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnAtras).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AñadirU).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EditarU).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EliminarU).BeginInit();
            SuspendLayout();
            // 
            // btnAU
            // 
            btnAU.Image = (Image)resources.GetObject("btnAU.Image");
            btnAU.Location = new Point(-1, 0);
            btnAU.Name = "btnAU";
            btnAU.Size = new Size(1112, 639);
            btnAU.SizeMode = PictureBoxSizeMode.StretchImage;
            btnAU.TabIndex = 5;
            btnAU.TabStop = false;
            // 
            // btnAtras
            // 
            btnAtras.Cursor = Cursors.Hand;
            btnAtras.Image = (Image)resources.GetObject("btnAtras.Image");
            btnAtras.Location = new Point(26, 573);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(139, 45);
            btnAtras.SizeMode = PictureBoxSizeMode.StretchImage;
            btnAtras.TabIndex = 7;
            btnAtras.TabStop = false;
            btnAtras.Click += btnAtras_Click;
            // 
            // AñadirU
            // 
            AñadirU.Cursor = Cursors.Hand;
            AñadirU.Image = (Image)resources.GetObject("AñadirU.Image");
            AñadirU.Location = new Point(629, 44);
            AñadirU.Name = "AñadirU";
            AñadirU.Size = new Size(139, 44);
            AñadirU.SizeMode = PictureBoxSizeMode.StretchImage;
            AñadirU.TabIndex = 8;
            AñadirU.TabStop = false;
            AñadirU.Click += Añadir_U_Click;
            // 
            // EditarU
            // 
            EditarU.Cursor = Cursors.Hand;
            EditarU.Image = (Image)resources.GetObject("EditarU.Image");
            EditarU.Location = new Point(774, 44);
            EditarU.Name = "EditarU";
            EditarU.Size = new Size(139, 44);
            EditarU.SizeMode = PictureBoxSizeMode.StretchImage;
            EditarU.TabIndex = 9;
            EditarU.TabStop = false;
            EditarU.Click += EditarU_Click;
            // 
            // EliminarU
            // 
            EliminarU.Cursor = Cursors.Hand;
            EliminarU.Image = (Image)resources.GetObject("EliminarU.Image");
            EliminarU.Location = new Point(919, 44);
            EliminarU.Name = "EliminarU";
            EliminarU.Size = new Size(154, 44);
            EliminarU.SizeMode = PictureBoxSizeMode.StretchImage;
            EliminarU.TabIndex = 10;
            EliminarU.TabStop = false;
            EliminarU.Click += EliminarU_Click;
            // 
            // Form6
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1112, 639);
            Controls.Add(EliminarU);
            Controls.Add(EditarU);
            Controls.Add(AñadirU);
            Controls.Add(btnAtras);
            Controls.Add(btnAU);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form6";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form6";
            Load += Form6_Load;
            ((System.ComponentModel.ISupportInitialize)btnAU).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnAtras).EndInit();
            ((System.ComponentModel.ISupportInitialize)AñadirU).EndInit();
            ((System.ComponentModel.ISupportInitialize)EditarU).EndInit();
            ((System.ComponentModel.ISupportInitialize)EliminarU).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox btnAU;
        private PictureBox btnAtras;
        private PictureBox AñadirU;
        private PictureBox pictureBox1;
        private PictureBox EliminarU;
        private PictureBox EditarU;
       
    }
}