using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_
{
    public partial class MenuCliente : Form
    {
        // ==========================================================
        // ESTRUCTURA DE DATOS SIMULADA (PREPARACIÓN PARA BD)
        // ==========================================================
        public class RutaBus
        {
            public string Nombre { get; set; }
            public double Precio { get; set; }
        }

        // Controles de la Interfaz
        private Panel panelSuperior;
        private Panel pnlPerfil;
        private Label lblSaldo;
        private Label lblLogoReise;
        private Button btnRecargar;

        private Panel panelIzquierdo;
        private PictureBox picTodasLasRutas;
        private Label lblTodasLasRutas;

        private Panel panelDerecho;
        private Label lblPregunta;
        private Label lblBuscar;
        private Panel pnlBuscarInput;
        private TextBox txtBuscar;

        // El contenedor principal de las tarjetas
        private FlowLayoutPanel containerRutas;

        public MenuCliente()
        {
            // Configuración de la ventana principal
            this.Size = new Size(1024, 640);
            this.MinimumSize = new Size(1024, 640);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;

            ConstruirEsqueletoInterfaz();
            CargarRutasDesdeOrigen();
        }

        private void ConstruirEsqueletoInterfaz()
        {
            // ==========================================
            // 1. BANDA SUPERIOR (Navbar / Header)
            // ==========================================
            panelSuperior = new Panel { Height = 70, Dock = DockStyle.Top, BackColor = Color.White };
            this.Controls.Add(panelSuperior);

            // Contenedor de Perfil (Avatar)
            pnlPerfil = new Panel { Size = new Size(180, 40), Location = new Point(20, 15), BackColor = Color.White };
            pnlPerfil.Paint += (s, e) => DibujarBordeSuave(pnlPerfil, e.Graphics, Color.Black, 2, 8);

            Label lblIconUser = new Label { Text = "👤", Font = new Font("Segoe UI", 14), Location = new Point(10, 6), AutoSize = true };
            pnlPerfil.Controls.Add(lblIconUser);
            panelSuperior.Controls.Add(pnlPerfil);

            // Muestra de Saldo
            lblSaldo = new Label
            {
                Text = "SALDO: ----",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(220, 25),
                AutoSize = true
            };
            panelSuperior.Controls.Add(lblSaldo);

            // Logo REISE (Estático a la derecha)
            lblLogoReise = new Label
            {
                Text = "reise",
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = true
            };
            panelSuperior.Controls.Add(lblLogoReise);

            // Botón RECARGAR
            btnRecargar = new Button
            {
                Text = "RECARGAR",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 180, 100),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(110, 32),
                Cursor = Cursors.Hand
            };
            btnRecargar.FlatAppearance.BorderSize = 0;
            btnRecargar.Paint += (s, e) => RecortarBordesControl(btnRecargar, 8);
            panelSuperior.Controls.Add(btnRecargar);

            // ==========================================
            // 2. SECCIÓN IZQUIERDA (Imagen de Fondo / Banner)
            // ==========================================
            panelIzquierdo = new Panel { Width = 380, Dock = DockStyle.Left, BackColor = Color.White };
            this.Controls.Add(panelIzquierdo);

            picTodasLasRutas = new PictureBox
            {
                Size = new Size(340, 510),
                Location = new Point(20, 10),
                BackColor = Color.LightGray,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            panelIzquierdo.Controls.Add(picTodasLasRutas);

            lblTodasLasRutas = new Label
            {
                Text = "TODAS LAS RUTAS",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                BackColor = Color.FromArgb(220, 255, 255, 255),
                ForeColor = Color.Black,
                Size = new Size(340, 60),
                TextAlign = ContentAlignment.MiddleCenter
            };
            picTodasLasRutas.Controls.Add(lblTodasLasRutas);
            lblTodasLasRutas.Location = new Point(0, 420);

            // ==========================================
            // 3. SECCIÓN DERECHA (Buscador y Área de Tarjetas)
            // ==========================================
            panelDerecho = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            this.Controls.Add(panelDerecho);

            lblPregunta = new Label
            {
                Text = "¿DÓNDE DESEAS IR HOY?",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 10),
                AutoSize = true
            };
            panelDerecho.Controls.Add(lblPregunta);

            lblBuscar = new Label { Text = "BUSCAR", Font = new Font("Segoe UI", 10, FontStyle.Regular), Location = new Point(20, 57), AutoSize = true };
            panelDerecho.Controls.Add(lblBuscar);

            // Input Buscar estilizado
            pnlBuscarInput = new Panel { Size = new Size(420, 32), Location = new Point(90, 52), BackColor = Color.White };
            pnlBuscarInput.Paint += (s, e) => DibujarBordeSuave(pnlBuscarInput, e.Graphics, Color.Gray, 1, 8);

            txtBuscar = new TextBox
            {
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.None,
                Size = new Size(400, 20),
                Location = new Point(10, 6)
            };
            pnlBuscarInput.Controls.Add(txtBuscar);
            panelDerecho.Controls.Add(pnlBuscarInput);

            // ==========================================================
            // 4. CONTENEDOR DE TARJETAS (Scroll vertical)
            // ==========================================================
            containerRutas = new FlowLayoutPanel
            {
                Location = new Point(20, 100),
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                BackColor = Color.White
            };
            panelDerecho.Controls.Add(containerRutas);

            // Manejo de posiciones dinámicas
            this.Resize += (s, e) => {
                if (panelSuperior != null && panelDerecho != null)
                {
                    lblLogoReise.Location = new Point(panelSuperior.Width - 140, 10);
                    btnRecargar.Location = new Point(panelSuperior.Width - 270, 18);

                    containerRutas.Width = panelDerecho.Width - 40;
                    containerRutas.Height = panelDerecho.Height - 120;
                }
            };

            this.OnResize(EventArgs.Empty);
        }

        private void CargarRutasDesdeOrigen()
        {
            containerRutas.Controls.Clear();

            // Datos simulados (Mock Data para el esqueleto)
            List<RutaBus> listaRutasDefinidas = new List<RutaBus>()
            {
                new RutaBus { Nombre = "RUTA i", Precio = 0.20 },
                new RutaBus { Nombre = "RUTA 41-A", Precio = 0.20 },
                new RutaBus { Nombre = "RUTA 13", Precio = 0.20 }
            };

            foreach (var ruta in listaRutasDefinidas)
            {
                GenerarTarjetaComponente(ruta.Nombre, $"${ruta.Precio:F2}");
            }
        }

        private void GenerarTarjetaComponente(string nombreRuta, string precio)
        {
            Panel card = new Panel
            {
                Size = new Size(520, 100),
                MinimumSize = new Size(520, 100),
                MaximumSize = new Size(520, 100),
                BackColor = Color.FromArgb(74, 79, 84),
                Margin = new Padding(0, 0, 0, 15)
            };
            card.Paint += (s, e) => RecortarBordesControl(card, 15);

            // Icono de Autobús
            Label lblIconBus = new Label
            {
                Text = "🚌",
                Font = new Font("Segoe UI", 34),
                ForeColor = Color.White,
                Location = new Point(20, 16),
                AutoSize = true
            };
            card.Controls.Add(lblIconBus);

            // Nombre de la Ruta
            Label lblNombre = new Label
            {
                Text = nombreRuta,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(110, 34),
                AutoSize = true
            };
            card.Controls.Add(lblNombre);

            // Precio
            Label lblPrecio = new Label
            {
                Text = precio,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(435, 20),
                AutoSize = true
            };
            card.Controls.Add(lblPrecio);

            // Botón Comprar
            Button btnComprar = new Button
            {
                Text = "COMPRAR",
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 180, 100),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(95, 28),
                Location = new Point(410, 55),
                Cursor = Cursors.Hand
            };
            btnComprar.FlatAppearance.BorderSize = 0;
            btnComprar.Paint += (s, e) => RecortarBordesControl(btnComprar, 6);
            card.Controls.Add(btnComprar);

            containerRutas.Controls.Add(card);
        }

        // ==========================================================
        // MÉTODOS AUXILIARES GRÁFICOS (GDI+)
        // ==========================================================
        private void RecortarBordesControl(Control ctrl, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            Rectangle rect = new Rectangle(0, 0, ctrl.Width, ctrl.Height);
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            ctrl.Region = new Region(path);
        }

        private void DibujarBordeSuave(Control ctrl, Graphics g, Color col, int thickness, int radius)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            Rectangle rect = new Rectangle(0, 0, ctrl.Width - 1, ctrl.Height - 1);
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            using (Pen p = new Pen(col, thickness))
            {
                g.DrawPath(p, path);
            }
        }
    }
}