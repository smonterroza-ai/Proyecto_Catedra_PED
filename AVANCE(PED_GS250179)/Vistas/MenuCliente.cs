using AVANCE_PED_GS250179_.Datos; // Asegura el acceso a tu clase Conexion
using AVANCE_PED_GS250179_.Servicio;
using AVANCE_PED_GS250179_.Vistas;
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
        // Instancia de tu clase de conexión para no usar cadenas quemadas en el diseño
        private Conexion conexion = new Conexion();

        // Instancia del servicio utilizando camelCase para evitar conflictos de nombres con la clase
        private RutaService rutaService = new RutaService();

        // Variable global indispensable para registrar las transacciones del cliente logueado
        private int idClienteActual;

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

        // Constructor recibe el ID enviado desde Form1 para romper el error de compilación
        public MenuCliente(int idCliente)
        {
            // Asignamos el parámetro a nuestra variable global
            this.idClienteActual = idCliente;

            this.Size = new Size(1024, 640);
            this.MinimumSize = new Size(1024, 640);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;

            ConstruirEsqueletoInterfaz();
            MostrarMenuPrincipalCliente();
            ActualizarLabelSaldoPantalla(); // Carga el saldo real desde la BD
        }

        private void ConstruirEsqueletoInterfaz()
        {
            this.Controls.Clear();

            // ==========================================
            // 1. BANDA SUPERIOR (Navbar)
            // ==========================================
            panelSuperior = new Panel { Height = 75, Dock = DockStyle.Top, BackColor = Color.White };

            // Contenedor con AutoSize dinámico para albergar nombres de cualquier longitud
            pnlPerfil = new Panel { Location = new Point(20, 16), Height = 42, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, BackColor = Color.White };
            pnlPerfil.Paint += (s, e) => DibujarBordeSuave(pnlPerfil, e.Graphics, Color.FromArgb(230, 230, 230), 1, 8);

            // Label dinámico modificado: Muestra "Bienvenido/a: [Nombre]" sin íconos molestos
            string nombreUsuarioLogueado = ObtenerNombreUsuarioBD();
            Label lblNombreUser = new Label
            {
                Text = $"Bienvenido/a: {nombreUsuarioLogueado}",
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(12, 12), // Ajustado al inicio del panel al no haber ícono
                AutoSize = true,
                Margin = new Padding(0, 0, 15, 0) // Previene que el borde toque el final del texto
            };
            pnlPerfil.Controls.Add(lblNombreUser);
            panelSuperior.Controls.Add(pnlPerfil);

            // Manteniendo posición X fija original para evitar colisiones visuales
            btnSalirLogin = new Button
            {
                Text = "❌ SALIR",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 53, 69),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(90, 32),
                Location = new Point(280, 21),
                Cursor = Cursors.Hand
            };
            btnSalirLogin.FlatAppearance.BorderSize = 1;
            btnSalirLogin.FlatAppearance.BorderColor = Color.FromArgb(240, 240, 240);
            btnSalirLogin.Click += BtnSalirLogin_Click;
            panelSuperior.Controls.Add(btnSalirLogin);

            lblSaldo = new Label
            {
                Text = "SALDO: $0.00",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(395, 26),
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
                BackColor = Color.FromArgb(0, 185, 113), // Color Verde Profesional de Reise
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(110, 32),
                Location = new Point(750, 20),
                Cursor = Cursors.Hand
            };
            btnRecargar.FlatAppearance.BorderSize = 0;
            btnRecargar.Paint += (s, e) => RecortarBordesControl(btnRecargar, 8);
            btnRecargar.Click += BtnRecargar_Click;
            panelSuperior.Controls.Add(btnRecargar);

            // ==========================================
            // 2. SECCIÓN IZQUIERDA (Navegación Lateral)
            // ==========================================
            panelIzquierdo = new Panel { Width = 380, Dock = DockStyle.Left, BackColor = Color.White };

            picTodasLasRutas = new PictureBox
            {
                Size = new Size(340, 410), // Ajustamos el alto para dejarle espacio limpio al texto abajo
                Location = new Point(20, 10),
                BackColor = Color.FromArgb(245, 245, 245), // Un gris suave de fondo por si la imagen tiene transparencias
                SizeMode = PictureBoxSizeMode.Zoom, // 'Zoom' mantiene la proporción de tu imagen sin deformarla
                Cursor = Cursors.Hand
            };

            // Intentamos cargar la imagen desde los recursos del proyecto de forma segura
            try
            {
                // Cambia "imagen_rutas_portada" por el nombre exacto con el que guardes tu imagen en Resources
                picTodasLasRutas.Image = Properties.Resources.icono_rutas;
            }
            catch
            {
                // Respaldo visual si aún no la has importado a Visual Studio
                picTodasLasRutas.BackColor = Color.FromArgb(230, 230, 230);
            }

            picTodasLasRutas.Click += (s, e) => MostrarDashboardTodasLasRutas();
            panelIzquierdo.Controls.Add(picTodasLasRutas);

            lblTodasLasRutas = new Label
            {
                Text = "TODAS LAS RUTAS",
                Font = new Font("Segoe UI", 16, FontStyle.Bold), // Un tamaño 16 queda más balanceado
                BackColor = Color.FromArgb(245, 245, 245),
                ForeColor = Color.Black,
                Size = new Size(340, 50),
                Location = new Point(20, 425), // Posicionado exactamente abajo de la imagen (410 + 10 + margen)
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };
            lblTodasLasRutas.Click += (s, e) => MostrarDashboardTodasLasRutas();
            lblTodasLasRutas.Paint += (s, e) => DibujarBordeSuave(lblTodasLasRutas, e.Graphics, Color.FromArgb(230, 230, 230), 1, 8);

            // Agregamos el label directamente al panel izquierdo, no a la imagen
            panelIzquierdo.Controls.Add(lblTodasLasRutas);

            // ==========================================
            // 3. SECCIÓN DERECHA (Lienzo Dinámico)
            // ==========================================
            panelDerecho = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };

            this.Controls.Add(panelDerecho);
            this.Controls.Add(panelIzquierdo);
            this.Controls.Add(panelSuperior);
        }

        // Método auxiliar para consultar el Nombre real del usuario logueado en la BD
        private string ObtenerNombreUsuarioBD()
        {
            SqlConnection cn = conexion.AbrirConexion();
            try
            {
                string query = "SELECT Nombre FROM InfoCliente WHERE IdCliente = @Id";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", idClienteActual);
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null && resultado != DBNull.Value)
                    {
                        return resultado.ToString();
                    }
                }
            }
            catch
            {
                return "Usuario Client";
            }
            finally
            {
                conexion.CerrarConexion(cn);
            }
            return "Usuario Client";
        }

        // VISTA A: MENÚ PRINCIPAL NORMAL (Columna única)
        private void MostrarMenuPrincipalCliente()
        {
            if (panelIzquierdo != null)
            {
                panelIzquierdo.Visible = true;
            }

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
            pnlBuscarInput.Paint += (s, e) => DibujarBordeSuave(pnlBuscarInput, e.Graphics, Color.DarkGray, 1, 8);
            TextBox txtBuscar = new TextBox { Font = new Font("Segoe UI", 10), BorderStyle = BorderStyle.None, Size = new Size(400, 20), Location = new Point(10, 6), };

            txtBuscar.TextChanged += (s, e) => CargarRutasDesdeBaseDatos(panelDerecho.Controls["containerRutas"] as FlowLayoutPanel, txtBuscar.Text, 540);

            pnlBuscarInput.Controls.Add(txtBuscar);
            panelDerecho.Controls.Add(pnlBuscarInput);

            FlowLayoutPanel containerRutas = new FlowLayoutPanel
            {
                Name = "containerRutas",
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

            CargarRutasDesdeBaseDatos(containerRutas, "", 540);
        }

        // VISTA B: DASHBOARD CENTRAL EN DOS COLUMNAS CON BUSCADOR
        private void MostrarDashboardTodasLasRutas()
        {
            if (panelIzquierdo != null)
            {
                panelIzquierdo.Visible = false; // Al ocultarse, panelDerecho toma todo el ancho (1004px aprox)
            }

            panelDerecho.Controls.Clear();

            // 1. Agregamos el PictureBox para el icono/imagen del título
            PictureBox picIconoDash = new PictureBox
            {
                Location = new Point(20, 15),
                Size = new Size(35, 35),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White
            };

            // Intenta cargar la imagen desde Resources de manera segura para evitar caídas si aún no la agregas
            try
            {
                picIconoDash.Image = Properties.Resources.icono_rutas;
            }
            catch
            {
                // Si aún no pones la imagen en Resources, dejará el cuadro vacío en blanco sin crashear
                picIconoDash.BackColor = Color.FromArgb(240, 240, 240);
            }
            panelDerecho.Controls.Add(picIconoDash);

            // 2. Título de la sección desplazado un poco a la derecha (X=65) para dejarle espacio al icono
            Label lblTituloDash = new Label
            {
                Text = "SISTEMA CENTRAL — TODAS LAS RUTAS DISPONIBLES",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(65, 12),
                Size = new Size(915, 40),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panelDerecho.Controls.Add(lblTituloDash);

            // =========================================================================
            // Barra de Acciones con Filtrador (Alineación corregida a lo ancho)
            // =========================================================================
            FlowLayoutPanel barraAcciones = new FlowLayoutPanel
            {
                Location = new Point(20, 55),
                Size = new Size(960, 45),
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                BackColor = Color.White
            };

            // Caja contenedora de Input estilizada
            Panel pnlBuscarInputDash = new Panel
            {
                Size = new Size(220, 32),
                BackColor = Color.White,
                Margin = new Padding(0, 3, 0, 0)
            };
            pnlBuscarInputDash.Paint += (s, e) => DibujarBordeSuave(pnlBuscarInputDash, e.Graphics, Color.DarkGray, 1, 6);

            TextBox txtBuscarDash = new TextBox { Font = new Font("Segoe UI", 10), BorderStyle = BorderStyle.None, Size = new Size(200, 20), Location = new Point(10, 6) };
            pnlBuscarInputDash.Controls.Add(txtBuscarDash);
            barraAcciones.Controls.Add(pnlBuscarInputDash);

            // Etiqueta Filtrar integrada
            Label lblBuscarDash = new Label
            {
                Text = "FILTRAR RUTAS:",
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60),
                AutoSize = true,
                Margin = new Padding(0, 10, 8, 0)
            };
            barraAcciones.Controls.Add(lblBuscarDash);

            // Botón Regresar
            Button btnVolverMenuNormal = new Button
            {
                Text = "← Volver al Menú",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(74, 79, 84),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                AutoSize = true,
                MinimumSize = new Size(135, 32),
                Margin = new Padding(0, 3, 40, 0),
                Cursor = Cursors.Hand
            };
            btnVolverMenuNormal.FlatAppearance.BorderSize = 0;
            btnVolverMenuNormal.Paint += (s, e) => RecortarBordesControl(btnVolverMenuNormal, 6);
            btnVolverMenuNormal.Click += (s, e) => {
                MostrarMenuPrincipalCliente();
                ActualizarLabelSaldoPantalla();
            };
            barraAcciones.Controls.Add(btnVolverMenuNormal);

            panelDerecho.Controls.Add(barraAcciones);

            // =========================================================================
            // Contenedor en Formato Grid Enorme (Dos Columnas Reales Limpias)
            // =========================================================================
            FlowLayoutPanel gridRutasTotales = new FlowLayoutPanel
            {
                Name = "gridRutasTotales",
                Location = new Point(20, 110),
                Size = new Size(975, 440), // Crecimiento estratégico para desplegar las dos columnas sin truncar
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true, // Esencial para el salto de columna
                BackColor = Color.White
            };
            gridRutasTotales.HorizontalScroll.Maximum = 0;
            gridRutasTotales.HorizontalScroll.Visible = false;
            panelDerecho.Controls.Add(gridRutasTotales);

            // Asignación de evento de filtrado en tiempo real pasándole los 455px optimizados por columna
            txtBuscarDash.TextChanged += (s, e) => CargarRutasDesdeBaseDatos(gridRutasTotales, txtBuscarDash.Text, 455);

            // Carga inicial: 2 Columnas impecables de 455px dejando margen de protección para el Scrollbar
            CargarRutasDesdeBaseDatos(gridRutasTotales, "", 455);
        }

        // Método auxiliar para consultar la tabla de Rutas e insertarlas en los paneles
        private void CargarRutasDesdeBaseDatos(FlowLayoutPanel layoutDestino, string filtroBusqueda, int anchoTarjeta)
        {
            if (layoutDestino == null) return;

            layoutDestino.SuspendLayout();
            layoutDestino.Controls.Clear();

            try
            {
                List<RutaDTO> rutas = rutaService.ObtenerRutas(filtroBusqueda);

                if (rutas.Count == 0)
                {
                    Label lblVacio = new Label
                    {
                        Text = "No se encontraron rutas disponibles.",
                        Font = new Font("Segoe UI", 11, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        AutoSize = false, // Lo cambiamos a false para controlarlo nosotros
                        Size = new Size(layoutDestino.Width - 40, 40), 
                        TextAlign = ContentAlignment.MiddleCenter, // Centramos el texto
                        Margin = new Padding(20, 20, 20, 0)
                    };
                    layoutDestino.Controls.Add(lblVacio);
                    return;
                }

                foreach (var r in rutas)
                {
                    string traytrayectoCompleto = $"{r.Inicio} ➔ {r.Final}";

                    // Enviamos el anchoTarjeta que requiere cada layout de forma estricta
                    Panel tarjeta = GenerarTarjetaComponente(r.NumeroRuta, traytrayectoCompleto, $"${r.CostoDelPasaje:F2}", anchoTarjeta);

                    foreach (Control c in tarjeta.Controls)
                    {
                        if (c is Button && c.Text == "COMPRAR")
                        {
                            c.Tag = r.IdRutaBuses; // Llave primaria de la BD
                            c.Click += BtnComprarRuta_Click;
                        }
                    }

                    layoutDestino.Controls.Add(tarjeta);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Carga de Rutas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            layoutDestino.ResumeLayout();
        }

        // CONTROL RESPONSIVO CORREGIDO: Se integró PictureBox real y se movieron los textos a la derecha
        private Panel GenerarTarjetaComponente(string numeroRuta, string trayecto, string precio, int widthDinamico)
        {
            // Espaciado dinámico lateral
            int margenDerecho = (widthDinamico < 500) ? 15 : 12;

            Panel card = new Panel
            {
                Size = new Size(widthDinamico, 115),
                BackColor = Color.FromArgb(50, 55, 60),
                Margin = new Padding(0, 0, margenDerecho, 15)
            };

            card.SizeChanged += (s, e) => RecortarBordesControl(card, 12);

            // 1. CORRECCIÓN PRINCIPAL: Se sustituye el Label con emoji por un PictureBox dinámico que lee de Resources
            PictureBox picIconBus = new PictureBox
            {
                Size = new Size(50, 50),
                Location = new Point(16, 16),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            try
            {
                picIconBus.Image = Properties.Resources.icono_rutas;
            }
            catch
            {
                // Respaldo visual en caso de fallo de carga
                picIconBus.BackColor = Color.DimGray;
            }
            card.Controls.Add(picIconBus);

            // 2. CORRECCIÓN DE POSICIÓN: Desplazado a X=82 para que no se solape con el PictureBox de 50px y corte letras
            Label lblNumRuta = new Label
            {
                Text = numeroRuta,
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 185, 113),
                Location = new Point(82, 16),
                AutoSize = true
            };
            card.Controls.Add(lblNumRuta);

            // 3. CORRECCIÓN DE POSICIÓN: Desplazado a X=82 para mantener la alineación vertical limpia
            Label lblTrayecto = new Label
            {
                Text = trayecto,
                Font = new Font("Segoe UI", 9f, FontStyle.Regular),
                ForeColor = Color.FromArgb(235, 235, 235),
                Location = new Point(82, 44),
                AutoSize = false,
                Size = new Size(widthDinamico - 100, 34)
            };
            card.Controls.Add(lblTrayecto);

            // 4. Costo del pasaje mantiene posición estética inferior
            Label lblPrecio = new Label { Text = precio, Font = new Font("Segoe UI", 13f, FontStyle.Bold), ForeColor = Color.White, Location = new Point(16, 80), AutoSize = true };
            card.Controls.Add(lblPrecio);

            // 5. Botón de acción Compra responsivo pegado al extremo derecho
            Button btnComprar = new Button
            {
                Text = "COMPRAR",
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 185, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 28),
                Location = new Point(widthDinamico - 115, 78),
                Cursor = Cursors.Hand
            };
            btnComprar.FlatAppearance.BorderSize = 0;
            btnComprar.SizeChanged += (s, e) => RecortarBordesControl(btnComprar, 6);
            card.Controls.Add(btnComprar);

            return card;
        }

        private void BtnComprarRuta_Click(object sender, EventArgs e)
        {
            Button btnSeleccionado = (Button)sender;
            int idRutaSeleccionada = Convert.ToInt32(btnSeleccionado.Tag);

            string numeroRuta = "";
            string inicioRecorrido = "";
            string finalRecorrido = "";
            decimal costoDelPasaje = 0.00m;

            Conexion con = new Conexion();
            SqlConnection cn = con.AbrirConexion();

            if (cn == null)
            {
                MessageBox.Show("No se pudo establecer conexión con el servidor de Reise2go.", "Error de Red", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string queryBuscarRuta = @"
                    SELECT r.CostoDelPasaje, i.NumeroRuta, rec.inicio, rec.Final 
                    FROM RutaBuses r
                    INNER JOIN InfoRutaBuses i ON r.IdRutaBuses = i.IdRutaBuses
                    INNER JOIN RecorridoRuta rec ON r.IdRecorridoRuta = rec.IdRecorridoRuta
                    WHERE r.IdRutaBuses = @IdRuta";

                using (SqlCommand cmd = new SqlCommand(queryBuscarRuta, cn))
                {
                    cmd.Parameters.AddWithValue("@IdRuta", idRutaSeleccionada);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            numeroRuta = reader["NumeroRuta"].ToString();
                            inicioRecorrido = reader["inicio"].ToString();
                            finalRecorrido = reader["Final"].ToString();
                            costoDelPasaje = Convert.ToDecimal(reader["CostoDelPasaje"]);
                        }
                        else
                        {
                            MessageBox.Show("No se encontraron especificaciones de itinerario para la ruta seleccionada.", "Aviso de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                using (DetalleRutaForm frmDetalle = new DetalleRutaForm(this.idClienteActual, idRutaSeleccionada, numeroRuta, inicioRecorrido, finalRecorrido, costoDelPasaje))
                {
                    if (frmDetalle.ShowDialog() == DialogResult.OK)
                    {
                        ActualizarLabelSaldoPantalla();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la pantalla de detalles: " + ex.Message, "Fallo del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.CerrarConexion(cn);
            }
        }

        private void BtnRecargar_Click(object sender, EventArgs e)
        {
            using (RecargarForm frmRecarga = new RecargarForm(this.idClienteActual))
            {
                if (frmRecarga.ShowDialog() == DialogResult.OK)
                {
                    ActualizarLabelSaldoPantalla();
                }
            }
        }

        private void ActualizarLabelSaldoPantalla()
        {
            SqlConnection cn = conexion.AbrirConexion();
            try
            {
                string query = "SELECT Saldo FROM InfoCliente WHERE IdCliente = @Id";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", idClienteActual);
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null && resultado != DBNull.Value)
                    {
                        decimal saldo = Convert.ToDecimal(resultado);
                        lblSaldo.Text = $"SALDO: ${saldo:F2}";
                    }
                    else
                    {
                        lblSaldo.Text = "SALDO: $0.00";
                    }
                }
            }
            catch
            {
                lblSaldo.Text = "SALDO: Error";
            }
            finally
            {
                conexion.CerrarConexion(cn);
            }
        }

        private void BtnSalirLogin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 login = new Form1();
                login.Show();
                this.Close();
            }
        }

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