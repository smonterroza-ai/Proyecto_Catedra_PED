using AVANCE_PED_GS250179_.Vistas;
using System.Data.SqlClient;
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
        private Button btnAbrirValidador;
        private int rolUsuarioActual;
        private static bool MostrarMensaje = false;

        public Form2(int idRol)
        {
            InitializeComponent();
            this.rolUsuarioActual = idRol;

            // Bloqueo estricto: por defecto se oculta todo. Solo el Admin (1) puede verlo.
            if (idRol != 1)
            {
                btnT.Visible = false;
                ptConductor.Visible = false;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CargarMetricasHoy();

            // Si no es administrador, se activa el entorno de empleado y el monitor de grafos
            if (rolUsuarioActual != 1)
            {
                CentrarBotonesEmpleado();

                if (btnAbrirValidador == null)
                {
                    CrearBotonEnlaceValidador();
                }
            }
        }

        private void CargarMetricasHoy()
        {
            Conexion conexion = new Conexion();
            SqlConnection cn = conexion.AbrirConexion();

            try
            {
                string queryPasajeros = @"
                    SELECT ISNULL(SUM(cp.CantidadAComprar), 0) 
                    FROM DetalleVenta dv
                    INNER JOIN CompraPasajes cp ON dv.IdCompraPasajes = cp.IdCompraPasajes
                    WHERE CAST(dv.Hora AS DATE) = CAST(GETDATE() AS DATE) AND dv.Estado = 'Aprobado'";

                using (SqlCommand cmdPasajeros = new SqlCommand(queryPasajeros, cn))
                {
                    int totalPasajeros = Convert.ToInt32(cmdPasajeros.ExecuteScalar());
                    lblPasajeros.Text = totalPasajeros.ToString();
                }

                string queryRecaudacion = @"
                    SELECT ISNULL(SUM(cp.TotalApagar), 0) 
                    FROM DetalleVenta dv
                    INNER JOIN CompraPasajes cp ON dv.IdCompraPasajes = cp.IdCompraPasajes
                    WHERE CAST(dv.Hora AS DATE) = CAST(GETDATE() AS DATE) AND dv.Estado = 'Aprobado'";

                using (SqlCommand cmdRecaudacion = new SqlCommand(queryRecaudacion, cn))
                {
                    decimal totalRecaudacion = Convert.ToDecimal(cmdRecaudacion.ExecuteScalar());
                    lblRecaudacion.Text = totalRecaudacion.ToString("0.00");
                }
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

        private void CentrarBotonesEmpleado()
        {
            if (rolUsuarioActual != 1)
            {
                int centroFormulario = this.ClientSize.Width / 2;
                int espacioEntreBotones = 45;

                ptCliente.Left = centroFormulario - (ptCliente.Width / 2);
                btnR.Left = ptCliente.Left - btnR.Width - espacioEntreBotones;
                btnU.Left = ptCliente.Right + espacioEntreBotones;

                int alturaUnificada = ptCliente.Top;
                btnR.Top = alturaUnificada;
                btnU.Top = alturaUnificada;
            }
        }

        private void CrearBotonEnlaceValidador()
        {
            btnAbrirValidador = new Button();
            btnAbrirValidador.Text = "MONITOR DE GRAFOS";
            btnAbrirValidador.Size = new Size(220, 45);

            btnAbrirValidador.Left = this.ClientSize.Width - btnAbrirValidador.Width - 25;
            btnAbrirValidador.Top = this.ClientSize.Height - 75;

            btnAbrirValidador.BackColor = Color.FromArgb(0, 184, 148);
            btnAbrirValidador.ForeColor = Color.White;
            btnAbrirValidador.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnAbrirValidador.FlatStyle = FlatStyle.Flat;
            btnAbrirValidador.FlatAppearance.BorderSize = 0;
            btnAbrirValidador.Cursor = Cursors.Hand;

            btnAbrirValidador.Click += (s, e) => {
                int idRutaDelConductor = 1;

                ValidarQrForm pantallaGrafo = new ValidarQrForm(idRutaDelConductor);
                pantallaGrafo.Owner = this;
                pantallaGrafo.Show();
                this.Hide();
            };

            this.Controls.Add(btnAbrirValidador);
            btnAbrirValidador.BringToFront();
        }

        private void btnR_Click(object sender, EventArgs e)
        {
            Form3 ruta = new Form3(this.rolUsuarioActual);
            ruta.Show(this);
            this.Hide();
        }

        private void btnT_Click(object sender, EventArgs e)
        {
            Form5 Tran = new Form5();
            Tran.Show(this);
            this.Hide();
        }

        private void btnU_Click(object sender, EventArgs e)
        {
            Form6 Uni = new Form6(this.rolUsuarioActual);
            Uni.Show(this);
            this.Hide();
        }

        private void ptConductor_Click(object sender, EventArgs e)
        {
            Form8 Conductor = new Form8();
            Conductor.Show(this);
            this.Hide();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de cerrar sesión?", "CERRAR SESIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                Form1 login = new Form1();
                login.Show();
                this.Close();
            }
        }

        private void ptCliente_Click(object sender, EventArgs e)
        {
            Form13 cliente = new Form13();
            cliente.Show(this);
            this.Hide();
        }
    }
}