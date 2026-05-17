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
            string login = txtuser.Text;
            string clave = txtpass.Text;
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(clave))
            {
                MessageBox.Show("Complete todos los campos", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                MessageBox.Show("Credenciales incorrectas");
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
    }
}
