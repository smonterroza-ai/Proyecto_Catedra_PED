using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVANCE_PED_GS250179_.Modelos
{
    internal class TipoVehiculo
    {
        public int IdTipoVehiculo { get; set; }
        public string TipoVehiculoNombre { get; set; }

        public override string ToString()
        {
            return TipoVehiculoNombre;
        }
    }
}
