namespace AVANCE_PED_GS250179_
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pictureBox1 = new PictureBox();
            txtuser = new TextBox();
            txtpass = new TextBox();
            btnIS = new Button();
            btnSalir = new Button();
            ptojo = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ptojo).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-4, -1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1128, 651);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // txtuser
            // 
            txtuser.Cursor = Cursors.IBeam;
            txtuser.Location = new Point(588, 278);
            txtuser.Name = "txtuser";
            txtuser.Size = new Size(336, 27);
            txtuser.TabIndex = 1;
            // 
            // txtpass
            // 
            txtpass.Cursor = Cursors.IBeam;
            txtpass.Location = new Point(588, 372);
            txtpass.Name = "txtpass";
            txtpass.PasswordChar = '*';
            txtpass.Size = new Size(336, 27);
            txtpass.TabIndex = 2;
            // 
            // btnIS
            // 
            btnIS.BackColor = SystemColors.ActiveCaptionText;
            btnIS.Cursor = Cursors.Hand;
            btnIS.FlatStyle = FlatStyle.Flat;
            btnIS.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnIS.ForeColor = Color.White;
            btnIS.Location = new Point(682, 432);
            btnIS.Name = "btnIS";
            btnIS.Size = new Size(150, 36);
            btnIS.TabIndex = 3;
            btnIS.Text = "INICIAR SESIÓN";
            btnIS.UseVisualStyleBackColor = false;
            btnIS.Click += btnIS_Click;
            // 
            // btnSalir
            // 
            btnSalir.BackColor = SystemColors.ActiveCaptionText;
            btnSalir.Cursor = Cursors.Hand;
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSalir.ForeColor = Color.White;
            btnSalir.Location = new Point(988, 575);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(89, 36);
            btnSalir.TabIndex = 4;
            btnSalir.Text = "SALIR";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // ptojo
            // 
            ptojo.Image = Properties.Resources.icons8_eye_24;
            ptojo.Location = new Point(893, 372);
            ptojo.Name = "ptojo";
            ptojo.Size = new Size(31, 27);
            ptojo.TabIndex = 5;
            ptojo.TabStop = false;
            ptojo.Click += ptojo_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1112, 639);
            Controls.Add(ptojo);
            Controls.Add(btnSalir);
            Controls.Add(btnIS);
            Controls.Add(txtpass);
            Controls.Add(txtuser);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "reise";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)ptojo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private TextBox txtuser;
        private TextBox txtpass;
        private Button btnIS;
        private Button btnSalir;
        private PictureBox ptojo;
    }
}
