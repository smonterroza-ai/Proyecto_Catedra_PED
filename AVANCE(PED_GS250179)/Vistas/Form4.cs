using AVANCE_PED_GS250179_.Datos;
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
        string inicioRecorridoBD = "";
        string finalRecorridoBD = "";
        public Form4()
        {
            InitializeComponent();
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

        private void btnAMapa_Click(object sender, EventArgs e)
        {
            Form10 ventanaMapa = new Form10();

            
            if (ventanaMapa.ShowDialog() == DialogResult.OK)
            {

                txtRecorrido.Text = ventanaMapa.RecorridoGenerado;
                inicioRecorridoBD = ventanaMapa.PuntoInicio;
                finalRecorridoBD = ventanaMapa.PuntoFinal;
            }

        }

        private void Confi_R_Click(object sender, EventArgs e)
        {
            // 1. Validar que no haya campos vacíos
            if (string.IsNullOrWhiteSpace(txtRuta.Text) || string.IsNullOrWhiteSpace(txtTari.Text) || string.IsNullOrWhiteSpace(txtRecorrido.Text))
            {
                MessageBox.Show("Por favor llena todos los campos y añade el mapa.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                cmdRecorrido.Parameters.AddWithValue("@inicio", inicioRecorridoBD);
                cmdRecorrido.Parameters.AddWithValue("@final", finalRecorridoBD);
                cmdRecorrido.ExecuteNonQuery();

                string queryInfo = "INSERT INTO InfoRecorridoRuta (IdRecorridoRuta, ParadasRuta) VALUES (@id, @paradas)";
                SqlCommand cmdInfo = new SqlCommand(queryInfo, cn);
                cmdInfo.Parameters.AddWithValue("@id", nuevoIdRecorrido);
                cmdInfo.Parameters.AddWithValue("@paradas", txtRecorrido.Text);
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

                txtRuta.Clear();
                txtTari.Clear();
                txtRecorrido.Clear();
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

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
