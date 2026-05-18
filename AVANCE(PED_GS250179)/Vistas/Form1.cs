using AVANCE_PED_GS250179_.Modelos;
using AVANCE_PED_GS250179_.Servicio;
using AVANCE_PED_GS250179_.Vistas; // Namespace donde se aloja tu RegistroForm
using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_
{
    public partial class Form1 : Form
    {
        private static bool MostrarMensaje = false;

        // Declaramos el botón físico de registro para Reise
        private Button btnIrARegistro;

        public Form1()
        {
            InitializeComponent();

            // Inyectamos el botón en la interfaz al inicializar el componente
            AgregarBotonRegistro();
        }

        // Método encargado de renderizar y posicionar el botón de Crear Cuenta
        private void AgregarBotonRegistro()
        {
            btnIrARegistro = new Button
            {
                Text = "CREAR CUENTA",
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                BackColor = Color.White,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(130, 35),

                // Posición matemática exacta: a la par (izquierda) de tu botón btnSalir
                Location = new Point(btnSalir.Location.X - 145, btnSalir.Location.Y),
                Cursor = Cursors.Hand
            };

            // Estilo plano y limpio que hace juego con el botón SALIR de Reise
            btnIrARegistro.FlatAppearance.BorderSize = 1;
            btnIrARegistro.FlatAppearance.BorderColor = Color.Black;

            // Vinculamos el evento Click para congelar el Login y abrir el Registro
            btnIrARegistro.Click += BtnIrARegistro_Click;

            // Agregamos el control a la colección del formulario
            this.Controls.Add(btnIrARegistro);

            // SUPER CRUCIAL: Fuerza al botón a ponerse al frente de capas/imágenes ocultas
            btnIrARegistro.BringToFront();
        }

        // Evento que gestiona la apertura modal del registro de usuarios
        private void BtnIrARegistro_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ocultamos el Login temporalmente para un flujo limpio

            using (RegistroForm frmRegistro = new RegistroForm())
            {
                // Abre el RegistroForm y detiene la ejecución aquí hasta que se cierre
                frmRegistro.ShowDialog();
            }

            this.Show(); // Al cerrar la ventana de registro, el login reaparece automáticamente
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!MostrarMensaje)
            {
                DialogResult r = MessageBox.Show("Bienvenido a REISE, App donde tu seguridad y comodidad es nuestra prioridad" +
                    "\tPresiona Ok para continuar.", "BIENVENIDO/A ;)",
                    MessageBoxButtons.OK);

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
                ptojo.Image = Properties.Resources.icons8_eye_24;
                contraseñaVisible = false;
            }
            else
            {
                txtpass.PasswordChar = '\0';
                ptojo.Image = Properties.Resources.icons8_ojo_cerrado_24;
                contraseñaVisible = true;
            }
        }
    }
}