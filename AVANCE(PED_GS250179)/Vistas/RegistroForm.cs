using AVANCE_PED_GS250179_.Datos;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_.Vistas
{
    public partial class RegistroForm : Form
    {
        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtCorreo;
        private TextBox txtContrasena;
        private Button btnRegistrar;
        private Button btnVolver;

        public RegistroForm()
        {
            this.Size = new Size(450, 580);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            InicializarInterfazRegistro();
        }

        private void InicializarInterfazRegistro()
        {
            Panel pnlHeader = new Panel { Size = new Size(450, 70), Location = new Point(0, 0), BackColor = Color.FromArgb(245, 247, 250) };
            Label lblTitulo = new Label { Text = "reise", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.Black, Location = new Point(30, 20), AutoSize = true };
            Label lblSub = new Label { Text = "| Nueva Cuenta", Font = new Font("Segoe UI", 11, FontStyle.Regular), ForeColor = Color.Gray, Location = new Point(105, 24), AutoSize = true };
            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Controls.Add(lblSub);
            this.Controls.Add(pnlHeader);

            int startY = 100;
            int spacing = 65;

            txtNombre = CrearCampoTexto("NOMBRE:", startY);
            txtApellido = CrearCampoTexto("APELLIDO:", startY + spacing);
            txtCorreo = CrearCampoTexto("CORREO ELECTRÓNICO:", startY + (spacing * 2));
            txtContrasena = CrearCampoTexto("CONTRASEÑA:", startY + (spacing * 3), true);

            btnRegistrar = new Button
            {
                Text = "CREAR CUENTA",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(390, 40),
                Location = new Point(30, 440),
                Cursor = Cursors.Hand
            };
            btnRegistrar.FlatAppearance.BorderSize = 0;
            btnRegistrar.Click += BtnRegistrar_Click;
            this.Controls.Add(btnRegistrar);

            btnVolver = new Button
            {
                Text = "REGRESAR AL INICIO DE SESIÓN",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(390, 35),
                Location = new Point(30, 495),
                Cursor = Cursors.Hand
            };
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.Click += (s, e) => { this.Close(); };
            this.Controls.Add(btnVolver);
        }

        private TextBox CrearCampoTexto(string etiqueta, int posY, bool esContrasena = false)
        {
            Label lbl = new Label { Text = etiqueta, Font = new Font("Segoe UI", 8.5f, FontStyle.Bold), ForeColor = Color.FromArgb(100, 100, 100), Location = new Point(30, posY), AutoSize = true };
            this.Controls.Add(lbl);

            TextBox txt = new TextBox { Font = new Font("Segoe UI", 11), Size = new Size(390, 28), Location = new Point(30, posY + 18), UseSystemPasswordChar = esContrasena };
            this.Controls.Add(txt);
            return txt;
        }

        private string EncriptarContrasena(string cadena)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(cadena));
                return Convert.ToBase64String(bytes);
            }
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) || string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MessageBox.Show("Por favor, rellene todos los campos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Conexion con = new Conexion();
            SqlConnection cn = con.AbrirConexion();
            if (cn == null) return;

            try
            {
                // 1. Validar correo duplicado en InfoCliente
                string queryCheck = "SELECT COUNT(1) FROM InfoCliente WHERE Correo = @Correo";
                using (SqlCommand cmdCheck = new SqlCommand(queryCheck, cn))
                {
                    cmdCheck.Parameters.AddWithValue("@Correo", txtCorreo.Text.Trim());
                    if (Convert.ToInt32(cmdCheck.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("El correo electrónico ingresado ya se encuentra registrado.", "Error de Duplicidad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // 2. Calcular el ID Autoincremental manual basado en la tabla Cliente (Entidad Fuerte)
                int nuevoId = 1;
                string queryId = "SELECT ISNULL(MAX(IdCliente), 0) + 1 FROM Cliente";
                using (SqlCommand cmdId = new SqlCommand(queryId, cn))
                {
                    nuevoId = Convert.ToInt32(cmdId.ExecuteScalar());
                }

                string hashClave = EncriptarContrasena(txtContrasena.Text.Trim());

                // 3. INSERCIÓN 1: Se registra primero el identificador base en la tabla Cliente
                string queryCliente = "INSERT INTO Cliente (IdCliente) VALUES (@Id)";
                using (SqlCommand cmdCliente = new SqlCommand(queryCliente, cn))
                {
                    cmdCliente.Parameters.AddWithValue("@Id", nuevoId);
                    cmdCliente.ExecuteNonQuery();
                }

                // 4. INSERCIÓN 2: Se registran los detalles del usuario con saldo inicial de 0.00m
                string queryInfoCliente = @"INSERT INTO InfoCliente (IdCliente, Nombre, Apellido, Correo, Contraseña, Saldo) 
                                            VALUES (@Id, @Nombre, @Apellido, @Correo, @Contra, @Saldo)";

                using (SqlCommand cmdInfo = new SqlCommand(queryInfoCliente, cn))
                {
                    cmdInfo.Parameters.AddWithValue("@Id", nuevoId);
                    cmdInfo.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                    cmdInfo.Parameters.AddWithValue("@Apellido", txtApellido.Text.Trim());
                    cmdInfo.Parameters.AddWithValue("@Correo", txtCorreo.Text.Trim());
                    cmdInfo.Parameters.AddWithValue("@Contra", hashClave);
                    cmdInfo.Parameters.AddWithValue("@Saldo", 0.00m);

                    cmdInfo.ExecuteNonQuery();
                }

                MessageBox.Show("¡Cuenta registrada con éxito en el sistema!", "Excelente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de consistencia al guardar: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.CerrarConexion(cn);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen borderPen = new Pen(Color.FromArgb(220, 224, 230), 2))
            {
                e.Graphics.DrawRectangle(borderPen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }
    }
}