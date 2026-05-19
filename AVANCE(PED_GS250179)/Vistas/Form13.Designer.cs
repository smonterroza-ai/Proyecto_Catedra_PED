namespace AVANCE_PED_GS250179_.Vistas
{
    partial class Form13
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
            btnRegresar = new Button();
            txtBuscar = new TextBox();
            dgvClientes = new DataGridView();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            SuspendLayout();
            // 
            // btnRegresar
            // 
            btnRegresar.BackColor = Color.DarkGray;
            btnRegresar.FlatAppearance.BorderSize = 0;
            btnRegresar.FlatStyle = FlatStyle.Flat;
            btnRegresar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRegresar.ForeColor = SystemColors.ControlLightLight;
            btnRegresar.Image = Properties.Resources.esquema_de_boton_circular_de_flecha_hacia_atras_izquierda;
            btnRegresar.Location = new Point(324, 425);
            btnRegresar.Name = "btnRegresar";
            btnRegresar.Size = new Size(111, 38);
            btnRegresar.TabIndex = 34;
            btnRegresar.Text = "  Regresar";
            btnRegresar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRegresar.UseVisualStyleBackColor = false;
            // 
            // txtBuscar
            // 
            txtBuscar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtBuscar.ForeColor = SystemColors.WindowText;
            txtBuscar.Location = new Point(656, 27);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por nombre";
            txtBuscar.Size = new Size(214, 25);
            txtBuscar.TabIndex = 33;
            // 
            // dgvClientes
            // 
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Location = new Point(324, 69);
            dgvClientes.Margin = new Padding(3, 2, 3, 2);
            dgvClientes.Name = "dgvClientes";
            dgvClientes.RowHeadersWidth = 51;
            dgvClientes.Size = new Size(547, 337);
            dgvClientes.TabIndex = 32;
            // 
            // label6
            // 
            label6.BackColor = Color.FromArgb(0, 0, 64);
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.ImageAlign = ContentAlignment.BottomCenter;
            label6.Location = new Point(324, 22);
            label6.Name = "label6";
            label6.Size = new Size(295, 28);
            label6.TabIndex = 31;
            label6.Text = "MONITOR DE CLIENTES";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form13
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondo_transacciones;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(881, 470);
            Controls.Add(btnRegresar);
            Controls.Add(txtBuscar);
            Controls.Add(dgvClientes);
            Controls.Add(label6);
            DoubleBuffered = true;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form13";
            Text = "Form13";
            Load += Form13_Load;
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRegresar;
        private TextBox txtBuscar;
        private DataGridView dgvClientes;
        private Label label6;
    }
}