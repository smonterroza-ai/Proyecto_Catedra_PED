using AVANCE_PED_GS250179_.Vistas;
using System.Data.SqlClient;
using AVANCE_PED_GS250179_.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_
{
    public partial class Form2 : Form
    {
        public Form2(int idRol)
        {
            InitializeComponent();

            if (idRol == 2)
            {
                // Aquí pones el nombre de los botones ocultas
                btnT.Visible = false;
                ptConductor.Visible = false;
            }
        }

        private static bool MostrarMensaje = false;

        private void btnR_Click(object sender, EventArgs e)
        {
            Form3 ruta = new Form3();
            ruta.Show(this);
            this.Hide();



        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de cerrar sesión?", "CERRAR SESIÓN", MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                Form1 login = new Form1();
                login.Show();
                this.Close();
            }
        }

        private void CargarMetricasHoy()
        {
            Conexion conexion = new Conexion();
            SqlConnection cn = conexion.AbrirConexion();

            try
            {
                // 1. Consulta para Pasajeros de Hoy
                string queryPasajeros = @"
            SELECT ISNULL(SUM(cp.CantidadAComprar), 0) 
            FROM DetalleVenta dv
            INNER JOIN CompraPasajes cp ON dv.IdCompraPasajes = cp.IdCompraPasajes
            WHERE CAST(dv.Hora AS DATE) = CAST(GETDATE() AS DATE) AND dv.Estado = 'Aprobado'";

                SqlCommand cmdPasajeros = new SqlCommand(queryPasajeros, cn);
                int totalPasajeros = Convert.ToInt32(cmdPasajeros.ExecuteScalar());

                // Lo mostramos en la etiqueta
                lblPasajeros.Text = totalPasajeros.ToString();

                // 2. Consulta para Recaudación de Hoy
                string queryRecaudacion = @"
            SELECT ISNULL(SUM(cp.TotalApagar), 0) 
            FROM DetalleVenta dv
            INNER JOIN CompraPasajes cp ON dv.IdCompraPasajes = cp.IdCompraPasajes
            WHERE CAST(dv.Hora AS DATE) = CAST(GETDATE() AS DATE) AND dv.Estado = 'Aprobado'";

                SqlCommand cmdRecaudacion = new SqlCommand(queryRecaudacion, cn);
                decimal totalRecaudacion = Convert.ToDecimal(cmdRecaudacion.ExecuteScalar());

                // Lo mostramos en la etiqueta con formato de dinero ($0.00)
                lblRecaudacion.Text = "$" + totalRecaudacion.ToString("0.00");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las métricas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.CerrarConexion(cn);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CargarMetricasHoy();
        }

        private void btnT_Click(object sender, EventArgs e)
        {
            Form5 Tran = new Form5();
            Tran.Show(this);

            this.Hide();
        }

        private void btnU_Click(object sender, EventArgs e)
        {
            Form6 Uni = new Form6();
            Uni.Show(this);

            this.Hide();
        }

        private void ptConductor_Click(object sender, EventArgs e)
        {
            Form8 Conductor = new Form8();
            Conductor.Show(this);

            this.Hide();
        }

        private void ptCliente_Click(object sender, EventArgs e)
        {
            Form13 cliente = new Form13();
            cliente.Show(this);
            
            this.Hide();
        }
    }
}
