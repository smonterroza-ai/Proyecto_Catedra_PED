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
            txtRuta = new TextBox();
            txtTari = new TextBox();
            txtRecorrido = new TextBox();
            label1 = new Label();
            label4 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnAgregar = new Button();
            btnRegresar = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // txtRuta
            // 
            txtRuta.BorderStyle = BorderStyle.None;
            txtRuta.Cursor = Cursors.IBeam;
            txtRuta.Font = new Font("Segoe UI", 12F);
            txtRuta.Location = new Point(341, 164);
            txtRuta.Margin = new Padding(3, 2, 3, 2);
            txtRuta.Name = "txtRuta";
            txtRuta.Size = new Size(49, 22);
            txtRuta.TabIndex = 5;
            // 
            // txtTari
            // 
            txtTari.BorderStyle = BorderStyle.None;
            txtTari.Cursor = Cursors.IBeam;
            txtTari.Font = new Font("Segoe UI", 12F);
            txtTari.Location = new Point(412, 164);
            txtTari.Margin = new Padding(3, 2, 3, 2);
            txtTari.Name = "txtTari";
            txtTari.Size = new Size(49, 22);
            txtTari.TabIndex = 6;
            txtTari.KeyPress += txtTari_KeyPress;
            // 
            // txtRecorrido
            // 
            txtRecorrido.BorderStyle = BorderStyle.None;
            txtRecorrido.Cursor = Cursors.IBeam;
            txtRecorrido.Enabled = false;
            txtRecorrido.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtRecorrido.Location = new Point(341, 242);
            txtRecorrido.Margin = new Padding(3, 2, 3, 2);
            txtRecorrido.Multiline = true;
            txtRecorrido.Name = "txtRecorrido";
            txtRecorrido.Size = new Size(315, 111);
            txtRecorrido.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(346, 132);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.ControlLightLight;
            label4.Location = new Point(341, 137);
            label4.Name = "label4";
            label4.Size = new Size(36, 17);
            label4.TabIndex = 18;
            label4.Text = "Ruta";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(412, 137);
            label2.Name = "label2";
            label2.Size = new Size(43, 17);
            label2.TabIndex = 19;
            label2.Text = "Tarifa";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(341, 213);
            label3.Name = "label3";
            label3.Size = new Size(67, 17);
            label3.TabIndex = 20;
            label3.Text = "Recorrido";
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.ForestGreen;
            btnAgregar.FlatAppearance.BorderSize = 0;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAgregar.ForeColor = SystemColors.ControlLightLight;
            btnAgregar.Image = Properties.Resources.boton_agregar;
            btnAgregar.Location = new Point(513, 192);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(143, 38);
            btnAgregar.TabIndex = 30;
            btnAgregar.Text = "  Agregar mapa";
            btnAgregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnRegresar
            // 
            btnRegresar.BackColor = Color.DarkGray;
            btnRegresar.FlatAppearance.BorderSize = 0;
            btnRegresar.FlatStyle = FlatStyle.Flat;
            btnRegresar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRegresar.ForeColor = SystemColors.ControlLightLight;
            btnRegresar.Image = Properties.Resources.esquema_de_boton_circular_de_flecha_hacia_atras_izquierda;
            btnRegresar.Location = new Point(12, 429);
            btnRegresar.Name = "btnRegresar";
            btnRegresar.Size = new Size(111, 38);
            btnRegresar.TabIndex = 49;
            btnRegresar.Text = "  Regresar";
            btnRegresar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRegresar.UseVisualStyleBackColor = false;
            btnRegresar.Click += btnRegresar_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.SteelBlue;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.ControlLightLight;
            button1.Image = Properties.Resources.marca_de_verificacion;
            button1.Location = new Point(435, 393);
            button1.Name = "button1";
            button1.Size = new Size(101, 38);
            button1.TabIndex = 48;
            button1.Text = "  Listo";
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondo2_Agregar_;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(973, 479);
            Controls.Add(btnRegresar);
            Controls.Add(button1);
            Controls.Add(btnAgregar);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(label1);
            Controls.Add(txtRecorrido);
            Controls.Add(txtTari);
            Controls.Add(txtRuta);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form4";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form4";
            Load += Form4_Load;
            Shown += Form4_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtRuta;
        private TextBox txtTari;
        private TextBox txtRecorrido;
        private Label label1;
        private Label label4;
        private Label label2;
        private Label label3;
        private Button btnAgregar;
        private Button btnRegresar;
        private Button button1;
    }
}