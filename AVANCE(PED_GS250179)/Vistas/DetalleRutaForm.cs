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
        // Atributos de sesión y ruta (Modelo Reise2go)
        private int idClienteLogueado;
        private int idRutaBusesSeleccionada;
        private decimal costoDelPasaje;
        private string numeroRuta;
        private string inicioRecorrido;
        private string finalRecorrido;

        // Elementos de la Interfaz (Renderizados a mano con GDI+)
        private Label lblHeaderTitulo;
        private Label lblHeaderSubtitulo;
        private Label lblNumeroRuta;
        private Label lblItinerario;
        private Panel pnlContenedorGrafo; // Panel donde se pinta el grafo real de la ruta
        private Label lblPrecio;
        private Label lblNotaInformativa;
        private Button btnComprarTicket; // El verde vibrante
        private Button btnCancelar; // El rojo llamativo

        // Constructor adaptado
        public DetalleRutaForm(int idCliente, int idRuta, string numeroRuta, string inicio, string final, decimal costo)
        {
            this.idClienteLogueado = idCliente;
            this.idRutaBusesSeleccionada = idRuta;
            this.numeroRuta = numeroRuta;
            this.inicioRecorrido = inicio;
            this.finalRecorrido = final;
            this.costoDelPasaje = costo;

            // Altura de 610 px optimizada para albergar el componente gráfico
            this.Size = new Size(500, 610);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            InicializarInterfazEstilizada();
        }

        private void InicializarInterfazEstilizada()
        {
            // ==========================================
            // 1. PANEL SUPERIOR DE ENCABEZADO (Header)
            // ==========================================
            Panel pnlHeader = new Panel { Size = new Size(500, 80), Location = new Point(0, 0), BackColor = Color.FromArgb(245, 247, 250) };

            lblHeaderTitulo = new Label
            {
                Text = "REISE",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204), // Azul reise
                Location = new Point(25, 20),
                AutoSize = true
            };

            lblHeaderSubtitulo = new Label
            {
                Text = "| Detalles de Facturación y Ruta",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(70, 70, 70), // Gris oscuro legible
                Location = new Point(125, 21),
                AutoSize = true
            };

            pnlHeader.Controls.Add(lblHeaderTitulo);
            pnlHeader.Controls.Add(lblHeaderSubtitulo);
            this.Controls.Add(pnlHeader);

            // ==========================================
            // 2. DETALLES CENTRALES DE LA RUTA
            // ==========================================
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

            // ==========================================
            // 3. PANEL CONTENEDOR DEL GRAFO DINÁMICO
            // ==========================================
            pnlContenedorGrafo = new Panel { Size = new Size(450, 85), Location = new Point(25, 200), BackColor = Color.FromArgb(252, 253, 255) };
            pnlContenedorGrafo.Paint += (s, e) => DibujarBordeSuaveControl(pnlContenedorGrafo, e.Graphics, Color.FromArgb(210, 225, 240), 1, 8);
            pnlContenedorGrafo.Paint += PnlContenedorGrafo_Paint; // Vinculamos el motor GDI+
            this.Controls.Add(pnlContenedorGrafo);

            // ==========================================
            // 4. CONTENEDORES DE PAGO Y ACCIONES
            // ==========================================
            // Panel de Tarifa
            Panel pnlPrecioFondo = new Panel { Size = new Size(450, 70), Location = new Point(25, 300), BackColor = Color.FromArgb(248, 249, 250) };
            pnlPrecioFondo.Paint += (s, e) => DibujarBordeSuaveControl(pnlPrecioFondo, e.Graphics, Color.FromArgb(222, 226, 230), 1, 8);

            Label lblTextoPrecio = new Label { Text = "PRECIO UNITARIO:", Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), ForeColor = Color.FromArgb(100, 100, 100), Location = new Point(15, 26), AutoSize = true };

            lblPrecio = new Label
            {
                Text = $"${costoDelPasaje:F2}",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 167, 69), // Verde éxito
                Location = new Point(335, 18),
                AutoSize = true
            };
            pnlPrecioFondo.Controls.Add(lblTextoPrecio);
            pnlPrecioFondo.Controls.Add(lblPrecio);
            this.Controls.Add(pnlPrecioFondo);

            // Nota Informativa
            lblNotaInformativa = new Label
            {
                Text = "📌 Nota para el cliente:\nVerifique que su recorrido seleccionado sea el correcto en el mapa relacional superior. Al presionar el botón Comprar, el sistema validará informáticamente su saldo e ingresará la petición en la cola lineal interna.",
                Font = new Font("Segoe UI", 8.5f, FontStyle.Italic),
                ForeColor = Color.Gray,
                Location = new Point(25, 385),
                Size = new Size(450, 55),
                AutoSize = false
            };
            this.Controls.Add(lblNotaInformativa);

            // Botón: COMPRAR PASAJE
            btnComprarTicket = new Button
            {
                Text = "COMPRAR PASAJE",
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

            // Botón: CANCELAR
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

        // ============================================================
        // LÓGICA DE DIBUJO COMPLETA DE VÉRTICES Y ARISTAS (GDI+)
        // ============================================================
        private void PnlContenedorGrafo_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Procesamos el itinerario de forma dinámica basándonos en tu string de Origen y Destino
            string[] paradasRuta = { inicioRecorrido, "Control", finalRecorrido };
            int totalNodos = paradasRuta.Length;

            // Calculamos la distribución proporcional horizontal dentro del panel
            int rangoAncho = pnlContenedorGrafo.Width - 100;
            int espacioX = rangoAncho / (totalNodos - 1);

            Point[] posicionesNodos = new Point[totalNodos];
            for (int i = 0; i < totalNodos; i++)
            {
                posicionesNodos[i] = new Point(50 + (i * espacioX), 35);
            }

            // 1. Renderizar Aristas/Líneas de conexión del Grafo
            using (Pen lapizArista = new Pen(Color.FromArgb(0, 102, 204), 3))
            {
                lapizArista.CustomEndCap = new AdjustableArrowCap(5, 5); // Flecha direccional

                for (int i = 0; i < totalNodos - 1; i++)
                {
                    g.DrawLine(lapizArista,
                               posicionesNodos[i].X + 14, posicionesNodos[i].Y,
                               posicionesNodos[i + 1].X - 14, posicionesNodos[i + 1].Y);
                }
            }

            // 2. Renderizar Vértices (Círculos lógicos)
            for (int i = 0; i < totalNodos; i++)
            {
                string idNodo = (i == 0) ? "A" : (i == totalNodos - 1) ? "B" : "ℹ";

                // Color corporativo diferenciado por tipo de nodo
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

            using (SolidBrush pincelFondo = new SolidBrush(colorNodo))
            {
                g.FillEllipse(pincelFondo, rectCirculo);
            }

            using (Pen lapizBorde = new Pen(Color.White, 2))
            {
                g.DrawEllipse(lapizBorde, rectCirculo);
            }

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

        // Lógica transaccional de Base de Datos con la Cola Estructural
        private void BtnComprarTicket_Click(object sender, EventArgs e)
        {
            DialogResult confirmacion = MessageBox.Show("¿Está seguro de que desea realizar el cobro e inscribir esta compra?",
                                                  "Confirmar Pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion != DialogResult.Yes) return;

            ColaCompraManual colaFilaUnica = new ColaCompraManual();
            colaFilaUnica.Encolar(idRutaBusesSeleccionada, costoDelPasaje, numeroRuta);

            NodoCompra elementoAProcesar = colaFilaUnica.Desencolar();
            if (elementoAProcesar == null) return;

            Conexion con = new Conexion();
            SqlConnection cn = con.AbrirConexion();
            if (cn == null) return;

            SqlTransaction transaccionCentral = cn.BeginTransaction();

            try
            {
                decimal saldoDisponible = 0.00m;
                string querySaldo = "SELECT Saldo FROM InfoCliente WHERE IdCliente = @IdCliente";
                using (SqlCommand cmdSaldo = new SqlCommand(querySaldo, cn, transaccionCentral))
                {
                    cmdSaldo.Parameters.AddWithValue("@IdCliente", idClienteLogueado);
                    object result = cmdSaldo.ExecuteScalar();
                    saldoDisponible = result != null ? Convert.ToDecimal(result) : 0.00m;
                }

                if (saldoDisponible < elementoAProcesar.Precio)
                {
                    MessageBox.Show($"Operación rechazada. Saldo insuficiente.\nCosto del viaje: ${elementoAProcesar.Precio:F2}\nSaldo disponible: ${saldoDisponible:F2}\n\nPor favor, recargue su cuenta.",
                                    "Fondos Insuficientes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    transaccionCentral.Rollback();
                    return;
                }

                string queryDebito = "UPDATE InfoCliente SET Saldo = Saldo - @Costo WHERE IdCliente = @IdCliente";
                using (SqlCommand cmdDebito = new SqlCommand(queryDebito, cn, transaccionCentral))
                {
                    cmdDebito.Parameters.AddWithValue("@Costo", elementoAProcesar.Precio);
                    cmdDebito.Parameters.AddWithValue("@IdCliente", idClienteLogueado);
                    cmdDebito.ExecuteNonQuery();
                }

                int nuevoIdCompra = 1;
                string queryMaxCompra = "SELECT ISNULL(MAX(IdCompraPasajes), 0) + 1 FROM CompraPasajes";
                using (SqlCommand cmdMaxC = new SqlCommand(queryMaxCompra, cn, transaccionCentral))
                {
                    nuevoIdCompra = Convert.ToInt32(cmdMaxC.ExecuteScalar());
                }

                string queryCompra = "INSERT INTO CompraPasajes (IdCompraPasajes, IdRutaBuses, CantidadAComprar, TotalApagar) VALUES (@IdCompra, @IdRuta, 1, @Total)";
                using (SqlCommand cmdCompra = new SqlCommand(queryCompra, cn, transaccionCentral))
                {
                    cmdCompra.Parameters.AddWithValue("@IdCompra", nuevoIdCompra);
                    cmdCompra.Parameters.AddWithValue("@IdRuta", elementoAProcesar.IdRuta);
                    cmdCompra.Parameters.AddWithValue("@Total", elementoAProcesar.Precio);
                    cmdCompra.ExecuteNonQuery();
                }

                int nuevoIdDetalle = 1;
                string queryMaxDetalle = "SELECT ISNULL(MAX(IdDetalleVenta), 0) + 1 FROM DetalleVenta";
                using (SqlCommand cmdMaxD = new SqlCommand(queryMaxDetalle, cn, transaccionCentral))
                {
                    nuevoIdDetalle = Convert.ToInt32(cmdMaxD.ExecuteScalar());
                }

                string queryDetalle = "INSERT INTO DetalleVenta (IdDetalleVenta, IdCompraPasajes, IdCliente, IdMetodosDePago, Hora, Estado) VALUES (@IdDetalle, @IdCompra, @IdCliente, 1, GETDATE(), 'Aprobado')";
                using (SqlCommand cmdDetalle = new SqlCommand(queryDetalle, cn, transaccionCentral))
                {
                    cmdDetalle.Parameters.AddWithValue("@IdDetalle", nuevoIdDetalle);
                    cmdDetalle.Parameters.AddWithValue("@IdCompra", nuevoIdCompra);
                    cmdDetalle.Parameters.AddWithValue("@IdCliente", idClienteLogueado);
                    cmdDetalle.ExecuteNonQuery();
                }

                transaccionCentral.Commit();

                MessageBox.Show($"¡Pago del pasaje registrado exitosamente!\nBuen viaje en la {elementoAProcesar.NombreRuta}.",
                                "Viaje Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                transaccionCentral.Rollback();
                MessageBox.Show("Fallo interno fatal al procesar el nodo de compra: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.CerrarConexion(cn);
            }
        }

        // Dibujar Sombra Exterior
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

        // Recortar Bordes de Botones
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

        // Dibujar contorno de paneles
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