using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVANCE_PED_GS250179_.Modelos
{
    internal class NodoRuta
    {
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Valor { get; set; } 
        public List<AristaRuta> ListaAdyacencia { get; set; } 
        
        public int DistanciaNodo { get; set; }
        public bool Visitado { get; set; }
        public NodoRuta Padre { get; set; }

        public int PosX { get; set; }
        public int PosY { get; set; }

        public NodoRuta(string valor, int x, int y)
        {
            Valor = valor;
            PosX = x;
            PosY = y;
            ListaAdyacencia = new List<AristaRuta>();
            Visitado = false; 
        }
    }
}
