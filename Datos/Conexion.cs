using System;
using System.Data.SqlClient;
using System.Configuration;

namespace AVANCE_PED_GS250179_.Datos
{
    public class Conexion
    {
        private static readonly string cadenaConexion;

        static Conexion()
        {
            try
            {
                // Lee la cadena desde App.config; si no existe, usa la cadena por defecto.
                cadenaConexion = ConfigurationManager.ConnectionStrings["Reise2"]?.ConnectionString
                                 ?? "Server=.;Database=Reise2;Trusted_Connection=True;";
            }
            catch
            {
                cadenaConexion = "Server=.;Database=Reise2;Trusted_Connection=True;";
            }
        }

        public SqlConnection AbrirConexion()
        {
            var cn = new SqlConnection(cadenaConexion);
            try
            {
                cn.Open();
                return cn;
            }
            catch (SqlException ex)
            {
                // Re-lanzar con información útil para depuración
                throw new Exception($"No se pudo abrir la conexión (SqlError {ex.Number}): {ex.Message}", ex);
            }
        }

        public void CerrarConexion(SqlConnection cn)
        {
            if (cn != null && cn.State == System.Data.ConnectionState.Open)
                cn.Close();
        }

        public void ExecuteQuery(string query)
        {
            using (var cn = new SqlConnection(cadenaConexion))
            {
                cn.Open();
                using (var cmd = new SqlCommand(query, cn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}