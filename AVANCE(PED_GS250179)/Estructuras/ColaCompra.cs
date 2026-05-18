using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVANCE_PED_GS250179_.Estructuras
{
    public class NodoCompra
    {
        public int IdRuta { get; set; }
        public decimal Precio { get; set; }
        public string NombreRuta { get; set; }
        public NodoCompra Siguiente { get; set; }

        public NodoCompra(int id, decimal precio, string nombre)
        {
            this.IdRuta = id;
            this.Precio = precio;
            this.NombreRuta = nombre;
            this.Siguiente = null;
        }
    }

    // 2. La Cola: Manejo manual de Front (Frente) y Rear (Final)
    public class ColaCompraManual
    {
        private NodoCompra frente;
        private NodoCompra final;

        public ColaCompraManual()
        {
            frente = null;
            final = null;
        }

        // Encolar: Insertar al final
        public void Encolar(int id, decimal precio, string nombre)
        {
            NodoCompra nuevo = new NodoCompra(id, precio, nombre);
            if (final == null)
            {
                frente = nuevo;
                final = nuevo;
            }
            else
            {
                final.Siguiente = nuevo;
                final = nuevo;
            }
        }

        // Desencolar: Sacar del frente
        public NodoCompra Desencolar()
        {
            if (frente == null) return null;

            NodoCompra aux = frente;
            frente = frente.Siguiente;

            if (frente == null) final = null;

            return aux;
        }

        public bool EstaVacia() => frente == null;
    }
}
