namespace AVANCE_PED_GS250179_
{
    partial class Form5
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
            label6 = new Label();
            dgvTransaccion = new DataGridView();
            txtBuscar = new TextBox();
            btnRegresar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvTransaccion).BeginInit();
            SuspendLayout();
            // 
            // label6
            // 
            label6.BackColor = Color.FromArgb(0, 0, 64);
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.ImageAlign = ContentAlignment.BottomCenter;
            label6.Location = new Point(384, 43);
            label6.Name = "label6";
            label6.Size = new Size(295, 28);
            label6.TabIndex = 13;
            label6.Text = "MONITOR DE TRANSACCIONES";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dgvTransaccion
            // 
            dgvTransaccion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTransaccion.Location = new Point(384, 90);
            dgvTransaccion.Margin = new Padding(3, 2, 3, 2);
            dgvTransaccion.Name = "dgvTransaccion";
            dgvTransaccion.RowHeadersWidth = 51;
            dgvTransaccion.Size = new Size(547, 337);
            dgvTransaccion.TabIndex = 14;
            // 
            // txtBuscar
            // 
            txtBuscar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtBuscar.ForeColor = SystemColors.WindowText;
            txtBuscar.Location = new Point(717, 48);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por transacción";
            txtBuscar.Size = new Size(214, 25);
            txtBuscar.TabIndex = 15;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // btnRegresar
            // 
            btnRegresar.BackColor = Color.DarkGray;
            btnRegresar.FlatAppearance.BorderSize = 0;
            btnRegresar.FlatStyle = FlatStyle.Flat;
            btnRegresar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRegresar.ForeColor = SystemColors.ControlLightLight;
            btnRegresar.Image = Properties.Resources.esquema_de_boton_circular_de_flecha_hacia_atras_izquierda;
            btnRegresar.Location = new Point(384, 446);
            btnRegresar.Name = "btnRegresar";
            btnRegresar.Size = new Size(111, 38);
            btnRegresar.TabIndex = 30;
            btnRegresar.Text = "  Regresar";
            btnRegresar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRegresar.UseVisualStyleBackColor = false;
            btnRegresar.Click += btnRegresar_Click;
            // 
            // Form5
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondo_transacciones;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(973, 496);
            Controls.Add(btnRegresar);
            Controls.Add(txtBuscar);
            Controls.Add(dgvTransaccion);
            Controls.Add(label6);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form5";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form5";
            Load += Form5_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTransaccion).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label6;
        private DataGridView dgvTransaccion;
        private TextBox txtBuscar;
        private Button btnRegresar;
    }
}