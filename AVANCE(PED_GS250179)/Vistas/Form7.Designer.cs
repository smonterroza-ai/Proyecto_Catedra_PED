namespace AVANCE_PED_GS250179_
{
    partial class Form7
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form7));
            btnAtras = new PictureBox();
            Confi_U = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)btnAtras).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Confi_U).BeginInit();
            SuspendLayout();
            // 
            // btnAtras
            // 
            btnAtras.Cursor = Cursors.Hand;
            btnAtras.Image = (Image)resources.GetObject("btnAtras.Image");
            btnAtras.Location = new Point(12, 430);
            btnAtras.Margin = new Padding(3, 2, 3, 2);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(122, 34);
            btnAtras.SizeMode = PictureBoxSizeMode.StretchImage;
            btnAtras.TabIndex = 8;
            btnAtras.TabStop = false;
            btnAtras.Click += btnAtras_Click;
            // 
            // Confi_U
            // 
            Confi_U.Cursor = Cursors.Hand;
            Confi_U.Image = (Image)resources.GetObject("Confi_U.Image");
            Confi_U.Location = new Point(387, 395);
            Confi_U.Margin = new Padding(3, 2, 3, 2);
            Confi_U.Name = "Confi_U";
            Confi_U.Size = new Size(110, 31);
            Confi_U.SizeMode = PictureBoxSizeMode.StretchImage;
            Confi_U.TabIndex = 15;
            Confi_U.TabStop = false;
            Confi_U.Click += Confi_U_Click;
            // 
            // Form7
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondo2_Agregar_;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(873, 475);
            Controls.Add(Confi_U);
            Controls.Add(btnAtras);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form7";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form7";
            Load += Form7_Load;
            ((System.ComponentModel.ISupportInitialize)btnAtras).EndInit();
            ((System.ComponentModel.ISupportInitialize)Confi_U).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox btnAtras;
        private PictureBox Confi_U;
    }
}