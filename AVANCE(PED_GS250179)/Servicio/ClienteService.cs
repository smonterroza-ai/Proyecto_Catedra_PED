using AVANCE_PED_GS250179_.Datos;
using AVANCE_PED_GS250179_.Modelos;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace AVANCE_PED_GS250179_.Servicio
{
    internal class ClienteService
    {
        private Conexion conexion = new Conexion();

        // Método para procesar la contraseña con SHA256 (Base64)
        public string HashearContrasena(string contrasena)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(contrasena);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        // Validar el inicio de sesión usando Correo y Contraseña
        public Cliente ValidarLogin(string correo, string contrasena)
        {
            Cliente cliente = null;
            SqlConnection cn = conexion.AbrirConexion();

            try
            {
                
                string query = @"
                    SELECT 
                        IdCliente,     -- Posición 0
                        Nombre,        -- Posición 1
                        Apellido,      -- Posición 2
                        Correo         -- Posición 3
                    FROM InfoCliente 
                    WHERE Correo = @Correo AND Contraseña = @Contra";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    
                    cmd.Parameters.AddWithValue("@Correo", correo.Trim());
                    cmd.Parameters.AddWithValue("@Contra", HashearContrasena(contrasena.Trim()));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = new Cliente
                            {
                                IdCliente = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Apellido = reader.GetString(2),
                                Correo = reader.GetString(3)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar login del cliente: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion(cn);
            }

            return cliente;
        }
    }
}