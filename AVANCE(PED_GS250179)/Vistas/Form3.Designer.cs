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
            label6 = new Label();
            dgvRutas = new DataGridView();
            txtBuscar = new TextBox();
            btnRegresar = new Button();
            btnEliminar = new Button();
            btnEditar = new Button();
            btnAgregar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvRutas).BeginInit();
            SuspendLayout();
            // 
            // label6
            // 
            label6.BackColor = Color.MidnightBlue;
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.ImageAlign = ContentAlignment.BottomCenter;
            label6.Location = new Point(77, 99);
            label6.Name = "label6";
            label6.Size = new Size(371, 29);
            label6.TabIndex = 12;
            label6.Text = "GESTION DE RUTAS";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvRutas
            // 
            dgvRutas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRutas.Location = new Point(77, 144);
            dgvRutas.Margin = new Padding(3, 2, 3, 2);
            dgvRutas.Name = "dgvRutas";
            dgvRutas.RowHeadersWidth = 51;
            dgvRutas.Size = new Size(782, 238);
            dgvRutas.TabIndex = 13;
            // 
            // txtBuscar
            // 
            txtBuscar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtBuscar.ForeColor = SystemColors.WindowText;
            txtBuscar.Location = new Point(612, 102);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por ruta";
            txtBuscar.Size = new Size(247, 25);
            txtBuscar.TabIndex = 14;
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
            btnRegresar.Location = new Point(33, 429);
            btnRegresar.Name = "btnRegresar";
            btnRegresar.Size = new Size(111, 38);
            btnRegresar.TabIndex = 29;
            btnRegresar.Text = "  Regresar";
            btnRegresar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRegresar.UseVisualStyleBackColor = false;
            btnRegresar.Click += btnRegresar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.IndianRed;
            btnEliminar.BackgroundImageLayout = ImageLayout.None;
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEliminar.ForeColor = SystemColors.ControlLightLight;
            btnEliminar.Image = Properties.Resources.eliminar;
            btnEliminar.Location = new Point(798, 17);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(110, 38);
            btnEliminar.TabIndex = 28;
            btnEliminar.Text = "  Eliminar";
            btnEliminar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnEditar
            // 
            btnEditar.BackColor = Color.Orange;
            btnEditar.BackgroundImageLayout = ImageLayout.None;
            btnEditar.FlatAppearance.BorderSize = 0;
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEditar.ForeColor = SystemColors.ControlLightLight;
            btnEditar.Image = Properties.Resources.boligrafo;
            btnEditar.Location = new Point(673, 17);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(110, 38);
            btnEditar.TabIndex = 27;
            btnEditar.Text = "  Editar";
            btnEditar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEditar.UseVisualStyleBackColor = false;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.BackColor = Color.SteelBlue;
            btnAgregar.FlatAppearance.BorderSize = 0;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAgregar.ForeColor = SystemColors.ControlLightLight;
            btnAgregar.Image = Properties.Resources.boton_agregar;
            btnAgregar.Location = new Point(545, 17);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(110, 38);
            btnAgregar.TabIndex = 26;
            btnAgregar.Text = "  Agregar";
            btnAgregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondo1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(941, 479);
            Controls.Add(btnRegresar);
            Controls.Add(btnEliminar);
            Controls.Add(btnEditar);
            Controls.Add(btnAgregar);
            Controls.Add(txtBuscar);
            Controls.Add(dgvRutas);
            Controls.Add(label6);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form3";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Rutas";
            Load += Form3_Load;
            Shown += Form3_Shown;
            ((System.ComponentModel.ISupportInitialize)dgvRutas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label6;
        private DataGridView dgvRutas;
        private TextBox txtBuscar;
        private Button btnRegresar;
        private Button btnEliminar;
        private Button btnEditar;
        private Button btnAgregar;
    }
}