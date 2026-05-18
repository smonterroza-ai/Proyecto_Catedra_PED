using AVANCE_PED_GS250179_.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVANCE_PED_GS250179_.Servicio
{
    internal class MapaService
    {
        public List<NodoRuta> DecodificarCoordenadasGPS(string datosGPS)
        {
            List<NodoRuta> listaNodos = new List<NodoRuta>();

            if (string.IsNullOrEmpty(datosGPS)) return listaNodos;

            string[] paradas = datosGPS.Split('|');

            foreach (string parada in paradas)
            {
                string[] partes = parada.Split(':');
                string nombre = partes[0];
                string[] gps = partes[1].Split(',');

                double lat = Convert.ToDouble(gps[0], System.Globalization.CultureInfo.InvariantCulture);
                double lng = Convert.ToDouble(gps[1], System.Globalization.CultureInfo.InvariantCulture);

                NodoRuta nodo = new NodoRuta(nombre, 0, 0);
                nodo.Latitud = lat;
                nodo.Longitud = lng;

                listaNodos.Add(nodo);
            }

            return listaNodos;
        }
    }
}
