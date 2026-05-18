using AVANCE_PED_GS250179_.Modelos;
using AVANCE_PED_GS250179_.Servicio;
using System.Diagnostics.Contracts;

namespace AVANCE_PED_GS250179_
{
    public partial class Form1 : Form
    {

        private static bool MostrarMensaje = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            if (!MostrarMensaje)
            {
                DialogResult r = MessageBox.Show("Bienvenido a REISE, App donde tu seguridad y comodidad es nuestra prioridad" +
                    "\tPresiona Ok para continuar.", "BIENVENIDO/A ;)",
                    MessageBoxButtons.OK);

                /* if (r == DialogResult.OK)
                 {
                     MessageBox.Show("En esta versión beta no necesitas registrarte, presiona el botón de <Iniciar Sesión>", "AVISO",
                         MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 }*/

                MostrarMensaje = true;
            }

        }

        private void btnIS_Click(object sender, EventArgs e)
{
    string login = txtuser.Text.Trim(); // .Trim() elimina espacios accidentales
    string clave = txtpass.Text.Trim();

    if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(clave))
    {
        MessageBox.Show("Complete todos los campos", "Atención",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }

    try
    {
        // INTENTAR AUTENTICAR COMO CLIENTE
        ClienteService clienteService = new ClienteService();
        Cliente clienteLogueado = clienteService.ValidarLogin(login, clave);

        if (clienteLogueado != null)
        {
            MessageBox.Show($"¡Bienvenido/a {clienteLogueado.Nombre}!", "Acceso Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MenuCliente formCliente = new MenuCliente(clienteLogueado.IdCliente);
            formCliente.Show();
            this.Hide();
            return; // Finaliza el método con éxito
        }
    }
    catch (Exception ex)
    {
        // Si hay un error de conexión, nombre de columna inválido, etc., saltará aquí:
        MessageBox.Show("Error interno en Login Cliente:\n" + ex.Message, "Depuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
    }

    // SI NO ENCONTRÓ CLIENTE, INTENTA COMO EMPLEADO
    try
    {
        EmpleadoService service = new EmpleadoService();
        Empleado empLogueado = service.ValidarLogin(login, clave);

        if (empLogueado != null)
        {
            Form2 formAdmin = new Form2(empLogueado.IdEmpleado);
            formAdmin.Show();
            this.Hide(); // Oculta el login
        }
        else
        {
            MessageBox.Show("Credenciales incorrectas", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error interno en Login Empleado:\n" + ex.Message, "Depuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult R = MessageBox.Show("Desea salir del Programa", "SALIR", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (R == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        bool contraseñaVisible = false;
        private void ptojo_Click(object sender, EventArgs e)
        {
            if (contraseñaVisible == true)
            {
                txtpass.PasswordChar = '*';

                ptojo.Image = Properties.Resources.icons8_ojo_cerrado_24;

                contraseñaVisible = false;
            }
            // Si está oculta, la mostramos
            else
            {
                // Quitar la máscara ('\0' significa carácter nulo, o sea, sin máscara)
                txtpass.PasswordChar = '\0';

                // Cambiar la imagen al ojo abierto (reemplaza 'ojo_abierto' por el nombre de tu imagen)
                ptojo.Image = Properties.Resources.icons8_eye_24;

                // Actualizamos el estado
                contraseñaVisible = true;
            }
        }
    }
}
