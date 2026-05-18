using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVANCE_PED_GS250179_.Estructuras
{
    internal class ListaEnlazada : IEnumerable
    {
        private NodoLista cabeza;

        public NodoLista Cabeza => cabeza;

        public void Agregar(object dato)
        {
            NodoLista nuevo = new NodoLista(dato);

            if (cabeza == null)
                cabeza = nuevo;
            else
            {
                NodoLista actual = cabeza;

                while (actual.Siguiente != null)
                    actual = actual.Siguiente;

                actual.Siguiente = nuevo;
            }
        }

        public IEnumerator GetEnumerator()
        {
            NodoLista actual = cabeza;

            while (actual != null)
            {
                yield return actual.Datos;
                actual = actual.Siguiente;
            }
        }
    }
}
