using AVANCE_PED_GS250179_.Datos; 
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
        public bool EliminarRuta(int idRuta)
        {
            SqlConnection cn = conexion.AbrirConexion();
            SqlTransaction transaccion = cn.BeginTransaction(); 

            try
            {
                // Obtenemos el ID del recorrido asociado a esta ruta
                SqlCommand cmdGetRecorrido = new SqlCommand("SELECT IdRecorridoRuta FROM RutaBuses WHERE IdRutaBuses = @id", cn, transaccion);
                cmdGetRecorrido.Parameters.AddWithValue("@id", idRuta);
                object resultado = cmdGetRecorrido.ExecuteScalar();

                if (resultado == null) throw new Exception("Ruta no encontrada.");
                int idRecorrido = Convert.ToInt32(resultado);

                // Borramos en cascada 

                // Borrar de InfoRutaBuses
                SqlCommand cmdInfoRuta = new SqlCommand("DELETE FROM InfoRutaBuses WHERE IdRutaBuses = @id", cn, transaccion);
                cmdInfoRuta.Parameters.AddWithValue("@id", idRuta);
                cmdInfoRuta.ExecuteNonQuery();

                SqlCommand cmdRuta = new SqlCommand("DELETE FROM RutaBuses WHERE IdRutaBuses = @id", cn, transaccion);
                cmdRuta.Parameters.AddWithValue("@id", idRuta);
                cmdRuta.ExecuteNonQuery();

                SqlCommand cmdInfoRec = new SqlCommand("DELETE FROM InfoRecorridoRuta WHERE IdRecorridoRuta = @idRec", cn, transaccion);
                cmdInfoRec.Parameters.AddWithValue("@idRec", idRecorrido);
                cmdInfoRec.ExecuteNonQuery();

                SqlCommand cmdRec = new SqlCommand("DELETE FROM RecorridoRuta WHERE IdRecorridoRuta = @idRec", cn, transaccion);
                cmdRec.Parameters.AddWithValue("@idRec", idRecorrido);
                cmdRec.ExecuteNonQuery();

                transaccion.Commit();
                return true;
            }
            catch (SqlException sqlEx)
            {
                transaccion.Rollback(); 
                if (sqlEx.Number == 547)
                    throw new Exception("No se puede eliminar esta ruta porque tiene pasajes vendidos en el sistema.");
                else
                    throw new Exception("Error BD: " + sqlEx.Message);
            }
            finally
            {
                conexion.CerrarConexion(cn);
            }
        }
        public string ObtenerRecorridoPorIdRuta(int idRuta)
        {
            string recorrido = "";
            SqlConnection cn = conexion.AbrirConexion();
            try
            {
                string query = @"SELECT irr.ParadasRuta 
                                 FROM RutaBuses rb
                                 INNER JOIN InfoRecorridoRuta irr ON rb.IdRecorridoRuta = irr.IdRecorridoRuta
                                 WHERE rb.IdRutaBuses = @id";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@id", idRuta);
                object result = cmd.ExecuteScalar();
                if (result != null) recorrido = result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener recorrido: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion(cn);
            }
            return recorrido;
        }

        public bool ActualizarRuta(int idRuta, string numeroRuta, decimal tarifa, string inicio, string final, string paradas, string coordenadas)
        {
            SqlConnection cn = conexion.AbrirConexion();
            SqlTransaction transaccion = cn.BeginTransaction();
            try
            {
                // 1. Obtener el IdRecorridoRuta para saber cuál modificar
                SqlCommand cmdGetRecorrido = new SqlCommand("SELECT IdRecorridoRuta FROM RutaBuses WHERE IdRutaBuses = @id", cn, transaccion);
                cmdGetRecorrido.Parameters.AddWithValue("@id", idRuta);
                int idRecorrido = Convert.ToInt32(cmdGetRecorrido.ExecuteScalar());

                // UPDATE 1: Tabla RutaBuses (Tarifa)
                SqlCommand cmdRuta = new SqlCommand("UPDATE RutaBuses SET CostoDelPasaje = @tarifa WHERE IdRutaBuses = @id", cn, transaccion);
                cmdRuta.Parameters.AddWithValue("@tarifa", tarifa);
                cmdRuta.Parameters.AddWithValue("@id", idRuta);
                cmdRuta.ExecuteNonQuery();

                // UPDATE 2: Tabla InfoRutaBuses (Número de Ruta)
                SqlCommand cmdInfoRuta = new SqlCommand("UPDATE InfoRutaBuses SET NumeroRuta = @num WHERE IdRutaBuses = @id", cn, transaccion);
                cmdInfoRuta.Parameters.AddWithValue("@num", numeroRuta);
                cmdInfoRuta.Parameters.AddWithValue("@id", idRuta);
                cmdInfoRuta.ExecuteNonQuery();

                // UPDATE 3: Tabla RecorridoRuta (Inicio y Fin)
                SqlCommand cmdRecorrido = new SqlCommand("UPDATE RecorridoRuta SET inicio = @ini, Final = @fin WHERE IdRecorridoRuta = @idRec", cn, transaccion);
                cmdRecorrido.Parameters.AddWithValue("@ini", inicio);
                cmdRecorrido.Parameters.AddWithValue("@fin", final);
                cmdRecorrido.Parameters.AddWithValue("@idRec", idRecorrido);
                cmdRecorrido.ExecuteNonQuery();

                // UPDATE 4: Tabla InfoRecorridoRuta (Paradas y COORDENADAS GPS)
                SqlCommand cmdInfoRec = new SqlCommand("UPDATE InfoRecorridoRuta SET ParadasRuta = @paradas, CoordenadasGPS = @coords WHERE IdRecorridoRuta = @idRec", cn, transaccion);
                cmdInfoRec.Parameters.AddWithValue("@paradas", paradas);
                cmdInfoRec.Parameters.AddWithValue("@coords", coordenadas); // ¡AQUÍ GUARDAMOS LAS COORDENADAS!
                cmdInfoRec.Parameters.AddWithValue("@idRec", idRecorrido);
                cmdInfoRec.ExecuteNonQuery();

                transaccion.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                throw new Exception("Error al actualizar la ruta: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion(cn);
            }
        }
        public string ObtenerCoordenadasGPS(int idRuta)
        {
            string coordenadas = "";
            SqlConnection cn = conexion.AbrirConexion();
            try
            {
                string query = "SELECT irr.CoordenadasGPS FROM RutaBuses rb INNER JOIN InfoRecorridoRuta irr ON rb.IdRecorridoRuta = irr.IdRecorridoRuta WHERE rb.IdRutaBuses = @id";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@id", idRuta);
                object result = cmd.ExecuteScalar();

                // Si hay datos, los guardamos en la variable
                if (result != DBNull.Value && result != null)
                {
                    coordenadas = result.ToString();
                }
            }
            catch (Exception) { /* Ignoramos si la ruta es muy vieja y no tiene GPS */ }
            finally { conexion.CerrarConexion(cn); }

            return coordenadas;
        }
    }
}