using AVANCE_PED_GS250179_.Datos;
using AVANCE_PED_GS250179_.Vistas;
using System.Data.SqlClient;

namespace AVANCE_PED_GS250179_
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            bool esPrimerUso = true; // Por defecto asumimos que sí
            Conexion conexion = new Conexion();
            SqlConnection cn = null;

            try
            {
                //Verificamos cuántos empleados existen en la BD
                cn = conexion.AbrirConexion();
                string query = "SELECT COUNT(*) FROM Empleado";
                SqlCommand cmd = new SqlCommand(query, cn);
                int cantidadEmpleados = Convert.ToInt32(cmd.ExecuteScalar());

                // Si hay al menos un empleado, ya no es el primer uso
                if (cantidadEmpleados > 0)
                {
                    esPrimerUso = false;
                }
            }
            catch (Exception ex)
            {
                // Si la BD no está encendida o hay un error de conexión, avisamos y cerramos
                MessageBox.Show("Error crítico al conectar con la base de datos: " + ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (cn != null)
                {
                    conexion.CerrarConexion(cn);
                }
            }

            
            if (esPrimerUso)
            {
                
                Application.Run(new Form9());
            }
            else
            {
                
                Application.Run(new Form8());
            }
        }

    }
}