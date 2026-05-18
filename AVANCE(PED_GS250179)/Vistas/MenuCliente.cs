using AVANCE_PED_GS250179_;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient; // Regresamos al SqlClient nativo del Framework
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_
{
    public partial class MenuCliente : Form
    {
        //CONEXIÓN CON LA BD
        private Datos.Conexion conexionGeneral = new Datos.Conexion();

        // Identificadores del usuario autenticado (Asignado por defecto temporalmente, o mediante constructor)
        private int idClienteLogueado = 1;

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

        // Constructor estándar
        public MenuCliente()
        {
            this.Size = new Size(1024, 640);
            this.MinimumSize = new Size(1024, 640);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;

            ConstruirEsqueletoInterfaz();
            MostrarMenuPrincipalCliente();
            ActualizarLabelSaldoPantalla();
        }

        // Sobrecarga opcional para recibir el ID del cliente directamente desde el Login
        public MenuCliente(int idCliente) : this()
        {
            this.idClienteLogueado = idCliente;
            ActualizarLabelSaldoPantalla();
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
                Text = "SALDO: ---.--",
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

        // VISTA A: MENÚ PRINCIPAL CON TOP 3 RUTAS DISPONIBLES (Desde la Vista SQL)
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

            string queryTopRutas = "SELECT TOP 3 IdRutaBuses, NumeroRuta, inicioDeRuta, FinalDeRuta, CostoDelpasaje, Marca, Modelo, PlacaVehiculo FROM dbo.InfoTransportes";

            try
            {
                using (SqlConnection connection = conexionGeneral.AbrirConexion())
                {
                    using (SqlCommand command = new SqlCommand(queryTopRutas, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            containerRutas.Controls.Clear();
                            bool tieneRegistros = false;

                            while (reader.Read())
                            {
                                tieneRegistros = true;
                                int idRuta = Convert.ToInt32(reader["IdRutaBuses"]);
                                string numeroRuta = reader["NumeroRuta"].ToString();
                                string origen = reader["inicioDeRuta"].ToString();
                                string destino = reader["FinalDeRuta"].ToString();
                                decimal costo = Convert.ToDecimal(reader["CostoDelpasaje"]);
                                string marca = reader["Marca"].ToString();
                                string modelo = reader["Modelo"].ToString();
                                string placa = reader["PlacaVehiculo"].ToString();

                                containerRutas.Controls.Add(GenerarTarjetaComponente(idRuta, numeroRuta, origen, destino, costo, marca, modelo, placa));
                            }

                            if (!tieneRegistros)
                            {
                                MostrarMensajeInformativoPanel(containerRutas, "No se encontraron rutas disponibles en este momento.");
                            }
                        }
                    }
                }
            }
            catch
            {
                MostrarMensajeInformativoPanel(containerRutas, "Error de comunicación con el servidor central de Reise.");
            }
        }

        // VISTA B: DASHBOARD CENTRAL — TODAS LAS RUTAS DISPONIBLES
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

            string queryTodasRutas = "SELECT IdRutaBuses, NumeroRuta, inicioDeRuta, FinalDeRuta, CostoDelpasaje, Marca, Modelo, PlacaVehiculo FROM dbo.InfoTransportes";

            try
            {
                using (SqlConnection connection = conexionGeneral.AbrirConexion())
                {
                    using (SqlCommand command = new SqlCommand(queryTodasRutas, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            gridRutasTotales.Controls.Clear();
                            bool tieneRegistros = false;

                            while (reader.Read())
                            {
                                tieneRegistros = true;
                                int idRuta = Convert.ToInt32(reader["IdRutaBuses"]);
                                string numeroRuta = reader["NumeroRuta"].ToString();
                                string origen = reader["inicioDeRuta"].ToString();
                                string destino = reader["FinalDeRuta"].ToString();
                                decimal costo = Convert.ToDecimal(reader["CostoDelpasaje"]);
                                string marca = reader["Marca"].ToString();
                                string modelo = reader["Modelo"].ToString();
                                string placa = reader["PlacaVehiculo"].ToString();

                                gridRutasTotales.Controls.Add(GenerarTarjetaComponente(idRuta, numeroRuta, origen, destino, costo, marca, modelo, placa));
                            }

                            if (!tieneRegistros)
                            {
                                MostrarMensajeInformativoPanel(gridRutasTotales, "Catálogo de rutas actualmente vacío.");
                            }
                        }
                    }
                }
            }
            catch
            {
                MostrarMensajeInformativoPanel(gridRutasTotales, "Error al conectar con la base de datos de rutas.");
            }
        }

        // Obtiene el saldo real de la tabla InfoCliente de la Base de Datos
        private void ActualizarLabelSaldoPantalla()
        {
            string querySaldo = "SELECT Saldo FROM InfoCliente WHERE IdCliente = @IdCliente";

            try
            {
                using (SqlConnection connection = conexionGeneral.AbrirConexion())
                {
                    using (SqlCommand command = new SqlCommand(querySaldo, connection))
                    {
                        command.Parameters.AddWithValue("@IdCliente", idClienteLogueado);
                        object resultado = command.ExecuteScalar();

                        if (resultado != null && resultado != DBNull.Value)
                        {
                            decimal saldoActual = Convert.ToDecimal(resultado);
                            lblSaldo.Text = $"SALDO: ${saldoActual:F2}";
                        }
                        else
                        {
                            lblSaldo.Text = "SALDO: $0.00";
                        }
                    }
                }
            }
            catch
            {
                lblSaldo.Text = "SALDO: $0.00";
            }
        }

        // Pinta avisos visuales elegantes en los paneles cuando no hay conexión u objetos
        private void MostrarMensajeInformativoPanel(FlowLayoutPanel panel, string mensaje)
        {
            panel.Controls.Clear();
            Label lblMensaje = new Label
            {
                Text = mensaje,
                Font = new Font("Segoe UI", 11, FontStyle.Italic),
                ForeColor = Color.Gray,
                Size = new Size(520, 80),
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(10, 40, 10, 0)
            };
            panel.Controls.Add(lblMensaje);
        }

        // Generación dinámica de tus tarjetas grises con lógica transaccional SQL real
        private Panel GenerarTarjetaComponente(int idRuta, string numeroRuta, string origen, string destino, decimal costo, string marca, string modelo, string placa)
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

            Label lblNombre = new Label
            {
                Text = $"{numeroRuta}: {origen} → {destino}",
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 18),
                Size = new Size(400, 45),
                UseMnemonic = false
            };
            card.Controls.Add(lblNombre);

            Label lblBusDetalle = new Label
            {
                Text = $"Unidad: {marca} {modelo} [{placa}]",
                Font = new Font("Segoe UI", 8.5f, FontStyle.Italic),
                ForeColor = Color.FromArgb(200, 200, 200),
                Location = new Point(20, 68),
                Size = new Size(380, 20)
            };
            card.Controls.Add(lblBusDetalle);

            Label lblPrecio = new Label
            {
                Text = $"${costo:F2}",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(445, 20),
                AutoSize = true
            };
            card.Controls.Add(lblPrecio);

            Button btnComprar = new Button
            {
                Text = "COMPRAR",
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 180, 100),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(95, 28),
                Location = new Point(425, 55),
                Cursor = Cursors.Hand
            };
            btnComprar.FlatAppearance.BorderSize = 0;
            btnComprar.Paint += (s, e) => RecortarBordesControl(btnComprar, 6);

            // EVENTO CLICK: Procesamiento transaccional SQL
            btnComprar.Click += (s, e) =>
            {
                string mensaje = $"¿Deseas comprar 1 pasaje para la {numeroRuta}?\n\n" +
                                 $"De: {origen}\nA: {destino}\n\n" +
                                 $"Costo: ${costo:F2}\nVehículo: {marca} {modelo} ({placa})";

                DialogResult resultadoConfirmacion = MessageBox.Show(mensaje, "Confirmar Compra", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultadoConfirmacion == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connection = conexionGeneral.AbrirConexion())
                        {
                            // 1. Validar fondos del cliente antes de operar
                            string sqlCheckSaldo = "SELECT Saldo FROM InfoCliente WHERE IdCliente = @IdCliente";
                            decimal saldoDisponible = 0;

                            using (SqlCommand cmdCheck = new SqlCommand(sqlCheckSaldo, connection))
                            {
                                cmdCheck.Parameters.AddWithValue("@IdCliente", idClienteLogueado);
                                object resSaldo = cmdCheck.ExecuteScalar();
                                saldoDisponible = resSaldo != null ? Convert.ToDecimal(resSaldo) : 0;
                            }

                            if (saldoDisponible < costo)
                            {
                                MessageBox.Show("Saldo insuficiente para completar la compra. Por favor, recargue su cuenta.", "Reise - Fondos Insuficientes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            // 2. Transacción de compra de pasaje atómica
                            using (SqlTransaction transaction = connection.BeginTransaction())
                            {
                                try
                                {
                                    // A. Descontar saldo del cliente
                                    string sqlDebitar = "UPDATE InfoCliente SET Saldo = Saldo - @Costo WHERE IdCliente = @IdCliente";
                                    using (SqlCommand cmdDebitar = new SqlCommand(sqlDebitar, connection, transaction))
                                    {
                                        cmdDebitar.Parameters.AddWithValue("@Costo", costo);
                                        cmdDebitar.Parameters.AddWithValue("@IdCliente", idClienteLogueado);
                                        cmdDebitar.ExecuteNonQuery();
                                    }

                                    // B. Obtener IdCompraPasajes correlativo
                                    string sqlGetIdCompra = "SELECT ISNULL(MAX(IdCompraPasajes), 0) + 1 FROM CompraPasajes";
                                    int nuevoIdCompra = 0;
                                    using (SqlCommand cmdId = new SqlCommand(sqlGetIdCompra, connection, transaction))
                                    {
                                        nuevoIdCompra = Convert.ToInt32(cmdId.ExecuteScalar());
                                    }

                                    // C. Insertar registro en CompraPasajes
                                    string sqlCompra = "INSERT INTO CompraPasajes (IdCompraPasajes, IdRutaBuses, CantidadAcomprar, TotalApagar) VALUES (@IdCompra, @IdRuta, 1, @Total)";
                                    using (SqlCommand cmdCompra = new SqlCommand(sqlCompra, connection, transaction))
                                    {
                                        cmdCompra.Parameters.AddWithValue("@IdCompra", nuevoIdCompra);
                                        cmdCompra.Parameters.AddWithValue("@IdRuta", idRuta);
                                        cmdCompra.Parameters.AddWithValue("@Total", costo);
                                        cmdCompra.ExecuteNonQuery();
                                    }

                                    // D. Obtener IdDetalleVenta correlativo
                                    string sqlGetIdVenta = "SELECT ISNULL(MAX(IdDetalleVenta), 0) + 1 FROM DetalleVenta";
                                    int nuevoIdVenta = 0;
                                    using (SqlCommand cmdIdV = new SqlCommand(sqlGetIdVenta, connection, transaction))
                                    {
                                        nuevoIdVenta = Convert.ToInt32(cmdIdV.ExecuteScalar());
                                    }

                                    // E. Registrar en DetalleVenta (El trigger 'obtenerfecha' manejará el tiempo en la BD)
                                    string sqlDetalle = "INSERT INTO DetalleVenta (IdDetalleVenta, IdCompraPasajes, IdCliente, IdMetodosDePago, Estado) VALUES (@IdVenta, @IdCompra, @IdCliente, 1, 'Completado')";
                                    using (SqlCommand cmdDetalle = new SqlCommand(sqlDetalle, connection, transaction))
                                    {
                                        cmdDetalle.Parameters.AddWithValue("@IdVenta", nuevoIdVenta);
                                        cmdDetalle.Parameters.AddWithValue("@IdCompra", nuevoIdCompra);
                                        cmdDetalle.Parameters.AddWithValue("@IdCliente", idClienteLogueado);
                                        cmdDetalle.ExecuteNonQuery();
                                    }

                                    // Confirmamos todos los cambios juntos
                                    transaction.Commit();
                                    MessageBox.Show("¡Pasaje adquirido con éxito! Buen viaje.", "Reise", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    // Actualizar el saldo inmediatamente reflejado en el Navbar
                                    ActualizarLabelSaldoPantalla();
                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Error interno al procesar la compra: " + ex.Message, "Error de Transacción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo conectar a la central de Reise: " + ex.Message, "Error de Red/SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };

            card.Controls.Add(btnComprar);
            return card;
        }

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

        private void BtnSalirLogin_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 login = new Form1();
                login.Show();
                this.Hide();
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