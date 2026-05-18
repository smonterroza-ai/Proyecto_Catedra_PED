using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVANCE_PED_GS250179_.Modelos
{
    internal class RolEmpleado
    {
        public int IdRolEmpleado { get; set; }

        public string Roles { get; set; }

        public override string ToString()
        {
            return Roles;
        }
    }
}
