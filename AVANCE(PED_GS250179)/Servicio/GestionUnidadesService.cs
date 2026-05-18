using AVANCE_PED_GS250179_.Datos;
using AVANCE_PED_GS250179_.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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
            DataTable tabla =
                new DataTable();

            using (SqlConnection cn =
                conexion.AbrirConexion())
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
        // AGREGAR
        // =========================================
        public bool AgregarUnidad(
            GestionUnidades unidad
        )
        {
            using (SqlConnection cn =
                conexion.AbrirConexion())
            {
                SqlTransaction transaccion =
                    cn.BeginTransaction();

                try
                {
                    int idDetalle =
                        ObtenerUltimoId(
                            "DetalleBuses",
                            "IdDetalleBuses",
                            cn,
                            transaccion
                        );

                    int idRuta =
                        ObtenerUltimoId(
                            "RutaBuses",
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
                    // INSERT RUTA BUSES
                    // =====================================

                    string queryRuta = @"
                    INSERT INTO RutaBuses
                    (
                        IdRutaBuses,
                        IdEmpresa,
                        IdRecorridoRuta,
                        CostoDelPasaje
                    )
                    VALUES
                    (
                        @IdRuta,
                        @IdEmpresa,
                        @IdRecorrido,
                        @Costo
                    )";

                    SqlCommand cmdRuta =
                        new SqlCommand(
                            queryRuta,
                            cn,
                            transaccion
                        );

                    cmdRuta.Parameters.AddWithValue(
                        "@IdRuta",
                        idRuta
                    );

                    cmdRuta.Parameters.AddWithValue(
                        "@IdEmpresa",
                        1
                    );

                    cmdRuta.Parameters.AddWithValue(
                        "@IdRecorrido",
                        1
                    );

                    cmdRuta.Parameters.AddWithValue(
                        "@Costo",
                        0.25
                    );

                    cmdRuta.ExecuteNonQuery();

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
                catch (Exception ex)
                {
                    transaccion.Rollback();

                    MessageBox.Show(
                        ex.ToString()
                    );

                    return false;
                }
            }
        }

        // =========================================
        // EDITAR
        // =========================================
        public bool EditarUnidad(
            GestionUnidades unidad
        )
        {
            using (SqlConnection cn =
                conexion.AbrirConexion())
            {
                SqlTransaction transaccion =
                    cn.BeginTransaction();

                try
                {
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

                    object resultado =
                        cmdObtener.ExecuteScalar();

                    if (resultado == null)
                    {
                        throw new Exception(
                            "No existe el detalle"
                        );
                    }

                    int idDetalle =
                        Convert.ToInt32(resultado);

                    // =====================================
                    // UPDATE DETALLE
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
                    // UPDATE INFO RUTA
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
                catch (Exception ex)
                {
                    transaccion.Rollback();

                    MessageBox.Show(
                        ex.ToString()
                    );

                    return false;
                }
            }
        }

        // =========================================
        // ELIMINAR
        // =========================================
        public bool EliminarUnidad(
            int idRutaBus
        )
        {
            using (SqlConnection cn =
                conexion.AbrirConexion())
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
                            Convert.ToInt32(
                                resultado
                            );
                    }

                    // =====================================
                    // DELETE INFO RUTA
                    // =====================================

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

                    // =====================================
                    // DELETE RUTA BUSES
                    // =====================================

                    SqlCommand cmdRuta =
                        new SqlCommand(
                            "DELETE FROM RutaBuses WHERE IdRutaBuses=@Id",
                            cn,
                            transaccion
                        );

                    cmdRuta.Parameters.AddWithValue(
                        "@Id",
                        idRutaBus
                    );

                    cmdRuta.ExecuteNonQuery();

                    // =====================================
                    // DELETE DETALLE
                    // =====================================

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
                catch (Exception ex)
                {
                    transaccion.Rollback();

                    MessageBox.Show(
                        ex.ToString()
                    );

                    return false;
                }
            }
        }

        // =========================================
        // BUSCAR
        // =========================================
        public DataTable BuscarUnidad(
            string texto
        )
        {
            DataTable tabla =
                new DataTable();

            using (SqlConnection cn =
                conexion.AbrirConexion())
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
                    OR irb.NumeroRuta LIKE @texto
                    OR irb.MotoristaNombre LIKE @texto";

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

            using (SqlConnection cn =
                conexion.AbrirConexion())
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
                        Convert.ToInt32(reader[0]);

                    unidad.NumeroRuta =
                        reader[1].ToString();

                    unidad.PlacaVehiculo =
                        reader[2].ToString();

                    unidad.Marca =
                        reader[3].ToString();

                    unidad.Modelo =
                        reader[4].ToString();

                    unidad.MotoristaNombre =
                        reader[5].ToString();

                    unidad.EstadoVehiculo =
                        reader[6].ToString();

                    unidad.IdTipoVehiculo =
                        Convert.ToInt32(reader[7]);
                }

                reader.Close();
            }

            return unidad;
        }

        // =========================================
        // IDS
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

        // =========================================
        // TIPOS VEHÍCULO
        // =========================================
        public List<TipoVehiculo>
            ObtenerTiposVehiculo()
        {
            List<TipoVehiculo> lista =
                new List<TipoVehiculo>();

            using (SqlConnection cn =
                conexion.AbrirConexion())
            {
                string query = @"
                SELECT
                    IdTipoVehiculo,
                    TipoVehiculo
                FROM TipoVehiculo";

                SqlCommand cmd =
                    new SqlCommand(query, cn);

                SqlDataReader dr =
                    cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(
                        new TipoVehiculo
                        {
                            IdTipoVehiculo =
                                Convert.ToInt32(
                                    dr["IdTipoVehiculo"]
                                ),

                            TipoVehiculoNombre =
                                dr["TipoVehiculo"].ToString()
                        }
                    );
                }
            }

            return lista;
        }

        // =========================================
        // CONDUCTORES
        // =========================================
        public List<Empleado>
            ObtenerConductores()
        {
            List<Empleado> lista =
                new List<Empleado>();

            using (SqlConnection cn =
                conexion.AbrirConexion())
            {
                string query = @"
                SELECT
                    IdEmpleado,
                    Nombre
                FROM InfoEmpleado";

                SqlCommand cmd =
                    new SqlCommand(query, cn);

                SqlDataReader dr =
                    cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(
                        new Empleado
                        {
                            IdEmpleado =
                                Convert.ToInt32(
                                    dr["IdEmpleado"]
                                ),

                            Nombre =
                                dr["Nombre"].ToString()
                        }
                    );
                }
            }

            return lista;
        }

        // =========================================
        // RUTAS
        // =========================================
        public List<Ruta>
            ObtenerRutas()
        {
            List<Ruta> lista =
                new List<Ruta>();

            using (SqlConnection cn =
                conexion.AbrirConexion())
            {
                string query = @"
                SELECT
                    IdRutaBuses,
                    NumeroRuta
                FROM InfoRutaBuses";

                SqlCommand cmd =
                    new SqlCommand(query, cn);

                SqlDataReader dr =
                    cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(
                        new Ruta
                        {
                            IdRutaBuses =
                                Convert.ToInt32(
                                    dr["IdRutaBuses"]
                                ),

                            NumeroRuta =
                                dr["NumeroRuta"].ToString()
                        }
                    );
                }
            }

            return lista;
        }
    }
}