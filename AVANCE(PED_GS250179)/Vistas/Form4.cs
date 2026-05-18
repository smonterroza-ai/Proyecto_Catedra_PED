using AVANCE_PED_GS250179_.Datos;
using AVANCE_PED_GS250179_.Servicio;
using AVANCE_PED_GS250179_.Vistas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_
{
    public partial class Form4 : Form
    {
        private int? idRutaEditar = null; // Si es null = Añadir, si tiene número = Editar
        private string puntoInicioEditar = "";
        private string puntoFinalEditar = "";
        private string coordenadasGPSEditar = "";
        RutaService _rutaService = new RutaService();
        string inicioRecorridoBD = "";
        string finalRecorridoBD = "";
        public Form4()
        {
            InitializeComponent();
        }

        public void ConfigurarModoEditar(RutaDTO ruta)
        {
            idRutaEditar = ruta.IdRutaBuses;
            txtRuta.Text = ruta.NumeroRuta;
            txtTari.Text = ruta.CostoDelPasaje.ToString();

            puntoInicioEditar = ruta.Inicio;
            puntoFinalEditar = ruta.Final;

            txtRecorrido.Text = _rutaService.ObtenerRecorridoPorIdRuta(ruta.IdRutaBuses);

            
            
            coordenadasGPSEditar = _rutaService.ObtenerCoordenadasGPS(ruta.IdRutaBuses);

            ventanaMapa.ReconstruirMapa(coordenadasGPSEditar);
          
        }

        private static bool MostrarMensaje = false;

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Form3 Rutas = new Form3();
            Rutas.Show();

            this.Hide();


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            if (!MostrarMensaje)
                MessageBox.Show("En esta area, se agregarán lo que son los datos de la nueva ruta que se desea añadir."
                , "Información", MessageBoxButtons.OK);

            MostrarMensaje = true;
        }

        Form10 ventanaMapa = new Form10();
        private void btnAMapa_Click(object sender, EventArgs e)
        {
            if (ventanaMapa.ShowDialog() == DialogResult.OK)
            {

                txtRecorrido.Text = ventanaMapa.DatosMapa.RecorridoGenerado;
                inicioRecorridoBD = ventanaMapa.DatosMapa.PuntoInicio;
                finalRecorridoBD = ventanaMapa.DatosMapa.PuntoFinal;
            }
        }

        private void Confi_R_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRuta.Text) || string.IsNullOrWhiteSpace(txtTari.Text) || string.IsNullOrWhiteSpace(txtRecorrido.Text))
            {
                MessageBox.Show("Por favor llena todos los campos y añade el mapa.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Evaluar si estamos EDITANDO o AGREGANDO
            if (idRutaEditar.HasValue)
            {
                try
                {
                    // ---------------------------------------------------------
                    // MODO EDICIÓN: Definimos qué datos usar (los nuevos del mapa o los viejos de la BD)
                    // ---------------------------------------------------------
                    string inicio = string.IsNullOrEmpty(ventanaMapa.DatosMapa.PuntoInicio) ? puntoInicioEditar : ventanaMapa.DatosMapa.PuntoInicio;
                    string final = string.IsNullOrEmpty(ventanaMapa.DatosMapa.PuntoFinal) ? puntoFinalEditar : ventanaMapa.DatosMapa.PuntoFinal;

                    // AQUÍ AGREGAMOS LA LÓGICA DE LAS COORDENADAS GPS:
                    string coords = string.IsNullOrEmpty(ventanaMapa.DatosMapa.CoordenadasGeneradas) ? coordenadasGPSEditar : ventanaMapa.DatosMapa.CoordenadasGeneradas;

                    decimal tarifa = Convert.ToDecimal(txtTari.Text.Trim());

                    // AQUÍ AGREGAMOS LA VARIABLE 'coords' AL FINAL DEL LLAMADO DEL SERVICIO:
                    if (_rutaService.ActualizarRuta(idRutaEditar.Value, txtRuta.Text.Trim(), tarifa, inicio, final, txtRecorrido.Text.Trim(), coords))
                    {
                        MessageBox.Show("¡La ruta se actualizó exitosamente en la Base de Datos!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {

                Conexion conexion = new Conexion();
                SqlConnection cn = conexion.AbrirConexion();

                try
                {
                    string queryId = "SELECT ISNULL(MAX(IdRecorridoRuta), 0) + 1 FROM RecorridoRuta";
                    SqlCommand cmdId = new SqlCommand(queryId, cn);
                    int nuevoIdRecorrido = Convert.ToInt32(cmdId.ExecuteScalar());

                    string queryRecorrido = "INSERT INTO RecorridoRuta (IdRecorridoRuta, inicio, Final) VALUES (@id, @inicio, @final)";
                    SqlCommand cmdRecorrido = new SqlCommand(queryRecorrido, cn);
                    cmdRecorrido.Parameters.AddWithValue("@id", nuevoIdRecorrido);
                    // AQUÍ TOMAMOS INICIO Y FIN DIRECTAMENTE DEL MAPA:
                    cmdRecorrido.Parameters.AddWithValue("@inicio", ventanaMapa.DatosMapa.PuntoInicio);
                    cmdRecorrido.Parameters.AddWithValue("@final", ventanaMapa.DatosMapa.PuntoFinal);
                    cmdRecorrido.ExecuteNonQuery();

                    // AQUÍ MODIFICAMOS EL INSERT PARA QUE GUARDE LA COLUMNA CoordenadasGPS:
                    string queryInfo = "INSERT INTO InfoRecorridoRuta (IdRecorridoRuta, ParadasRuta, CoordenadasGPS) VALUES (@id, @paradas, @coords)";
                    SqlCommand cmdInfo = new SqlCommand(queryInfo, cn);
                    cmdInfo.Parameters.AddWithValue("@id", nuevoIdRecorrido);
                    cmdInfo.Parameters.AddWithValue("@paradas", txtRecorrido.Text);
                    // AQUÍ MANDAMOS LAS COORDENADAS OCULTAS (O vacío si de casualidad no hay nada):
                    cmdInfo.Parameters.AddWithValue("@coords", ventanaMapa.DatosMapa.CoordenadasGeneradas ?? "");
                    cmdInfo.ExecuteNonQuery();

                    string queryIdRuta = "SELECT ISNULL(MAX(IdRutaBuses), 0) + 1 FROM RutaBuses";
                    SqlCommand cmdIdRuta = new SqlCommand(queryIdRuta, cn);
                    int nuevoIdRuta = Convert.ToInt32(cmdIdRuta.ExecuteScalar());

                    string queryRutaBuses = "INSERT INTO RutaBuses (IdRutaBuses, IdEmpresa, IdRecorridoRuta, CostoDelPasaje) VALUES (@idRuta, 1, @idRecorrido, @costo)";
                    SqlCommand cmdRutaBuses = new SqlCommand(queryRutaBuses, cn);
                    cmdRutaBuses.Parameters.AddWithValue("@idRuta", nuevoIdRuta);
                    cmdRutaBuses.Parameters.AddWithValue("@idRecorrido", nuevoIdRecorrido);
                    cmdRutaBuses.Parameters.AddWithValue("@costo", Convert.ToDecimal(txtTari.Text));
                    cmdRutaBuses.ExecuteNonQuery();

                    string queryInfoRuta = "INSERT INTO InfoRutaBuses (IdRutaBuses, NumeroRuta, MotoristaNombre, IdDetalleBuses) VALUES (@idRuta, @numRuta, 'Pendiente', 1)";
                    SqlCommand cmdInfoRuta = new SqlCommand(queryInfoRuta, cn);
                    cmdInfoRuta.Parameters.AddWithValue("@idRuta", nuevoIdRuta);
                    cmdInfoRuta.Parameters.AddWithValue("@numRuta", txtRuta.Text);
                    cmdInfoRuta.ExecuteNonQuery();

                    MessageBox.Show("¡La ruta se configuró y guardó exitosamente en la Base de Datos!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiamos los campos
                    txtRuta.Clear();
                    txtTari.Clear();
                    txtRecorrido.Clear();

                    // Limpiamos también el objeto del mapa para que quede limpio para la próxima vez
                    ventanaMapa.DatosMapa = new AVANCE_PED_GS250179_.Modelos.Mapa();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conexion.CerrarConexion(cn);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtTari_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Solo permitir un punto decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
       
    }
}
