using AVANCE_PED_GS250179_.Datos;
using AVANCE_PED_GS250179_.Modelos;
using AVANCE_PED_GS250179_.Servicio;
using GMap.NET.WindowsForms;
using GMap.NET;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_.Vistas
{
    public partial class Form10 : Form
    {
        public string RecorridoGenerado { get; set; }
        public string PuntoInicio { get; set; }
        public string PuntoFinal { get; set; }

        Point mouseActual = new Point();
        GrafoService grafoService = new GrafoService();
        GMapMarker marcadorOrigen = null;
        int var_control = 0;
        bool modoGrabacion = false;
        List<NodoRuta> rutaEnConstruccion = new List<NodoRuta>();
        // Capas transparentes sobre el mapa
        GMapOverlay capaMarcadores = new GMapOverlay("marcadores");
        GMapOverlay capaRutas = new GMapOverlay("rutas");
        GMapMarker marcadorParaEliminar = null;


        NodoRuta NodoOrigen = null;
        NodoRuta NodoDestino = null;
        public Form10()
        {
            InitializeComponent();
        }

        private void Pizarra_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            // 1. DIBUJAR LÍNEAS EXISTENTES Y SUS PESOS
            foreach (var nodo in grafoService.Nodos)
            {
                foreach (var arista in nodo.ListaAdyacencia)
                {
                    e.Graphics.DrawLine(new Pen(Color.Black, 2), nodo.PosX, nodo.PosY, arista.NodoDestino.PosX, arista.NodoDestino.PosY);

                    int medioX = (nodo.PosX + arista.NodoDestino.PosX) / 2;
                    int medioY = (nodo.PosY + arista.NodoDestino.PosY) / 2;

                    e.Graphics.DrawString(arista.Distancia.ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Blue, medioX, medioY);
                }
            }

            if (var_control == 1 && NodoOrigen != null)
            {
                Pen lapizGuia = new Pen(Color.Gray, 2) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
                e.Graphics.DrawLine(lapizGuia, NodoOrigen.PosX, NodoOrigen.PosY, mouseActual.X, mouseActual.Y);
            }

            int radio = 15;
            foreach (var nodo in grafoService.Nodos)
            {
                Rectangle areaNodo = new Rectangle(nodo.PosX - radio, nodo.PosY - radio, radio * 2, radio * 2);
                e.Graphics.FillEllipse(Brushes.Gold, areaNodo);
                e.Graphics.DrawEllipse(Pens.Black, areaNodo);

                StringFormat formatoTexto = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString(nodo.Valor, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, nodo.PosX, nodo.PosY, formatoTexto);
            }
        }

        private void btnProbar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private NodoRuta DetectarNodo(Point puntoClic)
        {
            int radioTolerancia = 20; // Margen de píxeles para acertar el clic en el círculo

            foreach (var nodo in grafoService.Nodos)
            {
                // Fórmula de distancia euclidiana entre dos puntos
                double distancia = Math.Sqrt(Math.Pow(puntoClic.X - nodo.PosX, 2) + Math.Pow(puntoClic.Y - nodo.PosY, 2));

                if (distancia <= radioTolerancia)
                {
                    return nodo; // Encontró el nodo sobre el que se hizo clic
                }
            }
            return null; // Clic en espacio vacío
        }

        private void Pizarra_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                NodoOrigen = DetectarNodo(e.Location);

                if (NodoOrigen != null)
                {

                    var_control = 1;
                    mouseActual = e.Location;
                }
                else
                {
                    DialogResult respuesta = MessageBox.Show("¿Desea agregar un nuevo punto de ruta aquí?",
                                                             "Nuevo Punto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (respuesta == DialogResult.Yes)
                    {
                        string nombreParada = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nombre de la parada:", "Nueva Parada", "");

                        if (!string.IsNullOrEmpty(nombreParada.Trim()))
                        {
                            if (grafoService.Nodos.Exists(n => n.Valor.ToLower() == nombreParada.Trim().ToLower()))
                            {
                                MessageBox.Show("Este punto de ruta ya existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                NodoRuta nuevoNodo = new NodoRuta(nombreParada.Trim(), e.X, e.Y);
                                grafoService.AgregarVertice(nuevoNodo);
                            }
                        }
                    }
                    var_control = 0;
                }
                Pizarra.Refresh();
            }
        }

        private void Pizarra_MouseMove(object sender, MouseEventArgs e)
        {
            if (var_control == 1)
            {
                mouseActual = e.Location;
                Pizarra.Invalidate();
            }
        }

        private void Pizarra_MouseUp(object sender, MouseEventArgs e)
        {
            if (var_control == 1)
            {
                NodoDestino = DetectarNodo(e.Location);


                if (NodoDestino != null && NodoDestino != NodoOrigen)
                {
                    string pesoStr = Microsoft.VisualBasic.Interaction.InputBox("Ingrese la distancia o tiempo para esta sección (Costo):", "Configurar Ruta", "1");


                    if (double.TryParse(pesoStr, out double distancia) && distancia > 0)
                    {
                        grafoService.AgregarArco(NodoOrigen.Valor, NodoDestino.Valor, distancia);
                    }
                }

                var_control = 0;
                NodoOrigen = null;
                NodoDestino = null;
                Pizarra.Refresh();
            }
        }

        private void btnIniciarGrabacion_Click(object sender, EventArgs e)
        {

            if (grafoService.Nodos.Count < 2)
            {
                MessageBox.Show("Debes crear al menos 2 paradas y unirlas con una línea.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Dictionary<NodoRuta, int> lineasEntrantes = new Dictionary<NodoRuta, int>();

            foreach (var nodo in grafoService.Nodos)
            {
                lineasEntrantes[nodo] = 0;
            }

            foreach (var nodo in grafoService.Nodos)
            {
                foreach (var arista in nodo.ListaAdyacencia)
                {
                    lineasEntrantes[arista.NodoDestino]++;
                }
            }

            NodoRuta nodoActual = grafoService.Nodos.FirstOrDefault(n => lineasEntrantes[n] == 0 && n.ListaAdyacencia.Count > 0);

            if (nodoActual == null)
            {
                MessageBox.Show("No se pudo detectar el inicio de la ruta. Asegúrate de no haber conectado las paradas en un círculo cerrado.", "Error de Ruta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            List<string> paradasOrdenadas = new List<string>();

            while (nodoActual != null)
            {
                paradasOrdenadas.Add(nodoActual.Valor);

                if (nodoActual.ListaAdyacencia.Count > 0)
                {
                    nodoActual = nodoActual.ListaAdyacencia[0].NodoDestino;
                }
                else
                {
                    nodoActual = null;
                }
            }


            PuntoInicio = paradasOrdenadas.First();
            PuntoFinal = paradasOrdenadas.Last();
            RecorridoGenerado = string.Join(" - ", paradasOrdenadas);


            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Form10_Load(object sender, EventArgs e)
        {

            GMap.NET.MapProviders.GMapProvider.UserAgent = "ReiseTransportSystem_Elmer";


            mapaReise.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;


            mapaReise.Position = new GMap.NET.PointLatLng(13.69294, -89.21819);

            mapaReise.MinZoom = 5;
            mapaReise.MaxZoom = 18;
            mapaReise.Zoom = 14;

            mapaReise.DragButton = MouseButtons.Right;
            mapaReise.CanDragMap = true;
            mapaReise.MouseWheelZoomEnabled = true;
            mapaReise.Overlays.Add(capaRutas);
            mapaReise.Overlays.Add(capaMarcadores);
        }

        private void mapaReise_OnMarkerClick(GMap.NET.WindowsForms.GMapMarker item, MouseEventArgs e)
        {
            // SI ES CLIC DERECHO: Mostramos el menú para eliminar
            if (e.Button == MouseButtons.Right)
            {
                marcadorParaEliminar = item;
                // Mostramos el menú justo donde está el mouse
                menuMarcador.Show(mapaReise, e.Location);
            }
            // SI ES CLIC IZQUIERDO: Lógica normal de unión
            else if (e.Button == MouseButtons.Left)
            {
                if (marcadorOrigen == null)
                {
                    marcadorOrigen = item;
                    MessageBox.Show($"Origen: {item.ToolTipText}\nAhora toca el destino.", "Ruta");
                }
                else if (marcadorOrigen != item)
                {
                    grafoService.AgregarArco(marcadorOrigen.ToolTipText, item.ToolTipText, 1);
                    GMapRoute rutaVisual = new GMapRoute("ruta_visual");
                    rutaVisual.Points.Add(marcadorOrigen.Position);
                    rutaVisual.Points.Add(item.Position);
                    rutaVisual.Stroke = new Pen(Color.Blue, 3);
                    capaRutas.Routes.Add(rutaVisual);
                    marcadorOrigen = null;
                    mapaReise.Refresh();
                }
            }

            if (marcadorOrigen == null)
            {
                marcadorOrigen = item;
                MessageBox.Show($"Origen seleccionado: {item.ToolTipText}\nAhora haz clic en el destino.", "Ruta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Si ya teníamos un origen y tocamos un pin diferente, creamos la ruta
                if (marcadorOrigen != item)
                {
                    // 1. Guardamos la conexión en el Cerebro (GrafoService)
                    // Le ponemos peso 1 por defecto (o puedes usar un InputBox para pedir la distancia como antes)
                    grafoService.AgregarArco(marcadorOrigen.ToolTipText, item.ToolTipText, 1);

                    // 2. Dibujamos la línea azul en el mapa real
                    GMapRoute rutaVisual = new GMapRoute("ruta_visual");
                    rutaVisual.Points.Add(marcadorOrigen.Position);
                    rutaVisual.Points.Add(item.Position);

                    // Estilo de la línea
                    rutaVisual.Stroke = new Pen(Color.Blue, 3);

                    // La agregamos a la capa de rutas que creamos antes
                    capaRutas.Routes.Add(rutaVisual);

                    // 3. Limpiamos el origen para que puedas seguir conectando el siguiente par de pines
                    marcadorOrigen = null;
                }
            }
        }

        private void mapaReise_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Magia de GMap: Convertimos el clic de la pantalla a coordenadas GPS exactas
                PointLatLng coordenadas = mapaReise.FromLocalToLatLng(e.X, e.Y);

                // Pedimos el nombre de la parada
                string nombreParada = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nombre de la parada:", "Nueva Parada", "");

                if (!string.IsNullOrWhiteSpace(nombreParada))
                {
                    if (grafoService.Nodos.Exists(n => n.Valor.ToLower() == nombreParada.Trim().ToLower()))
                    {
                        MessageBox.Show("Esta parada ya existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Creamos un PIN ROJO (Estilo Google Maps)
                    GMarkerGoogle marcador = new GMarkerGoogle(coordenadas, GMarkerGoogleType.red_pushpin);
                    marcador.ToolTipText = nombreParada.Trim();
                    marcador.Tag = nombreParada.Trim(); // Guardamos el nombre oculto en el marcador

                    // Lo agregamos a la capa visual
                    capaMarcadores.Markers.Add(marcador);


                    NodoRuta nuevoNodo = new NodoRuta(nombreParada.Trim(), e.X, e.Y);
                    nuevoNodo.Latitud = coordenadas.Lat;
                    nuevoNodo.Longitud = coordenadas.Lng;

                    grafoService.AgregarVertice(nuevoNodo);
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Deseas borrar toda la ruta?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                capaMarcadores.Markers.Clear();
                capaRutas.Routes.Clear();
                grafoService.Nodos.Clear();
                marcadorOrigen = null;
                mapaReise.Refresh();
            }
        }

        private void eliminarPuntoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (marcadorParaEliminar != null)
            {
                string nombre = marcadorParaEliminar.ToolTipText;

                // 1. Eliminar del Grafo (el cerebro)
                var nodo = grafoService.Nodos.FirstOrDefault(n => n.Valor == nombre);
                if (nodo != null) grafoService.Nodos.Remove(nodo);

                // 2. Eliminar de la Capa de Marcadores (lo visual)
                capaMarcadores.Markers.Remove(marcadorParaEliminar);

                // 3. Limpiar rutas conectadas a este punto
                // (Para simplificar, borramos las líneas visuales para que las vuelvas a trazar)
                capaRutas.Routes.Clear();
                marcadorOrigen = null;

                mapaReise.Refresh();
                marcadorParaEliminar = null;
            }
        }
    }
}
