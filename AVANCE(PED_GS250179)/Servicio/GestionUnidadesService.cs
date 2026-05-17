using AVANCE_PED_GS250179_.Datos;
using AVANCE_PED_GS250179_.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AVANCE_PED_GS250179_.Servicio
{
    internal class GestionUnidadesService
    {
        Conexion conexion = new Conexion();

        // =========================================
        // MOSTRAR UNIDADES
        // =========================================
        public DataTable MostrarUnidades()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection cn = conexion.AbrirConexion())
            {
                string query = @"
                SELECT
                    irb.IdRutaBuses AS Id,
                    irb.NumeroRuta AS Ruta,
                    db.PlacaVehiculo AS Placa,
                    db.Marca,
                    db.Modelo,
                    tv.TipoVehiculo,
                    irb.MotoristaNombre AS Conductor,
                    db.EstadoVehiculo AS Estado
                FROM InfoRutaBuses irb
                INNER JOIN DetalleBuses db
                    ON irb.IdDetalleBuses = db.IdDetalleBuses
                INNER JOIN TipoVehiculo tv
                    ON db.IdTipoVehiculo = tv.IdTipoVehiculo";

                SqlDataAdapter da =
                    new SqlDataAdapter(query, cn);

                da.Fill(tabla);
            }

            return tabla;
        }

        // =========================================
        // AGREGAR UNIDAD
        // =========================================
        public bool AgregarUnidad(GestionUnidades unidad)
        {
            using (SqlConnection cn = conexion.AbrirConexion())
            {
                SqlTransaction transaccion =
                    cn.BeginTransaction();

                try
                {
                    // =====================================
                    // GENERAR IDS
                    // =====================================

                    int idDetalle = ObtenerUltimoId(
                        "DetalleBuses",
                        "IdDetalleBuses",
                        cn,
                        transaccion
                    );

                    int idRuta = ObtenerUltimoId(
                        "InfoRutaBuses",
                        "IdRutaBuses",
                        cn,
                        transaccion
                    );

                    // =====================================
                    // INSERT DETALLE
                    // =====================================

                    string queryDetalle = @"
                    INSERT INTO DetalleBuses
                    (
                        IdDetalleBuses,
                        Marca,
                        Modelo,
                        EstadoVehiculo,
                        PlacaVehiculo,
                        IdTipoVehiculo
                    )
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
                        new SqlCommand(
                            queryDetalle,
                            cn,
                            transaccion
                        );

                    cmdDetalle.Parameters.AddWithValue(
                        "@IdDetalle",
                        idDetalle
                    );

                    cmdDetalle.Parameters.AddWithValue(
                        "@Marca",
                        unidad.Marca
                    );

                    cmdDetalle.Parameters.AddWithValue(
                        "@Modelo",
                        unidad.Modelo
                    );

                    cmdDetalle.Parameters.AddWithValue(
                        "@Estado",
                        unidad.EstadoVehiculo
                    );

                    cmdDetalle.Parameters.AddWithValue(
                        "@Placa",
                        unidad.PlacaVehiculo
                    );

                    cmdDetalle.Parameters.AddWithValue(
                        "@IdTipo",
                        unidad.IdTipoVehiculo
                    );

                    cmdDetalle.ExecuteNonQuery();

                    // =====================================
                    // INSERT INFO RUTA
                    // =====================================

                    string queryInfo = @"
                    INSERT INTO InfoRutaBuses
                    (
                        IdRutaBuses,
                        NumeroRuta,
                        MotoristaNombre,
                        IdDetalleBuses
                    )
                    VALUES
                    (
                        @IdRuta,
                        @NumeroRuta,
                        @Motorista,
                        @IdDetalle
                    )";

                    SqlCommand cmdInfo =
                        new SqlCommand(
                            queryInfo,
                            cn,
                            transaccion
                        );

                    cmdInfo.Parameters.AddWithValue(
                        "@IdRuta",
                        idRuta
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@NumeroRuta",
                        unidad.NumeroRuta
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@Motorista",
                        unidad.MotoristaNombre
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@IdDetalle",
                        idDetalle
                    );

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

        // =========================================
        // EDITAR UNIDAD
        // =========================================
        public bool EditarUnidad(GestionUnidades unidad)
        {
            using (SqlConnection cn = conexion.AbrirConexion())
            {
                SqlTransaction transaccion =
                    cn.BeginTransaction();

                try
                {
                    // =====================================
                    // OBTENER ID DETALLE
                    // =====================================

                    string obtenerDetalle = @"
                    SELECT IdDetalleBuses
                    FROM InfoRutaBuses
                    WHERE IdRutaBuses = @IdRuta";

                    SqlCommand cmdObtener =
                        new SqlCommand(
                            obtenerDetalle,
                            cn,
                            transaccion
                        );

                    cmdObtener.Parameters.AddWithValue(
                        "@IdRuta",
                        unidad.IdRutaBuses
                    );

                    int idDetalle =
                        Convert.ToInt32(
                            cmdObtener.ExecuteScalar()
                        );

                    // =====================================
                    // ACTUALIZAR DETALLE
                    // =====================================

                    string queryDetalle = @"
                    UPDATE DetalleBuses
                    SET
                        Marca = @Marca,
                        Modelo = @Modelo,
                        EstadoVehiculo = @Estado,
                        PlacaVehiculo = @Placa,
                        IdTipoVehiculo = @IdTipo
                    WHERE IdDetalleBuses = @IdDetalle";

                    SqlCommand cmdDetalle =
                        new SqlCommand(
                            queryDetalle,
                            cn,
                            transaccion
                        );

                    cmdDetalle.Parameters.AddWithValue(
                        "@Marca",
                        unidad.Marca
                    );

                    cmdDetalle.Parameters.AddWithValue(
                        "@Modelo",
                        unidad.Modelo
                    );

                    cmdDetalle.Parameters.AddWithValue(
                        "@Estado",
                        unidad.EstadoVehiculo
                    );

                    cmdDetalle.Parameters.AddWithValue(
                        "@Placa",
                        unidad.PlacaVehiculo
                    );

                    cmdDetalle.Parameters.AddWithValue(
                        "@IdTipo",
                        unidad.IdTipoVehiculo
                    );

                    cmdDetalle.Parameters.AddWithValue(
                        "@IdDetalle",
                        idDetalle
                    );

                    cmdDetalle.ExecuteNonQuery();

                    // =====================================
                    // ACTUALIZAR INFO RUTA
                    // =====================================

                    string queryInfo = @"
                    UPDATE InfoRutaBuses
                    SET
                        NumeroRuta = @NumeroRuta,
                        MotoristaNombre = @Motorista
                    WHERE IdRutaBuses = @IdRuta";

                    SqlCommand cmdInfo =
                        new SqlCommand(
                            queryInfo,
                            cn,
                            transaccion
                        );

                    cmdInfo.Parameters.AddWithValue(
                        "@NumeroRuta",
                        unidad.NumeroRuta
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@Motorista",
                        unidad.MotoristaNombre
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@IdRuta",
                        unidad.IdRutaBuses
                    );

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

        // =========================================
        // ELIMINAR UNIDAD
        // =========================================
        public bool EliminarUnidad(int idRutaBus)
        {
            using (SqlConnection cn = conexion.AbrirConexion())
            {
                SqlTransaction transaccion =
                    cn.BeginTransaction();

                try
                {
                    int idDetalle = 0;

                    string obtenerDetalle = @"
                    SELECT IdDetalleBuses
                    FROM InfoRutaBuses
                    WHERE IdRutaBuses = @IdRuta";

                    SqlCommand cmdDetalle =
                        new SqlCommand(
                            obtenerDetalle,
                            cn,
                            transaccion
                        );

                    cmdDetalle.Parameters.AddWithValue(
                        "@IdRuta",
                        idRutaBus
                    );

                    object resultado =
                        cmdDetalle.ExecuteScalar();

                    if (resultado != null)
                    {
                        idDetalle =
                            Convert.ToInt32(resultado);
                    }

                    // ELIMINAR INFO RUTA
                    SqlCommand cmdInfo =
                        new SqlCommand(
                            "DELETE FROM InfoRutaBuses WHERE IdRutaBuses=@Id",
                            cn,
                            transaccion
                        );

                    cmdInfo.Parameters.AddWithValue(
                        "@Id",
                        idRutaBus
                    );

                    cmdInfo.ExecuteNonQuery();

                    // ELIMINAR DETALLE
                    SqlCommand cmdBus =
                        new SqlCommand(
                            "DELETE FROM DetalleBuses WHERE IdDetalleBuses=@Id",
                            cn,
                            transaccion
                        );

                    cmdBus.Parameters.AddWithValue(
                        "@Id",
                        idDetalle
                    );

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

        // =========================================
        // BUSCAR
        // =========================================
        public DataTable BuscarUnidad(string texto)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection cn = conexion.AbrirConexion())
            {
                string query = @"
                SELECT
                    irb.IdRutaBuses AS Id,
                    irb.NumeroRuta AS Ruta,
                    db.PlacaVehiculo AS Placa,
                    db.Marca,
                    db.Modelo,
                    tv.TipoVehiculo,
                    irb.MotoristaNombre AS Conductor,
                    db.EstadoVehiculo AS Estado
                FROM InfoRutaBuses irb
                INNER JOIN DetalleBuses db
                    ON irb.IdDetalleBuses = db.IdDetalleBuses
                INNER JOIN TipoVehiculo tv
                    ON db.IdTipoVehiculo = tv.IdTipoVehiculo
                WHERE
                    db.PlacaVehiculo LIKE @texto
                    OR irb.MotoristaNombre LIKE @texto
                    OR irb.NumeroRuta LIKE @texto
                    OR tv.TipoVehiculo LIKE @texto";

                SqlDataAdapter da =
                    new SqlDataAdapter(query, cn);

                da.SelectCommand.Parameters.AddWithValue(
                    "@texto",
                    "%" + texto + "%"
                );

                da.Fill(tabla);
            }

            return tabla;
        }

        // =========================================
        // OBTENER POR ID
        // =========================================
        public GestionUnidades ObtenerUnidadPorId(
            int idEditar
        )
        {
            GestionUnidades unidad =
                new GestionUnidades();

            using (SqlConnection cn = conexion.AbrirConexion())
            {
                string query = @"
                SELECT
                    irb.IdRutaBuses,
                    irb.NumeroRuta,
                    db.PlacaVehiculo,
                    db.Marca,
                    db.Modelo,
                    irb.MotoristaNombre,
                    db.EstadoVehiculo,
                    db.IdTipoVehiculo
                FROM InfoRutaBuses irb
                INNER JOIN DetalleBuses db
                    ON irb.IdDetalleBuses = db.IdDetalleBuses
                WHERE irb.IdRutaBuses = @Id";

                SqlCommand cmd =
                    new SqlCommand(query, cn);

                cmd.Parameters.AddWithValue(
                    "@Id",
                    idEditar
                );

                SqlDataReader reader =
                    cmd.ExecuteReader();

                if (reader.Read())
                {
                    unidad.IdRutaBuses =
                        reader.GetInt32(0);

                    unidad.NumeroRuta =
                        Convert.ToInt32(reader[1]);

                    unidad.PlacaVehiculo =
                        reader.GetString(2);

                    unidad.Marca =
                        reader.GetString(3);

                    unidad.Modelo =
                        reader.GetString(4);

                    unidad.MotoristaNombre =
                        reader.GetString(5);

                    unidad.EstadoVehiculo =
                        reader.GetString(6);

                    unidad.IdTipoVehiculo =
                        reader.GetInt32(7);
                }

                reader.Close();
            }

            return unidad;
        }

        // =========================================
        // GENERAR IDS
        // =========================================
        private int ObtenerUltimoId(
            string tabla,
            string campo,
            SqlConnection cn,
            SqlTransaction transaccion
        )
        {
            string query =
                $"SELECT ISNULL(MAX({campo}),0)+1 FROM {tabla}";

            SqlCommand cmd =
                new SqlCommand(
                    query,
                    cn,
                    transaccion
                );

            return Convert.ToInt32(
                cmd.ExecuteScalar()
            );
        }

        public List<TipoVehiculo> ObtenerTiposVehiculo()
        {
            List<TipoVehiculo> lista = new List<TipoVehiculo>();

            using (SqlConnection cn = conexion.AbrirConexion())
            {
                string query = "SELECT IdTipoVehiculo, TipoVehiculo FROM TipoVehiculo";

                SqlCommand cmd = new SqlCommand(query, cn);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new TipoVehiculo
                    {
                        IdTipoVehiculo = Convert.ToInt32(dr["IdTipoVehiculo"]),
                        TipoVehiculoNombre = dr["TipoVehiculo"].ToString()
                    });
                }
            }

            return lista;
        }
    }
}
