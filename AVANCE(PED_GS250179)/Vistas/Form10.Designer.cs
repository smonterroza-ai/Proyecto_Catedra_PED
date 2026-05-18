namespace AVANCE_PED_GS250179_.Vistas
{
    partial class Form10
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
            components = new System.ComponentModel.Container();
            Pizarra = new Panel();
            mapaReise = new GMap.NET.WindowsForms.GMapControl();
            menuMarcador = new ContextMenuStrip(components);
            eliminarPuntoToolStripMenuItem = new ToolStripMenuItem();
            button2 = new Button();
            btnEliminar = new Button();
            btnEditar = new Button();
            btnAgregar = new Button();
            Pizarra.SuspendLayout();
            menuMarcador.SuspendLayout();
            SuspendLayout();
            // 
            // Pizarra
            // 
            Pizarra.BackColor = Color.White;
            Pizarra.Controls.Add(mapaReise);
            Pizarra.Location = new Point(3, 1);
            Pizarra.Margin = new Padding(3, 2, 3, 2);
            Pizarra.Name = "Pizarra";
            Pizarra.Size = new Size(701, 338);
            Pizarra.TabIndex = 0;
            Pizarra.Paint += Pizarra_Paint;
            Pizarra.MouseDown += Pizarra_MouseDown;
            Pizarra.MouseMove += Pizarra_MouseMove;
            Pizarra.MouseUp += Pizarra_MouseUp;
            // 
            // mapaReise
            // 
            mapaReise.Bearing = 0F;
            mapaReise.CanDragMap = true;
            mapaReise.EmptyTileColor = Color.Navy;
            mapaReise.GrayScaleMode = false;
            mapaReise.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            mapaReise.LevelsKeepInMemory = 5;
            mapaReise.Location = new Point(0, 0);
            mapaReise.Margin = new Padding(3, 2, 3, 2);
            mapaReise.MarkersEnabled = true;
            mapaReise.MaxZoom = 2;
            mapaReise.MinZoom = 2;
            mapaReise.MouseWheelZoomEnabled = true;
            mapaReise.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            mapaReise.Name = "mapaReise";
            mapaReise.NegativeMode = false;
            mapaReise.PolygonsEnabled = true;
            mapaReise.RetryLoadTile = 0;
            mapaReise.RoutesEnabled = true;
            mapaReise.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            mapaReise.SelectedAreaFillColor = Color.FromArgb(33, 65, 105, 225);
            mapaReise.ShowTileGridLines = false;
            mapaReise.Size = new Size(698, 335);
            mapaReise.TabIndex = 0;
            mapaReise.Zoom = 0D;
            mapaReise.OnMarkerClick += mapaReise_OnMarkerClick;
            mapaReise.MouseDoubleClick += mapaReise_MouseDoubleClick;
            // 
            // menuMarcador
            // 
            menuMarcador.ImageScalingSize = new Size(20, 20);
            menuMarcador.Items.AddRange(new ToolStripItem[] { eliminarPuntoToolStripMenuItem });
            menuMarcador.Name = "contextMenuStrip1";
            menuMarcador.Size = new Size(153, 26);
            // 
            // eliminarPuntoToolStripMenuItem
            // 
            eliminarPuntoToolStripMenuItem.Name = "eliminarPuntoToolStripMenuItem";
            eliminarPuntoToolStripMenuItem.Size = new Size(152, 22);
            eliminarPuntoToolStripMenuItem.Text = "Eliminar Punto";
            eliminarPuntoToolStripMenuItem.Click += eliminarPuntoToolStripMenuItem_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.DarkGray;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = SystemColors.ControlLightLight;
            button2.Image = Properties.Resources.esquema_de_boton_circular_de_flecha_hacia_atras_izquierda;
            button2.Location = new Point(734, 288);
            button2.Name = "button2";
            button2.Size = new Size(111, 38);
            button2.TabIndex = 26;
            button2.Text = "  Regresar";
            button2.TextImageRelation = TextImageRelation.ImageBeforeText;
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
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
            btnEliminar.Location = new Point(733, 100);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(111, 38);
            btnEliminar.TabIndex = 27;
            btnEliminar.Text = "  Eliminar";
            btnEliminar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEliminar.UseVisualStyleBackColor = false;
            // 
            // btnEditar
            // 
            btnEditar.BackColor = Color.Orange;
            btnEditar.BackgroundImageLayout = ImageLayout.None;
            btnEditar.FlatAppearance.BorderSize = 0;
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEditar.ForeColor = SystemColors.ControlLightLight;
            btnEditar.Image = Properties.Resources.limpiar;
            btnEditar.Location = new Point(734, 56);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(110, 38);
            btnEditar.TabIndex = 28;
            btnEditar.Text = "  Limpiar";
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
            btnAgregar.Location = new Point(734, 12);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(110, 38);
            btnAgregar.TabIndex = 29;
            btnAgregar.Text = "  Agregar";
            btnAgregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAgregar.UseVisualStyleBackColor = false;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // Form10
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(869, 338);
            Controls.Add(btnAgregar);
            Controls.Add(btnEditar);
            Controls.Add(btnEliminar);
            Controls.Add(button2);
            Controls.Add(Pizarra);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form10";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form10";
            Load += Form10_Load;
            Shown += Form10_Shown;
            Pizarra.ResumeLayout(false);
            menuMarcador.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel Pizarra;
        private GMap.NET.WindowsForms.GMapControl mapaReise;
        private ContextMenuStrip menuMarcador;
        private ToolStripMenuItem eliminarPuntoToolStripMenuItem;
        private Button button2;
        private Button btnEliminar;
        private Button btnEditar;
        private Button btnAgregar;
    }
}