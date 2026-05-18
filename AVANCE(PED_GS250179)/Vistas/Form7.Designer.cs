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
            txtPlaca = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtMarca = new TextBox();
            label4 = new Label();
            txtModelo = new TextBox();
            label5 = new Label();
            label6 = new Label();
            cmbEstado = new ComboBox();
            cmbTipoVehiculo = new ComboBox();
            label7 = new Label();
            cmbConductor = new ComboBox();
            cmbRuta = new ComboBox();
            btnRegresar = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // txtPlaca
            // 
            txtPlaca.Location = new Point(305, 186);
            txtPlaca.Name = "txtPlaca";
            txtPlaca.Size = new Size(84, 23);
            txtPlaca.TabIndex = 16;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(305, 156);
            label1.Name = "label1";
            label1.Size = new Size(40, 17);
            label1.TabIndex = 17;
            label1.Text = "Placa";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(305, 221);
            label2.Name = "label2";
            label2.Size = new Size(72, 17);
            label2.TabIndex = 19;
            label2.Text = "Conductor";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(305, 291);
            label3.Name = "label3";
            label3.Size = new Size(45, 17);
            label3.TabIndex = 21;
            label3.Text = "Marca";
            // 
            // txtMarca
            // 
            txtMarca.Location = new Point(305, 321);
            txtMarca.Name = "txtMarca";
            txtMarca.Size = new Size(134, 23);
            txtMarca.TabIndex = 20;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.ControlLightLight;
            label4.Location = new Point(445, 291);
            label4.Name = "label4";
            label4.Size = new Size(55, 17);
            label4.TabIndex = 23;
            label4.Text = "Modelo";
            // 
            // txtModelo
            // 
            txtModelo.Location = new Point(445, 321);
            txtModelo.Name = "txtModelo";
            txtModelo.Size = new Size(132, 23);
            txtModelo.TabIndex = 22;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = SystemColors.ControlLightLight;
            label5.Location = new Point(485, 156);
            label5.Name = "label5";
            label5.Size = new Size(49, 17);
            label5.TabIndex = 25;
            label5.Text = "Estado";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = SystemColors.ControlLightLight;
            label6.Location = new Point(395, 156);
            label6.Name = "label6";
            label6.Size = new Size(36, 17);
            label6.TabIndex = 27;
            label6.Text = "Ruta";
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(485, 186);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(92, 23);
            cmbEstado.TabIndex = 28;
            // 
            // cmbTipoVehiculo
            // 
            cmbTipoVehiculo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoVehiculo.FormattingEnabled = true;
            cmbTipoVehiculo.Location = new Point(472, 251);
            cmbTipoVehiculo.Name = "cmbTipoVehiculo";
            cmbTipoVehiculo.Size = new Size(105, 23);
            cmbTipoVehiculo.TabIndex = 30;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = SystemColors.ControlLightLight;
            label7.Location = new Point(472, 221);
            label7.Name = "label7";
            label7.Size = new Size(111, 17);
            label7.TabIndex = 29;
            label7.Text = "Tipo de vehiculo";
            // 
            // cmbConductor
            // 
            cmbConductor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbConductor.FormattingEnabled = true;
            cmbConductor.Location = new Point(305, 251);
            cmbConductor.Name = "cmbConductor";
            cmbConductor.Size = new Size(161, 23);
            cmbConductor.TabIndex = 31;
            // 
            // cmbRuta
            // 
            cmbRuta.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRuta.FormattingEnabled = true;
            cmbRuta.Location = new Point(395, 186);
            cmbRuta.Name = "cmbRuta";
            cmbRuta.Size = new Size(84, 23);
            cmbRuta.TabIndex = 32;
            // 
            // btnRegresar
            // 
            btnRegresar.BackColor = Color.DarkGray;
            btnRegresar.FlatAppearance.BorderSize = 0;
            btnRegresar.FlatStyle = FlatStyle.Flat;
            btnRegresar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRegresar.ForeColor = SystemColors.ControlLightLight;
            btnRegresar.Image = Properties.Resources.esquema_de_boton_circular_de_flecha_hacia_atras_izquierda;
            btnRegresar.Location = new Point(12, 425);
            btnRegresar.Name = "btnRegresar";
            btnRegresar.Size = new Size(111, 38);
            btnRegresar.TabIndex = 47;
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
            button1.Location = new Point(395, 389);
            button1.Name = "button1";
            button1.Size = new Size(101, 38);
            button1.TabIndex = 46;
            button1.Text = "  Listo";
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // Form7
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondo2_Agregar_;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(873, 475);
            Controls.Add(btnRegresar);
            Controls.Add(button1);
            Controls.Add(cmbRuta);
            Controls.Add(cmbConductor);
            Controls.Add(cmbTipoVehiculo);
            Controls.Add(label7);
            Controls.Add(cmbEstado);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(txtModelo);
            Controls.Add(label3);
            Controls.Add(txtMarca);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtPlaca);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form7";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form7";
            Load += Form7_Load;
            Shown += Form7_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtPlaca;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtMarca;
        private Label label4;
        private TextBox txtModelo;
        private Label label5;
        private Label label6;
        private ComboBox cmbEstado;
        private ComboBox cmbTipoVehiculo;
        private Label label7;
        private ComboBox cmbConductor;
        private ComboBox cmbRuta;
        private Button btnRegresar;
        private Button button1;
    }
}