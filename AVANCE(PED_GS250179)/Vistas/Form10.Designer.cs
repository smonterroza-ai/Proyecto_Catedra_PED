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
            Pizarra = new Panel();
            mapaReise = new GMap.NET.WindowsForms.GMapControl();
            btnRegresar = new Button();
            btnGuardarRuta = new Button();
            Pizarra.SuspendLayout();
            SuspendLayout();
            // 
            // Pizarra
            // 
            Pizarra.BackColor = Color.White;
            Pizarra.Controls.Add(mapaReise);
            Pizarra.Location = new Point(3, 1);
            Pizarra.Name = "Pizarra";
            Pizarra.Size = new Size(801, 450);
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
            mapaReise.Size = new Size(798, 447);
            mapaReise.TabIndex = 0;
            mapaReise.Zoom = 0D;
            mapaReise.OnMarkerClick += mapaReise_OnMarkerClick;
            mapaReise.MouseDoubleClick += mapaReise_MouseDoubleClick;
            // 
            // btnRegresar
            // 
            btnRegresar.Location = new Point(825, 74);
            btnRegresar.Name = "btnRegresar";
            btnRegresar.Size = new Size(94, 29);
            btnRegresar.TabIndex = 1;
            btnRegresar.Text = "Regresar";
            btnRegresar.UseVisualStyleBackColor = true;
            btnRegresar.Click += btnProbar_Click;
            // 
            // btnGuardarRuta
            // 
            btnGuardarRuta.Location = new Point(807, 12);
            btnGuardarRuta.Name = "btnGuardarRuta";
            btnGuardarRuta.Size = new Size(131, 29);
            btnGuardarRuta.TabIndex = 2;
            btnGuardarRuta.Text = "Guardar ruta";
            btnGuardarRuta.UseVisualStyleBackColor = true;
            btnGuardarRuta.Click += btnIniciarGrabacion_Click;
            // 
            // Form10
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(940, 450);
            Controls.Add(btnGuardarRuta);
            Controls.Add(btnRegresar);
            Controls.Add(Pizarra);
            Name = "Form10";
            Text = "Form10";
            Load += Form10_Load;
            Pizarra.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel Pizarra;
        private Button btnRegresar;
        private Button btnGuardarRuta;
        private GMap.NET.WindowsForms.GMapControl mapaReise;
    }
}