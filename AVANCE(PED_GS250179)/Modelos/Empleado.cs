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
        public string Correo { get; set; }
        public string Usuario { get; set; }     
        public int IdRolEmpleado { get; set; }
        public string NombreRol { get; set; }
    }
}
