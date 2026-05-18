using AVANCE_PED_GS250179_.Estructuras;
using AVANCE_PED_GS250179_.Modelos;
using AVANCE_PED_GS250179_.Servicio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_
{
    public partial class Form7 : Form
    {
        GestionUnidadesService service = new GestionUnidadesService();

        private int idRutaEditar = 0;
        private bool esEditar = false;

        ListaEnlazada listaRutas;

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
            CargarConductores();
            CargarRutas();

            cmbEstado.Items.Clear();

            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Mantenimiento");
            cmbEstado.Items.Add("Inactivo");

            cmbEstado.SelectedIndex = -1;

            if (esEditar)
            {
                GestionUnidades unidad =
                    service.ObtenerUnidadPorId(
                        idRutaEditar
                    );

                if (unidad == null)
                {
                    MessageBox.Show(
                        "No se encontró la unidad"
                    );

                    return;
                }

                // TEXTBOX
                txtPlaca.Text =
                    unidad.PlacaVehiculo;

                txtMarca.Text =
                    unidad.Marca;

                txtModelo.Text =
                    unidad.Modelo;

                // COMBOBOX
                cmbEstado.Text =
                    unidad.EstadoVehiculo;

                foreach (TipoVehiculo t in cmbTipoVehiculo.Items)
                {
                    if (t.IdTipoVehiculo == unidad.IdTipoVehiculo)
                    {
                        cmbTipoVehiculo.SelectedItem = t;
                        break;
                    }
                }

                // RUTA
                foreach (Ruta r in cmbRuta.Items)
                {
                    if (r.IdRutaBuses == unidad.IdRutaBuses)
                    {
                        cmbRuta.SelectedItem = r;
                        break;
                    }
                }

                for (int i = 0; i < cmbConductor.Items.Count; i++)
                {
                    Empleado emp =
                        (Empleado)cmbConductor.Items[i];

                    if (emp.Nombre ==
                        unidad.MotoristaNombre)
                    {
                        cmbConductor.SelectedIndex = i;
                        break;
                    }
                }
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
                if (txtPlaca.Text.Trim() == "" ||
                    txtMarca.Text.Trim() == "" ||
                    txtModelo.Text.Trim() == "" ||
                    cmbEstado.SelectedIndex == -1 ||
                    cmbRuta.SelectedIndex == -1 ||
                    cmbConductor.SelectedIndex == -1 ||
                    cmbTipoVehiculo.SelectedIndex == -1)
                {
                    MessageBox.Show("Complete todos los campos");
                    return;
                }

                GestionUnidades unidad = new GestionUnidades();

                unidad.PlacaVehiculo = txtPlaca.Text.Trim();
                unidad.Marca = txtMarca.Text.Trim();
                unidad.Modelo = txtModelo.Text.Trim();
                unidad.EstadoVehiculo = cmbEstado.Text;

                Ruta ruta = cmbRuta.SelectedItem as Ruta;

                if (ruta == null)
                {
                    MessageBox.Show("Seleccione una ruta");
                    return;
                }

                unidad.IdRutaBuses = ruta.IdRutaBuses;
                unidad.NumeroRuta = ruta.NumeroRuta;

                Empleado emp = cmbConductor.SelectedItem as Empleado;

                if (emp == null)
                {
                    MessageBox.Show("Seleccione un conductor");
                    return;
                }

                unidad.IdEmpleado = emp.IdEmpleado;
                unidad.MotoristaNombre = emp.Nombre;

                TipoVehiculo tipo = cmbTipoVehiculo.SelectedItem as TipoVehiculo;

                if (tipo == null)
                {
                    MessageBox.Show("Seleccione un tipo de vehículo");
                    return;
                }

                unidad.IdTipoVehiculo = tipo.IdTipoVehiculo;
                unidad.TipoVehiculo = tipo.TipoVehiculoNombre;

                bool resultado;

                if (esEditar)
                {
                    unidad.IdRutaBuses = idRutaEditar;
                    resultado = service.EditarUnidad(unidad);
                }
                else
                {
                    resultado = service.AgregarUnidad(unidad);
                }

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
            cmbTipoVehiculo.Items.Clear();

            ListaEnlazada lista = service.ObtenerTiposVehiculo();

            NodoLista actual = lista.Cabeza;

            while (actual != null)
            {
                TipoVehiculo t = (TipoVehiculo)actual.Datos;

                cmbTipoVehiculo.Items.Add(t);

                actual = actual.Siguiente;
            }

            cmbTipoVehiculo.SelectedIndex = -1;
        }

        private void CargarConductores()
        {
            cmbConductor.Items.Clear();

            ListaEnlazada lista = service.ObtenerConductores();

            NodoLista actual = lista.Cabeza;

            while (actual != null)
            {
                Empleado e = (Empleado)actual.Datos;

                cmbConductor.Items.Add(e);

                actual = actual.Siguiente;
            }

            cmbConductor.SelectedIndex = -1;
        }

        private void CargarRutas()
        {
            cmbRuta.Items.Clear();

            listaRutas = service.ObtenerRutas();

            NodoLista actual = listaRutas.Cabeza;

            while (actual != null)
            {
                Ruta r = actual.Datos as Ruta;

                if (r != null)
                {
                    cmbRuta.Items.Add(r);
                }

                actual = actual.Siguiente;
            }

            cmbRuta.SelectedIndex = -1;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtRuta_TextChanged(object sender, EventArgs e)
        {
            
        }

    }
}
