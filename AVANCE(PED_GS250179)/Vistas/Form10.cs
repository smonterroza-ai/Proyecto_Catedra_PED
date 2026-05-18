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
        public Mapa DatosMapa { get; set; } = new Mapa();

        Point mouseActual = new Point();
        GrafoService grafoService = new GrafoService();
        GMapMarker marcadorOrigen = null;
        int var_control = 0;

        // Capas para el mapa
        GMapOverlay capaMarcadores = new GMapOverlay("marcadores");
        GMapOverlay capaRutas = new GMapOverlay("rutas");
        GMapMarker marcadorParaEliminar = null;

        NodoRuta NodoOrigen = null;
        NodoRuta NodoDestino = null;

        public string puntoInicioEditar { get; set; } = "";
        public string puntoFinalEditar { get; set; } = "";
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
            int radioTolerancia = 20;
            foreach (var nodo in grafoService.Nodos)
            {
                double distancia = Math.Sqrt(Math.Pow(puntoClic.X - nodo.PosX, 2) + Math.Pow(puntoClic.Y - nodo.PosY, 2));
                if (distancia <= radioTolerancia) return nodo;
            }
            return null;
        }

        public void ReconstruirMapa(string datosGPS)
        {
            capaMarcadores.Markers.Clear();
            capaRutas.Routes.Clear();
            grafoService.Nodos.Clear();

            MapaService mapaSvc = new MapaService();
            List<NodoRuta> nodosRecuperados = mapaSvc.DecodificarCoordenadasGPS(datosGPS);

            if (nodosRecuperados.Count == 0) return;

            GMapMarker marcadorAnterior = null;

            foreach (NodoRuta nodo in nodosRecuperados)
            {
                PointLatLng coordenada = new PointLatLng(nodo.Latitud, nodo.Longitud);

                // 1. Pintar pin rojo
                var marcador = new GMarkerGoogle(coordenada, GMarkerGoogleType.red_pushpin);
                marcador.ToolTipText = nodo.Valor;
                capaMarcadores.Markers.Add(marcador);

                // 2. Insertar al grafo interno
                grafoService.AgregarVertice(nodo);

                // 3. Conectar líneas visuales con el marcador anterior
                if (marcadorAnterior != null)
                {
                    grafoService.AgregarArco(marcadorAnterior.ToolTipText, nodo.Valor, 1);
                    var route = GMap.NET.MapProviders.OpenStreetMapProvider.Instance.GetRoute(marcadorAnterior.Position, coordenada, false, false, 14);

                    if (route != null)
                    {
                        var rutaVisual = new GMapRoute(route.Points, "ruta_recuperada");
                        rutaVisual.Stroke = new Pen(Color.Blue, 3);
                        capaRutas.Routes.Add(rutaVisual);
                    }
                }
                marcadorAnterior = marcador;
            }

            if (marcadorAnterior != null) mapaReise.Position = marcadorAnterior.Position;
            mapaReise.Refresh();
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
            foreach (var nodo in grafoService.Nodos) lineasEntrantes[nodo] = 0;

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
            List<string> listaCoordenadas = new List<string>();

            while (nodoActual != null)
            {
                paradasOrdenadas.Add(nodoActual.Valor);

                string lat = nodoActual.Latitud.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string lng = nodoActual.Longitud.ToString(System.Globalization.CultureInfo.InvariantCulture);
                listaCoordenadas.Add($"{nodoActual.Valor}:{lat},{lng}");

                if (nodoActual.ListaAdyacencia.Count > 0)
                    nodoActual = nodoActual.ListaAdyacencia[0].NodoDestino;
                else
                    nodoActual = null;
            }

            // Guardado directo en las propiedades del Modelo
            DatosMapa.PuntoInicio = paradasOrdenadas.First();
            DatosMapa.PuntoFinal = paradasOrdenadas.Last();
            DatosMapa.RecorridoGenerado = string.Join(" - ", paradasOrdenadas);
            DatosMapa.CoordenadasGeneradas = string.Join("|", listaCoordenadas);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Form10_Load(object sender, EventArgs e)
        {

            GMap.NET.MapProviders.GMapProvider.UserAgent = "ReiseTransportSystem_Elmer";
            mapaReise.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;

            mapaReise.Position = new PointLatLng(13.69294, -89.21819);
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
            if (e.Button == MouseButtons.Right)
            {
                marcadorParaEliminar = item;
                menuMarcador.Show(mapaReise, e.Location);
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (marcadorOrigen == null)
                {
                    marcadorOrigen = item;
                    MessageBox.Show($"Origen seleccionado: {item.ToolTipText}\nAhora haz clic en el destino.", "Ruta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (marcadorOrigen != item)
                {
                    grafoService.AgregarArco(marcadorOrigen.ToolTipText, item.ToolTipText, 1);

                    var route = GMap.NET.MapProviders.OpenStreetMapProvider.Instance.GetRoute(
                        marcadorOrigen.Position,
                        item.Position,
                        false,
                        false,
                        (int)mapaReise.Zoom
                    );

                    if (route != null)
                    {
                        GMapRoute rutaVisual = new GMapRoute(route.Points, "ruta_real");
                        rutaVisual.Stroke = new Pen(Color.Blue, 3);
                        capaRutas.Routes.Add(rutaVisual);
                    }
                    else
                    {
                        GMapRoute rutaRecta = new GMapRoute("ruta_recta");
                        rutaRecta.Points.Add(marcadorOrigen.Position);
                        rutaRecta.Points.Add(item.Position);
                        rutaRecta.Stroke = new Pen(Color.Blue, 3);
                        capaRutas.Routes.Add(rutaRecta);
                    }

                    marcadorOrigen = null;
                    mapaReise.Refresh();
                }
            }
        }

        private void mapaReise_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PointLatLng coordenadas = mapaReise.FromLocalToLatLng(e.X, e.Y);
                string nombreParada = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nombre de la parada:", "Nueva Parada", "");

                if (!string.IsNullOrWhiteSpace(nombreParada))
                {
                    if (grafoService.Nodos.Exists(n => n.Valor.ToLower() == nombreParada.Trim().ToLower()))
                    {
                        MessageBox.Show("Esta parada ya existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    GMarkerGoogle marcador = new GMarkerGoogle(coordenadas, GMarkerGoogleType.red_pushpin);
                    marcador.ToolTipText = nombreParada.Trim();
                    marcador.Tag = nombreParada.Trim();

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
            if (MessageBox.Show("¿Deseas borrar toda la ruta?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                capaMarcadores.Markers.Clear();
                capaRutas.Routes.Clear();
                grafoService.Nodos.Clear();
                marcadorOrigen = null;
                mapaReise.Refresh();

                // Blanqueamos el modelo de datos
                DatosMapa = new Modelos.Mapa();

                // Eliminamos a distancia el texto reflejado en el formulario principal
                foreach (Form formularioAbierto in Application.OpenForms)
                {
                    Control[] cajaRecorrido = formularioAbierto.Controls.Find("txtRecorrido", true);

                    if (cajaRecorrido.Length > 0)
                    {
                        cajaRecorrido[0].Text = "";
                        break;
                    }
                }
            }
        
        }

        private void eliminarPuntoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (marcadorParaEliminar != null)
            {
                string nombre = marcadorParaEliminar.ToolTipText;

                var nodo = grafoService.Nodos.FirstOrDefault(n => n.Valor == nombre);
                if (nodo != null) grafoService.Nodos.Remove(nodo);

                capaMarcadores.Markers.Remove(marcadorParaEliminar);

                capaRutas.Routes.Clear();
                marcadorOrigen = null;

                mapaReise.Refresh();
                marcadorParaEliminar = null;
            }
        }
    }
}
