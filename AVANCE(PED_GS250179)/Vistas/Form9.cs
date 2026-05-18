using AVANCE_PED_GS250179_.Datos;
using AVANCE_PED_GS250179_.Servicio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
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
            // 1. Validaciones básicas (que no haya campos vacíos)
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtDui.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) || string.IsNullOrWhiteSpace(txtTel.Text) ||
                string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Por favor, llena todos los campos de texto.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Encriptar la contraseña usando TU clase EmpleadoService
            EmpleadoService empService = new EmpleadoService();
            string contrasenaEncriptada = empService.HashearContrasena(txtPass.Text.Trim());

            // 3. Preparar la conexión a la Base de Datos
            Conexion conexion = new Conexion();
            SqlConnection cn = conexion.AbrirConexion();

            try
            {
                // Generamos un nuevo ID para el empleado (asumiendo que no usas IDENTITY, como hicimos en las rutas)
                string queryId = "SELECT ISNULL(MAX(IdEmpleado), 0) + 1 FROM Empleado";
                SqlCommand cmdId = new SqlCommand(queryId, cn);
                int nuevoIdEmpleado = Convert.ToInt32(cmdId.ExecuteScalar());

                // INSERT 1: Tabla Empleado (Datos operativos)
                // Nota: Asumo que por defecto al registrarlo se le asigna un IdRolEmpleado genérico (ej. 2 para motorista/usuario)
                string queryEmpleado = @"INSERT INTO Empleado (IdEmpleado, IdRolEmpleado, Estado, FechaContratacion) 
                                 VALUES (@id, 2, @estado, @fechaContratacion)";
                SqlCommand cmdEmpleado = new SqlCommand(queryEmpleado, cn);
                cmdEmpleado.Parameters.AddWithValue("@id", nuevoIdEmpleado);
                cmdEmpleado.Parameters.AddWithValue("@estado", cbEstado.SelectedItem?.ToString() ?? "Activo");
                cmdEmpleado.Parameters.AddWithValue("@fechaContratacion", dtpCon.Value.Date);
                cmdEmpleado.ExecuteNonQuery();

                // INSERT 2: Tabla InfoEmpleado (Datos personales y credenciales)
                string queryInfo = @"INSERT INTO InfoEmpleado (IdEmpleado, Nombre, DUI, FechaNacimiento, Correo, Telefono, Usuario, Contraseña) 
                             VALUES (@id, @nombre, @dui, @fechaNac, @correo, @telefono, @usuario, @contrasena)";
                SqlCommand cmdInfo = new SqlCommand(queryInfo, cn);
                cmdInfo.Parameters.AddWithValue("@id", nuevoIdEmpleado);
                cmdInfo.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                cmdInfo.Parameters.AddWithValue("@dui", txtDui.Text.Trim());
                cmdInfo.Parameters.AddWithValue("@fechaNac", dtpNa.Value.Date);
                cmdInfo.Parameters.AddWithValue("@correo", txtCorreo.Text.Trim());
                cmdInfo.Parameters.AddWithValue("@telefono", dtpCon.Text.Trim());
                cmdInfo.Parameters.AddWithValue("@usuario", txtUsuario.Text.Trim());

                // Pasamos la contraseña ya hasheada en Base64
                cmdInfo.Parameters.AddWithValue("@contrasena", contrasenaEncriptada);
                cmdInfo.ExecuteNonQuery();

                MessageBox.Show("¡Usuario registrado exitosamente en el sistema!", "Registro Completo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Opcional: Limpiar los campos después de guardar
                // txtNombre.Clear(); txtDui.Clear(); txtUsuario.Clear(); txtContrasena.Clear(); ...
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
    }
}
