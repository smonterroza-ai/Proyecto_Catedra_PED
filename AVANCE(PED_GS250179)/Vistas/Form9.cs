using AVANCE_PED_GS250179_.Datos;
using AVANCE_PED_GS250179_.Servicio;
using AVANCE_PED_GS250179_.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_.Vistas
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        bool contraseñaVisible = false;

        private void ptojo_Click(object sender, EventArgs e)
        {
            if (contraseñaVisible == true)
            {
                txtPass.PasswordChar = '*';

                ptojo.Image = Properties.Resources.icons8_eye_24;

                contraseñaVisible = false;
            }
            // Si está oculta, la mostramos
            else
            {
                // Quitar la máscara ('\0' significa carácter nulo, o sea, sin máscara)
                txtPass.PasswordChar = '\0';

                // Cambiar la imagen al ojo abierto (reemplaza 'ojo_abierto' por el nombre de tu imagen)
                ptojo.Image = Properties.Resources.icons8_ojo_cerrado_24;

                // Actualizamos el estado
                contraseñaVisible = true;
            }
        }

        private void ptojo2_Click(object sender, EventArgs e)
        {
            if (contraseñaVisible == true)
            {
                txtConfPass.PasswordChar = '*';

                ptojo2.Image = Properties.Resources.icons8_eye_24;

                contraseñaVisible = false;
            }
            // Si está oculta, la mostramos
            else
            {
                // Quitar la máscara ('\0' significa carácter nulo, o sea, sin máscara)
                txtConfPass.PasswordChar = '\0';

                // Cambiar la imagen al ojo abierto (reemplaza 'ojo_abierto' por el nombre de tu imagen)
                ptojo2.Image = Properties.Resources.icons8_ojo_cerrado_24;

                // Actualizamos el estado
                contraseñaVisible = true;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Ignora cualquier otro símbolo o número
            }
        }

        private void txtDui_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            // Agregar guion automáticamente después de 8 números 
            if (txtDui.Text.Length == 8 && e.KeyChar != (char)Keys.Back)
            {
                txtDui.Text += "-";
                txtDui.SelectionStart = txtDui.Text.Length;
            }
        }

        private void txtTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            // Agregar guion automáticamente después de 4 números
            if (txtTel.Text.Length == 4 && e.KeyChar != (char)Keys.Back)
            {
                txtTel.Text += "-";
                txtTel.SelectionStart = txtTel.Text.Length;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Validaciones básicas (que no haya campos vacíos o solo con guiones)
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDui.Text) || txtDui.Text == "-" ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(txtTel.Text) || txtTel.Text == "-" ||
                string.IsNullOrWhiteSpace(txtUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtPass.Text) ||
                string.IsNullOrWhiteSpace(txtConfPass.Text))
            {
                MessageBox.Show("Por favor, llena todos los campos de texto.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validar formato de Correo Electrónico
            string patronCorreo = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtCorreo.Text.Trim(), patronCorreo))
            {
                MessageBox.Show("Por favor ingresa un correo electrónico válido (ejemplo@dominio.com).", "Correo Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return;
            }

            // 3. Validar Fechas (No permitir fechas del futuro)
            if (dtpNa.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("La fecha de nacimiento no puede ser una fecha en el futuro.", "Fecha Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNa.Focus();
                return;
            }

            if (dtpCon.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("La fecha de contratación no puede ser una fecha en el futuro.", "Fecha Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpCon.Focus();
                return;
            }

            // 4. Validar que las contraseñas coincidan exactamente
            if (txtPass.Text.Trim() != txtConfPass.Text.Trim())
            {
                MessageBox.Show("Las contraseñas no coinciden. Por favor, escríbelas de nuevo.", "Error de seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPass.Clear();
                txtConfPass.Clear();
                txtPass.Focus();
                return;
            }

            // 5. Encriptar la contraseña usando tu EmpleadoService
            EmpleadoService empService = new EmpleadoService();
            string contrasenaEncriptada = empService.HashearContrasena(txtPass.Text.Trim());

            // 6. Preparar la conexión a la Base de Datos
            Conexion conexion = new Conexion();
            SqlConnection cn = conexion.AbrirConexion();

            try
            {
                // Generamos el ID
                string queryId = "SELECT ISNULL(MAX(IdEmpleado), 0) + 1 FROM Empleado";
                SqlCommand cmdId = new SqlCommand(queryId, cn);
                int nuevoIdEmpleado = Convert.ToInt32(cmdId.ExecuteScalar());

                // INSERT 1: Tabla Empleado
                string queryEmpleado = @"INSERT INTO Empleado (IdEmpleado, IdRolEmpleado) VALUES (@id, 1)";
                SqlCommand cmdEmpleado = new SqlCommand(queryEmpleado, cn);
                cmdEmpleado.Parameters.AddWithValue("@id", nuevoIdEmpleado);
                cmdEmpleado.ExecuteNonQuery();

                // INSERT 2: Tabla InfoEmpleado (Aquí van todos los datos personales)
                string queryInfo = @"INSERT INTO InfoEmpleado (IdEmpleado, Nombre, DUI, FechaNacimiento, Direccion, Telefono, FechaContratacion, Correo, Usuario, Contraseña) VALUES (@id, @nombre, @dui, @fechaNac, @direccion, @telefono, @fechaContratacion, @correo, @usuario, @contrasena)";
                SqlCommand cmdInfo = new SqlCommand(queryInfo, cn);
                cmdInfo.Parameters.AddWithValue("@id", nuevoIdEmpleado);
                cmdInfo.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                cmdInfo.Parameters.AddWithValue("@dui", txtDui.Text.Trim());
                cmdInfo.Parameters.AddWithValue("@fechaNac", dtpNa.Value.Date);

                // Como no hay Textbox de dirección en tu pantalla, mandamos un texto por defecto
                cmdInfo.Parameters.AddWithValue("@direccion", "No especificada");

                cmdInfo.Parameters.AddWithValue("@telefono", txtTel.Text.Trim());
                cmdInfo.Parameters.AddWithValue("@fechaContratacion", dtpCon.Value.Date);
                cmdInfo.Parameters.AddWithValue("@correo", txtCorreo.Text.Trim());
                cmdInfo.Parameters.AddWithValue("@usuario", txtUsuario.Text.Trim());
                cmdInfo.Parameters.AddWithValue("@contrasena", contrasenaEncriptada);
                cmdInfo.ExecuteNonQuery();

                MessageBox.Show("¡Administrador registrado exitosamente!\nAhora puedes iniciar sesión.", "Primer Uso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cerramos esta pantalla y abrimos el Login
                this.Hide();
                Form1 login = new Form1();
                login.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar en la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.CerrarConexion(cn);
            }
        }

        private void Form9_Shown(object sender, EventArgs e)
        {
            RedondearBoton.RedondearBotones(button1, 30);
        }
    }
}
