using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AVANCE_PED_GS250179_
{
    public partial class Form5 : Form
    {

        private static bool MostrarMensaje = false;

        public Form5()
        {
            InitializeComponent();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Owner.Show();

            this.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            if (!MostrarMensaje)
                MessageBox.Show("En este apartado se podrá ver el registro de transacciones.","INFORMACIÓN",
                    MessageBoxButtons.OK);

            MostrarMensaje = true;
        }
    }
}
