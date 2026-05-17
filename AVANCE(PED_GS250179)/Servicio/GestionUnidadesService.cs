using AVANCE_PED_GS250179_.Datos;
using AVANCE_PED_GS250179_.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVANCE_PED_GS250179_.Servicio
{
    internal class GestionUnidadesService
    {
        Conexion conexion = new Conexion();

        // =========================
        // MOSTRAR UNIDADES
        // =========================
        public DataTable MostrarUnidades()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection cn = conexion.AbrirConexion())
            {
                string query = @"
                SELECT 
                    rb.IdRutaBuses,
                    irb.NumeroRuta,
                    db.PlacaVehiculo,
                    db.Marca,
                    db.Modelo,
                    db.EstadoVehiculo,
                    rb.MotoristaNombre,
                    tv.TipoVehiculo,
                    rr.Inicio,
                    rr.Final
                FROM RutaBuses rb
                INNER JOIN InfoRutaBuses irb
                    ON rb.IdRutaBuses = irb.IdRutaBuses
                INNER JOIN DetalleBuses db
                    ON irb.IdDetalleBuses = db.IdDetalleBuses
                INNER JOIN TipoVehiculo tv
                    ON db.IdTipoVehiculo = tv.IdTipoVehiculo
                INNER JOIN RecorridoRuta rr
                    ON rb.IdRecorridoRuta = rr.IdRecorridoRuta";

                SqlDataAdapter da = new SqlDataAdapter(query, cn);

                da.Fill(tabla);
            }

            return tabla;
        }

        // =========================
        // AGREGAR UNIDAD
        // =========================
        public bool AgregarUnidad(GestionUnidades unidad)
        {
            using (SqlConnection cn = conexion.AbrirConexion())
            {
                SqlTransaction transaccion = cn.BeginTransaction();

                try
                {
                    // INSERT DETALLE BUSES
                    string queryDetalle = @"
                    INSERT INTO DetalleBuses
                    VALUES
                    (
                        @IdDetalle,
                        @Marca,
                        @Modelo,
                        @Estado,
                        @Placa,
                        @IdTipo
                    )";

                    SqlCommand cmdDetalle =
                        new SqlCommand(queryDetalle, cn, transaccion);

                    int idDetalle = ObtenerUltimoId(
                        "DetalleBuses",
                        "IdDetalleBuses",
                        cn,
                        transaccion
                    );

                    cmdDetalle.Parameters.AddWithValue("@IdDetalle", idDetalle);
                    cmdDetalle.Parameters.AddWithValue("@Marca", unidad.Marca);
                    cmdDetalle.Parameters.AddWithValue("@Modelo", unidad.Modelo);
                    cmdDetalle.Parameters.AddWithValue("@Estado", unidad.EstadoVehiculo);
                    cmdDetalle.Parameters.AddWithValue("@Placa", unidad.PlacaVehiculo);
                    cmdDetalle.Parameters.AddWithValue("@IdTipo", unidad.IdTipoVehiculo);

                    cmdDetalle.ExecuteNonQuery();

                    // INSERT RUTA BUSES
                    string queryRuta = @"
                    INSERT INTO RutaBuses
                    VALUES
                    (
                        @IdRuta,
                        @IdEmpresa,
                        @IdRecorrido,
                        @Motorista,
                        @Costo
                    )";

                    SqlCommand cmdRuta =
                        new SqlCommand(queryRuta, cn, transaccion);

                    int idRuta = ObtenerUltimoId(
                        "RutaBuses",
                        "IdRutaBuses",
                        cn,
                        transaccion
                    );

                    cmdRuta.Parameters.AddWithValue("@IdRuta", idRuta);
                    cmdRuta.Parameters.AddWithValue("@IdEmpresa", unidad.IdEmpresa);
                    cmdRuta.Parameters.AddWithValue("@IdRecorrido", unidad.IdRecorridoRuta);
                    cmdRuta.Parameters.AddWithValue("@Motorista", unidad.MotoristaNombre);
                    cmdRuta.Parameters.AddWithValue("@Costo", 0.25);

                    cmdRuta.ExecuteNonQuery();

                    // INSERT INFO RUTA
                    string queryInfo = @"
                    INSERT INTO InfoRutaBuses
                    VALUES
                    (
                        @IdRuta,
                        @NumeroRuta,
                        @IdDetalle
                    )";

                    SqlCommand cmdInfo =
                        new SqlCommand(queryInfo, cn, transaccion);

                    cmdInfo.Parameters.AddWithValue("@IdRuta", idRuta);
                    cmdInfo.Parameters.AddWithValue("@NumeroRuta", unidad.NumeroRuta);
                    cmdInfo.Parameters.AddWithValue("@IdDetalle", idDetalle);

                    cmdInfo.ExecuteNonQuery();

                    transaccion.Commit();

                    return true;
                }
                catch
                {
                    transaccion.Rollback();
                    return false;
                }
            }
        }

        // =========================
        // ELIMINAR UNIDAD
        // =========================
        public bool EliminarUnidad(int idRutaBus)
        {
            using (SqlConnection cn = conexion.AbrirConexion())
            {
                SqlTransaction transaccion = cn.BeginTransaction();

                try
                {
                    int idDetalle = 0;

                    string obtenerDetalle = @"
                    SELECT IdDetalleBuses
                    FROM InfoRutaBuses
                    WHERE IdRutaBuses = @IdRuta";

                    SqlCommand cmdDetalle =
                        new SqlCommand(obtenerDetalle, cn, transaccion);

                    cmdDetalle.Parameters.AddWithValue("@IdRuta", idRutaBus);

                    object resultado = cmdDetalle.ExecuteScalar();

                    if (resultado != null)
                    {
                        idDetalle = Convert.ToInt32(resultado);
                    }

                    // INFO RUTA
                    SqlCommand cmdInfo = new SqlCommand(
                        "DELETE FROM InfoRutaBuses WHERE IdRutaBuses=@Id",
                        cn,
                        transaccion
                    );

                    cmdInfo.Parameters.AddWithValue("@Id", idRutaBus);

                    cmdInfo.ExecuteNonQuery();

                    // RUTA BUSES
                    SqlCommand cmdRuta = new SqlCommand(
                        "DELETE FROM RutaBuses WHERE IdRutaBuses=@Id",
                        cn,
                        transaccion
                    );

                    cmdRuta.Parameters.AddWithValue("@Id", idRutaBus);

                    cmdRuta.ExecuteNonQuery();

                    // DETALLE BUSES
                    SqlCommand cmdBus = new SqlCommand(
                        "DELETE FROM DetalleBuses WHERE IdDetalleBuses=@Id",
                        cn,
                        transaccion
                    );

                    cmdBus.Parameters.AddWithValue("@Id", idDetalle);

                    cmdBus.ExecuteNonQuery();

                    transaccion.Commit();

                    return true;
                }
                catch
                {
                    transaccion.Rollback();
                    return false;
                }
            }
        }

        // =========================
        // BUSCAR
        // =========================
        public DataTable BuscarUnidad(string texto)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection cn = conexion.AbrirConexion())
            {
                string query = @"
                SELECT 
                    rb.IdRutaBuses,
                    irb.NumeroRuta,
                    db.PlacaVehiculo,
                    db.Marca,
                    db.Modelo,
                    db.EstadoVehiculo,
                    rb.MotoristaNombre
                FROM RutaBuses rb
                INNER JOIN InfoRutaBuses irb
                    ON rb.IdRutaBuses = irb.IdRutaBuses
                INNER JOIN DetalleBuses db
                    ON irb.IdDetalleBuses = db.IdDetalleBuses
                WHERE
                    db.PlacaVehiculo LIKE @texto
                    OR rb.MotoristaNombre LIKE @texto";

                SqlDataAdapter da = new SqlDataAdapter(query, cn);

                da.SelectCommand.Parameters.AddWithValue(
                    "@texto",
                    "%" + texto + "%"
                );

                da.Fill(tabla);
            }

            return tabla;
        }

        // =========================
        // GENERAR IDs
        // =========================
        private int ObtenerUltimoId(
            string tabla,
            string campo,
            SqlConnection cn,
            SqlTransaction transaccion
        )
        {
            string query = $"SELECT ISNULL(MAX({campo}),0)+1 FROM {tabla}";

            SqlCommand cmd = new SqlCommand(query, cn, transaccion);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
