using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_
{
    public partial class RecargarForm : Form
    {
        // Cadena de conexión heredada de tu arquitectura
        //private string connectionString = "Server=TU_SERVIDOR;Database=ReiseDB;Trusted_Connection=True;";

        // Elementos de interfaz
        private ComboBox cmbMonto;
        private TextBox txtNombreTarjeta;
        private TextBox txtNumeroTarjeta;
        private TextBox txtExpiracion;
        private TextBox txtCVV;
        private Button btnPagar;
        private Button btnCancelar;

        public RecargarForm()
        {
            // Configuración de la ventana (Estilo modal moderno)
            this.Size = new Size(460, 580);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            ConstruirInterfazRecarga();
        }

        private void ConstruirInterfazRecarga()
        {
            // Panel Superior / Encabezado Temático (Inspirado en paga-todo.com)
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

            // Título de Sección
            Label lblSeccion = new Label
            {
                Text = "1. INFORMACIÓN DE LA RECARGA",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(25, 90),
                AutoSize = true
            };
            this.Controls.Add(lblSeccion);

            // Selector de Cantidad (Monto a recargar)
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
            cmbMonto.SelectedIndex = 0; // Por defecto $3.00 como la muestra
            this.Controls.Add(cmbMonto);

            // Subtítulo de Tarjeta
            Label lblDatosTarjeta = new Label
            {
                Text = "2. DETALLES DE TU TARJETA",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(25, 210),
                AutoSize = true
            };
            this.Controls.Add(lblDatosTarjeta);

            // Campo: Nombre del Titular
            Label lblNombre = new Label { Text = "Nombre del titular", Font = new Font("Segoe UI", 9, FontStyle.Regular), Location = new Point(25, 245), AutoSize = true };
            this.Controls.Add(lblNombre);

            txtNombreTarjeta = CrearTextBoxEstilizado(25, 267, 400);
            this.Controls.Add(txtNombreTarjeta);

            // Campo: Número de Tarjeta
            Label lblNumTarjeta = new Label { Text = "Número de tarjeta (16 dígitos)", Font = new Font("Segoe UI", 9, FontStyle.Regular), Location = new Point(25, 315), AutoSize = true };
            this.Controls.Add(lblNumTarjeta);

            txtNumeroTarjeta = CrearTextBoxEstilizado(25, 337, 400);
            txtNumeroTarjeta.MaxLength = 16;
            this.Controls.Add(txtNumeroTarjeta);

            // Campo: Expiración (MM/AA)
            Label lblExp = new Label { Text = "Expiración (MM/AA)", Font = new Font("Segoe UI", 9, FontStyle.Regular), Location = new Point(25, 385), AutoSize = true };
            this.Controls.Add(lblExp);

            txtExpiracion = CrearTextBoxEstilizado(25, 407, 185);
            txtExpiracion.MaxLength = 5;
            this.Controls.Add(txtExpiracion);

            // Campo: CVV
            Label lblCVV = new Label { Text = "Código CVV", Font = new Font("Segoe UI", 9, FontStyle.Regular), Location = new Point(240, 385), AutoSize = true };
            this.Controls.Add(lblCVV);

            txtCVV = CrearTextBoxEstilizado(240, 407, 185);
            txtCVV.MaxLength = 3;
            this.Controls.Add(txtCVV);

            // Botón de Acción Principal: PAGAR CON TARJETA (Azul corporativo como la muestra)
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

            // Botón Cancelar/Cerrar
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

        // Evento que procesa y valida la lógica de negocio
        private void BtnPagar_Click(object sender, EventArgs e)
        {
            // Validaciones rápidas de campos vacíos
            if (string.IsNullOrWhiteSpace(txtNombreTarjeta.Text) ||
                txtNumeroTarjeta.Text.Length < 16 ||
                string.IsNullOrWhiteSpace(txtExpiracion.Text) ||
                txtCVV.Text.Length < 3)
            {
                MessageBox.Show("Por favor, rellene correctamente todos los campos de la tarjeta bancaria.",
                                "Datos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Extracción limpia del monto numérico eliminando el signo '$'
            string itemSeleccionado = cmbMonto.SelectedItem.ToString();
            double montoARecargar = Convert.ToDouble(itemSeleccionado.Replace("$", ""));

            // CONEXIÓN DIRECTA A TU SQL SERVER PARA ACTUALIZAR EL SALDO
            // NOTA: Ajusta el WHERE según cómo manejes la sesión del usuario (ej. ID de Cliente)
           
            
        }

        // Generador auxiliar de cajas de texto consistentes
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

        // Pintar borde exterior al formulario completo para darle presencia visual
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