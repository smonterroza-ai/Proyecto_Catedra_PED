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

        public bool AgregarEmpleadoCompleto(Empleado e)
        {
            try
            {
                using (SqlConnection cn = conexion.AbrirConexion())
                {
                    SqlTransaction tr = cn.BeginTransaction();

                    try
                    {
                        // INFO
                        string q1 = @"
                        INSERT INTO InfoEmpleado
                        (Nombre, DUI, FechaNacimiento, Direccion, Telefono,
                         FechaContratacion, Correo, Usuario, Contraseña)
                        VALUES
                        (@Nombre, @DUI, @FechaNacimiento, @Direccion, @Telefono,
                         @FechaContratacion, @Correo, @Usuario, @Contraseña);
                        SELECT SCOPE_IDENTITY();";

                        SqlCommand cmd1 = new SqlCommand(q1, cn, tr);

                        cmd1.Parameters.AddWithValue("@Nombre", e.Nombre);
                        cmd1.Parameters.AddWithValue("@DUI", e.DUI);
                        cmd1.Parameters.AddWithValue("@FechaNacimiento", e.FechaNacimiento);
                        cmd1.Parameters.AddWithValue("@Direccion", e.Direccion);
                        cmd1.Parameters.AddWithValue("@Telefono", e.Telefono);
                        cmd1.Parameters.AddWithValue("@FechaContratacion", e.FechaContratacion);
                        cmd1.Parameters.AddWithValue("@Correo", e.Correo);
                        cmd1.Parameters.AddWithValue("@Usuario", e.Usuario);
                        cmd1.Parameters.AddWithValue("@Contraseña", e.Contraseña);

                        int id = Convert.ToInt32(cmd1.ExecuteScalar());

                        // EMPLEADO (ROL FK)
                        string q2 = @"
                        INSERT INTO Empleado (IdEmpleado, IdRolEmpleado)
                        VALUES (@IdEmpleado, @IdRolEmpleado)";

                        SqlCommand cmd2 = new SqlCommand(q2, cn, tr);

                        cmd2.Parameters.AddWithValue("@IdEmpleado", id);
                        cmd2.Parameters.AddWithValue("@IdRolEmpleado", e.IdRolEmpleado);

                        cmd2.ExecuteNonQuery();

                        tr.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        MessageBox.Show(ex.Message);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool EditarEmpleado(Empleado e)
        {
            try
            {
                using (SqlConnection cn = conexion.AbrirConexion())
                {
                    string query = @"
                    UPDATE InfoEmpleado
                    SET Nombre=@Nombre,
                        DUI=@DUI,
                        FechaNacimiento=@FechaNacimiento,
                        Direccion=@Direccion,
                        Telefono=@Telefono,
                        FechaContratacion=@FechaContratacion,
                        Correo=@Correo
                    WHERE IdEmpleado=@IdEmpleado";

                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@IdEmpleado", e.IdEmpleado);
                    cmd.Parameters.AddWithValue("@Nombre", e.Nombre);
                    cmd.Parameters.AddWithValue("@DUI", e.DUI);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", e.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Direccion", e.Direccion);
                    cmd.Parameters.AddWithValue("@Telefono", e.Telefono);
                    cmd.Parameters.AddWithValue("@FechaContratacion", e.FechaContratacion);
                    cmd.Parameters.AddWithValue("@Correo", e.Correo);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public Empleado ObtenerEmpleadoPorId(int id)
        {
            Empleado e = null;

            using (SqlConnection cn = conexion.AbrirConexion())
            {
                string query = @"
                SELECT ie.*, em.IdRolEmpleado
                FROM InfoEmpleado ie
                INNER JOIN Empleado em ON ie.IdEmpleado = em.IdEmpleado
                WHERE ie.IdEmpleado=@Id";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    e = new Empleado()
                    {
                        IdEmpleado = (int)dr["IdEmpleado"],
                        Nombre = dr["Nombre"].ToString(),
                        DUI = dr["DUI"].ToString(),
                        FechaNacimiento = (DateTime)dr["FechaNacimiento"],
                        Direccion = dr["Direccion"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        FechaContratacion = (DateTime)dr["FechaContratacion"],
                        Correo = dr["Correo"].ToString(),
                        IdRolEmpleado = (int)dr["IdRolEmpleado"]
                    };
                }
            }

            return e;
        }

        public ListaEnlazada ObtenerRoles()
        {
            ListaEnlazada lista = new ListaEnlazada();

            using (SqlConnection cn = conexion.AbrirConexion())
            {
                string query = "SELECT * FROM RolEmpleado";

                SqlCommand cmd = new SqlCommand(query, cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Agregar(new RolEmpleado()
                    {
                        IdRolEmpleado = Convert.ToInt32(dr["IdRolEmpleado"]),
                        Roles = dr["Roles"].ToString()
                    });
                }
            }

            return lista;
        }

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
