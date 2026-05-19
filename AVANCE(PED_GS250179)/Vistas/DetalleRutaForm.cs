using AVANCE_PED_GS250179_.Datos;
using AVANCE_PED_GS250179_.Estructuras;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_.Vistas
{
    public partial class DetalleRutaForm : Form
    {
        private int idClienteLogueado;
        private int idRutaBusesSeleccionada;
        private decimal costoDelPasaje;
        private string numeroRuta;
        private string inicioRecorrido;
        private string finalRecorrido;

        private Label lblHeaderTitulo;
        private Label lblHeaderSubtitulo;
        private Label lblNumeroRuta;
        private Label lblItinerario;
        private Panel pnlContenedorGrafo;
        private Label lblPrecio;
        private Label lblNotaInformativa;
        private Button btnComprarTicket;
        private Button btnCancelar;

        public DetalleRutaForm(int idCliente, int idRuta, string numeroRuta, string inicio, string final, decimal costo)
        {
            this.idClienteLogueado = idCliente;
            this.idRutaBusesSeleccionada = idRuta;
            this.numeroRuta = numeroRuta;
            this.inicioRecorrido = inicio;
            this.finalRecorrido = final;
            this.costoDelPasaje = costo;

            this.Size = new Size(500, 610);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            InicializarInterfazEstilizada();
        }

        private void InicializarInterfazEstilizada()
        {
            Panel pnlHeader = new Panel { Size = new Size(500, 80), Location = new Point(0, 0), BackColor = Color.FromArgb(245, 247, 250) };

            lblHeaderTitulo = new Label
            {
                Text = "REISE",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204),
                Location = new Point(25, 20),
                AutoSize = true
            };

            lblHeaderSubtitulo = new Label
            {
                Text = "| Detalles de Facturación y Ruta",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(70, 70, 70),
                Location = new Point(125, 21),
                AutoSize = true
            };

            pnlHeader.Controls.Add(lblHeaderTitulo);
            pnlHeader.Controls.Add(lblHeaderSubtitulo);
            this.Controls.Add(pnlHeader);

            lblNumeroRuta = new Label
            {
                Text = numeroRuta.ToUpper(),
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                Location = new Point(25, 100),
                AutoSize = true
            };
            this.Controls.Add(lblNumeroRuta);

            lblItinerario = new Label
            {
                Text = $"📍 RECORRIDO DE LA UNIDAD:\nDesde: {inicioRecorrido}\nHasta: {finalRecorrido}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(120, 120, 120),
                Location = new Point(25, 145),
                Size = new Size(450, 45),
                AutoSize = false
            };
            this.Controls.Add(lblItinerario);

            pnlContenedorGrafo = new Panel { Size = new Size(450, 85), Location = new Point(25, 200), BackColor = Color.FromArgb(252, 253, 255) };
            pnlContenedorGrafo.Paint += (s, e) => DibujarBordeSuaveControl(pnlContenedorGrafo, e.Graphics, Color.FromArgb(210, 225, 240), 1, 8);
            pnlContenedorGrafo.Paint += PnlContenedorGrafo_Paint;
            this.Controls.Add(pnlContenedorGrafo);

            Panel pnlPrecioFondo = new Panel { Size = new Size(450, 70), Location = new Point(25, 300), BackColor = Color.FromArgb(248, 249, 250) };
            pnlPrecioFondo.Paint += (s, e) => DibujarBordeSuaveControl(pnlPrecioFondo, e.Graphics, Color.FromArgb(222, 226, 230), 1, 8);

            Label lblTextoPrecio = new Label { Text = "PRECIO UNITARIO:", Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), ForeColor = Color.FromArgb(100, 100, 100), Location = new Point(15, 26), AutoSize = true };

            lblPrecio = new Label
            {
                Text = $"${costoDelPasaje:F2}",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 167, 69),
                Location = new Point(335, 18),
                AutoSize = true
            };
            pnlPrecioFondo.Controls.Add(lblTextoPrecio);
            pnlPrecioFondo.Controls.Add(lblPrecio);
            this.Controls.Add(pnlPrecioFondo);

            lblNotaInformativa = new Label
            {
                Text = "📌 Nota para el cliente:\nAl presionar comprar, su boleto se generará con un código QR único y se enviará a la cola de tránsito. El conductor del autobús validará su acceso y actualizará el grafo de abordaje.",
                Font = new Font("Segoe UI", 8.5f, FontStyle.Italic),
                ForeColor = Color.Gray,
                Location = new Point(25, 385),
                Size = new Size(450, 55),
                AutoSize = false
            };
            this.Controls.Add(lblNotaInformativa);

            btnComprarTicket = new Button
            {
                Text = "COMPRAR PASAJE Y GENERAR QR",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(450, 42),
                Location = new Point(25, 460),
                Cursor = Cursors.Hand
            };
            btnComprarTicket.FlatAppearance.BorderSize = 0;
            btnComprarTicket.Paint += (s, e) => RecortarBordesControl(btnComprarTicket, 6);
            btnComprarTicket.Click += BtnComprarTicket_Click;
            this.Controls.Add(btnComprarTicket);

            btnCancelar = new Button
            {
                Text = "CANCELAR Y VOLVER AL MENÚ",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(450, 35),
                Location = new Point(25, 515),
                Cursor = Cursors.Hand
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Paint += (s, e) => RecortarBordesControl(btnCancelar, 6);
            btnCancelar.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            this.Controls.Add(btnCancelar);
        }

        private void PnlContenedorGrafo_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            string[] paradasRuta = { inicioRecorrido, "Control", finalRecorrido };
            int totalNodos = paradasRuta.Length;

            int rangoAncho = pnlContenedorGrafo.Width - 100;
            int espacioX = rangoAncho / (totalNodos - 1);

            Point[] posicionesNodos = new Point[totalNodos];
            for (int i = 0; i < totalNodos; i++)
            {
                posicionesNodos[i] = new Point(50 + (i * espacioX), 35);
            }

            using (Pen lapizArista = new Pen(Color.FromArgb(0, 102, 204), 3))
            {
                lapizArista.CustomEndCap = new AdjustableArrowCap(5, 5);

                for (int i = 0; i < totalNodos - 1; i++)
                {
                    g.DrawLine(lapizArista,
                               posicionesNodos[i].X + 14, posicionesNodos[i].Y,
                               posicionesNodos[i + 1].X - 14, posicionesNodos[i + 1].Y);
                }
            }

            for (int i = 0; i < totalNodos; i++)
            {
                string idNodo = (i == 0) ? "A" : (i == totalNodos - 1) ? "B" : "ℹ";

                Color colorNodo = (i == 0) ? Color.FromArgb(0, 102, 204) :
                                  (i == totalNodos - 1) ? Color.FromArgb(40, 167, 69) :
                                  Color.LightSlateGray;

                DibujarVerticeGrafo(g, posicionesNodos[i], idNodo, paradasRuta[i], colorNodo);
            }
        }

        private void DibujarVerticeGrafo(Graphics g, Point centro, string idNodo, string ubicacion, Color colorNodo)
        {
            int radio = 14;
            Rectangle rectCirculo = new Rectangle(centro.X - radio, centro.Y - radio, radio * 2, radio * 2);

            using (SolidBrush pincelFondo = new SolidBrush(colorNodo)) { g.FillEllipse(pincelFondo, rectCirculo); }
            using (Pen lapizBorde = new Pen(Color.White, 2)) { g.DrawEllipse(lapizBorde, rectCirculo); }

            using (SolidBrush pincelTextoNodo = new SolidBrush(Color.White))
            {
                Font fuenteNodo = new Font("Segoe UI", 9, FontStyle.Bold);
                StringFormat formatoCentrado = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString(idNodo, fuenteNodo, pincelTextoNodo, centro, formatoCentrado);
            }

            using (SolidBrush pincelUbicacion = new SolidBrush(Color.FromArgb(60, 64, 67)))
            {
                Font fuenteUbicacion = new Font("Segoe UI", 7.5f, FontStyle.Bold);
                StringFormat formatoEtiqueta = new StringFormat { Alignment = StringAlignment.Center };

                string textoCorto = ubicacion.Length > 15 ? ubicacion.Substring(0, 12) + "..." : ubicacion;
                g.DrawString(textoCorto, fuenteUbicacion, pincelUbicacion, new Point(centro.X, centro.Y + 18), formatoEtiqueta);
            }
        }

        private void BtnComprarTicket_Click(object sender, EventArgs e)
        {
            DialogResult confirmacion = MessageBox.Show("¿Está seguro de que desea comprar este boleto? Se guardará en la cola de abordaje y se generará su código QR.",
                                              "Confirmar Compra", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion != DialogResult.Yes) return;

            Conexion con = new Conexion();
            SqlConnection cn = con.AbrirConexion();
            if (cn == null) return;

            SqlTransaction transaccionCentral = cn.BeginTransaction();

            try
            {
                // 1. Validar saldo del cliente
                decimal saldoDisponible = 0.00m;
                string querySaldo = "SELECT Saldo FROM InfoCliente WHERE IdCliente = @IdCliente";
                using (SqlCommand cmdSaldo = new SqlCommand(querySaldo, cn, transaccionCentral))
                {
                    cmdSaldo.Parameters.AddWithValue("@IdCliente", idClienteLogueado);
                    object result = cmdSaldo.ExecuteScalar();
                    saldoDisponible = result != null ? Convert.ToDecimal(result) : 0.00m;
                }

                if (saldoDisponible < costoDelPasaje)
                {
                    MessageBox.Show($"Operación rechazada. Saldo insuficiente.\nCosto del viaje: ${costoDelPasaje:F2}\nSaldo disponible: ${saldoDisponible:F2}",
                                    "Fondos Insuficientes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    transaccionCentral.Rollback();
                    return;
                }

                // 2. Descontar Saldo
                string queryDebito = "UPDATE InfoCliente SET Saldo = Saldo - @Costo WHERE IdCliente = @IdCliente";
                using (SqlCommand cmdDebito = new SqlCommand(queryDebito, cn, transaccionCentral))
                {
                    cmdDebito.Parameters.AddWithValue("@Costo", costoDelPasaje);
                    cmdDebito.Parameters.AddWithValue("@IdCliente", idClienteLogueado);
                    cmdDebito.ExecuteNonQuery();
                }

                // 3. Obtener e Insertar Cabecera de Compra
                int nuevoIdCompra = 1;
                string queryMaxCompra = "SELECT ISNULL(MAX(IdCompraPasajes), 0) + 1 FROM CompraPasajes";
                using (SqlCommand cmdMaxC = new SqlCommand(queryMaxCompra, cn, transaccionCentral)) { nuevoIdCompra = Convert.ToInt32(cmdMaxC.ExecuteScalar()); }

                string queryCompra = "INSERT INTO CompraPasajes (IdCompraPasajes, IdRutaBuses, CantidadAComprar, TotalApagar) VALUES (@IdCompra, @IdRuta, 1, @Total)";
                using (SqlCommand cmdCompra = new SqlCommand(queryCompra, cn, transaccionCentral))
                {
                    cmdCompra.Parameters.AddWithValue("@IdCompra", nuevoIdCompra);
                    cmdCompra.Parameters.AddWithValue("@IdRuta", idRutaBusesSeleccionada);
                    cmdCompra.Parameters.AddWithValue("@Total", costoDelPasaje);
                    cmdCompra.ExecuteNonQuery();
                }

                // 4. Obtener e Insertar DetalleVenta (Estado Pendiente)
                int nuevoIdDetalle = 1;
                string queryMaxDetalle = "SELECT ISNULL(MAX(IdDetalleVenta), 0) + 1 FROM DetalleVenta";
                using (SqlCommand cmdMaxD = new SqlCommand(queryMaxDetalle, cn, transaccionCentral)) { nuevoIdDetalle = Convert.ToInt32(cmdMaxD.ExecuteScalar()); }

                string queryDetalle = "INSERT INTO DetalleVenta (IdDetalleVenta, IdCompraPasajes, IdCliente, IdMetodosDePago, Hora, Estado) " +
                                      "VALUES (@IdDetalle, @IdCompra, @IdCliente, 1, GETDATE(), 'Pendiente')";
                using (SqlCommand cmdDetalle = new SqlCommand(queryDetalle, cn, transaccionCentral))
                {
                    cmdDetalle.Parameters.AddWithValue("@IdDetalle", nuevoIdDetalle);
                    cmdDetalle.Parameters.AddWithValue("@IdCompra", nuevoIdCompra);
                    cmdDetalle.Parameters.AddWithValue("@IdCliente", idClienteLogueado);
                    cmdDetalle.ExecuteNonQuery();
                }

                // Confirmamos los cambios en la Base de Datos
                transaccionCentral.Commit();

                // =========================================================================
                // 🌟 ENCOLAR EN MEMORIA RAM: Enviamos el nuevoIdDetalle relacional de SQL
                // =========================================================================
                ColaCompraManual.InstanciaCompartida.Encolar(
                    idRutaBusesSeleccionada,
                    costoDelPasaje,
                    numeroRuta,
                    nuevoIdDetalle // <-- Pasado con éxito al constructor del nodo
                );

                // 5. Popup Visor de QR síncrono (El texto se genera de forma temporal al vuelo)
                string datosEstructuradosQR = $"REISE_TICKET|ID:{nuevoIdDetalle}|Ruta:{numeroRuta}|Monto:{costoDelPasaje}";
                MostrarPopupQR(datosEstructuradosQR, nuevoIdDetalle, numeroRuta);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                if (transaccionCentral.Connection != null) transaccionCentral.Rollback();
                MessageBox.Show("Error fatal al procesar el ticket QR: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.CerrarConexion(cn);
            }
        }

        private void MostrarPopupQR(string datosQR, int idDetalle, string rName)
        {
            Form frmPopupQR = new Form { Size = new Size(320, 400), Text = "Tu Pase de Abordaje QR", StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedToolWindow, BackColor = Color.White };
            PictureBox picBoxQR = new PictureBox { Dock = DockStyle.Top, Height = 260, SizeMode = PictureBoxSizeMode.Zoom };

            try
            {
                string urlSegura = "https://api.qrserver.com/v1/create-qr-code/?size=250x250&data=" + Uri.EscapeDataString(datosQR);
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    byte[] imageBytes = webClient.DownloadData(urlSegura);
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBytes)) { picBoxQR.Image = Image.FromStream(ms); }
                }
            }
            catch
            {
                Bitmap bmpError = new Bitmap(250, 250);
                using (Graphics g = Graphics.FromImage(bmpError))
                {
                    g.Clear(Color.FromArgb(240, 240, 240));
                    g.DrawString("Boleto Registrado\n[Falta conexión para QR]", new Font("Segoe UI", 10, FontStyle.Bold), Brushes.DarkRed, new RectangleF(0, 0, 250, 250), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }
                picBoxQR.Image = bmpError;
            }

            Label lblInstruccion = new Label { Text = $"Boleto #{idDetalle} - Ruta {rName}\nMuestre este QR al conductor al subir.", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), TextAlign = ContentAlignment.MiddleCenter, ForeColor = Color.FromArgb(50, 50, 50) };
            frmPopupQR.Controls.Add(lblInstruccion); frmPopupQR.Controls.Add(picBoxQR);
            frmPopupQR.ShowDialog();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int shadowDepth = 15;
            for (int i = shadowDepth; i > 0; i--)
            {
                using (Pen pen = new Pen(Color.FromArgb(50 / (i / 2 + 1), Color.Black), i))
                {
                    e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, this.Width, this.Height));
                }
            }

            using (Pen borderPen = new Pen(Color.FromArgb(220, 220, 220), 2))
            {
                e.Graphics.DrawRectangle(borderPen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }

        private void RecortarBordesControl(Control c, int radioBorde)
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, c.Width, c.Height);
            int diameter = radioBorde * 2;
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            c.Region = new Region(path);
        }

        private void DibujarBordeSuaveControl(Control c, Graphics g, Color color, int thickness, int radio)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, radio, radio, 180, 90);
                path.AddArc(c.Width - radio - 1, 0, radio, radio, 270, 90);
                path.AddArc(c.Width - radio - 1, c.Height - radio - 1, radio, radio, 0, 90);
                path.AddArc(0, c.Height - radio - 1, radio, radio, 90, 90);
                path.CloseAllFigures();
                using (Pen pen = new Pen(color, thickness)) { g.DrawPath(pen, path); }
            }
        }
    }
}