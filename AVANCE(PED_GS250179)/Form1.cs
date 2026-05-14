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

                if (r == DialogResult.OK)
                {
                    MessageBox.Show("En esta versión beta no necesitas registrarte, presiona el botón de <Iniciar Sesión>", "AVISO",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                MostrarMensaje = true;
            }

        }

        private void btnIS_Click(object sender, EventArgs e)
        {
            Form2 menu = new Form2();
            menu.Show();

            this.Hide();

            
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
