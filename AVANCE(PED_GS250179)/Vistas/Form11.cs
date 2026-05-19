using AVANCE_PED_GS250179_.Estructuras;
using AVANCE_PED_GS250179_.Modelos;
using AVANCE_PED_GS250179_.Servicio;
using AVANCE_PED_GS250179_.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace AVANCE_PED_GS250179_.Vistas
{
    public partial class Form11 : Form
    {
        EmpleadoService service = new EmpleadoService();

        public Form11()
        {
            InitializeComponent();
        }

        bool contraseñaVisible = false;

        public Form11(int idEmpleado)
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btnguardar_Click(object sender, EventArgs e)
        {

        }

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

            cmbRol.DisplayMember = "Roles";
            cmbRol.ValueMember = "IdRolEmpleado";

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
        }

        private void btnAtras_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombre.Text.Trim() == "" ||
                    txtDUI.Text.Trim() == "" ||
                    txtTelefono.Text.Trim() == "" ||
                    txtCorreo.Text.Trim() == "" ||
                    txtUsuario.Text.Trim() == "" ||
                    txtDireccion.Text.Trim() == "" ||
                    txtContraseña.Text.Trim() == "" ||
                    txtConfirmarContraseña.Text.Trim() == "" ||
                    cmbRol.SelectedIndex == -1)
                {
                    MessageBox.Show("Complete todos los campos");
                    return;
                }

                if (txtContraseña.Text != txtConfirmarContraseña.Text)
                {
                    MessageBox.Show("Las contraseñas no coinciden");
                    return;
                }

                RolEmpleado rolSeleccionado =
                    (RolEmpleado)cmbRol.SelectedItem;

                Empleado emp = new Empleado()
                {
                    Nombre = txtNombre.Text.Trim(),
                    DUI = txtDUI.Text.Trim(),
                    FechaNacimiento = dtpNacimiento.Value,
                    Direccion = txtDireccion.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    FechaContratacion = dtpContratacion.Value,
                    Correo = txtCorreo.Text.Trim(),
                    Usuario = txtUsuario.Text.Trim(),

                    Contraseña = service.HashearContrasena(
                        txtContraseña.Text.Trim()
                    ),

                    IdRolEmpleado =
                        rolSeleccionado.IdRolEmpleado
                };

                bool resultado =
                    service.AgregarEmpleadoCompleto(emp);

                if (resultado)
                {
                    MessageBox.Show(
                        "Empleado agregado correctamente"
                    );

                    Form8 form = new Form8();
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
                MessageBox.Show(
                    "Error: " + ex.Message
                );
            }
        }

        private void Form11_Shown(object sender, EventArgs e)
        {
            RedondearBoton.RedondearBotones(button1, 30);
            RedondearBoton.RedondearBotones(btnRegresar, 30);
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Form8 frm = new Form8();
            frm.Show();

            this.Hide();
        }

        private void ptojo_Click(object sender, EventArgs e)
        {
            if (contraseñaVisible == true)
            {
                txtContraseña.PasswordChar = '*';

                ptojo.Image = Properties.Resources.icons8_eye_24;

                contraseñaVisible = false;
            }
            // Si está oculta, la mostramos
            else
            {
                // Quitar la máscara ('\0' significa carácter nulo, o sea, sin máscara)
                txtContraseña.PasswordChar = '\0';

                // Cambiar la imagen al ojo abierto (reemplaza 'ojo_abierto' por el nombre de tu imagen)
                ptojo.Image = Properties.Resources.icons8_ojo_cerrado_24;

                // Actualizamos el estado
                contraseñaVisible = true;
            }
        }

        private void ptojo2_Click(object sender, EventArgs e)
        {
            if (contraseñaVisible == true)
            {
                txtConfirmarContraseña.PasswordChar = '*';

                ptojo2.Image = Properties.Resources.icons8_eye_24;

                contraseñaVisible = false;
            }
            // Si está oculta, la mostramos
            else
            {
                // Quitar la máscara ('\0' significa carácter nulo, o sea, sin máscara)
                txtConfirmarContraseña.PasswordChar = '\0';

                // Cambiar la imagen al ojo abierto (reemplaza 'ojo_abierto' por el nombre de tu imagen)
                ptojo2.Image = Properties.Resources.icons8_ojo_cerrado_24;

                // Actualizamos el estado
                contraseñaVisible = true;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Ignora cualquier otro símbolo o número
            }
        }

        private void txtDUI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            // Agregar guion automáticamente después de 8 números 
            if (txtDUI.Text.Length == 8 && e.KeyChar != (char)Keys.Back)
            {
                txtDUI.Text += "-";
                txtDUI.SelectionStart = txtDUI.Text.Length;
            }
        }
    }
}
