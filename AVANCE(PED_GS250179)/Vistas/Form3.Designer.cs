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
            pictureBox1 = new PictureBox();
            btnAtras = new PictureBox();
            btnAR = new PictureBox();
            btnEditarR = new PictureBox();
            btnEliminarRuta = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnAtras).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnAR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnEditarR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnEliminarRuta).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-8, -6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1128, 651);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // btnAtras
            // 
            btnAtras.Cursor = Cursors.Hand;
            btnAtras.Image = (Image)resources.GetObject("btnAtras.Image");
            btnAtras.Location = new Point(23, 582);
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
            // Form3
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1112, 639);
            Controls.Add(btnEliminarRuta);
            Controls.Add(btnEditarR);
            Controls.Add(btnAR);
            Controls.Add(btnAtras);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form3";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Rutas";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnAtras).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnAR).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnEditarR).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnEliminarRuta).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox btnAtras;
        private PictureBox btnAR;
        private PictureBox btnEditarR;
        private PictureBox btnEliminarRuta;
    }
}