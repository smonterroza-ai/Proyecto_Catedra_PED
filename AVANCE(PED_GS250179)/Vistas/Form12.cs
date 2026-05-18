using AVANCE_PED_GS250179_.Estructuras;
using AVANCE_PED_GS250179_.Modelos;
using AVANCE_PED_GS250179_.Servicio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_.Vistas
{
    public partial class Form12 : Form
    {
        EmpleadoService service = new EmpleadoService();

        private int idEmpleadoEditar;
        private bool esEditar = false;

        public Form12()
        {
            InitializeComponent();
        }

        public Form12(int idEmpleado)
        {
            InitializeComponent();
            idEmpleadoEditar = idEmpleado;
            esEditar = true;
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            CargarRoles();

            if (esEditar)
            {
                Empleado emp = service.ObtenerEmpleadoPorId(idEmpleadoEditar);

                if (emp == null)
                {
                    MessageBox.Show("Empleado no encontrado");
                    return;
                }

                txtNombre.Text = emp.Nombre;
                txtDUI.Text = emp.DUI;
                txtTelefono.Text = emp.Telefono;
                txtCorreo.Text = emp.Correo;
                txtDireccion.Text = emp.Direccion;

                dtpNacimiento.Value = emp.FechaNacimiento;
                dtpContratacion.Value = emp.FechaContratacion;

                foreach (RolEmpleado r in cmbRol.Items)
                {
                    if (r.IdRolEmpleado == emp.IdRolEmpleado)
                    {
                        cmbRol.SelectedItem = r;
                        break;
                    }
                }
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombre.Text.Trim() == "" ||
                    txtDUI.Text.Trim() == "" ||
                    txtTelefono.Text.Trim() == "" ||
                    txtCorreo.Text.Trim() == "" ||
                    txtDireccion.Text.Trim() == "" ||
                    cmbRol.SelectedIndex == -1)
                {
                    MessageBox.Show("Complete los campos");
                    return;
                }

                RolEmpleado rolSeleccionado = (RolEmpleado)cmbRol.SelectedItem;

                Empleado emp = new Empleado()
                {
                    IdEmpleado = idEmpleadoEditar,
                    Nombre = txtNombre.Text.Trim(),
                    DUI = txtDUI.Text.Trim(),
                    FechaNacimiento = dtpNacimiento.Value,
                    Direccion = txtDireccion.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    FechaContratacion = dtpContratacion.Value,
                    Correo = txtCorreo.Text.Trim(),
                    IdRolEmpleado = rolSeleccionado.IdRolEmpleado
                };

                bool resultado = service.EditarEmpleado(emp);

                if (resultado)
                {
                    MessageBox.Show("Empleado editado correctamente");

                    Form8 form = new Form8();
                    form.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Error al editar");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void CargarRoles()
        {
            cmbRol.Items.Clear();

            ListaEnlazada lista = service.ObtenerRoles();

            NodoLista actual = lista.Cabeza;

            while (actual != null)
            {
                cmbRol.Items.Add(actual.Datos);
                actual = actual.Siguiente;
            }

            cmbRol.SelectedIndex = -1;
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Form8 form =
                new Form8();

            form.Show();

            this.Hide();
        }
    }
}
