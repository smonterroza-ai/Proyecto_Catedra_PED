using AVANCE_PED_GS250179_.Datos;
using AVANCE_PED_GS250179_.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AVANCE_PED_GS250179_.Servicio
{
    internal class EmpleadoService
    {
        Conexion conexion = new Conexion();

        public string HashearContrasena(string contrasena)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(contrasena);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public Empleado ValidarLogin(string correo, string contrasena)
        {
            Empleado empleado = null;
            SqlConnection cn = conexion.AbrirConexion();

            try
            {
                
                string query = @"
        SELECT 
            ie.IdEmpleado,     -- Posición 0
            ie.Nombre,         -- Posición 1
            ie.Correo,         -- Posición 2
            ie.Usuario,        -- Posición 3
            r.IdRolEmpleado,   -- Posición 4
            r.Roles            -- Posición 5
        FROM InfoEmpleado ie
        INNER JOIN Empleado e ON ie.IdEmpleado = e.IdEmpleado
        INNER JOIN RolEmpleado r ON e.IdRolEmpleado = r.IdRolEmpleado
        WHERE ie.Usuario = @usuario AND ie.Contraseña = @contra";
                

                SqlCommand cmd = new SqlCommand(query, cn);

                cmd.Parameters.AddWithValue("@usuario", correo);
                cmd.Parameters.AddWithValue("@contra", HashearContrasena(contrasena));

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Empaquetamos los datos usando los nuevos números de posición
                    empleado = new Empleado
                    {
                        IdEmpleado = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Correo = reader.GetString(2),
                        Usuario = reader.GetString(3),
                        IdRolEmpleado = reader.GetInt32(4),
                        NombreRol = reader.GetString(5)
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar login: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion(cn);
            }

            return empleado;
        }
    }
}
