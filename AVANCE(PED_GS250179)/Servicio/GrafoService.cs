using AVANCE_PED_GS250179_.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVANCE_PED_GS250179_.Servicio
{
    internal class GrafoService
    {

        public List<NodoRuta> Nodos { get; set; } 

        public GrafoService()
        {
            Nodos = new List<NodoRuta>(); 
        }

        public void AgregarVertice(NodoRuta nuevoNodo) 
        {
            Nodos.Add(nuevoNodo);
        }

        public bool AgregarArco(string origen, string destino, double distancia)
        {
            NodoRuta vOrigen = Nodos.Find(v => v.Valor == origen);
            NodoRuta vDestino = Nodos.Find(v => v.Valor == destino);

            if (vOrigen != null && vDestino != null)
            {
                // Pasamos la variable 'distancia' en lugar de 'peso'
                vOrigen.ListaAdyacencia.Add(new AristaRuta(vDestino, distancia));
                return true;
            }
            return false;
        }
        public List<NodoRuta> CalcularRutaMasCorta(string nombreOrigen, string nombreDestino)
        {
            var ruta = new List<NodoRuta>();
            var distancias = new Dictionary<string, double>();
            var padres = new Dictionary<string, NodoRuta>();
            var noVisitados = new List<NodoRuta>();

            // Inicialización: Ponemos todas las distancias al infinito
            foreach (var nodo in Nodos)
            {
                distancias[nodo.Valor] = double.MaxValue;
                padres[nodo.Valor] = null;
                noVisitados.Add(nodo);
            }

            // La distancia al punto de partida es 0
            distancias[nombreOrigen] = 0;

            while (noVisitados.Count > 0)
            {
                // Ordenamos para analizar siempre el nodo más cercano primero
                noVisitados.Sort((x, y) => distancias[x.Valor].CompareTo(distancias[y.Valor]));
                NodoRuta actual = noVisitados[0];
                noVisitados.Remove(actual);

                // Si ya llegamos al destino, no necesitamos seguir buscando
                if (actual.Valor == nombreDestino) break;

                // Si la distancia más corta es infinito, significa que están en islas separadas
                if (distancias[actual.Valor] == double.MaxValue) break;

                // Analizamos los vecinos del nodo actual
                foreach (var arista in actual.ListaAdyacencia)
                {
                    NodoRuta vecino = arista.NodoDestino;
                    if (noVisitados.Contains(vecino))
                    {
                        // Calculamos cuánto cuesta llegar a este vecino pasando por el nodo actual
                        double nuevaDistancia = distancias[actual.Valor] + arista.Distancia;

                        // Si encontramos un atajo, lo guardamos
                        if (nuevaDistancia < distancias[vecino.Valor])
                        {
                            distancias[vecino.Valor] = nuevaDistancia;
                            padres[vecino.Valor] = actual; // Recordamos de dónde vinimos
                        }
                    }
                }
            }

            
            NodoRuta paso = Nodos.Find(n => n.Valor == nombreDestino);

            if (paso != null && (padres[paso.Valor] != null || paso.Valor == nombreOrigen))
            {
                while (paso != null)
                {
                    ruta.Insert(0, paso); // Lo insertamos al inicio de la lista
                    paso = padres[paso.Valor];
                }
            }

            return ruta; 
        }
    }
}
