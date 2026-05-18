using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private static bool MostrarMensaje = false;

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Form3 Rutas = new Form3();
            Rutas.Show();

            this.Hide();


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            if (!MostrarMensaje)
                MessageBox.Show("En esta area, se agregarán lo que son los datos de la nueva ruta que se desea añadir."
                , "Información", MessageBoxButtons.OK);

            MostrarMensaje = true;
        }

        private void btnAMapa_Click(object sender, EventArgs e)
        {

            MessageBox.Show("No se puede acceder a este apartado, no ha sido desarrollado.");

        }

        private void Confi_R_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No se Crear una Ruta, ya que el sistema no está conectado con la Base de Datos.",
                "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
