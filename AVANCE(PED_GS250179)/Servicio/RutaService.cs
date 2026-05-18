using AVANCE_PED_GS250179_.Datos; // Tu clase Conexion
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AVANCE_PED_GS250179_.Servicio
{
    // Modelo adaptado a las columnas reales de tu base de datos
    public class RutaDTO
    {
        public int IdRutaBuses { get; set; }
        public string NumeroRuta { get; set; }
        public decimal CostoDelPasaje { get; set; }
        public string Inicio { get; set; }
        public string Final { get; set; }
    }

    public class RutaService
    {
        private Conexion conexion = new Conexion();

        public List<RutaDTO> ObtenerRutas(string filtroBusqueda = "")
        {
            List<RutaDTO> listaRutas = new List<RutaDTO>();
            SqlConnection cn = conexion.AbrirConexion();

            try
            {
                // CONSULTA RELACIONAL EXACTA: Une los costos, los números de ruta y los destinos de inicio/final
                string query = @"
                    SELECT 
                        RB.IdRutaBuses, 
                        IRB.NumeroRuta, 
                        RB.CostoDelPasaje, 
                        RR.inicio, 
                        RR.Final
                    FROM RutaBuses RB
                    INNER JOIN RecorridoRuta RR ON RB.IdRecorridoRuta = RR.IdRecorridoRuta
                    INNER JOIN InfoRutaBuses IRB ON RB.IdRutaBuses = IRB.IdRutaBuses";

                // Si el cliente busca por texto, filtramos por el número de ruta o por el destino final
                if (!string.IsNullOrEmpty(filtroBusqueda))
                {
                    query += " WHERE IRB.NumeroRuta LIKE @Filtro OR RR.Final LIKE @Filtro OR RR.inicio LIKE @Filtro";
                }

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    if (!string.IsNullOrEmpty(filtroBusqueda))
                    {
                        cmd.Parameters.AddWithValue("@Filtro", "%" + filtroBusqueda + "%");
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RutaDTO ruta = new RutaDTO
                            {
                                IdRutaBuses = reader.GetInt32(0),
                                NumeroRuta = reader.GetString(1),
                                CostoDelPasaje = reader.GetDecimal(2),
                                Inicio = reader.GetString(3),
                                Final = reader.GetString(4)
                            };
                            listaRutas.Add(ruta);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en RutaService al consultar la base de datos de Reise: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion(cn);
            }

            return listaRutas;
        }
    }
}