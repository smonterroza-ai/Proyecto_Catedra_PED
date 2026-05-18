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
    public partial class Form11 : Form
    {
        EmpleadoService service =
            new EmpleadoService();

        private int idEmpleadoEditar = 0;

        private bool esEditar = false;

        public Form11()
        {
            InitializeComponent();
        }

        public Form11(int idEmpleado)
        {
            InitializeComponent();

            idEmpleadoEditar = idEmpleado;

            esEditar = true;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            try
            {
                // ============================
                // VALIDACIONES
                // ============================

                if (
                    txtNombre.Text.Trim() == "" ||
                    txtDUI.Text.Trim() == "" ||
                    txtTelefono.Text.Trim() == "" ||
                    txtCorreo.Text.Trim() == "" ||
                    txtUsuario.Text.Trim() == "" ||
                    txtDireccion.Text.Trim() == "" ||
                    txtContraseña.Text.Trim() == "" ||
                    txtConfirmarContraseña.Text.Trim() == "" ||
                    cmbRol.SelectedIndex == -1
                )
                {
                    MessageBox.Show(
                        "Complete todos los campos"
                    );

                    return;
                }

                // ============================
                // VALIDAR CONTRASEÑAS
                // ============================

                if (
                    txtContraseña.Text.Trim() !=
                    txtConfirmarContraseña.Text.Trim()
                )
                {
                    MessageBox.Show(
                        "Las contraseñas no coinciden"
                    );

                    return;
                }

                // ============================
                // OBJETO
                // ============================

                Empleado empleado =
                    new Empleado();

                if (esEditar)
                {
                    empleado.IdEmpleado =
                        idEmpleadoEditar;
                }

                empleado.Nombre =
                    txtNombre.Text.Trim();

                empleado.DUI =
                    txtDUI.Text.Trim();

                empleado.Telefono =
                    txtTelefono.Text.Trim();

                empleado.Correo =
                    txtCorreo.Text.Trim();

                empleado.Usuario =
                    txtUsuario.Text.Trim();

                empleado.Direccion =
                    txtDireccion.Text.Trim();

                empleado.Contraseña =
                    txtContraseña.Text.Trim();

                empleado.FechaNacimiento =
                    dtpNacimiento.Value;

                empleado.FechaContratacion =
                    dtpContratacion.Value;

                empleado.IdRolEmpleado =
                    Convert.ToInt32(
                        cmbRol.SelectedValue
                    );

                // ============================
                // GUARDAR / EDITAR
                // ============================

                bool resultado =
                    esEditar
                    ? service.EditarEmpleado(
                        empleado
                    )
                    : service.AgregarEmpleado(
                        empleado
                    );

                if (resultado)
                {
                    MessageBox.Show(
                        esEditar
                        ? "Empleado editado correctamente"
                        : "Empleado agregado correctamente"
                    );

                    Form8 form =
                        new Form8();

                    form.Show();

                    this.Hide();
                }
                else
                {
                    MessageBox.Show(
                        "Error al guardar"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: " + ex.Message
                );
            }
        }

        // ============================
        // CARGAR ROLES
        // ============================

        private void CargarRoles()
        {
            cmbRol.Items.Clear();

            ListaEnlazada lista = service.ObtenerRoles();

            NodoLista actual = lista.Cabeza;

            while (actual != null)
            {
                RolEmpleado r = (RolEmpleado)actual.Datos;

                cmbRol.Items.Add(r);

                actual = actual.Siguiente;
            }

            cmbRol.SelectedIndex = -1;
        }

        private void btnAtras_Click(
            object sender,
            EventArgs e
        )
        {
            Form8 form =
                new Form8();

            form.Show();

            this.Hide();
        }

        private void Form11_Load(object sender, EventArgs e)
        {
            CargarRoles();

            // ============================
            // EDITAR
            // ============================

            if (esEditar)
            {
                Empleado empleado =
                    service.ObtenerEmpleadoPorId(
                        idEmpleadoEditar
                    );

                if (empleado == null)
                {
                    MessageBox.Show(
                        "No se encontró el empleado"
                    );

                    return;
                }

                // ============================
                // TEXTBOX
                // ============================

                txtNombre.Text =
                    empleado.Nombre;

                txtDUI.Text =
                    empleado.DUI;

                txtTelefono.Text =
                    empleado.Telefono;

                txtCorreo.Text =
                    empleado.Correo;

                txtUsuario.Text =
                    empleado.Usuario;

                txtDireccion.Text =
                    empleado.Direccion;

                txtContraseña.Text = "";

                txtConfirmarContraseña.Text = "";

                // ============================
                // FECHAS
                // ============================

                if (empleado.FechaNacimiento > dtpNacimiento.MinDate)
                {
                    dtpNacimiento.Value =
                        empleado.FechaNacimiento;
                }
                else
                {
                    dtpNacimiento.Value =
                        DateTime.Now;
                }

                if (empleado.FechaContratacion > dtpContratacion.MinDate)
                {
                    dtpContratacion.Value =
                        empleado.FechaContratacion;
                }
                else
                {
                    dtpContratacion.Value =
                        DateTime.Now;
                }

                // ============================
                // COMBOBOX
                // ============================

                cmbRol.SelectedValue =
                    empleado.IdRolEmpleado;
            }
        }

        private void btnAtras_Click_1(object sender, EventArgs e)
        {
            Form8 frm = new Form8();
            frm.Show();

            this.Hide();
        }
    }
}
