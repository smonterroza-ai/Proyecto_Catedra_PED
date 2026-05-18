using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVANCE_PED_GS250179_.Modelos
{
    public class Mapa
    {
        public string RecorridoGenerado { get; set; }
        public string PuntoInicio { get; set; }
        public string PuntoFinal { get; set; }
        public string CoordenadasGeneradas { get; set; }

        public Mapa()
        {
            RecorridoGenerado = "";
            PuntoInicio = "";
            PuntoFinal = "";
            CoordenadasGeneradas = "";
        }
    }
}
