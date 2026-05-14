namespace AVANCE_PED_GS250179_
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            pictureBox1 = new PictureBox();
            btnR = new PictureBox();
            btnT = new PictureBox();
            btnU = new PictureBox();
            btnSalir = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnT).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnU).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnSalir).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-6, -1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1128, 639);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // btnR
            // 
            btnR.Cursor = Cursors.Hand;
            btnR.Image = (Image)resources.GetObject("btnR.Image");
            btnR.Location = new Point(50, 158);
            btnR.Name = "btnR";
            btnR.Size = new Size(254, 250);
            btnR.SizeMode = PictureBoxSizeMode.StretchImage;
            btnR.TabIndex = 2;
            btnR.TabStop = false;
            btnR.Click += btnR_Click;
            // 
            // btnT
            // 
            btnT.Cursor = Cursors.Hand;
            btnT.Image = (Image)resources.GetObject("btnT.Image");
            btnT.Location = new Point(431, 158);
            btnT.Name = "btnT";
            btnT.Size = new Size(254, 250);
            btnT.SizeMode = PictureBoxSizeMode.StretchImage;
            btnT.TabIndex = 3;
            btnT.TabStop = false;
            btnT.Click += btnT_Click;
            // 
            // btnU
            // 
            btnU.Cursor = Cursors.Hand;
            btnU.Image = (Image)resources.GetObject("btnU.Image");
            btnU.Location = new Point(805, 158);
            btnU.Name = "btnU";
            btnU.Size = new Size(254, 250);
            btnU.SizeMode = PictureBoxSizeMode.StretchImage;
            btnU.TabIndex = 4;
            btnU.TabStop = false;
            btnU.Click += btnU_Click;
            // 
            // btnSalir
            // 
            btnSalir.Cursor = Cursors.Hand;
            btnSalir.Image = (Image)resources.GetObject("btnSalir.Image");
            btnSalir.Location = new Point(1057, 51);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(43, 42);
            btnSalir.SizeMode = PictureBoxSizeMode.StretchImage;
            btnSalir.TabIndex = 5;
            btnSalir.TabStop = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1112, 639);
            Controls.Add(btnSalir);
            Controls.Add(btnU);
            Controls.Add(btnT);
            Controls.Add(btnR);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menu Principal";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnR).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnT).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnU).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnSalir).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox btnR;
        private PictureBox btnT;
        private PictureBox btnU;
        private PictureBox btnSalir;
    }
}