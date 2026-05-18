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

        // CORREGIDO: Instancia del servicio utilizando camelCase para evitar conflictos de nombres con la clase
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
                Text = "SALDO: $0.00",
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
            btnRecargar.Click += BtnRecargar_Click;
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
        // VISTA A: MENÚ PRINCIPAL NORMAL
        private void MostrarMenuPrincipalCliente()
        {
            // CORREGIDO: Volvemos a mostrar el panel izquierdo al regresar al menú principal
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
            pnlBuscarInput.Paint += (s, e) => DibujarBordeSuave(pnlBuscarInput, e.Graphics, Color.Gray, 1, 8);
            TextBox txtBuscar = new TextBox { Font = new Font("Segoe UI", 10), BorderStyle = BorderStyle.None, Size = new Size(400, 20), Location = new Point(10, 6) };

            // Filtro en tiempo real al escribir en la caja
            txtBuscar.TextChanged += (s, e) => CargarRutasDesdeBaseDatos(panelDerecho.Controls["containerRutas"] as FlowLayoutPanel, txtBuscar.Text);

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

            CargarRutasDesdeBaseDatos(containerRutas, "");
        }

        // VISTA B: DASHBOARD CENTRAL
        private void MostrarDashboardTodasLasRutas()
        {
            // CORREGIDO: Ocultamos el panel de navegación izquierdo por completo
            if (panelIzquierdo != null)
            {
                panelIzquierdo.Visible = false;
            }

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

            // CORREGIDO: Como ahora se oculta el panel izquierdo, ampliamos el Size de 580 a 960 de ancho 
            // para que aproveche todo el espacio libre y el diseño se vea espectacular.
            FlowLayoutPanel gridRutasTotales = new FlowLayoutPanel
            {
                Location = new Point(20, 112),
                Size = new Size(960, 410),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.White
            };
            gridRutasTotales.HorizontalScroll.Maximum = 0;
            gridRutasTotales.HorizontalScroll.Visible = false;
            panelDerecho.Controls.Add(gridRutasTotales);

            CargarRutasDesdeBaseDatos(gridRutasTotales, "");
        }

        // VISTA B: DASHBOARD CENTRAL
        

        // Método auxiliar para consultar la tabla de Rutas e insertarlas en los paneles
        private void CargarRutasDesdeBaseDatos(FlowLayoutPanel layoutDestino, string filtroBusqueda)
        {
            if (layoutDestino == null) return;

            layoutDestino.SuspendLayout();
            layoutDestino.Controls.Clear();

            try
            {
                // CORREGIDO: Llamada con minúscula a la instancia 'rutaService' para eliminar el error CS0120
                List<RutaDTO> rutas = rutaService.ObtenerRutas(filtroBusqueda);

                if (rutas.Count == 0)
                {
                    Label lblVacio = new Label
                    {
                        Text = "No se encontraron rutas disponibles.",
                        Font = new Font("Segoe UI", 11, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Margin = new Padding(20)
                    };
                    layoutDestino.Controls.Add(lblVacio);
                    return;
                }

                foreach (var r in rutas)
                {
                    // Concatenamos el Inicio y el Final para mostrar el trayecto completo en la tarjeta
                    string trayectoCompleto = $"{r.Inicio} ➔ {r.Final}";

                    // CORREGIDO: Pasamos los 3 argumentos que la nueva firma requiere (Número, trayecto, costo)
                    Panel tarjeta = GenerarTarjetaComponente(r.NumeroRuta, trayectoCompleto, $"${r.CostoDelPasaje:F2}");

                    // Buscamos el botón COMPRAR para incrustarle el IdRutaBuses correcto
                    foreach (Control c in tarjeta.Controls)
                    {
                        if (c is Button && c.Text == "COMPRAR")
                        {
                            c.Tag = r.IdRutaBuses; // Llave primaria real de tu BD
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
            finally
            {
                layoutDestino.ResumeLayout();
            }
        }

        // CORREGIDO: Firma adaptada a 3 parámetros para mostrar estéticamente Número de ruta y Destinos en la tarjeta
        private Panel GenerarTarjetaComponente(string numeroRuta, string trayecto, string precio)
        {
            Panel card = new Panel
            {
                Size = new Size(540, 105),
                MinimumSize = new Size(540, 105),
                MaximumSize = new Size(540, 105),
                BackColor = Color.FromArgb(74, 79, 84),
                Margin = new Padding(0, 0, 0, 15)
            };
            card.SizeChanged += (s, e) => RecortarBordesControl(card, 15);

            // 1. Icono del autobús
            Label lblIconBus = new Label { Text = "🚌", Font = new Font("Segoe UI", 32), ForeColor = Color.White, Location = new Point(20, 20), AutoSize = true };
            card.Controls.Add(lblIconBus);

            // 2. Número de Ruta (Ej: "Ruta 42")
            Label lblNumRuta = new Label { Text = numeroRuta, Font = new Font("Segoe UI", 15, FontStyle.Bold), ForeColor = Color.FromArgb(0, 210, 120), Location = new Point(105, 18), AutoSize = true };
            card.Controls.Add(lblNumRuta);

            // 3. Recorrido de la Ruta (Ej: "Soyapango ➔ Santa Tecla")
            Label lblTrayecto = new Label { Text = trayecto, Font = new Font("Segoe UI", 11, FontStyle.Regular), ForeColor = Color.White, Location = new Point(106, 52), AutoSize = true };
            card.Controls.Add(lblTrayecto);

            // 4. Costo del pasaje (Ej: "$0.35")
            Label lblPrecio = new Label { Text = precio, Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.White, Location = new Point(445, 20), AutoSize = true };
            card.Controls.Add(lblPrecio);

            // 5. Botón de acción Compra
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
            btnComprar.SizeChanged += (s, e) => RecortarBordesControl(btnComprar, 6);
            card.Controls.Add(btnComprar);

            return card;
        }

        // Manejador del botón Comprar dentro de las tarjetas de buses

        private void BtnComprarRuta_Click(object sender, EventArgs e)
        {
            // 1. Identificamos qué botón originó el evento y extraemos el ID de la ruta guardado en su propiedad Tag
            Button btnSeleccionado = (Button)sender;
            int idRutaSeleccionada = Convert.ToInt32(btnSeleccionado.Tag);

            // Variables locales para almacenar la información unificada del recorrido
            string numeroRuta = "";
            string inicioRecorrido = "";
            string finalRecorrido = "";
            decimal costoDelPasaje = 0.00m;

            // 2. Abrimos la conexión utilizando tu clase de configuración
            Conexion con = new Conexion();
            SqlConnection cn = con.AbrirConexion();

            if (cn == null)
            {
                MessageBox.Show("No se pudo establecer conexión con el servidor de Reise2go.", "Error de Red", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 3. Consulta SQL con INNER JOIN basada fielmente en tu script de base de datos
                // Extrae el costo, número de ruta público, origen y destino unificando las llaves foráneas
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
                            // Asignamos los valores recuperados de las tablas correspondientes
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

                // 4. PASO CLAVE: Instanciamos el formulario de detalle pasándole los parámetros
                // NOTA: "this.idClienteActual" representa la variable global donde guardas el ID del cliente logueado (ej. Sarai con ID = 1)
                using (DetalleRutaForm frmDetalle = new DetalleRutaForm(this.idClienteActual, idRutaSeleccionada, numeroRuta, inicioRecorrido, finalRecorrido, costoDelPasaje))
                {
                    // Mostramos el formulario como ventana modal (bloquea el menú de atrás hasta que decida Comprar o Cancelar)
                    if (frmDetalle.ShowDialog() == DialogResult.OK)
                    {
                        // Si dentro del DetalleRutaForm presionó "COMPRAR PASAJE" y la transacción fue exitosa,
                        // el formulario devuelve DialogResult.OK, por lo que actualizamos el Label del saldo en el menú principal.
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
                // Aseguramos el cierre de la conexión pase lo que pase
                con.CerrarConexion(cn);
            }
        }

        private void BtnRecargar_Click(object sender, EventArgs e)
        {
            // Le pasas el id del cliente actual (ej. objeto de sesión o campo global de tu formulario)
            using (RecargarForm frmRecarga = new RecargarForm(this.idClienteActual))
            {
                if (frmRecarga.ShowDialog() == DialogResult.OK)
                {
                    // Si el pago se ejecutó con éxito, refrescas la interfaz del menú inmediatamente
                    ActualizarLabelSaldoPantalla();
                }
            }
        }

        // CONECTADO: Lee de forma exacta el saldo del cliente desde la base de datos
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

        // MÉTODOS AUXILIARES GRÁFICOS (GDI+)
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