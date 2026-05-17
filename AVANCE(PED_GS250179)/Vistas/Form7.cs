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
        public Form7()
        {
            InitializeComponent();
        }

        private static bool MostrarM = false;

        private void Form7_Load(object sender, EventArgs e)
        {
            if (!MostrarM)
                MessageBox.Show("En este apartado se encuentra la Insersión de Datos sobre nuevas Unidades", "INFORMACIÓN",
                    MessageBoxButtons.OK);

            MostrarM = true;
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Form6 Uni = new Form6();
            Uni.Show();

            this.Hide();
        }

        private void Confi_U_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No se puede realizar este proceso, ya que el sistema no está conectado con la Base de Datos.",
                "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
