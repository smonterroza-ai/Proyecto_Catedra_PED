using AVANCE_PED_GS250179_.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVANCE_PED_GS250179_.Servicio
{
    public class TransaccionService
    {
        Conexion conexion = new Conexion();

        public DataTable ObtenerTransacciones()
        {
            return EjecutarConsultaTransacciones(""); 
        }
        public DataTable BuscarTransacciones(string filtro)
        {
            return EjecutarConsultaTransacciones(filtro);
        }

        private DataTable EjecutarConsultaTransacciones(string filtro)
        {
            DataTable dt = new DataTable();
            SqlConnection cn = conexion.AbrirConexion();

            try
            {
                string query = @"
                    SELECT 
                        dv.IdDetalleVenta AS [ID VENTA],
                        (ic.Nombre + ' ' + ic.Apellido) AS [CLIENTE],
                        irb.NumeroRuta AS [RUTA],
                        cp.CantidadAComprar AS [CANTIDAD BOLETOS],
                        cp.TotalApagar AS [TOTAL ($)],
                        mp.NombreMetodosDePago AS [MÉTODO PAGO],
                        dv.Hora AS [FECHA/HORA],
                        dv.Estado AS [ESTADO]
                    FROM DetalleVenta dv
                    INNER JOIN CompraPasajes cp ON dv.IdCompraPasajes = cp.IdCompraPasajes
                    INNER JOIN InfoCliente ic ON dv.IdCliente = ic.IdCliente
                    INNER JOIN MetodosDePago mp ON dv.IdMetodosDePago = mp.IdMetodosDePago
                    INNER JOIN InfoRutaBuses irb ON cp.IdRutaBuses = irb.IdRutaBuses
                    WHERE 1 = 1 ";

                // Si el usuario escribió algo en el buscador, agregamos esta condición
                if (!string.IsNullOrEmpty(filtro))
                {
                    query += " AND (ic.Nombre LIKE @filtro OR ic.Apellido LIKE @filtro OR irb.NumeroRuta LIKE @filtro)";
                }

                query += " ORDER BY dv.Hora DESC"; // Ordenamos de la más reciente a la más antigua

                SqlCommand cmd = new SqlCommand(query, cn);

                if (!string.IsNullOrEmpty(filtro))
                {
                    // Los % son para que busque coincidencias en cualquier parte del texto
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar las transacciones: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion(cn);
            }

            return dt;
        }
    }
}
