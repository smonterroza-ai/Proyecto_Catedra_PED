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
            btnAU = new PictureBox();
            btnAtras = new PictureBox();
            txtPlaca = new TextBox();
            txtRutaB = new TextBox();
            txtConductor = new TextBox();
            txtMarca = new TextBox();
            txtModelo = new TextBox();
            txtEstado = new TextBox();
            Confi_U = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)btnAU).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnAtras).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Confi_U).BeginInit();
            SuspendLayout();
            // 
            // btnAU
            // 
            btnAU.Image = (Image)resources.GetObject("btnAU.Image");
            btnAU.Location = new Point(0, 0);
            btnAU.Name = "btnAU";
            btnAU.Size = new Size(1112, 639);
            btnAU.SizeMode = PictureBoxSizeMode.StretchImage;
            btnAU.TabIndex = 6;
            btnAU.TabStop = false;
            // 
            // btnAtras
            // 
            btnAtras.Cursor = Cursors.Hand;
            btnAtras.Image = (Image)resources.GetObject("btnAtras.Image");
            btnAtras.Location = new Point(12, 582);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(139, 45);
            btnAtras.SizeMode = PictureBoxSizeMode.StretchImage;
            btnAtras.TabIndex = 8;
            btnAtras.TabStop = false;
            btnAtras.Click += btnAtras_Click;
            // 
            // txtPlaca
            // 
            txtPlaca.BorderStyle = BorderStyle.None;
            txtPlaca.Cursor = Cursors.IBeam;
            txtPlaca.Font = new Font("Segoe UI", 12F);
            txtPlaca.Location = new Point(340, 248);
            txtPlaca.Name = "txtPlaca";
            txtPlaca.Size = new Size(56, 27);
            txtPlaca.TabIndex = 9;
            // 
            // txtRutaB
            // 
            txtRutaB.BorderStyle = BorderStyle.None;
            txtRutaB.Cursor = Cursors.IBeam;
            txtRutaB.Font = new Font("Segoe UI", 12F);
            txtRutaB.Location = new Point(436, 248);
            txtRutaB.Name = "txtRutaB";
            txtRutaB.Size = new Size(56, 27);
            txtRutaB.TabIndex = 10;
            // 
            // txtConductor
            // 
            txtConductor.BorderStyle = BorderStyle.None;
            txtConductor.Cursor = Cursors.IBeam;
            txtConductor.Font = new Font("Segoe UI", 12F);
            txtConductor.Location = new Point(340, 323);
            txtConductor.Name = "txtConductor";
            txtConductor.Size = new Size(152, 27);
            txtConductor.TabIndex = 11;
            // 
            // txtMarca
            // 
            txtMarca.BorderStyle = BorderStyle.None;
            txtMarca.Cursor = Cursors.IBeam;
            txtMarca.Font = new Font("Segoe UI", 12F);
            txtMarca.Location = new Point(340, 403);
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(152, 27);
            txtMarca.TabIndex = 12;
            // 
            // txtModelo
            // 
            txtModelo.BorderStyle = BorderStyle.None;
            txtModelo.Cursor = Cursors.IBeam;
            txtModelo.Font = new Font("Segoe UI", 12F);
            txtModelo.Location = new Point(548, 403);
            txtModelo.Name = "txtModelo";
            txtModelo.Size = new Size(152, 27);
            txtModelo.TabIndex = 13;
            // 
            // txtEstado
            // 
            txtEstado.BorderStyle = BorderStyle.None;
            txtEstado.Cursor = Cursors.IBeam;
            txtEstado.Font = new Font("Segoe UI", 12F);
            txtEstado.Location = new Point(636, 323);
            txtEstado.Name = "txtEstado";
            txtEstado.Size = new Size(64, 27);
            txtEstado.TabIndex = 14;
            // 
            // Confi_U
            // 
            Confi_U.Cursor = Cursors.Hand;
            Confi_U.Image = (Image)resources.GetObject("Confi_U.Image");
            Confi_U.Location = new Point(471, 460);
            Confi_U.Name = "Confi_U";
            Confi_U.Size = new Size(126, 41);
            Confi_U.SizeMode = PictureBoxSizeMode.StretchImage;
            Confi_U.TabIndex = 15;
            Confi_U.TabStop = false;
            Confi_U.Click += Confi_U_Click;
            // 
            // Form7
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1112, 639);
            Controls.Add(Confi_U);
            Controls.Add(txtEstado);
            Controls.Add(txtModelo);
            Controls.Add(txtMarca);
            Controls.Add(txtConductor);
            Controls.Add(txtRutaB);
            Controls.Add(txtPlaca);
            Controls.Add(btnAtras);
            Controls.Add(btnAU);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form7";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form7";
            Load += Form7_Load;
            ((System.ComponentModel.ISupportInitialize)btnAU).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnAtras).EndInit();
            ((System.ComponentModel.ISupportInitialize)Confi_U).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox btnAU;
        private PictureBox btnAtras;
        private TextBox txtPlaca;
        private TextBox txtRutaB;
        private TextBox txtConductor;
        private TextBox txtMarca;
        private TextBox txtModelo;
        private TextBox txtEstado;
        private PictureBox Confi_U;
    }
}