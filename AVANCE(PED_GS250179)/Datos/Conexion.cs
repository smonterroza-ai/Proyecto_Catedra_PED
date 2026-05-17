using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AVANCE_PED_GS250179_.Datos
{
    public class Conexion
    {
        public static string cadenaConexion = "Server=.;Database=Reise2;Trusted_Connection=True;";

        public SqlConnection AbrirConexion()
        {
            SqlConnection cn = new SqlConnection(cadenaConexion);
            cn.Open();
            return cn;
        }

        public void CerrarConexion(SqlConnection cn)
        {
            if (cn != null && cn.State == System.Data.ConnectionState.Open)
                cn.Close();
        }
    }
}
