using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AVANCE_PED_GS250179_.Modelos;
using AVANCE_PED_GS250179_.Servicio;

namespace AVANCE_PED_GS250179_
{
    public partial class Form7 : Form
    {
        GestionUnidadesService service = new GestionUnidadesService();

        private int idRutaEditar = 0;
        private bool esEditar = false;

        public Form7()
        {
            InitializeComponent();
        }

        public Form7(int idRutaBuses)
        {
            InitializeComponent();
            idRutaEditar = idRutaBuses;
            esEditar = true;
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            CargarTiposVehiculo();

            cmbEstado.Items.Clear();
            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Mantenimiento");
            cmbEstado.Items.Add("Inactivo");

            cmbEstado.SelectedIndex = -1;
            CargarRutas();
            CargarConductores();

            if (esEditar)
            {
                GestionUnidades unidad =
                    service.ObtenerUnidadPorId(idRutaEditar);

                if (unidad == null)
                {
                    MessageBox.Show("No se encontró la unidad");
                    return;
                }

                txtPlaca.Text = unidad.PlacaVehiculo;
                cmbRuta.Text = unidad.NumeroRuta.ToString();
                cmbConductor.Text = unidad.MotoristaNombre;
                cmbEstado.Text = unidad.EstadoVehiculo;
                txtMarca.Text = unidad.Marca;
                txtModelo.Text = unidad.Modelo;

                cmbTipoVehiculo.SelectedValue = unidad.IdTipoVehiculo;
                cmbRuta.SelectedValue = unidad.IdRutaBuses;
                cmbConductor.SelectedValue = unidad.IdEmpleado;
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Form6 Uni = new Form6();
            Uni.Show();

            this.Hide();
        }

        private void Confi_U_Click(object sender, EventArgs e)
        {
            try
            {
                // VALIDACIÓN
                if (txtPlaca.Text.Trim() == "" ||
                    cmbRuta.Text.Trim() == "" ||
                    cmbConductor.Text.Trim() == "" ||
                    cmbEstado.Text.Trim() == "" ||
                    txtMarca.Text.Trim() == "" ||
                    txtModelo.Text.Trim() == "" ||
                    cmbTipoVehiculo.SelectedIndex == -1)
                {
                    MessageBox.Show("Complete todos los campos");
                    return;
                }

                GestionUnidades unidad = new GestionUnidades();

                if (esEditar)
                {
                    unidad.IdRutaBuses = idRutaEditar;
                }

                unidad.PlacaVehiculo = txtPlaca.Text.Trim();
                unidad.NumeroRuta = cmbRuta.Text.Trim();
                unidad.MotoristaNombre = cmbConductor.Text.Trim();
                unidad.EstadoVehiculo = cmbEstado.Text;
                unidad.Marca = txtMarca.Text.Trim();
                unidad.Modelo = txtModelo.Text.Trim();

                unidad.IdTipoVehiculo =
                    Convert.ToInt32(cmbTipoVehiculo.SelectedValue);

                bool resultado = esEditar
                    ? service.EditarUnidad(unidad)
                    : service.AgregarUnidad(unidad);

                if (resultado)
                {
                    MessageBox.Show("Guardado correctamente");

                    Form6 form = new Form6();
                    form.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Error al guardar");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void CargarTiposVehiculo()
        {
            var lista = service.ObtenerTiposVehiculo();

            cmbTipoVehiculo.DataSource = lista;
            cmbTipoVehiculo.DisplayMember = "TipoVehiculoNombre";
            cmbTipoVehiculo.ValueMember = "IdTipoVehiculo";
            cmbTipoVehiculo.SelectedIndex = -1;
        }

        private void CargarRutas()
        {
            var lista =
                service.ObtenerRutas();

            cmbRuta.DataSource =
                lista;

            cmbRuta.DisplayMember =
                "NumeroRuta";

            cmbRuta.ValueMember =
                "IdRutaBuses";

            cmbRuta.SelectedIndex = -1;
        }

        private void CargarConductores()
        {
            var lista =
                service.ObtenerConductores();

            cmbConductor.DataSource =
                lista;

            cmbConductor.DisplayMember =
                "Nombre";

            cmbConductor.ValueMember =
                "IdEmpleado";

            cmbConductor.SelectedIndex = -1;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtRuta_TextChanged(object sender, EventArgs e)
        {
            
        }

    }
}
