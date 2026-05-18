using AVANCE_PED_GS250179_.Datos;
using AVANCE_PED_GS250179_.Estructuras;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using AVANCE_PED_GS250179_.Estructuras;

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

            // CORREGIDO: Altura incrementada para evitar que el texto de la nota se corte
            this.Size = new Size(500, 520);
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
            // CORREGIDO: Altura ajustada para contener ambos textos cómodamente
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
                Location = new Point(125, 21), // Alineado a la par de REISE
                AutoSize = true
            };

            pnlHeader.Controls.Add(lblHeaderTitulo);
            pnlHeader.Controls.Add(lblHeaderSubtitulo);
            this.Controls.Add(pnlHeader);

            // ==========================================
            // 2. DETALLES CENTRALES DE LA RUTA
            // ==========================================
            // Identificador Grande de la Ruta (Ej: RUTA 42)
            lblNumeroRuta = new Label
            {
                Text = numeroRuta.ToUpper(),
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41), // Gris muy oscuro casi negro
                Location = new Point(25, 110),
                AutoSize = true
            };
            this.Controls.Add(lblNumeroRuta);

            // Trayecto Detallado Origen -> Destino
            lblItinerario = new Label
            {
                // CORREGIDO: Texto formateado exactamente como la captura
                Text = $"📍 RECORRIDO DE LA UNIDAD:\nDesde: {inicioRecorrido}\nHasta: {finalRecorrido}",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(120, 120, 120), // Gris medio para subtítulos
                Location = new Point(25, 160),
                Size = new Size(450, 60), // Alto suficiente para las 3 líneas
                AutoSize = false
            };
            this.Controls.Add(lblItinerario);

            // Contenedor Estilizado para resaltar la Tarifa
            Panel pnlPrecioFondo = new Panel { Size = new Size(450, 75), Location = new Point(25, 230), BackColor = Color.FromArgb(248, 249, 250) };
            pnlPrecioFondo.Paint += (s, e) => DibujarBordeSuaveControl(pnlPrecioFondo, e.Graphics, Color.FromArgb(222, 226, 230), 1, 8);

            Label lblTextoPrecio = new Label { Text = "PRECIO UNITARIO:", Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), ForeColor = Color.FromArgb(100, 100, 100), Location = new Point(15, 28), AutoSize = true };

            lblPrecio = new Label
            {
                Text = $"${costoDelPasaje:F2}",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 167, 69), // Verde vibrante de compra
                Location = new Point(335, 20),
                AutoSize = true
            };
            pnlPrecioFondo.Controls.Add(lblTextoPrecio);
            pnlPrecioFondo.Controls.Add(lblPrecio);
            this.Controls.Add(pnlPrecioFondo);

            // ==========================================
            // 3. NOTA INFORMATIVA Y BOTONES DE ACCIÓN
            // ==========================================
            // CORREGIDO: Tamaño (Size) y AutoSize ajustados para garantizar que NINGÚN texto se corte Kenneth
            lblNotaInformativa = new Label
            {
                Text = "📌 Nota para el cliente:\nVerifique que su recorrido seleccionado sea el correcto. Al presionar el botón Comprar, el sistema validará informáticamente su saldo e ingresará la petición en la cola lineal interna.",
                Font = new Font("Segoe UI", 8.5f, FontStyle.Italic),
                ForeColor = Color.Gray,
                Location = new Point(25, 320), // Movido más abajo para dar aire
                Size = new Size(450, 65), // ALTO INCREMENTADO PARA EVITAR EL CORTE
                AutoSize = false // ESTO ES CLAVE para el multiline
            };
            this.Controls.Add(lblNotaInformativa);

            // Botón de Acción Principal: COMPRAR PASAJE (Verde vibrante que resalta)
            btnComprarTicket = new Button
            {
                Text = "COMPRAR PASAJE",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(40, 167, 69), // Verde Compra Vibrante
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(450, 42),
                Location = new Point(25, 400),
                Cursor = Cursors.Hand
            };
            btnComprarTicket.FlatAppearance.BorderSize = 0;
            // CORREGIDO: Borde redondeado suave para el botón
            btnComprarTicket.Paint += (s, e) => RecortarBordesControl(btnComprarTicket, 6);
            btnComprarTicket.Click += BtnComprarTicket_Click;
            this.Controls.Add(btnComprarTicket);

            // Botón: Cancelar (Rojo llamativo bien visible)
            btnCancelar = new Button
            {
                Text = "CANCELAR Y VOLVER AL MENÚ",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(220, 53, 69), // Rojo Cancelación/Peligro
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(450, 35),
                Location = new Point(25, 450),
                Cursor = Cursors.Hand
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            // CORREGIDO: Borde redondeado suave para el botón
            btnCancelar.Paint += (s, e) => RecortarBordesControl(btnCancelar, 6);
            btnCancelar.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            this.Controls.Add(btnCancelar);
        }

        // Lógica de Negocio empleando la Cola Manual Kenneth
        private void BtnComprarTicket_Click(object sender, EventArgs e)
        {
            // Confirmación extra de seguridad
            DialogResult confirmacion = MessageBox.Show("¿Está seguro de que desea realizar el cobro e inscribir esta compra?",
                                                  "Confirmar Pago", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion != DialogResult.Yes) return;

            // 1. Inicializamos la Cola Manual creada desde cero
            ColaCompraManual colaFilaUnica = new ColaCompraManual();

            // 2. Metemos los datos al Rear (final) de la cola manual
            colaFilaUnica.Encolar(idRutaBusesSeleccionada, costoDelPasaje, numeroRuta);

            // 3. Desencolamos del Front (frente) Kenneth para aislar el nodo a procesar en la BD
            NodoCompra elementoAProcesar = colaFilaUnica.Desencolar();
            if (elementoAProcesar == null) return;

            Conexion con = new Conexion();
            SqlConnection cn = con.AbrirConexion();
            if (cn == null) return;

            SqlTransaction transaccionCentral = cn.BeginTransaction();

            try
            {
                // PASO A: Comprobar saldo en InfoCliente Kenneth
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

                // PASO B: Restar saldo Kenneth
                string queryDebito = "UPDATE InfoCliente SET Saldo = Saldo - @Costo WHERE IdCliente = @IdCliente";
                using (SqlCommand cmdDebito = new SqlCommand(queryDebito, cn, transaccionCentral))
                {
                    cmdDebito.Parameters.AddWithValue("@Costo", elementoAProcesar.Precio);
                    cmdDebito.Parameters.AddWithValue("@IdCliente", idClienteLogueado);
                    cmdDebito.ExecuteNonQuery();
                }

                // PASO C: Autoincrementar ID CompraPasajes Kenneth
                int nuevoIdCompra = 1;
                string queryMaxCompra = "SELECT ISNULL(MAX(IdCompraPasajes), 0) + 1 FROM CompraPasajes";
                using (SqlCommand cmdMaxC = new SqlCommand(queryMaxCompra, cn, transaccionCentral))
                {
                    nuevoIdCompra = Convert.ToInt32(cmdMaxC.ExecuteScalar());
                }

                // PASO D: Insertar CompraPasajes Kenneth
                string queryCompra = "INSERT INTO CompraPasajes (IdCompraPasajes, IdRutaBuses, CantidadAComprar, TotalApagar) VALUES (@IdCompra, @IdRuta, 1, @Total)";
                using (SqlCommand cmdCompra = new SqlCommand(queryCompra, cn, transaccionCentral))
                {
                    cmdCompra.Parameters.AddWithValue("@IdCompra", nuevoIdCompra);
                    cmdCompra.Parameters.AddWithValue("@IdRuta", elementoAProcesar.IdRuta);
                    cmdCompra.Parameters.AddWithValue("@Total", elementoAProcesar.Precio);
                    cmdCompra.ExecuteNonQuery();
                }

                // PASO E: Autoincrementar ID DetalleVenta Kenneth
                int nuevoIdDetalle = 1;
                string queryMaxDetalle = "SELECT ISNULL(MAX(IdDetalleVenta), 0) + 1 FROM DetalleVenta";
                using (SqlCommand cmdMaxD = new SqlCommand(queryMaxDetalle, cn, transaccionCentral))
                {
                    nuevoIdDetalle = Convert.ToInt32(cmdMaxD.ExecuteScalar());
                }

                // PASO F: Insertar DetalleVenta (Metodo Pago 1 = Tarjeta Reise Kenneth)
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

                this.DialogResult = DialogResult.OK; // Indica éxito al formulario padre Kenneth
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

        // ==========================================
        // MÉTODOS DE RENDERIZADO AVANZADO (GDI+) Kenneth
        // ==========================================

        // 1. DIBUJAR SOMBRA EXTERIOR PARA QUE EL FORMULARIO RESALTE Kenneth
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Sombra exterior difuminada Kenneth (para que el Form resalte del fondo)
            int shadowDepth = 15; // Profundidad de la sombra
            for (int i = shadowDepth; i > 0; i--)
            {
                // Un gris suave que se difumina hacia afuera Kenneth
                using (Pen pen = new Pen(Color.FromArgb(50 / (i / 2 + 1), Color.Black), i))
                {
                    e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, this.Width, this.Height));
                }
            }

            // Borde exterior suave del formulario principal Kenneth
            using (Pen borderPen = new Pen(Color.FromArgb(220, 220, 220), 2))
            {
                e.Graphics.DrawRectangle(borderPen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }

        // 2. RECORTAR BORDES DE BOTONES (GDI+ a pata) Kenneth
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

        // 3. DIBUJAR CONTORNO SUAVE PARA PANELES Kenneth
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