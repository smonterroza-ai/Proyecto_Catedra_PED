using AVANCE_PED_GS250179_.Estructuras;
using AVANCE_PED_GS250179_.Datos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_.Vistas
{
    public partial class ValidarQrForm : Form
    {
        private Button btnDesencolar;
        private Button btnRegresar;
        private Panel pnlLienzoGrafo;

        public int IdRutaFiltrada { get; set; } = 0;

        public ValidarQrForm()
        {
            InitializeComponent();
            ConfigurarVentana();
            InicializarComponentesEstilizados();

            this.Activated += ValidarQrForm_Activated;
        }

        private void ConfigurarVentana()
        {
            this.Text = "Monitor de Tránsito - Grafo de Nodos QR";
            this.Size = new Size(720, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(33, 33, 33);
        }

        private void InicializarComponentesEstilizados()
        {
            Label lblLeyendaNodoRojo = new Label { Text = "Frente (FIFO) = Siguiente QR a Desencolar", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(231, 76, 60), Location = new Point(40, 30), AutoSize = true };
            this.Controls.Add(lblLeyendaNodoRojo);

            Label lblLeyendaNodoAzul = new Label { Text = "Punteros Encolados en Memoria Dinámica", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(52, 152, 219), Location = new Point(40, 55), AutoSize = true };
            this.Controls.Add(lblLeyendaNodoAzul);

            pnlLienzoGrafo = new Panel { Location = new Point(40, 100), Size = new Size(640, 240), BackColor = Color.FromArgb(43, 43, 43) };
            pnlLienzoGrafo.Paint += PnlLienzoGrafo_Paint;
            this.Controls.Add(pnlLienzoGrafo);

            btnDesencolar = new Button { Text = "DESENCOLAR NODO EN MEMORIA", Font = new Font("Segoe UI", 10, FontStyle.Bold), BackColor = Color.FromArgb(230, 126, 34), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(270, 45), Location = new Point(40, 375), Cursor = Cursors.Hand };
            btnDesencolar.FlatAppearance.BorderSize = 0;
            btnDesencolar.Click += BtnDesencolar_Click;
            this.Controls.Add(btnDesencolar);

            btnRegresar = new Button { Text = "← REGRESAR", Font = new Font("Segoe UI", 10, FontStyle.Bold), BackColor = Color.FromArgb(127, 140, 141), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(130, 45), Location = new Point(550, 375), Cursor = Cursors.Hand };
            btnRegresar.FlatAppearance.BorderSize = 0;

            btnRegresar.Click += (s, e) => {
                if (this.Owner != null)
                {
                    this.Owner.Show();
                }
                this.Close();
            };
            this.Controls.Add(btnRegresar);
        }

        private void ValidarQrForm_Activated(object sender, EventArgs e)
        {
            if (pnlLienzoGrafo != null)
            {
                pnlLienzoGrafo.Invalidate();
                pnlLienzoGrafo.Update();
            }
        }

        private void PnlLienzoGrafo_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Recupere la lista completa directamente de la memoria RAM
            List<NodoCompra> nodosFiltrados = ColaCompraManual.InstanciaCompartida.ObenerListaParaDibujar();
            int totalNodos = nodosFiltrados.Count;

            if (totalNodos == 0)
            {
                using (Font fuenteVacia = new Font("Segoe UI", 12, FontStyle.Italic))
                {
                    StringFormat formato = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    string mensajeVacio = "Cola vacía. No hay nodos en memoria.";

                    g.DrawString(mensajeVacio, fuenteVacia, Brushes.Gray, new RectangleF(0, 0, pnlLienzoGrafo.Width, pnlLienzoGrafo.Height), formato);
                }
                return;
            }

            int radioVertice = 32;
            int centroY = pnlLienzoGrafo.Height / 2 - 10;
            int margenIzquierdo = 70;
            int espacioX = totalNodos > 1 ? (pnlLienzoGrafo.Width - (margenIzquierdo * 2)) / (totalNodos) : 0;

            Point[] posiciones = new Point[totalNodos];
            for (int i = 0; i < totalNodos; i++)
            {
                int x = totalNodos == 1 ? pnlLienzoGrafo.Width / 2 : margenIzquierdo + (i * espacioX);
                posiciones[i] = new Point(x, centroY);
            }

            using (Pen lapizArista = new Pen(Color.FromArgb(26, 188, 156), 3))
            {
                lapizArista.CustomEndCap = new AdjustableArrowCap(6, 6);
                for (int i = 0; i < totalNodos - 1; i++)
                {
                    g.DrawLine(lapizArista, posiciones[i].X + radioVertice, posiciones[i].Y, posiciones[i + 1].X - radioVertice, posiciones[i + 1].Y);
                }
            }

            for (int i = 0; i < totalNodos; i++)
            {
                Color colorNodo = (i == 0) ? Color.FromArgb(231, 76, 60) : Color.FromArgb(52, 152, 219);
                Point p = posiciones[i];

                Rectangle rectCirculo = new Rectangle(p.X - radioVertice, p.Y - radioVertice, radioVertice * 2, radioVertice * 2);
                using (SolidBrush pincelFondo = new SolidBrush(colorNodo)) { g.FillEllipse(pincelFondo, rectCirculo); }
                using (Pen borde = new Pen(Color.White, 2)) { g.DrawEllipse(borde, rectCirculo); }

                using (Font fuenteEtiqueta = new Font("Segoe UI", 9, FontStyle.Bold))
                {
                    string txtPuntero = (i == 0) ? "FRENTE" : $"Siguiente {i}";
                    SizeF tamTxt = g.MeasureString(txtPuntero, fuenteEtiqueta);
                    g.DrawString(txtPuntero, fuenteEtiqueta, Brushes.LightGray, new PointF(p.X - (tamTxt.Width / 2), p.Y - radioVertice - 22));
                }

                using (Font fuenteInterna = new Font("Segoe UI", 9, FontStyle.Bold))
                {
                    StringFormat formatoCentro = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString(nodosFiltrados[i].NombreRuta, fuenteInterna, Brushes.White, p, formatoCentro);
                }

                using (Font fuenteMeta = new Font("Segoe UI", 8, FontStyle.Regular))
                {
                    string metadata = $"Ticket: #{nodosFiltrados[i].IdDetalleVenta}\nIdRuta: {nodosFiltrados[i].IdRuta}\nVal: {nodosFiltrados[i].Precio:F2}";
                    g.DrawString(metadata, fuenteMeta, Brushes.DarkGray, new PointF(p.X, p.Y + radioVertice + 8), new StringFormat { Alignment = StringAlignment.Center });
                }
            }
        }

        private void BtnDesencolar_Click(object sender, EventArgs e)
        {
            if (ColaCompraManual.InstanciaCompartida.EstaVacia())
            {
                MessageBox.Show("La cola dinámica en memoria está vacía.", "Estructura Vacía", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Desencolado FIFO directo y puro de la cabeza de la estructura
            NodoCompra nodoProcesado = ColaCompraManual.InstanciaCompartida.Desencolar();

            if (nodoProcesado != null)
            {
                Conexion con = new Conexion();
                SqlConnection cn = con.AbrirConexion();

                if (cn != null)
                {
                    try
                    {
                        string queryActualizarEstado = @"
                            UPDATE DetalleVenta 
                            SET Estado = 'Aprobado' 
                            WHERE IdDetalleVenta = @IdDetalle";

                        using (SqlCommand cmd = new SqlCommand(queryActualizarEstado, cn))
                        {
                            cmd.Parameters.AddWithValue("@IdDetalle", nodoProcesado.IdDetalleVenta);
                            int filasAfectadas = cmd.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                MessageBox.Show($"¡Pasaje procesado y validado en Base de Datos!\n\n" +
                                                $"• Ruta: {nodoProcesado.NombreRuta}\n" +
                                                $"• Precio: {nodoProcesado.Precio:F2}\n" +
                                                $"• ID Ticket SQL: #{nodoProcesado.IdDetalleVenta}\n\n" +
                                                $"Estado transaccional: APROBADO",
                                                "Validación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("El nodo se removió de la cola, pero no se encontró la coincidencia de ID en la tabla DetalleVenta.",
                                                "Advertencia de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar la base de datos: " + ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        con.CerrarConexion(cn);
                    }
                }
            }

            pnlLienzoGrafo.Invalidate();
        }
    }
}