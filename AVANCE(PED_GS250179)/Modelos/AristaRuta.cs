using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVANCE_PED_GS250179_.Modelos
{
    internal class AristaRuta
    {
        public NodoRuta NodoDestino { get; set; }

        public double Distancia { get; set; }

        public AristaRuta(NodoRuta destino, double distancia)
        {
            NodoDestino = destino;
            Distancia = distancia;
        }
    }
}
