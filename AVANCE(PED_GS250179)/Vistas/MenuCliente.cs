using AVANCE_PED_GS250179_;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_
{
    
    public partial class MenuCliente : Form
    {
        // CADENA DE CONEXIÓN GLOBAL
        private string connectionString = "Server=TU_SERVIDOR;Database=ReiseDB;Trusted_Connection=True;";

        // Controles principales de la Interfaz
        private Panel panelSuperior;
        private Panel pnlPerfil;
        private Label lblSaldo;
        private Label lblLogoReise;
        private Button btnRecargar;
        private Button btnSalirLogin;

        private Panel panelIzquierdo;
        private PictureBox picTodasLasRutas;
        private Label lblTodasLasRutas;

        private Panel panelDerecho;

        public MenuCliente()
        {
            this.Size = new Size(1024, 640);
            this.MinimumSize = new Size(1024, 640);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;

            ConstruirEsqueletoInterfaz();
            MostrarMenuPrincipalCliente();
            ActualizarLabelSaldoPantalla(); // Carga el saldo inicial al iniciar
        }

        private void ConstruirEsqueletoInterfaz()
        {
            this.Controls.Clear();

            // ==========================================
            // 1. BANDA SUPERIOR (Navbar)
            // ==========================================
            panelSuperior = new Panel { Height = 75, Dock = DockStyle.Top, BackColor = Color.White };

            pnlPerfil = new Panel { Size = new Size(150, 42), Location = new Point(20, 16), BackColor = Color.White };
            pnlPerfil.Paint += (s, e) => DibujarBordeSuave(pnlPerfil, e.Graphics, Color.Black, 2, 8);

            Label lblIconUser = new Label { Text = "👤", Font = new Font("Segoe UI", 14), Location = new Point(12, 6), AutoSize = true };
            pnlPerfil.Controls.Add(lblIconUser);
            panelSuperior.Controls.Add(pnlPerfil);

            btnSalirLogin = new Button
            {
                Text = "❌ SALIR",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(200, 50, 50),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(85, 32),
                Location = new Point(185, 21),
                Cursor = Cursors.Hand
            };
            btnSalirLogin.FlatAppearance.BorderSize = 1;
            btnSalirLogin.FlatAppearance.BorderColor = Color.FromArgb(230, 230, 230);
            btnSalirLogin.Click += BtnSalirLogin_Click;
            panelSuperior.Controls.Add(btnSalirLogin);

            lblSaldo = new Label
            {
                Text = "SALDO: ----",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(290, 26),
                AutoSize = true
            };
            panelSuperior.Controls.Add(lblSaldo);

            lblLogoReise = new Label
            {
                Text = "reise",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(880, 10),
                AutoSize = true
            };
            panelSuperior.Controls.Add(lblLogoReise);

            btnRecargar = new Button
            {
                Text = "RECARGAR",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 180, 100),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(110, 32),
                Location = new Point(750, 20),
                Cursor = Cursors.Hand
            };
            btnRecargar.FlatAppearance.BorderSize = 0;
            btnRecargar.Paint += (s, e) => RecortarBordesControl(btnRecargar, 8);
            btnRecargar.Click += BtnRecargar_Click; // Vinculación de recarga
            panelSuperior.Controls.Add(btnRecargar);

            // ==========================================
            // 2. SECCIÓN IZQUIERDA (Navegación Lateral)
            // ==========================================
            panelIzquierdo = new Panel { Width = 380, Dock = DockStyle.Left, BackColor = Color.White };

            picTodasLasRutas = new PictureBox
            {
                Size = new Size(340, 490),
                Location = new Point(20, 10),
                BackColor = Color.LightGray,
                SizeMode = PictureBoxSizeMode.CenterImage,
                Cursor = Cursors.Hand
            };
            picTodasLasRutas.Click += (s, e) => MostrarDashboardTodasLasRutas();
            panelIzquierdo.Controls.Add(picTodasLasRutas);

            lblTodasLasRutas = new Label
            {
                Text = "TODAS LAS RUTAS",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                BackColor = Color.FromArgb(220, 255, 255, 255),
                ForeColor = Color.Black,
                Size = new Size(340, 60),
                Location = new Point(0, 410),
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };
            lblTodasLasRutas.Click += (s, e) => MostrarDashboardTodasLasRutas();
            picTodasLasRutas.Controls.Add(lblTodasLasRutas);

            // ==========================================
            // 3. SECCIÓN DERECHA (Lienzo Dinámico)
            // ==========================================
            panelDerecho = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };

            this.Controls.Add(panelDerecho);
            this.Controls.Add(panelIzquierdo);
            this.Controls.Add(panelSuperior);
        }

        // VISTA A: MENÚ PRINCIPAL NORMAL
        private void MostrarMenuPrincipalCliente()
        {
            panelDerecho.Controls.Clear();

            Label lblPregunta = new Label
            {
                Text = "¿DÓNDE DESEAS IR HOY?",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 10),
                AutoSize = true
            };
            panelDerecho.Controls.Add(lblPregunta);

            Label lblBuscar = new Label { Text = "BUSCAR", Font = new Font("Segoe UI", 10, FontStyle.Regular), Location = new Point(20, 57), AutoSize = true };
            panelDerecho.Controls.Add(lblBuscar);

            Panel pnlBuscarInput = new Panel { Size = new Size(420, 32), Location = new Point(90, 52), BackColor = Color.White };
            pnlBuscarInput.Paint += (s, e) => DibujarBordeSuave(pnlBuscarInput, e.Graphics, Color.Gray, 1, 8);
            TextBox txtBuscar = new TextBox { Font = new Font("Segoe UI", 10), BorderStyle = BorderStyle.None, Size = new Size(400, 20), Location = new Point(10, 6) };
            pnlBuscarInput.Controls.Add(txtBuscar);
            panelDerecho.Controls.Add(pnlBuscarInput);

            FlowLayoutPanel containerRutas = new FlowLayoutPanel
            {
                Location = new Point(20, 105),
                Size = new Size(580, 410),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.White
            };
            containerRutas.HorizontalScroll.Maximum = 0;
            containerRutas.HorizontalScroll.Visible = false;
            panelDerecho.Controls.Add(containerRutas);

            // Simulación o lectura SQL de Top 3 rutas sugeridas
            
        }

        // VISTA B: DASHBOARD CENTRAL
        private void MostrarDashboardTodasLasRutas()
        {
            panelDerecho.Controls.Clear();

            Label lblTituloDash = new Label
            {
                Text = "SISTEMA CENTRAL — TODAS LAS RUTAS DISPONIBLES",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(20, 12),
                Size = new Size(540, 50),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };
            panelDerecho.Controls.Add(lblTituloDash);

            Button btnVolverMenuNormal = new Button
            {
                Text = "← Volver al Menú",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(74, 79, 84),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(140, 30),
                Location = new Point(20, 68),
                Cursor = Cursors.Hand
            };
            btnVolverMenuNormal.FlatAppearance.BorderSize = 0;
            btnVolverMenuNormal.Paint += (s, e) => RecortarBordesControl(btnVolverMenuNormal, 6);
            btnVolverMenuNormal.Click += (s, e) => MostrarMenuPrincipalCliente();
            panelDerecho.Controls.Add(btnVolverMenuNormal);

            FlowLayoutPanel gridRutasTotales = new FlowLayoutPanel
            {
                Location = new Point(20, 112),
                Size = new Size(580, 410),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.White
            };
            gridRutasTotales.HorizontalScroll.Maximum = 0;
            gridRutasTotales.HorizontalScroll.Visible = false;
            panelDerecho.Controls.Add(gridRutasTotales);

           //CONECION CON LA BD
            
        }

        // FUNCIÓN GENERADORA DE TARJETAS (Arreglada línea 219 de errores)
        private Panel GenerarTarjetaComponente(string nombreRuta, string precio)
        {
            Panel card = new Panel
            {
                Size = new Size(540, 105),
                MinimumSize = new Size(540, 105),
                MaximumSize = new Size(540, 105),
                BackColor = Color.FromArgb(74, 79, 84),
                Margin = new Padding(0, 0, 0, 15)
            };
            card.Paint += (s, e) => RecortarBordesControl(card, 15);

            Label lblIconBus = new Label { Text = "🚌", Font = new Font("Segoe UI", 34), ForeColor = Color.White, Location = new Point(30, 18), AutoSize = true };
            card.Controls.Add(lblIconBus);

            Label lblNombre = new Label { Text = nombreRuta, Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.White, Location = new Point(125, 36), AutoSize = true };
            card.Controls.Add(lblNombre);

            Label lblPrecio = new Label { Text = precio, Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.White, Location = new Point(445, 22), AutoSize = true };
            card.Controls.Add(lblPrecio);

            Button btnComprar = new Button
            {
                Text = "COMPRAR",
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 180, 100),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(95, 28),
                Location = new Point(420, 58),
                Cursor = Cursors.Hand
            };
            btnComprar.FlatAppearance.BorderSize = 0;
            btnComprar.Paint += (s, e) => RecortarBordesControl(btnComprar, 6);
            card.Controls.Add(btnComprar);

            return card;
        }

        // MANEJADORES DE EVENTOS
        private void BtnRecargar_Click(object sender, EventArgs e)
        {
            using (RecargarForm pantallaRecarga = new RecargarForm())
            {
                if (pantallaRecarga.ShowDialog(this) == DialogResult.OK)
                {
                    ActualizarLabelSaldoPantalla();
                }
            }
        }

        private void ActualizarLabelSaldoPantalla()
        {
            
        }

        private void BtnSalirLogin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LoginForm login = new LoginForm();
                login.Show();
                this.Close();
            }
        }

        // MÉTODOS AUXILIARES GRÁFICOS (GDI+) - Solución a Errores CS0103
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