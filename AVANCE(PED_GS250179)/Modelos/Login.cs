using AVANCE_PED_GS250179_.Datos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVANCE_PED_GS250179_.Modelos
{
    internal class Login
    {
        public string ValidarUsuario(string correo, string contrasena)
        {
            string rolEncontrado = "";
            Conexion objConexion = new Conexion();

            try
            {
                using (SqlConnection cn = objConexion.AbrirConexion())
                {
                    // consulta que une las 3 tablas
                    string query = @"SELECT r.Roles 
                                     FROM InfoEmpleado ie
                                     INNER JOIN Empleado e ON ie.IdEmpleado = e.IdEmpleado
                                     INNER JOIN RolEmpleado r ON e.IdRolEmpleado = r.IdRolEmpleado
                                     WHERE ie.Correo = @correo AND ie.Contraseña = @contra";

                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        // Usamos parámetros
                        cmd.Parameters.AddWithValue("@correo", correo);
                        cmd.Parameters.AddWithValue("@contra", contrasena);

                        
                        object resultado = cmd.ExecuteScalar();

                        if (resultado != null)
                        {
                            rolEncontrado = resultado.ToString();
                        }
                    }
                } 
            }
            catch (Exception ex)
            {
                // Si hay un error con la base, lo puedes manejar aquí
                throw new Exception("Error al conectar a la base de datos: " + ex.Message);
            }

            return rolEncontrado;
        }
    }
}
