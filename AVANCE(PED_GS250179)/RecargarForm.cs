using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_
{
    public partial class RecargarForm : Form
    {
        private Datos.Conexion conexionGeneral = new Datos.Conexion();

        private ComboBox cmbMonto;
        private TextBox txtNombreTarjeta;
        private TextBox txtNumeroTarjeta;
        private TextBox txtExpiracion;
        private TextBox txtCVV;
        private Button btnPagar;
        private Button btnCancelar;

        public RecargarForm()
        {
            this.Size = new Size(460, 580);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            ConstruirInterfazRecarga();
        }

        private void ConstruirInterfazRecarga()
        {
            Panel pnlHeader = new Panel { Size = new Size(460, 65), Location = new Point(0, 0), BackColor = Color.FromArgb(245, 247, 250) };
            Label lblHeaderTitulo = new Label
            {
                Text = "reise  |  Pasarela de Pago Seguro",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 204),
                Location = new Point(25, 22),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblHeaderTitulo);
            this.Controls.Add(pnlHeader);

            Label lblSeccion = new Label
            {
                Text = "1. INFORMACIÓN DE LA RECARGA",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(25, 90),
                AutoSize = true
            };
            this.Controls.Add(lblSeccion);

            Label lblMonto = new Label { Text = "Cantidad a depositar", Font = new Font("Segoe UI", 9, FontStyle.Regular), Location = new Point(25, 130), AutoSize = true };
            this.Controls.Add(lblMonto);

            cmbMonto = new ComboBox
            {
                Font = new Font("Segoe UI", 11),
                Size = new Size(400, 30),
                Location = new Point(25, 152),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbMonto.Items.AddRange(new object[] { "$3.00", "$5.00", "$10.00", "$20.00", "$50.00" });
            cmbMonto.SelectedIndex = 0;
            this.Controls.Add(cmbMonto);

            Label lblDatosTarjeta = new Label
            {
                Text = "2. DETALLES DE TU TARJETA",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(25, 210),
                AutoSize = true
            };
            this.Controls.Add(lblDatosTarjeta);

            Label lblNombre = new Label { Text = "Nombre del titular", Font = new Font("Segoe UI", 9, FontStyle.Regular), Location = new Point(25, 245), AutoSize = true };
            this.Controls.Add(lblNombre);

            txtNombreTarjeta = CrearTextBoxEstilizado(25, 267, 400);
            this.Controls.Add(txtNombreTarjeta);

            Label lblNumTarjeta = new Label { Text = "Número de tarjeta (16 dígitos)", Font = new Font("Segoe UI", 9, FontStyle.Regular), Location = new Point(25, 315), AutoSize = true };
            this.Controls.Add(lblNumTarjeta);

            txtNumeroTarjeta = CrearTextBoxEstilizado(25, 337, 400);
            txtNumeroTarjeta.MaxLength = 16;
            this.Controls.Add(txtNumeroTarjeta);

            Label lblExp = new Label { Text = "Expiración (MM/AA)", Font = new Font("Segoe UI", 9, FontStyle.Regular), Location = new Point(25, 385), AutoSize = true };
            this.Controls.Add(lblExp);

            txtExpiracion = CrearTextBoxEstilizado(25, 407, 185);
            txtExpiracion.MaxLength = 5;
            this.Controls.Add(txtExpiracion);

            Label lblCVV = new Label { Text = "Código CVV", Font = new Font("Segoe UI", 9, FontStyle.Regular), Location = new Point(240, 385), AutoSize = true };
            this.Controls.Add(lblCVV);

            txtCVV = CrearTextBoxEstilizado(240, 407, 185);
            txtCVV.MaxLength = 3;
            this.Controls.Add(txtCVV);

            btnPagar = new Button
            {
                Text = "PAGAR CON TARJETA",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 82, 164),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(400, 42),
                Location = new Point(25, 470),
                Cursor = Cursors.Hand
            };
            btnPagar.FlatAppearance.BorderSize = 0;
            btnPagar.Click += BtnPagar_Click;
            this.Controls.Add(btnPagar);

            btnCancelar = new Button
            {
                Text = "Cancelar transacción",
                Font = new Font("Segoe UI", 9, FontStyle.Underline),
                ForeColor = Color.Gray,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(160, 25),
                Location = new Point(150, 525),
                Cursor = Cursors.Hand
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            this.Controls.Add(btnCancelar);
        }

        private void BtnPagar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreTarjeta.Text) ||
                txtNumeroTarjeta.Text.Length < 16 || !long.TryParse(txtNumeroTarjeta.Text, out _) ||
                string.IsNullOrWhiteSpace(txtExpiracion.Text) ||
                txtCVV.Text.Length < 3 || !int.TryParse(txtCVV.Text, out _))
            {
                MessageBox.Show("Por favor, rellene correctamente todos los campos de la tarjeta bancaria.",
                                "Datos Incompletos / Incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string itemSeleccionado = cmbMonto.SelectedItem.ToString();
            decimal montoARecargar = Convert.ToDecimal(itemSeleccionado.Replace("$", ""));

            int idMetodoPago = 1;
            if (txtNumeroTarjeta.Text.StartsWith("5"))
            {
                idMetodoPago = 2;
            }

            try
            {
                using (SqlConnection cn = conexionGeneral.AbrirConexion())
                {
                    int idCompraPasajes = 1;
                    string queryIdCompra = "SELECT ISNULL(MAX(IdCompraPasajes), 0) + 1 FROM CompraPasajes";
                    using (SqlCommand cmdId = new SqlCommand(queryIdCompra, cn))
                    {
                        idCompraPasajes = Convert.ToInt32(cmdId.ExecuteScalar());
                    }

                    int idDetalleVenta = 1;
                    string queryIdVenta = "SELECT ISNULL(MAX(IdDetalleVenta), 0) + 1 FROM DetalleVenta";
                    using (SqlCommand cmdIdV = new SqlCommand(queryIdVenta, cn))
                    {
                        idDetalleVenta = Convert.ToInt32(cmdIdV.ExecuteScalar());
                    }

                    using (SqlTransaction transaccion = cn.BeginTransaction())
                    {
                        try
                        {
                            string sqlCompra = "INSERT INTO CompraPasajes (IdCompraPasajes, IdRutaBuses, CantidadAComprar, TotalApagar) " +
                                               "VALUES (@idCompra, @idRuta, @cantidad, @total)";

                            using (SqlCommand cmdCompra = new SqlCommand(sqlCompra, cn, transaccion))
                            {
                                cmdCompra.Parameters.AddWithValue("@idCompra", idCompraPasajes);
                                cmdCompra.Parameters.AddWithValue("@idRuta", 1);
                                cmdCompra.Parameters.AddWithValue("@cantidad", 1);
                                cmdCompra.Parameters.AddWithValue("@total", montoARecargar);
                                cmdCompra.ExecuteNonQuery();
                            }

                            string sqlVenta = "INSERT INTO DetalleVenta (IdDetalleVenta, IdCompraPasajes, IdCliente, IdMetodosDePago, Estado) " +
                                              "VALUES (@idVenta, @idCompra, @idCliente, @idMetodo, @estado)";

                            using (SqlCommand cmdVenta = new SqlCommand(sqlVenta, cn, transaccion))
                            {
                                cmdVenta.Parameters.AddWithValue("@idVenta", idDetalleVenta);
                                cmdVenta.Parameters.AddWithValue("@idCompra", idCompraPasajes);
                                cmdVenta.Parameters.AddWithValue("@idCliente", 1); // ID del usuario logueado
                                cmdVenta.Parameters.AddWithValue("@idMetodo", idMetodoPago);
                                cmdVenta.Parameters.AddWithValue("@estado", "Activo");
                                cmdVenta.ExecuteNonQuery();
                            }

                            transaccion.Commit();

                            MessageBox.Show($"¡Pago Seguro Procesado Exitosamente!\nSe abonaron {itemSeleccionado} a tu cuenta Reise.",
                                            "Pasarela Seguro Reise", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // IMPORTANTE: Retornamos OK para avisarle a MenuCliente que se debe actualizar
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception exInner)
                        {
                            transaccion.Rollback();
                            throw new Exception("Error interno en la transacción SQL: " + exInner.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo procesar la transacción bancaria.\nDetalle: " + ex.Message,
                                "Error de Comunicación Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private TextBox CrearTextBoxEstilizado(int x, int y, int ancho)
        {
            TextBox tb = new TextBox
            {
                Font = new Font("Segoe UI", 11),
                Size = new Size(ancho, 28),
                Location = new Point(x, y),
                BorderStyle = BorderStyle.FixedSingle
            };
            return tb;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen p = new Pen(Color.FromArgb(220, 220, 220), 2))
            {
                e.Graphics.DrawRectangle(p, 0, 0, this.Width - 1, this.Height - 1);
            }
        }
    }
}