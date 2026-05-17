namespace AVANCE_PED_GS250179_
{
    partial class Form4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            pictureBox1 = new PictureBox();
            btnAtras = new PictureBox();
            txtRuta = new TextBox();
            txtTari = new TextBox();
            txtRecorrido = new TextBox();
            btnAMapa = new PictureBox();
            Confi_R = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnAtras).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnAMapa).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Confi_R).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1, 1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1112, 639);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // btnAtras
            // 
            btnAtras.Cursor = Cursors.Hand;
            btnAtras.Image = (Image)resources.GetObject("btnAtras.Image");
            btnAtras.Location = new Point(21, 582);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(139, 45);
            btnAtras.SizeMode = PictureBoxSizeMode.StretchImage;
            btnAtras.TabIndex = 4;
            btnAtras.TabStop = false;
            btnAtras.Click += btnAtras_Click;
            // 
            // txtRuta
            // 
            txtRuta.BorderStyle = BorderStyle.None;
            txtRuta.Cursor = Cursors.IBeam;
            txtRuta.Font = new Font("Segoe UI", 12F);
            txtRuta.Location = new Point(390, 227);
            txtRuta.Name = "txtRuta";
            txtRuta.Size = new Size(56, 27);
            txtRuta.TabIndex = 5;
            // 
            // txtTari
            // 
            txtTari.BorderStyle = BorderStyle.None;
            txtTari.Cursor = Cursors.IBeam;
            txtTari.Font = new Font("Segoe UI", 12F);
            txtTari.Location = new Point(518, 227);
            txtTari.Name = "txtTari";
            txtTari.Size = new Size(56, 27);
            txtTari.TabIndex = 6;
            // 
            // txtRecorrido
            // 
            txtRecorrido.BorderStyle = BorderStyle.None;
            txtRecorrido.Cursor = Cursors.IBeam;
            txtRecorrido.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtRecorrido.Location = new Point(390, 310);
            txtRecorrido.Name = "txtRecorrido";
            txtRecorrido.Size = new Size(360, 27);
            txtRecorrido.TabIndex = 7;
            // 
            // btnAMapa
            // 
            btnAMapa.Cursor = Cursors.Hand;
            btnAMapa.Image = (Image)resources.GetObject("btnAMapa.Image");
            btnAMapa.Location = new Point(635, 243);
            btnAMapa.Name = "btnAMapa";
            btnAMapa.Size = new Size(126, 40);
            btnAMapa.SizeMode = PictureBoxSizeMode.StretchImage;
            btnAMapa.TabIndex = 8;
            btnAMapa.TabStop = false;
            btnAMapa.Click += btnAMapa_Click;
            // 
            // Confi_R
            // 
            Confi_R.Cursor = Cursors.Hand;
            Confi_R.Image = (Image)resources.GetObject("Confi_R.Image");
            Confi_R.Location = new Point(504, 454);
            Confi_R.Name = "Confi_R";
            Confi_R.Size = new Size(123, 39);
            Confi_R.SizeMode = PictureBoxSizeMode.StretchImage;
            Confi_R.TabIndex = 9;
            Confi_R.TabStop = false;
            Confi_R.Click += Confi_R_Click;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1112, 639);
            Controls.Add(Confi_R);
            Controls.Add(btnAMapa);
            Controls.Add(txtRecorrido);
            Controls.Add(txtTari);
            Controls.Add(txtRuta);
            Controls.Add(btnAtras);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form4";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form4";
            Load += Form4_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnAtras).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnAMapa).EndInit();
            ((System.ComponentModel.ISupportInitialize)Confi_R).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox btnAtras;
        private TextBox txtRuta;
        private TextBox txtTari;
        private TextBox txtRecorrido;
        private PictureBox btnAMapa;
        private PictureBox Confi_R;
    }
}