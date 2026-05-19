using System;
using System.Collections.Generic;

namespace AVANCE_PED_GS250179_.Estructuras
{
    public class NodoCompra
    {
        public int IdRuta { get; set; }
        public decimal Precio { get; set; }
        public string NombreRuta { get; set; }
        // 🌟 CLAVE: Propiedad para enlazar la RAM con la fila exacta en la tabla DetalleVenta
        public int IdDetalleVenta { get; set; }
        public NodoCompra Siguiente { get; set; }

        public NodoCompra(int id, decimal precio, string nombre, int idDetalleVenta)
        {
            this.IdRuta = id;
            this.Precio = precio;
            this.NombreRuta = nombre;
            this.IdDetalleVenta = idDetalleVenta;
            this.Siguiente = null;
        }
    }

    public class ColaCompraManual
    {
        private NodoCompra frente;
        private NodoCompra final;

        // Instancia única global en memoria
        public static ColaCompraManual InstanciaCompartida { get; } = new ColaCompraManual();

        public ColaCompraManual()
        {
            frente = null;
            final = null;
        }

        // Encolar de forma manual (FIFO) - Actualizado con el Id relacional
        public void Encolar(int id, decimal precio, string nombre, int idDetalleVenta)
        {
            NodoCompra nuevo = new NodoCompra(id, precio, nombre, idDetalleVenta);
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

        // Desencolar (Remover el frente)
        public NodoCompra Desencolar()
        {
            if (frente == null) return null;

            NodoCompra aux = frente;
            frente = frente.Siguiente;

            if (frente == null) final = null;

            return aux;
        }

        public bool EstaVacia() => frente == null;

        // Recorre los punteros para el GDI+ sin destruir la cola real
        public List<NodoCompra> ObenerListaParaDibujar()
        {
            List<NodoCompra> lista = new List<NodoCompra>();
            NodoCompra actual = frente;
            while (actual != null)
            {
                lista.Add(actual);
                actual = actual.Siguiente;
            }
            return lista;
        }
    }
}