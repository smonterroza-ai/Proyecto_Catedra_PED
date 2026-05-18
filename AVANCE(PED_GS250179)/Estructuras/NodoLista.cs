using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVANCE_PED_GS250179_.Estructuras
{
    internal class NodoLista
    {
        private object datos;
        public object Datos { get { return datos; } set { datos = value; } }

        private NodoLista siguiente;
        public NodoLista Siguiente { get { return siguiente; } set { siguiente = value; } }

        public NodoLista(object valorDatos) : this(valorDatos, null) { }

        public NodoLista(object valorDatos, NodoLista siguienteNodo)
        {
            datos = valorDatos;
            siguiente = siguienteNodo;
        }
    }
}
