using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_
{
    public partial class LoginForm : Form
    {
        private Label lblWelcome;

        // Contenedores para emular TextBox redondeados
        private Panel pnlInputUser;
        private TextBox txtUsername;

        private Panel pnlInputPass;
        private TextBox txtPassword;

        private Button btnLogin;

        public LoginForm()
        {
            this.Size = new Size(800, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;

            InicializarComponentesSuaves();
        }

        private void InicializarComponentesSuaves()
        {
            this.Paint += LoginMinimalistaForm_Paint;

            // 1. TEXTO WELCOME
            lblWelcome = new Label
            {
                Text = "Bienvenido!",
                Font = new Font("Segoe UI Light", 32, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = true,
                BackColor = Color.Transparent
            };
            this.Controls.Add(lblWelcome);

            // 2. INPUT USERNAME (Contenedor Redondeado)
            pnlInputUser = new Panel { Size = new Size(270, 40), BackColor = Color.FromArgb(110, 110, 110) };
            pnlInputUser.Paint += RunderComponent_Paint; // Evento para redondear el panel

            txtUsername = new TextBox
            {
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(110, 110, 110),
                BorderStyle = BorderStyle.None, // Quitamos el borde cuadrado nativo
                Width = 250,
                Location = new Point(10, 10), // Padding interno para centrar el texto
                Text = "Username"
            };
            txtUsername.Enter += RemovePlaceholder;
            txtUsername.Leave += AddPlaceholder;
            pnlInputUser.Controls.Add(txtUsername);
            this.Controls.Add(pnlInputUser);

            // 3. INPUT PASSWORD (Contenedor Redondeado)
            pnlInputPass = new Panel { Size = new Size(270, 40), BackColor = Color.FromArgb(110, 110, 110) };
            pnlInputPass.Paint += RunderComponent_Paint;

            txtPassword = new TextBox
            {
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(110, 110, 110),
                BorderStyle = BorderStyle.None,
                Width = 250,
                Location = new Point(10, 10),
                Text = "Password"
            };
            txtPassword.Enter += RemovePlaceholder;
            txtPassword.Leave += AddPlaceholder;
            pnlInputPass.Controls.Add(txtPassword);
            this.Controls.Add(pnlInputPass);

            // 4. BOTÓN LOGIN (Redondeado nativo)
            btnLogin = new Button
            {
                Text = "Login",
                Size = new Size(270, 42),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.White,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            // Enlazamos el evento Click con el método que abrirá el menú (como en JS)
            btnLogin.Click += BtnLogin_Click;
            btnLogin.Paint += RunderComponent_Paint; // También le aplicamos el suavizado de bordes
            this.Controls.Add(btnLogin);

            this.Resize += (s, e) => { AcomodarControles(); this.Invalidate(); };
            AcomodarControles();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            // 1. (Opcional) Validación básica estilo JS
            if (txtUsername.Text == "Username" || string.IsNullOrWhiteSpace(txtUsername.Text) ||
                txtPassword.Text == "Password" || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Por favor, ingrese sus credenciales.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Simulamos la validación (aquí irá luego tu lógica de base de datos)
            // Supongamos que tu formulario del menú principal se llama 'MenuPrincipalForm'
           /* Form2 menu = new Form2();

            // Mostramos el menú principal
            menu.Show();

            // Ocultamos el formulario de Login actual para que no quede flotando atrás
            this.Hide();

            // NOTA PROFESIONAL: 
            // Para que la aplicación se cierre por completo cuando el usuario cierre el menú principal,
            // enlazamos el evento 'FormClosed' del menú para que destruya el proceso del Login oculto:
            menu.FormClosed += (s, args) => this.Close();*/
        }


        private void AcomodarControles()
        {
            int centroX = this.Width / 2;
            lblWelcome.Location = new Point(centroX - (lblWelcome.Width / 2), 80);
            pnlInputUser.Location = new Point(centroX - (pnlInputUser.Width / 2), 165);
            pnlInputPass.Location = new Point(centroX - (pnlInputPass.Width / 2), 220);
            btnLogin.Location = new Point(centroX - (btnLogin.Width / 2), 275);
        }

        // --- EL METODO MÁGICO PARA REDONDEAR TEXTBOXES Y BOTONES ---
        private void RunderComponent_Paint(object sender, PaintEventArgs e)
        {
            Control control = (Control)sender;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias; // Súper importante para que no se vea pixelado

            int radius = 12; // Modifica este número si quieres los bordes más o menos redondeados (tipo border-radius: 12px)
            Rectangle rect = new Rectangle(0, 0, control.Width - 1, control.Height - 1);

            using (GraphicsPath path = GetRoundedRectanglePath(rect, radius))
            {
                // Recorta el componente para que tenga las esquinas curvas
                control.Region = new Region(path);

                // Si es un panel de Input, le dibujamos un borde fino blanco semitransparente como en tu imagen
                if (control is Panel)
                {
                    using (Pen penBorde = new Pen(Color.FromArgb(80, 255, 255, 255), 1))
                    {
                        g.DrawPath(penBorde, path);
                    }
                }
            }
        }

        // Generador de rutas con curvas
        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        // Fondo Degradado (El que ya corregimos antes)
        private void LoginMinimalistaForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(43, 47, 50)))
            {
                g.FillRectangle(bgBrush, this.ClientRectangle);
            }

            int expansion = 150;
            Rectangle radialRect = new Rectangle(-expansion, -expansion, this.Width + (expansion * 2), this.Height + (expansion * 2));

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(radialRect);
                using (PathGradientBrush pgb = new PathGradientBrush(path))
                {
                    pgb.CenterColor = Color.FromArgb(125, 130, 133);
                    pgb.SurroundColors = new Color[] { Color.FromArgb(43, 47, 50) };
                    pgb.CenterPoint = new PointF(this.Width / 2f, this.Height / 2.3f);
                    g.FillRectangle(pgb, this.ClientRectangle);
                }
            }
        }

        private void RemovePlaceholder(object sender, EventArgs e)
        {
            TextBox tx = (TextBox)sender;

            // Si el texto actual es el marcador de posición, lo vaciamos para que el usuario escriba
            if (tx.Text == "Username" || tx.Text == "Password")
            {
                tx.Text = "";
                tx.ForeColor = Color.White; // Color del texto real cuando escriben

                // Si es el campo de contraseña, activamos los puntitos ocultos '●'
                if (tx == txtPassword)
                {
                    tx.UseSystemPasswordChar = true;
                }
            }
        }

        private void AddPlaceholder(object sender, EventArgs e)
        {
            TextBox tx = (TextBox)sender;

            // Si el usuario no escribió nada y dejó el campo vacío, restauramos el marcador
            if (string.IsNullOrWhiteSpace(tx.Text))
            {
                tx.ForeColor = Color.FromArgb(200, 200, 200); // Color gris claro para el placeholder

                if (tx == txtUsername)
                {
                    tx.Text = "Username";
                }

                if (tx == txtPassword)
                {
                    tx.UseSystemPasswordChar = false; // Desactivamos los puntitos para que se lea "Password"
                    tx.Text = "Password";
                }
            }
        }
    }
}