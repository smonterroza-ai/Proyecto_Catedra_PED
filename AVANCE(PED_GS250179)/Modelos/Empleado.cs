using AVANCE_PED_GS250179_.Datos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVANCE_PED_GS250179_.Modelos
{
    internal class Empleado
    {
        public int IdEmpleado { get; set; }

        public string Nombre { get; set; }

        public string DUI { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public DateTime FechaContratacion { get; set; }

        public string Correo { get; set; }

        public string Usuario { get; set; }

        public string Contrasena { get; set; }

        public int IdRolEmpleado { get; set; }

        public string NombreRol { get; set; }
        public string Contraseña { get; internal set; }
    }
}
