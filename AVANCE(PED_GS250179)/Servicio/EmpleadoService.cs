using AVANCE_PED_GS250179_.Datos;
using AVANCE_PED_GS250179_.Estructuras;
using AVANCE_PED_GS250179_.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AVANCE_PED_GS250179_.Servicio
{
    internal class EmpleadoService
    {
        Conexion conexion = new Conexion();

        public string HashearContrasena(string contrasena)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(contrasena);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public Empleado ValidarLogin(string correo, string contrasena)
        {
            Empleado empleado = null;
            SqlConnection cn = conexion.AbrirConexion();

            try
            {
                
                string query = @"
        SELECT 
            ie.IdEmpleado,     -- Posición 0
            ie.Nombre,         -- Posición 1
            ie.Correo,         -- Posición 2
            ie.Usuario,        -- Posición 3
            r.IdRolEmpleado,   -- Posición 4
            r.Roles            -- Posición 5
        FROM InfoEmpleado ie
        INNER JOIN Empleado e ON ie.IdEmpleado = e.IdEmpleado
        INNER JOIN RolEmpleado r ON e.IdRolEmpleado = r.IdRolEmpleado
        WHERE ie.Usuario = @usuario AND ie.Contraseña = @contra";
                

                SqlCommand cmd = new SqlCommand(query, cn);

                cmd.Parameters.AddWithValue("@usuario", correo);
                cmd.Parameters.AddWithValue("@contra", HashearContrasena(contrasena));

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Empaquetamos los datos usando los nuevos números de posición
                    empleado = new Empleado
                    {
                        IdEmpleado = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Correo = reader.GetString(2),
                        Usuario = reader.GetString(3),
                        IdRolEmpleado = reader.GetInt32(4),
                        NombreRol = reader.GetString(5)
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar login: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion(cn);
            }

            return empleado;
        }

        // =========================================
        // MOSTRAR
        // =========================================

        public DataTable MostrarEmpleados()
        {
            DataTable tabla =
                new DataTable();

            using (SqlConnection cn =
                conexion.AbrirConexion())
            {
                string query = @"
                SELECT
                    e.IdEmpleado,
                    ie.Nombre,
                    ie.DUI,
                    ie.Telefono,
                    ie.Correo,
                    ie.Usuario,
                    r.Roles
                FROM Empleado e
                INNER JOIN InfoEmpleado ie
                    ON e.IdEmpleado = ie.IdEmpleado
                INNER JOIN RolEmpleado r
                    ON e.IdRolEmpleado = r.IdRolEmpleado";

                SqlDataAdapter da =
                    new SqlDataAdapter(query, cn);

                da.Fill(tabla);
            }

            return tabla;
        }

        // =========================================
        // AGREGAR
        // =========================================

        public bool AgregarEmpleado(
            Empleado empleado
        )
        {
            using (SqlConnection cn =
                conexion.AbrirConexion())
            {
                SqlTransaction transaccion =
                    cn.BeginTransaction();

                try
                {
                    int idEmpleado =
                        ObtenerUltimoId(
                            "Empleado",
                            "IdEmpleado",
                            cn,
                            transaccion
                        );

                    // =========================
                    // INSERT EMPLEADO
                    // =========================

                    string queryEmpleado = @"
                    INSERT INTO Empleado
                    (
                        IdEmpleado,
                        IdRolEmpleado
                    )
                    VALUES
                    (
                        @IdEmpleado,
                        @IdRol
                    )";

                    SqlCommand cmdEmpleado =
                        new SqlCommand(
                            queryEmpleado,
                            cn,
                            transaccion
                        );

                    cmdEmpleado.Parameters.AddWithValue(
                        "@IdEmpleado",
                        idEmpleado
                    );

                    cmdEmpleado.Parameters.AddWithValue(
                        "@IdRol",
                        empleado.IdRolEmpleado
                    );

                    cmdEmpleado.ExecuteNonQuery();

                    // =========================
                    // INSERT INFO
                    // =========================

                    string queryInfo = @"
                    INSERT INTO InfoEmpleado
                    (
                        IdEmpleado,
                        Nombre,
                        DUI,
                        FechaNacimiento,
                        Direccion,
                        Telefono,
                        FechaContratacion,
                        Correo,
                        Usuario,
                        Contraseña
                    )
                    VALUES
                    (
                        @IdEmpleado,
                        @Nombre,
                        @DUI,
                        @FechaNacimiento,
                        @Direccion,
                        @Telefono,
                        @FechaContratacion,
                        @Correo,
                        @Usuario,
                        @Contraseña
                    )";

                    SqlCommand cmdInfo =
                        new SqlCommand(
                            queryInfo,
                            cn,
                            transaccion
                        );

                    cmdInfo.Parameters.AddWithValue(
                        "@IdEmpleado",
                        idEmpleado
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@Nombre",
                        empleado.Nombre
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@DUI",
                        empleado.DUI
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@FechaNacimiento",
                        empleado.FechaNacimiento
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@Direccion",
                        empleado.Direccion
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@Telefono",
                        empleado.Telefono
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@FechaContratacion",
                        empleado.FechaContratacion
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@Correo",
                        empleado.Correo
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@Usuario",
                        empleado.Usuario
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@Contraseña",
                        HashearContrasena(
                            empleado.Contraseña
                        )
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

        public bool EditarEmpleado(
            Empleado empleado
        )
        {
            using (SqlConnection cn =
                conexion.AbrirConexion())
            {
                SqlTransaction transaccion =
                    cn.BeginTransaction();

                try
                {
                    // =========================
                    // UPDATE EMPLEADO
                    // =========================

                    string queryEmpleado = @"
                    UPDATE Empleado
                    SET
                        IdRolEmpleado = @IdRol
                    WHERE IdEmpleado = @IdEmpleado";

                    SqlCommand cmdEmpleado =
                        new SqlCommand(
                            queryEmpleado,
                            cn,
                            transaccion
                        );

                    cmdEmpleado.Parameters.AddWithValue(
                        "@IdRol",
                        empleado.IdRolEmpleado
                    );

                    cmdEmpleado.Parameters.AddWithValue(
                        "@IdEmpleado",
                        empleado.IdEmpleado
                    );

                    cmdEmpleado.ExecuteNonQuery();

                    // =========================
                    // UPDATE INFO
                    // =========================

                    string queryInfo = @"
                    UPDATE InfoEmpleado
                    SET
                        Nombre = @Nombre,
                        DUI = @DUI,
                        FechaNacimiento = @FechaNacimiento,
                        Direccion = @Direccion,
                        Telefono = @Telefono,
                        FechaContratacion = @FechaContratacion,
                        Correo = @Correo,
                        Usuario = @Usuario,
                        Contraseña = @Contraseña
                    WHERE IdEmpleado = @IdEmpleado";

                    SqlCommand cmdInfo =
                        new SqlCommand(
                            queryInfo,
                            cn,
                            transaccion
                        );

                    cmdInfo.Parameters.AddWithValue(
                        "@Nombre",
                        empleado.Nombre
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@DUI",
                        empleado.DUI
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@FechaNacimiento",
                        empleado.FechaNacimiento
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@Direccion",
                        empleado.Direccion
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@Telefono",
                        empleado.Telefono
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@FechaContratacion",
                        empleado.FechaContratacion
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@Correo",
                        empleado.Correo
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@Usuario",
                        empleado.Usuario
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@Contraseña",
                        HashearContrasena(
                            empleado.Contraseña
                        )
                    );

                    cmdInfo.Parameters.AddWithValue(
                        "@IdEmpleado",
                        empleado.IdEmpleado
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
        // ELIMINAR EMPLEADO
        // =========================================
        public bool EliminarEmpleado(
            int idEmpleado
        )
        {
            using (SqlConnection cn =
                conexion.AbrirConexion())
            {
                SqlTransaction transaccion =
                    cn.BeginTransaction();

                try
                {
                    SqlCommand cmdInfo =
                        new SqlCommand(
                            "DELETE FROM InfoEmpleado WHERE IdEmpleado=@Id",
                            cn,
                            transaccion
                        );

                    cmdInfo.Parameters.AddWithValue(
                        "@Id",
                        idEmpleado
                    );

                    cmdInfo.ExecuteNonQuery();

                    SqlCommand cmdEmpleado =
                        new SqlCommand(
                            "DELETE FROM Empleado WHERE IdEmpleado=@Id",
                            cn,
                            transaccion
                        );

                    cmdEmpleado.Parameters.AddWithValue(
                        "@Id",
                        idEmpleado
                    );

                    cmdEmpleado.ExecuteNonQuery();

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
        // BUSCAR EMPLEADOS
        // =========================================
        public DataTable BuscarEmpleado(
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
                    e.IdEmpleado AS ID,
                    ie.Nombre,
                    ie.DUI,
                    ie.Telefono,
                    ie.Correo,
                    ie.Usuario,
                    r.Roles AS Rol
                FROM Empleado e
                INNER JOIN InfoEmpleado ie
                    ON e.IdEmpleado = ie.IdEmpleado
                INNER JOIN RolEmpleado r
                    ON e.IdRolEmpleado = r.IdRolEmpleado
                WHERE
                    ie.Nombre LIKE @texto
                    OR ie.DUI LIKE @texto
                    OR ie.Correo LIKE @texto
                    OR ie.Usuario LIKE @texto";

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

        public Empleado ObtenerEmpleadoPorId(
    int idEmpleado
)
        {
            Empleado empleado = null;

            using (SqlConnection cn =
                conexion.AbrirConexion())
            {
                string query = @"
        SELECT
            ie.IdEmpleado,
            ie.Nombre,
            ie.DUI,
            ie.FechaNacimiento,
            ie.Direccion,
            ie.Telefono,
            ie.FechaContratacion,
            ie.Correo,
            ie.Usuario,
            e.IdRolEmpleado
        FROM InfoEmpleado ie
        INNER JOIN Empleado e
            ON ie.IdEmpleado = e.IdEmpleado
        WHERE ie.IdEmpleado = @IdEmpleado";

                SqlCommand cmd =
                    new SqlCommand(query, cn);

                cmd.Parameters.AddWithValue(
                    "@IdEmpleado",
                    idEmpleado
                );

                SqlDataReader reader =
                    cmd.ExecuteReader();

                if (reader.Read())
                {
                    empleado = new Empleado();

                    empleado.IdEmpleado =
                        Convert.ToInt32(
                            reader["IdEmpleado"]
                        );

                    empleado.Nombre =
                        reader["Nombre"].ToString();

                    empleado.DUI =
                        reader["DUI"].ToString();

                    empleado.Direccion =
                        reader["Direccion"].ToString();

                    empleado.Telefono =
                        reader["Telefono"].ToString();

                    empleado.Correo =
                        reader["Correo"].ToString();

                    empleado.Usuario =
                        reader["Usuario"].ToString();

                    empleado.IdRolEmpleado =
                        Convert.ToInt32(
                            reader["IdRolEmpleado"]
                        );

                    empleado.FechaNacimiento =
                        Convert.ToDateTime(
                            reader["FechaNacimiento"]
                        );

                    empleado.FechaContratacion =
                        Convert.ToDateTime(
                            reader["FechaContratacion"]
                        );
                }

                reader.Close();
            }

            return empleado;
        }

        // =========================================
        // ROLES
        // =========================================

        public ListaEnlazada ObtenerRoles()
        {
            ListaEnlazada lista = new ListaEnlazada();

            using (SqlConnection cn = conexion.AbrirConexion())
            {
                string query = @"
            SELECT IdRolEmpleado, Roles
            FROM RolEmpleado";

                SqlCommand cmd = new SqlCommand(query, cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    RolEmpleado r = new RolEmpleado
                    {
                        IdRolEmpleado = Convert.ToInt32(dr["IdRolEmpleado"]),
                        Roles = dr["Roles"].ToString()
                    };

                    lista.Agregar(r);
                }
            }

            return lista;
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
    }
}
