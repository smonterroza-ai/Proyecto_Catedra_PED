namespace AVANCE_PED_GS250179_
{
    partial class Form6
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
            dgvUnidades = new DataGridView();
            label1 = new Label();
            txtBuscar = new TextBox();
            btnRegresar = new Button();
            btnEliminar = new Button();
            btnEditar = new Button();
            btnAgregar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvUnidades).BeginInit();
            SuspendLayout();
            // 
            // dgvUnidades
            // 
            dgvUnidades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUnidades.Location = new Point(78, 146);
            dgvUnidades.Name = "dgvUnidades";
            dgvUnidades.Size = new Size(782, 238);
            dgvUnidades.TabIndex = 11;
            // 
            // label1
            // 
            label1.BackColor = Color.MidnightBlue;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(78, 94);
            label1.Name = "label1";
            label1.Size = new Size(371, 29);
            label1.TabIndex = 12;
            label1.Text = "GESTIÓN DE UNIDADES";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtBuscar
            // 
            txtBuscar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtBuscar.ForeColor = SystemColors.WindowText;
            txtBuscar.Location = new Point(613, 98);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por placa";
            txtBuscar.Size = new Size(247, 25);
            txtBuscar.TabIndex = 13;
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
            btnRegresar.Location = new Point(31, 427);
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
            btnEliminar.Location = new Point(796, 20);
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
            btnEditar.Location = new Point(671, 20);
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
            btnAgregar.Location = new Point(543, 20);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(110, 38);
            btnAgregar.TabIndex = 26;
            btnAgregar.Text = "  Agregar";
            btnAgregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // Form6
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fondo1;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(936, 479);
            Controls.Add(btnRegresar);
            Controls.Add(btnEliminar);
            Controls.Add(btnEditar);
            Controls.Add(btnAgregar);
            Controls.Add(txtBuscar);
            Controls.Add(label1);
            Controls.Add(dgvUnidades);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form6";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form6";
            Load += Form6_Load;
            Shown += Form6_Shown;
            ((System.ComponentModel.ISupportInitialize)dgvUnidades).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pictureBox1;
        private DataGridView dgvUnidades;
        private Label label1;
        private TextBox txtBuscar;
        private Button btnRegresar;
        private Button btnEliminar;
        private Button btnEditar;
        private Button btnAgregar;
    }
}